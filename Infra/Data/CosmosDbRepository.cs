﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.using System

using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Core.Exception;
using Core.Interface;
using Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TodoService.Infrastructure.Data
{
    public abstract class CosmosDbRepository<T> : IRepository<T>, IDocumentCollectionContext<T> where T : Entity
    {
        private readonly ICosmosDbClientFactory _cosmosDbClientFactory;

        protected CosmosDbRepository(ICosmosDbClientFactory cosmosDbClientFactory)
        {
            _cosmosDbClientFactory = cosmosDbClientFactory;
        }
        
        public virtual IList<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                return  cosmosDbClient.ReadDocumentBySql<T>(predicate).ToList();

            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }
                throw;
            }
        }


        public virtual async Task<IList<T>> GetAll(string sql) 
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document = cosmosDbClient.ReadDocumentBySql(sql);
                var arrayDocument = string.Join<Document>(",", document.ToList());                     
                return await Task.Factory.StartNew( () => JsonConvert.DeserializeObject<IList<T>>( $"[ {arrayDocument} ]"));

            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }    
        public async Task<T> GetByIdAsync(string id)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document = await cosmosDbClient.ReadDocumentAsync(id, new RequestOptions
                {
                    PartitionKey = ResolvePartitionKey(id)
                });

                return JsonConvert.DeserializeObject<T>(document.ToString());
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                entity.id = GenerateId(entity);
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document = await cosmosDbClient.CreateDocumentAsync(entity);
                return JsonConvert.DeserializeObject<T>(document.ToString());
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new EntityAlreadyExistsException();
                }

                throw;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document = await cosmosDbClient.ReplaceDocumentAsync(entity.id, entity);
                return JsonConvert.DeserializeObject<T>(document.ToString());
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document =  await cosmosDbClient.DeleteDocumentAsync(id, new RequestOptions
                {
                    PartitionKey = ResolvePartitionKey(id)
                });               

            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }

        public abstract string CollectionName { get; }
        public virtual string GenerateId(T entity) => Guid.NewGuid().ToString();
        public virtual PartitionKey ResolvePartitionKey(string entityId) => null;


    }
}

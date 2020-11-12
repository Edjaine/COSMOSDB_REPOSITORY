// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.using System

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace TodoService.Infrastructure.Data
{
    public interface ICosmosDbClient
    {
        IQueryable<Document> ReadDocumentBySql(string query);
        IQueryable<T> ReadDocumentBySql<T>(Expression<Func<T, bool>> predicate); 
        Task<DocumentCollection> ReadDocumentCollectionAsync(
            CancellationToken cancellationToken = default(CancellationToken));
        Task<Document> ReadDocumentAsync(string documentId, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Document> CreateDocumentAsync(object document, RequestOptions options = null,
            bool disableAutomaticIdGeneration = false,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Document> ReplaceDocumentAsync(string documentId, object document, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Document> DeleteDocumentAsync(string documentId, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}

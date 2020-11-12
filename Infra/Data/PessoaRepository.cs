// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.using System

using System;
using Microsoft.Azure.Documents;
using TodoService.Infrastructure.Data;
using Core.Models;
using Core.Interface;

namespace Pessoa.Infrastructure.Data
{
    public class PessoaRepository : CosmosDbRepository<Core.Models.Pessoa> , IPessoaRepository
    {
        public PessoaRepository(ICosmosDbClientFactory factory) : base(factory) { }

        public override string CollectionName { get; } = "Pessoa";
        public override string GenerateId(Core.Models.Pessoa entity) => $"{entity.idade}:{Guid.NewGuid()}";
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId.Split(':')[0]);
    }
}

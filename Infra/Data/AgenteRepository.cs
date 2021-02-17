// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.using System

using System;
using Microsoft.Azure.Documents;
using TodoService.Infrastructure.Data;
using Core.Models;
using Core.Interface;

namespace Infra.Data
{
    public class AgenteRepository<T> : CosmosDbRepository<Core.Models.Agente<T>> , Core.Interface.IAgenteRepository<T>
    {
        public AgenteRepository(ICosmosDbClientFactory factory) : base(factory) { }
        public override string CollectionName { get; } = "Agente";
        public override string GenerateId(Core.Models.Agente<T> entity) => $"{entity.tipo}:{Guid.NewGuid()}";
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId.Split(':')[0]);
    }
}

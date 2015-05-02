﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Storage.Queue;
using Microsoft.Azure.WebJobs.Host.Triggers;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Microsoft.Azure.WebJobs.Host.Queues.Listeners
{
    internal class QueueTriggerExecutor : ITriggerExecutor<IStorageQueueMessage>
    {
        private readonly ListenerExecutionContext _context;
        private readonly ITriggeredFunctionExecutor _innerExecutor;

        public QueueTriggerExecutor(ListenerExecutionContext context, ITriggeredFunctionExecutor innerExecutor)
        {
            _context = context;
            _innerExecutor = innerExecutor;
        }

        public async Task<bool> ExecuteAsync(IStorageQueueMessage value, CancellationToken cancellationToken)
        {
            Guid? parentId = QueueCausalityManager.GetOwner(value);
            return await _innerExecutor.TryExecuteAsync(parentId, value, _context, cancellationToken);
        }
    }
}

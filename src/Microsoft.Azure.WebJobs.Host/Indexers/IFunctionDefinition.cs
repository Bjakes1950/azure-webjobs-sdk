﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Reflection;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;

namespace Microsoft.Azure.WebJobs.Host.Indexers
{
    internal interface IFunctionDefinition
    {
        MethodInfo Method { get; }

        IFunctionInstanceFactory InstanceFactory { get; }

        IListenerFactory ListenerFactory { get; }
    }
}

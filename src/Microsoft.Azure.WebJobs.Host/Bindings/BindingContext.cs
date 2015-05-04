﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Azure.WebJobs.Host.Blobs;
using Microsoft.Azure.WebJobs.Host.Queues;

namespace Microsoft.Azure.WebJobs.Host.Bindings
{
    /// <summary>
    /// Context for parameter binding.
    /// </summary>
    public class BindingContext
    {
        private readonly FunctionBindingContext _functionContext;
        private readonly IReadOnlyDictionary<string, object> _bindingData;
        private readonly CancellationToken _cancellationToken;

        private AmbientBindingContext _ambientContext;
        private ValueBindingContext _valueContext;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="valueContext">The value binding context.</param>
        /// <param name="bindingData">The binding data.</param>
        public BindingContext(ValueBindingContext valueContext, IReadOnlyDictionary<string, object> bindingData)
        {
            _valueContext = valueContext;
            _bindingData = bindingData;

            _functionContext = valueContext.FunctionContext;
            _cancellationToken = valueContext.CancellationToken;
        }

        internal BindingContext(AmbientBindingContext ambientContext, CancellationToken cancellationToken)
        {
            _functionContext = ambientContext.FunctionContext;
            _bindingData = ambientContext.BindingData;
            _cancellationToken = cancellationToken;

            _ambientContext = ambientContext;
        }

        /// <summary>
        /// The instance ID of the function being bound to.
        /// </summary>
        public Guid FunctionInstanceId
        {
            get { return _functionContext.FunctionInstanceId; }
        }

        /// <summary>
        /// Gets the function <see cref="CancellationToken"/>.
        /// </summary>
        public CancellationToken FunctionCancellationToken
        {
            get { return _functionContext.FunctionCancellationToken; }
        }

        /// <summary>
        /// Gets the console output.
        /// </summary>
        public TextWriter ConsoleOutput
        {
            get { return _functionContext.ConsoleOutput; }
        }

        /// <summary>
        /// Gets the binding data.
        /// </summary>
        public IReadOnlyDictionary<string, object> BindingData
        {
            get { return _bindingData; }
        }

        /// <summary>
        /// Gets the <see cref="CancellationToken"/> to use.
        /// </summary>
        public CancellationToken CancellationToken
        {
            get { return _cancellationToken; }
        }

        internal AmbientBindingContext AmbientContext
        {
            get
            {
                if (_ambientContext == null)
                {
                    _ambientContext = new AmbientBindingContext(_functionContext, _bindingData);
                }

                return _ambientContext;
            }
        }

        /// <summary>
        /// Gets the value binding context.
        /// </summary>
        public ValueBindingContext ValueContext
        {
            get
            {
                if (_valueContext == null)
                {
                    _valueContext = new ValueBindingContext(_functionContext, _cancellationToken);
                }

                return _valueContext;
            }
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ResponseFilterExample
{
    public class JsonOptionsInjector : IConfigureOptions<MvcNewtonsoftJsonOptions>
    {
        private readonly IServiceProvider _serviceProvider;

        public JsonOptionsInjector(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Configure(MvcNewtonsoftJsonOptions options)
        {
            options.SerializerSettings.ContractResolver = new ContractResolver(_serviceProvider);
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}
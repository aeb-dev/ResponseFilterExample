using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ResponseFilterExample
{
    public class ContractResolver : CamelCasePropertyNamesContractResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public ContractResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            property.ShouldSerialize = (instance) =>
            {
                IActionContextAccessor actionAccessor = _serviceProvider.GetRequiredService<IActionContextAccessor>();
                if (actionAccessor == null || actionAccessor.ActionContext == null)
                {
                    return true;
                }

                ControllerActionDescriptor actionDescriptor = (ControllerActionDescriptor)actionAccessor.ActionContext.ActionDescriptor;

                MethodInfo method = actionDescriptor.ControllerTypeInfo.DeclaredMethods.First(x => x.Name == actionDescriptor.ActionName);
                CustomAttributeData filterModeAttribute = method.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(ResponseFilterModeAttribute));
                if (filterModeAttribute == null)
                {
                    return true;
                }

                FilterMode filterMode = (FilterMode) filterModeAttribute.ConstructorArguments[0].Value;
                bool hasField = method.CustomAttributes.Any(x => x.AttributeType == typeof(ResponseFilterAttribute) && ((IEnumerable<CustomAttributeTypedArgument>)x.ConstructorArguments[0].Value).Any(y => (string)y.Value == member.Name));
                bool decision = filterMode == FilterMode.Include ? hasField : !hasField;
                return decision;
            };

            return property;
        }
    }
}
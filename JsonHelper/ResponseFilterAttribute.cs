using System;
using System.Collections.Generic;

namespace ResponseFilterExample
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ResponseFilterAttribute : Attribute
    {
        private readonly IEnumerable<string> _propertyNameList;

        public ResponseFilterAttribute(params string[] propertyNameList)
        {
            _propertyNameList = propertyNameList;
        }
    }
}

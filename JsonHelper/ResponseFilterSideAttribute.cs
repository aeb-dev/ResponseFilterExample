using System;

namespace ResponseFilterExample
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ResponseFilterModeAttribute : Attribute
    {
        private readonly FilterMode _mode;

        public ResponseFilterModeAttribute(FilterMode mode)
        {
            _mode = mode;
        }
    }
}
using System;

namespace Cqrs.ApiGenerator
{
    internal class QueryEndpointInfo
    {
        public string ControllerName { get; set; }
        public Type QueryType { get; set; }
        public Type ReturnType { get; set; }
        
        public string ActionName { get; set; }
    }
}
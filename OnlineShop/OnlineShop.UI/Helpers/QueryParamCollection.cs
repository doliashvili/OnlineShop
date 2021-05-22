using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OnlineShop.UI.Helpers
{
    public sealed class QueryParamCollection : Dictionary<string, QueryParamValue>
    {
        public string ToQueryString()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);

            foreach (var (key, value) in this)
            {
                if (value.HasValue)
                    query.Add(key, value.ToString());
            }

            return query.ToString() ?? string.Empty;
        }
    }
}
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

            foreach (var item in this)
            {
                var value = item.Value;
                if (value.Value is not null)
                    query.Add(item.Key, value.ToString());
                else
                if (value.Values is not null)
                {
                    foreach (var v in value.Values)
                    {
                        query.Add(item.Key, v.ToString());
                    }
                }
            }

            return query.ToString() ?? string.Empty;
        }
    }
}
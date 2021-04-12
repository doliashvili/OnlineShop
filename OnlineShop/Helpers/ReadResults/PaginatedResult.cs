﻿using System;
using System.Collections.Generic;

namespace Helpers.ReadResults
{
    public class PaginatedResult<T> : Result
    {
        public PaginatedResult(List<T> data) => Data = data;
        public List<T> Data { get; set; }

        internal PaginatedResult(bool succeeded, List<T> data = default, List<string> messages = null, long count = 0, int page = 1, int pageSize = 10)
        {
            Data = data;
            Page = page;
            Succeeded = succeeded;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }
        
        public static PaginatedResult<T> Failure(List<string> messages = null) 
            => new (false, default, messages);

        public static PaginatedResult<T> Success(List<T> data, long count, int page, int pageSize) 
            => new (true, data, null, count, page, pageSize);

        public int Page { get; set; }
        public int TotalPages { get; set; }

        public long TotalCount { get; set; }

        public bool HasPreviousPage => Page > 1;

        public bool HasNextPage => Page < TotalPages;
    }
}

﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Extensions
{
    public static class IQueryableExtension
    {
        public static async Task<PagedResult<T>> GetPaged<T>(this IQueryable<T> query,
                                                 int page, int pageSize, int skip = -1) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            // If no skip value is provided
            if(skip == -1)
            {
                skip = (page - 1) * pageSize;
            }

            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
    }
}

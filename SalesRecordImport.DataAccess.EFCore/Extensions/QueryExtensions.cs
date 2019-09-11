using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Catel;
using Microsoft.EntityFrameworkCore;
using SalesRecordImport.DataAccess.Options.Abstract;
using SalesRecordImport.DataAccess.QueryResults;


namespace SalesRecordImport.DataAccess.Extensions
{
    public static class QueryExtensions
    {
        public static async Task<IPagedResult<TModel>> GetPageAsync<TModel>(this IQueryable<TModel> query,
            IPaginationOptions paginationOptions)
        {
            var page = paginationOptions?.Page ?? 1;
            var totalCount = await query.CountAsync();
            var size = paginationOptions?.Size ?? totalCount;

            var result = await query.Skip((page - 1) * size).Take(size).ToListAsync();

            return new PagedResult<TModel>(result, totalCount);
        }

        public static IQueryable<TModel> Order<TModel>(this IQueryable<TModel> query,
                                                            IOrderOptions orderOptions,
                                                            IReadOnlyDictionary<string, Expression<Func<TModel, object>>> orderingMap)
        {
            Argument.IsNotNull(nameof(orderingMap), orderingMap);

            if (!string.IsNullOrWhiteSpace(orderOptions.OrderColumn) &&
                orderingMap.TryGetValue(orderOptions.OrderColumn, out var orderFunction))
            {
                return orderOptions.OrderAscending ? query.OrderBy(orderFunction)
                      : query.OrderByDescending(orderFunction);
            }

            return query;
        }
    }
}
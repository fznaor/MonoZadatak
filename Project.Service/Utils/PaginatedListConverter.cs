using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Utils
{
    public class PaginatedListConverter<TSource, TDestination> : ITypeConverter<PaginatedList<TSource>, PaginatedList<TDestination>> where TSource : class where TDestination : class
    {
        public PaginatedList<TDestination> Convert(PaginatedList<TSource> source, PaginatedList<TDestination> destination, ResolutionContext context)
        {
            var collection = context.Mapper.Map<List<TSource>, List<TDestination>>(source.ToList());
            return new PaginatedList<TDestination>(collection, source.PageIndex, source.TotalPages);
        }
    }
}

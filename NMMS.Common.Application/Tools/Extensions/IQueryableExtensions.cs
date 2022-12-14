namespace NMMS.Common.Application.Tools.Extensions
{
    public static class IQueryableExtensions
    {
        public static PagedData<T> ToPagedData<T>(this IQueryable<T> query, int page, int offset)
        {
            var count = query.Count();
            var list = query.Skip((page - 1) * offset).Take(offset);
            var data = new PagedData<T>()
            {
                Data = list,
                TotalItemCount = count,
                Page = page,
                Offset = offset,
                PageCount = count % offset == 0 ? count / offset : count / offset + 1
            };
            return data;
        }
    }
}

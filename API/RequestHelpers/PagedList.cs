namespace API.RequestHelpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            this.MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count/(double)pageSize)
            };
            AddRange(items);
        }
        public MetaData MetaData { get; set; }

        public static async Task<PagedList<T> > ToPagedList(IQueryable<T> query, 
        int pageNumber, int pageSize)
        {
            var count = query.Count();
            var items = query.Skip((pageNumber-1) * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(new PagedList<T>(items, count, pageNumber, pageSize));
        }
    }
}
namespace Utilities.Framework.Pagination
{

    public class PagedList<T> : IPagedList<T>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int IndexFrom { get; set; }

        public IEnumerable<T> Items { get; set; }

        public bool HasPreviousPage => this.PageIndex - this.IndexFrom > 0;

        public bool HasNextPage => this.PageIndex - this.IndexFrom + 1 < this.TotalPages;


        public PagedList(IEnumerable<T> source, int pageNumber, int pageSize, int totalCount)
        {
            this.PageIndex = pageNumber;
            this.PageSize = pageSize;
            this.IndexFrom = pageNumber * pageSize;

            var itemList = source.ToList();
            this.TotalCount = totalCount;
            this.TotalPages = (int)Math.Ceiling(this.TotalCount / (double)this.PageSize);

            this.Items = itemList.Skip(this.IndexFrom).Take(this.PageSize).ToList();
        }

        public PagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            this.PageIndex = pageNumber;
            this.PageSize = pageSize;
            this.IndexFrom = pageNumber * pageSize;
            this.TotalCount = source.Count();
            this.TotalPages = (int)Math.Ceiling(this.TotalCount / (double)this.PageSize);
            this.Items = source.Skip(this.IndexFrom).Take(this.PageSize).ToList();
        }

        public PagedList()
        {
            this.Items = new T[0];
        }
    }


}
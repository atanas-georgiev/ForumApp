namespace ForumApp.Mvc.Models.Shared
{
    using System.Collections.Generic;

    public class PagableListViewModel<T>
    {
        public int Page { get; set; }

        public int Pages { get; set; }

        public int ParentId { get; set; }

        public string Title { get; set; }

        public IEnumerable<T> Data { get; set; }

        public int NextPage
        {
            get
            {
                if (this.Page >= this.Pages)
                {
                    return 1;
                }

                return this.Page + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                if (this.Page <= 1)
                {
                    return this.Pages;
                }

                return this.Page - 1;
            }
        }
    }
}
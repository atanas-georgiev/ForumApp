namespace ForumApp.Mvc.Models.Forum
{
    using System.Collections.Generic;

    public class ForumListViewModel
    {
        public int Page { get; set; }

        public int Pages { get; set; }

        public IEnumerable<ForumViewModel> Forums { get; set; }

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
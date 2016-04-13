namespace ForumApp.Mvc.Models.Forum
{
    using System.ComponentModel.DataAnnotations;

    using ForumApp.Data.Models;
    using ForumApp.Mvc.Infrastructure.Mappings;

    public class ForumListViewModel : IMapFrom<Forum>
    {
        [MinLength(5)]
        [MaxLength(150)]
        public string Title { get; set; }
    }
}
namespace ForumApp.Mvc.Models.Forum
{    
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using ForumApp.Data.Models;
    using ForumApp.Mvc.Infrastructure.Mappings;

    public class ForumViewModel : IMapFrom<Forum>, IHaveCustomMappings
    {
        [MinLength(5)]
        [MaxLength(150)]
        public string Title { get; set; }

        public int NumberPosts { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Forum, ForumViewModel>(string.Empty)
                .ForMember(m => m.NumberPosts, opt => opt.MapFrom(c => c.Posts.Count));
        }
    }
}
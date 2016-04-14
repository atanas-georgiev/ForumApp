namespace ForumApp.Mvc.Models.Post
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using ForumApp.Data.Models;
    using ForumApp.Mvc.Infrastructure.Mappings;

    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(1000)]
        public string Text { get; set; }

        [MaxLength(150)]
        public string Author { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }

        public int NumberComments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Post, PostViewModel>(string.Empty)
                .ForMember(m => m.NumberComments, opt => opt.MapFrom(c => c.Comments.Count));
        }
    }
}
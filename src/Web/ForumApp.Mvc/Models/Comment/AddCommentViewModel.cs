namespace ForumApp.Mvc.Models.Comment
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ForumApp.Mvc.Infrastructure.Mappings;

    public class AddCommentViewModel : IMapFrom<Data.Models.Comment>
    {
        public AddCommentViewModel()
        {
            this.CreatedDateTime = DateTime.UtcNow;
        }

        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        
        [MaxLength(150)]
        public string Author { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
    }
}
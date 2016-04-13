namespace ForumApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Forum
    {
        public int Id { get; set; }

        [MinLength(5)]
        [MaxLength(150)]
        public string Titile { get; set; }
    }
}

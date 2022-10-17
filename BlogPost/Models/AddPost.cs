using BlogPost.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Models
{
    public class AddPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("Author")]
        public String? AuthorId { get; set; }
        public BlogPostUser Author { get; set; }
    }
}

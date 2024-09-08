using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(280, ErrorMessage ="Title can't be more than 280 characters")]
        public string Title { get; set; }=string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Content be more than 280 characters")]
        public string Content { get; set; }= string.Empty;
    }
}

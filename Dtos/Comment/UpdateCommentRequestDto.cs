using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;
namespace api.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters long.")]
        [MaxLength(200, ErrorMessage = "Content cannot exceed 200 characters.")]
        public string Content { get; set; } = string.Empty;
    }
}
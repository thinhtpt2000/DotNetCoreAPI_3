using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SocialMedia.Core.DTOs
{
    public class PostDto
    {
        /// <summary>
        /// Auto-generated id for post entity
        /// </summary>
        public int Id { get; set; }
        
        public int UserId { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Description { get; set; }
        
        public string Image { get; set; }
    }
}

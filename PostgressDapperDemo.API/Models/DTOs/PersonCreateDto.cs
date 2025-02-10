using System.ComponentModel.DataAnnotations;

namespace PostgressDapperDemo.API.Models.DTOs
{
    public class PersonCreateDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(30)]
        public string Email { get; set; } = string.Empty;
    }
}

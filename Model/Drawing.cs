using System.ComponentModel.DataAnnotations;

namespace autoCadApiDevelopment.Models
{
    public class Drawing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

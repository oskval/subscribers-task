using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Subscriber
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public DateTime SubscriptionDate { get; set; }
    }
}

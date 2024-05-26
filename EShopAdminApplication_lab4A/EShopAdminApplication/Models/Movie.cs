using System.ComponentModel.DataAnnotations;

namespace EShopAdminApplication.Models
{
    public class Movie
    {
        //public Guid Id { get; set; }
        [Required]
        public string MovieName { get; set; }
        [Required]
        public string MovieDescription { get; set; }
        [Required]
        public string MovieImage { get; set; }
        [Required]
        public double Rating { get; set; }

    }
}

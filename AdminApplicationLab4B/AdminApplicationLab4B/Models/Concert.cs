using System.ComponentModel.DataAnnotations;

namespace AdminApplicationLab4B.Models
{
    public class Concert
    {
        [Required]
        public string ConcertName { get; set; }
        [Required]
        public string ConcertDescription { get; set; }
        [Required]
        public string ConcertImage { get; set; }
        [Required]
        public double Rating { get; set; }
    }
}

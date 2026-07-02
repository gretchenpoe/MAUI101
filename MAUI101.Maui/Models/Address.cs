using System.ComponentModel.DataAnnotations;

namespace MAUI101.Maui.Models
{
    public class Address
    {
        [Required (ErrorMessage = "Street address is required.")]
        public string StreetAddress { get; set; }
        [Required (ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required (ErrorMessage = "State is required.")]
        public string State { get; set; }
        [Required (ErrorMessage = "Zip code is required.")]
        public string ZipCode { get; set; }

    }
}
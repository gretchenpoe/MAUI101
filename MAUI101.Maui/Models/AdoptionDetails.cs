using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MAUI101.Maui.Models
{
    public class AdoptionDetails
    {
        [Required (ErrorMessage = "Pet is required.")]
        public Pet Pet { get; set; }
        
        [Required (ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required (ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        [Required (ErrorMessage = "Date of birth is required.")]
        public DateTime BirthDate { get; set; }
        public Address Address { get; set; } = new ();
        [Required (ErrorMessage = "Phone number is required.")] 
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Pet name is required.")]
        public string PetName { get; set; }
    }
}
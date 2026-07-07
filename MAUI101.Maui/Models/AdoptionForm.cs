using System.ComponentModel.DataAnnotations;
using SQLite;

namespace MAUI101.Maui.Models
{
    [Table("adoptionForms")]
    public class AdoptionForm
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Required (ErrorMessage = "Pet is required.")]
        public string PetId { get; set; }
        
        [Required (ErrorMessage = "First name is required.")]
        [SQLite.MaxLength(100)]
        public string FirstName { get; set; }
        [SQLite.MaxLength(100)]
        public string? MiddleName { get; set; }
        [Required (ErrorMessage = "Last name is required.")]
        [SQLite.MaxLength(100)]
        public string LastName { get; set; }
        [Required (ErrorMessage = "Date of birth is required.")]
        public DateTime BirthDate { get; set; }
        [Required (ErrorMessage = "Street address is required.")]
        public string StreetAddress { get; set; }
        [Required (ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required (ErrorMessage = "State is required.")]
        public string State { get; set; }
        [Required (ErrorMessage = "Zip code is required.")]
        public string ZipCode { get; set; }
        [Required (ErrorMessage = "Phone number is required.")] 
        [SQLite.MaxLength(15)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [SQLite.MaxLength(254)]
        public string Email { get; set; }

        [Required (ErrorMessage = "Pet name is required.")]
        [SQLite.MaxLength(100)]
        public string PetName { get; set; }
    }
}
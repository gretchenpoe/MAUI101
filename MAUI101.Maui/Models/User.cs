using System.ComponentModel.DataAnnotations;
using SQLite;

namespace MAUI101.Maui.Models
{
    [Table("users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
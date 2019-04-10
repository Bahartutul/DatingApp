using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegister
    {
        [Required]
        public string username { get; set; }
        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="Must be 4 to 8 character")]
        public string password{get;set;}
    }
}
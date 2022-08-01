using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace ExpenseTrackerDomain.Models
{
    public class User : MongoEntity
    {        
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }


        public string FirebaseCloudMessagingToken { get; set; }
    }
}
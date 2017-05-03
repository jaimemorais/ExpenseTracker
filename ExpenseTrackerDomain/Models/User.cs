using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace ExpenseTrackerDomain.Models
{
    public class User : MongoEntity
    {        
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
    }
}
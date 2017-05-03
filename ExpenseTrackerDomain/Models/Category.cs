using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace ExpenseTrackerDomain.Models
{
    public class Category : MongoEntity
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string UserName;
    }
}
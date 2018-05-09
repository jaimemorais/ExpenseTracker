using System;

namespace ExpenseTrackerApp.Model
{
    [Serializable]
    public class Category 
    {
        public string Id { get; set; }

        public string UserName { get; set; }        

        public string Name { get; set; }

    }

}
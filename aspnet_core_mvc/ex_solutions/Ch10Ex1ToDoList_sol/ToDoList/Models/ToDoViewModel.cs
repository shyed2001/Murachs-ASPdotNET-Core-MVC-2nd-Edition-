using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ToDoList.Models
{
    public class ToDoViewModel
    {
        public Filters Filters { get; set; } = new Filters("all-all-all");
        public List<Status> Statuses { get; set; } = new List<Status>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public Dictionary<string, string> DueFilters { get; set; } = new Dictionary<string, string>();
        public List<ToDo> Tasks { get; set; } = new List<ToDo>();

        public ToDo CurrentTask { get; set; } = new ToDo();  // used for Add

        //[ValidateNever]
        //public Filters Filters { get; set; } = null!;
        //[ValidateNever]
        //public List<Status> Statuses { get; set; } = null!;
        //[ValidateNever]
        //public List<Category> Categories { get; set; } = null!;
        //[ValidateNever]
        //public Dictionary<string, string> DueFilters { get; set; } = null!;
        //[ValidateNever]
        //public List<ToDo> Tasks { get; set; } = null!;

        //public ToDo CurrentTask { get; set; } = null!;  // used for Add
    }
}

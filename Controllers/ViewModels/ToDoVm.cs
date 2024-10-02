namespace TODO.API.Controllers.ViewModels
{
    public class ToDoVm
    {
        public string TaskName { get; set; }
        public string Description { get; set; }

        public PersonVm Person { get; set; }
    }
}

namespace TODO.API.Models
{
    public class TODOModel
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedTask { get; set; }
        public DateTime? EndTask { get; set; }
        public DateTime? UpdateTask { get; set; }
        public int PersonId { get; set; }
    }
}

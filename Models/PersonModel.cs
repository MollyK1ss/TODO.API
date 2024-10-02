namespace TODO.API.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateBirth { get; set; }
        public string Gender { get; set; }

        public virtual List<TODOModel> tODOModels { get; set; }
        public virtual DocumentModel Document { get; set; }
    }
}

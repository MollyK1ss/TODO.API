namespace TODO.API.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Series { get; set; }
        public int Number { get; set; }
        public DateTime DateIssue { get; set; }
        public string Organization { get; set; }
        public string CodeOrg { get; set; }
        public string PlaceBirth { get; set; }
        
    }
}

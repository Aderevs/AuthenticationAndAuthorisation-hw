namespace AuthenticationAndAuthorization_hw.Logic.Notes
{
    public class Note
    {
        public string Tittle {  get; set; }
        public string Content { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime CreateTime { get; set; }
        public Note()
        {
            CreateTime = DateTime.Now;
        }
    }
}

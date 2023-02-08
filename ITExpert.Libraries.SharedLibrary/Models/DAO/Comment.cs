namespace ITExpert.Libraries.SharedLibrary.Models.DAO
{
    public sealed class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int TodoId { get; set; }
    }
}

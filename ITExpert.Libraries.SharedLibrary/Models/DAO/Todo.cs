using ITExpert.Libraries.SharedLibrary.Enums;

namespace ITExpert.Libraries.SharedLibrary.Models.DAO
{
    public sealed class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public TodoCategory Category { get; set; }
        public TodoColor Color { get; set; }
        public DateTime CreationDateTime { get; set; }
        public List<Comment> Comments { get; set; }
    }
}

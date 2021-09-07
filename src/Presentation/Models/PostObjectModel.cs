using System.Text.Json;

namespace Presentation.Models
{
    public class PostObjectModel
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
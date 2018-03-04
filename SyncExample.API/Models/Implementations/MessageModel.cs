namespace SyncExample.API.Models.Implementations
{
    public class MessageModel : IMessageModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public int? ClientId { get; set; }
    }
}

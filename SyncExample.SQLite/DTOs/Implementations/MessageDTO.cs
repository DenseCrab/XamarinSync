using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.SQLite.DTOs.Implementations
{
    public class MessageDTO : IMessageDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public int ApiId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset? SyncedOn { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.SQLite.DTOs
{
    public interface IBaseDTO
    {
        int Id { get; set; }
        int ApiId { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
        DateTimeOffset? SyncedOn { get; set; }
        DateTimeOffset? CreatedOn { get; set; }
        DateTimeOffset? UpdatedOn { get; set; }
    }
}

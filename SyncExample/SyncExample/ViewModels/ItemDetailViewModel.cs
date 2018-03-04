using SyncExample.SQLite.DTOs.Implementations;

namespace SyncExample.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public MessageDTO Item { get; set; }
        public ItemDetailViewModel(MessageDTO item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}

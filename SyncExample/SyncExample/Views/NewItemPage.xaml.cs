using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SyncExample.SQLite.DTOs.Implementations;

namespace SyncExample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public MessageDTO Message { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Message = new MessageDTO
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Message);
            await Navigation.PopModalAsync();
        }
    }
}
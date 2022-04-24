using ChatAppClient.Model;
using ChatAppClient.Utilities;
using ChatAppClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatAppClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        private string userName;
       
        ChatViewModel chatView;
        public ChatPage(string user)
        {
            userName = user;
            //creating the chatview model
             chatView = new ChatViewModel(user);
            

            //binding the chatview model class or object to the chat page
            this.BindingContext = chatView;

            InitializeComponent();
        }
         private void BtnSend_Clicked(Object sender, EventArgs e)
        {
            txtMessage.Text = "";

        }
        

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatAppClient
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            var RegisterPage = new RegisterPage();
            RegisterPage.isNewUser = false;
            Navigation.PushAsync(RegisterPage);
        }
        private void BtnMainRegister_Clicked(object sender, EventArgs e)
        {
            var RegisterPage = new RegisterPage();
            RegisterPage.isNewUser = true;
            Navigation.PushAsync(RegisterPage);
        }
    }
}

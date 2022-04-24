using ChatAppClient.Model;
using ChatAppClient.Repository;
using ChatAppClient.Utilities;
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
    public partial class RegisterPage : ContentPage
    {
        public bool isNewUser;
        private AppUser appUser;

        public RegisterPage()
        {
            InitializeComponent();
           
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ClearScreen();
            if (isNewUser)
            {
                btnLoginRegister.IsVisible = false;
                btnRegister.IsVisible = true;
                userNamePanel.IsVisible = true;
            }
            else
            {
                userNamePanel.IsVisible = false;
                btnLoginRegister.IsVisible = true;
                btnRegister.IsVisible = false;
            }
        }
        //login button is clicked
        private void BtnLoginRegister_Clicked(object sender, EventArgs e)
        { 
            string email = txtEmail.Text;

            GetAppUser(email.ToString());

            if(appUser != null)
            {
                var chatPage = new ChatPage(appUser.UserName);
               
                ClearScreen();
                this.Navigation.PushAsync(chatPage);
            }
        }
        private void BtnRegister_Clicked(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string email = txtEmail.Text;

            //creating the appuser
            var appUser = new AppUser { ID = 0, UserName = userName, Email = email };            
            AddAppUser(appUser);
            var chatPage = new ChatPage(appUser.UserName);
            
            ClearScreen();
            this.Navigation.PushAsync(chatPage);
                

        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            
        }

        private async void GetAppUser(string Email)
        {
            
            try
            {
                AppUserRepsoitory ar = new AppUserRepsoitory();
                appUser = await ar.GetAppUserByEmail(Email);
                                
            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                
                await DisplayAlert("Error Logging in the user:", sb.ToString(), "Ok");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.GetBaseException().Message.Contains("connection with the server"))
                    {

                        await DisplayAlert("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Error", "If the problem persists, please call your system administrator.", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("General Error", "If the problem persists, please call your system administrator.", "Ok");
                }
            }
            
        }

        private async void AddAppUser(AppUser user)
        {
            try
            {
                AppUserRepsoitory r = new AppUserRepsoitory();
                if (user.ID == 0)
                {
                    await r.PostAppUser(user);
                    appUser = user;
                }
            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }

                await DisplayAlert("Error Regestering the user:", sb.ToString(), "Ok");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.GetBaseException().Message.Contains("connection with the server"))
                    {

                        await DisplayAlert("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Error", "If the problem persists, please call your system administrator.", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("General Error", "If the problem persists, please call your system administrator.", "Ok");
                }
            }
        }

        private void ClearScreen()
        {
            txtEmail.Text = "";
            txtUserName.Text = "";
        }
    }
}
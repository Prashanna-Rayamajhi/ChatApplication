using ChatAppClient.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatAppClient
{
    public partial class App : Application
    {
        
        public App()
        {
           
            InitializeComponent();

            NavigationPage nav = new NavigationPage(new MainPage());
            MainPage = nav;

           
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

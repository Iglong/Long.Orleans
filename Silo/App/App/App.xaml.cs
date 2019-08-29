using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App.Services;
using App.Views;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            new ServiceCollection().InjectionServices();
            DependencyService.Register<MockDataStore>();
            MainPage =new NavigationPage( new AppShell());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

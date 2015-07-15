
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Json;
using trade.ui;
using Xamarin.Forms;

namespace trade
{
	public class App : Application
	{
        
        public App ()
		{

            MainPage = new MainPage { };
        }



        public ContentPage getMainPage()
        {
            return (ContentPage)MainPage;
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}

    
}


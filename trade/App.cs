
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Json;
using trade.ui;
using trade.ui.page;
using Xamarin.Forms;

namespace trade
{
	public class App : Application
	{
        
        public App ()
		{

            MainPage = new RootPage { };
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

    public class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            var menuPage = new MenuPage();

            menuPage.Menu.ItemSelected += (sender, e) =>
            {
                
                NavigateTo(new ui.page.MenuItem((ui.page.MenuItem)e.SelectedItem) as ui.page.MenuItem, () =>
                {
                    //((ListView)sender).SelectedItem = null;
                });
                
            };
            Master = menuPage;
            Page startPage;
            if((bool)Prefs.get(PrefsConst.LOGED_IN, false))
            {
                startPage = new NavigationPage(new MainPage { });
            }
            else
            {
                startPage = new NavigationPage(new LoginPage{ });
            }
            Detail = startPage;

            MessagingCenter.Subscribe<ui.page.MenuItem> (this, "OpenInDetail", (sender) => {
                NavigateTo(sender, null);
            });
            
                         
        }

        public void NavigateTo(ui.page.MenuItem menu, Action lambda)
        {
            Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);

            Detail = new NavigationPage(displayPage);

            IsPresented = false;

            if(lambda!=null)lambda.Invoke();
        }
    }

}


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace trade.ui.page
{
    public class MenuPage : ContentPage
    {

        public ListView Menu { get; set; }

        public MenuPage()
        {
           
            Title = "Navigation"; // The Title property must be set.
            BackgroundColor = Color.FromHex("333333");

            Menu = new NavigationListView();

           
            var layout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            layout.Children.Add(Menu);

            Content = layout;
        }

    }

    
    public class NavigationListView : ListView
    {
        public NavigationListView()
        {
            List<MenuItem> data = new MenuListData();

            ItemsSource = data;
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;

            var cell = new DataTemplate(typeof(TextCell));
            cell.SetBinding(TextCell.TextProperty, "Title");
            //cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");

            ItemTemplate = cell;
        }
    }

    public class MenuListData : List<MenuItem>
    {
        public MenuListData()
        {
            this.Add(new MainPageMI());

            this.Add(new LoginMI());



        }
    }

    public class MenuItem
    {
        public MenuItem()
        {

        }
        public MenuItem(MenuItem m)
        {
            Title = m.Title;
            IconSource = m.IconSource;
            TargetType = m.TargetType;
        }
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }

    public class MainPageMI : MenuItem
    {
       public MainPageMI()
        {
            Title = "MainPage";
            IconSource = "contracts.png";
            TargetType = typeof(MainPage);
        }
    }

    public class LoginMI : MenuItem
    {
        public LoginMI()
        {
            Title = "Login";
            IconSource = "contracts.png";
            TargetType = typeof(LoginPage);
        }
    }


}

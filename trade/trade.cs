
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Json;
using Xamarin.Forms;

namespace trade
{
	public class App : Application
	{
        ObservableCollection<FilmData> filmsList;
        public App ()
		{

            var search = new Editor {};

            var listView = new ListView
            {
                RowHeight = 40
            };
            filmsList = new ObservableCollection<FilmData>();
            filmsList.Add(new FilmData
            {
                Title = "Start typing for search"
            });
            listView.ItemsSource = filmsList;
            search.TextChanged += async (object sender, TextChangedEventArgs e) =>
            {
                var data = new Dictionary<string, string>();
                data.Add("s", e.NewTextValue);
                string result = await Api.getInstanse().get(ApiRequest.FIND_MOVIE, data);
                var resultJson = JsonObject.Parse(result);
                if (resultJson.ContainsKey("Search"))
                {
                    filmsList.Clear();
                    var searchResults = resultJson["Search"];
                    
                    var itemsCollection = new List<string>();
                    for (int i = 0; i < searchResults.Count; i++)
                    {
                        if (searchResults[i].ContainsKey("Title"))
                        {
                            filmsList.Add(new FilmData {
                                Title = searchResults[i]["Title"],
                                Year = searchResults[i]["Year"],
                                Type = searchResults[i]["Type"],
                                ImdbID = searchResults[i]["imdbID"],
                            });
                        }
                    }                   
                }
            };

            var mainPage = new ContentPage
            {

                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        search,
                        listView
                    }
                }
            };

            listView.ItemTemplate = new DataTemplate(typeof(TextCell));
            listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Title");
            
            listView.ItemTapped += async (object sender, ItemTappedEventArgs e) =>{
                ((ListView)sender).SelectedItem = null;
                FilmData filmData = (FilmData)e.Item;
                var prms = new Dictionary<string, string>();
                prms.Add("i", filmData.ImdbID);
                prms.Add("plot", "full");


                //Create Alert

#if __ANDROID__

                var context = Xamarin.Forms.Forms.Context;
                
                var dlgAlert = (new Android.App.AlertDialog.Builder(context)).Create();
                dlgAlert.SetTitle("List View Alert Dialog");
                var dialogListView = new Android.Widget.ListView(context);
                Android.Widget.ArrayAdapter dialogAdapter = new Android.Widget.ArrayAdapter(context, Android.Resource.Layout.SimpleListItem1);
                dialogAdapter.Add("Year: " + filmData.Year);
                dialogAdapter.Add("Type: " + filmData.Type);

                dialogListView.Adapter = dialogAdapter;

                dlgAlert.SetView(dialogListView);
                dlgAlert.SetButton("Close", handllerCancelButton);
                dlgAlert.Show();

                string filmResult = await Api.getInstanse().get(ApiRequest.FIND_MOVIE, prms);
                var resultJson = JsonObject.Parse(filmResult);
                if (resultJson.ContainsKey("Plot"))
                {
                    filmData.Plot = resultJson["Plot"];
                    dialogAdapter.Add(filmData.Plot);
                    dialogAdapter.NotifyDataSetChanged();

                }

            };
#else
            string filmResult = await Api.getInstanse().get(ApiRequest.FIND_MOVIE, prms);
                var resultJson = JsonObject.Parse(filmResult);
            if (resultJson.ContainsKey("Plot")){
                    filmData.Plot = resultJson["Plot"];

                    await mainPage.DisplayAlert(filmData.Title, "Year: " + filmData.Year + "\n" + "\n" + "Type: " + filmData.Type + "\n" + "\n" + filmData.Plot, "close");
                }                           
                
            };
#endif




            MainPage = mainPage;
        }

#if __ANDROID__
        void handllerCancelButton(object sender, Android.Content.DialogClickEventArgs e)
        {
            Android.App.AlertDialog objAlertDialog = sender as Android.App.AlertDialog;
            objAlertDialog.Cancel();
        }
#endif


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

    public class FilmData
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string ImdbID { get; set; }
        public string Type { get; set; }        
        public string Plot { get; set; }

    }
}


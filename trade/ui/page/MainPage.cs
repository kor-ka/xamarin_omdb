using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Json;
using trade.api.entity;
using Xamarin.Forms;

namespace trade.ui
{
	public class MainPage : ContentPage
	{
        ObservableCollection<FilmData> filmsList;
        public MainPage()
		{
            Title = "FilmSearch";

            var search = new Editor { };

            var listView = new ListView
            {
                RowHeight = 40
            };
            filmsList = new ObservableCollection<FilmData>();
            filmsList.Add(new FilmData
            {
                Title = "Start typing for search",
                Touchable = false,
            });
            listView.ItemsSource = filmsList;
            search.TextChanged += async (object sender, TextChangedEventArgs e) =>
            {

                var data = new Dictionary<string, string>();
                data.Add("s", e.NewTextValue);
                JsonValue result = await Api.getInstanse().get(ApiRequest.FIND_MOVIE, data);
                JsonValue resultJson = result;
                if (resultJson.ContainsKey("Search"))
                {
                    filmsList.Clear();
                    var searchResults = resultJson["Search"];

                    var itemsCollection = new List<string>();
                    for (int i = 0; i < searchResults.Count; i++)
                    {
                        if (searchResults[i].ContainsKey("Title"))
                        {
                            filmsList.Add(new FilmData
                            {
                                Title = searchResults[i]["Title"],
                                Year = searchResults[i]["Year"],
                                Type = searchResults[i]["Type"],
                                ImdbID = searchResults[i]["imdbID"],
                            });
                        }
                    }
                }
            };

            listView.ItemTemplate = new DataTemplate(typeof(TextCell));
            listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Title");

            listView.ItemTapped += async (object sender, ItemTappedEventArgs e) => {
                ((ListView)sender).SelectedItem = null;
                FilmData filmData = (FilmData)e.Item;
                if (filmData.Touchable)
                {
                    var prms = new Dictionary<string, string>();
                    prms.Add("i", filmData.ImdbID);
                    prms.Add("plot", "full");

                    /////////////////////////
                    #if __ANDROID__
                    /////////////////////////
                    var context = Xamarin.Forms.Forms.Context;

                    var dlgAlert = (new Android.App.AlertDialog.Builder(context)).Create();
                    dlgAlert.SetTitle(filmData.Title);
                    var dialogListView = new Android.Widget.ListView(context);
                    Android.Widget.ArrayAdapter dialogAdapter = new Android.Widget.ArrayAdapter(context, Android.Resource.Layout.SimpleListItem1);
                    dialogAdapter.Add("Year: " + filmData.Year);
                    dialogAdapter.Add("Type: " + filmData.Type);

                    dialogListView.Adapter = dialogAdapter;

                    dlgAlert.SetView(dialogListView);
                    dlgAlert.SetButton("Close", handllerCancelButton);
                    dlgAlert.Show();

                    JsonValue filmResult = await Api.getInstanse().get(ApiRequest.FIND_MOVIE, prms);
                    JsonValue resultJson = filmResult;
                    if (resultJson.ContainsKey("Plot"))
                    {
                        filmData.Plot = resultJson["Plot"];
                        dialogAdapter.Add(filmData.Plot);
                        dialogAdapter.NotifyDataSetChanged();

                    }
                    /////////////////////////
                    #else
                    /////////////////////////
                    JsonValue filmResult = await Api.getInstanse().get(ApiRequest.FIND_MOVIE, prms);
                    var resultJson = filmResult;
                    if (resultJson.ContainsKey("Plot")){
                            filmData.Plot = resultJson["Plot"];
                            await mainPage.DisplayAlert(filmData.Title, "Year: " + filmData.Year + "\n" + "\n" + "Type: " + filmData.Type + "\n" + "\n" + filmData.Plot, "close");
                    }
                    /////////////////////////
                    #endif
                    /////////////////////////

                }

            };

            



            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                        search,
                        listView
                    }
            };
        }


        #if __ANDROID__
        void handllerCancelButton(object sender, Android.Content.DialogClickEventArgs e)
        {
            Android.App.AlertDialog objAlertDialog = sender as Android.App.AlertDialog;
            objAlertDialog.Cancel();
        }
        #endif
    }

}

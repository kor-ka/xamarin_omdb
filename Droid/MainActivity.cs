using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Json;
using System.Collections.ObjectModel;
using Xamarin.Forms.Platform.Android;

namespace trade.Droid
{
    [Activity(Label = "OmdbTest",
       MainLauncher = true,
       ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
            ActionBar.Show();
           
        }

	}
}


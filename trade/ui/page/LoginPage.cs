
using Xamarin.Forms;

namespace trade.ui.page
{
	public class LoginPage : ContentPage
	{
		public LoginPage()
		{
            Title = "Login";
            var emailET = new Entry { Placeholder = "Username" };
            emailET.Text = Prefs.get(PrefsConst.EMAIL, "") as string;
            emailET.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                Prefs.set(PrefsConst.EMAIL, e.NewTextValue);
            };

            var passET = new Entry { Placeholder = "Password", IsPassword = true };
            passET.TextChanged += (object sender, TextChangedEventArgs e) =>
            {

            };

            var loginBtn = new Button {
                Text = "Login",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("77D065")
            };
            loginBtn.Clicked += (object sender, System.EventArgs e) =>
            {
                Prefs.set(PrefsConst.LOGED_IN, true);

                
                MessagingCenter.Send<MenuItem>(new MainPageMI(), "OpenInDetail");
            };


            var regiterBtn = new Button { Text = "Register", BackgroundColor = Color.Transparent };
            regiterBtn.Clicked += (object sender, System.EventArgs e) =>
            {

            };
            
           
            Content = new StackLayout {
				Children = {
					
                    emailET,                    
                    passET,
                    loginBtn,
                    regiterBtn,
                }
			};
		}

       
    }
}

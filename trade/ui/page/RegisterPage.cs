using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace trade.ui.page
{
	public class RegisterPage : ContentPage
	{
		public RegisterPage()
		{
            var emailET = new Entry { Placeholder = "Email" };
            var passwordET = new Entry { Placeholder = "Password", IsPassword = true };
            /*
            var phoneET = new Entry { Placeholder =  "Phone *" };
            var languageET = new Entry { Placeholder =  "Language" };
            var currencyET = new Entry { Placeholder =  "Currency"};
            var nameET= new Entry { Placeholder =  "Name" };
            var surnameET= new Entry { Placeholder = "Surname" };
            var refET = new Entry { Placeholder =  "Referal code"};
            */
            var confirmBtn = new Button { Text = "CONFIRM" };
            confirmBtn.Clicked += async (object sender, EventArgs e) =>
            {
                if (emailET.Text.Length > 0 && passwordET.Text.Length > 0)
                {
                    bool registred = await Api.getInstanse().login(emailET.Text, passwordET.Text);
                    if (registred)
                    {
                        await Navigation.PushAsync(new MainPage());
                    }
                }

            };

            Content = new StackLayout {
				Children = {
					emailET,
                    passwordET,
                    confirmBtn
				}
			};
		}

        
    }
}

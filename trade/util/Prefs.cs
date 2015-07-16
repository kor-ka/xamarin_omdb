using System;
using Xamarin.Forms;

namespace trade
{

    public class Prefs
    {
        public static void set(PrefsConst key, Object value)
        {
            Application.Current.Properties[key.ToString()] = value;
        }

        public static Object get(PrefsConst key, Object defaultVal)
        {
            string stringKey = key.ToString();
            if (Application.Current.Properties.ContainsKey(stringKey))
            {
                return Application.Current.Properties[stringKey];
            }
            else
            {
                return defaultVal;
            }
        }
    }
    public enum PrefsConst
    {
        EMAIL,
        LOGIN,
        LOGED_IN,
    }
}


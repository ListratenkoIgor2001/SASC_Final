using Xamarin.Forms;

using SASC_Final.Services;

namespace SASC_Final
{
    public class Settings
    {
        public bool UseFrontCamera = true;

        public Settings() { }

        public void SaveAsync() 
        {
            new LocalStore<Settings>().SaveData(this);
            Application.Current.SavePropertiesAsync();
        }
    }
}

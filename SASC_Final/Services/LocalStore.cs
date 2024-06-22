using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using SASC_Final.Helpers;
using SASC_Final.Services.Interfaces;

using Xamarin.Forms;

namespace SASC_Final.Services
{
    public class LocalStore<T>: ILocalStore<T>
    {
        //JsonSerializerSettings _privateResolver = Extentions.GetPrivateSerializer();

        public void DeleteData(string key = "")
        {           
            var prop = typeof(T).FullName + key;
            if (Application.Current.Properties.ContainsKey(prop))
            {
                Application.Current.Properties.Remove(prop);
            }
        }

        public T LoadData(string key = "")
        {
            var prop = typeof(T).FullName + key;
            if (Application.Current.Properties.ContainsKey(prop))
            {
                var json = Application.Current.Properties[prop] as string;
                var result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
            return default(T);
        }
        public void SaveData(T obj, string key = "")
        {
            var json = JsonConvert.SerializeObject(obj);
            Application.Current.Properties[typeof(T).FullName + key] = json;

            Application.Current.SavePropertiesAsync();
        }
    }
}

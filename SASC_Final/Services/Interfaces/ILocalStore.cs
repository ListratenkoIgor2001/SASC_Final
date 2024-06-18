using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Xamarin.Forms;

namespace SASC_Final.Services.Interfaces
{
    public interface ILocalStore<T>
    {
        T LoadData(string key = "");
        void SaveData(T obj, string key = "");
        void DeleteData(string key = "");
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

using SASC_Final.Models;
using SASC_Final.Services.Interfaces;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace SASC_Final.Services
{
    public class ItemsStorage<T>
    {
        private LocalStore<List<T>> _storage = new LocalStore<List<T>>();
        private string _storageKey;
        public T CurrentItem { get; set; }
        public List<T> Items;
        public int Count => Items.Count;

        public ItemsStorage(List<T> items = null, string key = "")
        {
            _storageKey = key;
            Items = items ?? (Items = new List<T>());
            LoadData();
        }
        public List<T> GetItems()
        {
            return Items ?? (Items = new List<T>());
        }

        public void SetItems(List<T> items)
        {
            Items = new List<T>(items);
            SaveData();
        }

        private void SaveData() => _storage.SaveData(Items, _storageKey);

        private List<T> LoadData()
        {
            var result = _storage.LoadData(_storageKey);
            return result == null ? Items : Items = result;           
        }
        public void Clear()
        {
            Items = new List<T>();
            _storage.DeleteData(_storageKey);
        }
    }
}

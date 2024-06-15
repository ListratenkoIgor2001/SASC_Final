using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

using Xamarin.Essentials;

namespace SASC_Final.Services
{
    public class ItemsStorage<T>
    {
        private string filePath;

        public T CurrentItem{get;set;}
        public ItemsStorage(List<T> items = null)
        {
            //var filePath = Path.Combine(FileSystem.AppDataDirectory, $"{typeof(T).FullName}.json");
            Items = items ?? (Items = new List<T>());
        }
        public List<T> Items;
        public List<T> GetItems()
        {
            return Items ?? (Items = new List<T>());
        }

        public void SetItems(List<T> items) {
            Items = new List<T>(items);
        }

        private void SaveData()
        {
            var jsonData = JsonSerializer.Serialize(Items);
            File.WriteAllText(filePath, jsonData);
        }

        private List<T> LoadData()
        {
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<T>>(jsonData);
            }
            return new List<T>();
        }
        public int Count => Items.Count;
        public void Clear()
        {
            Items = new List<T>();
            //SaveData();
        }
    }
}

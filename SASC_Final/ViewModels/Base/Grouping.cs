using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using SASC_Final.Models;

namespace SASC_Final.ViewModels.Base
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }
        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (T item in items)
            { 
                Items.Add(item);
            }
        }
    }
}

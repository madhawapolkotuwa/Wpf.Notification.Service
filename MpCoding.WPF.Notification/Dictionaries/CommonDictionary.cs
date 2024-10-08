using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MpCoding.WPF.Notification.Dictionaries
{
    public class CommonDictionary : ResourceDictionary
    {
        public static ConcurrentDictionary<Uri, ResourceDictionary> DictionaryStore = new ConcurrentDictionary<Uri, ResourceDictionary>();

        private Uri _uri;

        public new Uri Uri
        {
            get { return _uri; }
            set
            {
                if (!(value == null))
                {
                    if (!DictionaryStore.ContainsKey(value))
                    {
                        DictionaryStore.TryAdd(value, this);
                    }
                    else
                    {
                        DictionaryStore.TryGetValue(value, out var _);
                        base.MergedDictionaries.Add(DictionaryStore[value]);
                    }

                    base.Source = value;
                    _uri = value;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Enigma.Shell.Model
{
    public class SearchData : INotifyPropertyChanged
    {
        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                _SearchText = value;
                FirePropertyChanged("SearchText");
            }
        }

        private int dictionaryId;
        public int DictionaryId
        {
            get { return dictionaryId; }
            set
            {
                if (dictionaryId != value)
                {
                    dictionaryId = value;
                    FirePropertyChanged("DictionaryId");
                }
            }
        }

        public SearchData()
        {
        }

        private void FirePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

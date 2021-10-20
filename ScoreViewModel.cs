using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Drumroll
{
    public class ScoreViewModel : INotifyPropertyChanged
    {

        internal ScoreViewModel Copy()
        {
            return ((ScoreViewModel)this.MemberwiseClone());
        }

        private int _taps = 0;
        public int Taps
        {
            get
            {
                return _taps;
            }
            set
            {
                if (value != _taps)
                {
                    _taps = value;
                    NotifyPropertyChanged("Taps");
                }
            }
        }

        private string _name = "";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

}
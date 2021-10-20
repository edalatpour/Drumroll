using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Drumroll
{
    public class LeadersViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<ScoreViewModel> Scores { get; set; }

        public LeadersViewModel()
        {
            this.Scores = new ObservableCollection<ScoreViewModel>();
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

        public void SortScores()
        {

            List<ScoreViewModel> scoreList = this.Scores.ToList();

            var sortedScores = from score in scoreList
                              orderby score.Taps descending
                              select score;

            this.Scores.Clear();

            int i = 0;
            foreach (var score in sortedScores)
            {
                if (i == 10)
                {
                    break;
                }
                this.Scores.Add(score);
                i++;
            }

        }

        public static LeadersViewModel Get()
        {
            LeadersViewModel lvm = IsolatedStorageHelper.GetObject<LeadersViewModel>("Leaderboard");
            return (lvm);
        }

        public void Save()
        {
            SortScores();
            IsolatedStorageHelper.SaveObject<LeadersViewModel>("Leaderboard", this);
        }

    }

}
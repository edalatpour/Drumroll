using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework.Audio;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Media;
using Microsoft.Phone.Tasks;

namespace Drumroll
{
    public partial class HighScorePage : PhoneApplicationPage
    {

        private ScoreViewModel _score;

        public HighScorePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (this.NavigationContext.QueryString.ContainsKey("taps"))
            {
                _score = new ScoreViewModel();
                _score.Taps = int.Parse(this.NavigationContext.QueryString["taps"]);
                this.DataContext = _score;
            }
            else
            {
                MessageBox.Show("Score was not passed.");
                NavigationService.GoBack();
            }
        }

        private void Name_GotFocus(object sender, RoutedEventArgs e)
        {
            Name.SelectAll();
        }

        private void doneButton_Click(object sender, EventArgs e)
        {

            // this causes the data binding for the current textbox to update
            object focusObj = FocusManager.GetFocusedElement();
            if (focusObj != null && focusObj is TextBox)
            {
                var binding = (focusObj as TextBox).GetBindingExpression(TextBox.TextProperty);
                binding.UpdateSource();
            }

            App.ViewModel.Leaders.Scores.Add(_score);
            App.ViewModel.Leaders.SortScores();

            NavigationService.GoBack();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

    }

}
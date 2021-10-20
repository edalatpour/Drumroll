using System;
using System.Collections.Generic;
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

namespace Drumroll
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DrumPage.xaml", UriKind.Relative));
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        private void LeadersButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LeadersPage.xaml", UriKind.Relative));
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }

}
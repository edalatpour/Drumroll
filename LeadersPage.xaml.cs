﻿using System;
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
    public partial class LeadersPage : PhoneApplicationPage
    {

        LeadersViewModel leaders;

        public LeadersPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            App.ViewModel.Leaders.SortScores();
            LayoutRoot.DataContext = App.ViewModel.Leaders;
        }

    }

}
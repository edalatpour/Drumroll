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
using Microsoft.Xna.Framework.Audio;
using System.IO;
using DrumRoll;

namespace Drumroll
{

    public enum Side
    {
        Neither,
        Left,
        Right,
        Both
    }

    public partial class DrumPage : PhoneApplicationPage
    {

        private Stopwatch _stopwatch;
        private int _myTaps = 0;
        private int _theirTaps = 0;
        private Side _myActiveSide = Side.Neither;
        private Side _theirActiveSide = Side.Neither;
        private SoundEffect _myTapSE;
        private SoundEffect _theirTapSE;

        public DrumPage()
        {
            InitializeComponent();
            Stream stream = Microsoft.Xna.Framework.TitleContainer.OpenStream(@"Sounds\MyTap.wav");
            _myTapSE = SoundEffect.FromStream(stream);
            stream = Microsoft.Xna.Framework.TitleContainer.OpenStream(@"Sounds\TheirTap.wav");
            _theirTapSE = SoundEffect.FromStream(stream);
            SetTheirActiveSide(Side.Both);
            SetMyActiveSide(Side.Both);
            _stopwatch = new Stopwatch();
            _stopwatch.Tick += new EventHandler(_stopwatch_Tick);
            _stopwatch.Elapsed += new EventHandler(_stopwatch_Elapsed);
            _stopwatch.Reset(10);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Color color = (Color)Resources["PhoneAccentColor"];
            //UpdateRectangle(TheirTimeRect, color);
            //UpdateRectangle(TheirTapsRect, color);
            //UpdateRectangle(TheirLeftButton, color);
            //UpdateRectangle(TheirRightButton, color);
            //UpdateRectangle(TimeRect, color);
            //UpdateRectangle(TapsRect, color);
            //UpdateRectangle(LeftButton, color);
            //UpdateRectangle(RightButton, color);
            Touch.FrameReported += new TouchFrameEventHandler(Touch_FrameReported);
        }

        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Touch.FrameReported -= new TouchFrameEventHandler(Touch_FrameReported);
        }

        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {

            TouchPointCollection tpc = e.GetTouchPoints(null);

            foreach (TouchPoint tp in tpc)
            {

                if (tp.Action == TouchAction.Down)
                {
                    Point point = tp.Position;

                    if (RectangleContainsPoint(LeftButton, point))
                    {
                        MyLeftTap();
                    }

                    if (RectangleContainsPoint(RightButton, point))
                    {
                        MyRightTap();
                    }

                    if (RectangleContainsPoint(TheirLeftButton, point))
                    {
                        TheirLeftTap();
                    }

                    if (RectangleContainsPoint(TheirRightButton, point))
                    {
                        TheirRightTap();
                    }

                }

            }

        }

        private bool RectangleContainsPoint(Rectangle rect, Point point)
        {
            Point controlOrigin = GetOrigin(rect);
            if (((point.X >= controlOrigin.X) && (point.X <= controlOrigin.X + rect.ActualWidth))
                && ((point.Y >= controlOrigin.Y) && (point.Y <= controlOrigin.Y + rect.ActualHeight)))
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        private Point GetOrigin(UIElement element)
        {
            GeneralTransform gt = element.TransformToVisual(Application.Current.RootVisual as UIElement);
            return(gt.Transform(new Point(0, 0)));
        }

        void _stopwatch_Tick(object sender, EventArgs e)
        {
            string text = _stopwatch.RemainingTime.ToString(@"s\.f");
            Clock.Text = text;
            TheirClock.Text = text;
        }

        void _stopwatch_Elapsed(object sender, EventArgs e)
        {
            string text = _stopwatch.RemainingTime.ToString(@"s\.f");
            Clock.Text = text;
            TheirClock.Text = text;
            SetTheirActiveSide(Side.Neither);
            SetMyActiveSide(Side.Neither);
            CheckHighScores();
        }

        private void MyLeftTap()
        {
            if (_myActiveSide != Side.Neither)
            {
                HandleMyTap(Side.Left);
                SetMyActiveSide(Side.Right);
            }
        }

        private void MyRightTap()
        {
            if (_myActiveSide != Side.Neither)
            {
                HandleMyTap(Side.Right);
                SetMyActiveSide(Side.Left);
            }
        }

        private void TheirLeftTap()
        {
            if (_theirActiveSide != Side.Neither)
            {
                HandleTheirTap(Side.Left);
                SetTheirActiveSide(Side.Right);
            }
        }

        private void TheirRightTap()
        {
            if (_theirActiveSide != Side.Neither)
            {
                HandleTheirTap(Side.Right);
                SetTheirActiveSide(Side.Left);
            }
        }

        private void HandleMyTap(Side side)
        {
            if ((_myActiveSide == Side.Both) || (side == _myActiveSide))
            {
                if (!_stopwatch.IsRunning)
                {
                    _stopwatch.Start();
                }
                _myTaps++;
                Display.Text = string.Format("{0}", _myTaps);
                _myTapSE.Play();
            }
            else
            {
                // play a "bonk" sound!
            }
        }

        private void HandleTheirTap(Side side)
        {
            if ((_theirActiveSide == Side.Both) || (side == _theirActiveSide))
            {
                if (!_stopwatch.IsRunning)
                {
                    _stopwatch.Start();
                }
                _theirTaps++;
                TheirDisplay.Text = string.Format("{0}", _theirTaps);
                _theirTapSE.Play();
            }
            else
            {
                // play a "bonk" sound!
            }
        }

        private void SetMyActiveSide(Side side)
        {
            if (side == Side.Left)
            {
                LeftCircle.Visibility = System.Windows.Visibility.Visible;
                RightCircle.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if(side == Side.Right)
            {
                LeftCircle.Visibility = System.Windows.Visibility.Collapsed;
                RightCircle.Visibility = System.Windows.Visibility.Visible;
            }
            else if(side == Side.Both)
            {
                LeftCircle.Visibility = System.Windows.Visibility.Visible;
                RightCircle.Visibility = System.Windows.Visibility.Visible;
            }
            else if (side == Side.Neither)
            {
                LeftCircle.Visibility = System.Windows.Visibility.Collapsed;
                RightCircle.Visibility = System.Windows.Visibility.Collapsed;
            }
            _myActiveSide = side;
        }

        private void SetTheirActiveSide(Side side)
        {
            if (side == Side.Left)
            {
                TheirLeftCircle.Visibility = System.Windows.Visibility.Visible;
                TheirRightCircle.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (side == Side.Right)
            {
                TheirLeftCircle.Visibility = System.Windows.Visibility.Collapsed;
                TheirRightCircle.Visibility = System.Windows.Visibility.Visible;
            }
            else if (side == Side.Both)
            {
                TheirLeftCircle.Visibility = System.Windows.Visibility.Visible;
                TheirRightCircle.Visibility = System.Windows.Visibility.Visible;
            }
            else if (side == Side.Neither)
            {
                TheirLeftCircle.Visibility = System.Windows.Visibility.Collapsed;
                TheirRightCircle.Visibility = System.Windows.Visibility.Collapsed;
            }
            _theirActiveSide = side;
        }

        private void CheckHighScores()
        {

            LeadersViewModel leaders = App.ViewModel.Leaders;
            bool isHighScore = false;
            if (leaders.Scores.Count < 10)
            {
                isHighScore = true;
            }
            else
            {
                foreach (ScoreViewModel score in leaders.Scores)
                {
                    if (_myTaps > score.Taps)
                    {
                        isHighScore = true;
                        break;
                    }
                }
            }
            if (isHighScore)
            {
                string uriText = string.Format("/HighScorePage.xaml?taps={0}", _myTaps.ToString());
                NavigationService.Navigate(new Uri(uriText, UriKind.Relative));
            }

        }

        private void UpdateRectangle(Rectangle rect, Color color)
        {
            Brush strokeBrush = new SolidColorBrush(color);
            rect.Stroke = strokeBrush;
            rect.StrokeThickness = 10;
            Brush fillBrush = new SolidColorBrush(color);
            fillBrush.Opacity = 0.5;
            rect.Fill = fillBrush;
        }

    }

}
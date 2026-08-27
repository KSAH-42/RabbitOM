using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RabbitOM.Player.Controls
{
    public class CommunicationStatusControl : Control
    {
        static CommunicationStatusControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata( typeof( CommunicationStatusControl ) , new FrameworkPropertyMetadata( typeof( CommunicationStatusControl ) ) );
        }


        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register(
                nameof(Fill),
                    typeof(Brush),
                        typeof(CommunicationStatusControl),
                            new PropertyMetadata(Brushes.White));

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register(
                nameof(Duration),
                    typeof(Duration),
                        typeof(CommunicationStatusControl),
                            new PropertyMetadata(new Duration(TimeSpan.FromSeconds(2))));

        public static readonly DependencyProperty SpeedRatioProperty =
            DependencyProperty.Register(
                nameof(SpeedRatio),
                    typeof(double),
                        typeof(CommunicationStatusControl),
                            new PropertyMetadata( 0.8 ) );

        public static readonly DependencyProperty ConnectingStatusProperty =
            DependencyProperty.Register(
                nameof(ConnectingStatus),
                    typeof(bool),
                        typeof(CommunicationStatusControl),
                            new PropertyMetadata(false));

        public static readonly DependencyProperty ConnectingMessageProperty =
            DependencyProperty.Register(
                nameof(ConnectingMessage),
                    typeof(string),
                        typeof(CommunicationStatusControl),
                            new PropertyMetadata( ));

        




        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        public Duration Duration
        {
            get => (Duration)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public double SpeedRatio
        {
            get => (double) GetValue(SpeedRatioProperty);
            set => SetValue(SpeedRatioProperty, value);
        }

        public bool ConnectingStatus
        {
            get => (bool) GetValue(ConnectingStatusProperty);
            set => SetValue(ConnectingStatusProperty, value);
        }

        public string ConnectingMessage
        {
            get => (string) GetValue(ConnectingMessageProperty);
            set => SetValue(ConnectingMessageProperty, value);
        }
    }
}

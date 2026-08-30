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

        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register(
                nameof(AnimationDuration),
                    typeof(Duration),
                        typeof(CommunicationStatusControl),
                            new PropertyMetadata(new Duration(TimeSpan.FromSeconds(2))));

        public static readonly DependencyProperty AnimationSpeedProperty =
            DependencyProperty.Register(
                nameof(AnimationSpeed),
                    typeof(double),
                        typeof(CommunicationStatusControl),
                            new PropertyMetadata( 1.0 ) );

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
                        typeof(CommunicationStatusControl));






        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        public Duration AnimationDuration
        {
            get => (Duration)GetValue(AnimationDurationProperty);
            set => SetValue(AnimationDurationProperty, value);
        }

        public double AnimationSpeed
        {
            get => (double) GetValue(AnimationSpeedProperty);
            set => SetValue(AnimationSpeedProperty, value);
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

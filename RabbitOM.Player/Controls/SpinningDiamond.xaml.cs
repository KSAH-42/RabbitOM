using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RabbitOM.Player.Controls
{
    public partial class SpinningDiamond : UserControl
    {
        public SpinningDiamond()
        {
            InitializeComponent();
            DataContext = this;
        }





        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register(
                nameof(Fill),
                    typeof(Brush),
                        typeof(SpinningDiamond),
                            new PropertyMetadata(Brushes.White));

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register(
                nameof(Duration),
                    typeof(Duration),
                        typeof(SpinningDiamond),
                            new PropertyMetadata(new Duration(TimeSpan.FromSeconds(2))));

        public static readonly DependencyProperty IsSpinningProperty =
            DependencyProperty.Register(
                nameof(IsSpinning),
                    typeof(bool),
                        typeof(SpinningDiamond),
                            new PropertyMetadata(false));

        public static readonly DependencyProperty SpeedRatioProperty =
            DependencyProperty.Register(
                nameof(SpeedRatio),
                    typeof(double),
                        typeof(SpinningDiamond),
                            new PropertyMetadata( 0.8 ) );





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

        public bool IsSpinning
        {
            get => (bool) GetValue(IsSpinningProperty);
            set => SetValue(IsSpinningProperty, value);
        }
        
        public double SpeedRatio
        {
            get => (double) GetValue(SpeedRatioProperty);
            set => SetValue(SpeedRatioProperty, value);
        }
    }
}

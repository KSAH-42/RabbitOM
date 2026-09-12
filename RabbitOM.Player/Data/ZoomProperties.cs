using System;
using System.Windows;
using System.Windows.Media;

namespace RabbitOM.Player.Data
{
    public sealed class ZoomProperties : DependencyObject
    {
        public static readonly DependencyProperty ScaleXProperty = DependencyProperty.Register( nameof(ScaleX), typeof(double), typeof(ZoomProperties), new PropertyMetadata(1.0));
        public static readonly DependencyProperty ScaleYProperty = DependencyProperty.Register( nameof(ScaleY), typeof(double), typeof(ZoomProperties), new PropertyMetadata(1.0));
        public static readonly DependencyProperty TranslationXProperty = DependencyProperty.Register( nameof(TranslationX), typeof(double), typeof(ZoomProperties));
        public static readonly DependencyProperty TranslationYProperty = DependencyProperty.Register( nameof(TranslationY), typeof(double), typeof(ZoomProperties));
        public static readonly DependencyProperty SelectionInnerXProperty = DependencyProperty.Register( nameof(SelectionInnerX), typeof(double), typeof(ZoomProperties));
        public static readonly DependencyProperty SelectionInnerYProperty = DependencyProperty.Register( nameof(SelectionInnerY), typeof(double), typeof(ZoomProperties));
        public static readonly DependencyProperty SelectionXProperty = DependencyProperty.Register( nameof(SelectionX), typeof(double), typeof(ZoomProperties));
        public static readonly DependencyProperty SelectionYProperty = DependencyProperty.Register( nameof(SelectionY), typeof(double), typeof(ZoomProperties));
        public static readonly DependencyProperty SelectionWidthProperty = DependencyProperty.Register( nameof(SelectionWidth), typeof(double), typeof(ZoomProperties));
        public static readonly DependencyProperty SelectionHeightProperty = DependencyProperty.Register( nameof(SelectionHeight), typeof(double), typeof(ZoomProperties));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register( nameof(Text), typeof(string), typeof(ZoomProperties));
        public static readonly DependencyProperty TextColorProperty = DependencyProperty.Register( nameof(TextColor), typeof(Brush), typeof(ZoomProperties) , new PropertyMetadata( Brushes.Orange ));
        public static readonly DependencyProperty TextPositionXProperty = DependencyProperty.Register( nameof(TextPositionX), typeof(double), typeof(ZoomProperties));
        public static readonly DependencyProperty TextPositionYProperty = DependencyProperty.Register( nameof(TextPositionY), typeof(double), typeof(ZoomProperties));
        public static readonly DependencyProperty VisibilityProperty = DependencyProperty.Register( nameof(Visibility), typeof(Visibility), typeof(ZoomProperties), new PropertyMetadata( Visibility.Collapsed ));



        public double ScaleX
        {
            get => (double) GetValue( ScaleXProperty );
            set => SetValue( ScaleXProperty , value );
        }

        public double ScaleY
        {
            get => (double) GetValue( ScaleYProperty );
            set => SetValue( ScaleYProperty , value );
        }

        public double TranslationX
        {
            get => (double) GetValue( TranslationXProperty );
            set => SetValue( TranslationXProperty , value );
        }

        public double TranslationY
        {
            get => (double) GetValue( TranslationYProperty );
            set => SetValue( TranslationYProperty , value );
        }

        public double SelectionInnerX
        {
            get => (double) GetValue( SelectionInnerXProperty );
            set => SetValue( SelectionInnerXProperty , value );
        }

        public double SelectionInnerY
        {
            get => (double) GetValue( SelectionInnerYProperty );
            set => SetValue( SelectionInnerYProperty , value );
        }

        public double SelectionX
        {
            get => (double) GetValue( SelectionXProperty );
            set => SetValue( SelectionXProperty , value );
        }

        public double SelectionY
        {
            get => (double) GetValue( SelectionYProperty );
            set => SetValue( SelectionYProperty , value );
        }

        public double SelectionWidth
        {
            get => (double) GetValue( SelectionWidthProperty );
            set => SetValue( SelectionWidthProperty , value );
        }

        public double SelectionHeight
        {
            get => (double) GetValue( SelectionHeightProperty );
            set => SetValue( SelectionHeightProperty , value );
        }

        public string Text
        {
            get => (string) GetValue( TextProperty );
            set => SetValue( TextProperty , value );
        }

        public Brush TextColor
        {
            get => (Brush) GetValue( TextColorProperty );
            set => SetValue( TextColorProperty , value );
        }

        public double TextPositionX
        {
            get => (double) GetValue( TextPositionXProperty );
            set => SetValue( TextPositionXProperty , value );
        }

        public double TextPositionY
        {
            get => (double) GetValue( TextPositionYProperty );
            set => SetValue( TextPositionYProperty , value );
        }

        public Visibility Visibility
        {
            get => (Visibility) GetValue( VisibilityProperty );
            set => SetValue( VisibilityProperty , value );
        }
    }
}

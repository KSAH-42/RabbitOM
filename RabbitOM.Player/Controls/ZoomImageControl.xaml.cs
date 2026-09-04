// This implementation represent zoom region selector and it's compose of two rectangle
// the inner rectangle is the region that keep the aspect
// normally the video stream must not be stretch it must keep ratio
// and display black zones called pillars in the terms of video computer graphics
// that's the main reason that there is two rectangles

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RabbitOM.Player.Controls
{
    public partial class ZoomImageControl : UserControl
    {
        private Point _start;






        public ZoomImageControl()
        {
            InitializeComponent();
        }








        public static readonly RoutedEvent RegionSelectedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(RegionSelected),
                    RoutingStrategy.Direct,
                        typeof(RoutedEventHandler<SelectedRegionRoutedEventArgs>),
                            typeof(ZoomImageControl));







        public event RoutedEventHandler<SelectedRegionRoutedEventArgs> RegionSelected
        {
            add    => AddHandler( RegionSelectedEvent , value );
            remove => RemoveHandler( RegionSelectedEvent , value );
        }






        public static readonly DependencyProperty ScaleXProperty =
            DependencyProperty.Register(
                nameof(ScaleX),
                    typeof(double),
                        typeof(ZoomImageControl),
                            new PropertyMetadata(1.0));

        public static readonly DependencyProperty ScaleYProperty =
            DependencyProperty.Register(
                nameof(ScaleY),
                    typeof(double),
                        typeof(ZoomImageControl),
                            new PropertyMetadata(1.0));

        public static readonly DependencyProperty TranslationXProperty =
            DependencyProperty.Register(
                nameof(TranslationX),
                    typeof(double),
                        typeof(ZoomImageControl));

        public static readonly DependencyProperty TranslationYProperty =
            DependencyProperty.Register(
                nameof(TranslationY),
                    typeof(double),
                        typeof(ZoomImageControl));

        public static readonly DependencyProperty SelectionInnerXProperty =
            DependencyProperty.Register(
                nameof(SelectionInnerX),
                    typeof(double),
                        typeof(ZoomImageControl));

        public static readonly DependencyProperty SelectionInnerYProperty =
            DependencyProperty.Register(
                nameof(SelectionInnerY),
                    typeof(double),
                        typeof(ZoomImageControl));


        public static readonly DependencyProperty SelectionXProperty =
            DependencyProperty.Register(
                nameof(SelectionX),
                    typeof(double),
                        typeof(ZoomImageControl));

        public static readonly DependencyProperty SelectionYProperty =
            DependencyProperty.Register(
                nameof(SelectionY),
                    typeof(double),
                        typeof(ZoomImageControl));

        public static readonly DependencyProperty SelectionWidthProperty =
            DependencyProperty.Register(
                nameof(SelectionWidth),
                    typeof(double),
                        typeof(ZoomImageControl));

        public static readonly DependencyProperty SelectionHeightProperty =
            DependencyProperty.Register(
                nameof(SelectionHeight),
                    typeof(double),
                        typeof(ZoomImageControl));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                    typeof(string),
                        typeof(ZoomImageControl));

        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register(
                nameof(TextColor),
                    typeof(Brush),
                        typeof(ZoomImageControl)
                            , new PropertyMetadata( Brushes.Orange ));

        public static readonly DependencyProperty TextPositionXProperty =
            DependencyProperty.Register(
                nameof(TextPositionX),
                    typeof(double),
                        typeof(ZoomImageControl));

        public static readonly DependencyProperty TextPositionYProperty =
            DependencyProperty.Register(
                nameof(TextPositionY),
                    typeof(double),
                        typeof(ZoomImageControl));

        public static readonly DependencyProperty RegionVisibilityProperty =
            DependencyProperty.Register(
                nameof(RegionVisibility),
                    typeof(Visibility),
                        typeof(ZoomImageControl),
                            new PropertyMetadata( Visibility.Collapsed ));






        public double ScaleX
        {
            get => (double) GetValue( ScaleXProperty );
            private set => SetValue( ScaleXProperty , value );
        }

        public double ScaleY
        {
            get => (double) GetValue( ScaleYProperty );
            private set => SetValue( ScaleYProperty , value );
        }

        public double TranslationX
        {
            get => (double) GetValue( TranslationXProperty );
            private set => SetValue( TranslationXProperty , value );
        }

        public double TranslationY
        {
            get => (double) GetValue( TranslationYProperty );
            private set => SetValue( TranslationYProperty , value );
        }

        public double SelectionInnerX
        {
            get => (double) GetValue( SelectionInnerXProperty );
            private set => SetValue( SelectionInnerXProperty , value );
        }

        public double SelectionInnerY
        {
            get => (double) GetValue( SelectionInnerYProperty );
            private set => SetValue( SelectionInnerYProperty , value );
        }

        public double SelectionX
        {
            get => (double) GetValue( SelectionXProperty );
            private set => SetValue( SelectionXProperty , value );
        }

        public double SelectionY
        {
            get => (double) GetValue( SelectionYProperty );
            private set => SetValue( SelectionYProperty , value );
        }

        public double SelectionWidth
        {
            get => (double) GetValue( SelectionWidthProperty );
            private set => SetValue( SelectionWidthProperty , value );
        }

        public double SelectionHeight
        {
            get => (double) GetValue( SelectionHeightProperty );
            private set => SetValue( SelectionHeightProperty , value );
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
            private set => SetValue( TextPositionXProperty , value );
        }

        public double TextPositionY
        {
            get => (double) GetValue( TextPositionYProperty );
            private set => SetValue( TextPositionYProperty , value );
        }

        public Visibility RegionVisibility
        {
            get => (Visibility) GetValue( RegionVisibilityProperty );
            private set => SetValue( RegionVisibilityProperty , value );
        }







        public bool UpdateTransforms()
        {
            const double limit = 8;

            if ( ! IsEnabled || SelectionWidth <= limit || SelectionHeight <= limit || ActualWidth <= limit || ActualHeight <= limit )
            {
                return false;
            }

            ScaleX = ActualWidth / SelectionWidth;
            ScaleY = ActualHeight / SelectionHeight;

            TranslationX = -SelectionX * ScaleX;
            TranslationY = -SelectionY * ScaleY;
            return true;
        }

        public void ClearSelection()
        {
            ScaleX = 1.0;
            ScaleY = 1.0;
            TranslationX = 0;
            TranslationY = 0;
            SelectionInnerX = 0;
            SelectionInnerY = 0;
            SelectionX = 0;
            SelectionY = 0;
            SelectionWidth = 0;
            SelectionHeight = 0;
            TextPositionY = 0;
            TextPositionX = 0;
            InnerRectangle.Width = 0;
            InnerRectangle.Height = 0;
        }






        protected virtual void OnRegionSelected( SelectedRegionRoutedEventArgs e )
        {
            RaiseEvent( e );
        }

        private void OnControlEnabledChanged( object sender , DependencyPropertyChangedEventArgs e )
        {
            if ( ! IsEnabled )
            {
                RegionVisibility = Visibility.Collapsed;
            }
        }

        private void OnCanvasMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _start = e.GetPosition( sender as Canvas );

            ClearSelection();
        }

        private void OnCanvasMouseUp( object sender , MouseButtonEventArgs e )
        {
            var eventArgs = new SelectedRegionRoutedEventArgs( RegionSelectedEvent , this , SelectionInnerX , SelectionInnerY , InnerRectangle.ActualWidth , InnerRectangle.ActualHeight , ScaleX , ScaleY , TranslationX , TranslationY );

            UpdateTransforms();

            RegionVisibility = Visibility.Collapsed;

            OnRegionSelected( eventArgs );
        }

        private void OnCanvasMouseMove(object sender, MouseEventArgs e)
        {
            if ( ! IsEnabled || e.LeftButton != MouseButtonState.Pressed )
            {
                return;
            }

            var pos = e.GetPosition(sender as Canvas);

            SelectionX = Math.Min(pos.X, _start.X);
            SelectionY = Math.Min(pos.Y, _start.Y);
            SelectionWidth = Math.Abs(pos.X - _start.X);
            SelectionHeight = Math.Abs(pos.Y - _start.Y);

            TextPositionX = SelectionX;
            TextPositionY = SelectionY;

            var min = Math.Min( SelectionWidth , SelectionHeight );

            InnerRectangle.Width = min;
            InnerRectangle.Height = min;

            SelectionInnerX = SelectionX + SelectionWidth / 2 - min/2;
            SelectionInnerY = SelectionY;

            if ( SelectionWidth <= SelectionHeight )
            {
                SelectionInnerY += SelectionHeight / 2 - min/2;
            }

            Canvas.SetTop( InnerRectangle , SelectionInnerY );
            Canvas.SetLeft( InnerRectangle , SelectionInnerX );

            RegionVisibility = Visibility.Visible;
        }
    }
}

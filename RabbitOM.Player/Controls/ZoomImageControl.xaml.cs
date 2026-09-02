using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RabbitOM.Player.Controls
{
    public partial class ZoomImageControl : UserControl
    {
        private Point _start;






        public ZoomImageControl()
        {
            InitializeComponent();
        }








        public static readonly RoutedEvent SelectedRegionEvent =
            EventManager.RegisterRoutedEvent(
                nameof(SelectedRegion),
                    RoutingStrategy.Direct,
                        typeof(RoutedEventHandler),
                            typeof(ZoomImageControl));







        public event RoutedEventHandler SelectedRegion
        {
            add    => AddHandler( SelectedRegionEvent , value );
            remove => RemoveHandler( SelectedRegionEvent , value );
        }







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
                        typeof(ZoomImageControl)
                            , new PropertyMetadata( "Zoom not implemented" ));

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

        public static readonly DependencyProperty TextVisisbilityProperty =
            DependencyProperty.Register(
                nameof(TextVisibility),
                    typeof(Visibility),
                        typeof(ZoomImageControl),
                            new PropertyMetadata( Visibility.Collapsed ));







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

        public Visibility TextVisibility
        {
            get => (Visibility) GetValue( TextVisisbilityProperty );
            private set => SetValue( TextVisisbilityProperty , value );
        }








        public void ClearSelection()
        {
            SelectionX = 0;
            SelectionY = 0;
            SelectionWidth = 0;
            SelectionHeight = 0;
            InnerRectangle.Width = 0;
            InnerRectangle.Height = 0;
            TextPositionX = 0;
            TextPositionY = 0;
        }







        
        private void OnControlEnabledChanged( object sender , DependencyPropertyChangedEventArgs e )
        {
            if ( ! IsEnabled )
            {
                TextVisibility = Visibility.Collapsed;
            }
        }

        private void OnCanvasMouseUp( object sender , MouseButtonEventArgs e )
        {
            var eventArgs = new SelectedRegionRoutedEventArgs( SelectedRegionEvent , this , SelectionY , SelectionX , InnerRectangle.ActualWidth , InnerRectangle.ActualHeight );

            ClearSelection();
            TextVisibility = Visibility.Collapsed;

            OnSelectedRegion( eventArgs );
        }

        private void OnCanvasMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _start = e.GetPosition( sender as Canvas );

            ClearSelection();
            TextVisibility = Visibility.Visible;
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

            var min = Math.Min( SelectionWidth , SelectionHeight );

            InnerRectangle.Width = min;
            InnerRectangle.Height = min;

            if ( SelectionWidth > SelectionHeight )
            {
                Canvas.SetLeft( InnerRectangle , SelectionX + SelectionWidth / 2 - min/2 );
                Canvas.SetTop( InnerRectangle , SelectionY );
            }
            else
            {
                Canvas.SetLeft( InnerRectangle , SelectionX + SelectionWidth / 2 - min/2 );
                Canvas.SetTop( InnerRectangle , SelectionY + SelectionHeight / 2 - min/2 );
            }

            TextPositionX = SelectionX ;
            TextPositionY = SelectionY ;
        }

        protected virtual void OnSelectedRegion( SelectedRegionRoutedEventArgs e )
        {
            if ( SelectedRegionRoutedEventArgs.IsValid( e ) )
            {
                RaiseEvent( e );
            }
        }
    }
}

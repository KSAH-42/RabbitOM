using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace RabbitOM.Player.Controls
{
    using RabbitOM.Player.Data;

    public partial class ViewPortControl : UserControl
    {
        public static readonly DependencyProperty StretchImageProperty = DependencyProperty.Register( nameof(StretchImage) , typeof(Stretch) , typeof(ViewPortControl) , new PropertyMetadata( Stretch.Fill ) );

        public ViewPortControl()
        {
            InitializeComponent();
        }

        public Stretch StretchImage
        {
            get => (Stretch) GetValue( StretchImageProperty );
            set => SetValue( StretchImageProperty , value );
        }

        public ZoomProperties ZoomProperties { get; } = new ZoomProperties();

        public Image Image { get => _image; }



        public bool UpdateTransforms()
        {
            const double limit = 8;

            if ( ! IsEnabled || ZoomProperties.SelectionWidth <= limit || ZoomProperties.SelectionHeight <= limit || ActualWidth <= limit || ActualHeight <= limit )
            {
                return false;
            }

            ZoomProperties.ScaleX = ActualWidth / InnerRectangle.ActualWidth;
            ZoomProperties.ScaleY = ActualHeight / InnerRectangle.ActualHeight;
            ZoomProperties.TranslationX = -ZoomProperties.SelectionInnerX * ZoomProperties.ScaleX;
            ZoomProperties.TranslationY = -ZoomProperties.SelectionInnerY * ZoomProperties.ScaleY;

            OnZoomIn();
            return true;
        }

        public void ClearZoomSelection()
        {
            ZoomProperties.ScaleX = 1.0;
            ZoomProperties.ScaleY = 1.0;
            ZoomProperties.TranslationX = 0;
            ZoomProperties.TranslationY = 0;
            ZoomProperties.SelectionInnerX = 0;
            ZoomProperties.SelectionInnerY = 0;
            ZoomProperties.SelectionX = 0;
            ZoomProperties.SelectionY = 0;
            ZoomProperties.SelectionWidth = 0;
            ZoomProperties.SelectionHeight = 0;
            ZoomProperties.TextPositionY = 0;
            ZoomProperties.TextPositionX = 0;
            InnerRectangle.Width = 0;
            InnerRectangle.Height = 0;

            OnZoomReset();
        }

        public void ClearImage()
        {
            _image.Source = null;
        }



        private void OnControlEnabledChanged( object sender , DependencyPropertyChangedEventArgs e )
        {
            if ( ! IsEnabled )
            {
                ZoomProperties.Visibility = Visibility.Collapsed;
            }
        }

        private void OnCanvasMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var canvas = sender as Canvas;

            if ( canvas == null )
            {
                return;
            }

            canvas.CaptureMouse();

            ZoomProperties.StartPoint = e.GetPosition( canvas );

            ClearZoomSelection();
        }

        private void OnCanvasMouseUp( object sender , MouseButtonEventArgs e )
        {
            var canvas = sender as Canvas;

            if ( canvas == null )
            {
                return;
            }

            canvas.ReleaseMouseCapture();

            UpdateTransforms();

            ZoomProperties.Visibility = Visibility.Collapsed;
        }

        private void OnCanvasMouseMove(object sender, MouseEventArgs e)
        {
            if ( ! IsEnabled || e.LeftButton != MouseButtonState.Pressed )
            {
                return;
            }

            var canvas = sender as Canvas;

            if ( canvas == null || ! canvas.IsMouseCaptured )
            {
                return;
            }

            var pos = e.GetPosition( canvas );

            ZoomProperties.SelectionX = Math.Min(pos.X, ZoomProperties.StartPoint.X);
            ZoomProperties.SelectionY = Math.Min(pos.Y, ZoomProperties.StartPoint.Y);
            ZoomProperties.SelectionWidth = Math.Abs(pos.X - ZoomProperties.StartPoint.X);
            ZoomProperties.SelectionHeight = Math.Abs(pos.Y - ZoomProperties.StartPoint.Y);

            ZoomProperties.TextPositionX = ZoomProperties.SelectionX;
            ZoomProperties.TextPositionY = ZoomProperties.SelectionY;

            var min = Math.Min( ZoomProperties.SelectionWidth , ZoomProperties.SelectionHeight );

            InnerRectangle.Width = min;
            InnerRectangle.Height = min;

            ZoomProperties.SelectionInnerX = ZoomProperties.SelectionX + ZoomProperties.SelectionWidth / 2 - min/2;
            ZoomProperties.SelectionInnerY = ZoomProperties.SelectionY;

            if ( ZoomProperties.SelectionWidth <= ZoomProperties.SelectionHeight )
            {
                ZoomProperties.SelectionInnerY += ZoomProperties.SelectionHeight / 2 - min/2;
            }

            Canvas.SetTop( InnerRectangle , ZoomProperties.SelectionInnerY );
            Canvas.SetLeft( InnerRectangle , ZoomProperties.SelectionInnerX );

            ZoomProperties.Visibility = Visibility.Visible;
        }

        private void OnZoomIn()
        {
            var sb = (Storyboard)Resources["ZoomStoryboard"];

            if ( sb == null || sb.Children.Count != 4 )
            {
                return;
            }

            if ( ZoomProperties.HasValidTransformValues() )
            {
                if ( sb.Children[0] is DoubleAnimation animScaleX )
                {
                    animScaleX.To = ZoomProperties.ScaleX;
                }

                if ( sb.Children[1] is DoubleAnimation animScaleY )
                {
                    animScaleY.To = ZoomProperties.ScaleY;
                }

                if ( sb.Children[2] is DoubleAnimation animTransX )
                {
                    animTransX.To = ZoomProperties.TranslationX;
                }

                if ( sb.Children[3] is DoubleAnimation animTransY )
                {
                    animTransY.To = ZoomProperties.TranslationY;
                }

                sb.Begin( this );
            }
        }

        private void OnZoomReset()
        {
            var sb = (Storyboard)Resources["ZoomStoryboard"];

            if ( sb == null || sb.Children.Count != 4 )
            {
                return;
            }

            if ( ZoomProperties.HasValidTransformValues() )
            {
                if ( sb.Children[0] is DoubleAnimation animScaleX )
                {
                    animScaleX.To = 1.0;
                }

                if ( sb.Children[1] is DoubleAnimation animScaleY )
                {
                    animScaleY.To = 1.0;
                }

                if ( sb.Children[2] is DoubleAnimation animTransX )
                {
                    animTransX.To = 0.0;
                }

                if ( sb.Children[3] is DoubleAnimation animTransY )
                {
                    animTransY.To = 0.0;
                }

                sb.Begin( this );
            }
        }
    }
}

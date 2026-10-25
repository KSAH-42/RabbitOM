using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shell;

namespace RabbitOM.Player.Themes.Styles
{
	public partial class WindowStyle : ResourceDictionary
	{
		public static readonly DependencyProperty IsFullScreenProperty = DependencyProperty.RegisterAttached( "IsFullScreen" , typeof( bool ) , typeof( Window ) , new PropertyMetadata( false , OnFullScreenChanged ) );






		public WindowStyle()
		{
			InitializeComponent();
		}






		public static void SetFullScreen( DependencyObject dependencyObject , bool status )
		{
			if ( dependencyObject == null )
			{
				throw new ArgumentNullException( nameof( dependencyObject ) );
			}

			dependencyObject?.SetValue( IsFullScreenProperty , status );
		}

		public static bool GetFullScreen( DependencyObject dependencyObject )
		{
			if ( dependencyObject == null )
			{
				throw new ArgumentNullException( nameof( dependencyObject ) );
			}

			return (bool) dependencyObject.GetValue( IsFullScreenProperty );
		}








		public void OnClickTileBar( object sender , MouseButtonEventArgs e )
        {
			e.Handled = true;

			var element = sender as FrameworkElement;

			if ( element == null )
			{
				return;
			}

			var window = element.TemplatedParent as Window;

			if ( window == null )
			{
				return;
			}

			if (e.ClickCount != 2)
			{
				window.DragMove();
			}
			else
			{
				if ( window.WindowState == System.Windows.WindowState.Normal )
				{
					window.WindowState  = System.Windows.WindowState.Maximized;
					window.MaxHeight    = SystemParameters.MaximizedPrimaryScreenHeight;
				}
				else
				{
					window.WindowState  = System.Windows.WindowState.Normal;
				}
			}
        }

        public void OnClickButtonMinimize( object sender , RoutedEventArgs e )
        {
			e.Handled = true;

			var element = sender  as FrameworkElement;

			if ( element == null )
			{
				return;
			}

			var window = element.TemplatedParent as Window;

			if ( window == null )
			{
				return;
			}

			window.WindowState = System.Windows.WindowState.Minimized;
        }

		public void OnClickButtonMaximize( object sender, RoutedEventArgs e )
        {
			e.Handled = true;

			var element = sender as FrameworkElement;

			if ( element == null )
			{
				return;
			}

			var window = element.TemplatedParent as Window;

			if ( window == null )
			{
				return;
			}

			if (window.WindowState == System.Windows.WindowState.Normal)
			{
				window.MaxHeight   = SystemParameters.MaximizedPrimaryScreenHeight;
				window.WindowState = System.Windows.WindowState.Maximized;
			}
			else
			{
				window.WindowState = System.Windows.WindowState.Normal;
			}
        }

		public void OnClickButtonClose( object sender , RoutedEventArgs e )
        {
			e.Handled = true;

			var element = sender  as FrameworkElement;

			if ( element == null )
			{
				return;
			}

			var window = element.TemplatedParent as Window;

			if ( window == null )
			{
				return;
			}

			window.Close();
        }








		public static void OnFullScreenChanged( DependencyObject dependencyObject , DependencyPropertyChangedEventArgs e )
		{
			var window = dependencyObject as Window;

			if ( window == null )
			{
				return;
			}

			if ( e.NewValue is bool status )
			{
				var border = window.Template.FindName( "Part_TileBar" , window ) as Border;

				if ( border == null )
				{
					return;
				}

				border.Visibility = status ? Visibility.Collapsed : Visibility.Visible;
				window.WindowState = WindowState.Maximized;

				WindowChrome.SetIsHitTestVisibleInChrome( window , status );
			}
		}
	}
}

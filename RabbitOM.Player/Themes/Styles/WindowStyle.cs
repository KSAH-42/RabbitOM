using System;
using System.Windows;
using System.Windows.Input;

namespace RabbitOM.Player.Styles
{
	public partial class WindowStyle : ResourceDictionary
	{
		public WindowStyle()
		{
			InitializeComponent();
		}


		public void TileBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
			var element = sender as FrameworkElement;

			if ( element == null )
			{
				return;
			}

			var window = element.TemplatedParent as Window;

			window?.DragMove();
        }

		public void MainBorder_MouseLeftButtonDown(object sender,MouseButtonEventArgs e)
		{
			e.Handled = true;

            if ( e.ClickCount != 2 )
            {
                return;
            }

            var element = e.OriginalSource as FrameworkElement;

            if ( element == null )
            {
                return;
            }

			var window = element.TemplatedParent as Window;

            if ( window == null )
            {
                return;
            }

			if ( window.WindowState == System.Windows.WindowState.Normal )
			{
				window.MaxHeight    = SystemParameters.MaximizedPrimaryScreenHeight;
				window.WindowState  = System.Windows.WindowState.Maximized;
			}
			else
			{
				window.WindowState  = System.Windows.WindowState.Normal;
			}
		}

        public void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
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

		public void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
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

		public void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
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
	}
}

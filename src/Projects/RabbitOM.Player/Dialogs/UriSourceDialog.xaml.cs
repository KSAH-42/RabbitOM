using System;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace RabbitOM.Player.Dialogs
{
	public partial class UriSourceDialog : Window
	{
		public static readonly RoutedCommand CloseCommand = new RoutedCommand();

		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register( nameof(UriSourceDialog), typeof(string), typeof(UriSourceDialog) , new PropertyMetadata("rtsp://admin:password@127.0.0.1:554") );

		public UriSourceDialog()
		{
			InitializeComponent();
		}

		public string Value
        {
            get => GetValue( ValueProperty ) as string;
            set => SetValue( ValueProperty , value );
        }

		private void OnCancel( object sender , ExecutedRoutedEventArgs e )
        {
            Close();
        }

        private void OnClose( object sender , ExecutedRoutedEventArgs e )
        {
            DialogResult = true;
        }
	}
}

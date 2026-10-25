using System;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace RabbitOM.Player.Dialogs
{
	public partial class UriPromptDialog : Window
	{
		public static readonly RoutedCommand CloseCommand = new RoutedCommand();

		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register( nameof(UriPromptDialog), typeof(string), typeof(UriPromptDialog) , new PropertyMetadata("rtsp://admin:password@127.0.0.1:554") );

		public UriPromptDialog()
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

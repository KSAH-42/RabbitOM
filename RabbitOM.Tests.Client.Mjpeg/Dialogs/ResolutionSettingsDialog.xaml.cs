using System;
using System.Windows;
using System.Windows.Input;

namespace RabbitOM.Tests.Client.Mjpeg.Dialogs
{
    public partial class ResolutionSettingsDialog : Window
    {
        public ResolutionSettingsDialog()
        {
            InitializeComponent();
        }





        public static RoutedCommand AcceptButtonCommand = new RoutedCommand();





        public static readonly DependencyProperty ResolutionWidthProperty 
            = DependencyProperty.Register(
                "ResolutionWidth", typeof(int),
                    typeof(ResolutionSettingsDialog) );

        public static readonly DependencyProperty ResolutionHeightProperty 
            = DependencyProperty.Register(
                "ResolutionHeight", typeof(int),
                    typeof(ResolutionSettingsDialog) );

        public static readonly DependencyProperty ReplaceResolutionProperty 
            = DependencyProperty.Register(
                "ReplaceResolution", typeof(bool),
                    typeof(ResolutionSettingsDialog) );

        





        public int ResolutionWidth
        {
            get => (int) GetValue( ResolutionWidthProperty );
            set => SetValue( ResolutionWidthProperty , value );
        }

        public int ResolutionHeight
        {
            get => (int) GetValue( ResolutionHeightProperty );
            set => SetValue( ResolutionHeightProperty , value );
        }

        public bool ReplaceResolution
        {
            get => (bool) GetValue( ReplaceResolutionProperty );
            set => SetValue( ReplaceResolutionProperty , value );
        }





        private void OnCanAcceptSettings( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = ResolutionWidth > 0 && ResolutionHeight > 0;
        }

        private void OnAcceptSettings( object sender , ExecutedRoutedEventArgs e )
        {
            DialogResult = true;
        }
    }
}

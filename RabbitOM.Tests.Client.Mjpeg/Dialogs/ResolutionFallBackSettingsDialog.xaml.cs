// Resolution fallback are used in case when jpeg resolutions are not present on the standard rtp rfc

using System;
using System.Windows;
using System.Windows.Input;

namespace RabbitOM.Tests.Client.Mjpeg.Dialogs
{
    public partial class ResolutionFallBackSettingsDialog : Window
    {
        public static RoutedCommand AcceptButtonCommand = new RoutedCommand();
        



        public ResolutionFallBackSettingsDialog()
        {
            InitializeComponent();
        }




        public static readonly DependencyProperty ResolutionWidthProperty 
            = DependencyProperty.Register(
                "WidthResolution", typeof(int) ,
                    typeof(ResolutionFallBackSettingsDialog) );

        public static readonly DependencyProperty ResolutionHeightProperty 
            = DependencyProperty.Register(
                "HeightResolution", typeof(int) ,
                    typeof(ResolutionFallBackSettingsDialog) );

        public static readonly DependencyProperty ReplaceResolutionProperty 
            = DependencyProperty.Register(
                "ReplaceResolution", typeof(bool) ,
                    typeof(ResolutionFallBackSettingsDialog) );
        



        public int WidthResolution
        {
            get => (int) GetValue( ResolutionWidthProperty );
            set => SetValue( ResolutionWidthProperty , value );
        }

        public int HeightResolution
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
            e.CanExecute = WidthResolution >= 2 && HeightResolution >= 2;
        }

        private void OnAcceptSettings( object sender , ExecutedRoutedEventArgs e )
        {
            DialogResult = true;
        }
    }
}

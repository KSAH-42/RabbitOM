using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RabbitOM.Player.Dialogs
{
    public partial class SaveImageDialog : Window
    {
        public static readonly RoutedCommand SelectFileCommand = new RoutedCommand();
        public static readonly RoutedCommand TakeSnapshotCommand = new RoutedCommand();
        public static readonly RoutedCommand SaveButtonCommand = new RoutedCommand();

        public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register( "FileName", typeof(string) , typeof(SaveImageDialog) );
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(BitmapSource) ,typeof(SaveImageDialog) );

        public SaveImageDialog()
        {
            InitializeComponent();
        }

        public string FileName
        {
            get => GetValue( FileNameProperty ) as string;
            set => SetValue( FileNameProperty , value );
        }

        public BitmapSource Image
        {
            get => GetValue( ImageProperty ) as BitmapSource;
            set => SetValue( ImageProperty , value );
        }

        public BitmapSource Source
        {
            get;
            set;
        }

        public void TakeSnasphot()
        {
            Image = Source?.Clone();
        }

        private void OnCanSelectFile( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = Image != null;
        }

        private void OnSelectFile( object sender , ExecutedRoutedEventArgs e )
        {
            try
            {
                var dialog = new OpenFileDialog()
                {
                    Filter = "Image file (*.bmp)|*.bmp", CheckFileExists = false
                };

                if ( dialog.ShowDialog() == true )
                {
                    FileName = dialog.FileName;
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.ToString() );
            }
        }

        private void OnCanTakeSnapshot( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = Source != null;
        }

        private void OnTakeSnapshot( object sender , ExecutedRoutedEventArgs e )
        {
            Image = Source.Clone();
        }

        private void OnCanSave( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = ! string.IsNullOrWhiteSpace( FileName );
        }

        private void OnSave( object sender , ExecutedRoutedEventArgs e )
        {
            if ( MessageBox.Show( "Would you like to save ?" , "Informations"  , MessageBoxButton.YesNo , MessageBoxImage.Question ) != MessageBoxResult.Yes )
            {
                return;
            }

            try
            {
                using ( var stream = File.Create( FileName ) )
                {
                    var encoder = new BmpBitmapEncoder();

                    encoder.Frames.Add( BitmapFrame.Create( Image ) );
                    encoder.Save( stream );
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.ToString() );
            }
        }
    }
}

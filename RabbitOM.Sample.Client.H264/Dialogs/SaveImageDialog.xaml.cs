using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RabbitOM.Sample.Client.H264.Dialogs
{
    public partial class SaveImageDialog : Window
    {
        public static RoutedCommand TakeSnapshotCommand = new RoutedCommand();

        public static RoutedCommand SaveButtonCommand = new RoutedCommand();




        public SaveImageDialog()
        {
            InitializeComponent();
        }




        public static readonly DependencyProperty ImageProperty
            = DependencyProperty.Register(
                "Image", typeof(BitmapSource) ,
                    typeof(SaveImageDialog) );





        public BitmapSource Image
        {
            get => (BitmapSource) GetValue( ImageProperty );
            set => SetValue( ImageProperty , value );
        }

        public WriteableBitmap BitmapSource
        {
            get;
            set;
        }


        private void OnCanTakeSnapshot( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = BitmapSource != null;
        }

        private void OnTakeSnapshot( object sender , ExecutedRoutedEventArgs e )
        {
            Image = BitmapSource.Clone();
        }


        private void OnCanSave( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

        private void OnSave( object sender , ExecutedRoutedEventArgs e )
        {
            try
            {
                var dialog = new SaveFileDialog()
                {
                    Filter = "Image file (*.bmp)|*.bmp"
                };

                var result = dialog.ShowDialog();

                if ( ! result.HasValue || ! result.Value )
                {
                    return;
                }

                using ( var stream = System.IO.File.Create( dialog.FileName ) )
                {
                    BitmapEncoder encoder = new BmpBitmapEncoder();

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

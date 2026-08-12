using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RabbitOM.Player.Controls
{
    // to the replacement of the actual implementation located on the MainWindow class
    public partial class MediaControl : UserControl
    {
        public MediaControl()
        {
            InitializeComponent();
        }







        public static readonly RoutedEvent CommunicationStartedEvent = EventManager.RegisterRoutedEvent( "CommunicationStarted" , RoutingStrategy.Bubble , typeof(RoutedEventHandler) , typeof(MediaControl) );

        public static readonly RoutedEvent CommunicationStoppedEvent = EventManager.RegisterRoutedEvent( "CommunicationStopped" , RoutingStrategy.Bubble , typeof(RoutedEventHandler) , typeof(MediaControl) );

        public static readonly RoutedEvent ConnectedEvent = EventManager.RegisterRoutedEvent( "Connected" , RoutingStrategy.Bubble , typeof(RoutedEventHandler) , typeof(MediaControl) );

        public static readonly RoutedEvent DisconnectedEvent = EventManager.RegisterRoutedEvent( "Disconnected" , RoutingStrategy.Bubble , typeof(RoutedEventHandler) , typeof(MediaControl) );










        public event RoutedEventHandler CommunicationStarted
        {
            add    => AddHandler( CommunicationStartedEvent , value );
            remove => RemoveHandler( CommunicationStartedEvent , value );
        }

        public event RoutedEventHandler CommunicationStopped
        {
            add    => AddHandler( CommunicationStoppedEvent , value );
            remove => RemoveHandler( CommunicationStoppedEvent , value );
        }

        public event RoutedEventHandler Connected
        {
            add    => AddHandler( ConnectedEvent , value );
            remove => RemoveHandler( ConnectedEvent , value );
        }

        public event RoutedEventHandler Disconnected
        {
            add    => AddHandler( DisconnectedEvent , value );
            remove => RemoveHandler( DisconnectedEvent , value );
        }










        public bool IsCommunicationStarted()
        {
            throw new NotImplementedException();
        }

        public void StartCommunication( string uri )
        {
            throw new NotImplementedException();
        }

        public void StartCommunicationWithUdp( string uri , int port )
        {
            throw new NotImplementedException();
        }

        public void StartCommunicationWithMulticast( string uri , string address , int port )
        {
            throw new NotImplementedException();
        }

        public void StartCommunicationWithMulticast( string uri , string address , int port , byte ttl )
        {
            throw new NotImplementedException();
        }

        public void StopCommunication()
        {
            throw new NotImplementedException();
        }

        public ImageSource GetImage()
        {
            throw new NotImplementedException();
        }








        private void OnLoaded( object sender , RoutedEventArgs e )
        {
            throw new NotImplementedException();
        }

        private void OnUnloaded( object sender , RoutedEventArgs e )
        {
            throw new NotImplementedException();
        }







        protected virtual void OnCommunicationStarted( RoutedEventArgs e )
        {
            RaiseEvent( new RoutedEventArgs( CommunicationStartedEvent , this ) );
        }

        protected virtual void OnCommunicationStopped( RoutedEventArgs e )
        {
            RaiseEvent( new RoutedEventArgs( CommunicationStoppedEvent , this ) );
        }

        protected virtual void OnConnected( RoutedEventArgs e )
        {
            RaiseEvent( new RoutedEventArgs( ConnectedEvent , this ) );
        }

        protected virtual void OnDisconnected( RoutedEventArgs e )
        {
            RaiseEvent( new RoutedEventArgs( DisconnectedEvent , this ) );
        }
    }
}

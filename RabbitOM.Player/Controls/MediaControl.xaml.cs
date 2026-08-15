// this component is used to display live stream based on rtsp uri
// and... it does not used the state gof pattern, so why ?
// normally a player has different states like: playing, paused, stopped, etc...
// but here we have 2 states only: started and stopped. That's all.
// it doesn"t really make sense to make a pause during a live
// but not for replay yes. And this component is design for live streaming only
// for instance, for the RtspV2, a RtspPlaybackReceiver must be used instead to control playback
// RtspV2.Receivers has been introduce to used and to scale individual receviers most for live and the rest for playbacks
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RabbitOM.Player.Controls
{
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

        public static readonly RoutedEvent FrameReceivedEvent = EventManager.RegisterRoutedEvent( "Disconnected" , RoutingStrategy.Bubble , typeof(RoutedEventHandler) , typeof(MediaControl) );










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

        public event RoutedEventHandler FrameReceived
        {
            add    => AddHandler( FrameReceivedEvent , value );
            remove => RemoveHandler( FrameReceivedEvent , value );
        }







        public static readonly DependencyProperty UriProperty = DependencyProperty.Register( "Uri", typeof(string) , typeof(MediaControl) );

        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register( "UserName", typeof(string) , typeof(MediaControl) );

        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register( "Password", typeof(string) , typeof(MediaControl) );

        public static readonly DependencyProperty TransportProperty = DependencyProperty.Register( "Transport", typeof(MediaPlayerTransport) , typeof(MediaControl) );








        public string Uri
        {
            get => GetValue( UriProperty ) as string;
            set => SetValue( UriProperty , value );
        }

        public string UserName
        {
            get => GetValue( UserNameProperty ) as string;
            set => SetValue( UserNameProperty , value );
        }

        public string Password
        {
            get => GetValue( PasswordProperty ) as string;
            set => SetValue( PasswordProperty , value );
        }

        public MediaPlayerTransport Transport
        {
            get => GetValue( TransportProperty ) as MediaPlayerTransport;
            set => SetValue( TransportProperty , value );
        }










        private void OnLoaded( object sender , RoutedEventArgs e )
        {
        }

        private void OnUnloaded( object sender , RoutedEventArgs e )
        {
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
        }

        public ImageSource GetImage()
        {
            throw new NotImplementedException();
        }



        protected virtual void OnCommunicationStarted()
        {
            RaiseEvent( new RoutedEventArgs( CommunicationStartedEvent , this ) );
        }

        protected virtual void OnCommunicationStopped()
        {
            RaiseEvent( new RoutedEventArgs( CommunicationStoppedEvent , this ) );
        }

        protected virtual void OnConnected()
        {
            RaiseEvent( new RoutedEventArgs( ConnectedEvent , this ) );
        }

        protected virtual void OnDisconnected()
        {
            RaiseEvent( new RoutedEventArgs( DisconnectedEvent , this ) );
        }

        protected virtual void OnFrameReceived()
        {
            RaiseEvent( new RoutedEventArgs( FrameReceivedEvent , this ) );
        }
    }
}

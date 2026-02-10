using RabbitOM.Streaming.Threading;
using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    public abstract class RtspMediaReceiver : IReceiver , IDisposable
    {
        public event EventHandler<RtspCommunicationStartedEventArgs> CommunicationStarted;
        public event EventHandler<RtspCommunicationStoppedEventArgs> CommunicationStopped;
        public event EventHandler<RtspConnectedEventArgs> Connected;
        public event EventHandler<RtspDisconnectedEventArgs> Disconnected;
        public event EventHandler<RtspStreamingStartedEventArgs> StreamingStarted;
        public event EventHandler<RtspStreamingStoppedEventArgs> StreamingStopped;
        public event EventHandler<RtspStreamingStatusChangedEventArgs> StreamingStatusChanged;
        public event EventHandler<RtspDataReceivedEventArgs> DataReceived;
        public event EventHandler<RtspErrorEventArgs> Error;

      




        ~RtspMediaReceiver()
        {
            Dispose( false );
        }
        




        private readonly BackgroundWorker _worker = new BackgroundWorker( "RTSP - Receiver" );





        public bool IsCommunicationStarted { get => _worker.IsStarted; }
        public bool IsCommunicationStopping { get => _worker.IsStopping; }
        public abstract bool IsConnected { get; }
        public abstract bool IsStreamingStarted { get; }
        public abstract bool IsReceivingData { get; }






        public bool StartCommunication()
        {
            return _worker.Start( () =>
            {
                OnCommunicationStarted( new RtspCommunicationStartedEventArgs() );

                try
                {
                    using ( var stateMachine = CreateStateMachine() )
                    {
                        while ( _worker.CanContinue( stateMachine.IdleTime ) )
                        {
                            stateMachine.Run();
                        }
                    }
                }
                catch( Exception ex )
                {
                    OnError( new RtspInternalErrorEventArgs( ex.Message ) );
                }

                OnCommunicationStopped( new RtspCommunicationStoppedEventArgs() );
            } );
        }

        public void StopCommunication()
        {
            _worker.Stop();
        }

        public void StopCommunication(TimeSpan timeout)
        {
            _worker.Stop( timeout );
        }

        public void Dispose()
        {
            StopCommunication();

            Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }

        protected void EnsureNotStarted()
        {
            if ( _worker.IsStarted )
            {
                throw new InvalidOperationException( "the operation can not be executed while the receiver is still running" );
            }
        }

        protected abstract RtspStateMachine CreateStateMachine();







        public static void RaiseEvents( RtspMediaReceiver receiver , EventArgs e )
        {
            if ( receiver == null )
            {
                throw new ArgumentNullException( nameof( receiver ) );
            }

            if ( e == null )
            {
                throw new ArgumentNullException( nameof( e ) );
            }

            if ( e is RtspCommunicationStartedEventArgs || e is RtspCommunicationStoppedEventArgs )
            {
                throw new InvalidOperationException();
            }

            switch( e )
            {
                case RtspConnectedEventArgs evt:
                    receiver.OnConnected( evt );
                    break;

                case RtspDisconnectedEventArgs evt:
                    receiver.OnDisconnected( evt );
                    break;

                case RtspStreamingStartedEventArgs evt:
                    receiver.OnStreamingStarted( evt );
                    break;

                case RtspStreamingStoppedEventArgs evt:
                    receiver.OnStreamingStopped( evt );
                    break;

                case RtspStreamingStatusChangedEventArgs evt:
                    receiver.OnStreamingStatusChanged( evt );
                    break;

                case RtspDataReceivedEventArgs evt:
                    receiver.OnDataReceived( evt );
                    break;

                case RtspErrorEventArgs evt:
                    receiver.OnError( evt );
                    break;

                default:
                    throw new NotSupportedException();
            }
        }






        

        protected virtual void OnCommunicationStarted(RtspCommunicationStartedEventArgs e)
        {
            CommunicationStarted?.TryInvoke( this , e );
        }

        protected virtual void OnCommunicationStopped(RtspCommunicationStoppedEventArgs e)
        {
            CommunicationStopped?.TryInvoke( this , e );
        }

        protected virtual void OnConnected(RtspConnectedEventArgs e)
        {
            Connected?.TryInvoke( this , e );
        }

        protected virtual void OnDisconnected(RtspDisconnectedEventArgs e)
        {
            Disconnected?.TryInvoke( this , e );
        }

        protected virtual void OnStreamingStarted(RtspStreamingStartedEventArgs e)
        {
            StreamingStarted?.TryInvoke( this , e );
        }

        protected virtual void OnStreamingStopped(RtspStreamingStoppedEventArgs e)
        {
            StreamingStopped?.TryInvoke( this , e );
        }

        protected virtual void OnStreamingStatusChanged(RtspStreamingStatusChangedEventArgs e)
        {
            StreamingStatusChanged?.TryInvoke( this , e );
        }

        protected virtual void OnDataReceived(RtspDataReceivedEventArgs e)
        {
            DataReceived?.TryInvoke( this , e );
        }

        protected virtual void OnError(RtspErrorEventArgs e)
        {
            Error?.TryInvoke( this , e );
        }
    }
}

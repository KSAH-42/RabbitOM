using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal abstract class RtspHeaderRegistry
    {
        public abstract int CountHeaders();
        
        public abstract int CountHeadersKeys();
        
        public abstract int ContainsHeader( string name );
        
        public abstract bool IsHeaderForbidden( string name );
        
        public abstract bool CanAddHeader( string name );
        
        public abstract bool CanAddHeader( string name , string value );
        
        public abstract void AddHeader( string name , string value );
        
        public abstract void RemoveHeader( string name );
        
        public abstract void RemoveHeader( string name , int index );
        
        public abstract void ClearHeaders();
        
        public abstract IEnumerable<string> GetHeaderNames();    
        
        public abstract string GetHeaderValue( string name );
        
        public abstract string GetHeaderValue( string name , int index );
        
        public abstract IEnumerable<string> GetHeaderValues( string name );
        
        public abstract TObject GetHeaderValueAs<TObject>( string name ) where TObject : class;
        
        public abstract void SetHeaderValueAs<TObject>( string name , TObject value ) where TObject : class;
        
        public abstract bool TryAddHeader( string name , string value );
        
        public abstract bool TryGetHeaderValues( string name , out IEnumerable<string> result );
        
        public abstract bool TryGetHeaderValue( string name , out string result );
        
        public abstract bool TryGetHeaderValueAt( string name , int index , out string result );
        
        public abstract IEnumerator<KeyValuePair<string,string>> GetEnumerator();





        protected virtual void OnHeaderAdded( string name , string value ) { }
        
        protected virtual void OnHeaderRemove( string name ) { }
        
        protected virtual void OnHeaderCleared( string name ) { }
    }
}

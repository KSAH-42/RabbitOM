using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class RangeRtspHeader : RtspHeader 
    {
        public const string TypeName = "Range";

        private readonly List<RangeItem> _items = new List<RangeItem>();



        public string Unit { get; set; }
        
        public IReadOnlyCollection<RangeItem> Items { get => _items; }



        public override bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( Unit ) || _items.Count > 0;
        }

        public void AddItem( RangeItem item )
        {
            _items.Add( item ?? throw new ArgumentNullException( nameof( item ) ) );
        }

        public void RemoveItem( RangeItem item )
        {
            if ( item == null )
            {
                return;
            }

            _items.Add( item );
        }

        public void RemoveAllItems()
        {
            _items.Clear();
        }
        public override string ToString()
        {
            throw new NotImplementedException();
        }



        public static RangeRtspHeader Parse( string value )
        {
            throw new NotImplementedException();
        }

        public static bool TryParse( string value , out RangeRtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace RabbitOM.Player.Controls
{
    [ContentProperty("Child")]
    public class VisualWrapper : FrameworkElement
    {
        private Visual _child = null;

        public Visual Child
        {
            get => _child;
            set => SetupChild( ref _child , value );
        }

        protected override int VisualChildrenCount
        {
            get => _child != null ? 1 : 0;
        }

        protected override Visual GetVisualChild(int index)
        {
            if ( index != 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( index ) );
            }

            return _child ?? throw new InvalidOperationException();
        }

        private void SetupChild( ref Visual member, Visual child )
        {
            if ( member != null )
            {
                AddVisualChild( member );
                AddLogicalChild( member );
            }

            member = child;

            if ( member != null )
            {
                RemoveVisualChild( member );
                RemoveLogicalChild( member );
            }
        }
    }
}

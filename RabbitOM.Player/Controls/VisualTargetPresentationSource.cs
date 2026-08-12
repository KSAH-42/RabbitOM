using System;
using System.Windows;
using System.Windows.Media;

namespace RabbitOM.Player.Controls
{
    public sealed class VisualTargetPresentationSource : PresentationSource, IDisposable
	{
		private readonly VisualTarget _visualTarget;

		private bool _isDisposed;

		public VisualTargetPresentationSource( HostVisual hostVisual )
		{
			_visualTarget = new VisualTarget( hostVisual );

			AddSource();
		}

		public override Visual RootVisual
		{
			get => _visualTarget.RootVisual;

			set
			{
				Visual oldRoot = _visualTarget.RootVisual;

				_visualTarget.RootVisual = value;

				RootChanged( oldRoot, value );

                if ( value is UIElement rootElement )
                {
                    rootElement.Measure( new Size( Double.PositiveInfinity , Double.PositiveInfinity ) );
                    rootElement.Arrange( new Rect( rootElement.DesiredSize ) );
                }
            }
		}

		protected override CompositionTarget GetCompositionTargetCore()
		{
			return _visualTarget;
		}

		public override bool IsDisposed
		{
			get => _isDisposed;
		}

		public void Dispose()
		{
			RemoveSource();
			_isDisposed = true;
		}
	}
}

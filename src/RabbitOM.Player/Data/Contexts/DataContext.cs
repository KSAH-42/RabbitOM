using System;
using System.Windows;

namespace RabbitOM.Player.Data.Contexts
{
    public abstract class DataContext : DependencyObject
    {
        public static readonly DependencyProperty IsDirtyProperty = DependencyProperty.Register( nameof(IsDirty), typeof(bool), typeof(DataContext) );

        public bool IsDirty
        {
            get => (bool) GetValue( IsDirtyProperty );
            protected set => SetValue( IsDirtyProperty , value );
        }

        protected override void OnPropertyChanged( DependencyPropertyChangedEventArgs e )
		{
            base.OnPropertyChanged(e);

            if (e.Property.Name == nameof(IsDirty))
            {
                return;
            }

            if (!IsDirty)
            {
                IsDirty = true;
            }
		}
    }
}

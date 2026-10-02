using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace RabbitOM.Player.Themes.Styles
{
	public static class TabControlFadeBehavior
    {
        public static readonly DependencyProperty EnableFadeProperty = DependencyProperty.RegisterAttached( "EnableFade", typeof(bool), typeof(TabControlFadeBehavior), new PropertyMetadata(false, OnFadeStatusChanged));
        private static readonly DependencyProperty HasAnimatedProperty = DependencyProperty.RegisterAttached( "HasAnimated", typeof(bool), typeof(TabControlFadeBehavior), new PropertyMetadata(false));
        private static readonly DependencyProperty FadeTimeoutProperty = DependencyProperty.RegisterAttached( "FadeTimeout", typeof(TimeSpan), typeof(TabControlFadeBehavior), new PropertyMetadata(TimeSpan.FromMilliseconds( 422 ) ) );



        public static bool GetEnableFade(DependencyObject dependencyObject)
        {
            if ( dependencyObject == null )
            {
                return false;
            }

            return (bool) dependencyObject?.GetValue( EnableFadeProperty );
        }

        public static TimeSpan GetFadeTimeout(DependencyObject dependencyObject)
        {
            if ( dependencyObject == null )
            {
                return TimeSpan.Zero;
            }

            return (TimeSpan) dependencyObject?.GetValue( FadeTimeoutProperty );
        }

        public static void SetEnableFade(DependencyObject dependencyObject, bool value)
        {
            if ( dependencyObject == null )
            {
                return;
            }

            dependencyObject.SetValue( EnableFadeProperty, value);
        }

        public static void SetFadeTimeout(DependencyObject dependencyObject, TimeSpan value)
        {
            if ( dependencyObject == null )
            {
                return;
            }

            dependencyObject.SetValue( FadeTimeoutProperty, value);
        }









        private static void OnFadeStatusChanged( DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if ( dependencyObject == null )
            {
                return;
            }

            var control = dependencyObject as TabControl;

            if ( control == null )
            {
                return;
            }

            if ( (bool) e.NewValue )
            {
                WeakEventManager<TabControl, SelectionChangedEventArgs>.AddHandler(control,nameof(TabControl.SelectionChanged),OnSelectionChanged);
            }
            else
            {
                WeakEventManager<TabControl, SelectionChangedEventArgs>.RemoveHandler(control,nameof(TabControl.SelectionChanged),OnSelectionChanged);
            }
        }

		private static void OnSelectionChanged( object sender , SelectionChangedEventArgs e )
		{
            var control = sender as TabControl;

            if ( control == null )
            {
                return;
            }

            var presenter = control.Template.FindName("PART_SelectedContentHost", control) as ContentPresenter;

            if ( presenter == null )
            {
                return;
            }

            if ( ! (bool)control.GetValue(HasAnimatedProperty) )
            {
                control.SetValue( HasAnimatedProperty , true );
                return;
            }

            var timeout = (TimeSpan) control.GetValue( FadeTimeoutProperty );

            if ( timeout == TimeSpan.Zero )
            {
                presenter.Opacity = 1;
                return;
            }

            presenter.Opacity = 0;
            presenter.BeginAnimation( UIElement.OpacityProperty, new DoubleAnimation { From = 0, To = 1, Duration = timeout });
        }
	}
}

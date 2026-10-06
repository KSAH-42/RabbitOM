using ICSharpCode.AvalonEdit;
using System;
using System.Windows;

namespace RabbitOM.Player.Controls
{
	public static class TextEditorBehaviors
	{
		public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached( "Text", typeof(string), typeof(TextEditorBehaviors), new FrameworkPropertyMetadata( default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged));

        public static string GetText(DependencyObject dependencyObject)
        {
            if ( dependencyObject == null )
            {
                return string.Empty;
            }

            return dependencyObject.GetValue(TextProperty) as string ?? string.Empty;
        }

        public static void SetText(DependencyObject dependencyObject, string value)
        {
            if ( dependencyObject == null )
            {
                return;
            }

            dependencyObject.SetValue( TextProperty , value ?? string.Empty );
        }

        private static void OnTextChanged( DependencyObject dependencyObject , DependencyPropertyChangedEventArgs e )
        {
            if ( dependencyObject == null )
            {
                return;
            }

            var textEditor = dependencyObject as TextEditor;

            if ( textEditor == null )
            {
                return;
            }

            textEditor.TextChanged -= OnTextEditorValueChanged;

            if (textEditor.Text != (string)e.NewValue)
            {
                textEditor.Text = (string)e.NewValue;
            }

            textEditor.TextChanged += OnTextEditorValueChanged;
        }

        private static void OnTextEditorValueChanged( object sender, EventArgs e)
        {
            var textEditor = sender as TextEditor;

            if ( textEditor == null )
            {
                return;
            }

            textEditor.SetValue( TextProperty , textEditor.Text );
        }
	}
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RabbitOM.Player.Controls
{
	using RabbitOM.Player.Data;
	using RabbitOM.Player.Services;

	public partial class ScriptEditorControl : UserControl , IDisposable
	{
		public static readonly DependencyProperty ScriptProperty = DependencyProperty.Register( nameof(Script), typeof(string) , typeof(ScriptEditorControl) );
		public static readonly DependencyProperty IsRunningProperty = DependencyProperty.Register( nameof(IsScriptRunning), typeof(bool) , typeof(ScriptEditorControl) );


		public ScriptEditorControl()
		{
			InitializeComponent();

			DataContext = this;

			CommandBindings.Add( new CommandBinding( ClearCommand , (s,e) => Clear() ) );
			CommandBindings.Add( new CommandBinding( ResetCommand , (s,e) => Reset() ) );
			CommandBindings.Add( new CommandBinding( ImportCommand , (s,e) => Import() ) );
			CommandBindings.Add( new CommandBinding( ExportCommand , (s,e) => Export() ) );
			CommandBindings.Add( new CommandBinding( RunScriptCommand , (s,e) => RunScript() ) );
			CommandBindings.Add( new CommandBinding( StopScriptCommand , (s,e) => StopScript() ) );
		}



		public RoutedCommand ImportCommand { get; } = new RoutedCommand();

		public RoutedCommand ExportCommand { get; } = new RoutedCommand();

		public RoutedCommand RunScriptCommand { get; } = new RoutedCommand();

		public RoutedCommand StopScriptCommand { get; } = new RoutedCommand();

		public RoutedCommand ClearCommand { get; } = new RoutedCommand();

		public RoutedCommand ResetCommand { get; } = new RoutedCommand();




		public string Script
        {
            get => GetValue( ScriptProperty ) as string;
            set => SetValue( ScriptProperty , value );
        }

		public bool IsScriptRunning
		{
			get => (bool) GetValue( IsRunningProperty );
			private set => SetValue( IsRunningProperty , value );
		}




		public void Import()
		{
			throw new NotImplementedException();
		}

		public void Export()
		{
			throw new NotImplementedException();
		}

		public void RunScript()
		{
			// compile using task to not freeze the ui
		    // then show a dialog result only if the compilation failed and let the user to copy and paste error code
			// in another window, the best ui will expose a kind of text box without a using a dialog box. this is not an ide.
			// will do the same thing as "git extensions" when it display the output of the git process when making a push
			throw new NotImplementedException();
		}

		public void StopScript()
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			Script = string.Empty;
		}

		public void Reset()
		{
			Script = ResourceService.GetResourceFile( ResourceService.ScriptTemplate );
		}

		public void Dispose()
		{
			if ( IsScriptRunning )
			{
				StopScript();
			}
		}
	}
}

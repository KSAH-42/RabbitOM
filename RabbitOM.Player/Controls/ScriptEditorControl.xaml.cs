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

			CommandBindings.Add( new CommandBinding( ClearCommand , (s,e) => Clear() ) );
			CommandBindings.Add( new CommandBinding( ResetCommand , (s,e) => Reset() ) );
			CommandBindings.Add( new CommandBinding( LoadScriptCommand , (s,e) => LoadScript() ) );
			CommandBindings.Add( new CommandBinding( SaveScriptCommand , (s,e) => SaveScript() ) );
			CommandBindings.Add( new CommandBinding( RunScriptCommand , (s,e) => RunScript() ) );
			CommandBindings.Add( new CommandBinding( StopScriptCommand , (s,e) => StopScript() ) );
		}



		public RoutedCommand LoadScriptCommand { get; } = new RoutedCommand();

		public RoutedCommand SaveScriptCommand { get; } = new RoutedCommand();

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




		public void LoadScript()
		{
			throw new NotImplementedException();
		}

		public void SaveScript()
		{
			throw new NotImplementedException();
		}

		public void RunScript()
		{
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

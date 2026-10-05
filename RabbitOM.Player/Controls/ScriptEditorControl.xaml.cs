using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RabbitOM.Player.Controls
{
	using RabbitOM.Player.Services;

	public partial class ScriptEditorControl : UserControl
	{
		public static readonly DependencyProperty ScriptProperty = DependencyProperty.Register( nameof(Script), typeof(string) , typeof(ScriptEditorControl) );
		public static readonly DependencyProperty IsScriptRunningProperty = DependencyProperty.Register( nameof(IsScriptRunning), typeof(bool) , typeof(ScriptEditorControl) );
		public static readonly DependencyProperty RunnerProperty = DependencyProperty.Register( nameof(Runner), typeof(IScriptRunner) , typeof(ScriptEditorControl) , new PropertyMetadata( null , OnScriptRunnerChanged ));


		public ScriptEditorControl()
		{
			InitializeComponent();

			DataContext = this;

			CommandBindings.Add( new CommandBinding( ClearCommand , OnClear ) );
			CommandBindings.Add( new CommandBinding( ResetCommand , OnReset ) );
			CommandBindings.Add( new CommandBinding( ImportCommand , OnImport ) );
			CommandBindings.Add( new CommandBinding( ExportCommand , OnExport ) );
			CommandBindings.Add( new CommandBinding( StartStopCommand , OnStartStop , OnCanStartStop) );

			Reset();
		}




		public RoutedCommand ImportCommand { get; } = new RoutedCommand();

		public RoutedCommand ExportCommand { get; } = new RoutedCommand();

		public RoutedCommand StartStopCommand { get; } = new RoutedCommand();

		public RoutedCommand ClearCommand { get; } = new RoutedCommand();

		public RoutedCommand ResetCommand { get; } = new RoutedCommand();






		public string Script
        {
            get => GetValue( ScriptProperty ) as string;
            set => SetValue( ScriptProperty , value );
        }

		public bool IsScriptRunning
		{
			get => (bool) GetValue( IsScriptRunningProperty );
			private set => SetValue( IsScriptRunningProperty , value );
		}

		public IScriptRunner Runner
		{
			get => (IScriptRunner) GetValue( RunnerProperty );
			set => SetValue( RunnerProperty , value );
		}





		public void RunScript()
		{
			try
			{
				if ( Runner != null && ! Runner.IsRunning )
				{
					Runner.Run( Script );
				}
			}
			catch( Exception ex )
			{
				MessageBox.Show( ex.ToString() );
			}
			finally
			{
				IsScriptRunning = Runner?.IsRunning ?? false;
			}
		}

		public void StopScript()
		{
			try
			{
				Runner?.Terminate();
			}
			catch( Exception ex )
			{
				MessageBox.Show( ex.ToString() );
			}
			finally
			{
				IsScriptRunning = Runner?.IsRunning ?? false;
			}
		}

		public void Import( string fileName )
		{
			try
			{
				Script = File.ReadAllText( fileName );
			}
			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		public void Export( string fileName )
		{
			try
			{
				File.WriteAllText( fileName , Script );
			}
			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		public void Reset()
		{
			Script = ResourceService.GetResourceFile( ResourceService.ScriptTemplate );
		}

		public void Clear()
		{
			Script = string.Empty;
		}






		private void OnCanStartStop( object sender , CanExecuteRoutedEventArgs e )
		{
			e.CanExecute = Runner != null;
		}

		private void OnStartStop( object sender , ExecutedRoutedEventArgs e )
		{
			if ( IsScriptRunning )
			{
				StopScript();
			}
			else
			{
				RunScript();
			}
		}

		private void OnImport( object sender , ExecutedRoutedEventArgs e )
		{
			var dialog = new OpenFileDialog() { Filter = "XML file (*.xml)|*.xml", CheckFileExists = false };

			if ( dialog.ShowDialog() == true )
			{
				Import( dialog.FileName );
			}
		}

		private void OnExport( object sender , ExecutedRoutedEventArgs e )
		{
			var dialog = new SaveFileDialog() { Filter = "XML file (*.xml)|*.xml" };

			if ( dialog.ShowDialog() == true )
			{
				Export( dialog.FileName );
			}
		}

		private void OnReset( object sender , ExecutedRoutedEventArgs e )
		{
			Reset();
		}

		private void OnClear( object sender , ExecutedRoutedEventArgs e )
		{
			Clear();
		}



		private static void OnScriptRunnerChanged( DependencyObject dependencyObject , DependencyPropertyChangedEventArgs e )
		{
			var control = dependencyObject as ScriptEditorControl;

			if ( control == null )
			{
				return;
			}

			var runner = e.NewValue as IScriptRunner;

			control.IsScriptRunning = runner?.IsRunning ?? false;
		}
	}
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

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

			CommandBindings.Add( new CommandBinding( ClearCommand , OnClearScript , OnCanClearScript ) );
			CommandBindings.Add( new CommandBinding( ResetCommand , OnResetScript , OnCanResetScript ) );
			CommandBindings.Add( new CommandBinding( ImportCommand , OnImportScript , OnCanImportScript ) );
			CommandBindings.Add( new CommandBinding( ExportCommand , OnExportScript , OnCanExportScript ) );
			CommandBindings.Add( new CommandBinding( RunCommand , OnRunScript , OnCanRunScript ) );
			CommandBindings.Add( new CommandBinding( StopCommand , OnStopScript , OnCanStopScript ) );

			Script = ResourceService.GetResourceFile( ResourceService.ScriptTemplate );
		}



		public RoutedCommand ImportCommand { get; } = new RoutedCommand();

		public RoutedCommand ExportCommand { get; } = new RoutedCommand();

		public RoutedCommand RunCommand { get; } = new RoutedCommand();

		public RoutedCommand StopCommand { get; } = new RoutedCommand();

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




		public void Dispose()
		{
		}




		private void OnCanImportScript( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

		private void OnCanExportScript( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

		private void OnCanRunScript( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

		private void OnCanStopScript( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

		private void OnCanClearScript( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

		private void OnCanResetScript( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

		private void OnImportScript( object sender , ExecutedRoutedEventArgs e )
        {
        }

		private void OnExportScript( object sender , ExecutedRoutedEventArgs e )
        {
        }

		private void OnRunScript( object sender , ExecutedRoutedEventArgs e )
        {
        }

		private void OnStopScript( object sender , ExecutedRoutedEventArgs e )
        {
        }

		private void OnClearScript( object sender , ExecutedRoutedEventArgs e )
        {
        }

		private void OnResetScript( object sender , ExecutedRoutedEventArgs e )
        {
        }
	}
}

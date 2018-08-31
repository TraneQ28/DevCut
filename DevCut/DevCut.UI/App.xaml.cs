using DevCut.UI.ViewModels;
using DevCut.UI.Views;
using System.Windows;

namespace DevCut.UI
{
	/// <summary>
	/// Interaktionslogik für "App.xaml"
	/// </summary>
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			var mainWindow = new MainWindow();
			var viewModel = new MainViewModel();
			mainWindow.DataContext = viewModel;
			mainWindow.Show();
		}
	}
}

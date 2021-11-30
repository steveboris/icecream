using System.Windows;

namespace IceCream
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			// Create the startup window
			MainWindow wnd = new MainWindow();
			wnd.Title = "Monitor 1";
			Monitor monitor = new Monitor();
			monitor.Title = "Monitor 2";
			// Show the window
			wnd.Show();
			monitor.Show();
		}
	}
}

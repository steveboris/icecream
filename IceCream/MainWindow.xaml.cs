using System.Windows;

using IceCream.ViewModel;

namespace IceCream
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new StationViewModel();
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _viewModel;
        }
        private readonly StationViewModel _viewModel;
    }
}

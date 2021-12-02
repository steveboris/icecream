using System.Windows;

using IceCream.ViewModel;

namespace IceCream
{
    /// <summary>
    /// Interaction logic for Monitor.xaml
    /// </summary>
    public partial class Monitor : Window
    {
        public Monitor()
        {
            InitializeComponent();

            _viewModel = new StationViewModel();
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _viewModel;
        }
        private readonly StationViewModel _viewModel;
    }
}

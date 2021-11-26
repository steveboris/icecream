using IceCream.Model;
using IceCream.ViewModel;
using System.Windows;
using System.Windows.Controls;

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


        private void LvSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            var lv = sender as ListView;
            if (lv == null) return;
            SelectedStation = (Station)lv.SelectedItem;
            _viewModel.SelectedStation = SelectedStation;
        }

        private readonly StationViewModel _viewModel;
        private Station SelectedStation;
    }
}

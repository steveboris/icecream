using System.Collections.ObjectModel;

using IceCream.Interfaces;

namespace IceCream.Model
{
    public class StationCollection : IStationCollection
    {
        public StationCollection()
        {
            _stations = _fileWatcher.GetStationsList();
        }

        public ObservableCollection<Station> Stations()
        {
            return _stations;
        }

        // Properties
        private FileWatcher _fileWatcher = new FileWatcher();
        private ObservableCollection<Station> _stations = new ObservableCollection<Station>();
    }
}

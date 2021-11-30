using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

using IceCream.Interfaces;
using IceCream.Model;

namespace IceCream.ViewModel
{
    public class StationViewModel : ViewModel, IViewModel
    {
        public StationViewModel()
        {
            // upload existing stations
            Load();
        }

        private void Load()
        {
            //Stations = fileWatcher.GetStation();
            if (fileWatcher.GetStationsList() != null)
            {
                foreach (Station station in fileWatcher.GetStationsList())
                {
                    _stationList.Add(station);
                }
            }

            // On collection changed
            fileWatcher.GetStationsList().CollectionChanged += OnCollectionChanged;
        }

        public void OnCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (Station station in e.NewItems)
            {
                // Add the station to the display
                UpdateViewModel(station);
            }
            // Notify station added
            MessageBox.Show("New Station added! ID: " + _stationList.Last().StationID);
        }

        /// <summary>
        /// Update the Data in the list
        /// </summary>
        /// <param name="_station">
        /// The current station to be displayed
        /// </param>
        private void UpdateViewModel(Station _station)
        {
            ObservableCollection<Station> stations = new ObservableCollection<Station>();

            // First make a copy of the station that already exists before the new station
            foreach (Station station in StationList)
            {
                stations.Add(station);
            }
            stations.Add(_station);
            // Set the new value of the list
            StationList = stations;
        }

        public string StationID
        { 
            get
            {
                if (_currentSelectedStation != null)
                    return _currentSelectedStation.StationID;

                return string.Empty;
            }
            set
            {
                _currentSelectedStation.StationID = value;
            } 
        }

        public string Date 
        { 
            get
            {
                if (_currentSelectedStation != null)
                    return _currentSelectedStation.Date;

                return string.Empty;
            }
            set
            {
                _currentSelectedStation.Date = value;
            }
        }

        public int Target 
        {
            get 
            {
                if (_currentSelectedStation != null)
                    return _currentSelectedStation.Target;

                return 0;
            }
            set 
            {
                _currentSelectedStation.Target = value;
            } 
        }

        public int Actual
        { 
            get
            {
                if (_currentSelectedStation != null)
                    return _currentSelectedStation.Actual;

                return 0;
            }
            set
            {
                _currentSelectedStation.Actual = value;

                // If actual has changed, the variance needs to be updated as well.
                OnPropertyChanged(nameof(Actual));
                UpdateOnActualValueChanged();
            }
        }

        public int Variance
        { 
            get
            {
                if (_currentSelectedStation != null)
                    return _currentSelectedStation.Actual - _currentSelectedStation.Target;

                return 0;
            } 
            set
            {
                _currentSelectedStation.Variance = value;
                OnPropertyChanged(nameof(Variance));
            }
        }

        public string Color 
        { 
            get
            {
                if (_currentSelectedStation != null)
                {
                    var diff = ComputeDifference(Variance, Target);

                    if (diff == 1)
                        _color = ColorTypes.Red;
                    else if (diff == 2)
                        _color = ColorTypes.Green;
                    else
                        _color = ColorTypes.Black;

                    return _color;
                }

                return string.Empty;
            }
            set
            {
                SetProperty(ref _color, value);
                OnPropertyChanged(nameof(Variance));
            }
        }

        public Station CurrentSelectedStation
        {
            get => _currentSelectedStation;
            set
            {
                //_currentSelectedStation = value;
                SetProperty(ref _currentSelectedStation, value);

                // If the selection has changed, the others fields needs to be updated as well 
                UpdateSelectedStationChanged();
            }
        }

        public void UpdateSelectedStationChanged()
        {
            OnPropertyChanged(nameof(StationID));
            OnPropertyChanged(nameof(Target));
        }

        public void UpdateOnActualValueChanged()
        {
            OnPropertyChanged(nameof(Variance));
            OnPropertyChanged(nameof(Color));
        }

        public ObservableCollection<Station> StationList 
        {
            get => _stationList;
            set
            {
                _stationList = value;
                OnPropertyChanged(nameof(StationList));
            }
        }

        public int ComputeDifference(int variance, int target)
        {
            if (variance <= (target * 10 / 100))
                return 1;

            else if (variance >= (-target * 5 / 100))
                return 2;

            return 0;
        }

        // Properties
        private string _notification;
        private string _color;
        private Station _currentSelectedStation;
        private ObservableCollection<Station> _stationList = new ObservableCollection<Station>();
        FileWatcher fileWatcher = new FileWatcher();
    }
}

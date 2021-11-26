using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

using IceCream.Model;

namespace IceCream.ViewModel
{
    internal class StationViewModel : INotifyPropertyChanged
    {
               
        public StationViewModel()
        {
            Load();
        }

        private void Load()
        {
            //Stations = fileWatcher.GetStation();
            if (fileWatcher.GetStation() != null)
            {
                foreach(Station station in fileWatcher.GetStation())
                {
                    _stationList.Add(station);
                }
            }

            NextStation();
            fileWatcher.GetStation().CollectionChanged += StationViewModel_CollectionChanged;
        }

        private void StationViewModel_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (Station station in e.NewItems)
            {
                UpdateViewModel(station);
            }
        }

        /// <summary>
        /// Update the Data in the list
        /// </summary>
        /// <param name="_station"></param>
        private void UpdateViewModel(Station _station)
        {
            ObservableCollection<Station> stations = new ObservableCollection<Station>();

            // First make a copy of the station that already exists before the new station
            foreach (Station station in Stations)
            {
                stations.Add(station);  
            }
            stations.Add(_station);
             
            // Set the new value of the list
            Stations = stations;

            // Notify new station added
            NewStationText = string.Format("New Station added: {0}", _station.StationID);
        }

        public ObservableCollection<Station> Stations
        {
            get => _stationList;
            set 
            {
                _stationList = value;
                OnPropertyChanged(nameof(Stations));
            }
        }
        
        /// <summary>
        /// Calls when any property of the current selected station changed.
        /// Compute the difference between the target and the variance to what color to be displayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Station_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Station st = sender as Station;
            if (st != null)
            {
                var diff = ComputeDifference(st.Variance, st.Target);

                if (diff == 2)
                    _color = RED;
                else if (diff == 1)
                    _color = GREEN;
                else
                    _color = "";

                Color = _color;
            }
        }

        public void NextStation()
        {
            if (_stationList.Count > 0)
            {
                if (SelectedStation == null)
                    SelectedStation = _stationList[0];

                int index = _stationList.IndexOf(_stationList.First(s => s.StationID == SelectedStation.StationID));
                if (index < _stationList.Count)
                {
                    SelectedStation = _stationList[index];
                    SetCurrentStation();
                }
            }
        }

        public void SetCurrentStation()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(_stationList);
            if (view != null)
                view.MoveCurrentTo(SelectedStation);
        }

        private int ComputeDifference(int variance, int target)
        {
            if (target < 0) target = -target;

            if (variance >= (target * 10/100))
            {
                // if the diff is 10% or more below the target
                return 1;
            }
            else if (variance <= (target * 5/100))
            {
                // if the diff is 5% or more above the target
                return 2;
            }

            // otherwise
            return 0;
        }

        public string Color
        {
            get
            { 
                return _color;
            }
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public Station SelectedStation
        {
            get => _selectedStation;
            set
            {
                if (_selectedStation != value)
                    _selectedStation = value;

                // reset the default color
                Color = RED;
                // reset the notification text station added
                NewStationText = string.Empty;
                _selectedStation.PropertyChanged += Station_PropertyChanged;
                //OnPropertyChanged(nameof(SelectedStation));
                //OnPropertyChanged(nameof(Color));
            }
        }

        public string NewStationText
        {
            get => _text;
            set
            {
                _text = value;
;                OnPropertyChanged(nameof(NewStationText));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        FileWatcher fileWatcher = new FileWatcher();
        private ObservableCollection<Station> _stationList = new ObservableCollection<Station>();
        private Station _selectedStation;

        private string _color = RED;
        const string RED = "#FF0000";
        const string GREEN = "#00FF00";
        string _text = "";
    }
}

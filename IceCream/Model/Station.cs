using System.ComponentModel;

namespace IceCream.Model
{
    // INotifyPropertyChanged notifies the View of property changes, so that Bindings are updated.
    internal class Station : INotifyPropertyChanged
    {
        public Station(string stationid, int target)
        {
            this.stationID = stationid;
            this.target = target;
        }

        public string StationID 
        {
            get => stationID;
            set
            {
                stationID = value;
                //OnPropertyChanged(nameof(StationID));
            }
        }

        public string Date
        {
            get => date; 
            set 
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }

            /*
             get => date;
             set { SetProperty(ref date, value); }
             */
        }

        public int Target 
        {
            get => target;
            set
            {
                target = value;
                OnPropertyChanged(nameof(Target));
            }
        }

        public int Actual 
        {
            get => actual;
            set
            {
                actual = value;
                OnPropertyChanged(nameof(Actual));

                //Also update the variance value
                OnPropertyChanged(nameof(Variance));
            }
        }

        public int Variance 
        {
            get => actual - target;
            set
            {
                variance = value;
                OnPropertyChanged(nameof(Variance));
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

        private string stationID;
        private string date;
        private int actual;
        private int variance;
        private int target;
    }
}

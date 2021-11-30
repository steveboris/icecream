using System.Collections.ObjectModel;

using IceCream.Model;

namespace IceCream.Interfaces
{
    /// <summary>
    /// Interface that will implement the Views
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Binding Property. Where the value of the Stationid will be displayed.
        /// </summary>
        public string StationID { get; set; }

        /// <summary>
        /// Binding Property. Where the value of the date will be displayed.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Binding Property. Where the value of the target will be displayed.
        /// </summary>
        public int Target { get; set; }

        /// <summary>
        /// Binding Property. Where the value of the actual will be displayed.
        /// </summary>
        public int Actual { get; set; }

        /// <summary>
        /// Binding Property. Where the value of the variance will be displayed.
        /// </summary>
        public int Variance { get; set; }

        /// <summary>
        /// Binding Property. This will be used to highlight the value of the variance. 
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Binding Property. Text to be display when a new station has been added.
        /// </summary>
        public string StationAddedText { get; set; }

        /// <summary>
        /// This property is a binding element, that has to be added into the view file. 
        /// This will help to save the current selected Station from the list.
        /// </summary>
        Station CurrentSelectedStation { get; set; }

        /// <summary>
        /// Calls this method to update the value of the StationIc and Target, when current selected station changed.
        /// </summary>
        void UpdateSelectedStationChanged();

        /// <summary>
        /// Calls this method to update the value of the Variance and color, when the value of actual changed.
        /// </summary>
        void UpdateOnActualValueChanged();

        /// <summary>
        /// List of stations to be displaying.
        /// </summary>
        ObservableCollection<Station> StationList { get; set; }

        /// <summary>
        /// Compute the difference between the target and the actual value to know what color
        /// will be used to highlight the value if the variance.
        /// </summary>
        /// <returns> 
        /// 1 if the diff is 10% or more below the target 
        /// 2 if the diff is 5% or more above the target
        /// 0 otherwise
        /// </returns>
        int ComputeDifference(int variance, int target);

        /// <summary>
        /// Calls this method when the selection changed. This has to be connect with the collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e);

        /// <summary>
        /// Calls this method to notify users when a new station has been added.
        /// </summary>
        void NotifyOnStationAdded();

        /// <summary>
        /// Clears notification on station changed.
        /// </summary>
        void ClearNotification();
    }
}

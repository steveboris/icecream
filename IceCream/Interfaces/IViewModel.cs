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
    }
}

using IceCream.Model;
using System.Collections.ObjectModel;

namespace IceCream.Interfaces
{
    public interface IStationCollection
    {
        ObservableCollection<Station> Stations();
    }
}

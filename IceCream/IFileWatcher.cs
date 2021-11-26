using System.Collections.Generic;
using System.Collections.ObjectModel;

using IceCream.Model;

namespace IceCream
{
    interface IFileWatcher
    {
       public  ObservableCollection<Station> GetStation();
    }
}

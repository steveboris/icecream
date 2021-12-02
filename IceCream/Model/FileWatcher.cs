using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using IceCream.Model;

namespace IceCream
{
    public class FileWatcher
    {
        private string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public FileWatcher()
        {
            fileSystemWatcher = new FileSystemWatcher();

            fileSystemWatcher.Path = Path;
            //fileSystemWatcher.Filter = "ice.txt";
            fileSystemWatcher.IncludeSubdirectories = true;
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastAccess | 
                                             NotifyFilters.LastWrite | 
                                             NotifyFilters.FileName | 
                                             NotifyFilters.DirectoryName;
            
            fileSystemWatcher.Changed += OnChanged;
            fileSystemWatcher.Error += OnError;

            fileSystemWatcher.EnableRaisingEvents = true;

            // Adding default station
            _stations.Add(new Station()
            {
                StationID = "NV141",
                Target = 42
            });
            //_stations.Add(new Station("NV142", 76));
        }

        public ObservableCollection<Station> GetStationsList() => _stations;

        public void OnChanged(object sender, FileSystemEventArgs e)
        {
            //var filename = e.Name;
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            try
            {
                FileStream fs = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read);

                string line = String.Empty;
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        Trace.WriteLine(line);

                        string[] parts = line.Split(',');

                        try
                        {
                            int target = int.Parse(parts[1]);
                            if (parts.Length == 2)
                            {
                                // add new station if it is not already exists
                                var stationExists = _stations.FirstOrDefault(x => x.StationID == parts[0] && x.Target == target);
                                if (stationExists == null)
                                    _stations.Add(new Station()
                                    {
                                        StationID = parts[0],
                                        Target = target
                                    });

                                GetStationsList();
                            }
                        }
                        catch { }
                    }
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }

        }

        private void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private void PrintException(Exception ex)
        {
            if (ex != null)
            {
                Trace.WriteLine($"Message: {ex.Message}");
                Trace.WriteLine("Stacktrace:");
                Trace.WriteLine(ex.StackTrace);
                PrintException(ex.InnerException);
            }
        }

        private FileSystemWatcher fileSystemWatcher;
        private ObservableCollection<Station> _stations = new ObservableCollection<Station>()  ;
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IceCream.Model;

namespace IceCream
{
    internal class FileWatcher : IFileWatcher
    {
        private string Path = @"C:\Users\Steve\Desktop\";
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
            fileSystemWatcher.Created += OnCreated;
            fileSystemWatcher.Deleted += OnDeleted;
            fileSystemWatcher.Renamed += OnRenamed;
            fileSystemWatcher.Error += OnError;

            fileSystemWatcher.EnableRaisingEvents = true;

            _stations.Add(new Station("NV141", 42));
        }

        public ObservableCollection<Station> GetStation() => _stations;

        public void OnChanged(object sender, FileSystemEventArgs e)
        {
            //var filename = e.Name;
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

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
                                _stations.Add(new Station(parts[0], target));

                            GetStation();
                        }
                    }
                    catch { }
                }
            }

        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";
            Trace.WriteLine(value);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e) =>
            Trace.WriteLine($"Deleted: {e.FullPath}");

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            Trace.WriteLine($"Renamed:");
            Trace.WriteLine($"    Old: {e.OldFullPath}");
            Trace.WriteLine($"    New: {e.FullPath}");
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

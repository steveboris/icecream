using IceCream.Model;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace IceCream
{
    class StationCollection : ObservableCollection<Station>
	{
		IFileWatcher file;

		public StationCollection()
        {
		}

        public override event NotifyCollectionChangedEventHandler? CollectionChanged;
	}
}


/*
 
public void ReadFileForWatch()
{
	OpenFileDialog fileD = new OpenFileDialog()
	{
		Filter = "Text (txt)|*.txt",
		FilterIndex = 1,
		Multiselect = false
	};

	var Filename = String.Empty;
	var Path = String.Empty;

	if (fileD.ShowDialog() == true)
	{
		string[] FullPath = fileD.FileName.ToString().Split('\\');
		for (var i = 0; i < FullPath.Length - 1; ++i)
		{
			Path += String.Format(@"{0}\", FullPath[i]);
		}

		Filename = FullPath[FullPath.Length - 1];
	}

	Console.WriteLine("path: " + Path + " name:" + Filename);

	var watcher = new FileSystemWatcher();
	watcher.Path = Path;

	watcher.NotifyFilter = NotifyFilters.Attributes
							| NotifyFilters.CreationTime
							| NotifyFilters.DirectoryName
							| NotifyFilters.FileName
							| NotifyFilters.LastAccess
							| NotifyFilters.LastWrite
							| NotifyFilters.Security
							| NotifyFilters.Size;

	watcher.Changed += OnChanged;
	watcher.Created += OnCreated;
	watcher.Deleted += OnDeleted;
	watcher.Renamed += OnRenamed;
	watcher.Error += OnError;

	watcher.Filter = Filename;
	watcher.IncludeSubdirectories = true;
	watcher.EnableRaisingEvents = true;
	Console.ReadLine();
}
private void OnChanged(object sender, FileSystemEventArgs e)
{
	if (e.ChangeType != WatcherChangeTypes.Changed)
	{
		return;
	}

	string line = String.Empty;
	FileStream fs = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read);

	using (var sr = new StreamReader(fs, Encoding.UTF8))
	{
		while ((line = sr.ReadLine()) != null)
		{
			Trace.WriteLine(line);
		}
	}

	Trace.WriteLine($"Changed: {e.FullPath}");
}

private void OnCreated(object sender, FileSystemEventArgs e)
{
	string value = $"Created: {e.FullPath}";
	Trace.WriteLine(value);
}

private void OnDeleted(object sender, FileSystemEventArgs e) =>
	Console.WriteLine($"Deleted: {e.FullPath}");

private void OnRenamed(object sender, RenamedEventArgs e)
{
	Console.WriteLine($"Renamed:");
	Console.WriteLine($"    Old: {e.OldFullPath}");
	Console.WriteLine($"    New: {e.FullPath}");
}

private void OnError(object sender, ErrorEventArgs e) =>
	PrintException(e.GetException());

private void PrintException(Exception ex)
{
	if (ex != null)
	{
        Console.WriteLine($"Message: {ex.Message}");
		Console.WriteLine("Stacktrace:");
		Console.WriteLine(ex.StackTrace);
		Console.WriteLine();
		PrintException(ex.InnerException);
	}
}
 */
using System;
using System.IO;
using System.Diagnostics;

namespace LayUI.Wpf.Controls.SVG.FileLoaders
{
    public class FileSystemLoader : IExternalFileLoader
    {
        static FileSystemLoader()
        {
            Instance = new FileSystemLoader();
        }

        public static FileSystemLoader Instance { get; }

        public Stream LoadFile(string hRef, string svgFilename)
        {
            var path = Environment.CurrentDirectory;
            if (!string.IsNullOrEmpty(svgFilename))
            {
                path = Path.GetDirectoryName(svgFilename);
            }
            string filename = Path.Combine(path, hRef);
            if (File.Exists(filename))
                return File.OpenRead(filename);

            // For the issue #43 : Environment.CurrentDirectory prevents msix packaging
            path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            filename = Path.Combine(path, hRef);
            if (File.Exists(filename))
                return File.OpenRead(filename);

            Trace.TraceWarning("Unresolved URI: " + hRef);

            return null;
        }
    }
}

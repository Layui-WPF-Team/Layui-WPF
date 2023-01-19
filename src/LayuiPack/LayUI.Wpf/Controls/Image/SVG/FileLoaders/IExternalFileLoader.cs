using System.IO;

namespace LayUI.Wpf.Controls.SVG.FileLoaders
{
    public interface IExternalFileLoader
    {
        Stream LoadFile(string hRef, string svgFilename);
    }
}

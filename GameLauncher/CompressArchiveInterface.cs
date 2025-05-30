using Aspose.Zip;
using Aspose.Zip.Bzip2;
using Aspose.Zip.Gzip;
using Aspose.Zip.Lzip;
using Aspose.Zip.Rar;
using Aspose.Zip.SevenZip;
using Aspose.Zip.Tar;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher
{
    public interface CompressArchiveInterface : System.IDisposable
    {
        void ExtractToDirectory(string dirName);
        string[] GetNames();
    }
    public class ZipCompressArchive : CompressArchiveInterface
    {
        private ZipArchive archive;
        public ZipCompressArchive(ZipArchive _archive)
        {
            archive = _archive;
        }
        public void ExtractToDirectory(string dirName) => archive.ExtractToDirectory(dirName);
        public string[] GetNames()
        {
            string[] names = new string[archive.Entries.Count];
            for (int i = 0; i < archive.Entries.Count; i++)
            {
                names[i] = archive.Entries[i].FullName;
            }
            return names;
        }
        public void Dispose() => archive.Dispose();
    }
    public class RarCompressArchive : CompressArchiveInterface
    {
        private RarArchive archive;
        public RarCompressArchive(RarArchive _archive)
        {
            archive = _archive;
        }
        public void ExtractToDirectory(string dirName) => archive.ExtractToDirectory(dirName);
        public string[] GetNames()
        {
            string[] names = new string[archive.Entries.Count];
            for (int i = 0; i < archive.Entries.Count; i++)
            {
                names[i] = archive.Entries[i].Name;
                // ToDo: Make sure suffix / if is directory
            }
            return names;
        }
        public void Dispose() => archive.Dispose();
    }
    public class SevenZipCompressArchive : CompressArchiveInterface
    {
        private SevenZipArchive archive;
        public SevenZipCompressArchive(SevenZipArchive _archive)
        {
            archive = _archive;
        }
        public void ExtractToDirectory(string dirName) => archive.ExtractToDirectory(dirName);
        public string[] GetNames()
        {
            string[] names = new string[archive.Entries.Count];
            for (int i = 0; i < archive.Entries.Count; i++)
            {
                names[i] = archive.Entries[i].Name;
                // ToDo: Make sure suffix / if is directory
            }
            return names;
        }
        public void Dispose() => archive.Dispose();
    }
    public class TarCompressArchive : CompressArchiveInterface
    {
        private TarArchive archive;
        public TarCompressArchive(TarArchive _archive)
        {
            archive = _archive;
        }
        public void ExtractToDirectory(string dirName) => archive.ExtractToDirectory(dirName);
        public string[] GetNames()
        {
            string[] names = new string[archive.Entries.Count];
            for (int i = 0; i < archive.Entries.Count; i++)
            {
                names[i] = archive.Entries[i].Name;
                // ToDo: Make sure suffix / if is directory
            }
            return names;
        }
        public void Dispose() => archive.Dispose();
    }
    public class CompressArchiveClass
    {
        protected string dirExtractedPath;
        public string[] GetNamesFromDir()
        {
            string[] files = Directory.GetFiles(dirExtractedPath, "*", SearchOption.AllDirectories);
            string[] names = new string[files.Length];
            for (int i = 0; i < files.Length; ++i)
            {
                names[i] = files[i].Substring(dirExtractedPath.Length);
            }
            return names;
        }
    }
    public class GzipCompressArchive : CompressArchiveClass, CompressArchiveInterface
    {
        private GzipArchive archive;
        public GzipCompressArchive(GzipArchive _archive)
        {
            archive = _archive;
        }
        public void ExtractToDirectory(string dirName)
        {
            dirExtractedPath = dirName;
            archive.ExtractToDirectory(dirName);
        }
        public string[] GetNames() => GetNamesFromDir();
        public void Dispose() => archive.Dispose();
    }
    public class Bzip2CompressArchive : CompressArchiveClass, CompressArchiveInterface
    {
        private Bzip2Archive archive;
        public Bzip2CompressArchive(Bzip2Archive _archive)
        {
            archive = _archive;
        }
        public void ExtractToDirectory(string dirName) => archive.ExtractToDirectory(dirName);
        public string[] GetNames() => GetNamesFromDir();
        public void Dispose() => archive.Dispose();
    }
    public class LzipCompressArchive : CompressArchiveClass, CompressArchiveInterface
    {
        private LzipArchive archive;
        public LzipCompressArchive(LzipArchive _archive)
        {
            archive = _archive;
        }
        public void ExtractToDirectory(string dirName) => archive.ExtractToDirectory(dirName);
        public string[] GetNames() => GetNamesFromDir();
        public void Dispose() => archive.Dispose();
    }
    public class ZipAltCompressArchive : CompressArchiveInterface
    {
        private Archive archive;
        public ZipAltCompressArchive(Archive _archive)
        {
            archive = _archive;
        }
        public void ExtractToDirectory(string dirName) => archive.ExtractToDirectory(dirName);
        public string[] GetNames()
        {
            string[] names = new string[archive.Entries.Count];
            for (int i = 0; i < archive.Entries.Count; i++)
            {
                names[i] = archive.Entries[i].Name;
                // ToDo: Make sure suffix / if is directory
            }
            return names;
        }
        public void Dispose() => archive.Dispose();
    }
    public class CreateCompressArchiveInterface
    {
        public static CompressArchiveInterface Create(string fileName, bool doAltZip = false)
        {
            string ext = Path.GetExtension(fileName).ToLower();
            if (ext.Equals(".zip") && doAltZip)
                return new ZipAltCompressArchive(new Archive(fileName));
            if (ext.Equals(".zip"))
                return new ZipCompressArchive(ZipFile.OpenRead(fileName));
            if (ext.Equals(".7z") || ext.Equals(".7zip"))
                return new SevenZipCompressArchive(new SevenZipArchive(fileName));
            if (ext.Equals(".rar"))
                return new RarCompressArchive(new RarArchive(fileName));
            if (ext.Equals(".tar"))
                return new TarCompressArchive(new TarArchive(fileName));
            // The following three types will only work to call ExtractToDirectory, and do not fully support GetNames()
            if (ext.Equals(".gz") || ext.Equals(".gzip"))
                return new GzipCompressArchive(new GzipArchive(fileName));
            if (ext.Equals(".bz2") || ext.Equals(".bzip") || ext.Equals(".bzip2"))
                return new Bzip2CompressArchive(new Bzip2Archive(fileName));
            if (ext.Equals(".lz") || ext.Equals(".lzip"))
                return new LzipCompressArchive(new LzipArchive(fileName));

            // ".zip", ".7z", ".7zip", ".rar", ".tar" 
            return null;
        }
    }
}

using Aspose.Zip;
using Aspose.Zip.Bzip2;
using Aspose.Zip.Gzip;
using Aspose.Zip.Lzip;
using Aspose.Zip.Rar;
using Aspose.Zip.SevenZip;
using Aspose.Zip.Tar;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Net.Mime.MediaTypeNames;

namespace GameLauncher
{
    public interface IDecompressArchive : System.IDisposable
    {
        bool ExtractToDirectory(string dirName);
        string[] GetNames();
    }
    public interface ICompressArchive : System.IDisposable
    {
        bool CompressFile(string filePath);
    }
    public interface IArchive : IDecompressArchive, ICompressArchive
    {
    }
    public class ZipCompressArchive : IArchive
    {
        private ZipArchive archive;
        public string archivePath { get; private set; }
        public ZipCompressArchive(ZipArchive archive)
        {
            this.archive = archive;
        }
        public ZipCompressArchive(string archivePath)
        {
            this.archivePath = archivePath;
            archive = ZipFile.Open(archivePath, ZipArchiveMode.Create);
        }
        public bool ExtractToDirectory(string dirName)
        {
            try
            {
                archive.ExtractToDirectory(dirName);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public string[] GetNames()
        {
            string[] names = new string[archive.Entries.Count];
            for (int i = 0; i < archive.Entries.Count; i++)
            {
                names[i] = archive.Entries[i].FullName;
            }
            return names;
        }
        public bool CompressFile(string filePath)
        {
            archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
            return File.Exists(archivePath);
        }
        public void Dispose() => archive.Dispose();
    }
    public class RarCompressArchive : IArchive
    {// There's no free RAR library to create RAR files. The CompressFile function requires WinRar be installed in the below path.
        public const string RarExec = @"C:\Program Files\WinRAR\Rar.exe";
        public static bool IsWinRarInstalled()=> File.Exists(RarExec);
        private RarArchive archive;
        public string archivePath { get; private set; }
        public RarCompressArchive(RarArchive archive)
        {
            this.archive = archive;
        }
        public RarCompressArchive(string archivePath)
        {
            this.archivePath = archivePath;
            archive = null;
        }
        public bool ExtractToDirectory(string dirName)
        {
            try
            {
                archive.ExtractToDirectory(dirName);
            }
            catch
            {
                return false;
            }
            return true;
        }
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
        public bool CompressFile(string filePath)
        {
            if (!IsWinRarInstalled())
                return false;
            try
            {
                Process process = Process.Start(new ProcessStartInfo($"\"{RarExec}\"", $"a -m5 \"{archivePath}\" \"{filePath}\"")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                });
                process.WaitForExit();
            }
            catch
            {
                return false;
            }
            return File.Exists(archivePath);
        }
        public void Dispose()
        {
            if (archive != null) 
                archive.Dispose();
        }
    }
    public class SevenZipCompressArchive : IArchive
    {
        private SevenZipArchive archive;
        public string archivePath { get; private set; }
        public SevenZipCompressArchive(SevenZipArchive archive)
        {
            this.archive = archive;
        }
        public SevenZipCompressArchive(string archivePath)
        {
            this.archivePath = archivePath;
            archive = new SevenZipArchive();
        }
        public bool ExtractToDirectory(string dirName)
        {
            try
            {
                archive.ExtractToDirectory(dirName);
            }
            catch
            {
                return false;
            }
            return true;
        }
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
        public bool CompressFile(string filePath)
        {
            archive.CreateEntry(Path.GetFileName(filePath),filePath);
            archive.Save(archivePath);
            return File.Exists(archivePath);
        }
        public void Dispose() => archive.Dispose();
    }
    public class TarCompressArchive : IArchive
    {
        private TarArchive archive;
        public string archivePath { get; private set; }
        public TarCompressArchive(TarArchive archive)
        {
            this.archive = archive;
        }
        public TarCompressArchive(string archivePath)
        {
            this.archivePath = archivePath;
            archive = new TarArchive();
        }
        public bool ExtractToDirectory(string dirName)
        {
            try
            {
                archive.ExtractToDirectory(dirName);
            }
            catch
            {
                return false;
            }
            return true;
        }
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
        public bool CompressFile(string filePath)
        {
            archive.CreateEntry(Path.GetFileName(filePath), filePath);
            archive.Save(archivePath);
            return File.Exists(archivePath);
        }
        public void Dispose() => archive.Dispose();
    }
    public class CompressArchiveClass
    {
        protected string dirExtractedPath;
        public string archivePath { get; protected set; } = "";
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
    public class GzipCompressArchive : CompressArchiveClass, IArchive
    {
        private GzipArchive archive;
        public GzipCompressArchive(GzipArchive archive)
        {
            this.archive = archive;
        }
        public GzipCompressArchive(string archivePath)
        {
            this.archivePath = archivePath;
            archive = new GzipArchive();
        }
        public bool ExtractToDirectory(string dirName)
        {
            dirExtractedPath = dirName;
            try
            {
                archive.ExtractToDirectory(dirName);
            }
            catch 
            {
                return false;
            }
            return true;
        }
        public string[] GetNames() => GetNamesFromDir();
        public bool CompressFile(string filePath)
        {
            archive.SetSource(filePath);
            archive.Save(archivePath);
            return File.Exists(archivePath);
        }
        public void Dispose() => archive.Dispose();
    }
    public class Bzip2CompressArchive : CompressArchiveClass, IArchive
    {
        private Bzip2Archive archive;
        public Bzip2CompressArchive(Bzip2Archive archive)
        {
            this.archive = archive;
        }
        public Bzip2CompressArchive(string archivePath)
        {
            this.archivePath = archivePath;
            archive = new Bzip2Archive();
        }
        public bool ExtractToDirectory(string dirName)
        {
            try
            {
                archive.ExtractToDirectory(dirName);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public string[] GetNames() => GetNamesFromDir();
        public bool CompressFile(string filePath)
        {
            archive.SetSource(filePath);
            archive.Save(archivePath);
            return File.Exists(archivePath);
        }
        public void Dispose() => archive.Dispose();
    }
    public class LzipCompressArchive : CompressArchiveClass, IArchive
    {
        private LzipArchive archive;
        public LzipCompressArchive(LzipArchive archive)
        {
            this.archive = archive;
        }
        public LzipCompressArchive(string archivePath)
        {
            this.archivePath = archivePath;
            archive = new LzipArchive();
        }
        public bool ExtractToDirectory(string dirName)
        {
            try
            {
                archive.ExtractToDirectory(dirName);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public string[] GetNames() => GetNamesFromDir();
        public bool CompressFile(string filePath)
        {
            archive.SetSource(filePath);
            archive.Save(archivePath);
            return File.Exists(archivePath);
        }
        public void Dispose() => archive.Dispose();
    }
    public class ZipAltCompressArchive : IArchive
    {
        private Archive archive;
        public string archivePath { get; protected set; } = "";
        public ZipAltCompressArchive(Archive archive)
        {
            this.archive = archive;
        }
        public ZipAltCompressArchive(string archivePath)
        {
            this.archivePath = archivePath;
            archive = new Archive();
        }
        public bool ExtractToDirectory(string dirName)
        {
            try
            {
                archive.ExtractToDirectory(dirName);
            }
            catch
            {
                return false;
            }
            return true;
        }
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
        public bool CompressFile(string filePath)
        {
            archive.CreateEntry(Path.GetFileName(filePath), filePath);
            archive.Save(archivePath);
            return File.Exists(archivePath);
        }
        public void Dispose() => archive.Dispose();
    }
    public class CompressArchive
    {
        public static IDecompressArchive Open(string fileName, bool doAltZip = false)
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
        public static bool CompressFile(string archivePath, string filePath, bool doAltZip = true)
        {
            string ext = Path.GetExtension(archivePath).ToLower();
            if (ext.Equals(".zip") && doAltZip)
            {
                using (ICompressArchive archive = new ZipAltCompressArchive(archivePath))
                {
                    return archive.CompressFile(filePath);
                }
            }
            else if (ext.Equals(".zip"))
            {
                using (ICompressArchive archive = new ZipCompressArchive(archivePath))// This doesn't work. It creates bad zip files.
                {
                    return archive.CompressFile(filePath);
                }
            }
            else if (ext.Equals(".7z") || ext.Equals(".7zip"))
            {
                using (ICompressArchive archive = new SevenZipCompressArchive(archivePath))
                {
                    return archive.CompressFile(filePath);
                }
            }
            else if (ext.Equals(".rar"))
            {
                using (ICompressArchive archive = new RarCompressArchive(archivePath))
                {
                    return archive.CompressFile(filePath);
                }
            }
            else if (ext.Equals(".tar"))
            {
                using (ICompressArchive archive = new TarCompressArchive(archivePath))
                {
                    return archive.CompressFile(filePath);
                }
            }
            else if (ext.Equals(".gz") || ext.Equals(".gzip"))
            {
                using (ICompressArchive archive = new GzipCompressArchive(archivePath))
                {
                    return archive.CompressFile(filePath);
                }
            }
            else if (ext.Equals(".bz2") || ext.Equals(".bzip") || ext.Equals(".bzip2"))
            {
                using (ICompressArchive archive = new Bzip2CompressArchive(archivePath))
                {
                    return archive.CompressFile(filePath);
                }
            }
            else if (ext.Equals(".lz") || ext.Equals(".lzip"))
            {
                using (ICompressArchive archive = new LzipCompressArchive(archivePath))
                {
                    return archive.CompressFile(filePath);
                }
            }

            return false;
        }
        public static IArchive CreateArchive(string archivePath, bool throwExceptionForUnsupportedType = true, bool doAltZip = false)
        {
            string ext = Path.GetExtension(archivePath).ToLower();
            if (ext.Equals(".zip") && doAltZip)
                return new ZipAltCompressArchive(archivePath);
            else if (ext.Equals(".zip"))
                return new ZipCompressArchive(archivePath);
            else if (ext.Equals(".7z") || ext.Equals(".7zip"))
                return new SevenZipCompressArchive(archivePath);
            else if (ext.Equals(".rar"))
                return new RarCompressArchive(archivePath);
            else if (ext.Equals(".tar"))
                return new TarCompressArchive(archivePath);
            else if (ext.Equals(".gz") || ext.Equals(".gzip"))
                return new GzipCompressArchive(archivePath);
            else if (ext.Equals(".bz2") || ext.Equals(".bzip") || ext.Equals(".bzip2"))
                return new Bzip2CompressArchive(archivePath);
            else if (ext.Equals(".lz") || ext.Equals(".lzip"))
                return new LzipCompressArchive(archivePath);
            return null;
        }
    }
}

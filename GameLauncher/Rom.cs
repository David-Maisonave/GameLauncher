using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher
{
    public class Rom : IComparable<Rom>, IEquatable<Rom>
    {
        public const string M3 = "English, Spanish, French";
        public const string M4 = "English, Spanish, French, German";
        public const string M5 = "English, Spanish, French, German, Italian";
        public const string M6 = "English, Spanish, French, German, Italian, Japanese";

        public string Title;
        public string NameSimplified;
        public string NameOrg;
        public string Compressed;
        public int System;
        public string FilePath;
        public int PreferredEmulatorID;
        public string ImagePath;
        public int QtyPlayers;
        public string Region;
        public string Developer;
        public long RomSize;
        public string Genre;
        public string NotesCore;
        public string NotesUser;
        public string FileFormat;
        public string ReleaseDate;
        public string Status;
        public string Version;
        public string Description;
        public string Language;
        public int Year;
        public string Rating;
        public string Checksum;
        public string CompressChecksum;
        public Rom(string nameTruncated, int systemID, string path, string nameOrg, string title, string compressed, int preferredEmulatorID = 0, string imagePath = null, int qtyPlayers = 0, string region = null, string developer = null, long romSize = 0, string genre = null, string notesCore = null, string notesUser = null, string fileFormat = null, string releaseDate = null, string status = null, string version = null, string description = null, string language = null, int year = 0, string rating = null, string checksum = null, string compressChecksum = null)
        {
            NameSimplified = nameTruncated;
            System = systemID;
            FilePath = path;
            NameOrg = nameOrg;
            Title = title;
            Compressed = compressed;
            PreferredEmulatorID = preferredEmulatorID;
            ImagePath = imagePath;
            QtyPlayers = qtyPlayers;
            Region = region;
            Developer = developer;
            RomSize = romSize;
            Genre = genre;
            NotesCore = notesCore;
            NotesUser = notesUser;
            FileFormat = fileFormat;
            ReleaseDate = releaseDate;
            Status = status;
            Version = version;
            Description = description;
            Language = language;
            Checksum = checksum;
            CompressChecksum = compressChecksum;
            Year = year;
            Rating = rating;
        }
        public override bool Equals(object obj)
        {
            Rom rom = (Rom)obj;
            return FilePath == rom.FilePath;
        }
        public bool Equals(Rom rom)
        {
            return rom != null && FilePath == rom.FilePath;
        }
        public int CompareTo(Rom rom)
        {
            return this.FilePath.CompareTo(rom.FilePath);
        }

        public override string ToString()
        {
            return FilePath;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();  // HashCode.Combine(FilePath);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #region Misc supporting classes
    public class SortRomByFilePathLen : IComparer<Rom>
    {
        public int Compare(Rom x, Rom y)
        {
            int xi = x.FilePath.Length;
            int yi = y.FilePath.Length;
            return xi == yi ? x.FilePath.CompareTo(y.FilePath) : xi.CompareTo(yi);
        }
    }
    public class SortRomByFilePathLenRev : IComparer<Rom>
    {
        public int Compare(Rom x, Rom y)
        {
            int xi = x.FilePath.Length;
            int yi = y.FilePath.Length;
            return xi == yi ? x.FilePath.CompareTo(y.FilePath) : yi.CompareTo(xi);
        }
    }
    public class SortRomByFileSizeLen : IComparer<Rom>
    {
        public int Compare(Rom x, Rom y)
        {
            long xi = x.RomSize;
            long yi = y.RomSize;
            if (xi == yi)
            {
                xi = x.FilePath.Length;
                yi = y.FilePath.Length;
            }
            return xi == yi ? x.FilePath.CompareTo(y.FilePath) : xi.CompareTo(yi);
        }
    }
    public class SortRomByFileSizeLenRev : IComparer<Rom>
    {
        public int Compare(Rom x, Rom y)
        {
            long xi = x.RomSize;
            long yi = y.RomSize;
            if (xi == yi)
            {
                xi = x.FilePath.Length;
                yi = y.FilePath.Length;
            }
            return xi == yi ? x.FilePath.CompareTo(y.FilePath) : yi.CompareTo(xi);
        }
    }
    public class SortRomByFileVersion : IComparer<Rom>
    {
        public int Compare(Rom x, Rom y)
        {
            if (!x.Version.Equals(y.Version))
                return y.Version.CompareTo(x.Version);
            long xi = x.RomSize;
            long yi = y.RomSize;
            if (xi == yi)
            {
                xi = x.FilePath.Length;
                yi = y.FilePath.Length;
            }
            return xi == yi ? x.FilePath.CompareTo(y.FilePath) : yi.CompareTo(xi);
        }
    }
    #endregion
}

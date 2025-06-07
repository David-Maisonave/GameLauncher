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
        public string Publisher;
        public string WikipediaURL;
        public float StarRating;
        public int StarRatingVoteCount;
        public bool Favorite;
        public bool Disable;
        public Rom(string NameSimplified, int System, string FilePath, string NameOrg, string Title, string Compressed, int PreferredEmulatorID = 0, 
            string ImagePath = null, int QtyPlayers = 0, string Region = null, string Developer = null, long RomSize = 0, string Genre = null, 
            string NotesCore = null, string NotesUser = null, string FileFormat = null, string ReleaseDate = null, string Status = null, 
            string Version = null, string Description = null, string Language = null, int Year = 0, string Rating = null, string Checksum = null,
            string CompressChecksum = null, string Publisher = null, string WikipediaURL = null, float StarRating = 0, int StarRatingVoteCount = 0, 
            bool Favorite = false, bool Disable = false)
        {
            this.NameSimplified = NameSimplified;
            this.System = System;
            this.FilePath = FilePath;
            this.NameOrg = NameOrg;
            this.Title = Title;
            this.Compressed = Compressed;
            this.PreferredEmulatorID = PreferredEmulatorID;
            this.ImagePath = ImagePath;
            this.QtyPlayers = QtyPlayers;
            this.Region = Region;
            this.Developer = Developer;
            this.RomSize = RomSize;
            this.Genre = Genre;
            this.NotesCore = NotesCore;
            this.NotesUser = NotesUser;
            this.FileFormat = FileFormat;
            this.ReleaseDate = ReleaseDate;
            this.Status = Status;
            this.Version = Version;
            this.Description = Description;
            this.Language = Language;
            this.Checksum = Checksum;
            this.CompressChecksum = CompressChecksum;
            this.Year = Year;
            this.Rating = Rating;
            this.Publisher = Publisher;
            this.WikipediaURL = WikipediaURL;
            this.StarRating = StarRating;
            this.StarRatingVoteCount = StarRatingVoteCount;
            this.Favorite = Favorite;
            this.Disable = Disable;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher
{
    public class GameDetails
    {
        public string System;
        public string Title;
        public string NameSimplified;
        public string Compressed;
        public int QtyPlayers;
        public int Year;
        public string Status;
        public string ImageFileName;
        public string Region;
        public string Developer;
        public string ReleaseDate;
        public string Genre;
        public string NotesCore;
        public string NotesUser;
        public string FileFormat;
        public string Version;
        public string Description;
        public string Language;
        public string Rating;
        public string Publisher;
        public float StarRating;
        public int StarRatingVoteCount;
        public string FileName;
        public string WikipediaURL;
        public GameDetails(string System, string Title, string NameSimplified, string Compressed, int QtyPlayers,
            string Developer = "", string ReleaseDate = "", int Year = 0, string Genre = "",
            string Status = "", string ImageFileName = "", string Region = "",
            string NotesCore = "", string NotesUser = "", string FileFormat = "",
            string Version = "", string Description = "", string Language = "", string Rating = "", string Publisher = "", string FileName = "", float StarRating = 0, int StarRatingVoteCount = 0, string WikipediaURL = null)
        {
            this.System = System;
            this.Title = Title;
            this.NameSimplified = NameSimplified;
            this.Compressed = Compressed;
            this.QtyPlayers = QtyPlayers;
            this.Year = Year;
            this.Status = Status;
            this.ImageFileName = ImageFileName;
            this.Region = Region;
            this.Developer = Developer;
            this.ReleaseDate = ReleaseDate;
            this.Genre = Genre;
            this.NotesCore = NotesCore;
            this.NotesUser = NotesUser;
            this.FileFormat = FileFormat;
            this.Version = Version;
            this.Description = Description;
            this.Language = Language;
            this.Rating = Rating;
            this.Publisher = Publisher;
            this.FileName = FileName;
            this.StarRating = StarRating;
            this.StarRatingVoteCount = StarRatingVoteCount;
            this.WikipediaURL = WikipediaURL;
        }
        public override string ToString()
        {
            return $"{System}-{Title}";
        }
    }
}

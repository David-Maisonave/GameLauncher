using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher
{
    public class Roms_UserChanges
    {
        public string Title;
        public string FilePath;
        public int PreferredEmulatorID;
        public string ImagePath;
        public int QtyPlayers;
        public string Status;
        public string Region;
        public string Developer;
        public string ReleaseDate;
        public string Genre;
        public string NotesCore;
        public string NotesUser;
        public string Version;
        public string Description;
        public string Language;
        public int Year;
        public string Rating;
        public string Publisher;
        public string WikipediaURL;
        public int PlayCount;
        public bool Favorite;
        public bool Disable;
        public bool Hide;
        public Roms_UserChanges(string FilePath, string Title, int PreferredEmulatorID, string ImagePath, int QtyPlayers, string Status, string Region,
            string Developer, string ReleaseDate, string Genre, string NotesCore, string NotesUser, string Version, string Description, string Language, int Year, string Rating, string Publisher, string WikipediaURL,
            int PlayCount, bool Favorite, bool Disable, bool Hide)
        {
            this.FilePath = FilePath;
            this.Title = Title;
            this.PreferredEmulatorID = PreferredEmulatorID;
            this.ImagePath = ImagePath;
            this.QtyPlayers = QtyPlayers;
            this.Status = Status;
            this.Region = Region;
            this.Developer = Developer;
            this.Genre = Genre;
            this.NotesCore = NotesCore;
            this.NotesUser = NotesUser;
            this.ReleaseDate = ReleaseDate;
            this.Version = Version;
            this.Description = Description;
            this.Language = Language;
            this.Year = Year;
            this.Rating = Rating;
            this.Publisher = Publisher;
            this.WikipediaURL = WikipediaURL;
            this.PlayCount = PlayCount;
            this.Favorite = Favorite;
            this.Disable = Disable;
            this.Hide = Hide;
        }
    }
}

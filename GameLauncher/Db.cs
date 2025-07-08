using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher
{
    public class Db
    {
        private Form_Main form_Main = null;
        public Db(Form_Main form_Main) 
        { 
            this.form_Main = form_Main;
        }
        public bool Get(ref Rom item, string key, string key2 = "")
        {
            return false;
        }
        public bool Get(ref GameImage item, string key)
        {
            return false;
        }
        public bool Get(ref Roms_UserChanges item, string key)
        {
            return false;
        }
        public bool Get(ref Mru item, string key)
        {
            return false;
        }
        public bool Get(ref ImageToRomNeverMatch item, string key)
        {
            return false;
        }
        public bool Get(ref GameDetails item, string key)
        {
            return false;
        }
        public bool Get(ref AlternateNames item, string key)
        {
            return false;
        }
        public bool Get(ref GameSystems item, string key)
        {
            return false;
        }
    }
    #region DB associated classes
    public class GameImage
    {
        public string Title;
        public string NameSimplified;
        public string NameOrg;
        public string Compressed;
        public string FilePath;
        public string Checksum;
        public GameImage(string Title, string NameSimplified, string FilePath, string NameOrg, string Compressed, string Checksum)
        {
            this.Title = Title;
            this.NameSimplified = NameSimplified;
            this.FilePath = FilePath;
            this.NameOrg = NameOrg;
            this.Compressed = Compressed;
            this.Checksum = Checksum;
        }
    }
    public class AlternateNames
    {
        public string DefaultName;
        public string Alternate;
        public string Title;
        public string NameSimplified;
        public string Compressed;
        public AlternateNames(string DefaultName, string Alternate, string Title, string NameSimplified, string Compressed)
        {
            this.DefaultName = DefaultName;
            this.Alternate = Alternate;
            this.Title = Title;
            this.NameSimplified = NameSimplified;
            this.Compressed = Compressed;
        }
    }
    public class Mru
    {
        public string FilePath;
        public string DateLastUsed;
        public Mru(string FilePath, string DateLastUsed)
        {
            this.FilePath = FilePath;
            this.DateLastUsed = DateLastUsed;
        }
    }
    public class EmulatorAttributes
    {
        public string EmulatorExecutable;
        public bool DecompressFile;
        public bool NotSupported;
        public string PreferredExtension;
        public EmulatorAttributes(string EmulatorExecutable, bool DecompressFile = false, bool NotSupported = false, string PreferredExtension = "")
        {
            this.EmulatorExecutable = EmulatorExecutable;
            this.DecompressFile = DecompressFile;
            this.NotSupported = NotSupported;
            this.PreferredExtension = PreferredExtension;
        }
    }
    #endregion /////////////////////////////////////////////////////////////////////////////////
}
// 862-208-6067
//         6668
// 856-212-0119 
// 856-212-0014


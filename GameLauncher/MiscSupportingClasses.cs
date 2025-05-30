using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLauncher
{
    #region Generic classes
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
    public class CursorWait : IDisposable
    {
        public CursorWait(bool appStarting = false, bool applicationCursor = false)
        {
            Cursor.Current = appStarting ? Cursors.AppStarting : Cursors.WaitCursor;
            if (applicationCursor)
                Application.UseWaitCursor = true;
        }

        public void Dispose()
        {
            Cursor.Current = Cursors.Default;
            Application.UseWaitCursor = false;
        }
    }
    #endregion /////////////////////////////////////////////////////////////////////////////////
    #region Data packets supporting classes
    public class SystemScanTaskData
    {
        public string systemName;
        public string emulatorDir;
        public EmulatorExecutables emulatorExecutables;
    }
    public class PreviousCollectedImages
    {
        public ImageList list1;
        public ImageList list2;
    }
    public class PreviousCollectedData : PreviousCollectedImages
    {
        public List<Rom> roms;
    }
    public enum DeleteDuplicateBy
    {
        None = 0,
        DuplicateChecksum,
        DuplicateTitleInAnySystem,
        DuplicateTitleInSameSystem
    }
    public class InitializeRomsInDatabaseForSystem_Arg
    {
        public string emulatorDir;
        public EmulatorExecutables emulatorExecutables;
        public bool isMainThread;
        public bool scanImageDir;
        public bool hideGroup;
    }
    #endregion /////////////////////////////////////////////////////////////////////////////////
    #region DB associated classes
    public class EmulatorExecutables
    {
        public const int MAX_EMULATOR_EXECUTABLES = 10;
        public string[] EmulatorPaths = new string[MAX_EMULATOR_EXECUTABLES] { "", "", "", "", "", "", "", "", "", "" };
    }
    public class GameSystem : EmulatorExecutables
    {
        public string Name;
        public string RomDirPath;
        public string ImageDirPath;
        public int ID;
        public GameSystem(string name, string romPath, string imgPath, EmulatorExecutables paths, int id)
        {
            Name = name;
            RomDirPath = romPath;
            ImageDirPath = imgPath;
            EmulatorPaths = paths.EmulatorPaths;
            ID = id;
        }
    }
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
    #endregion /////////////////////////////////////////////////////////////////////////////////
}

using Aspose.Zip;
using Aspose.Zip.Bzip2;
using Aspose.Zip.Gzip;
using Aspose.Zip.Lzip;
using Aspose.Zip.Rar;
using Aspose.Zip.SevenZip;
using Aspose.Zip.Tar;

using Microsoft.Data.Sqlite;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell.Interop;
using Microsoft.WindowsAPICodePack.Taskbar;

using SharpDX.DirectInput;

using Shell32;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Xml.Linq;

using static GameLauncher.Form_Main;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.ListViewItem;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace GameLauncher
{
    public partial class Form_Main : Form
    {
        #region const or readonly variables
        public const string SQL_CREATETABLES =
                "CREATE TABLE \"GameSystems\" (\r\n\t\"Name\"\tTEXT NOT NULL CHECK(length(\"Name\") > 0) UNIQUE,\r\n\t\"ID\"\tINTEGER NOT NULL UNIQUE,\r\n\t\"RomDirPath\"\tTEXT NOT NULL,\r\n\t\"ImageDirPath\"\tTEXT NOT NULL,\r\n\t\"PlayCount\"\tINTEGER DEFAULT 0,\r\n\t\"Initiated\"\tINTEGER DEFAULT 0,\r\n\t\"Hide\"\tBoolean DEFAULT 0,\r\n\t\"EmulatorPath1\"\tTEXT NOT NULL,\r\n\t\"EmulatorPath2\"\tTEXT,\r\n\t\"EmulatorPath3\"\tTEXT,\r\n\t\"EmulatorPath4\"\tTEXT,\r\n\t\"EmulatorPath5\"\tTEXT,\r\n\t\"EmulatorPath6\"\tTEXT,\r\n\t\"EmulatorPath7\"\tTEXT,\r\n\t\"EmulatorPath8\"\tTEXT,\r\n\t\"EmulatorPath9\"\tTEXT,\r\n\t\"EmulatorPath10\"\tTEXT,\r\n\t\"DecompressFile1\"\tBoolean DEFAULT 0,\r\n\t\"DecompressFile2\"\tBoolean DEFAULT 0,\r\n\t\"DecompressFile3\"\tBoolean DEFAULT 0,\r\n\t\"DecompressFile4\"\tBoolean DEFAULT 0,\r\n\t\"DecompressFile5\"\tBoolean DEFAULT 0,\r\n\t\"DecompressFile6\"\tBoolean DEFAULT 0,\r\n\t\"DecompressFile7\"\tBoolean DEFAULT 0,\r\n\t\"DecompressFile8\"\tBoolean DEFAULT 0,\r\n\t\"DecompressFile9\"\tBoolean DEFAULT 0,\r\n\t\"DecompressFile10\"\tBoolean DEFAULT 0,\r\n\t\"NotSupported1\"\tBoolean DEFAULT 0,\r\n\t\"NotSupported2\"\tBoolean DEFAULT 0,\r\n\t\"NotSupported3\"\tBoolean DEFAULT 0,\r\n\t\"NotSupported4\"\tBoolean DEFAULT 0,\r\n\t\"NotSupported5\"\tBoolean DEFAULT 0,\r\n\t\"NotSupported6\"\tBoolean DEFAULT 0,\r\n\t\"NotSupported7\"\tBoolean DEFAULT 0,\r\n\t\"NotSupported8\"\tBoolean DEFAULT 0,\r\n\t\"NotSupported9\"\tBoolean DEFAULT 0,\r\n\t\"NotSupported10\"\tBoolean DEFAULT 0,\r\n\t\"PreferredExtension1\"\tTEXT,\r\n\t\"PreferredExtension2\"\tTEXT,\r\n\t\"PreferredExtension3\"\tTEXT,\r\n\t\"PreferredExtension4\"\tTEXT,\r\n\t\"PreferredExtension5\"\tTEXT,\r\n\t\"PreferredExtension6\"\tTEXT,\r\n\t\"PreferredExtension7\"\tTEXT,\r\n\t\"PreferredExtension8\"\tTEXT,\r\n\t\"PreferredExtension9\"\tTEXT COLLATE NOCASE,\r\n\t\"PreferredExtension10\"\tTEXT,\r\n\tPRIMARY KEY(\"ID\" AUTOINCREMENT)\r\n);\r\n" +
                "CREATE TABLE \"Roms\" (\r\n\t\"FilePath\"\tTEXT NOT NULL UNIQUE,\r\n\t\"Title\"\tTEXT NOT NULL,\r\n\t\"NameSimplified\"\tTEXT NOT NULL,\r\n\t\"NameOrg\"\tTEXT NOT NULL,\r\n\t\"Compressed\"\tTEXT NOT NULL,\r\n\t\"System\"\tINTEGER NOT NULL,\r\n\t\"PreferredEmulator\"\tINTEGER NOT NULL DEFAULT 0,\r\n\t\"ImagePath\"\tTEXT,\r\n\t\"QtyPlayers\"\tINTEGER NOT NULL DEFAULT 1,\r\n\t\"Status\"\tTEXT,\r\n\t\"Region\"\tTEXT,\r\n\t\"Developer\"\tTEXT,\r\n\t\"ReleaseDate\"\tTEXT,\r\n\t\"RomSize\"\tINTEGER,\r\n\t\"Genre\"\tTEXT,\r\n\t\"NotesCore\"\tTEXT,\r\n\t\"NotesUser\"\tTEXT,\r\n\t\"FileFormat\"\tTEXT,\r\n\t\"Version\"\tTEXT,\r\n\t\"Description\"\tTEXT,\r\n\t\"Language\"\tTEXT,\r\n\t\"Year\"\tINTEGER,\r\n\t\"Rating\"\tTEXT,\r\n\t\"Checksum\"\tTEXT,\r\n\t\"CompressChecksum\"\tTEXT,\r\n\t\"Publisher\"\tTEXT,\r\n\t\"WikipediaURL\"\tTEXT,\r\n\t\"StarRating\"\tREAL,\r\n\t\"StarRatingVoteCount\"\tINTEGER,\r\n\t\"PlayCount\"\tINTEGER DEFAULT 0,\r\n\t\"Favorite\"\tBoolean DEFAULT 0,\r\n\t\"Disable\"\tBoolean DEFAULT 0,\r\n\t\"Hide\"\tBoolean DEFAULT 0,\r\n\tPRIMARY KEY(\"FilePath\",\"Title\")\r\n);\r\n" +
                "CREATE TABLE \"Images\" (\r\n\t\"Title\"\tTEXT NOT NULL,\r\n\t\"NameSimplified\"\tTEXT NOT NULL,\r\n\t\"NameOrg\"\tTEXT NOT NULL,\r\n\t\"Compressed\"\tTEXT NOT NULL,\r\n\t\"FilePath\"\tTEXT NOT NULL UNIQUE,\r\n\t\"Checksum\"\tTEXT\r\n);\r\n" +
                "CREATE TABLE \"PersistenceVariables\" (\r\n\t\"Name\"\tTEXT NOT NULL UNIQUE,\r\n\t\"Value\"\tTEXT,\r\n\t\"ValueInt\"\tINTEGER,\r\n\tPRIMARY KEY(\"Name\")\r\n);\r\n" +
                "CREATE TABLE \"Roms_UserChanges\" (\r\n\t\"Title\"\tTEXT NOT NULL,\r\n\t\"FilePath\"\tTEXT NOT NULL UNIQUE,\r\n\t\"PreferredEmulator\"\tINTEGER NOT NULL DEFAULT -1,\r\n\t\"ImagePath\"\tTEXT,\r\n\t\"QtyPlayers\"\tINTEGER NOT NULL DEFAULT 0,\r\n\t\"Status\"\tTEXT,\r\n\t\"Region\"\tTEXT,\r\n\t\"Developer\"\tTEXT,\r\n\t\"ReleaseDate\"\tTEXT,\r\n\t\"Genre\"\tTEXT,\r\n\t\"NotesCore\"\tTEXT,\r\n\t\"NotesUser\"\tTEXT,\r\n\t\"Version\"\tTEXT,\r\n\t\"Description\"\tTEXT,\r\n\t\"Language\"\tTEXT,\r\n\t\"Year\"\tINTEGER DEFAULT 0,\r\n\t\"Rating\"\tTEXT,\r\n\t\"Publisher\"\tTEXT,\r\n\t\"WikipediaURL\"\tTEXT,\r\n\t\"PlayCount\"\tINTEGER DEFAULT 0,\r\n\t\"Favorite\"\tBoolean DEFAULT 0,\r\n\t\"Disable\"\tBoolean DEFAULT 0,\r\n\t\"Hide\"\tBoolean DEFAULT 0,\r\n\tPRIMARY KEY(\"FilePath\")\r\n);\r\n" +
                "CREATE TABLE \"InitialDefaultEmulatorAttributes\" (\r\n\t\"EmulatorExecutable\"\tTEXT NOT NULL UNIQUE,\r\n\t\"DecompressFile\"\tNUMERIC DEFAULT 0,\r\n\t\"NotSupported\"\tINTEGER DEFAULT 0,\r\n\t\"PreferredExtension\"\tTEXT,\r\n\tPRIMARY KEY(\"EmulatorExecutable\")\r\n);\r\n" +
                "CREATE TABLE \"ErrorLog\" (\r\n\t\"Process\"\tTEXT NOT NULL,\r\n\t\"Message\"\tTEXT NOT NULL,\r\n\t\"Code\"\tINTEGER NOT NULL,\r\n\t\"Circumstances\"\tTEXT,\r\n\t\"Stack\"\tTEXT\r\n);\r\n" +
                "CREATE TABLE \"FilterAutoCompleteCustomSource\" (\r\n\t\"Source\"\tTEXT NOT NULL UNIQUE,\r\n\tPRIMARY KEY(\"Source\")\r\n);\r\n" +
                "CREATE TABLE \"MRU\" (\r\n\t\"FilePath\"\tTEXT NOT NULL UNIQUE,\r\n\t\"DateLastUsed\"\tTEXT NOT NULL,\r\n\tPRIMARY KEY(\"FilePath\")\r\n);\r\n" +
                "CREATE TABLE \"ImageToRomMatchAlwaysNo\" (\r\n\t\"RomChecksum\"\tTEXT NOT NULL CHECK(length(\"RomChecksum\") > 22),\r\n\t\"ImageChecksum\"\tTEXT NOT NULL CHECK(length(\"ImageChecksum\") >22),\r\n\tPRIMARY KEY(\"RomChecksum\",\"ImageChecksum\")\r\n);\r\n" +
                "\r\n";
        public const string SQL_DB_INIT =
                "INSERT INTO InitialDefaultEmulatorAttributes (EmulatorExecutable, DecompressFile, NotSupported, PreferredExtension) VALUES (\"duckstation-qt-x64-ReleaseLTCG.exe\", 1, 0, \".cue\");\r\n" +
                "INSERT INTO InitialDefaultEmulatorAttributes (EmulatorExecutable, DecompressFile, NotSupported, PreferredExtension) VALUES (\"duckstation\", 1, 0, \".cue\");\r\n" +
                "INSERT INTO InitialDefaultEmulatorAttributes (EmulatorExecutable, DecompressFile, NotSupported, PreferredExtension) VALUES (\"NeoRAGE\",0, 1, \"\");\r\n" +
                "\r\n";
        public const string SQL_GAMEDETAILS_DB_CREATETABLES =
                "CREATE TABLE \"GameDetails\" (\r\n\t\"System\"\tTEXT NOT NULL,\r\n\t\"Title\"\tTEXT NOT NULL,\r\n\t\"NameSimplified\"\tTEXT NOT NULL,\r\n\t\"Compressed\"\tTEXT NOT NULL,\r\n\t\"QtyPlayers\"\tNUMERIC NOT NULL DEFAULT 0,\r\n\t\"PlayMode\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"Year\"\tINTEGER NOT NULL DEFAULT 0,\r\n\t\"Status\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"ImageFileName\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"Region\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"Developer\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"ReleaseDate\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"Genre\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"NotesCore\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"NotesUser\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"FileFormat\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"Version\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"Description\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"Language\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"Rating\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"Publisher\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"WikipediaURL\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"StarRating\"\tREAL NOT NULL DEFAULT 0,\r\n\t\"StarRatingVoteCount\"\tINTEGER NOT NULL DEFAULT 0,\r\n\t\"FileName\"\tTEXT NOT NULL DEFAULT \"\",\r\n\t\"ID\"\tTEXT NOT NULL DEFAULT \"\",\r\n\tPRIMARY KEY(\"System\",\"Title\")\r\n);\r\n" +
                "\r\n";
        public const string SQL_ALTERNATENAMES_DB_CREATETABLES =
                "CREATE TABLE \"AlternateNames\" (\r\n\t\"DefaultName\"\tTEXT,\r\n\t\"Alternate\"\tTEXT,\r\n\t\"Title\"\tTEXT,\r\n\t\"NameSimplified\"\tTEXT,\r\n\t\"Compressed\"\tTEXT,\r\n\tPRIMARY KEY(\"DefaultName\",\"Alternate\")\r\n);\r\n" +
                "\r\n";
        public static readonly string[] SUPPORTED_COMPRESSION_FILE = { ".zip", ".7z", ".7zip", ".rar", ".tar", ".gz", ".gzip", ".bz2", ".bzip", ".bzip2", ".lz", ".lzip" };
        public readonly string[] SUPPORTED_IMAGE_FILES = { "*.png", "*.bmp", "*.jpg", "*.jpeg", "*.tif" }; // These are the supported types according to following link: https://learn.microsoft.com/en-us/dotnet/api/system.drawing.image.fromfile?view=windowsdesktop-9.0&redirectedfrom=MSDN#overloads
        public readonly string[] SUPPORTED_CONVERSION_IMAGE_FILES = { "*.png", "*.bmp"};
        private readonly string[] VALID_ROM_TYPES = { "3ds", "app", "bin", "car", "dsi", "gb", "gba", "gbc", "gcm", "gen", "gg", "ids", "iso", "md", "n64", "nes", "nds", "ngc", "ngp", "nsp", "pce", "rom", "sfc", "smc", "smd", "sms", "srl", "v64", "vpk", "wad", "xci", "xiso", "z64" };
        public static readonly string[] COMMON_EMULATOR_PATHS = { @"C:\Emulator", @"C:\GameEmulator", @"C:\RetroGameEmulator", @"C:\RetroEmulator", @"C:\Game", @"C:\Retro", @"C:\RetroGame", @"C:\GameRetro" };
        public const string ROM_EXTS_ALL = "ROM Files|*.3ds;*.app;*.bin;*.car;*.dat;*.dsi;*.gb;*.gba;*.gbc;*.gcm;*.gcz;*.gen;*.gg;*.ids;*.iso;*.lst;*.md;*.n64;*.nds;*.ngc;*.ngp;*.nsp;*.pce;*.rom;*.sfc;*.smc;*.smd;*.sms;*.srl;*.v64;*.vpk;*.wad;*.xci;*.xiso;*.z64|" +
            "Compress Files (*.7z,*.bz2,*.gz,*.rar,*.tar,*.zip)|*.7z;*.7zip;*.bz2;*.gz;*.gzip;*.rar;*.tar;*.tar.bz2;*.tar.gz;*.zip|" +
            "All files (*.*)|*.*";
        public const string ROM_EXTS_COMMON = "ROM Files (*.bin,*.gb,*.gbc,*.gen,*.n64,*.nds,*.smc)|*.3ds;*.app;*.bin;*.car;*.dat;*.dsi;*.gb;*.gba;*.gbc;*.gcm;*.gcz;*.gen;*.gg;*.ids;*.iso;*.lst;*.md;*.n64;*.nds;*.ngc;*.ngp;*.nsp;*.pce;*.rom;*.sfc;*.smc;*.smd;*.sms;*.srl;*.v64;*.vpk;*.wad;*.xci;*.xiso;*.z64|" +
            "Compress Files (*.7z,*.bz2,*.gz,*.rar,*.tar,*.zip)|*.7z;*.7zip;*.bz2;*.gz;*.gzip;*.rar;*.tar;*.tar.bz2;*.tar.gz;*.zip|" +
            "All files (*.*)|*.*";
        public const string ROM_EXTS_DIV_BY_CONSOLE =
            "3DS Files (*.3ds,*.cia)|*.3ds;*.cia|" +
            "Android App Files (*.apk,*.obb)|*.apk;*.obb|" +
            "Apple iPhone Files (*.ipa)|*.ipa|" +
            "Atari Files (*.bin,*.rom,*.car)|*.bin;*.rom;*.car|" +
            "Game Boy Files (*.gb,*.gbc,*.gba,*.srl)|*.gb;*.gbc;*.gba;*.srl|" +
            "Game Cube Files (*.gcm,*.gcz)|*.gcm;*.gcz|" +
            "Game Gear Files (*.gg)|*.gg|" +
            "NAOMI Files (*.bin,*.dat,*.lst)|*.bin;*.dat;*.lst|" +
            "Neo Geo Files (*.ngp,*.ngc)|*.ngp;*.ngc|" +
            "Nintendo 64 Files (*.n64,*.v64,*.z64)|*.n64;*.v64;*.z64|" +
            "Nintendo DS Files (*.nds,*.srl,*.dsi,*.app,*.ids)|*.nds;*.srl;*.dsi;*.app;*.ids|" +
            "Nintendo NES Files (*.nes,*.nez,*.unf,*.unif)|*.nes;*.nez;*.unf;*.unif|" +
            "Nintendo Switch Files (*.nsp,*.xci)|*.nsp;*.xci|" +
            "PC Engine Files (*.pce)|*.pce|" +
            "PlayStation Files (*.vpk)|*.vpk|" +
            "SNES Files (*.smc ,*.sfc)|*.smc ;*.sfc|" +
            "Sega Files (*.gen,*.md ,*.smd,*.sms)|*.gen;*.md ;*.smd;*.sms|" +
            "Virtual Boy Files (*.vb)|*.vb|" +
            "Wii Files (*.wbfs,*.wad)|*.wbfs;*.wad|" +
            "WonderSwan Files (*.ws,*.wsc)|*.ws;*.wsc|" +
            "Xbox Files (*.xiso)|*.xiso|" +
            "Compress Files (*.7z,*.bz2,*.gz,*.rar,*.tar,*.zip)|*.7z;*.7zip;*.bz2;*.gz;*.gzip;*.rar;*.tar;*.tar.bz2;*.tar.gz;*.zip|" +
            "All files (*.*)|*.*";
        public const string DATABASE_FILE_TYPES = "Database File  (*.db,*.db3,*.database,*.sqlite,*.sqlite3,*.sql)|*.db;*.db3;*.database;*.sqlite;*.sqlite3;*.sql|All Files (*.*)|*.*";
        public const int MINIMUM_ROM_SIZE = 10000; // NES has "Demo Boy 2" which is 1948 bytes. Next smallest is "NES PowerPad Test Cart" at 2,598 bytes.
        public const int MINIMUM_ZIP_SIZE = 1000;
        public const int MAXIMUM_PROGRESSBAR = 1000;
        public const long MAX_SECONDS_TO_WAIT_F5 = 5;
        public const long MAX_SECONDS_TO_WAIT_DEFAULT = 2;
        public const string GAMELAUNCHER_LOGGING_FILENAME = "GameLauncher_logging.";
        public const string DEFAULTIMAGEFILENAME = "GameController.png";
        public const string GAMELAUNCHER_DB_NAME = "GameLauncher.db";
        public const string GAMEDETAILS_DB_NAME = "GameDetails.db";
        public const string DATA_SUBPATH = @"data\";
        public const string GAMELAUNCHER_SUBPATH = @"GameLauncher\";
        private const string DEFAULT_ROM_SUBFOLDER_NAME = "roms";
        private const string DEFAULT_IMAGE_SUBFOLDER_NAME = "images";
        private const string ITEM_ANY = "__Any__";
        private const string ITEM_ALL = "_All_";
        private const string GAMES_WITHOUT_IMAGE = "Games Without Image";
        private const string GAMES_WITH_IMAGE = "Games With Image";
        private const string CLEA_ALL_FILTERS = "__Clear_All_Filters__";
        private const string ITEM_FAVORITE = "_Favorite_";
        private const string DB_BACKUP_EXT = ".GameLauncher_Daily_Backup.db";
        public const string DETACH = "DETACH DATABASE 'D';";
        private readonly Dictionary<string, string> minimum_decade_year = new Dictionary<string, string>()
            {
                { "1980-1989", "1980" },
                { "1990-1999", "1990" },
                { "2000-2010", "2000" },
                { "2020's", "2020" },
            };
        private readonly Dictionary<string, string> maximum_decade_year = new Dictionary<string, string>()
            {
                { "1980-1989", "1989" },
                { "1990-1999", "1999" },
                { "2000-2010", "2009" },
                { "2020's", "2099" },
            };
        public const int PHRASE_MATCH_MIN_WORDS = 4;
        public const int GamePadDown = 18000;
        public const int GamePadUp = 0;
        public const int GamePadLeft = 27000;
        public const int GamePadRight = 9000;
        // -------------------------------------------------------------------------------------------------------------------------------------
        #endregion /////////////////////////////////////////////////////////////////////////////////
        #region Modifiable variables
        private List<Rom> romList = new List<Rom>();
        private List<Rom> romsWithoutImages = null;
        private string romSubFolderName = @"\roms";
        private string imageSubFolderName = @"\images";
        private string dataDirPath = null;
        private string binDirPath = null;
        public string defaultImagePath { get; private set; } = null;
        private Dictionary<string, PreviousCollectedData> previousCollections = new Dictionary<string, PreviousCollectedData>();
        private ImageList[] tmpImageList1 = null;
        private ImageList[] tmpImageList2 = null;
        private List<int> startPosList = null;
        private string lastSearchStr = "";
        private string lastDirSelected = "";
        private bool gavePreviousWarningOnImageChangeNotTakeAffect = false;
        public List<string> validRomTypes { get; private set; }
        public System.Timers.Timer filterTimer = new System.Timers.Timer();
        private string newFilterValue = "";
        private static int incrementTempDir = 0;
        private bool formInitiated = false;
        private string gameLauncherPath = null;
        private string gameLauncherDbPath = null;
        private string logFileName = null;
        // -------------------------------------------------------------------------------------------------------------------------------------
        #endregion /////////////////////////////////////////////////////////////////////////////////
        #region Modifiable static variables used by static methods
        private static Form_Main form_Main = null;
        // -----------------------------------
        // BEGIN -- joystick related
        private static Thread threadJoyStick = null;
        private static bool threadJoyStickAborting = false;
        private static bool noJoystickFound = false;
        private static string miscData = "";
        private static bool waitingShellExecuteToComplete = false;
        private static Dictionary<string, long> dictAvoidRepeat = new Dictionary<string, long>();
        // END -- joystick related
        // -----------------------------------
        private static bool cancelWork = false;
        private static bool pauseWork = false;
        private static int pauseInSeconds = 0;
        private static int qtyWork = 0;
        private static int qtyChanges = 0;
        private static int qtyNoChanges = 0;
        private static int qtyProcess = 0;
        private static int qtySet2Changes = 0;
        private static int qtyTotalChanges = 0;
        private static int qtyTotalProcess = 0;
        private static TaskbarManager taskbarProcess = TaskbarManager.Instance;

        private static StreamWriter logFileWriter = null;
        public static SqliteConnection connection { get; private set; } = null;
        public static SqliteConnection connection_GameDetails { get; private set; } = null;
        public static MD5 md5 { get; private set; } = MD5.Create();
        public static SHA256 sha256 { get; private set; } = SHA256.Create();
        public static FileSystemWatcher watcher_ImagerFolder = null;
        public static string lastImageFolderWatch = "";
        public static Db db = null;
        // -------------------------------------------------------------------------------------------------------------------------------------
        #endregion /////////////////////////////////////////////////////////////////////////////////
        #region Constructor, Form1_Shown, and form initialization functions
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Form_Main()
        {
            form_Main = this;
            db = new Db(this);
            InitializeComponent();
            int x = StrCompare.GetDifference("frogger2threedeep", "frogger2threeedeep");
            x = StrCompare.GetDifference("frogger2threeedeep", "frogger2threedeep");
            x = StrCompare.GetDifference("1234567890", "12345678"); // source2 is shorter
            x = StrCompare.GetDifference("12345678", "1234567890"); // source1 is shorter
            x = StrCompare.GetDifference("0123456789", "12345678"); // source2 is shorter
            x = StrCompare.GetDifference("12345678", "0123456789"); // source1 is shorter
            x = StrCompare.GetDifference("0123456789", "abcdefgh"); // source2 is shorter
            x = StrCompare.GetDifference("abcdefgh", "0123456789"); // source1 is shorter
            x = StrCompare.GetDifference("abcdefgh", "ABCDEFGH"); // case test
            x = StrCompare.GetDifference("abcdefghi", "ABCDEFGH"); // case test
            x = StrCompare.Difference("abcdefgh", "ABCDEFGH"); // case test
            x = StrCompare.Difference("abcdefghi", "ABCDEFGH"); // case test
            x = StrCompare.Difference("abcdefgh", "ABCDEFGHI"); // case test
            x = StrCompare.GetDifference("", "");
            x = StrCompare.GetDifference("fox", "foxx");
            x = StrCompare.GetDifference("foxx", "fox");
            x = StrCompare.GetDifference("xxxxx", "yyyyy");
            x = StrCompare.GetDifference("The world is on fire", "The world is on fire");
            x = StrCompare.GetDifference("Th world is on fire", "The world is on fire");
            x = StrCompare.GetDifference("The world is on fire", "Th world is on fire");
            x = StrCompare.GetDifference("The the world is on fire", "The world is on fire");
            x = StrCompare.GetDifference("world is on fire", "The world is on fire");
            x = StrCompare.GetDifference("is fire world on", "The world is on fire");
            x = StrCompare.GetPhraseDifference("is fire world on", "The world is on fire");
            x = StrCompare.GetPhraseDifference("world is on fire", "The world is on fire");
            x = StrCompare.GetPhraseDifference("WorldIsOnFire", "world is on fire");
            x = StrCompare.GetPhraseDifference("WorldIsOnFire", "world is on fire");
            x = StrCompare.GetPhraseDifference("world is on fire", "The world is on fire");
            x = StrCompare.GetPhraseDifference("world is on fire", "The world is on fire", true);
            TempDirStorage.DeleteTempMultithreadDir();
            toolStripTextBox_Filter.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            PopulateValidRomTypes();
            ValidatePropertiesSettings();
            PopulateStaticComboBoxMenu();
            binDirPath = AppDomain.CurrentDomain.BaseDirectory;
            dataDirPath = AppDomain.CurrentDomain.BaseDirectory;
            filterTimer.Elapsed += new System.Timers.ElapsedEventHandler(Filter_Timer_Elapsed);
            filterTimer.Interval = 1500;
            filterTimer.Start();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            string dbPath = GetDbPath();
            if (dbPath != null && Directory.Exists(System.IO.Path.GetDirectoryName(dbPath)))
                dataDirPath = System.IO.Path.GetDirectoryName(dbPath);
            defaultImagePath = GetDefaultImagePath(dbPath); // Make sure to do this before InitializeDbConnection
            logFileName = GetLoggingFileName();
            logFileWriter = new StreamWriter(logFileName, append: true);
            logFileWriter.AutoFlush = true;
            if (Properties.Settings.Default.LoggingLevel > 0)
                Console.SetError(logFileWriter);
            InitializeDbConnection(dbPath);
            GetPropertySettingsFromDB();
            Properties.Settings.Default.Save();
            Populate_Filter_AutoCompleteCustomSource();
            PopulateDynamicComboBoxMenu(); // This has to be called after DB init
            // CreateDbDailyBackup(); // Add logic to do this new thread
            BeginInvoke(new MyDelegateForm_Main(DelegateCreateDbDailyBackup), GetDelegateAction(this, null));
            if (Properties.Settings.Default.useJoystickController)
            {
                threadJoyStick = new Thread(PollJoystick);
                threadJoyStick.Start();
            }
            SetImageFolderWatcher();
            UpdateSettings();
            toolStripMenuItem_ConvertRomToRAR.Enabled = RarCompressArchive.IsWinRarInstalled();
            formInitiated = true;
#if DEBUG
            Db.UnitTestCreatedFunctions();
            if (!db.UnitTestDbClass())
            {
                ErrorMessage("Database Unit test failed!!!\nCheck error logging.");
            }
#endif
        }
        private void PopulateStaticComboBoxMenu()
        {
            toolStripComboBoxIconDisplay.Items.Add("Large Icons");
            toolStripComboBoxIconDisplay.Items.Add("Small Icons");
            toolStripComboBoxIconDisplay.Items.Add("Tiles");
            toolStripComboBoxIconDisplay.Text = "Large Icons";

            toolStripComboBox_FilterYearsDecade.Items.Add(ITEM_ANY);
            toolStripComboBox_FilterYearsDecade.Items.Add("1980-1989");
            toolStripComboBox_FilterYearsDecade.Items.Add("1990-1999");
            toolStripComboBox_FilterYearsDecade.Items.Add("2000-2010");
            toolStripComboBox_FilterYearsDecade.Items.Add("2020's");
            toolStripComboBox_FilterYearsDecade.Text = ITEM_ANY;

            toolStripComboBox_FilterStarRating.Items.Add(ITEM_ANY);
            toolStripComboBox_FilterStarRating.Items.Add("5");
            toolStripComboBox_FilterStarRating.Items.Add("4+");
            toolStripComboBox_FilterStarRating.Items.Add("3+");
            toolStripComboBox_FilterStarRating.Items.Add("2+");
            toolStripComboBox_FilterStarRating.Text = ITEM_ANY;

            toolStripComboBox_GamesWithWithoutImage.Items.Add(ITEM_ANY);
            toolStripComboBox_GamesWithWithoutImage.Items.Add(GAMES_WITHOUT_IMAGE);
            toolStripComboBox_GamesWithWithoutImage.Items.Add(GAMES_WITH_IMAGE);
            toolStripComboBox_GamesWithWithoutImage.Items.Add(CLEA_ALL_FILTERS);
            toolStripComboBox_GamesWithWithoutImage.Text = ITEM_ANY;
        }
        private void PopulateDynamicComboBoxMenu() // This has to be called after DB init
        {
            List<string> Ratings = GetRatings();
            if (Ratings.Count > 0)
            {
                toolStripComboBox_FilterRating.Items.Clear();
                toolStripComboBox_FilterRating.Items.Add(ITEM_ANY);
                foreach (string item in Ratings)
                    toolStripComboBox_FilterRating.Items.Add((string)item);
                toolStripComboBox_FilterRating.Sorted = true;
                toolStripComboBox_FilterRating.Sorted = false;
                toolStripComboBox_FilterRating.Text = ITEM_ANY;
                toolStripComboBox_FilterRating.Items.Add(CLEA_ALL_FILTERS);
            }
            else
                toolStripMenuItem_RatingParentMenu.Enabled = false;

            List<string> Regions = GetRegions();
            if (Regions.Count > 0)
            {
                toolStripComboBox_FilterRegion.Items.Clear();
                toolStripComboBox_FilterRegion.Items.Add(ITEM_ANY);
                foreach (string item in Regions)
                    toolStripComboBox_FilterRegion.Items.Add((string)item);
                toolStripComboBox_FilterRegion.Sorted = true;
                toolStripComboBox_FilterRegion.Text = ITEM_ANY;
            }
            else
                toolStripMenuItem_RegionParentMenu.Enabled = false;

            List<string> Developers = GetDevelopers();
            if (Developers.Count > 0)
            {
                toolStripComboBox_FilterDeveloper.Items.Clear();
                toolStripComboBox_FilterDeveloper.Items.Add(ITEM_ANY);
                foreach(string item in Developers)
                    toolStripComboBox_FilterDeveloper.Items.Add((string)item);
                toolStripComboBox_FilterDeveloper.Sorted = true;
                toolStripComboBox_FilterDeveloper.Text = ITEM_ANY;
            }
            else
                toolStripMenuItem_DeveloperParentMenu.Enabled = false;
            
            List<string> Genres = GetGenres();
            if (Genres.Count > 0)
            {
                toolStripComboBox_FilterGenre.Items.Clear();
                toolStripComboBox_FilterGenre.Items.Add(ITEM_ANY);
                foreach(string item in Genres)
                    toolStripComboBox_FilterGenre.Items.Add((string)item);
                toolStripComboBox_FilterGenre.Sorted = true;
                toolStripComboBox_FilterGenre.Text = ITEM_ANY;
            }
            else
                toolStripMenuItem_GenreParentMenu.Enabled = false;
            
            List<string> Languages = GetLanguages();
            if (Languages.Count > 0)
            {
                toolStripComboBox_FilterLanguage.Items.Clear();
                toolStripComboBox_FilterLanguage.Items.Add(ITEM_ANY);
                foreach(string item in Languages)
                    toolStripComboBox_FilterLanguage.Items.Add((string)item);
                toolStripComboBox_FilterLanguage.Sorted = true;
                toolStripComboBox_FilterLanguage.Text = ITEM_ANY;
            }
            else
                toolStripMenuItem_LanguageParentMenu.Enabled = false;

            List<string> QtyPlayers = GetQtyPlayers();
            if (QtyPlayers.Count > 0)
            {
                toolStripComboBox_FilterMaxPlayers.Items.Clear();
                toolStripComboBox_FilterMaxPlayers.Items.Add(ITEM_ANY);
                toolStripComboBox_FilterMaxPlayers.Items.Add("Multi-Player");
                foreach (string item in QtyPlayers)
                {
                    if (Int32.Parse(item) > 1)
                    toolStripComboBox_FilterMaxPlayers.Items.Add((string)item);
                }
                toolStripComboBox_FilterMaxPlayers.Text = ITEM_ANY;
            }
            else
                toolStripMenuItem_MaxPlayersParentMenu.Enabled = false;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion /////////////////////////////////////////////////////////////////////////////////
        #region Static methods
        public static bool IsNullOrEmpty(string s) => String.IsNullOrEmpty(s);
        public static bool IsEmptyOrNull(string s) => String.IsNullOrEmpty(s);
        public static string GetFirstItemOnStack(Exception ex)
        {
            string stackTrace = ex.StackTrace;
            if (!IsNullOrEmpty(stackTrace))
            {
                // Split the stack trace into lines
                string[] lines = stackTrace.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                // Get the first line of the stack trace
                string firstLine = lines.FirstOrDefault();
                if (firstLine != null)
                    return firstLine;
            }
            return "";
        }
        public static void InfoLogging(string msg, bool alwaysConsoleWrite = true)
        {
            if (alwaysConsoleWrite || Properties.Settings.Default.LoggingLevel > 2)
                Console.WriteLine($"Info: {msg}");
            if (logFileWriter != null && Properties.Settings.Default.LoggingLevel > 2)
                logFileWriter.WriteLine($"Info: {msg}");
        }
        public static void WarnLogging(string msg)
        {
            Console.WriteLine($"Warning: {msg}");
            if (logFileWriter != null && Properties.Settings.Default.LoggingLevel > 1)
                logFileWriter.WriteLine($"Warning: {msg}");
        }
        public static void ErrorLogging(string msg)
        {
            Console.WriteLine($"Error: {msg}");
            if (logFileWriter != null && Properties.Settings.Default.LoggingLevel > 0)
                logFileWriter.WriteLine($"Error: {msg}");
        }
        public static void InfoMessage(string message, string title = "Info...") => MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        public static void WarnMessage(string message, string title = "Warning!") => MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        public static void ErrorMessage(string message, string title = "Error!!!", string processName = null, string circumstances = "", int errorCode = 0)
        {
            MessageBox.Show($"Error: {message}", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (processName != null && connection != null)
                DbErrorLogging(processName, message, "", circumstances, errorCode);
        }
        public static void ErrorMessage(Exception ex, string message, string title = "Error!!!", string circumstances = "", int errorCode = 0)
        {
            MessageBox.Show($"Error: {message}", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (connection == null)
                ErrorLogging(message);
            else
                DbErrorLogging(GetFirstItemOnStack(ex), message, ex.StackTrace, circumstances, errorCode);
        }
        public static string[] GetFilesByExtensions(string dir, SearchOption searchOption, params string[] extensions)
        {
            if (extensions == null)
                throw new ArgumentNullException("extensions");
            string[] files = new string[0];
            foreach (string e in extensions)
            {
                string ext = e;
                if (ext.StartsWith("."))
                    ext = $"*{e}";
                string[] newSet = Directory.GetFiles(dir, ext, searchOption);
                if (newSet != null && newSet.Length > 0)
                    files = files.Concat(newSet).ToArray();
            }
            return files;
        }
        public static bool NotEmpty(string str) => str != null && str.Length > 0;
        public static void RegexRenameFiles(string dir, string pattern, string replaceStr)
        {// Example Usage: RegexRenameFiles(@"C:\Emulator\NintendoDS\roms", "(roms\\\\)[0-9][0-9][0-9][0-9][\\s_]-[\\s_](.*)", "$1$2"); 
            if (!Directory.Exists(dir))
                return;
            string[] files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
            foreach (string f in files)
            {
                string newName = Regex.Replace(f, pattern, replaceStr);
                if (!newName.Equals(f, StringComparison.OrdinalIgnoreCase) && !File.Exists(newName))
                    File.Move(f, newName);
            }
        }
        public static byte[] GetChecksum(string filePath, ChecksumType checksumType = ChecksumType.None) 
        {
            try
            {
                return (Properties.Settings.Default.SHA256OverMD5 && checksumType == ChecksumType.None) || checksumType == ChecksumType.SHA256 ? sha256.ComputeHash(File.ReadAllBytes(filePath)) : md5.ComputeHash(File.ReadAllBytes(filePath));
            }
            catch (Exception ex)
            {
                ErrorLogging($"GetChecksum exception thrown \"{ex.Message}\" for file {filePath}!");
                if (checksumType != ChecksumType.None) 
                {
                    string checksumTye = checksumType == ChecksumType.SHA256 ? "SHA256" : "MD5";
                    ErrorMessage(ex,$"Error while getting {checksumType} checksum for file {filePath}.\nException Error:\n{ex.Message}\nCall Stack:\n{ex.StackTrace}", "Checksum Error!!!", $"Error with filePath = {filePath}");
                }
            }
            byte[] emptyBytes = {0};
            return default;
        }
        public static string GetChecksumStr(string filePath, ChecksumType checksumType = ChecksumType.None)
        {
            try
            {
                byte[] c = GetChecksum(filePath, checksumType);
                return Convert.ToBase64String(c);
            }
            catch (Exception ex)
            {
                ErrorLogging($"GetChecksumStr exception thrown \"{ex.Message}\" for file {filePath}!");
            }
            return "";
        }
        public static string GetShortcutTargetFile(string shortcutFilename)
        {
            string pathOnly = System.IO.Path.GetDirectoryName(shortcutFilename);
            string filenameOnly = System.IO.Path.GetFileName(shortcutFilename);

            // This requires a COM Reference to Shell32 (Microsoft Shell Controls And Automation).
            Shell32.Shell shell = new Shell32.Shell();
            Folder folder = shell.NameSpace(pathOnly);
            FolderItem folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null && folderItem.IsLink)
            {
                Shell32.ShellLinkObject link = (Shell32.ShellLinkObject)folderItem.GetLink;
                return link.Path;
            }

            return null;
        }
        private static bool IsRepeat(string keys, long MaxSecondsToWait = MAX_SECONDS_TO_WAIT_DEFAULT)
        {
            keys = $"__{keys}";
            DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
            long now = dto.ToUnixTimeSeconds();
            if (dictAvoidRepeat.ContainsKey(keys) && dictAvoidRepeat[keys] + MaxSecondsToWait > now)
                return true;
            dictAvoidRepeat[keys] = now;
            return false;
        }
        public static bool Send_Keys(string keys, long MaxSecondsToWait = MAX_SECONDS_TO_WAIT_DEFAULT)
        {
            DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
            long now = dto.ToUnixTimeSeconds();
            if (dictAvoidRepeat.ContainsKey(keys) && dictAvoidRepeat[keys] + MaxSecondsToWait > now)
                return false;
            dictAvoidRepeat[keys] = now;
            SendKeys.SendWait(keys);
            return true;
        }
        public static void PollJoystick()
        {
            try
            {
                // https://stackoverflow.com/questions/18416039/joystick-acquisition-with-sharpdx
                //var directInput = new DirectInput();
                // To simulate cursor and keys see following: https://gamedev.stackexchange.com/questions/19906/how-do-i-simulate-the-mouse-and-keyboard-using-c-or-c
                DirectInput directInput = new DirectInput();

                List<Joystick> sticks = new List<Joystick>();
                foreach (DeviceInstance device in directInput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly))
                {
                    Joystick stick = new Joystick(directInput, device.InstanceGuid);
                    stick.Acquire();

                    foreach (DeviceObjectInstance deviceObject in stick.GetObjects(DeviceObjectTypeFlags.Axis))
                    {
                        stick.GetObjectPropertiesById(deviceObject.ObjectId).Range = new InputRange(-100, 100);
                    }
                    sticks.Add(stick);
                }

                Guid joystickGuid = Guid.Empty;
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly))
                    joystickGuid = deviceInstance.InstanceGuid;
                // If Gamepad not found, look for a Joystick
                if (joystickGuid == Guid.Empty)
                    foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly))
                        joystickGuid = deviceInstance.InstanceGuid;

                if (joystickGuid == Guid.Empty)
                    foreach (var deviceInstance in directInput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly))
                        joystickGuid = deviceInstance.InstanceGuid;

                // If Joystick not found, throws an error
                if (joystickGuid == Guid.Empty)
                {
                    ErrorLogging("No joystick/Gamepad found."); // Maybe warning instead of error logging
                    noJoystickFound = true;
                    threadJoyStickAborting = true;
                    return;
                    //Console.ReadKey();
                    //Environment.Exit(1);
                }

                Joystick joystick = new Joystick(directInput, joystickGuid);
                joystick.Properties.BufferSize = 128;
                joystick.Acquire();
                while (threadJoyStickAborting == false)
                {
                    joystick.Poll();
                    if (threadJoyStickAborting && Properties.Settings.Default.useJoystickController == false)
                        return;
                    if (waitingShellExecuteToComplete)
                        continue;
                    //foreach (Joystick js in sticks) 
                    //{
                    //    JoystickUpdate lastState = js.GetBufferedData().Last();
                    //    if (lastState.Offset == JoystickOffset.X)
                    //    {
                    //        form1.textBoxStatus.Text = lastState.Value.ToString();
                    //    }
                    //}
                    miscData = "";
                    var data = joystick.GetBufferedData();
                    foreach (JoystickUpdate state in data)
                    {
                        if (waitingShellExecuteToComplete)
                            break;
                        if (state.Offset == JoystickOffset.X)
                        {
                            miscData += "[x]"; // 1st Joystick left and down
                        }
                        else if (state.Offset == JoystickOffset.Y)
                        {
                            miscData += "[y]"; // 1st Joystick right and up
                        }
                        else if (state.Offset == JoystickOffset.Z)
                        {
                            miscData += "[z]"; // Lower trigger [left]val=34000>, [right]val=31000>
                            if (state.Value > 34000)
                            {
                                if (!Send_Keys("{END}"))
                                    break;
                            }
                            else
                            {
                                if (!Send_Keys("{F9}"))
                                    break;
                            }
                        }
                        else if (state.Offset == JoystickOffset.RotationX)
                        {
                            miscData += "[RotationX]"; // 2nd Joystick left and right
                        }
                        else if (state.Offset == JoystickOffset.RotationY)
                        {
                            miscData += "[RotationY]";// 2nd Joystick up and down
                        }
                        else if (state.Offset == JoystickOffset.RotationZ)
                        {
                            miscData += "[RotationZ]";
                        }
                        else if (state.Offset == JoystickOffset.Buttons0)
                        {
                            miscData += "[Buttons0]"; // A button
                            if (!Send_Keys("{F5}", MAX_SECONDS_TO_WAIT_F5))
                                break;
                        }
                        else if (state.Offset == JoystickOffset.Buttons1)
                        {
                            miscData += "[Buttons1]"; // B button
                            if (!Send_Keys("{TAB}"))
                                break;
                        }
                        else if (state.Offset == JoystickOffset.Buttons2)
                        {
                            miscData += "[Buttons2]"; // X button
                            Send_Keys("{PGDN}");
                        }
                        else if (state.Offset == JoystickOffset.Buttons3)
                        {
                            miscData += "[Buttons3]"; // Y button
                            Send_Keys("{PGUP}");
                        }
                        else if (state.Offset == JoystickOffset.Buttons4)
                        {
                            miscData += "[Buttons4]"; // Left upper trigger
                            if (!Send_Keys("{HOME}"))
                                break;
                        }
                        else if (state.Offset == JoystickOffset.Buttons5)
                        {
                            miscData += "[Buttons5]"; // Right upper trigger
                            if (!Send_Keys("{F8}"))
                                break;
                        }
                        else if (state.Offset == JoystickOffset.Buttons6)
                        {
                            miscData += "[Buttons6]"; // Back/Select button
                            //System.Windows.Forms.Application.Exit();
                        }
                        else if (state.Offset == JoystickOffset.Buttons7)
                        {
                            miscData += "[Buttons7]"; // Start button
                            if (!Send_Keys("{F5}", MAX_SECONDS_TO_WAIT_F5))
                                break;
                        }
                        else if (state.Offset == JoystickOffset.PointOfViewControllers0)
                        { // PointOfViewControllers0, [up]val=0, [down]val=18000,[left]val=27000,[right]val=9000
                            miscData += $"[PointOfViewControllers0 {state.Value}]";
                            if (GamePadUp == state.Value)
                                SendKeys.SendWait("{UP}");
                            else if (GamePadDown == state.Value)
                                SendKeys.SendWait("{DOWN}");
                            else if (GamePadLeft == state.Value)
                                SendKeys.SendWait("{LEFT}");
                            else if (GamePadRight == state.Value)
                                SendKeys.SendWait("{RIGHT}");
                        }
                        else if (state.Offset == JoystickOffset.Buttons8)
                        {
                            miscData += "[Buttons8]"; // 1st Joystick center button
                            if (!Send_Keys("{F6}"))
                                break;
                        }
                        else if (state.Offset == JoystickOffset.Buttons9)
                        {
                            miscData += "[Buttons9]"; // 2nd Joystick center button
                            if (!Send_Keys("{F4}"))
                                break;
                        }
                        else
                        {
                            miscData += "[misc]";
                        }
                    }
                }
            }
            catch {
                ErrorLogging("PollJoystick exception thrown!!!");
                noJoystickFound = true;
            }
        }
        #endregion /////////////////////////////////////////////////////////////////////////////////
        #region Delegate coding
        public delegate void MyDelegateInitProgressBar(System.Windows.Forms.ProgressBar myControl, int arg);
        public void DelegateInitProgressBar(System.Windows.Forms.ProgressBar myControl, int max)
        {
            if (max == 0)
                return;
            myControl.Enabled = false;
            myControl.Visible = false;
            myControl.Update();
            myControl.Minimum = 1;
            myControl.Maximum = max;
            myControl.Step = 1;
            myControl.Value = 1;
            myControl.ResetText();
            myControl.Enabled = true;
            myControl.Visible = true;
            myControl.Update();
        }
        public void DelegateInitProgressBarPerformStep(System.Windows.Forms.ProgressBar myControl, int maxLenReset)
        {
            myControl.PerformStep();
            if (maxLenReset != -1 && myControl.Value >= maxLenReset)
                myControl.Value = 0;
            myControl.Update();
            myControl.Refresh();
            System.Windows.Forms.Application.DoEvents();
        }
        public delegate void MyDelegateForm_Main_DeleteDuplicateBy(Form_Main form_Main, DeleteDuplicateBy arg);
        public delegate void MyDelegateForm_Main(Form_Main form_Main, bool arg);
        public void DelegateDisplayOrHideProgressGroup(Form_Main form_Main, bool display) => form_Main.DisplayOrHideProgressGroup(display);
        public void DelegateScanForNewRomsOnAllSystems(Form_Main form_Main, bool arg) => form_Main.ScanForNewRomsOnAllSystems(arg);
        public void DelegateRescanSelectedSystem(Form_Main form_Main, bool arg) => form_Main.RescanSelectedSystem(null, arg);
        public void DelegateDeleteImageList(Form_Main form_Main, bool arg) => form_Main.DeleteImageList();
        public void DelegateCreateCacheForDisplaySystemIcons(Form_Main form_Main, bool arg) => form_Main.CreateCacheForDisplaySystemIcons(arg);
        public void DelegateDeleteImageFilesNotInDatabase(Form_Main form_Main, bool arg) => form_Main.DeleteImageFilesNotInDatabase(arg);
        public void DelegateDeleteDuplicateRomFilesInDatabase(Form_Main form_Main, DeleteDuplicateBy arg) => form_Main.DeleteDuplicateRomFilesInDatabase(arg);
        public void DelegateRedoDbInit(Form_Main form_Main, bool arg) => form_Main.RedoDbInit("Are you sure you want to rescan all ROM's?\nThis will take about 15-60 minutes.", "Rescan?", MessageBoxIcon.Question);
        public delegate void MyDelegateInitializeRomsInDatabaseForSystem(Form_Main form_Main, InitializeRomsInDatabaseForSystem_Arg arg);
        public void DelegateInitializeRomsInDatabaseForSystem(Form_Main form_Main, InitializeRomsInDatabaseForSystem_Arg arg) => form_Main.InitializeRomsInDatabaseForSystem(arg);
        public delegate void MyDelegateSetControlText(System.Windows.Forms.Control myControl, string text);
        public void UpdateTextInFromBackgroundTask(System.Windows.Forms.Control myControl, string text)
        {
            myControl.Text = text;
            myControl.Refresh();
            myControl.Update();
            myControl.Refresh();
            System.Windows.Forms.Application.DoEvents();
        }
        public void DelegateSetControlText(System.Windows.Forms.Control myControl, string text) => UpdateTextInFromBackgroundTask(myControl, text);
        public delegate void MyDelegateSetTextBoxText(System.Windows.Forms.TextBox myControl, string text);
        public void DelegateSetTextBoxText(System.Windows.Forms.TextBox myControl, string text) => UpdateTextInFromBackgroundTask(myControl, text);
        public delegate void MyDelegateSetLabelText(System.Windows.Forms.Label myControl, string text);
        public void DelegateSetLabelText(System.Windows.Forms.Label myControl, string text) => UpdateTextInFromBackgroundTask(myControl, text);
        public void DelegateCreateDbDailyBackup(Form_Main form_Main, bool arg) => form_Main.CreateDbDailyBackup();
        public object[] GetDelegateAction(object control, object arg)
        {
            object[] myArray = new object[2];
            myArray[0] = control;
            myArray[1] = arg;
            return myArray;
        }
        #endregion /////////////////////////////////////////////////////////////////////////////////
        #region SQL small supporting methods
        private bool CreateAndInitSqlTables() => UpdateDB(SQL_CREATETABLES) && UpdateDB(SQL_DB_INIT);
        public static bool UpdateDB(string sql, SqliteConnection conn)
        {
            if (conn == null)
            {
                WarnLogging($"UpdateDB called with conn = NULL; SQL = {sql}");
                return false;
            }
            InfoLogging($"UpdateDB called with sql = {sql}", false);
            for (int i = 0; i < Properties.Settings.Default.MaxRetryDbExecute; ++i)
            {
                try
                {
                    using (SqliteCommand command = new SqliteCommand(sql, conn))
                    {
                        int x = command.ExecuteNonQuery(); // Returns number of rows changed, or -1 for select statement
                        if (sql.StartsWith("CREATE ", StringComparison.OrdinalIgnoreCase))
                            return x == 0; // Warning: When creating tables, zero is expected return value.
                        return x != 0;
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogging($"UpdateDB exception thrown \"{ex.Message}\"!\nSQL={sql}");
                    Thread.Sleep(Properties.Settings.Default.DbRetryTimeoutInSeconds * 1000);
                }
            }
            return false;
        }
        private static bool UpdateDB(string sql) => UpdateDB(sql, connection);
        public static void DbErrorLogging(string processName, string message, string stack = "", string circumstances = "", int errorCode = 0)
        {
            try 
            {   // Make sure not to fail while trying to log an error to the database.
                ErrorLogging($"{message}\nStack={stack}");
                UpdateDB("INSERT OR REPLACE INTO ErrorLog (Process, Message, Code, Circumstances, Stack) VALUES " +
                    $"(\"{processName}\", \"{message.Replace("\"", "'")}\", {errorCode}, \"{circumstances.Replace("\"", "'")}\", \"{stack.Replace("\"", "'")}\")");
            }
            catch (Exception ex)
            {
                ErrorLogging($"DbErrorLogging exception thrown \"{ex.Message}\"!");
            }
        }
        public static void DbErrorLogging(Exception ex, string message, string circumstances = "", int errorCode = 0) => DbErrorLogging(GetFirstItemOnStack(ex), message, ex.StackTrace, circumstances, errorCode);
        private static void EraseOldErrorLogging(string processName = "%") => UpdateDB($"DELETE FROM ErrorLog WHERE Process like \"{processName}\"");
        private static string GetFirstColStr(string sql, string defaultValue = "", string preferredContains = "", string preferredNotContain = "") => GetFirstColStr(sql, defaultValue, connection, preferredContains, preferredNotContain);
        private static string GetFirstColStr(string sql, SqliteConnection conn, string preferredContains = "", string preferredNotContain = "") => GetFirstColStr(sql, "", conn, preferredContains, preferredNotContain);
        private static string GetFirstColStr(string sql, string defaultValue, SqliteConnection conn, string preferredContains = "", string preferredNotContain = "")
        {
            // On failure, returns default value
            if (IsEmptyOrNull(sql)) 
                return defaultValue;
            string notContains = "";
            try
            {
                using (SqlReader reader = new SqlReader(sql, conn))
                {
                    if (reader.Read())
                    {
                        var firstColumn = reader.GetString(0);
                        defaultValue = firstColumn;
                        if ((preferredContains.Length == 0 && preferredNotContain.Length == 0) ||
                            defaultValue.Contains(preferredContains, StringComparison.OrdinalIgnoreCase))
                            return firstColumn;
                        if (!defaultValue.Contains(preferredNotContain, StringComparison.OrdinalIgnoreCase))
                            notContains = firstColumn;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogging($"GetFirstColStr exception thrown \"{ex.Message}\"!");
            }
            return notContains.Length > 0 ? notContains : defaultValue;
        }
        private static int GetFirstColInt(string sql, int defaultValue = -1) => GetFirstColInt(sql, defaultValue, connection);
        private static int GetFirstColInt(string sql, SqliteConnection conn) => GetFirstColInt(sql, -1, conn);
        private static int GetFirstColInt(string sql, int defaultValue, SqliteConnection conn)
        {
            // On failure, returns default value
            if (IsEmptyOrNull(sql))
                return defaultValue;
            InfoLogging($"GetFirstColInt called with sql = {sql}", false);
            try
            {
                using (SqlReader reader = new SqlReader(sql, conn))
                {
                    if (reader.Read())
                    {
                        var idx = reader.GetInt32(0);
                        return idx;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogging($"GetFirstColInt exception thrown \"{ex.Message}\"!");
            }
            return defaultValue;
        }
        private int GetImageIndexByPath(string FilePath) => GetFirstColInt($"SELECT ID FROM Images WHERE FilePath like \"{FilePath}\"");
        public int GetSystemIndex(string Name) => GetFirstColInt($"SELECT ID FROM GameSystems WHERE Name = \"{Name}\"");
        public static string GetSystemNameByID(int id) => GetFirstColStr($"SELECT Name FROM GameSystems WHERE ID = \"{id}\"");
        private void EraseDbContent() => UpdateDB("DELETE FROM Roms;\r\nDELETE FROM Images;\r\nDELETE FROM GameSystems;\r\nDELETE FROM sqlite_sequence;\r\n");
        public string GetPersistenceVariable(string name, string defaultValue = "") => GetFirstColStr($"SELECT Value FROM PersistenceVariables WHERE Name = \"{name}\"", defaultValue);
        public int GetPersistenceVariable(string name, int defaultValue)=> GetFirstColInt($"SELECT ValueInt FROM PersistenceVariables WHERE Name = \"{name}\"", defaultValue);
        public bool GetPersistenceVariable(string name, bool defaultValue) => GetPersistenceVariable(name, defaultValue ? 1 : 0) == 1;
        public void GetPropertySettingsFromDB()
        {
            Properties.Settings.Default.SHA256OverMD5 = GetPersistenceVariable("SHA256OverMD5", Properties.Settings.Default.SHA256OverMD5);
            Properties.Settings.Default.DoZipChecksum = GetPersistenceVariable("DoZipChecksum", Properties.Settings.Default.DoZipChecksum);
            Properties.Settings.Default.DoImageChecksum = GetPersistenceVariable("DoImageChecksum", Properties.Settings.Default.DoImageChecksum);
            Properties.Settings.Default.DoRomChecksum = GetPersistenceVariable("DoRomChecksum", Properties.Settings.Default.DoRomChecksum);
            Properties.Settings.Default.DefaultImagePath = GetPersistenceVariable("DefaultImagePath", Properties.Settings.Default.DefaultImagePath);
            Properties.Settings.Default.EmulatorsBasePath = GetPersistenceVariable("EmulatorsBasePath", Properties.Settings.Default.EmulatorsBasePath);
            Properties.Settings.Default.usePreviousCollectionCache = GetPersistenceVariable("usePreviousCollectionCache", Properties.Settings.Default.usePreviousCollectionCache);
            Properties.Settings.Default.largeIconSize = GetPersistenceVariable("largeIconSize", Properties.Settings.Default.largeIconSize);
            Properties.Settings.Default.smallIconSize = GetPersistenceVariable("smallIconSize", Properties.Settings.Default.smallIconSize);
            Properties.Settings.Default.LoggingLevel = GetPersistenceVariable("LoggingLevel", Properties.Settings.Default.LoggingLevel);
        }
        private void SavePersistenceVariable(string name, string value) => UpdateDB($"INSERT OR REPLACE INTO PersistenceVariables (Name, Value) VALUES (\"{name}\", \"{value}\")");
        private void SavePersistenceVariable(string name, int valueInt) => UpdateDB($"INSERT OR REPLACE INTO PersistenceVariables (Name, ValueInt) VALUES (\"{name}\", \"{valueInt}\")");
        private void SavePersistenceVariable(string name, bool value) => SavePersistenceVariable(name, value ? 1 : 0);
        public void SavePropertySettingsToDB()
        {
            SavePersistenceVariable("SHA256OverMD5", Properties.Settings.Default.SHA256OverMD5);
            SavePersistenceVariable("DoZipChecksum", Properties.Settings.Default.DoZipChecksum);
            SavePersistenceVariable("DoImageChecksum", Properties.Settings.Default.DoImageChecksum);
            SavePersistenceVariable("DoRomChecksum", Properties.Settings.Default.DoRomChecksum);
            SavePersistenceVariable("DefaultImagePath", Properties.Settings.Default.DefaultImagePath);
            SavePersistenceVariable("EmulatorsBasePath", Properties.Settings.Default.EmulatorsBasePath);
            SavePersistenceVariable("usePreviousCollectionCache", Properties.Settings.Default.usePreviousCollectionCache);
            SavePersistenceVariable("largeIconSize", Properties.Settings.Default.largeIconSize);
            SavePersistenceVariable("smallIconSize", Properties.Settings.Default.smallIconSize);
            SavePersistenceVariable("LoggingLevel", Properties.Settings.Default.LoggingLevel);
        }
        public static string GetEmulatorExecutable(string systemName, int emulatorID = 1) => GetFirstColStr($"SELECT EmulatorPath{emulatorID} FROM GameSystems WHERE Name = \"{systemName}\"");
        public string GetEmulatorExecutable(int systemId, int emulatorID = 1) => GetFirstColStr($"SELECT EmulatorPath{emulatorID} FROM GameSystems WHERE ID = \"{systemId}\"");
        private List<string> GetQtyPlayers() => db.GetList("SELECT QtyPlayers FROM Roms WHERE QtyPlayers is NOT NULL GROUP BY QtyPlayers");
        private List<string> GetRegions(bool MajorRegions = true)
        {
            List<string> Regions = db.GetList("SELECT Region FROM Roms WHERE Region is NOT NULL GROUP BY Region");
            if (MajorRegions)
            {
                HashSet<string> majorRegions = new HashSet<string>();
                foreach (string r in Regions)
                {
                    string region = r.Replace("&", ",").Replace(" and ", ",").Replace("The ", "").Replace("United States", "USA");
                    if (region.Contains("All regions", StringComparison.OrdinalIgnoreCase)
                        || region.Contains("world", StringComparison.OrdinalIgnoreCase)
                        // || region.Contains("unknown", StringComparison.OrdinalIgnoreCase)
                        )
                        continue;
                    if (region.IndexOf(",") > 0)
                    {
                        string[] regions = region.Split(',');
                        foreach (string item in regions)
                            majorRegions.Add(item.Trim(' '));
                    }
                    else
                        majorRegions.Add(region);
                }
                Regions = new List<string>();
                foreach (string item in majorRegions)
                    Regions.Add(item);
            }
            return Regions;
        }
        private List<string> GetDevelopers() => db.GetList("SELECT Developer FROM Roms WHERE Developer is NOT NULL GROUP BY Developer");
        private List<string> GetGenres(bool MajorGenres = true)
        {
            List<string> Genres = db.GetList("SELECT Genre FROM Roms WHERE Genre is NOT NULL GROUP BY Genre");
            if (MajorGenres)
            {
                HashSet<string> majorGenres = new HashSet<string>();
                foreach(string m in Genres)
                {
                    string majorGenre = m.Replace("Fight-Action", "Fight").Replace("Fighting", "Fight")
                        .Replace("Racing-F1", "Racing").Replace("Racing-Future", "Racing")
                        .Replace("Racing-Kart", "Racing").Replace("Racing-Rally", "Racing");
                    int pos = majorGenre.IndexOf(";");
                    if (pos > 0)
                        majorGenres.Add(majorGenre.Substring(0, pos));
                    else
                        majorGenres.Add(majorGenre);
                }
                Genres = new List<string>();
                foreach (string majorGenre in majorGenres)
                    Genres.Add(majorGenre);
            }
            return Genres;
        }
        private List<string> GetRatings() => db.GetList("SELECT Rating FROM Roms WHERE Rating is NOT NULL AND Rating <> \"Not Rated\" GROUP BY Rating");
        private List<string> GetLanguages(bool doIndividualLanguages = true) 
        {
            List<string> Languages = db.GetList("SELECT Language FROM Roms WHERE Language is NOT NULL GROUP BY Language");
            if (doIndividualLanguages)
            {
                HashSet<string> individualLanguages = new HashSet<string>();
                foreach (string l in Languages)
                {
                    string language = l.Replace(" and/or ", ",").Replace(" translation", "");
                    if (language.IndexOf(",") > 0)
                    {
                        string[] languages = language.Split(',');
                        foreach (string item in languages)
                            individualLanguages.Add(item.Trim(' '));
                    }
                    else
                        individualLanguages.Add(language);
                }
                Languages = new List<string>();
                foreach (string language in individualLanguages)
                    Languages.Add(language);
            }
            return Languages;
        }
        #endregion /////////////////////////////////////////////////////////////////////////////////
        #region Database related methods
        ////////////////////////////////////////////////////////////////////////////////////////
        public bool InitializeDbConnection(string dbPath)
        {
            using (new CursorWait())
            {
                if (dbPath == null)
                {
                    ErrorMessage($"Can not find {GAMELAUNCHER_DB_NAME}.\nExpected to find file in following path\n{dbPath}\nPlease select option to set database ({GAMELAUNCHER_DB_NAME}) location.", "Missing database");
                    return false;
                }
                bool initDb = !File.Exists(dbPath);
                connection = Db.CreateConnection($"Filename={dbPath}");
                connection.Open();
                if (initDb)
                    CreateAndInitSqlTables();
                connection_GameDetails = Db.CreateConnection($"Filename={GetPropertyDataPath(GAMEDETAILS_DB_NAME)}");
                connection_GameDetails.Open();
                gameLauncherDbPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                gameLauncherPath = $"{System.Environment.GetEnvironmentVariable("USERPROFILE")}\\LaunchBox";
                gameLauncherDbPath = $"{gameLauncherPath}\\Metadata\\LaunchBox.Metadata.db";
                toolStripMenuItem_RenameLaunchBoxROMFiles.Enabled = File.Exists(gameLauncherDbPath);
                Properties.Settings.Default.DbPath = dbPath;
                List<string> SystemNames = GetSystemNames();
                if (SystemNames.Count < 1)
                {
                    SystemNames = InitializeRomsInDatabase(GetEmulatorsBasePath());
                    if (SystemNames.Count < 1)
                    {//ToDo: Add error message here.
                        return false;
                    }
                }
                else
                    GetEmulatorsBasePath(); // Call this to set Properties.Settings.Default.EmulatorsBasePath
                string LastSystemSelected = GetPersistenceVariable("toolStripComboBoxSystem");
                toolStripComboBoxSystem.Text = LastSystemSelected != null && LastSystemSelected.Length != 0 ? LastSystemSelected : SystemNames[0];
                string LastIconDisplaySelected = GetPersistenceVariable("toolStripComboBoxIconDisplay");
                toolStripComboBoxIconDisplay.Text = LastIconDisplaySelected != null && LastIconDisplaySelected.Length != 0 ? LastIconDisplaySelected : "Large Icons";
            }
            return true;
        }
        private string GetImageIndexByName(string NameSimplified, string NameOrg, string Title, string Compressed, string emulatorDir) => GetImageIndexByName(NameSimplified, NameOrg, Title, Compressed, emulatorDir, connection);
        private string GetImageIndexByName(string NameSimplified, string NameOrg, string Title, string Compressed, string emulatorDir, SqliteConnection conn)
        {
            string preferredContains = Properties.Settings.Default.PreviewOverBoxArt ? $"(preview)" : "(box)";
            string preferredNotContains = Properties.Settings.Default.PreviewOverBoxArt ? $"(box)" : "(preview)";
            string sql = $"SELECT FilePath FROM Images WHERE Title like \"{Title}\" and FilePath like \"{emulatorDir}%\"";
            string imgPath = "";
            if (emulatorDir.Length > 0)
            {
                imgPath = GetFirstColStr(sql, conn, preferredContains, preferredNotContains);
                if (NotEmpty(imgPath))
                    return imgPath;
            }

            sql = $"SELECT FilePath FROM Images WHERE Title like \"{Title}\"";
            imgPath = GetFirstColStr(sql, conn, preferredContains, preferredNotContains);
            if (NotEmpty(imgPath))
                return imgPath;

            if (emulatorDir.Length > 0)
            {
                sql = $"SELECT FilePath FROM Images WHERE NameSimplified like \"{NameSimplified}\" and FilePath like \"{emulatorDir}%\"";
                imgPath = "";
                imgPath = GetFirstColStr(sql, conn, preferredContains, preferredNotContains);
                if (NotEmpty(imgPath))
                    return imgPath;
            }

            sql = $"SELECT FilePath FROM Images WHERE NameSimplified like \"{NameSimplified}\"";
            imgPath = GetFirstColStr(sql, conn, preferredContains, preferredNotContains);
            if (NotEmpty(imgPath))
                return imgPath;

            if (NameOrg.Contains("-"))
            {
                string NameAfterHyphen = NameOrg.Substring(NameOrg.IndexOf("-") + 1);
                string NameAfterHyphenSimple = ConvertToSimplifiedName(NameAfterHyphen);
                sql = $"SELECT FilePath FROM Images WHERE NameSimplified like \"{NameAfterHyphenSimple}\"";
                imgPath = GetFirstColStr(sql, conn, preferredContains, preferredNotContains);
                if (NotEmpty(imgPath))
                    return imgPath;

                string nameWithNoNumbersNameAfterHyphen = ConvertToSimplifiedName(NameAfterHyphen, true);
                sql = $"SELECT FilePath FROM Images WHERE NameSimplified like \"{nameWithNoNumbersNameAfterHyphen}\"";
                imgPath = GetFirstColStr(sql, conn, preferredContains, preferredNotContains);
                if (NotEmpty(imgPath))
                    return imgPath;
            }
            string nameWithNoNumbers = ConvertToSimplifiedName(NameSimplified, true); // Remove numbers from the name. It's better to have an image to the older version of the game, than non at all.
            if (!nameWithNoNumbers.Equals(NameSimplified))
            {
                sql = $"SELECT FilePath FROM Images WHERE NameSimplified like \"{nameWithNoNumbers}\"";
                imgPath = GetFirstColStr(sql, conn, preferredContains, preferredNotContains);
                if (NotEmpty(imgPath))
                    return imgPath;
            }

            if (NameOrg.StartsWith("A ", StringComparison.CurrentCultureIgnoreCase))
            {
                string Name = NameOrg.Substring(1);
                sql = $"SELECT FilePath FROM Images WHERE NameSimplified like \"{Name}%\"";
                imgPath = GetFirstColStr(sql, conn, preferredContains, preferredNotContains);
                if (NotEmpty(imgPath))
                    return imgPath;
            }

            if (NameOrg.StartsWith("The ", StringComparison.CurrentCultureIgnoreCase))
            {
                string Name = NameOrg.Substring(3);
                sql = $"SELECT FilePath FROM Images WHERE NameSimplified like \"{Name}%\"";
                imgPath = GetFirstColStr(sql, conn, preferredContains, preferredNotContains);
                if (NotEmpty(imgPath))
                    return imgPath;
            }

            sql = $"SELECT FilePath FROM Images WHERE NameOrg like \"{NameOrg}\"";
            imgPath = GetFirstColStr(sql, conn, preferredContains, preferredNotContains);
            if (NotEmpty(imgPath))
                return imgPath;

            sql = $"SELECT FilePath FROM Images WHERE NameSimplified like \"{NameOrg}\"";
            imgPath = GetFirstColStr(sql, conn, preferredContains, preferredNotContains);
            if (NotEmpty(imgPath))
                return imgPath;

            sql = $"SELECT FilePath FROM Images WHERE Compressed like \"{Compressed}\"";
            imgPath = GetFirstColStr(sql, conn, preferredContains, preferredNotContains);
            if (NotEmpty(imgPath))
                return imgPath;

            sql = $"SELECT FilePath FROM Images WHERE Compressed like \"%{Compressed}%\"";
            imgPath = GetFirstColStr(sql, conn, preferredContains, preferredNotContains);
            if (NotEmpty(imgPath))
                return imgPath;

            sql = $"SELECT FilePath FROM Images WHERE NameOrg like \"%{Title}%\"";
            imgPath = GetFirstColStr(sql, conn, preferredContains, preferredNotContains);
            return imgPath;
        }
        private string AddImagePathToDb(string imgFile, bool incProgressBar, bool isMainThread, bool checkIfInDb = false, bool checkIfExists = false, 
            bool importFiles = false, bool checkMatchingRom = false, bool replaceGeneralImageFiles = false, GameSystem gameSystem = null, bool confirmationPrompt = false,
            SqliteConnection conn = null)
        {
            bool doImportFiles = importFiles;
            if (incProgressBar)
                IncrementProgressBar(progressBar_ROMs, isMainThread, $"Adding {imgFile}");
            string filename = Path.GetFileName(imgFile);
            if (filename.StartsWith("."))
            {
                SendStatus($"Skipping image file {imgFile} because invalid name; {qtyProcess} of {qtyWork}", isMainThread);
                return "";
            }
            if (checkIfExists && !File.Exists(imgFile))
                return "";
            if (checkIfInDb)
            {
                string imagePath = GetFirstColStr($"SELECT FilePath FROM Images WHERE FilePath like \"{imgFile}\"");
                if (NotEmpty(imagePath))
                    return imagePath;
            }
            if (Properties.Settings.Default.AutoConvertImageFilesToJpg)
            {
                imgFile = ConvertFileToJpg(imgFile, Properties.Settings.Default.AutoDeleteOldImageFileAfterConversion,false, isMainThread);
                if (doImportFiles && Properties.Settings.Default.OnlyImportConvertedImageFiles)
                {
                    string ext = Path.GetExtension(imgFile);
                    if (!ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase) && !ext.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
                        return "";
                }
            }
            string NameOrg = Path.GetFileNameWithoutExtension(imgFile);
            string NameSimplified = ConvertToNameSimplified(NameOrg);
            string Compressed = ConvertToCompress(NameOrg);
            string Title = ConvertToTitle(NameOrg);
            string Checksum = GetImageChecksum(imgFile);
            string masterImgPath = $"{GetEmulatorsBasePath()}\\{Properties.Settings.Default.imageSubFolderName}";
            if (!masterImgPath.EndsWith("\\"))
                masterImgPath += "\\";
            if (Properties.Settings.Default.DoImageChecksum && !IsEmptyOrNull(Checksum))
            {
                string imagePath = GetFirstColStr($"SELECT FilePath FROM Images WHERE Checksum = \"{Checksum}\"");
                if (NotEmpty(imagePath))
                {
                    string existingTitle = GetFirstColStr($"SELECT Title FROM Images WHERE Checksum = \"{Checksum}\"");
                    if (existingTitle == Title)
                    {
                        if (!doImportFiles || gameSystem == null || imagePath.StartsWith(gameSystem.ImageDirPath))
                            return imagePath;
                    }
                    if (gameSystem == null || imagePath.StartsWith(gameSystem.ImageDirPath))
                        doImportFiles = false;
                    imgFile = imagePath;
                }
            }
            string closeMatchRom = null;
            string systemOnly = gameSystem == null ? "" : $" AND System = {gameSystem.ID} ";
            if (doImportFiles)
            {
                string checkIfGeneralImageFolder = replaceGeneralImageFiles ? $" OR ImagePath like \"{masterImgPath}%\" " : "";
                // Only import if there are ROM's associated with the image
                string sql_title = $"SELECT FilePath FROM Roms WHERE ({GetWhereMissingImage()} {checkIfGeneralImageFolder}) {systemOnly} AND Title like \"{Title}\"";
                string sql_Compressed = $"SELECT FilePath FROM Roms WHERE ({GetWhereMissingImage()} {checkIfGeneralImageFolder}) {systemOnly} AND Compressed like \"{Compressed}\"";
                string romFile = GetFirstColStr(sql_title);
                if (IsNullOrEmpty(romFile) && Compressed.Length > 2)
                    romFile = GetFirstColStr(sql_Compressed);
                if (IsNullOrEmpty(romFile))
                {
                    // Check to see if there's an alias or close matching ROM file.
                    closeMatchRom = GetFirstColStr(GetCloseMatchRomSql(Title, systemOnly));
                    if (IsEmptyOrNull(closeMatchRom))
                        return "";
                    romFile = closeMatchRom;
                }
                if (IsNullOrEmpty(romFile) && romsWithoutImages != null)
                {
                    int MatchDiff = Title.Length * 2;
                    foreach (Rom rom in romsWithoutImages) 
                    {
                        int diff = StrCompare.GetDifference(rom.Title, Title);
                        if (diff < 2 && diff < MatchDiff)
                        {
                            MatchDiff = diff;
                            romFile = rom.FilePath;
                            if (diff == 0)
                                break;
                        }
                    }
                    if (IsNullOrEmpty(romFile))
                    {
                        foreach (Rom rom in romsWithoutImages)
                        {
                            int diff = StrCompare.GetDifference(rom.NameSimplified, NameSimplified);
                            if (diff < 2 && diff < MatchDiff)
                            {
                                MatchDiff = diff;
                                romFile = rom.FilePath;
                                if (diff == 0)
                                    break;
                            }
                        }
                    }
                    if (IsNullOrEmpty(romFile) && StrCompare.GetWordCount(Title) >= PHRASE_MATCH_MIN_WORDS)
                    {
                        foreach (Rom rom in romsWithoutImages)
                        {
                            int diff = StrCompare.GetPhraseDifference(rom.Title, Title);
                            if (diff < 2 && diff < MatchDiff)
                            {
                                MatchDiff = diff;
                                romFile = rom.FilePath;
                                if (diff == 0)
                                    break;
                            }
                        }
                    }
                    if (IsNullOrEmpty(romFile))
                    {
                        foreach (Rom rom in romsWithoutImages)
                        {
                            int diff = StrCompare.GetDifference(rom.Compressed, Compressed);
                            if (diff < 2 && diff < MatchDiff)
                            {
                                MatchDiff = diff;
                                romFile = rom.FilePath;
                                if (diff == 0)
                                    break;
                            }
                        }
                    }
                }
                string destImageDir = gameSystem == null ? GetParent_Parent(romFile, true) : gameSystem.ImageDirPath;
                if (gameSystem == null)
                {
                    if (destImageDir.Equals(masterImgPath, StringComparison.OrdinalIgnoreCase))
                        destImageDir = System.IO.Path.GetDirectoryName(romFile);
                    destImageDir = Path.Combine(destImageDir, Properties.Settings.Default.imageSubFolderName);
                }
                if (!Directory.Exists(destImageDir))
                    Directory.CreateDirectory(destImageDir);
                string destImageFile = Path.Combine(destImageDir, Path.GetFileName(imgFile));
                if (File.Exists(destImageFile))
                    return "";
                if (confirmationPrompt)
                {
                    Rom rom = db.Get_Rom(romFile);
                    Form_ConfirmImage form_ConfirmImage = new Form_ConfirmImage(this, imgFile, rom, $"Do you want to copy image file {imgFile} to {destImageFile}, and set ROM '{rom.Title}' to this image?");
                    form_ConfirmImage.ShowDialog();
                    if (!form_ConfirmImage.Ok)
                    {
                        if (form_ConfirmImage.Quit)
                            cancelWork = true;
                        return "";
                    }
                }
                File.Copy(imgFile, destImageFile);
                imgFile = destImageFile;
            }
            string sql = $"INSERT OR REPLACE INTO Images (Title, NameSimplified, NameOrg, Compressed, FilePath, Checksum) VALUES (\"{Title}\", \"{NameSimplified}\", \"{NameOrg}\", \"{Compressed}\", \"{imgFile}\", \"{Checksum}\")";
            if (db.Update(sql, conn))
                ++qtySet2Changes;
            if (checkMatchingRom)
            {
                if (closeMatchRom == null)
                {
                    if (FindRomMissingImageWhichMatchesThisImage(imgFile, Title, NameOrg, NameSimplified, Compressed) == 0 && !doImportFiles)
                    {
                        // Check to see if there's an alias or close matching ROM file.
                        closeMatchRom = GetFirstColStr(GetCloseMatchRomSql(Title, systemOnly));
                    }
                }
                if (!IsEmptyOrNull(closeMatchRom))
                {
                    Rom rom = db.Get_Rom(closeMatchRom);
                    if (rom != null && !rom.ImagePath.Equals(imgFile, StringComparison.OrdinalIgnoreCase))
                    {
                        if (rom.ImagePath.StartsWith(masterImgPath))
                        {
                            File.Delete(rom.ImagePath);
                            // ToDo: Add logic to update all roms with the new image path if the old image path is not the default image file
                        }
                        rom.ImagePath = imgFile;
                        if (db.Update(rom))
                            ++qtySet2Changes;
                    }
                }
            }
            return imgFile;
        }
        private string GetCloseMatchRomSql(string Title, string moreWhereClause) => CreateSqlQueryFindSimilarNames(Title, "FilePath", "Title", "Roms", $" AND ({GetWhereMissingImage()}) {moreWhereClause}");
        public List<GameImage> GetCloseMatchImageList(
            string phrase,                                  // Phrase to search
            string fieldToCompare = "Title",          // Column to compare keyword search
            SqliteConnection conn = null
        )
        {
            string sql = CreateSqlQueryFindSimilarNames(phrase, "*", fieldToCompare, "Images", "", 
                true,false,1, PHRASE_MATCH_MIN_WORDS, 3);
            List<GameImage> gameImages = null;
            return db.Get(sql, ref gameImages, conn) ? gameImages : null;
        }
        private string GetImagePathInDb(string imgFile) => AddImagePathToDb(imgFile, false, true, true);
        private void GetImagesAndWait(string imagePath, bool showCompleteMessage = true)
        {
            using (new CursorWait())
            {
                GetImages(imagePath, true);
                if (showCompleteMessage)
                    MessageBox.Show($"Image scan complete for path {imagePath}. Processed {qtyProcess} files and made {qtySet2Changes} DB changes.");
            }
        }
        private void GetImages(string imgPath, bool isMainThread = false, bool importFiles = false, SearchOption searchOption = SearchOption.TopDirectoryOnly, 
            bool checkIfInDb = false, bool checkMatchingRom = false, bool replaceGeneralImageFiles = false, GameSystem gameSystem = null, bool confirmationPrompt = false)
        {
            if (Directory.Exists(imgPath))
            {
                using (new CursorWait(taskbarProcess, progressBar_BottomRight))
                {
                    using (new TempConnection(false, confirmationPrompt))
                    {

                        if (confirmationPrompt)
                        {
                            romsWithoutImages = new List<Rom>();
                            db.Get(ref romsWithoutImages, $" WHERE {GetWhereMissingImage()}");
                        }
                        string[] imgFiles = GetFilesByExtensions(imgPath, searchOption, SUPPORTED_IMAGE_FILES);
                        ResetWorkStatus(imgFiles.Length, isMainThread);
                        ResetProgressBar(imgFiles.Length, isMainThread);
                        if (isMainThread)
                        {
                            foreach (var imgFile in imgFiles)
                            {
                                if (DidCancelButtonGetPressed(isMainThread))
                                {
                                    SendStatus($"GetImages cancelled with only processing {qtyProcess} of {imgFiles.Length} image files.", isMainThread);
                                    return;
                                }
                                IncProgress();
                                AddImagePathToDb(imgFile, true, isMainThread, checkIfInDb, false, 
                                    importFiles, checkMatchingRom, replaceGeneralImageFiles, gameSystem, confirmationPrompt);
                                SendStatus($"Processing image file {imgFile}; {qtyProcess} of {imgFiles.Length}", isMainThread);
                            }
                            SendStatus($"Completed processing {imgFiles.Length} image files. Made {qtySet2Changes} DB changes.", isMainThread);
                        }
                        else
                            Parallel.ForEach(imgFiles, (imgFile, loopState) =>
                            {
                                if (DidCancelButtonGetPressed(isMainThread))
                                    loopState.Stop();
                                AddImagePathToDb(imgFile, true, false, false, false, importFiles);
                            });
                    }
                }
                ClearProgress();
                if (confirmationPrompt)
                {
                    romsWithoutImages = null;
                }
            }
        }
        public void InitializeRomsInDatabaseForSystem(InitializeRomsInDatabaseForSystem_Arg arg) => InitializeRomsInDatabaseForSystem(arg.emulatorDir, arg.emulatorExecutables, arg.isMainThread, arg.scanImageDir, arg.hideGroup);

        private void InitializeRomsInDatabaseForSystem(string emulatorDir, EmulatorExecutables emulatorPaths, bool isMainThread = false, bool scanImageDir = true, bool hideGroup = false)
        {
            ResetWorkStatus();
            string romPath = emulatorDir + romSubFolderName;
            string imgPath = emulatorDir + imageSubFolderName;
            if (Directory.Exists(romPath) && IsEmulatorSupported(emulatorPaths))
            {
                string Name = Path.GetFileName(emulatorDir);
                string sql = $"INSERT OR REPLACE INTO GameSystems (Name, RomDirPath, ImageDirPath, EmulatorPath1, EmulatorPath2, EmulatorPath3, EmulatorPath4, EmulatorPath5, EmulatorPath6, EmulatorPath7, EmulatorPath8, EmulatorPath9, EmulatorPath10) VALUES " +
                    $"(\"{Name}\", \"{romPath}\", \"{imgPath}\", \"{emulatorPaths.EmulatorPaths[0]}\", \"{emulatorPaths.EmulatorPaths[1]}\", \"{emulatorPaths.EmulatorPaths[2]}\", \"{emulatorPaths.EmulatorPaths[3]}\", \"{emulatorPaths.EmulatorPaths[4]}\", \"{emulatorPaths.EmulatorPaths[5]}\", \"{emulatorPaths.EmulatorPaths[6]}\", \"{emulatorPaths.EmulatorPaths[7]}\", \"{emulatorPaths.EmulatorPaths[8]}\", \"{emulatorPaths.EmulatorPaths[9]}\")";
                UpdateDB(sql);
                int systemIndex = GetSystemIndex(Name);
                SendStatus($"The game system '{Name}' has been created successfully into row {systemIndex}", isMainThread);
                if (scanImageDir)
                {
                    UpdateGroupBoxText(isMainThread, $"Performing image scan on \"{imgPath}\"");
                    if (Directory.Exists(imgPath))
                        GetImages(imgPath, isMainThread);
                }
                UpdateGroupBoxText(isMainThread, $"Starting ROM scan on \"{romPath}\"");
                string[] romFiles = Directory.GetFiles(romPath, "*", SearchOption.AllDirectories);
                ResetProgressBar(romFiles.Length, isMainThread);
                foreach (string f in romFiles)
                {
                    if (DidCancelButtonGetPressed(isMainThread))
                        break;
                    IncrementProgressBar(progressBar_ROMs, isMainThread, $"Processing ROM {f}");
                    long RomSize = new System.IO.FileInfo(f).Length;
                    string ext = Path.GetExtension(f).ToLower().TrimStart('.');
                    if (ext.Equals("sav"))
                        continue;
                    if (!validRomTypes.Contains(ext.ToLower()) && RomSize < MINIMUM_ROM_SIZE)
                    {
                        if (ext.Equals("zip") || ext.Equals("bin"))
                        { // There are some Atari ROM's that are smaller than 2K
                            if (RomSize < MINIMUM_ZIP_SIZE)
                                continue;
                        }
                        else
                            continue;
                    }
                    db.Update(GetRomDetails(f, systemIndex, emulatorDir, RomSize));
                }
                if (!DidCancelButtonGetPressed(isMainThread))
                    GetMultiplayerRomData(emulatorDir);
                if (isMainThread)
                    UpdateGroupBoxText(isMainThread, $"ROM scan complete for \"{romPath}\""); // ToDo: Add quantity count to this
            }
            if (hideGroup)
                BeginInvoke(new MyDelegateForm_Main(DelegateDisplayOrHideProgressGroup), GetDelegateAction(this, false));
        }
        private string[] GetEmulatorDirs(string dirs)
        {
            string[] directories = null;
            if (dirs.Contains(";"))
                directories = dirs.Split(';');
            else if (dirs.Contains(","))
                directories = dirs.Split(',');

            if (directories == null)
                return Directory.GetDirectories(dirs, "*", SearchOption.TopDirectoryOnly);

            string[] emulatorDirs = new string[0];
            foreach (string dir in directories)
            {
                string romPath = dir + romSubFolderName;
                if (Directory.Exists(romPath))
                {
                    string[] thisDir = { dir};
                    emulatorDirs.Concat(thisDir);
                }
                else
                    emulatorDirs.Concat(Directory.GetDirectories(dir, "*", SearchOption.TopDirectoryOnly));
            }
            return emulatorDirs;
        }
        private List<string> ScanANewEmulatorToDatabase(string onlySearchSpecificEmulatorPath)
        {
            string startingPath = System.IO.Path.GetDirectoryName(onlySearchSpecificEmulatorPath);
            return InitializeRomsInDatabase(startingPath, null, null, false, 0, onlySearchSpecificEmulatorPath);
        }
        private List<string> InitializeRomsInDatabase(string startingPath = @"C:\Emulators", string romSubFolder = null, string imageSubFolder = null, bool createTables = false, int doMultithread = 0, string onlySearchSpecificEmulatorPath = null)
        {
            ResetWorkStatus();
            if (romSubFolder == null)
                romSubFolder = $"\\{Properties.Settings.Default.romSubFolderName}";
            if (imageSubFolder == null)
                imageSubFolder = $"\\{Properties.Settings.Default.imageSubFolderName}";
            romSubFolderName = romSubFolder;
            imageSubFolderName = imageSubFolder;
            Stopwatch totalProcessWatch = System.Diagnostics.Stopwatch.StartNew();
            Stopwatch databaseCollectionWatch = System.Diagnostics.Stopwatch.StartNew();
            textBoxStatus.Text = $"Getting top directories from \"{startingPath}\"";
            string[] emulatorDirs = GetEmulatorDirs(startingPath);
            DisplayOrHideProgressGroup(true, emulatorDirs.Length);
            if (createTables)
                CreateAndInitSqlTables();
            // Check for master image path. Note: These images can apply to any game in any system which matches truncated name
            string masterImgPath = startingPath + imageSubFolderName;
            if (onlySearchSpecificEmulatorPath == null && Directory.Exists(masterImgPath))
            {
                textBoxStatus.Text = $"Processing images in path {masterImgPath}";
                label_GameConsoleLabel.Text = $"Processing images in path {masterImgPath}";
                GetImages(masterImgPath,true);
            }

            textBoxStatus.Text = $"Creating {emulatorDirs.Length} SystemScanTaskData.";
            SystemScanTaskData[] systemScanTaskDatas = new SystemScanTaskData[emulatorDirs.Length];
            Stopwatch findEmulatorExecuteProcessWatch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < emulatorDirs.Length; ++i)
            {
                systemScanTaskDatas[i] = new SystemScanTaskData();
                systemScanTaskDatas[i].systemName = Path.GetFileName(emulatorDirs[i]);
                systemScanTaskDatas[i].emulatorDir = emulatorDirs[i];
                systemScanTaskDatas[i].emulatorExecutables = GetEmulatorPaths(emulatorDirs[i]);
            }
            findEmulatorExecuteProcessWatch.Stop();
            TimeSpan findEmulatorExecuteElapsed = findEmulatorExecuteProcessWatch.Elapsed;
            // Note: Not finding improvement in performance for either multithread methods.
            if (doMultithread == 0)
            {
                SendStatus($"Processing {emulatorDirs.Length} game console systems.", true);
                foreach (SystemScanTaskData systemScanTaskData in systemScanTaskDatas)
                {
                    if (DidCancelButtonGetPressed(true))
                        break;
                    if (onlySearchSpecificEmulatorPath != null && !systemScanTaskData.emulatorDir.StartsWith(onlySearchSpecificEmulatorPath))
                        continue;
                    IncrementProgressBar(progressBar1, true);
                    label_GameConsoleLabel.Text = $"Processing game console {systemScanTaskData.systemName}";
                    label_GameConsoleLabel.Update();
                    progressBar1.Update();
                    InitializeRomsInDatabaseForSystem(systemScanTaskData.emulatorDir, systemScanTaskData.emulatorExecutables, true);
                    SendStatus($"Process complete for game console {systemScanTaskData.systemName}", true);
                }
            }
            else
            {
                if (doMultithread == 1)
                {
                    System.Threading.Tasks.Task[] tasks = new System.Threading.Tasks.Task[emulatorDirs.Length];
                    for (int i = 0; i < emulatorDirs.Length; ++i)
                    {
                        int ii = i;
                        //tasks[ii] = System.Threading.Tasks.Task.Run(() => InitializeRomsInDatabaseForSystem(emulatorDirs[ii], systemScanTaskDatas[ii].emulatorExecutables));
                        tasks[ii] = System.Threading.Tasks.Task.Factory.StartNew(() => InitializeRomsInDatabaseForSystem(emulatorDirs[ii], systemScanTaskDatas[ii].emulatorExecutables));
                    }
                    System.Threading.Tasks.Task.WaitAll(tasks);
                }
                else if (doMultithread == 2)
                    Parallel.ForEach(systemScanTaskDatas, (systemScanTaskData, loopState) =>
                    {
                        if (DidCancelButtonGetPressed(true))
                            loopState.Stop();
                        InitializeRomsInDatabaseForSystem(systemScanTaskData.emulatorDir, systemScanTaskData.emulatorExecutables);
                    });
            }
            databaseCollectionWatch.Stop();
            TimeSpan databaseCollectionElapsed = databaseCollectionWatch.Elapsed;
            Stopwatch imageListCollectionWatch = System.Diagnostics.Stopwatch.StartNew();
            List<string> systemNames = CreateCacheForDisplaySystemIcons();
            imageListCollectionWatch.Stop();
            totalProcessWatch.Stop();
            TimeSpan imageListCollectionElapsed = imageListCollectionWatch.Elapsed;
            TimeSpan totalProcessElapsed = totalProcessWatch.Elapsed;
            DisplayOrHideProgressGroup(false);
            if (!DidCancelButtonGetPressed(true) && onlySearchSpecificEmulatorPath == null)
                InfoMessage($"Completed data collection. DB collection time = {databaseCollectionElapsed.ToString(@"hh\:mm\:ss")} and ImageList time = {imageListCollectionElapsed.ToString(@"mm\:ss")}\nTotal time = {totalProcessElapsed.ToString(@"hh\:mm\:ss")}\nfindEmulatorExecuteElapsed={findEmulatorExecuteElapsed.ToString(@"hh\:mm\:ss")}", "Process Complete");
            return systemNames;
        }
        public string GetPropertyDataPath(string propertyFileName = null)
        {
            string strAppPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string strFilePath = Path.Combine(strAppPath, "Properties");
            if (propertyFileName == null)
                return strFilePath;
            string strFullFilename = Path.Combine(strFilePath, propertyFileName);
            return strFullFilename;
        }
        public string GetDefaultDbPath() => GetPropertyDataPath(GAMELAUNCHER_DB_NAME);
        public string GetDbPath_sub()
        {
            string dbPath;
#if DEBUG
            dbPath = $"C:\\Emulators\\GameLauncher\\Properties\\{GAMELAUNCHER_DB_NAME}";
            if (File.Exists(dbPath))
                return dbPath;
#endif
            if (Properties.Settings.Default.DbPath != null && Properties.Settings.Default.DbPath.Length > 0)
            {
                if (File.Exists(Properties.Settings.Default.DbPath))
                    return Properties.Settings.Default.DbPath;
            }
            dbPath = GetDefaultDbPath();
            if (File.Exists(dbPath))
                return dbPath;
            dbPath = Path.Combine($"{binDirPath}{DATA_SUBPATH}", GAMELAUNCHER_DB_NAME);
            if (File.Exists(dbPath))
                return dbPath;
            foreach (var path in COMMON_EMULATOR_PATHS)
            {
                dbPath = Path.Combine(path, GAMELAUNCHER_DB_NAME);
                if (File.Exists(dbPath))
                    return dbPath;
                dbPath = Path.Combine($"{path}s", GAMELAUNCHER_DB_NAME);
                if (File.Exists(dbPath))
                    return dbPath;

                dbPath = Path.Combine(path, $"{DATA_SUBPATH}{GAMELAUNCHER_DB_NAME}");
                if (File.Exists(dbPath))
                    return dbPath;
                dbPath = Path.Combine($"{path}s", $"{DATA_SUBPATH}{GAMELAUNCHER_DB_NAME}");
                if (File.Exists(dbPath))
                    return dbPath;

                dbPath = Path.Combine(path, $"{GAMELAUNCHER_SUBPATH}{DATA_SUBPATH}{GAMELAUNCHER_DB_NAME}");
                if (File.Exists(dbPath))
                    return dbPath;
                dbPath = Path.Combine($"{path}s", $"{GAMELAUNCHER_SUBPATH}{DATA_SUBPATH}{GAMELAUNCHER_DB_NAME}");
                if (File.Exists(dbPath))
                    return dbPath;
            }
            dbPath = Path.Combine(dataDirPath, GAMELAUNCHER_DB_NAME);
            return dbPath;
        }
        public string GetDbPath()
        {
            string dbPath = GetDbPath_sub();
            if (IsNullOrEmpty(Properties.Settings.Default.DbPath))
                Properties.Settings.Default.DbPath = dbPath;
            return dbPath;
        }
        public bool RedoDbInit(string warningMessage = "", string messageTitle = "", MessageBoxIcon messageBoxIcon = MessageBoxIcon.Error)
        {
            if (warningMessage.Length > 0)
            {
                if (MessageBox.Show(warningMessage, messageTitle, MessageBoxButtons.OKCancel, messageBoxIcon) == DialogResult.Cancel)
                    return false;
            }
            DeleteImageList(); // Do this first so collection files get deleted before erasing DB.
            EraseDbContent();
            EraseOldErrorLogging();
            InitializeDbConnection(Properties.Settings.Default.DbPath);
            return true;
        }
        private List<string> GetSystemNames(bool updateComboBoxSystem = true)
        {
            List<string> SystemNames = new List<string>();
            if (connection == null)
                return SystemNames;
            if (updateComboBoxSystem)
                toolStripComboBoxSystem.Items.Clear();
            using (SqlReader reader = new SqlReader(@"SELECT Name FROM GameSystems ORDER BY Name"))
            {
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    SystemNames.Add(name);
                    if (updateComboBoxSystem)
                        toolStripComboBoxSystem.Items.Add(name);
                }
            }
            if (updateComboBoxSystem)
            {
                toolStripComboBoxSystem.Items.Add(ITEM_ANY);
                toolStripComboBoxSystem.Items.Add(ITEM_FAVORITE);
            }
            return SystemNames;
        }
        private Dictionary<int, string> GetSystemNamesAndID()
        {
            Dictionary<int, string> systemNamesAndID = new Dictionary<int, string>();
            if (connection == null)
                return systemNamesAndID;
            using (SqlReader reader = new SqlReader(@"SELECT Name,ID FROM GameSystems ORDER BY Name"))
            {
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    int ID = reader.GetInt32(1);
                    systemNamesAndID[ID] = name;
                }
            }
            return systemNamesAndID;
        }
        private Dictionary<int, string> GetSystemDirAndID()
        {
            Dictionary<int, string> systemDirAndID = new Dictionary<int, string>();
            if (connection == null)
                return systemDirAndID;
            using (SqlReader reader = new SqlReader(@"SELECT RomDirPath,ID FROM GameSystems ORDER BY Name"))
            {
                while (reader.Read())
                {
                    string RomDirPath = reader.GetString(0);
                    string emulatorDir = Path.GetDirectoryName(RomDirPath);
                    int ID = reader.GetInt32(1);
                    systemDirAndID[ID] = emulatorDir;
                }
            }
            return systemDirAndID;
        }
        private List<string> GetGameDetailsSystemNames()
        {
            List<string> SystemNames = new List<string>();
            if (connection_GameDetails == null)
                return SystemNames;
            using (SqlReader reader = new SqlReader(@"SELECT System FROM GameDetails ORDER BY System", connection_GameDetails))
            {
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    SystemNames.Add(name);
                }
            }
            return SystemNames;
        }
        private string GetGameSystemRomPath(string systemName = null)
        {
            if (systemName == null)
                systemName = toolStripComboBoxSystem.Text;
            string sql = $"SELECT RomDirPath FROM GameSystems WHERE Name like \"{systemName}\"";
            return GetFirstColStr(sql);
        }
        private string GetGameSystemImagePath(int systemID) => GetGameSystemImagePath(GetSystemNameByID(systemID));
        private string GetGameSystemImagePath(string systemName = null)
        {
            if (systemName == null)
                systemName = toolStripComboBoxSystem.Text;
            string sql = $"SELECT ImageDirPath FROM GameSystems WHERE Name like \"{systemName}\"";
            return GetFirstColStr(sql);
        }
        private void SetEmulatorExecute(string system, string emulatorExecutable, int emulatorIndex = 1) => UpdateDB($"UPDATE GameSystems SET EmulatorPath{emulatorIndex} = \"{emulatorExecutable}\" WHERE Name = \"{system}\"");
        private bool IsEmulatorSupported(string executable) => executable.Length > 0 && GetFirstColInt($"SELECT NotSupported FROM InitialDefaultEmulatorAttributes WHERE EmulatorExecutable like \"{Path.GetFileNameWithoutExtension(executable)}%\"", 0) != 1;
        public static bool EmulatorRequiresDecompression(string executable, int defaultValue) => executable.Length > 0 && GetFirstColInt($"SELECT DecompressFile FROM InitialDefaultEmulatorAttributes WHERE EmulatorExecutable like \"%{Path.GetFileNameWithoutExtension(executable)}%\"", defaultValue) == 1;
        private string EmulatorPreferredExtension(string executable) => GetFirstColStr($"SELECT PreferredExtension FROM InitialDefaultEmulatorAttributes WHERE EmulatorExecutable like \"{Path.GetFileNameWithoutExtension(executable)}%\"", "");
        private bool IsEmulatorSupported(EmulatorExecutables emulatorPaths) => emulatorPaths != null && IsEmulatorSupported(emulatorPaths.EmulatorPaths[0]);
        private void Populate_Filter_AutoCompleteCustomSource()
        {
            using (SqlReader reader = new SqlReader($"SELECT Source FROM FilterAutoCompleteCustomSource"))
            {
                while (reader.Read())
                    toolStripTextBox_Filter.AutoCompleteCustomSource.Add(reader.GetString(0));
            }
        }
        public enum BackupType
        {
            AllTables,
            MainTables,
            GameSystem,
            Roms,
            Images,
            EmulatorAttributes,
            Roms_UserChanges,
            Roms_UserChanges_CopyToRoms
        }
        private SqliteConnection GetBackupRestore_SaveFileDialog(string srcOrDest, bool checkFileExists)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = DATABASE_FILE_TYPES;
            saveFileDialog.Title = $"Enter {srcOrDest} GameLauncher database file.";
            saveFileDialog.FileName = "GameLauncher.backup.db";
            saveFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(Properties.Settings.Default.DbPath);
            saveFileDialog.CheckFileExists = checkFileExists;
            DialogResult results = saveFileDialog.ShowDialog();
            if (results == DialogResult.OK) 
            {
                if (checkFileExists == false && File.Exists(saveFileDialog.FileName))
                    File.Delete(saveFileDialog.FileName);
                SqliteConnection conn = Db.CreateConnection($"Filename={saveFileDialog.FileName}");
                conn.Open();
                return conn;
            }
            return null;
        }
        public void BackupDb(BackupType backupType)
        {
            using(SqliteConnection conn = GetBackupRestore_SaveFileDialog("destination", false))
            {
                if (conn == null)
                {
                    WarnLogging($"BackupDb called with conn = NULL");
                    return;
                }
                using (SqliteCommand command = new SqliteCommand(SQL_CREATETABLES, conn))
                {
                    if (command.ExecuteNonQuery() == 0)
                    {
                        string attachDb = $"ATTACH DATABASE '{Properties.Settings.Default.DbPath}' as 'D'";
                        if (UpdateDB(attachDb, conn))
                        {
                            const string copyGameSystem = "INSERT or IGNORE INTO GameSystems SELECT * FROM D.GameSystems;";
                            const string copyRoms = "INSERT or IGNORE INTO Roms SELECT * FROM D.Roms;";
                            const string copyRoms_UserChanges = "INSERT or IGNORE INTO Roms_UserChanges SELECT * FROM D.Roms_UserChanges;";
                            const string copyImages = "INSERT or IGNORE INTO Images SELECT * FROM D.Images;";
                            const string copyEmulatorAttributes = "INSERT or IGNORE INTO InitialDefaultEmulatorAttributes SELECT * FROM D.InitialDefaultEmulatorAttributes;";
                            const string copyPersistenceVariables = "INSERT or IGNORE INTO PersistenceVariables SELECT * FROM D.PersistenceVariables;";
                            const string copyMRU = "INSERT or IGNORE INTO MRU SELECT * FROM D.MRU;";
                            if (backupType == BackupType.MainTables || backupType == BackupType.GameSystem)
                                UpdateDB(copyGameSystem, conn);
                            if (backupType == BackupType.MainTables || backupType == BackupType.Roms)
                                UpdateDB(copyRoms, conn);
                            if (backupType == BackupType.MainTables || backupType == BackupType.Images)
                                UpdateDB(copyImages, conn);
                            if (backupType == BackupType.MainTables)
                            {
                                UpdateDB(copyEmulatorAttributes, conn);
                                UpdateDB(copyRoms_UserChanges, conn);
                                UpdateDB(copyPersistenceVariables, conn);
                                UpdateDB(copyMRU, conn);
                            }
                            UpdateDB(DETACH, conn);
                        }
                    }
                }
            }
            SqliteConnection.ClearAllPools();
            MessageBox.Show("Database Backup complete");
        }
        public void RestoreDb(BackupType backupType)
        {
            using (SqliteConnection conn = GetBackupRestore_SaveFileDialog("source", true)) 
            {
                if (conn == null)
                {
                    WarnLogging($"RestoreDb called with conn = NULL");
                    return;
                }
                string attachDb = $"ATTACH DATABASE '{Properties.Settings.Default.DbPath}' as 'D'";
                using (SqliteCommand command = new SqliteCommand(attachDb, conn))
                {
                    if (command.ExecuteNonQuery() == 0)
                    { // Roms, Images, GameSystem, PersistenceVariables, MRU, and Roms_UserChanges
                        const string copyGameSystem = "INSERT or IGNORE INTO D.GameSystems SELECT * FROM GameSystems;";
                        const string copyRoms = "INSERT or IGNORE INTO D.Roms SELECT * FROM Roms;";
                        const string copyRoms_UserChanges = "INSERT or IGNORE INTO D.Roms_UserChanges SELECT * FROM Roms_UserChanges;";
                        const string copyImages = "INSERT or IGNORE INTO D.Images SELECT * FROM Images;";
                        const string copyEmulatorAttributes = "INSERT or IGNORE INTO D.InitialDefaultEmulatorAttributes SELECT * FROM InitialDefaultEmulatorAttributes;";
                        const string copyPersistenceVariables = "INSERT or IGNORE INTO D.PersistenceVariables SELECT * FROM PersistenceVariables;";
                        const string copyMRU = "INSERT or IGNORE INTO D.MRU SELECT * FROM MRU;";
                        if (backupType == BackupType.MainTables || backupType == BackupType.GameSystem)
                            UpdateDB(copyGameSystem, conn);
                        if (backupType == BackupType.MainTables || backupType == BackupType.Roms)
                            UpdateDB(copyRoms, conn);
                        if (backupType == BackupType.MainTables || backupType == BackupType.Images)
                            UpdateDB(copyImages, conn);
                        if (backupType == BackupType.MainTables)
                        {
                            UpdateDB(copyEmulatorAttributes, conn);
                            UpdateDB(copyRoms_UserChanges, conn);
                            UpdateDB(copyPersistenceVariables, conn);
                            UpdateDB(copyMRU, conn);
                        }
                        if (backupType == BackupType.Roms_UserChanges_CopyToRoms)
                        {
                            List<Roms_UserChanges> RomsUserChanges = null;
                            db.Get(ref RomsUserChanges,"", conn);
                            foreach(Roms_UserChanges r in RomsUserChanges)
                            {
                                Rom rom = db.Get_Rom(r.FilePath);
                                if (rom == null)
                                    continue;
                                if (rom.Update(r))
                                    db.Update(rom);

                            }
                        }
                        UpdateDB(DETACH, conn);
                    }
                }
                SqliteConnection.ClearAllPools();
            }
            MessageBox.Show(backupType == BackupType.Roms_UserChanges_CopyToRoms? "Copied Roms_UserChanges to Roms table." : "Database restore complete");
        }
        public string WordLettersSeparated(string word, string sep = "%")
        {
            string returnWord = "";
            foreach (char c in word)
                returnWord += $"{c}{sep}";
            return returnWord;
        }
        public string CreateSqlQueryFindSimilarNames(
            string phrase,                                  // Phrase to search
            string fieldToReturn = "CompareName",           // Column or Columns to return in SQL query
            string fieldToCompare = "CompareName",          // Column to compare keyword search
            string table = "Games",                         // Target table
            string addedWhereClause = "",                   // Optional where clause section to add to existing where clause
            bool doNearWordSearch = false,                  // Look for words that have all the characters in the words in phrase string
            bool doMinimumFieldLen = false,                 // When enable, search for field having minimum length of phrase string length
            int minimumWordsRequired = 1,                   // Minimum quantity of words required in order to create the SQL query
            int minWordsPercMatch = PHRASE_MATCH_MIN_WORDS, // Minimum quantity of words required in order to do a percentage keyword matching vs 100% keyword matching
            int minimumWordLenForWordInWordMatch = 4,       // Minimum word length required in order to do a word in word search (substr).  Note: Also applies to doNearWordSearch
            int minCount = -1,                              // Leave as -1 to let code calculate minimum count match, or override and set to desired minimum words requiring a match
            int percentageMatch = 80                        // Percentage of required word matching when there's at least (minWordsPercMatch) quantity of words
            )
        {
            string[] keywords = StrCompare.GetKeywordList(ref phrase, true);
            int phraseLen = phrase.Length;
            if (keywords.Length < minimumWordsRequired)
                return null;
            if (minCount == -1)
            {
                if (keywords.Length >= minWordsPercMatch)
                {
                    double result = (double)percentageMatch / 100 * keywords.Length;
                    minCount = (int)result;
                }
                else
                    minCount = keywords.Length;
            }
            string sql = $"SELECT {fieldToReturn}, (";
            for(int i = 0;i < keywords.Length; ++i)
            {
                if (keywords[i].Length >= minimumWordLenForWordInWordMatch)
                {
                    string nearWordSearch = "";
                    if (doNearWordSearch)
                        nearWordSearch += $" OR {fieldToCompare} LIKE '%{WordLettersSeparated(keywords[i])}%' ";
                    sql += $"({fieldToCompare} LIKE '%{keywords[i]}%'  {nearWordSearch}) ";
                }
                else
                    sql += $"(' '||{fieldToCompare}||' ' LIKE '% {keywords[i]} %') ";
                //sql += $"({fieldToCompare} LIKE '{keywords[i]} %' OR {fieldToCompare} LIKE '% {keywords[i]} %' OR {fieldToCompare} LIKE '% {keywords[i]}') ";
                if (i + 1 < keywords.Length)
                    sql += " + ";
            }
            string minimumFieldLen = doMinimumFieldLen ? $"  AND length({fieldToCompare}) > {phraseLen} " : "";
            string noSpace = keywords.Length == 1 ? $" AND {fieldToCompare} NOT LIKE '% %'" : "";
            sql += $" ) AS KeyWordMatchCount FROM {table} WHERE KeyWordMatchCount > {minCount-1} AND {fieldToCompare} NOT LIKE '_-in-1%'  {minimumFieldLen} {noSpace} {addedWhereClause} ORDER BY KeyWordMatchCount DESC;";
            return sql;
        }
        public string GetWhereMissingImageNot()
        {
            return $" ImagePath <> '' AND ImagePath NOT LIKE '{Properties.Settings.Default.DefaultImagePath}'  AND ImagePath IS NOT NULL ";
        }
        public string GetWhereMissingImage()
        {
            return $" ImagePath = '' OR ImagePath LIKE '{Properties.Settings.Default.DefaultImagePath}'  OR ImagePath IS NULL ";
        }
        #endregion /////////////////////////////////////////////////////////////////////////////////
        #region Misc Methods
        public void CreateDbDailyBackup() // ToDo: Add menu option to restore to previous date
        {
            string dirPath = Path.Combine(System.IO.Path.GetTempPath(), $"{System.Windows.Forms.Application.ProductName}\\_temporary_backup_dir\\");
            string fileTodayDateStr = DateTime.Now.ToString("yyyyMMDD");
            string todaysFileName = $"{dirPath}{fileTodayDateStr}{DB_BACKUP_EXT}";
            if (File.Exists(todaysFileName))
                return;
            string yesterdayDateStr = DateTime.Today.AddDays(-1).ToString("yyyyMMDD");
            string dayBeforeYesterdayDateStr = DateTime.Today.AddDays(-2).ToString("yyyyMMDD");
            string yesterdayFileName = $"{dirPath}{yesterdayDateStr}{DB_BACKUP_EXT}";
            string dayBeforeYesterdayFileName = $"{dirPath}{dayBeforeYesterdayDateStr}{DB_BACKUP_EXT}";
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            File.Copy(GetDbPath(), todaysFileName);
            IOrderedEnumerable<string> dbFiles = Directory.GetFiles(dirPath, "*.db").OrderByDescending(d => new FileInfo(d).CreationTime);
            // ToDo: Verify that the list is coming in expected order
            if (dbFiles.Count() < 3)
                return; // If less than 3 backups, then skip trimming old backups
            int qtyOldFilesSkipped = 0;
            int qtyFilesDeleted = 0;
            foreach (string dbFile in dbFiles)
            {
                if (dbFile == todaysFileName || dbFile == yesterdayFileName || dbFile == dayBeforeYesterdayFileName)
                {
                    ++qtyOldFilesSkipped;
                    continue;
                }
                if (!dbFile.EndsWith(DB_BACKUP_EXT))
                    continue;
                ++qtyFilesDeleted;
                // The following logic is so that it tries to keep old backups if they're the last 3 backups in the file system
                if (qtyFilesDeleted < 2 || qtyOldFilesSkipped == 3)
                    File.Delete(dbFile);
            }
        }
        public string GetLoggingFileName()
        {
            string logFileExt = ".log.txt";
            string logPrefixName = GetPropertyDataPath(GAMELAUNCHER_LOGGING_FILENAME);
            string fileTodayDateStr = DateTime.Now.ToString("yyyyMMDD");
            string todaysFileName = $"{logPrefixName}{fileTodayDateStr}{logFileExt}";
            if (File.Exists(todaysFileName))
                return todaysFileName;
            string yesterdayDateStr = DateTime.Today.AddDays(-1).ToString("yyyyMMDD");
            string dayBeforeYesterdayDateStr = DateTime.Today.AddDays(-2).ToString("yyyyMMDD");
            string yesterdayFileName = $"{logPrefixName}{yesterdayDateStr}{logFileExt}";
            string dayBeforeYesterdayFileName = $"{logPrefixName}{dayBeforeYesterdayDateStr}{logFileExt}";
            IOrderedEnumerable<string> dbFiles = Directory.GetFiles(GetPropertyDataPath(), $"*{logFileExt}").OrderByDescending(d => new FileInfo(d).CreationTime);
            // ToDo: Verify that the list is coming in expected order
            if (dbFiles.Count() < 3)
                return todaysFileName; // If less than 3 log files, then skip trimming old log files
            int qtyOldFilesSkipped = 0;
            int qtyFilesDeleted = 0;
            foreach (string dbFile in dbFiles)
            {
                if (dbFile == todaysFileName || dbFile == yesterdayFileName || dbFile == dayBeforeYesterdayFileName)
                {
                    ++qtyOldFilesSkipped;
                    continue;
                }
                if (!dbFile.EndsWith(logFileExt))
                    continue;
                ++qtyFilesDeleted;
                // The following logic is so that it tries to keep old backups if they're the last 3 backups in the file system
                if (qtyFilesDeleted < 2 || qtyOldFilesSkipped == 3)
                    File.Delete(dbFile);
            }
            return todaysFileName;
        }
        public void SetImageFolderWatcher()
        {
            string masterImgPath = $"{GetEmulatorsBasePath()}\\{Properties.Settings.Default.imageSubFolderName}";
            if (Properties.Settings.Default.WatchImageFolderForChanges)
            {
                if (lastImageFolderWatch.Length > 0 && lastImageFolderWatch != masterImgPath && watcher_ImagerFolder != null)
                {
                    watcher_ImagerFolder.Dispose();
                    watcher_ImagerFolder = null;
                }
                if (masterImgPath.Length > 0 && Directory.Exists(masterImgPath) && lastImageFolderWatch != masterImgPath) 
                { 
                    watcher_ImagerFolder = new FileSystemWatcher(masterImgPath);
                    watcher_ImagerFolder.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite;
                    watcher_ImagerFolder.Changed += Watcher_OnChanged;
                    watcher_ImagerFolder.Created += Watcher_OnCreated;
                    watcher_ImagerFolder.Deleted += Watcher_OnDeleted;
                    watcher_ImagerFolder.Renamed += Watcher_OnRenamed;
                    watcher_ImagerFolder.Filter = "*.*";
                    watcher_ImagerFolder.IncludeSubdirectories = true;
                    watcher_ImagerFolder.EnableRaisingEvents = true;
                    lastImageFolderWatch = masterImgPath;
                }
            }
            else if(watcher_ImagerFolder != null)
            {
                watcher_ImagerFolder.Dispose();
                watcher_ImagerFolder = null;
            }
        }
        public void WatcherCreated(FileSystemEventArgs e)
        {
            InfoLogging($"Created: {e.FullPath}");
            string ext = $"*{Path.GetExtension(e.FullPath)}";
            if (SUPPORTED_IMAGE_FILES.Contains(ext)) 
                AddImagePathToDb(e.FullPath, true, false, false, false,false, true);
        }
        public void WatcherRenamed(RenamedEventArgs e)
        {
            InfoLogging($"Renamed:");
            InfoLogging($"    Old: {e.OldFullPath}");
            InfoLogging($"    New: {e.FullPath}");
            string ext = $"*{Path.GetExtension(e.FullPath)}";
            if (SUPPORTED_IMAGE_FILES.Contains(ext) && !e.OldFullPath.Equals(e.FullPath,StringComparison.OrdinalIgnoreCase))
            {
                db.DeleteImage(e.OldFullPath);
                AddImagePathToDb(e.FullPath, true, false, false, false, false, true);
            }
        }
        public void WatcherDeleted(FileSystemEventArgs e)
        {
            InfoLogging($"Deleted: {e.FullPath}");
            db.DeleteImage(e.FullPath);
        }
        public void WatcherChanged(FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            InfoLogging($"Changed: {e.FullPath}");
        }
        private void BrowseGameWikipediaPage()
        {
            Rom rom = GetSelectedROM();
            if (rom == null)
                return;
            if (rom.WikipediaURL != null && rom.WikipediaURL.Length > 0)
                System.Diagnostics.Process.Start(rom.WikipediaURL);
            else if (
                MessageBox.Show($"There's no assigned Wikipedia URL in the database for game {rom.Title}.\nUse the Edit ROM's Detail option to add associated Wikipedia URL.\nWould you like to google this game title for a Wikipedia URL?",
                    "No Wikipedia URL", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string title = System.Text.Encodings.Web.UrlEncoder.Default.Encode(rom.Title);
                Clipboard.SetText(rom.Title);
                System.Diagnostics.Process.Start($"https://www.google.com/search?q={title}+site:wikipedia.org");
            }
        }
        private void DeleteROM()
        {
            Rom rom = GetSelectedROM();
            if (rom == null)
                return;
            if (IsKeyPress(System.Windows.Forms.Keys.Control) ||  // If Alt key is held while selecting deletion option, than do silent delete.
                MessageBox.Show($"Are you sure you want to DELETE ROM with title\n'{rom.Title}'\nand having file name\n'{rom.FilePath}'", "Delete ROM", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                File.Delete(rom.FilePath);
                db.Delete(rom);
                DeleteImageList(rom.System);
            }
        }
        private void CopyTitleToClipboard()
        {
            Rom rom = GetSelectedROM();
            if (rom == null)
                return;
            Clipboard.SetText(rom.Title);
        }
        private void RenameROM()
        {
            Rom rom = GetSelectedROM();
            if (rom == null)
                return;
            string ext = Path.GetExtension(rom.FilePath);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"ROM File (*{ext})|*{ext}|All Files (*.*)|*.*";
            saveFileDialog.Title = "Enter new ROM file name. Warn: New file name will automatically change Title field.";
            saveFileDialog.FileName = rom.FilePath;
            saveFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(rom.FilePath);
            saveFileDialog.CheckFileExists = false;
            DialogResult results = saveFileDialog.ShowDialog();
            if (results == DialogResult.Cancel)
                return;
            if (saveFileDialog.FileName == "")
            {
                ErrorMessage($"Entered invalid file name for ROM file\n'{rom.Title}'", "Invalid Name!!!", "RenameROM", "User selected empty filename");
                return;
            }
            if (saveFileDialog.FileName.Equals(rom.FilePath))
            {
                WarnMessage($"Nothing to do, because the new file name is the same as the old file name:\n{saveFileDialog.FileName}", "Same Name");
                return;
            }
            bool needToDelRomFromDbBeforeUpdate = File.Exists(saveFileDialog.FileName);
            File.Move(rom.FilePath, saveFileDialog.FileName);
            if (needToDelRomFromDbBeforeUpdate)
                db.DeleteRom(saveFileDialog.FileName);
            db.Delete(rom); // Do this to make sure old record is remove before insert
            rom.FilePath = saveFileDialog.FileName;
            db.Update(rom, true);
            MessageBox.Show($"ROM '{rom.Title}' renamed to '{saveFileDialog.FileName}'.");
            DeleteImageList(rom.System);
        }
        private void CreateChecksumForImagesAllSystem(bool withMissingChecksumOnly = false)
        {
            List<GameImage> gameImages = null;
            string where = "";
            if (withMissingChecksumOnly)
                where = " WHERE Checksum = '' OR Checksum IS NULL ";
            using (new CursorWait(taskbarProcess, progressBar_BottomRight))
            {
                if (!db.Get(ref gameImages, where))
                    return;
                ResetWorkStatus(gameImages.Count);
                foreach (GameImage gameImage in gameImages)
                {
                    if (DidCancelButtonGetPressed(true))
                    {
                        SendStatus($"CreateChecksumForImagesAllSystem({withMissingChecksumOnly}) cancelled with only processing {qtyProcess} of {gameImages.Count} images.", true);
                        return;
                    }
                    IncProgress();
                    ++qtyTotalProcess;
                    SendStatus($"Processing GameImage {gameImage.NameSimplified}; {qtyProcess} out of {gameImages.Count}", true);
                    string Checksum = gameImage.Checksum;
                    gameImage.Checksum = GetChecksumStr(gameImage.FilePath);
                    if (!gameImage.Checksum.Equals(Checksum))
                    {
                        SendStatus($"Updating GameImage Title={gameImage.Title}; Checksum={gameImage.Checksum}; {qtyProcess} out of {gameImages.Count}", true);
                        ++qtyChanges;
                        ++qtyTotalChanges;
                        db.Update(gameImage);
                    }
                }
                SendStatus($"Updated {qtyChanges} GameImage's in DB out of {gameImages.Count}", true);
            }
        }
        private void CreateChecksumForRomsAllSystem(bool withMissingChecksumOnly = false)
        {
            List<Rom> roms = null;
            string where = "";
            if (withMissingChecksumOnly)
                where = " WHERE Checksum = '' OR Checksum IS NULL ";
            using (new CursorWait(taskbarProcess, progressBar_BottomRight))
            {
                if (!db.Get(ref roms, where))
                    return;
                ResetWorkStatus(roms.Count);
                foreach (Rom rom in roms)
                {
                    if (DidCancelButtonGetPressed(true))
                    {
                        SendStatus($"CreateChecksumForRomsAllSystem({withMissingChecksumOnly}) cancelled with only processing {qtyProcess} of {roms.Count} images.", true);
                        return;
                    }
                    IncProgress();
                    ++qtyTotalProcess;
                    SendStatus($"Processing Rom {rom.NameSimplified}; {qtyProcess} out of {roms.Count}", true);
                    string Checksum = rom.Checksum;
                    rom.Checksum = GetChecksumStr(rom.FilePath);
                    if (!rom.Checksum.Equals(Checksum))
                    {
                        SendStatus($"Updating Rom Title={rom.Title}; Checksum={rom.Checksum}; {qtyProcess} out of {roms.Count}", true);
                        ++qtyChanges;
                        ++qtyTotalChanges;
                        db.Update(rom);
                    }
                }
                SendStatus($"Updated {qtyChanges} Rom's in DB out of {roms.Count}", true);
            }
        }
        private void RemoveImagesMissingInFilesystem(string where = "", bool isMainThread = true)
        {
            List<GameImage> gameImages = null;
            using (new CursorWait(taskbarProcess, progressBar_BottomRight))
            {
                if (!db.Get(ref gameImages, where))
                    return;
                ResetWorkStatus(gameImages.Count, isMainThread);
                foreach (GameImage gameImage in gameImages)
                {
                    if (DidCancelButtonGetPressed(isMainThread))
                    {
                        SendStatus($"RemoveImagesMissingInFilesystem cancelled with only processing {qtyProcess} of {gameImages.Count} images.", isMainThread);
                        return;
                    }
                    IncProgress();
                    SendStatus($"Processing GameImage {gameImage.NameSimplified}; {qtyProcess} out of {gameImages.Count}", isMainThread);
                    if (!File.Exists(gameImage.FilePath))
                    {
                        SendStatus($"Removing from DB GameImage '{gameImage.Title}', because missing file '{gameImage.FilePath}'; {qtyProcess} out of {gameImages.Count}", isMainThread);
                        ++qtyChanges;
                        db.Delete(gameImage);
                    }
                }
                SendStatus($"Updated {qtyChanges} GameImage's in DB out of {gameImages.Count}", isMainThread);
            }
        }
        private void RemoveRomsMissingInFilesystem(string where = "", bool isMainThread = true)
        {
            List<Rom> rom_list = new List<Rom>();
            Dictionary<int, string> systemDirAndID = GetSystemDirAndID();
            using (new CursorWait(taskbarProcess, progressBar_BottomRight))
            {
                if (!db.Get(ref rom_list, where))
                    return;
                ResetWorkStatus(rom_list.Count, isMainThread);
                foreach (Rom rom in rom_list)
                {
                    if (DidCancelButtonGetPressed(isMainThread))
                    {
                        SendStatus($"RemoveRomsMissingInFilesystem cancelled with only processing {qtyProcess} of {rom_list.Count} ROM's.", isMainThread);
                        return;
                    }
                    IncProgress();
                    SendStatus($"Processing ROM {rom.Title}; {qtyProcess} out of {rom_list.Count}", isMainThread);
                    string imagePath = GetImageIndexByName(rom.NameSimplified, rom.NameOrg, rom.Title, rom.Compressed, GetDictItem(ref systemDirAndID, rom.System));
                    if (!File.Exists(rom.FilePath))
                    {
                        SendStatus($"Removing from DB rom '{rom.Title}', because missing file '{rom.FilePath}'; {qtyProcess} out of {rom_list.Count}", isMainThread);
                        ++qtyChanges;
                        db.Delete(rom);
                    }
                }
                SendStatus($"Updated {qtyChanges} ROM's in DB out of {rom_list.Count}", isMainThread);
                if (qtyChanges > 0)
                    BeginInvoke(new MyDelegateForm_Main(DelegateCreateCacheForDisplaySystemIcons), GetDelegateAction(this, true));
            }
        }
        private void JumpToBrowser(string link, bool copyTitleToClipboard = false)
        {
            Rom rom = GetSelectedROM();
            Dictionary<int, string> systemDirAndID = GetSystemNamesAndID();
            if (rom == null)
                return;
            string title = System.Text.Encodings.Web.UrlEncoder.Default.Encode(rom.Title);
            string system = GetDictItem(ref systemDirAndID, rom.System);
            if (copyTitleToClipboard)
                Clipboard.SetText(rom.Title);
            link = link.Replace("{title}", title).Replace("{system}", system);
            System.Diagnostics.Process.Start(link);
        }
        private void OpenFileLocation()
        {
            Rom rom = GetSelectedROM();
            if (rom == null)
                return;
            if (File.Exists(rom.FilePath))
                Process.Start("explorer.exe", $"/select, \"{rom.FilePath}\"");
            else
                ErrorMessage($"ROM file does not exist: {rom.FilePath}", "File does NOT exist!!", "OpenFileLocation", $"Missing ROM file {rom.FilePath}");
        }
        private void AddRomToFavorite()
        {
            Rom rom = GetSelectedROM();
            if (rom == null)
                return;
            rom.Favorite = true;
            db.Update(rom);
        }
        private void ImportImagesFromDir(bool confirmationPrompt, bool allSystems = false)
        {
            SelectFolderDialog fbd = new SelectFolderDialog();
            fbd.SelectedPath = Properties.Settings.Default.lastDirectoryUserSelected;
            fbd.Description = "Enter path to import images from..";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.lastDirectoryUserSelected = fbd.SelectedPath;
                using (new CursorWait())
                {
                    int SystemID = GetSystemIndex(toolStripComboBoxSystem.Text);
                    List<GameSystem> gameSystems = null;
                    db.Get(ref gameSystems);
                    foreach (GameSystem gameSystem in gameSystems)
                    {
                        if (allSystems == false && gameSystem.ID != SystemID)
                            continue;
                        GetImages(fbd.SelectedPath, true, true, SearchOption.AllDirectories, true, true, false, gameSystem, confirmationPrompt);
                    }
                    InfoMessage($"Image scan complete for path {fbd.SelectedPath}.");
                }
            }
        }
        private void ImageSearchSelectedDir(bool findMatchesForRomsWithMissingImages = false, bool confirmationPrompt = false)
        {
            SelectFolderDialog fbd = new SelectFolderDialog();
            fbd.SelectedPath = Properties.Settings.Default.lastDirectoryUserSelected;
            fbd.Description = "Enter path to search images.";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.lastDirectoryUserSelected = fbd.SelectedPath;
                GetImagesAndWait(fbd.SelectedPath, findMatchesForRomsWithMissingImages == false);
                if (findMatchesForRomsWithMissingImages)
                    SearchMatchingImage(SearchAlgorithm.CloseMatch, confirmationPrompt, false);
            }
        }
        private void AssignClipboardImage(bool overWriteFile = false)
        {
            IDataObject d = Clipboard.GetDataObject();
            if (d.GetDataPresent(DataFormats.Bitmap))
            {
                Rom rom = GetSelectedROM();
                if (rom != null)
                {
                    System.Drawing.Image image = Clipboard.GetImage();
                    string filename = $"{GetEmulatorsBasePath()}\\{Properties.Settings.Default.imageSubFolderName}\\{rom.Title}.jpg";
                    if (overWriteFile || !File.Exists(filename))
                        image.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                    AddImagePathToDb(filename, true, false, false, false, false, true);
                }
            }
        }
        private void ChangeAssignedImage()
        {
            Rom rom = GetSelectedROM();
            if (rom == null)
                return;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"Image File (*.png,*.jpg,*.jpeg,*.bmp,*.tif)|*.png;*.jpg;*.jpeg;*.bmp;*.tif|All Files (*.*)|*.*";
            saveFileDialog.Title = $"Select new image file to assign for ROM '{rom.Title}'";
            saveFileDialog.FileName = rom.ImagePath;
            saveFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(rom.ImagePath);
            string romSystemImageDir = GetGameSystemImagePath(rom.System);
            if (!saveFileDialog.InitialDirectory.Contains(romSystemImageDir, StringComparison.OrdinalIgnoreCase) && lastDirSelected.Length > 0)
                saveFileDialog.InitialDirectory = lastDirSelected;
            if (rom.ImagePath.Contains(DEFAULTIMAGEFILENAME))
            {
                saveFileDialog.FileName = $"{rom.Title}";
                string jpgFile = $"{saveFileDialog.InitialDirectory}\\{saveFileDialog.FileName}.jpg";
                if (File.Exists(jpgFile))
                    saveFileDialog.FileName = jpgFile;
            }
            saveFileDialog.CheckFileExists = true;
            saveFileDialog.OverwritePrompt = false;
            DialogResult results = saveFileDialog.ShowDialog();
            if (results == DialogResult.Cancel)
                return;
            if (saveFileDialog.FileName == "")
            {
                ErrorMessage("Entered invalid file name.", "Invalid Name!!!", "myListViewContextMenu_ChangeAssignedImage_Click");
                return;
            }
            if (saveFileDialog.FileName.Equals(rom.ImagePath, StringComparison.OrdinalIgnoreCase))
            {
                WarnMessage($"Nothing to do, because the new file name is the same as the old file name:\n{saveFileDialog.FileName}", "Same Name");
                return;
            }
            rom.ImagePath = saveFileDialog.FileName;
            lastDirSelected = Path.GetDirectoryName(rom.ImagePath);
            db.Update(rom);
            if (!gavePreviousWarningOnImageChangeNotTakeAffect)
                MessageBox.Show($"ROM '{rom.Title}' associated image changed. The change will not be seen on the list until restarting GameLauncher or until changing game console selection.");
            gavePreviousWarningOnImageChangeNotTakeAffect = true;
            DeleteImageList(rom.System);
        }
        public enum SearchAlgorithm
        {
            ExactMatch      = 30,   // 100% accuracy
            CloseMatch      = 10,   // 90% accuracy
            SimilarMatch    = 3,    // 80% accuracy
            AnyPartMatch    = 2,    // 30% accuracy
            FarMatch        = 1,    // 20% accuracy
        }
        private void SearchMatchingImage(SearchAlgorithm searchAlgorithm, bool confirmationPrompt = true, bool isMainThread = true)
        {
            List<Rom> rom_list = null;
            Dictionary<int, string> systemDirAndID = GetSystemDirAndID();
            using (new CursorWait(taskbarProcess, progressBar_BottomRight))
            {
                using (new TempConnection(true))
                {
                    List<AlternateNames> alternateNames = null;
                    if (!db.Get(ref rom_list, $" WHERE {GetWhereMissingImage()}"))
                        return;
                    List<GameImage> gameImages = null;
                    db.Get(ref gameImages);
                    ResetWorkStatus(rom_list.Count, isMainThread);
                    // ToDo: Add logic to search for any word in Rom.Title that's in image title
                    foreach (Rom rom in rom_list)
                    {
                        if (DidCancelButtonGetPressed(isMainThread))
                        {
                            SendStatus($"SearchMatchingImage cancelled with only processing {qtyProcess} of {rom_list.Count} ROM's.", isMainThread);
                            Db.CloseAlternateNames();
                            Db.CloseImageExternal();
                            return;
                        }
                        IncProgress();
                        ++qtyTotalProcess;
                        SendStatus($"Processing ROM {rom.Title}; {qtyProcess} out of {rom_list.Count}", isMainThread);
                        string imagePath = GetImageIndexByName(rom.NameSimplified, rom.NameOrg, rom.Title, rom.Compressed, GetDictItem(ref systemDirAndID, rom.System));
                        if (IsEmptyOrNull(imagePath))
                        {
                            string sql = CreateSqlQueryFindSimilarNames(rom.Title, "FilePath", "Title", "Images");
                            if (!IsEmptyOrNull(sql))
                                imagePath = GetFirstColStr(sql);
                        }
                        int diffFactor = (int)searchAlgorithm;
                        bool doImportImage = false;
                        int MatchDiff = 99999;
                        int MaxDiff = rom.Title.Length > diffFactor ? (rom.Title.Length / diffFactor) : 1;
                        float accuracy = 0f;
                        float altNameAccuracy = 0f;
                        string fieldDifference = "";
                        string alternateNameForThisRom = "";
                        bool foundAllRomLettersInImageLetters = false;
                        if (IsEmptyOrNull(imagePath))
                        {
                            MatchDiff = rom.Title.Length * 2;
                            foreach (GameImage image in gameImages)
                            {
                                int diff = StrCompare.GetDifference(rom.Title, image.Title);
                                if (searchAlgorithm == SearchAlgorithm.ExactMatch && diff != 0)
                                    continue;
                                if (diff <= MaxDiff && diff < MatchDiff)
                                {
                                    MatchDiff = diff;
                                    imagePath = image.FilePath;
                                    fieldDifference = "Title";
                                    accuracy = (float)(rom.Title.Length - MaxDiff) / (float)rom.Title.Length;
                                    if (diff == 0)
                                        break;
                                }
                            }
                            if (IsEmptyOrNull(imagePath) && alternateNames != null)
                            {
                                foreach (AlternateNames alternateName in alternateNames)
                                {
                                    int diff = StrCompare.GetDifference(rom.Title, alternateName.Title);
                                    if (searchAlgorithm == SearchAlgorithm.ExactMatch && diff != 0)
                                        continue;
                                    if (diff <= MaxDiff && diff < MatchDiff)
                                    {
                                        accuracy = (float)(alternateName.Title.Length - MaxDiff) / (float)alternateName.Title.Length;
                                        string default_Title = ConvertToTitle(alternateName.DefaultName);
                                        int alt_MaxDiff = default_Title.Length > diffFactor ? (default_Title.Length / diffFactor) : 1;
                                        foreach (GameImage image in gameImages)
                                        {
                                            int diff_alt = StrCompare.GetDifference(default_Title, image.Title);
                                            if (searchAlgorithm == SearchAlgorithm.ExactMatch && diff_alt != 0)
                                                continue;
                                            if (diff_alt <= alt_MaxDiff && diff_alt < MatchDiff)
                                            {
                                                MatchDiff = diff_alt;
                                                imagePath = image.FilePath;
                                                fieldDifference = "Alt-Title-T";
                                                altNameAccuracy = (float)(default_Title.Length - alt_MaxDiff) / (float)default_Title.Length;
                                                alternateNameForThisRom = alternateName.DefaultName;
                                                if (diff_alt == 0)
                                                    break;
                                            }
                                        }
                                        string default_NameSimplified = ConvertToNameSimplified(alternateName.DefaultName);
                                        alt_MaxDiff = 1;
                                        foreach (GameImage image in gameImages)
                                        {
                                            int diff_alt = StrCompare.GetDifference(default_NameSimplified, image.NameSimplified);
                                            if (searchAlgorithm == SearchAlgorithm.ExactMatch && diff_alt != 0)
                                                continue;
                                            if (diff_alt <= alt_MaxDiff && diff_alt < MatchDiff)
                                            {
                                                MatchDiff = diff_alt;
                                                imagePath = image.FilePath;
                                                fieldDifference = "Alt-NameSimplified-T";
                                                altNameAccuracy = (float)(default_NameSimplified.Length - alt_MaxDiff) / (float)default_NameSimplified.Length;
                                                alternateNameForThisRom = alternateName.DefaultName;
                                                if (diff_alt == 0)
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (IsEmptyOrNull(imagePath))
                        {
                            MaxDiff = rom.NameSimplified.Length > diffFactor ? (rom.NameSimplified.Length / diffFactor) : 1;
                            MatchDiff = rom.NameSimplified.Length * 2;
                            foreach (GameImage image in gameImages)
                            {
                                int diff = StrCompare.GetDifference(rom.NameSimplified, image.NameSimplified);
                                if (searchAlgorithm == SearchAlgorithm.ExactMatch && diff != 0)
                                    continue;
                                if (diff <= MaxDiff && diff < MatchDiff)
                                {
                                    MatchDiff = diff;
                                    imagePath = image.FilePath;
                                    fieldDifference = "NameSimplified";
                                    accuracy = (float)(rom.NameSimplified.Length - MaxDiff) / (float)rom.NameSimplified.Length;
                                    if (diff == 0)
                                        break;
                                }
                            }
                            if (IsEmptyOrNull(imagePath) && alternateNames != null)
                            {
                                foreach (AlternateNames alternateName in alternateNames)
                                {
                                    int diff = StrCompare.GetDifference(rom.NameSimplified, alternateName.NameSimplified);
                                    if (searchAlgorithm == SearchAlgorithm.ExactMatch && diff != 0)
                                        continue;
                                    if (diff <= MaxDiff && diff < MatchDiff)
                                    {
                                        accuracy = (float)(alternateName.NameSimplified.Length - MaxDiff) / (float)alternateName.NameSimplified.Length;
                                        string default_Title = ConvertToTitle(alternateName.DefaultName);
                                        int alt_MaxDiff = default_Title.Length > diffFactor ? (default_Title.Length / diffFactor) : 1;
                                        foreach (GameImage image in gameImages)
                                        {
                                            int diff_alt = StrCompare.GetDifference(default_Title, image.Title);
                                            if (searchAlgorithm == SearchAlgorithm.ExactMatch && diff_alt != 0)
                                                continue;
                                            if (diff_alt <= alt_MaxDiff && diff_alt < MatchDiff)
                                            {
                                                MatchDiff = diff_alt;
                                                imagePath = image.FilePath;
                                                fieldDifference = "Alt-Title-S";
                                                altNameAccuracy = (float)(default_Title.Length - alt_MaxDiff) / (float)default_Title.Length;
                                                alternateNameForThisRom = alternateName.DefaultName;
                                                if (diff_alt == 0)
                                                    break;
                                            }
                                        }
                                        string default_NameSimplified = ConvertToNameSimplified(alternateName.DefaultName);
                                        alt_MaxDiff = default_NameSimplified.Length > diffFactor ? (default_NameSimplified.Length / diffFactor) : 1;
                                        foreach (GameImage image in gameImages)
                                        {
                                            int diff_alt = StrCompare.GetDifference(default_NameSimplified, image.NameSimplified);
                                            if (searchAlgorithm == SearchAlgorithm.ExactMatch && diff_alt != 0)
                                                continue;
                                            if (diff_alt <= alt_MaxDiff && diff_alt < MatchDiff)
                                            {
                                                MatchDiff = diff_alt;
                                                imagePath = image.FilePath;
                                                fieldDifference = "Alt-NameSimplified-S";
                                                altNameAccuracy = (float)(default_NameSimplified.Length - alt_MaxDiff) / (float)default_NameSimplified.Length;
                                                alternateNameForThisRom = alternateName.DefaultName;
                                                if (diff_alt == 0)
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (IsEmptyOrNull(imagePath))
                        {
                            List<GameImage> gameImagesExtern = GetCloseMatchImageList(rom.Title, "Title", Db.connection_ImageExternal);
                            if (gameImagesExtern != null)
                                foreach (GameImage gameImage in gameImagesExtern) 
                                {
                                    int diff = StrCompare.GetDifference(rom.Title, gameImage.Title);
                                    if (searchAlgorithm == SearchAlgorithm.ExactMatch && diff != 0)
                                        continue;
                                    if (diff <= MaxDiff && diff < MatchDiff)
                                    {
                                        MatchDiff = diff;
                                        imagePath = gameImage.FilePath;
                                        fieldDifference = "Ext-Title";
                                        accuracy = (float)(rom.Title.Length - MaxDiff) / (float)rom.Title.Length;
                                        if (diff == 0)
                                            break;
                                    }
                                }
                            if (IsEmptyOrNull(imagePath))
                            {
                                string foundAllRomLettersInImageLetters_imagePath = null;
                                int foundAllRomLettersInImageLetters_MatchDiff = 99999;
                                float foundAllRomLettersInImageLetters_accuracy = 0f;
                                string foundAllRomLettersInImageLetters_fieldDifference = "";
                                int foundAllRomLettersInImageLetters_MaxDiff = 0;
                                gameImagesExtern = GetCloseMatchImageList(rom.NameSimplified, "NameSimplified", Db.connection_ImageExternal);
                                if (gameImagesExtern != null)
                                {
                                    foreach (GameImage gameImage in gameImagesExtern)
                                    {
                                        int diff = StrCompare.GetDifference(rom.NameSimplified, gameImage.NameSimplified);
                                        if (searchAlgorithm == SearchAlgorithm.ExactMatch && diff != 0)
                                            continue;
                                        if (diff <= MaxDiff && diff < MatchDiff)
                                        {
                                            MatchDiff = diff;
                                            imagePath = gameImage.FilePath;
                                            fieldDifference = "X-NameSimplified";
                                            accuracy = (float)(rom.NameSimplified.Length - MaxDiff) / (float)rom.NameSimplified.Length;
                                            if (diff == 0)
                                                break;
                                        }
                                        else if (confirmationPrompt && gameImage.NameSimplified.Length > rom.NameSimplified.Length && diff <= gameImage.NameSimplified.Length - rom.NameSimplified.Length && diff < foundAllRomLettersInImageLetters_MatchDiff)
                                        {
                                            foundAllRomLettersInImageLetters_MatchDiff = diff;
                                            foundAllRomLettersInImageLetters_imagePath = gameImage.FilePath;
                                            foundAllRomLettersInImageLetters_fieldDifference = "X-NameSimplified#";
                                            foundAllRomLettersInImageLetters_accuracy = (float)(gameImage.NameSimplified.Length - foundAllRomLettersInImageLetters_MatchDiff) / (float)gameImage.NameSimplified.Length;
                                            foundAllRomLettersInImageLetters_MaxDiff = gameImage.NameSimplified.Length - rom.NameSimplified.Length;
                                        }
                                    }
                                    if (IsEmptyOrNull(imagePath) && !IsEmptyOrNull(foundAllRomLettersInImageLetters_imagePath))
                                    {
                                        MatchDiff = foundAllRomLettersInImageLetters_MatchDiff;
                                        imagePath = foundAllRomLettersInImageLetters_imagePath;
                                        fieldDifference = foundAllRomLettersInImageLetters_fieldDifference;
                                        accuracy = foundAllRomLettersInImageLetters_accuracy;
                                        MaxDiff = foundAllRomLettersInImageLetters_MaxDiff;
                                        foundAllRomLettersInImageLetters = true;
                                    }
                                }
                            }
                            if (!IsEmptyOrNull(imagePath))// If image found in external image DB, than need to import the image after confirmation
                                doImportImage = true;
                        }
                        bool phraseMatch = false;
                        if (IsEmptyOrNull(imagePath) && searchAlgorithm != SearchAlgorithm.ExactMatch && StrCompare.GetWordCount(rom.Title) >= PHRASE_MATCH_MIN_WORDS)
                        {
                            MatchDiff = rom.Title.Length * 2;
                            foreach (GameImage image in gameImages)
                            {
                                int diff = StrCompare.GetPhraseDifference(rom.Title, image.Title);
                                if (diff < 2 && diff < MatchDiff)
                                {
                                    MatchDiff = diff;
                                    imagePath = image.FilePath;
                                    phraseMatch = true;
                                    if (diff == 0)
                                        break;
                                }
                            }
                        }
                        if (imagePath.Length > 0)
                        {
                            bool doChangeRomImagePath = true;
                            if (confirmationPrompt)
                            {
                                doChangeRomImagePath = false;
                                if (MatchDiff > MaxDiff)
                                {
                                    GameImage gameImage = db.Get_GameImage(imagePath);
                                    if (gameImage != null)
                                    {
                                        MaxDiff = rom.Title.Length > diffFactor ? (rom.Title.Length / diffFactor) : 1;
                                        MatchDiff = StrCompare.GetDifference(rom.Title, gameImage.Title);
                                        accuracy = (float)(rom.Title.Length - MaxDiff) / (float)rom.Title.Length;
                                    }
                                }
                                string aliasNameInfo = IsNullOrEmpty(alternateNameForThisRom) ? "" : $", which has an alias name of '{alternateNameForThisRom}'";
                                string accuracyDetails = IsNullOrEmpty(alternateNameForThisRom) ? $"; {fieldDifference} DIFF = {MatchDiff} / MaxDiff = {MaxDiff}" : $" and Alias Accuracy {altNameAccuracy * 100}%  . . . . . {fieldDifference}";
                                string romLettersInImageLettersInfo = foundAllRomLettersInImageLetters ? "; Found rom title letters in image title. !!!" : "";
                                string matchDiffDetails = MatchDiff <= MaxDiff ? (phraseMatch ? $"\nQuantity of words difference = {MatchDiff}" : $"\nMatch Accuracy = {accuracy * 100:00.}%{accuracyDetails}{romLettersInImageLettersInfo}") : "";
                                Form_ConfirmImage form_ConfirmImage = new Form_ConfirmImage(this, imagePath, rom, $"Do you want to set ROM '{rom.Title}'{aliasNameInfo} to image '{Path.GetFileNameWithoutExtension(imagePath)}'?{matchDiffDetails}");
                                form_ConfirmImage.ShowDialog();
                                if (form_ConfirmImage.Quit)
                                    cancelWork = true;
                                doChangeRomImagePath = form_ConfirmImage.Ok;
                            }
                            if (doChangeRomImagePath)
                            {
                                if (doImportImage)
                                {
                                    if (Properties.Settings.Default.AutoConvertImageFilesToJpg)
                                        imagePath = ConvertFileToJpg(imagePath, Properties.Settings.Default.AutoDeleteOldImageFileAfterConversion, false, isMainThread);
                                    string masterImgPath = $"{GetEmulatorsBasePath()}\\{Properties.Settings.Default.imageSubFolderName}";
                                    if (!masterImgPath.EndsWith("\\"))
                                        masterImgPath += "\\";
                                    string destImageDir = GetParent_Parent(rom.FilePath, true);
                                    if (destImageDir.Equals(masterImgPath, StringComparison.OrdinalIgnoreCase))
                                        destImageDir = System.IO.Path.GetDirectoryName(rom.FilePath);
                                    destImageDir = Path.Combine(destImageDir, Properties.Settings.Default.imageSubFolderName);
                                    if (!Directory.Exists(destImageDir))
                                        Directory.CreateDirectory(destImageDir);
                                    string destImageFile = Path.Combine(destImageDir, Path.GetFileName(imagePath));
                                    if (!File.Exists(destImageFile))
                                        File.Copy(imagePath, destImageFile);
                                    imagePath = destImageFile;
                                }
                                rom.ImagePath = imagePath;
                                SendStatus($"Updating ROM {rom.Title} with {rom.ImagePath}; {qtyProcess} out of {rom_list.Count}", isMainThread);
                                ++qtyChanges;
                                ++qtyTotalChanges;
                                db.Update(rom);
                            }
                        }
                    }
                    SendStatus($"Updated {qtyChanges} ROM's in DB out of {rom_list.Count}", isMainThread);
                    if (qtyChanges > 0)
                        BeginInvoke(new MyDelegateForm_Main(DelegateCreateCacheForDisplaySystemIcons), GetDelegateAction(this, true));
                }
            }
        }
        private int FindRomMissingImageWhichMatchesThisImage(string imgFile, string Title, string NameOrg, string NameSimplified, string Compressed)
        {
            int qtyMatches = 0;
            List<Rom> rom_list = new List<Rom>();
            string noImage = $" and ({GetWhereMissingImage()})";
            if (!db.Get(ref rom_list, $"WHERE Title = \"{Title}\" {noImage}"))
                if (!db.Get(ref rom_list, $"WHERE NameOrg = \"{NameOrg}\" {noImage}"))
                    if (!db.Get(ref rom_list, $"WHERE NameSimplified = \"{NameSimplified}\" {noImage}"))
                        db.Get(ref rom_list, $"WHERE Compressed = \"{Compressed}\" {noImage}");
            foreach (Rom rom in rom_list)
            {
                rom.ImagePath = imgFile;
                db.Update(rom);
                ++qtyMatches;
            }
            return qtyMatches;
        }
        private void ExecuteRom(int EmulatorID, int romIndex)
        {
            if (romList.Count == 0)
                return;
            Rom rom = romList[romIndex];
        }
        private void UpdateRomAndImageSubfolder()
        {
            if (Properties.Settings.Default.romSubFolderName.Length < 2)
                Properties.Settings.Default.romSubFolderName = DEFAULT_ROM_SUBFOLDER_NAME;
            if (Properties.Settings.Default.imageSubFolderName.Length < 2)
                Properties.Settings.Default.imageSubFolderName = DEFAULT_IMAGE_SUBFOLDER_NAME;
            romSubFolderName = $"\\{Properties.Settings.Default.romSubFolderName}";
            imageSubFolderName = $"\\{Properties.Settings.Default.imageSubFolderName}";
        }
        private void UpdateSettings()
        {
            if (Properties.Settings.Default.Maximised)
                WindowState = FormWindowState.Maximized;
            if (Properties.Settings.Default.lastDirectoryUserSelected.Length == 0)
                Properties.Settings.Default.lastDirectoryUserSelected = Properties.Settings.Default.EmulatorsBasePath;
            SetMRU();
            SetAdvanceOptions();
            UpdateRomAndImageSubfolder();
        }
        private bool UpdateRom(Rom rom, string Where)
        {
            GameDetails gameDetails = null;
            db.WhereGet(ref gameDetails, Where);
            if (gameDetails != null)
            {
                if (gameDetails.QtyPlayers > 0)
                    rom.QtyPlayers = gameDetails.QtyPlayers;
                if (gameDetails.Year > 0)
                    rom.Year = gameDetails.Year;
                if (gameDetails.Status.Length > 0)
                    rom.Status = gameDetails.Status;
                if (gameDetails.Region.Length > 0)
                    rom.Region = gameDetails.Region;
                if (gameDetails.Developer.Length > 0)
                    rom.Developer = gameDetails.Developer;
                if (gameDetails.ReleaseDate.Length > 0)
                    rom.ReleaseDate = gameDetails.ReleaseDate;
                if (gameDetails.Genre.Length > 0)
                    rom.Genre = gameDetails.Genre;
                if (gameDetails.NotesCore.Length > 0)
                    rom.NotesCore = gameDetails.NotesCore;
                if (gameDetails.NotesUser.Length > 0)
                    rom.NotesUser = gameDetails.NotesUser;
                if (gameDetails.FileFormat.Length > 0)
                    rom.FileFormat = gameDetails.FileFormat;
                if (gameDetails.Version.Length > 0)
                    rom.Version = gameDetails.Version;
                if (gameDetails.Description.Length > 0)
                    rom.Description = gameDetails.Description;
                if (gameDetails.Language.Length > 0)
                    rom.Language = gameDetails.Language;
                if (gameDetails.Rating.Length > 0)
                    rom.Rating = gameDetails.Rating;
                if (gameDetails.Publisher != null && gameDetails.Publisher.Length > 0)
                    rom.Publisher = gameDetails.Publisher;
                if (gameDetails.WikipediaURL != null && gameDetails.WikipediaURL.Length > 0)
                    rom.WikipediaURL = gameDetails.WikipediaURL;
                if (gameDetails.StarRating > 0)
                    rom.StarRating = gameDetails.StarRating;
                if (gameDetails.StarRatingVoteCount > 0)
                    rom.StarRatingVoteCount = gameDetails.StarRatingVoteCount;
                db.Update(rom);
                SendStatus($"Updated ROM {rom.Title}", true, false);
                return true;
            }
            return false;
        }
        public string ConvertFileToJpg(string file, bool deleteOrgFiles, bool overWriteIfTargetExists = false, bool isMainThread = true)
        {
            string returnValue = file;
            bool success = false;
            string jpgFile = file;
            string ext = Path.GetExtension(file);
            if (ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || ext.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
                return file;
            jpgFile = $"{jpgFile.Substring(0, jpgFile.Length - ext.Length)}.jpg";
            try
            {
                if (overWriteIfTargetExists && File.Exists(jpgFile))
                    File.Delete(jpgFile);

                if (File.Exists(jpgFile))
                {
                    success = true;
                    returnValue = jpgFile;
                    deleteOrgFiles = false; // Do not delete original file if not converted by this function.
                }
                else
                {
                    if (ext.Equals(".bmp", StringComparison.OrdinalIgnoreCase))
                        using (Bitmap finalBitmap = new Bitmap(file))
                            finalBitmap.Save(jpgFile, ImageFormat.Jpeg);
                    else
                        using (System.Drawing.Image image = System.Drawing.Image.FromFile(file))
                            image.Save(jpgFile, System.Drawing.Imaging.ImageFormat.Jpeg);
                    if (File.Exists(jpgFile))
                    {
                        success = true;
                        returnValue = jpgFile;
                    }
                    else
                        SendStatus($"Failed to converted file {file} to {jpgFile}...", isMainThread);
                }
                if (deleteOrgFiles && success)
                    File.Delete(file);
            }
            catch
            {
                SendStatus($"Failed to converted file {file} to {jpgFile}...", isMainThread);
            }
            return returnValue;
        }
        public void ConvertFilesToJpg(bool deleteOrgFiles, bool updateDbWithConvertedFiles)
        {// Convert all systems image path
            ResetWorkStatus(); 
            List<string> SystemNames = GetSystemNames(false);
            foreach(string systemName in SystemNames)
            {
                if (DidCancelButtonGetPressed(true))
                {
                    SendStatus($"ConvertFilesToJpg cancelled.", true);
                    return;
                }
                string imageDir = GetGameSystemImagePath(systemName);
                SendStatus($"Converting PNG files in path {imageDir}.", true);
                ConvertFilesToJpg(imageDir, deleteOrgFiles, updateDbWithConvertedFiles);
                DeleteImageList(systemName);
            }
            // Any PNG remaining in the DB are bad image files, so remove them.
            UpdateDB("DELETE FROM Images WHERE FilePath like \"%.png\"");
            foreach(string systemName in SystemNames)
                DisplaySystemIcons(systemName);
            if (qtyTotalChanges > 0)
                InfoMessage($"Processed {qtyTotalProcess} files, and converted {qtyTotalChanges} files.", $"Converted {qtyTotalChanges} Files");
            else
                InfoMessage($"Did not find any files to convert from the {qtyTotalProcess} files processed.");
        }
        public void ConvertFilesToJpg(string targetDir = "", bool deleteOrgFiles = false, bool updateDbWithConvertedFiles = false)
        {
            bool doInfoMessage = targetDir.Length == 0;
            if (doInfoMessage) 
            {
                SelectFolderDialog fbd = new SelectFolderDialog();
                fbd.SelectedPath = Properties.Settings.Default.lastDirectoryUserSelected;
                fbd.Description = "Enter path to convert image PNG files to JPG.";
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                targetDir = fbd.SelectedPath;
                Properties.Settings.Default.lastDirectoryUserSelected = targetDir;
            }
            qtyProcess = 0;
            qtyChanges = 0;
            string[] filesToConvert = GetFilesByExtensions(targetDir, SearchOption.AllDirectories, SUPPORTED_CONVERSION_IMAGE_FILES);
            Dictionary<string, string> filesToChangeInDB = new Dictionary<string, string>();
            List<string> filesToDelete = new List<string>();
            foreach (string file in filesToConvert)
            {
                if (DidCancelButtonGetPressed(true))
                {
                    SendStatus($"ConvertFilesToJpg cancelled with only processing {qtyProcess} of {filesToConvert.Length} processed.", true);
                    return;
                }
                ++qtyProcess;
                ++qtyTotalProcess;
                string jpgFile = file;
                string ext = Path.GetExtension(file);
                jpgFile = $"{jpgFile.Substring(0, jpgFile.Length - ext.Length)}.jpg";
                try
                {
                    if (ext.Equals(".bmp", StringComparison.OrdinalIgnoreCase))
                        using (Bitmap finalBitmap = new Bitmap(file))
                            finalBitmap.Save(jpgFile, ImageFormat.Jpeg);
                    else
                        using (System.Drawing.Image image = System.Drawing.Image.FromFile(file))
                            image.Save(jpgFile, System.Drawing.Imaging.ImageFormat.Jpeg);
                    if (File.Exists(jpgFile))
                    {
                        filesToDelete.Add(file);
                        filesToChangeInDB[file] = jpgFile;
                        ++qtyChanges;
                        ++qtyTotalChanges;
                        SendStatus($"Converted file {file} to {jpgFile}; File {qtyProcess} out of {filesToConvert.Length}", true);
                    }
                    else
                        SendStatus($"Failed to converted file {file} to {jpgFile}...", true);
                }
                catch 
                {
                    SendStatus($"Failed to converted file {file} to {jpgFile}...", true);
                }
            }
            int qtyDeleted = 0;
            if (filesToDelete.Count > 0 && (deleteOrgFiles || MessageBox.Show($"Created {filesToDelete.Count} JPG files. Do you want to delete the original PNG files in folder {targetDir}?", "Delete PNG Files?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                foreach (string fileToDelete in filesToDelete)
                {
                    if (DidCancelButtonGetPressed(true))
                    {
                        SendStatus($"ConvertFilesToJpg cancelled with only {qtyDeleted} deleted out of {filesToDelete.Count}.", true);
                        return;
                    }
                    try
                    {
                        File.Delete(fileToDelete);
                        ++qtyDeleted;
                        SendStatus($"Deleted file {fileToDelete}; Deleted {qtyDeleted} files out of {filesToDelete.Count}", true);
                    }
                    catch 
                    {
                        SendStatus($"Failed to deleted file {fileToDelete}.", true);
                    }
                }
                SendStatus($"Created {qtyChanges} JPG files out of {qtyProcess} PNG, and deleted {qtyDeleted} PNG files in folder {targetDir}.", true);
            }
            else
                SendStatus($"Created {qtyChanges} JPG files out of {qtyProcess} PNG in folder {targetDir}.", true);
            if (filesToDelete.Count > 0)
            {
                if (updateDbWithConvertedFiles || MessageBox.Show($"Do you want to update GameLauncher database to point to the new {filesToDelete.Count} JPG files?", "Update database?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (string fileToChangeInDB in filesToDelete)
                    {
                        UpdateDB($"UPDATE Images SET FilePath = \"{filesToChangeInDB[fileToChangeInDB]}\" WHERE FilePath like \"{fileToChangeInDB}\"");
                        UpdateDB($"UPDATE Roms SET ImagePath  = \"{filesToChangeInDB[fileToChangeInDB]}\" WHERE ImagePath like \"{fileToChangeInDB}\"");
                        SendStatus($"Updated file {fileToChangeInDB} to JPG file {filesToChangeInDB[fileToChangeInDB]}.", true);
                    }
                }
            }
            else if (doInfoMessage)
                InfoMessage($"Did not find any files to convert in path {targetDir}. Processed {qtyTotalProcess} files.", "No files found");
        }
        public void DecompressFiles(string romDir, bool updateDbWithConvertedFiles, bool deleteOrgCompressFile, bool decompressToFolder)
        {
            int qtyTotalDecompressFiles = 0;
            using (new CursorWait(taskbarProcess, progressBar_BottomRight))
            {
                string[] filesToDecompress = new string[0];
                foreach (string ext in SUPPORTED_COMPRESSION_FILE)
                {
                    string[] newSet = Directory.GetFiles(romDir, $"*{ext}");
                    if (newSet != null && newSet.Length > 0)
                        filesToDecompress = filesToDecompress.Concat(newSet).ToArray();
                }
                if (filesToDecompress.Length == 0)
                {
                    if (deleteOrgCompressFile == false)
                        MessageBox.Show($"Found no compress files in folder {romDir}");
                    return;
                }
                Dictionary<string, string> filesToChangeInDB = new Dictionary<string, string>();
                List<string> filesToDelete = new List<string>();
                ResetWorkStatus(filesToDecompress.Length);
                foreach (string file in filesToDecompress)
                {
                    if (DidCancelButtonGetPressed(true))
                    {
                        SendStatus($"DecompressFiles cancelled with only processing {qtyProcess} of {filesToDecompress.Length} processed.", true);
                        return;
                    }
                    IncProgress();
                    string destDir = Path.GetDirectoryName(file);
                    if (decompressToFolder)
                        destDir += $"\\{Path.GetFileNameWithoutExtension(file)}";
                    SendStatus($"Decompressing file '{file}'; {qtyProcess} files out of {filesToDecompress.Length} processed.", true);
                    using (IDecompressArchive archive = CompressArchive.Open(file))
                    {
                        if (archive.ExtractToDirectory(destDir))
                        {
                            bool fileExtractSuccess = false;
                            bool allFileExtracted = true;
                            int qtyFilesInThisCompressFile = 0;
                            string fileToChangeInDB = "";
                            foreach (string name in archive.GetNames())
                            {
                                string destinationPath = Path.GetFullPath(Path.Combine(destDir, name));
                                if (Directory.Exists(destinationPath))
                                    continue;
                                if (File.Exists(destinationPath))
                                {
                                    ++qtyTotalDecompressFiles;
                                    ++qtyFilesInThisCompressFile;
                                    fileExtractSuccess = true;
                                    fileToChangeInDB = destinationPath;
                                }
                                else
                                    allFileExtracted = false;
                            }
                            if (fileExtractSuccess && allFileExtracted)
                            {
                                filesToDelete.Add(file);
                                if (qtyFilesInThisCompressFile == 1 && fileToChangeInDB.Length > 0)
                                    filesToChangeInDB[file] = fileToChangeInDB; // If there's more than 1 file decompress there's no practical way to tell which file to swap in the database for the compress file.
                            }
                        }
                        else
                            SendStatus($"Failed to decompress file '{file}'; {qtyProcess} files out of {filesToDecompress.Length} processed.", true);
                    }
                }
                int qtyDeleted = 0;
                if (filesToDelete.Count > 0 && (deleteOrgCompressFile || MessageBox.Show($"Decompressed {qtyTotalDecompressFiles} files. Do you want to delete the original compressed files in folder {romDir}?", "Delete Compress Files?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    foreach (string fileToDelete in filesToDelete)
                    {
                        if (DidCancelButtonGetPressed(true))
                        {
                            SendStatus($"DecompressFiles cancelled with only {qtyDeleted} deleted out of {filesToDelete.Count}.", true);
                            return;
                        }
                        try
                        {
                            SendStatus($"Deleting file '{fileToDelete}'; {qtyDeleted} files out of {filesToDelete.Count} processed.", true);
                            File.Delete(fileToDelete);
                            ++qtyDeleted;
                        }
                        catch { }
                    }
                    SendStatus($"Decompressed {filesToDelete.Count} files out of {qtyProcess}, and deleted {qtyDeleted} compressed files in folder {romDir}.", true);
                }
                else
                    SendStatus($"Decompressed {filesToDelete.Count} files out of {qtyProcess} in folder {romDir}.", true);
                if (filesToDelete.Count > 0)
                {
                    if (updateDbWithConvertedFiles || MessageBox.Show($"Do you want to update GameLauncher database to point to the new {filesToDelete.Count} decompress files?", "Update database?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (string fileToChangeInDB in filesToDelete)
                            UpdateDB($"UPDATE Roms SET FilePath = \"{filesToChangeInDB[fileToChangeInDB]}\" WHERE FilePath like \"{fileToChangeInDB}\"");
                    }
                }
                else
                    InfoMessage($"Did not find any compress files to decompress in path {romDir}", "No compress files found");
            }
        }
        public void DecompressFiles(bool onlySelectedSystem, bool updateDbWithConvertedFiles = false, bool deleteOrgCompressFile = false, bool decompressToFolder = false)
        {
            string selectedSystem = toolStripComboBoxSystem.Text;
            if (onlySelectedSystem)
            {
                if (selectedSystem == ITEM_ANY)
                    onlySelectedSystem = false;
                else if (selectedSystem == ITEM_FAVORITE)
                {
                    MessageBox.Show($"Can not select this option while '{ITEM_FAVORITE}' is selected for system combo field.");
                    return;
                }
            }
            List<string> SystemNames = GetSystemNames(false);
            foreach (string systemName in SystemNames)
            {
                if (onlySelectedSystem && !systemName.Equals(selectedSystem))
                    continue;
                string romDir = GetGameSystemRomPath(systemName);
                DecompressFiles(romDir, updateDbWithConvertedFiles, deleteOrgCompressFile, decompressToFolder);
            }
        }
        public void CompressFilesInSelectedFolder(bool deleteOrgFiles, bool updateDbWithConvertedFiles, string compressFileType = "zip")
        {// Convert all systems ROM path
            List<string> SystemNames = GetSystemNames(false);
            foreach (string systemName in SystemNames)
            {
                string romDir = GetGameSystemRomPath(systemName);
                CompressFilesInSelectedFolder(compressFileType, romDir, deleteOrgFiles, updateDbWithConvertedFiles);
            }
        }
        public void CompressFilesInSelectedFolder(string compressFileType, string targetDir = "", bool deleteOrgFiles = false, bool updateDbWithConvertedFiles = false)
        {
            if (targetDir.Length == 0)
            {
                SelectFolderDialog fbd = new SelectFolderDialog();
                fbd.SelectedPath = Properties.Settings.Default.lastDirectoryUserSelected;
                fbd.Description = "Enter path to compress files.";
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                targetDir = fbd.SelectedPath;
                Properties.Settings.Default.lastDirectoryUserSelected = targetDir;
            }
            Properties.Settings.Default.lastDirectoryUserSelected = targetDir;
            using (new CursorWait(taskbarProcess, progressBar_BottomRight))
            {
                string[] filesToCompress = new string[0];
                foreach (string ext in validRomTypes)
                {
                    if (SUPPORTED_COMPRESSION_FILE.Contains($".{ext}"))
                        continue;
                    string[] newSet = Directory.GetFiles(targetDir, $"*.{ext}");
                    if (newSet != null && newSet.Length > 0)
                        filesToCompress = filesToCompress.Concat(newSet).ToArray();
                }
                if (filesToCompress.Length == 0)
                {
                    if (deleteOrgFiles == false)
                        MessageBox.Show($"Found no valid ROM type files to compress in folder {targetDir}");
                    return;
                }
                Dictionary<string, string> filesToChangeInDB = new Dictionary<string, string>();
                List<string> filesToDelete = new List<string>();
                ResetWorkStatus(filesToCompress.Length);
                foreach (string file in filesToCompress)
                {
                    if (DidCancelButtonGetPressed(true))
                    {
                        SendStatus($"CompressFilesInSelectedFolder cancelled with only processing {qtyProcess} of {filesToCompress.Length} processed.", true);
                        return;
                    }
                    IncProgress();
                    ++qtyTotalProcess;
                    string archivePath = file;
                    string ext = Path.GetExtension(file);
                    archivePath = $"{archivePath.Substring(0, archivePath.Length - ext.Length)}.{compressFileType}";
                    if (CompressArchive.CompressFile(archivePath, file))
                    {
                        filesToDelete.Add(file);
                        filesToChangeInDB[file] = archivePath;
                        ++qtyChanges;
                        ++qtyTotalChanges;
                        SendStatus($"Compressed file {file} to {archivePath}; File {qtyProcess} out of {filesToCompress.Length}", true);
                    }
                    else
                        SendStatus($"Failed to compressed file {file} to {archivePath}!!!", true);
                }
                int qtyDeleted = 0;
                if (filesToDelete.Count > 0 && (deleteOrgFiles || MessageBox.Show($"Compressed {filesToDelete.Count} files. Do you want to delete the original decompressed files in folder {targetDir}?", "Delete Original Files?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    foreach (string fileToDelete in filesToDelete)
                    {
                        if (DidCancelButtonGetPressed(true))
                        {
                            SendStatus($"CompressFilesInSelectedFolder cancelled with only {qtyDeleted} deleted out of {filesToDelete.Count}.", true);
                            return;
                        }
                        try
                        {
                            File.Delete(fileToDelete);
                            ++qtyDeleted;
                        }
                        catch { }
                    }
                    SendStatus($"Compressed {qtyChanges} files out of {qtyProcess}, and deleted {qtyDeleted} uncompressed files in folder {targetDir}.", true);
                }
                else
                    SendStatus($"Compressed {qtyChanges} files out of {qtyProcess} in folder {targetDir}.", true);
                if (filesToDelete.Count > 0)
                {
                    if (updateDbWithConvertedFiles || MessageBox.Show($"Do you want to update GameLauncher database to point to the new {filesToDelete.Count} compress files?", "Update database?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (string fileToChangeInDB in filesToDelete)
                            UpdateDB($"UPDATE Roms SET FilePath = \"{filesToChangeInDB[fileToChangeInDB]}\" WHERE FilePath like \"{fileToChangeInDB}\"");
                    }
                }
                else
                    InfoMessage($"Did not find any files to convert in path {targetDir}", "No files found");
            }
        }
        private void ConvertDblExtToSingleExt(string targetDir, bool updateDb = false)
        {
            if (IsNullOrEmpty(targetDir))
            {
                ErrorMessage("Can not continue, because ROM path is empty or null.");
                return;
            }
            using (new CursorWait(taskbarProcess, progressBar_BottomRight))
            {
                string[] compressFiles = GetFilesByExtensions(targetDir, SearchOption.AllDirectories, SUPPORTED_COMPRESSION_FILE);
                ResetWorkStatus(compressFiles.Length);
                foreach (string compressFile in compressFiles)
                {
                    if (DidCancelButtonGetPressed(true))
                    {
                        SendStatus($"ConvertDblExtToSingleExt cancelled with only processing {qtyProcess} of {compressFiles.Length} image files.", true);
                        return;
                    }
                    IncProgress();
                    string newFileName = Regex.Replace(compressFile, "\\.[0-9a-zA-Z]{3}(\\.[0-9a-zA-Z]{3})$", "$1");
                    if (!newFileName.Equals(compressFile))
                    {
                        File.Move(compressFile, newFileName);
                        if (File.Exists(newFileName))
                        {
                            ++qtyChanges;
                            SendStatus($"Renamed file {compressFile} to {newFileName}; {qtyProcess} of {compressFiles.Length}", true);
                            if (updateDb) 
                            {
                                Rom rom = db.Get_Rom(compressFile);
                                if (rom != null)
                                {
                                    db.Delete(rom); // Must do this before being able to update it.
                                    rom.FilePath = newFileName;
                                    db.Update(rom, true);
                                }
                            }
                        }
                        else
                        {
                            ++qtyNoChanges;
                            SendStatus($"Failed to rename file {compressFile} to {newFileName}; {qtyProcess} of {compressFiles.Length}", true);
                        }
                    }
                    else
                    {
                        ++qtyNoChanges;
                        SendStatus($"Skipped file {compressFile} because nothing to rename; {qtyProcess} of {compressFiles.Length}", true);
                    }
                }
                SendStatus($"Completed processing {compressFiles.Length} files. Made {qtyChanges} file changes, and no changes to {qtyNoChanges} files.", true);
                InfoMessage($"Completed processing {compressFiles.Length} files. Made {qtyChanges} file changes, and no changes to {qtyNoChanges} files.");
            }
        }
        private void PopulateValidRomTypes()
        {
            if (Properties.Settings.Default.ValidROMTypes.Length < 6)
                Properties.Settings.Default.ValidROMTypes = string.Join(",", VALID_ROM_TYPES);
            Properties.Settings.Default.ValidROMTypes = Properties.Settings.Default.ValidROMTypes.ToLower();
            validRomTypes = new List<string>(Properties.Settings.Default.ValidROMTypes.Split(','));
        }
        private void SetMRU()
        {
            toolStripMenuItem_RecentGames.DropDownItems.Clear();
            List<Mru> mrus = null;
            db.Get(ref mrus);
            if (mrus.Count > 0)
            {
                int count = 0;
                foreach (Mru mru in mrus)
                {
                    ++count;
                    ToolStripMenuItem item = new ToolStripMenuItem(mru.FilePath);
                    item.Tag = mru.FilePath;
                    item.Click += OpenRecentFile;
                    toolStripMenuItem_RecentGames.DropDownItems.Add(item);
                    if (count > Properties.Settings.Default.MaxMRU)
                        break;
                }
            }
            else
                toolStripMenuItem_RecentGames.Enabled = false;

        }
        private bool AddN64RdxToGameDetails(string fileName)
        {
            if (!File.Exists(fileName))
                return false;
            StreamReader sr = new StreamReader(fileName);
            int qtyChanges = 0;
            string line;
            string systemName = "Nintendo64";
            int qtyPlayers = 0;
            string title = "";
            string developer = "";
            string releaseDate = "";
            int Year = 0;
            string genre = "";
            string forceFeedback;
            const string GoodNamePrefix = "Good Name=";
            const string PlayersPrefix = "Players=";
            const string DeveloperPrefix = "Developer=";
            const string ReleaseDatePrefix = "ReleaseDate=";
            const string GenrePrefix = "Genre=";
            const string ForceFeedbackPrefix = "ForceFeedback=";
            while ((line = sr.ReadLine()) != null)
            {
                if (DidCancelButtonGetPressed(true))
                {
                    SendStatus($"AddN64RdxToGameDetails cancelled with only processing {qtyChanges} items.", true);
                    return false;
                }
                if (line.StartsWith(GoodNamePrefix))
                {
                    title = line.Substring(GoodNamePrefix.Length);
                    qtyPlayers = 0;
                    developer = "";
                    releaseDate = "";
                    Year = 0;
                    genre = "";
                }
                else if (line.StartsWith(PlayersPrefix))
                {
                    string qtyPlayerStr = line.Substring(PlayersPrefix.Length);
                    if (qtyPlayerStr.Length > 0 && Char.IsDigit(qtyPlayerStr[0]) && Int32.Parse(qtyPlayerStr) > 0)
                        qtyPlayers = Int32.Parse(qtyPlayerStr);
                }
                else if (line.StartsWith(DeveloperPrefix))
                    developer = line.Substring(DeveloperPrefix.Length);
                else if (line.StartsWith(ReleaseDatePrefix))
                {
                    releaseDate = line.Substring(ReleaseDatePrefix.Length);
                    if (releaseDate.Length > 0 && Char.IsDigit(releaseDate[0]))
                    {
                        string year = Regex.Replace(releaseDate, "([0-9]{4}).*", "$1");
                        // The IsDigit code should not be necessary, but there's a bug in Regex that's letting strings like "199x" get through.
                        if (year.Length == 4 && Char.IsDigit(year[0]) && Char.IsDigit(year[1]) && Char.IsDigit(year[2]) && Char.IsDigit(year[3]))
                            Year = Int32.Parse(year);
                    }
                    else
                        releaseDate = "";
                }
                else if (line.StartsWith(GenrePrefix))
                    genre = line.Substring(GenrePrefix.Length);
                else if (line.StartsWith(ForceFeedbackPrefix))
                {
                    forceFeedback = line.Substring(ForceFeedbackPrefix.Length);
                    title = ConvertToTitle(title);
                    string NameSimplified = ConvertToNameSimplified(title);
                    string Compressed = ConvertToCompress(title);
                    UpdateDB("INSERT OR REPLACE INTO GameDetails " +
                        "(System, Title, QtyPlayers, NameSimplified, Compressed, Developer, ReleaseDate, Genre, Year)" +
                        " values" +
                        $" (\"{systemName}\", \"{title}\", {qtyPlayers}, \"{NameSimplified}\", \"{Compressed}\", \"{developer}\", \"{releaseDate}\", \"{genre}\", {Year});",
                        connection_GameDetails);
                    ++qtyChanges;
                    ++qtyTotalChanges;
                    SendStatus($"Added details for game {systemName}-{title}; Item {qtyChanges}", true, false);
                }
            }
            SendStatus($"Updated {qtyChanges} items in DB for system {systemName}", true);
            return true;
        }
        private bool AddMultiplayerDetailsToGameDetails(string fileName)
        {
            if (!File.Exists(fileName))
                return false;
            StreamReader sr = new StreamReader(fileName);
            int qtyChanges = 0;
            string line;
            string systemName = null;
            int qtyPlayers = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (DidCancelButtonGetPressed(true))
                {
                    SendStatus($"AddMultiplayerDetailsToGameDetails cancelled with only processing {qtyChanges} items.", true);
                    return false;
                }
                if (line.StartsWith(";"))
                {
                    if (systemName == null)
                        systemName = line.Substring(1);
                    else
                    {
                        string qtyPlayerStr = line.Substring(1, 1);
                        if (qtyPlayerStr.Length > 0 && Char.IsDigit(qtyPlayerStr[0]) && Int32.Parse(qtyPlayerStr) > 0)
                            qtyPlayers = Int32.Parse(qtyPlayerStr);
                        else
                            break;
                    }
                    continue;
                }
                string title = ConvertToTitle(line);
                string NameSimplified = ConvertToNameSimplified(title);
                string Compressed = ConvertToCompress(title);
                UpdateDB("INSERT OR REPLACE INTO GameDetails " +
                    "(System, Title, QtyPlayers, NameSimplified, Compressed)" +
                    " values" +
                    $" (\"{systemName}\", \"{title}\", {qtyPlayers}, \"{NameSimplified}\", \"{Compressed}\");",
                    connection_GameDetails);
                ++qtyChanges;
                ++qtyTotalChanges;
            }
            SendStatus($"Updated {qtyChanges} items in DB for system {systemName}", true);
            return true;
        }
        private bool IsSpecificSystemSelected(string system = null)
        {
            if (system == null)
                system = toolStripComboBoxSystem.Text;
            return system != null && system.Length != 0 && system != ITEM_ANY && system != ITEM_FAVORITE;
        }
        private void ChangeTitle()
        {
            Rom rom = GetSelectedROM();
            if (rom == null)
                return;
            InputForm inputForm = new InputForm("                ROM Title:", "Change ROM Title", rom.Title, $"Enter new ROM title for game \"{rom.Title}\"", $"Enter new ROM title for game having file name \"{rom.FilePath}\"");
            inputForm.ShowDialog();
            if (inputForm.Ok)
            {
                rom.Title = inputForm.Value;
                db.Update(rom);
                DeleteImageList(rom.System);
            }
        }
        private void ChangeViewROMDetails()
        {
            Rom rom = GetSelectedROM();
            if (rom == null)
                return;
            Form_RomDetailsEditor romDetails = new Form_RomDetailsEditor(rom, this);
            romDetails.ShowDialog();
            if (romDetails.Ok)
            {
                db.Update(rom, false, true);
                DeleteImageList(rom.System);
            }
        }
        private void AssignPreferredEmulator()
        {
            Rom rom = GetSelectedROM();
            if (rom == null)
                return;
            int preferredEmulator = rom.PreferredEmulatorID > 0 ? rom.PreferredEmulatorID : 1;
            string emulatorExecutable = GetEmulatorExecutable(rom.System, preferredEmulator);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"Emulator Executable File (*.exe)|*.exe|All Files (*.*)|*.*";
            saveFileDialog.Title = "Select preferred emulator executable";
            saveFileDialog.FileName = emulatorExecutable;
            saveFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(emulatorExecutable);
            saveFileDialog.CheckFileExists = true;
            saveFileDialog.OverwritePrompt = false;
            DialogResult results = saveFileDialog.ShowDialog();
            if (results == DialogResult.Cancel)
                return;
            if (saveFileDialog.FileName == "" || !File.Exists(saveFileDialog.FileName))
            {
                ErrorMessage($"Entered invalid file name for emulator", "Invalid Name!!!", "myListViewContextMenu_AssignPreferredEmulator_Click", $"saveFileDialog.FileName={saveFileDialog.FileName}");
                return;
            }
            if (saveFileDialog.FileName.Equals(rom.FilePath, StringComparison.OrdinalIgnoreCase))
            {
                WarnMessage($"Nothing to do, because the new file name is the same as the old file name:\n{saveFileDialog.FileName}", "Same Name");
                return;
            }
            // Check if selected emulator already exists in DB
            int emulatorIndex = -1;
            int lastPopulatedEmulator = -1;
            for (int i = 1; i < 11; i++)
            {
                string sql = $"SELECT EmulatorPath{i} FROM GameSystems WHERE Name like \"{toolStripComboBoxSystem.Text}\"";
                string s = GetFirstColStr(sql);
                if (s != null && s.Length > 0)
                {
                    if (s.Equals(saveFileDialog.FileName, StringComparison.OrdinalIgnoreCase))
                    {
                        emulatorIndex = i;
                        break;
                    }
                    lastPopulatedEmulator = i;
                }
            }
            if (emulatorIndex == -1)
            {// Add emulator to DB
                if (lastPopulatedEmulator == 10)
                {
                    ErrorMessage($"Can not set emulator because there's already 10 emulators associated with game console system {toolStripComboBoxSystem.Text}", "Can not set emulator!!!", "myListViewContextMenu_AssignPreferredEmulator_Click");
                    return;
                }
                emulatorIndex = lastPopulatedEmulator + 1;
                UpdateDB($"UPDATE GameSystems SET EmulatorPath{emulatorIndex} = \"{saveFileDialog.FileName}\" WHERE Name like \"{toolStripComboBoxSystem.Text}\"");
            }
            if (emulatorIndex != -1)
            {
                rom.PreferredEmulatorID = emulatorIndex;
                db.Update(rom);
                InfoMessage($"Emulator updated.", "Success");
            }
        }
        private void ResetAllFilters(bool runFilter = false)
        {
            formInitiated = false;
            toolStripComboBox_FilterRating.Text = ITEM_ANY;
            toolStripComboBox_FilterMaxPlayers.Text = ITEM_ANY;
            toolStripComboBox_FilterStarRating.Text = ITEM_ANY;
            toolStripComboBox_FilterGenre.Text = ITEM_ANY;
            toolStripComboBox_FilterLanguage.Text = ITEM_ANY;
            toolStripComboBox_FilterYearsDecade.Text = ITEM_ANY;
            toolStripComboBox_FilterRegion.Text = ITEM_ANY;
            toolStripComboBox_FilterDeveloper.Text = ITEM_ANY;
            toolStripComboBox_GamesWithWithoutImage.Text = ITEM_ANY;
            toolStripTextBox_Filter.Text = "";
            formInitiated = true;
            if (runFilter)
                FilterRoms();
        }
        public void FilterRoms()
        {
            if (!formInitiated || connection == null)
                return;
            if (toolStripComboBox_FilterRating.Text == CLEA_ALL_FILTERS || toolStripComboBox_GamesWithWithoutImage.Text == CLEA_ALL_FILTERS)
                ResetAllFilters();
            int SystemID = GetSystemIndex(toolStripComboBoxSystem.Text);
            string where = IsSpecificSystemSelected() ? $"WHERE System = {SystemID} " : $"WHERE System >= 0 ";
            if (toolStripComboBox_FilterRating.Text != ITEM_ANY)
                where += $" AND Rating LIKE \"%{toolStripComboBox_FilterRating.Text}%\"";
            if (toolStripComboBox_FilterMaxPlayers.Text != ITEM_ANY)
                where += $" AND QtyPlayers = \"{toolStripComboBox_FilterMaxPlayers.Text}\"";
            if (toolStripComboBox_FilterStarRating.Text != ITEM_ANY)
                where += $" AND StarRating > {toolStripComboBox_FilterStarRating.Text.Substring(0, 1)}";
            if (toolStripComboBox_FilterGenre.Text != ITEM_ANY)
                where += $" AND Genre LIKE \"%{toolStripComboBox_FilterGenre.Text}%\"";
            if (toolStripComboBox_FilterLanguage.Text != ITEM_ANY)
                where += $" AND Language LIKE \"%{toolStripComboBox_FilterLanguage.Text}%\"";
            if (toolStripComboBox_FilterRegion.Text != ITEM_ANY)
            {
                if (toolStripComboBox_FilterRegion.Text == "USA")
                    where += $" AND (Region LIKE \"%USA%\" OR Region like \"%United States%\")";
                else
                    where += $" AND Region LIKE \"%{toolStripComboBox_FilterRegion.Text}%\"";
            }
            if (toolStripComboBox_FilterYearsDecade.Text != ITEM_ANY)
                where += $" AND Year >= {minimum_decade_year[toolStripComboBox_FilterYearsDecade.Text]} and Year <= {maximum_decade_year[toolStripComboBox_FilterYearsDecade.Text]}";
            if (toolStripComboBox_FilterDeveloper.Text != ITEM_ANY)
                where += $" AND Developer LIKE \"%{toolStripComboBox_FilterDeveloper.Text}%\"";
            if (toolStripComboBox_GamesWithWithoutImage.Text == GAMES_WITHOUT_IMAGE)
                where += $" AND (ImagePath = \"\" OR ImagePath = \"{Properties.Settings.Default.DefaultImagePath}\" OR ImagePath IS NULL)";
            else if (toolStripComboBox_GamesWithWithoutImage.Text == GAMES_WITH_IMAGE)
                where += $" AND ImagePath <> \"\" AND ImagePath <> \"{Properties.Settings.Default.DefaultImagePath}\" AND ImagePath IS NOT NULL"; //ToDo: add check if default image
            db.Get(SystemID, ref romList, "", where);
            DisplaySystemIcons(toolStripComboBoxSystem.Text, true, 1, true);
        }
        public int GetNumber(string numStr)
        {
            if (numStr == null)
                return 0;
            numStr = numStr.Trim();
            if (numStr.Length == 0)
                return 0;
            numStr = Regex.Replace(numStr, "[^0-9]*([0-9]+)[^0-9]*", "$1");
            return numStr.Length == 0 ? 0 : Int32.Parse(numStr);
        }
        private bool AddXmlGameDataToGameDetails(string fileName)
        {
            if (!File.Exists(fileName))
                return false;
            string system = $"{GetFileNameWithoutExtensionAndWithoutBin(fileName)}";
            StreamReader sr = new StreamReader(fileName);
            int qtyChanges = 0;
            string line;
            string ApplicationPath = "";
            string Developer = "";
            string Notes = "";
            string Publisher = "";
            string Rating = "";
            string ReleaseDate = "";
            string Status = "";
            string Title = "";
            string Region = "";
            string Genre = "";
            int MaxPlayers = 0;
            string PlayMode = "";
            string CommunityStarRating = "";
            int CommunityStarRatingTotalVotes = 0;
            string Version = ""; // Bad source. Can not trust this data
            int Year = 0;
            string WikipediaURL = "";
            string ID = "";
            while ((line = sr.ReadLine()) != null)
            {
                if (DidCancelButtonGetPressed(true))
                {
                    SendStatus($"AddXmlGameDataToGameDetails cancelled with only processing {qtyChanges} items.", true);
                    return false;
                }
                string tagName = Regex.Replace(line, @"\<([0-9a-zA-Z]+)\>([^\<]*)\<\/[0-9a-zA-Z]+\>", "$1");
                tagName = tagName.Trim(' ');
                string content = Regex.Replace(line, @"\<([0-9a-zA-Z]+)\>([^\<]*)\<\/[0-9a-zA-Z]+\>", "$2");
                content = content.Trim(' ');
                content = content.Replace("\"", "'");
                content = content.Replace("&amp;", "&");
                if (line.Contains("<Game>"))
                {
                    ApplicationPath = "";
                    Developer = "";
                    Notes = "";
                    Publisher = "";
                    Rating = "";
                    ReleaseDate = "";
                    Status = "";
                    Title = "";
                    Region = "";
                    Genre = "";
                    MaxPlayers = 0;
                    PlayMode = "";
                    CommunityStarRating = "";
                    CommunityStarRatingTotalVotes = 0;
                    Version = ""; // Bad source. Can not trust this data
                    Year = 0;
                    WikipediaURL = "";
                    ID = "";
                }
                else if (tagName.Equals("ApplicationPath"))
                    ApplicationPath = content;
                else if (tagName.Equals("Developer"))
                    Developer = content;
                else if (tagName.Equals("Notes"))
                    Notes = content;
                else if (tagName.Equals("Publisher"))
                    Publisher = content;
                else if (tagName.Equals("Rating"))
                    Rating = content;
                else if (tagName.Equals("ReleaseDate"))
                    ReleaseDate = content;
                else if (tagName.Equals("Status"))
                    Status = content;
                else if (tagName.Equals("Title"))
                    Title = content; // Not using
                else if (tagName.Equals("Region"))
                    Region = content;
                else if (tagName.Equals("Genre"))
                    Genre = content;
                else if (tagName.Equals("MaxPlayers"))
                    MaxPlayers = GetNumber(content);
                else if (tagName.Equals("PlayMode"))
                    PlayMode = content;
                else if (tagName.Equals("CommunityStarRating"))
                    CommunityStarRating = content;
                else if (tagName.Equals("CommunityStarRatingTotalVotes"))
                    CommunityStarRatingTotalVotes = GetNumber(content);
                else if (tagName.Equals("Version"))
                    Version = content; // Not using
                else if (tagName.Equals("WikipediaURL"))
                    WikipediaURL = content;
                else if (tagName.Equals("ID"))
                    ID = content;
                else if (line.Contains("</Game>"))
                {
                    if (ReleaseDate.Length > 0 && Char.IsDigit(ReleaseDate[0]))
                    {
                        string year = Regex.Replace(ReleaseDate, "([0-9]{4}).*", "$1");
                        // The IsDigit code should not be necessary, but there's a bug in Regex that's letting strings like "199x" get through.
                        if (year.Length == 4 && Char.IsDigit(year[0]) && Char.IsDigit(year[1]) && Char.IsDigit(year[2]) && Char.IsDigit(year[3]))
                            Year = Int32.Parse(year);
                    }
                    ApplicationPath = ApplicationPath.Trim(' ');
                    ApplicationPath = Path.GetFileName(ApplicationPath);
                    string title = ConvertToTitle(GetFileNameWithoutExtensionAndWithoutBin(ApplicationPath));
                    string NameSimplified = ConvertToNameSimplified(title);
                    string Compressed = ConvertToCompress(title);
                    string sql = "INSERT OR REPLACE INTO GameDetails " +
                        "(System, Title, QtyPlayers, NameSimplified, Compressed, Developer, NotesCore, Publisher, FileName, Rating, " +
                        "ReleaseDate, Year, Region, Genre, StarRating, StarRatingVoteCount, Status, WikipediaURL, ID, PlayMode)" +
                        " values" +
                        $" (\"{system}\", \"{title}\", {MaxPlayers}, \"{NameSimplified}\", \"{Compressed}\", \"{Developer}\", \"{Notes}\", \"{Publisher}\", \"{ApplicationPath}\", \"{Rating}\", " +
                        $"\"{ReleaseDate}\", {Year}, \"{Region}\", \"{Genre}\", {CommunityStarRating}, {CommunityStarRatingTotalVotes}, \"{Status}\", \"{WikipediaURL}\", \"{ID}\", \"{PlayMode}\");";
                    UpdateDB(sql, connection_GameDetails);
                    ++qtyChanges;
                    ++qtyTotalChanges;
                    SendStatus($"Added details for game {system}-{title} in DB; Item {qtyChanges}", true, false);
                }
            }
            SendStatus($"Updated {qtyChanges} items in DB for system {system}", true);
            return true;
        }
        private string Contains(string nameToCheck, List<string> names)
        {
            foreach (var name in names)
            {
                if (nameToCheck.Contains(name, StringComparison.OrdinalIgnoreCase))
                    return name;
            }
            return "";
        }
        private void SetEmulatorExecute(string systemName = null)
        {
            if (systemName == null)
                systemName = toolStripComboBoxSystem.Text;
            string emulatorExecutable = GetEmulatorExecutable(systemName);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"Emulator Executable File (*.exe)|*.exe|All Files (*.*)|*.*";
            saveFileDialog.Title = "Select preferred emulator executable";
            saveFileDialog.FileName = emulatorExecutable;
            saveFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(emulatorExecutable);
            saveFileDialog.CheckFileExists = true;
            saveFileDialog.OverwritePrompt = false;
            DialogResult results = saveFileDialog.ShowDialog();
            if (results == DialogResult.Cancel)
                return;
            if (saveFileDialog.FileName == "" || !File.Exists(saveFileDialog.FileName))
            {
                ErrorMessage($"Entered invalid file name for emulator", "Invalid Name!!!", "SetEmulatorExecute", "User either selected empty file name or a file that does NOT exists.");
                return;
            }
            if (saveFileDialog.FileName.Equals(emulatorExecutable, StringComparison.OrdinalIgnoreCase))
            {
                WarnMessage($"Nothing to do, because the new file name is the same as the old file name:\n{saveFileDialog.FileName}", "Same Name");
                return;
            }
            SetEmulatorExecute(systemName, saveFileDialog.FileName);
        }
        public Rom GetRomFromList(string romFilePath)
        {
            foreach (Rom rom in romList) 
            {
                if (rom.FilePath.Equals(romFilePath,StringComparison.OrdinalIgnoreCase))
                    return rom;
            }
            return null;
        }
        public string ConvertToTitle(string name)=> ConvertToSimplifiedName(name,false, false, true, false, true,
                        false, false, false, false, false,true, true,
                        true, true, false, false, false,false, true, true);
        public string ConvertToCompress(string name)=> ConvertToSimplifiedName(name, true, true);
        public string ConvertToNameSimplified(string name)=> ConvertToSimplifiedName(name,false,false,true,true,true,true,
            true,true,true,true,true,true,false,true, true, 
            true, true, true, true);
        private string ConvertToSimplifiedName(string name,
                bool removeNumbers = false, bool doHighCompress = false, bool removeBracesAndSuffixData = true, 
                bool trimSpaces = true, bool removeRomPrefixIndexes = true, bool removeApostropheEs = true, 
                bool convertSuffixRomanNumToDec = true, bool removeNonAlphaNum = true, bool removeApostrophe = true, 
                bool makeLowerCase = true, bool replaceUnderScoreWithSpace = false, bool removeRedundant = false, bool removeTrailingNonAlphaNumChar = false, bool swapTheA_ToFront = false,
                bool removeTheA = false, bool removeDisneyLego = false, bool swapAmpersandForAnd = false, bool removePlural_and_removeApostropheEs = false,
                bool removeByAuthor = false, bool insertSpacesBeforeUppercaseWords = false,
                string removeSpecificStrValue = "", string replacementStr = "") // removeSpecificStrValue and replacementStr is for possible future requirements, or for end user customization.
        { //ToDo: Add logic on title to separate JoustPong to Joust Pong        and PhantomPanzer to Phantom Panzer
            if (removeSpecificStrValue.Length > 0) // Keep this at the top because the other filters could hinder finding the specified string.
                name = name.Replace(removeSpecificStrValue, replacementStr);
            
            if (removeBracesAndSuffixData)
            {// Do before filtering non-alpha-num because that will hinder finding braces and suffix data
                name = Regex.Replace(name, @"\s[\(\[].*", ""); // Remove braces, suffix data, and prefix space. IE: "GameName (U) [!]" to "GameName"
                // Do the following incase there's no space before the brace.
                name = Regex.Replace(name, @"[\(\[].*", ""); // Remove braces and suffix string. IE: "GameName(U) [!]" to "GameName"
                if (name.Contains("_"))
                {
                    name = Regex.Replace(name, @"(?i)_FRA_.*", "");
                    name = Regex.Replace(name, @"(?i)_EUR_.*", "");
                    name = Regex.Replace(name, @"(?i)_USA_.*", "");
                    name = Regex.Replace(name, @"(?i)_JPN_.*", "");
                    name = Regex.Replace(name, @"(?i)_JAP_.*", "");
                    name = Regex.Replace(name, @"(?i)_GER_.*", "");
                    name = Regex.Replace(name, @"(?i)_KOR_.*", "");
                    name = Regex.Replace(name, @"(?i)_ITA_.*", "");
                    name = Regex.Replace(name, @"(?i)_NDS[_-].*", "");
                }
            }
            
            if (removeRomPrefixIndexes) // Do before filtering spaces and non-alpha-num because that will hinder finding prefix index number
                name = Regex.Replace(name, @"^[0-9][0-9][0-9][0-9][\s_]-[\s_]", ""); // Remove prefix index numbers. IE: "1234 - GameName" to "GameName"

            if (doHighCompress || replaceUnderScoreWithSpace)
                name = name.Replace("_", " ");
            if (swapAmpersandForAnd)
                name = name.Replace(" & ", " and ");
            if (removePlural_and_removeApostropheEs)
                name = Regex.Replace(name, @"'?[sS]\b", "");

            if (doHighCompress  || removeRedundant)
            {
                name = Regex.Replace(name, @"(?i)[\s_]DSi?\+?([\s_\(\[])", "$1");
                name = Regex.Replace(name, @"(?i)\sNDS$", "");
                name = Regex.Replace(name, @"(?i)\sDS$", "");
                name = Regex.Replace(name, @"(?i)N?64", "");
                name = Regex.Replace(name, @"(?i)3D", "");
                name = Regex.Replace(name, @"(?i)^NES\s", "");
                name = Regex.Replace(name, @"(?i)32x", "");
            }
            if (doHighCompress || swapTheA_ToFront)
            {
                string nameWithOutTheA = Regex.Replace(name, @"(,\s?(A|The)$)|(,\s?(A|The)\s)", "");
                if (!name.Equals(nameWithOutTheA))
                {
                    if (swapTheA_ToFront)
                    {
                        string nameWithTheA_Swap = Regex.Replace(name, @"^(.*),\s?(A|The)$", "$2 $1");
                        if (name.Equals(nameWithTheA_Swap))
                            nameWithTheA_Swap = Regex.Replace(name, @"^(.*),\s?(A|The)\s", "$2 $1 ");
                        name = nameWithTheA_Swap;
                    }
                    else
                    {
                        string nameWithTheA_Swap = Regex.Replace(name, @"^(.*),\s?(A|The)$", "$1");
                        if (name.Equals(nameWithTheA_Swap))
                            nameWithTheA_Swap = Regex.Replace(name, @"^(.*),\s?(A|The)\s", "$1 ");
                        name = nameWithTheA_Swap;
                    }
                }
            }
            if (doHighCompress)
            {
                removeNumbers = true;
                name = Regex.Replace(name, @"(?i)([a-zA-Z][a-zA-Z])'?s\b", "$1");
            }
            if (doHighCompress || removeTheA)
            {
                name = Regex.Replace(name, @"(?i)^A\s", "");
                name = Regex.Replace(name, @"(?i)^The\s", "");
            }
            if (doHighCompress)
            {
                name = Regex.Replace(name, @"(?i)\s[\,-\.]\sA\s", "");
                name = Regex.Replace(name, @"(?i)\s[\,-\.]\sThe\s", "");
                name = Regex.Replace(name, @"(?i)[\,-\.]\s?A", "");
                name = Regex.Replace(name, @"(?i)[\,-\.]\s?The", "");
                name = Regex.Replace(name, @"(?i)\sand\s", "");
                name = Regex.Replace(name, @"(?i)\sin\s", "");
                name = Regex.Replace(name, @"(?i)\sVersion\s", "");
                name = Regex.Replace(name, @"(?i)\sEdition\s", "");
                name = Regex.Replace(name, @"(?i)\sPart\s", "");
                name = Regex.Replace(name, @"(?i)^[2-9]-in-1\s?-?\s?", ""); // Remove "2-in-1 - ", and , "3-in-1 - " from start of name
            }
            if (doHighCompress || removeDisneyLego)
            {
                name = Regex.Replace(name, @"(?i)^Disney's\s", ""); // Remove Disney's from start of name
                name = Regex.Replace(name, @"(?i)^Disney-Pixar\s", ""); // Remove Disney's from start of name
                name = Regex.Replace(name, @"(?i)^Disney\s", ""); // Remove Disney's from start of name
                name = Regex.Replace(name, @"(?i)^LEGO\s", ""); // Remove LEGO from start of name
            }
            if (doHighCompress || removeByAuthor)
                name = Regex.Replace(name, @"(?i)(\sby\s.+)", ""); // Remove " by ?author?"
            if (doHighCompress || removeTheA)
            {
                string nameWithOutTheA = Regex.Replace(name, @"(,\s?(A|The)$)|(,\s?(A|The)\s)", "");
                if (!name.Equals(nameWithOutTheA))
                {
                    string nameWithTheA_Swap = Regex.Replace(name, @"^(.*),\s?(A|The)$", "$1");
                    if (name.Equals(nameWithTheA_Swap))
                        nameWithTheA_Swap = Regex.Replace(name, @"^(.*),\s?(A|The)\s", "$1 ");
                    name = nameWithTheA_Swap;
                }
                name = Regex.Replace(name, @"^(A|The)\s", "");
            }
            if (insertSpacesBeforeUppercaseWords) 
            {
                //if (name.Contains("Frogger"))
                //{
                //    name = name;
                //}
                name = Regex.Replace(name, "([a-z])([A-Z])", "$1 $2"); // Put spaces between words like FooDigitHere
                name = Regex.Replace(name, "([A-Z][a-z]+)([A-Z][a-z]*)", "$1 $2"); // Put spaces between words like FooDigitHere
            }

            if (convertSuffixRomanNumToDec)// Do before filtering spaces because it will hinder finding suffix roman numbers
            {
                const string strStringContains5_Or_10 = "VXvx";
                if (name.IndexOfAny(strStringContains5_Or_10.ToCharArray()) != -1)
                {// Only do this extra work if V or X is in the string.
                    if (name.Contains("x", StringComparison.OrdinalIgnoreCase))
                    {
                        name = Regex.Replace(name, @"(?i)\sXIX", "19");         // IE: "GameName XIX" to "GameName19"
                        name = Regex.Replace(name, @"(?i)\sXVIII", "18");       // IE: "GameName XVIII" to "GameName18"
                        name = Regex.Replace(name, @"(?i)\sXVII", "17");        // IE: "GameName XVII" to "GameName17"
                        name = Regex.Replace(name, @"(?i)\sXVI", "16");         // IE: "GameName XVI" to "GameName16"
                        name = Regex.Replace(name, @"(?i)\sXV", "15");          // IE: "GameName XV" to "GameName15"
                        name = Regex.Replace(name, @"(?i)\sXIV", "14");         // IE: "GameName XI" to "GameName14"
                        name = Regex.Replace(name, @"(?i)\sXIII", "13");        // IE: "GameName XIII" to "GameName13"
                        name = Regex.Replace(name, @"(?i)\sXII", "12");         // IE: "GameName XII" to "GameName12"
                        name = Regex.Replace(name, @"(?i)\sXI", "11");          // IE: "GameName XI" to "GameName11"
                        name = Regex.Replace(name, @"(?i)\sX$", "10");          // IE: "GameName X" to "GameName10"
                        name = Regex.Replace(name, @"(?i)\sX\s\-\s", "10 - ");  // IE: "GameName X" to "GameName10 - "
                        name = Regex.Replace(name, @"(?i)\sIX", "9");           // IE: "GameName IX" to "GameName9"
                    }
                    if (name.Contains("v", StringComparison.OrdinalIgnoreCase))
                    {
                        name = Regex.Replace(name, @"(?i)\sVIII", "8");         // IE: "GameName VIII" to "GameName8"
                        name = Regex.Replace(name, @"(?i)\sVII", "7");          // IE: "GameName VII" to "GameName7"
                        name = Regex.Replace(name, @"(?i)\sVI", "6");           // IE: "GameName VI" to "GameName6"
                        name = Regex.Replace(name, @"(?i)\sIV", "4");           // IE: "GameName IV" to "GameName4"
                        name = Regex.Replace(name, @"(?i)\sV$", "5");           // IE: "GameName V" to "GameName5"
                        name = Regex.Replace(name, @"(?i)\sV\s\-\s", "5 - ");   // IE: "GameName V" to "GameName5 - "
                    }
                }
                if (name.Contains(" I", StringComparison.OrdinalIgnoreCase))
                {
                    name = Regex.Replace(name, @"(?i)[^0-9a-zA-Z]i[^0-9a-zA-Z]i[^0-9a-zA-Z]i[^0-9a-zA-Z]?", "3");  // IE: "GameName I I I" to "GameName3"
                    name = Regex.Replace(name, @"(?i)\sIII", "3");                                                 // IE: "GameName III" to "GameName3"
                    name = Regex.Replace(name, @"(?i)[^0-9a-zA-Z]i[^0-9a-zA-Z]i[^0-9a-zA-Z]?", "2");               // IE: "GameName I I" to "GameName2"
                    name = Regex.Replace(name, @"(?i)\sII", "2");                                                  // IE: "GameName II" to "GameName2"
                    name = Regex.Replace(name, @"(?i)[^0-9a-zA-Z]i[^0-9a-zA-Z]v[^0-9a-zA-Z]?", "4");               // IE: "GameName I V" to "GameName4"
                    name = Regex.Replace(name, @"(?i)\sI$", "1");                                                  // IE: "GameName I" to "GameName1"
                    name = Regex.Replace(name, @"(?i)\sI\s\-\s", "1 - ");                                          // IE: "GameName I" to "GameName1 - "
                }
            }

            if (removeNumbers) // Do this after roman numeral conversion so that roman numerals are removed as well.
                name = Regex.Replace(name, @"[0-9]", "");//IE: "GameName 2" to "GameName "

            if (makeLowerCase)
                name = name.ToLower();
            
            if (removeApostropheEs) // Do before filtering non-alpha-num because that will hinder finding 's
                name = name.Replace("'s", "");//IE: "Dini's Soccer" to "Dini Soccer"

            if (removeNonAlphaNum)
                name = Regex.Replace(name, @"[^0-9a-zA-Z]", "");//IE: "WarioWare - Touched!" to "WarioWareTouched"
            else
            {// There's no need to do this logic if removeNonAlphaNum==true, and non-alpha-num characters have been removed.
                if (removeApostrophe)
                    name = name.Replace("'", "");//IE: "Baseball '99" to "Baseball 99"
                if (trimSpaces)
                    name = name.Trim(' ');//IE: "GameName " to "GameName"
            }

            name = Regex.Replace(name, @"\.[a-zA-Z][0-9a-zA-Z][0-9a-zA-Z]?$", "");
            name = Regex.Replace(name, @"(?i)\s?\-?ntsc$", "");// Remove "-ntsc" and " ntsc" from end of name

            if (removeTrailingNonAlphaNumChar)
                name = Regex.Replace(name, "[^0-9a-zA-Z.!]$", "");

            return name;
        }
        public enum ChecksumType
        {
            None = 0,
            MD5,
            SHA256,
        }
        public static string GetImageChecksum(string filePath, ChecksumType checksumType) => GetChecksumStr(filePath, checksumType);
        public static string GetImageChecksum(string filePath, bool alwaysGetChecksum = false, ChecksumType checksumType = ChecksumType.None) => Properties.Settings.Default.DoImageChecksum || alwaysGetChecksum ? GetChecksumStr(filePath, checksumType) : "";
        public static string GetRomChecksum(string filePath, ChecksumType checksumType) => GetChecksumStr(filePath, checksumType);
        public static string GetRomChecksum(string filePath, bool alwaysGetChecksum = false, ChecksumType checksumType = ChecksumType.None) => Properties.Settings.Default.DoRomChecksum || alwaysGetChecksum ? GetChecksumStr(filePath, checksumType) : "";
        public static string GetRomCompressChecksum(string filePath, ChecksumType checksumType) => GetRomCompressChecksum(filePath, true, checksumType);
        public static string GetRomCompressChecksum(string filePath, bool alwaysGetChecksum = false, ChecksumType checksumType = ChecksumType.None) 
        {
            if (!alwaysGetChecksum && (!Properties.Settings.Default.DoRomChecksum || !Properties.Settings.Default.DoZipChecksum))
                return "";
            string returnValue = "";
            int qtyFilesInZip = 0;
            try
            {
                string extRom = Path.GetExtension(filePath).ToLower();
                if (SUPPORTED_COMPRESSION_FILE.Contains(extRom))
                {
                    Random rnd = new Random();
                    using (TempDirStorage tempDirStorage = new TempDirStorage(GetTempDirSuffix(filePath, ++incrementTempDir)))
                    {
                        using (IDecompressArchive archive = CompressArchive.Open(filePath))
                        {
                            archive.ExtractToDirectory(tempDirStorage.tempDir);
                            foreach (var name in archive.GetNames())
                            {
                                string destinationPath = Path.GetFullPath(Path.Combine(tempDirStorage.tempDir, name));
                                if (Directory.Exists(destinationPath))
                                    continue;
                                returnValue = GetRomChecksum(destinationPath, checksumType);
                                ++qtyFilesInZip;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DbErrorLogging(ex, $"GetRomCompressChecksum exception thrown \"{ex.Message}\"!", $"Error with filePath = {filePath}");
            }
            // Only support zip files with a single compressed file. If more than 1 file, return checksum for entire zip file.
            return qtyFilesInZip == 1 ? returnValue : GetRomChecksum(filePath);
        }
        private string TimeRemaining(int seconds)
        {
            if (seconds < 100)
                return $"{seconds} seconds";
            // Adding comment in order to suppress warning about simplifying IF statement
            return seconds < 60 * 60
                ? $"{seconds / 60} minutes and {seconds-((int)(seconds / 60)*60)} seconds"
                : $"{seconds / (60*60)} hours and {seconds - ((int)(seconds / (60 * 60)) * 60 * 60)} minutes";
        }
        private bool DidCancelButtonGetPressed(bool isMainThread = false)
        {
            System.Windows.Forms.Application.DoEvents();
            while ((pauseWork || pauseInSeconds > 0) && !cancelWork)
            {
                Thread.Sleep(2000);
                System.Windows.Forms.Application.DoEvents();
                if (pauseWork && pauseInSeconds > 0)
                {
                    pauseInSeconds = 0;
                    pauseWork = false;
                    if (isMainThread)
                        SendStatus("Resuming process.", isMainThread, true);
                }
                else if (!pauseWork)
                {
                    pauseInSeconds -= 2;
                    if (isMainThread)
                        SendStatus($"Process is on pause for {TimeRemaining(pauseInSeconds)} by user for . Press pause key again to continue.", isMainThread, false);
                }
                else if (isMainThread)
                    SendStatus("Process is on pause by user. Press pause key again to continue.", isMainThread, false);
            }
            return cancelWork;
        }
        private static void ResetWorkStatus(int qtyWork, bool isMainThread = true) => ResetWorkStatus(isMainThread, true, true, qtyWork);
        private static void ResetWorkStatus(bool isMainThread = true, bool resetQtyVar = true, bool setTaskbarProgressState = true, int qtyWork = 0)
        {
            cancelWork = false;
            pauseWork = false;
            pauseInSeconds = 0;
            if (resetQtyVar)
            {
                Form_Main.qtyWork = qtyWork;
                if (setTaskbarProgressState)
                {
                    if (isMainThread)
                        SetProgress(qtyWork);
                    else
                        form_Main.BeginInvoke(new MyDelegateInitProgressBar(form_Main.DelegateInitProgressBar), form_Main.GetDelegateAction(form_Main.progressBar_BottomRight, qtyWork));
                }
                qtyChanges = 0;
                qtySet2Changes = 0;
                qtyTotalChanges = 0;
                qtyNoChanges = 0;
                qtyProcess = 0;
                qtyTotalProcess = 0;
            }
        }
        private static int IncProgress()
        {
            taskbarProcess.SetProgressValue(++qtyProcess, qtyWork);
            form_Main.progressBar_BottomRight.PerformStep();
            return qtyProcess;
        }
        private static void SetMiniProgressBar(int max)
        {
            form_Main.progressBar_BottomRight.Minimum = 1;
            form_Main.progressBar_BottomRight.Maximum = max;
            form_Main.progressBar_BottomRight.Step = 1;
            form_Main.progressBar_BottomRight.Value = 1;
            form_Main.progressBar_BottomRight.Enabled = true;
            form_Main.progressBar_BottomRight.Visible = true;
        }
        private static void SetProgress(int qtyWork)
        {
            if (qtyWork > 0) 
            {
                taskbarProcess.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Normal);
                taskbarProcess.SetProgressValue(0, qtyWork);
                SetMiniProgressBar(qtyWork);
            }
            else
                ClearProgress();
        }
        private static void ClearProgress(bool zeroOutQtyWork = false)
        {
            taskbarProcess.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.NoProgress);
            taskbarProcess.SetProgressValue(0, 0);
            form_Main.progressBar_BottomRight.Visible = false;
            if (zeroOutQtyWork)
                qtyWork = 0;
        }
        public void DeleteDuplicateRomFilesInDatabase(DeleteDuplicateBy deleteDuplicateBy)
        {
            ResetWorkStatus();
            List<Rom> roms = new List<Rom>();
            using (new CursorWait())
            {

                if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateChecksum || deleteDuplicateBy == DeleteDuplicateBy.DuplicateCompressChecksum)
                {
                    if (!Properties.Settings.Default.DoRomChecksum)
                    {
                        MessageBox.Show("ROM's checksum option is disabled, and this option is not available unless ROM checksum is enabled.  See Settings option.");
                        return;
                    }
                    if (!DidCancelButtonGetPressed() && Properties.Settings.Default.DoRomChecksum && deleteDuplicateBy == DeleteDuplicateBy.DuplicateChecksum)
                    {
                        using (SqlReader reader = new SqlReader("SELECT FilePath, Checksum FROM roms WHERE Checksum IN (SELECT * FROM (SELECT Checksum FROM roms GROUP BY Checksum HAVING COUNT(Checksum) > 1) AS a) and Checksum <> \"\" order by Checksum, length(FilePath) desc;"))
                        {
                            while (!DidCancelButtonGetPressed() && reader.Read())
                            {
                                string filePath = reader.GetString(0);
                                string checksum = reader.GetString(1);
                                if (IsNullOrEmpty(checksum))
                                    continue;
                                Rom rom = db.Get_Rom(filePath);
                                if (rom == null)
                                    ErrorLogging($"Failed to get file {filePath}");
                                else
                                    roms.Add(rom);
                            }
                        }
                    }
                    if (!DidCancelButtonGetPressed() && Properties.Settings.Default.DoZipChecksum && deleteDuplicateBy == DeleteDuplicateBy.DuplicateCompressChecksum)
                    {
                        using (SqlReader reader = new SqlReader("SELECT FilePath, CompressChecksum FROM roms WHERE CompressChecksum IN (SELECT * FROM (SELECT CompressChecksum FROM roms GROUP BY CompressChecksum HAVING COUNT(CompressChecksum) > 1) AS a) and CompressChecksum <> \"\" order by CompressChecksum, length(FilePath) desc;"))
                        {
                            while (!DidCancelButtonGetPressed() && reader.Read())
                            {
                                string filePath = reader.GetString(0);
                                string compressChecksum = reader.GetString(1);
                                if (IsNullOrEmpty(compressChecksum))
                                    continue;
                                Rom rom = db.Get_Rom(filePath);
                                if (rom == null)
                                    ErrorLogging($"Failed to get file {filePath}");
                                else
                                {
                                    rom.Checksum = compressChecksum;
                                    roms.Add(rom);
                                }
                            }
                        }
                    }
                }
                else if (   deleteDuplicateBy == DeleteDuplicateBy.DuplicateTitleInAnySystem || deleteDuplicateBy == DeleteDuplicateBy.DuplicateTitleInSameSystem ||
                            deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameSimplifiedInAnySystem || deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameSimplifiedInSameSystem ||
                            deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameOrgInAnySystem || deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameOrgInSameSystem ||
                            deleteDuplicateBy == DeleteDuplicateBy.DuplicateCompressedInAnySystem || deleteDuplicateBy == DeleteDuplicateBy.DuplicateCompressedInSameSystem
                    )
                {
                    string sql = "";
                    if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateTitleInAnySystem || deleteDuplicateBy == DeleteDuplicateBy.DuplicateTitleInSameSystem)
                    {
                        sql = deleteDuplicateBy == DeleteDuplicateBy.DuplicateTitleInAnySystem ?
                        "SELECT FilePath, Title, RomSize FROM roms WHERE Title IN (SELECT * FROM (SELECT Title FROM roms GROUP BY Title HAVING COUNT(Title) > 1) AS a) order by Title, RomSize desc;" :
                        $"SELECT FilePath, Title, System, RomSize FROM roms WHERE Title || System  IN (SELECT Title || System FROM roms GROUP BY Title,System HAVING COUNT(*) > 1) and System = {GetSystemIndex(toolStripComboBoxSystem.Text)} order by Title, RomSize desc;";
                    }
                    else if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameSimplifiedInAnySystem || deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameSimplifiedInSameSystem)
                    {
                        sql = deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameSimplifiedInAnySystem ?
                        "SELECT FilePath, Title, NameSimplified, RomSize FROM roms WHERE NameSimplified IN (SELECT * FROM (SELECT NameSimplified FROM roms GROUP BY NameSimplified HAVING COUNT(NameSimplified) > 1) AS a) order by NameSimplified, RomSize desc;" :
                        $"SELECT FilePath, Title, NameSimplified, System, RomSize FROM roms WHERE NameSimplified || System  IN (SELECT NameSimplified || System FROM roms GROUP BY NameSimplified,System HAVING COUNT(*) > 1) and System = {GetSystemIndex(toolStripComboBoxSystem.Text)} order by NameSimplified, RomSize desc;";
                    }
                    else if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameOrgInAnySystem || deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameOrgInSameSystem)
                    {
                        sql = deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameOrgInAnySystem ?
                        "SELECT FilePath, Title, NameOrg, RomSize FROM roms WHERE NameOrg IN (SELECT * FROM (SELECT NameOrg FROM roms GROUP BY NameOrg HAVING COUNT(NameOrg) > 1) AS a) order by NameOrg, RomSize desc;" :
                        $"SELECT FilePath, Title, NameOrg, System, RomSize FROM roms WHERE NameOrg || System  IN (SELECT NameOrg || System FROM roms GROUP BY NameOrg,System HAVING COUNT(*) > 1) and System = {GetSystemIndex(toolStripComboBoxSystem.Text)} order by NameOrg, RomSize desc;";
                    }
                    else if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateCompressedInAnySystem || deleteDuplicateBy == DeleteDuplicateBy.DuplicateCompressedInSameSystem)
                    {
                        sql = deleteDuplicateBy == DeleteDuplicateBy.DuplicateCompressedInAnySystem ?
                        "SELECT FilePath, Title, Compressed, RomSize FROM roms WHERE Compressed IN (SELECT * FROM (SELECT Compressed FROM roms GROUP BY Compressed HAVING COUNT(Compressed) > 1) AS a) and Compressed <> \"\" order by Compressed, RomSize desc;" :
                        $"SELECT FilePath, Title, Compressed, System, RomSize FROM roms WHERE Compressed || System  IN (SELECT Compressed || System FROM roms GROUP BY Compressed,System HAVING COUNT(*) > 1) and System = {GetSystemIndex(toolStripComboBoxSystem.Text)} and Compressed <> \"\" order by Compressed, RomSize desc;";
                    }
                    using (SqlReader reader = new SqlReader(sql))
                    {
                        while (!DidCancelButtonGetPressed() && reader.Read())
                        {
                            string filePath = reader.GetString(0);
                            Rom rom = db.Get_Rom(filePath);
                            if (rom == null)
                                ErrorLogging($"Failed to get file {filePath}");
                            else
                                roms.Add(rom);
                        }
                    }
                }
            }
            if (!DidCancelButtonGetPressed())
            {
                const string TreeNodeText = "TreeNode: ";
                Form_RomsToDelete formRomsToDelete = new Form_RomsToDelete(roms, deleteDuplicateBy);
                formRomsToDelete.ShowDialog();
                if (formRomsToDelete.RomSelectedToDelete.Count > 0)
                {
                    string message = formRomsToDelete.OnlyRemoveFromDb ? $"Are you sure you want to remove {formRomsToDelete.RomSelectedToDelete.Count} ROM's from GameLauncher database?" : $"Are you sure you want to delete {formRomsToDelete.RomSelectedToDelete.Count} ROM files?";
                    string title = formRomsToDelete.OnlyRemoveFromDb ? $"Database removal {formRomsToDelete.RomSelectedToDelete.Count} ROM's" : $"Delete {formRomsToDelete.RomSelectedToDelete.Count} Files";
                    if (MessageBox.Show(message, title,MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==  DialogResult.Yes)
                    {
                        using (new CursorWait())
                        {
                            foreach (string f in formRomsToDelete.RomSelectedToDelete)
                            {
                                string fileToDelete = f.StartsWith(TreeNodeText) ? f.Substring(TreeNodeText.Length) : f;
                                if (formRomsToDelete.OnlyRemoveFromDb == false)
                                    File.Delete(fileToDelete);
                                db.DeleteRom(fileToDelete);
                            }
                        }
                    }
                }
            }
        }
        public void DeleteImageFilesNotInDatabase(bool doSilentDelete)
        {
            if (!Properties.Settings.Default.DoImageChecksum)
            {
                MessageBox.Show("Image checksum option is disabled, and this option is not available unless image checksum is enabled.  See Settings option.");
                return;
            }
            using (new CursorWait())
            {
                ResetWorkStatus();
                string[] directories = GetEmulatorsBasePaths();
                foreach (var dir in directories)
                    if (Directory.Exists(dir))
                    {
                        string imagePath = Path.Combine(dir, imageSubFolderName.TrimStart('\\'));
                        DeleteImageFilesNotInDatabase(imagePath, doSilentDelete);
                        using (SqlReader reader = new SqlReader("SELECT ImageDirPath FROM GameSystems ORDER BY Name"))
                        {
                            while (!DidCancelButtonGetPressed() && reader.Read())
                                DeleteImageFilesNotInDatabase(reader.GetString(0), doSilentDelete);
                        }
                    }
            }
            if (DidCancelButtonGetPressed())
                InfoMessage($"Finish scanning for duplicate file images, and deleted {qtySet2Changes} duplicate files.", "Done");
        }
        public void DeleteImageFilesNotInDatabase(string path, bool doSilentDelete)
        {
            string[] imgFiles = GetFilesByExtensions(path, SearchOption.TopDirectoryOnly, SUPPORTED_IMAGE_FILES); // Directory.GetFiles(path, "*.png");
            if (doSilentDelete)
                Parallel.ForEach(imgFiles, (imgFile, loopState) =>
                {
                    if (DidCancelButtonGetPressed())
                        loopState.Stop();
                    DeleteImageFileNotInDatabase(imgFile, doSilentDelete);
                });
            else
                foreach (string imgFile in imgFiles)
                    DeleteImageFileNotInDatabase(imgFile, doSilentDelete);
        }
        public void DeleteImageFileNotInDatabase(string imgFile, bool doSilentDelete)
        {
            if (DidCancelButtonGetPressed())
                return;
            string path = GetFirstColStr($"SELECT FilePath FROM Images where FilePath like \"{imgFile}\"");
            if (IsNullOrEmpty(path))
            {
                if (!doSilentDelete)
                {
                    string Checksum = GetImageChecksum(imgFile);
                    string existingImgFile = GetFirstColStr($"SELECT FilePath FROM Images where Checksum = \"{Checksum}\"");
                    Form_ImageDeletionPrompt form_ImageDeletionPrompt = new Form_ImageDeletionPrompt(imgFile, existingImgFile);
                    form_ImageDeletionPrompt.ShowDialog();
                    if (form_ImageDeletionPrompt.CancelAll)
                        cancelWork = true;
                    if (!form_ImageDeletionPrompt.Yes)
                        return;
                }
                qtySet2Changes++;
                File.Delete(imgFile);
            }
        }
        private EmulatorExecutables GetEmulatorPaths(string path)
        {
            EmulatorExecutables emulatorPaths = new EmulatorExecutables();
            int index = 0;
            string[] linkFiles = Directory.GetFiles(path, "*.lnk", SearchOption.TopDirectoryOnly);
            foreach (string f in linkFiles)
            {
                string execFilePath = GetShortcutTargetFile(f);
                if (execFilePath != null)
                {
                    if (!File.Exists(execFilePath))
                        continue;
                    emulatorPaths.EmulatorPaths[index] = execFilePath;
                    index += 1;
                    if (index == EmulatorExecutables.MAX_EMULATOR_EXECUTABLES)
                        return emulatorPaths;
                }
            }
            string[] execFiles = Directory.GetFiles(path, "*.exe", SearchOption.AllDirectories);
            foreach (string f in execFiles)
            {
                if (f.Contains("setup") || f.Contains("install") || f.Contains("unins00"))
                    continue;
                emulatorPaths.EmulatorPaths[index] = f;
                index += 1;
                if (index == EmulatorExecutables.MAX_EMULATOR_EXECUTABLES)
                    return emulatorPaths;
            }
            return index == 0 ? null : emulatorPaths;
        }
        public static string GetPattern(string value, string pattern, char trimLeftChars = '\0', char trimRightChars = '\0', bool defaultEmptyStr = false)
        {
            string ReturnStr = defaultEmptyStr ? "" : null;
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(value);
            if (m.Success)
            {
                Group g = m.Groups[0];
                ReturnStr = g.Value;
                if (trimLeftChars != '\0')
                    ReturnStr = ReturnStr.TrimStart(trimLeftChars);
                if (trimRightChars != '\0')
                    ReturnStr = ReturnStr.TrimEnd(trimRightChars);
            }
            return ReturnStr;
        }
        public static string GetFileNameWithoutExtensionAndWithoutBin(string path)
        {
            path = Path.GetFileNameWithoutExtension(path);
            if (path != null && path.Contains("."))
                path = Regex.Replace(path, @"\.[a-zA-Z][0-9a-zA-Z][0-9a-zA-Z]?$", "");
            return path;
        }
        public static int GetYear(string name)
        {
            string YearOnly = Regex.Replace(name, @"\([0-9]*\-?[0-9]*\-?([0-9]{4})\)", "$1");
            try
            {
                return YearOnly != null && YearOnly.Length > 0 ? Int32.Parse(YearOnly) : 0;
            }
            catch
            {
                return 0;
            }
        }
        public static string GetVersion(string name)
        {
            string Version = GetPattern(name, @"\(V[0-9]+\.[0-9]+\)", '(', ')', true);
            if (name.Contains("(REV", StringComparison.OrdinalIgnoreCase))
            {
                string RevInfo = GetPattern(name, @"\(REV[0-9]+\)", '(', ')');
                if (RevInfo != null)
                    Version += $"[REV{RevInfo}]";
            }
            if (name.Contains("(PRG", StringComparison.OrdinalIgnoreCase))
            {
                string RevInfo = GetPattern(name, @"\(PRG[0-9]+\)", '(', ')');
                if (RevInfo != null)
                    Version += $"[PRG-REV{RevInfo}]";
            }
            if (name.Contains("(Beta)", StringComparison.OrdinalIgnoreCase))
                Version += "[Beta]";
            if (name.Contains("(Proto)", StringComparison.OrdinalIgnoreCase))
                Version += "[Prototype ]";
            return Version;
        }
        public static string GetNotesCore(string NameOrg, ref string Status, ref string Language)
        {
            string NotesCore = "";
            if (NameOrg.Contains("[C]"))// Needs to be case sensitive because [c] is used for Checksum
                NotesCore += "[Game Boy Color]";
            if (NameOrg.Contains("(PD)", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[Public Domain : Free software]";
            if (NameOrg.Contains("[h]", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(Hack)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("[f1C]", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[Hacked]";
            if (NameOrg.Contains("[p]", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(Bootleg)", StringComparison.OrdinalIgnoreCase) || GetPattern(NameOrg, @"\[p[0-9]+\]") != null)
                NotesCore += "[Pirate]";
            if (GetPattern(NameOrg, @"\[a[0-9]+\]") != null)
                NotesCore += $"[Alternate version {GetPattern(NameOrg, @"\[a[0-9]+\]")}]";
            if (GetPattern(NameOrg, @"\[t[0-9]+\]") != null)
                NotesCore += "[Trained. The ROM has been edited to alter stats or other gameplay mechanics]";
            if (NameOrg.Contains("[T]", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("[TR]", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[Translation]";
            else if (NameOrg.Contains("[T +]", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("[T+]", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[Newer translation]";
            else if (NameOrg.Contains("[T-]", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("[T -]", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[Older translation]";
            else if (NameOrg.Contains("[i]", StringComparison.OrdinalIgnoreCase))
                Status += "[Incomplete Translation]";
            if (NameOrg.Contains("[n]", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[File Name Translated]";
            if (NameOrg.Contains("(Unl)", StringComparison.OrdinalIgnoreCase))
                Status += "[Unlicensed]";
            if (NameOrg.Contains("(-)"))
                NotesCore += "[Year unknown]";
            if (NameOrg.Contains("(Taikenban)", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[Trial version]";
            if (NameOrg.Contains("(Genteiban)", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[Limited Edition]";
            if (NameOrg.Contains("(Shokai Genteiban)", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[Limited version]";
            if (NameOrg.Contains("(old)", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[Old version]";
            if (NameOrg.Contains("(Ch-Trad)", StringComparison.OrdinalIgnoreCase))
                Language = "[Traditional Chinese]";
            if (NameOrg.Contains("(Ch-Simple)", StringComparison.OrdinalIgnoreCase))
                Language = "Standard Chinese";
            if (NameOrg.Contains("(PAL)", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[European-(PAL)]";
            if (NameOrg.Contains("(NTSC)", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[NTSC]";
            if (NameOrg.Contains("(GC)", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[GameCube]";
            if (NameOrg.Contains("(4)", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[USA & Brazil NTSC only]";
            if (NameOrg.Contains("(5)", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[NTSC only]";
            if (NameOrg.Contains("(8)", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[PAL only]";
            if (NameOrg.Contains("(MP)", StringComparison.OrdinalIgnoreCase))
                NotesCore += "[Mega Play]";
            return NotesCore;
        }
        public static string GetLanguageAndRegion(string NameOrg, ref string Region)
        {
            string Language = "";
            if (NameOrg.Contains("(U)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(USA)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("_USA_", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(NTSC)", StringComparison.OrdinalIgnoreCase))
            {
                Region = "USA";
                Language = "English";
            }
            else if (NameOrg.Contains("(J)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(JPN)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(JAP)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("_JPN_", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("_JAP_", StringComparison.OrdinalIgnoreCase))
            {
                Region = "Japan";
                Language = "Japanese";
            }
            else if (NameOrg.Contains("(1)"))
            {
                Region = "Japan & Korea";
                Language = "Japanese and/or Korean";
            }
            else if (NameOrg.Contains("(As)", StringComparison.OrdinalIgnoreCase))
                Region = "Asia";
            else if (NameOrg.Contains("(B)", StringComparison.OrdinalIgnoreCase))
                Region = "Brazil";
            else if (NameOrg.Contains("(D)", StringComparison.OrdinalIgnoreCase))
                Region = "Netherlands";
            else if (NameOrg.Contains("(Gr)", StringComparison.OrdinalIgnoreCase))
                Region = "Greece";
            else if (NameOrg.Contains("(HK)", StringComparison.OrdinalIgnoreCase))
                Region = "Hong Kong";
            else if (NameOrg.Contains("(No)", StringComparison.OrdinalIgnoreCase))
                Region = "Norway";
            else if (NameOrg.Contains("(Sw)", StringComparison.OrdinalIgnoreCase))
                Region = "Sweden";
            else if (NameOrg.Contains("(E)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(EUR)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("_EUR_", StringComparison.OrdinalIgnoreCase))
                Region = "Europe";
            else if (NameOrg.Contains("(C)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(Ch)", StringComparison.OrdinalIgnoreCase))
                Region = "China";
            else if (NameOrg.Contains("(G)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(Ger)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("_GER_", StringComparison.OrdinalIgnoreCase))
            {
                Region = "Germany";
                Language = "German";
            }
            else if (NameOrg.Contains("(S)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(Spa)", StringComparison.OrdinalIgnoreCase))
            {
                Region = "Spain";
                Language = "Spanish";
            }
            else if (NameOrg.Contains("(F)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(Fre)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("_FRA_", StringComparison.OrdinalIgnoreCase))
            {
                Region = "France";
                Language = "French";
            }
            else if (NameOrg.Contains("(I)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(Ita)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("ITALiAN", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("_ITA_", StringComparison.OrdinalIgnoreCase))
            {
                Region = "Italy";
                Language = "Italian";
            }
            else if (NameOrg.Contains("(K)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("_KOR_", StringComparison.OrdinalIgnoreCase))
            {
                Region = "Korea";
                Language = "Korean";
            }
            else if (NameOrg.Contains("(R)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(Rus)", StringComparison.OrdinalIgnoreCase))
            {
                Region = "Russia";
                Language = "Russian";
            }
            else if (NameOrg.Contains("(FC)", StringComparison.OrdinalIgnoreCase))
            {
                Region = "Canada";
                Language = "French Canada";
            }
            else if (NameOrg.Contains("(UK)", StringComparison.OrdinalIgnoreCase))
            {
                Region = "United Kingdom";
                Language = "English";
            }
            else if (NameOrg.Contains("(Kor)", StringComparison.OrdinalIgnoreCase))
            {
                Region = "Korea";
                Language = "Korean";
            }

            if (NameOrg.Contains("(JUE)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(W)", StringComparison.OrdinalIgnoreCase))
                Region = "All regions";
            else if (NameOrg.Contains("(JU)", StringComparison.OrdinalIgnoreCase))
                Region = "Japan and USA";
            else if (NameOrg.Contains("(EU)", StringComparison.OrdinalIgnoreCase))
                Region = "Europe and USA";

            if (NameOrg.Contains("(Eng)", StringComparison.OrdinalIgnoreCase))
                Language = "English";
            if (NameOrg.Contains("(M3)", StringComparison.OrdinalIgnoreCase))
                Language = Rom.M3;
            else if (NameOrg.Contains("(M4)", StringComparison.OrdinalIgnoreCase))
                Language = Rom.M4;
            else if (NameOrg.Contains("(M5)", StringComparison.OrdinalIgnoreCase))
                Language = Rom.M5;
            else if (NameOrg.Contains("(M6)", StringComparison.OrdinalIgnoreCase))
                Language = Rom.M6;

            if (NameOrg.Contains("(UE)", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("(U)(E)", StringComparison.OrdinalIgnoreCase))
                Language = "English translation";
            return Language;
        }
        public static bool GetGoodRomStatus(string NameOrg, ref string Status)
        {
            bool isGoodRom = false;
            if (NameOrg.Contains("[!]") || NameOrg.Contains("(!)"))
            {
                Status += "[Verified Good Dump]";
                isGoodRom = true;
            }
            else if (NameOrg.Contains("[! p]", StringComparison.OrdinalIgnoreCase) || NameOrg.Contains("[!p]", StringComparison.OrdinalIgnoreCase))
            {
                Status += "[Verified Good Dump]";
                isGoodRom = true;
            }
            if (NameOrg.Contains("[###]") || NameOrg.Contains("[c]")) // This could be indicator for crack or checksum
            {   // Needs to be case sensitive because [C] is used for Color (for Game Boy Color)
                Status += "[Checksum]";
                isGoodRom = true;
            }
            if (NameOrg.Contains("[f]", StringComparison.OrdinalIgnoreCase) || GetPattern(NameOrg, @"\[f[0-9]+\]") != null)
            {
                Status += "[Fixed. Edited (cracked) to work in emulator]";
                isGoodRom = true;
            }
            if (!isGoodRom)
            {
                if (NameOrg.Contains("[x]", StringComparison.OrdinalIgnoreCase))
                    Status += "[Bad  checksum]";
                if (NameOrg.Contains("[b]", StringComparison.OrdinalIgnoreCase) || GetPattern(NameOrg, @"\[b[0-9]+\]") != null)
                    Status += "[Bad Dump - Bad ROM]";
                if (NameOrg.Contains("[o]", StringComparison.OrdinalIgnoreCase) || GetPattern(NameOrg, @"\[o[0-9]+\]") != null)
                    Status += "[Overdump - Possible Bad ROM]";
            }
            return isGoodRom;
        }
        public static int GetQtyPlayers(string Title, string NameOrg)
        {
            // ToDo: Add logic to read Project64.rdb and Project64.rdx to get rom details
            //       Add logic to read file MultiplayerGameList.txt to get quantity of player details.
            int qtyPlayers = 0;
            return qtyPlayers;
        }
        private Rom GetRomDetails(string romFilePath, int systemIndex, string emulatorDir, long RomSize)
        {
            string NameOrg = GetFileNameWithoutExtensionAndWithoutBin(romFilePath); // f.Substring(imgPath.Length + 1);
            string NameSimplified = ConvertToNameSimplified(NameOrg);
            string Compressed = ConvertToCompress(NameOrg);
            string Title = ConvertToTitle(NameOrg);
            string Status = "";
            string imagePath = GetImageIndexByName(NameSimplified, NameOrg, Title, Compressed, emulatorDir);
            string Region = "unknown";
            string Language = GetLanguageAndRegion(NameOrg, ref Region);
            bool isGoodRom = GetGoodRomStatus(NameOrg, ref Status);
            string NotesCore = GetNotesCore(NameOrg, ref Status, ref Language);
            string Version = GetVersion(NameOrg);
            string Rating = "";
            string Checksum = GetRomChecksum(romFilePath);
            string CompressChecksum = GetRomCompressChecksum(romFilePath);
            int QtyPlayers = GetQtyPlayers(Title, NameOrg);
            string Publisher = "";
            string WikipediaURL = "";
            float StarRating = 0;
            int StarRatingVoteCount = 0;
            int PlayCount = 0;
            bool Favorite = false;
            bool Disable = false;
            bool Hide = false;

            return new Rom(NameSimplified, systemIndex, romFilePath, NameOrg, Title, Compressed,
                0,imagePath, QtyPlayers, Region,"", RomSize, "", NotesCore,
                "","","",Status,Version,"",Language, GetYear(NameOrg), Rating,
                Checksum, CompressChecksum,
                Publisher, WikipediaURL, StarRating, StarRatingVoteCount, PlayCount, Favorite, Disable, Hide);
        }
        private void GetMultiplayerRomData(string SystemDirPath)
        {
            string MultiplayerFileInfo = SystemDirPath + @"\MultiplayerGameList.txt";
            if (!File.Exists(MultiplayerFileInfo))
                return;
            string[] lines = File.ReadAllLines(MultiplayerFileInfo);
            bool addInfoToNotesInsteadOfPlayerQty = false;
            foreach(var line in lines)
            {
                if (line.StartsWith(";"))
                {
                    if (line.StartsWith(";Rom not found"))
                        break;
                    if (line.StartsWith(";Might be multiplayer\r\n"))
                        addInfoToNotesInsteadOfPlayerQty = true;
                    continue;
                }
                string NameSimplified = ConvertToNameSimplified(line);
                string sql = $"UPDATE Roms SET QtyPlayers = 4 WHERE NameSimplified = \"{NameSimplified}\"";
                if (addInfoToNotesInsteadOfPlayerQty)
                    sql = $"UPDATE Roms SET NotesUser = \"Possible Multiplayer Game\" WHERE NameSimplified = \"{NameSimplified}\"";
                UpdateDB(sql);
            }
        }
        private void IncrementProgressBar(System.Windows.Forms.ProgressBar progressbar, bool isMainThread, string newText = "", int MaxLenReset = -1)
        {
            if (isMainThread)
            {
                progressbar.PerformStep();
                if (MaxLenReset != -1 && progressbar.Value >= MaxLenReset)
                    progressbar.Value = 0;
                progressbar.Update();
            }
            else
            {
                BeginInvoke(new MyDelegateInitProgressBar(DelegateInitProgressBarPerformStep), GetDelegateAction(progressbar, MaxLenReset));
            }
            if (newText.Length > 0)
                UpdateGroupBoxText(isMainThread, newText);
            System.Windows.Forms.Application.DoEvents();
        }
        private void UpdateGroupBoxText(bool isMainThread, string newText)
        {
            if (isMainThread)
            {
                label_RomScanLabel.Text = newText;
                label_RomScanLabel.Update();
                label_RomScanLabel.Refresh();
            }
            else
                BeginInvoke(new MyDelegateSetLabelText(DelegateSetLabelText), GetDelegateAction(label_RomScanLabel, newText));
            System.Windows.Forms.Application.DoEvents();
        }
        private void UpdateText(System.Windows.Forms.Control control, string newText, bool isMainThread)
        {
            if (isMainThread)
            {
                control.Text = newText;
                control.Update();
            }
            else
                BeginInvoke(new MyDelegateSetControlText(DelegateSetControlText), GetDelegateAction(control, newText));
            System.Windows.Forms.Application.DoEvents();
        }
        private void UpdateTextBoxText(System.Windows.Forms.TextBox control, string newText, bool isMainThread)
        {
            if (isMainThread)
            {
                control.Text = newText;
                control.Update();
            }
            else
                BeginInvoke(new MyDelegateSetTextBoxText(DelegateSetTextBoxText), GetDelegateAction(control, newText));
            System.Windows.Forms.Application.DoEvents();
        }
        private void ResetProgressBar(int max, bool isMainThread)
        {
            if (max == 0)
                return;
            if (isMainThread)
            {
                progressBar_ROMs.Minimum = 1;
                progressBar_ROMs.Maximum = max;
                progressBar_ROMs.Step = 1;
                progressBar_ROMs.Value = 1;
                progressBar_ROMs.Enabled = true;
                progressBar_ROMs.Visible = true;
                progressBar_ROMs.Update();
            }
            else
                BeginInvoke(new MyDelegateInitProgressBar(DelegateInitProgressBar), GetDelegateAction(progressBar_ROMs, max));
            System.Windows.Forms.Application.DoEvents();
        }
        public void SendStatus(string Msg, bool isMainThread = true, bool writeToConsole = true)
        {
            if (writeToConsole)
                InfoLogging(Msg, false);
            UpdateTextBoxText(textBoxStatus,Msg,isMainThread);
        }
        private void CreateCacheForDisplaySystemIcons(string systemName)
        {
            DeleteImageList(systemName);
            DisplaySystemIcons(systemName);
        }
        private List<string> CreateCacheForDisplaySystemIcons(bool SetComboBoxSystemSelectedItem = true, List<string> systemNames = null)
        {
            if (systemNames == null)
                systemNames = GetSystemNames();
            foreach (string systemName in systemNames)
                CreateCacheForDisplaySystemIcons(systemName);
            if (SetComboBoxSystemSelectedItem)
                toolStripComboBoxSystem.Text = systemNames[0];
            return systemNames;
        }
        private void DisplayOrHideProgressGroup(bool display, int Maximum = 0)
        {
            groupBox_ProgressBar.Enabled = groupBox_ProgressBar.Visible = display;
            label_GameConsoleLabel.Enabled = label_GameConsoleLabel.Visible = display;
            label_RomScanLabel.Enabled = label_RomScanLabel.Visible = display;
            progressBar_ROMs.Enabled = display;
            button_CancelScan.Enabled = display;
            myListView.Enabled = myListView.Visible  = display == false;
            toolStripComboBoxSystem.Enabled  = display == false;
            toolStripComboBoxIconDisplay.Enabled  = display == false;
            SetAdvanceOptions(display == false);
            settingsToolStripMenuItem.Enabled  = display == false;
            if (display)
            {
                if (Maximum > 0)
                {
                    progressBar1.Enabled = progressBar1.Visible = display;
                    progressBar1.Minimum = 1;
                    progressBar1.Maximum = Maximum;
                    progressBar1.Step = 1;
                    progressBar1.Value = 1;
                    progressBar1.Show();
                }
                groupBox_ProgressBar.Show();
                label_GameConsoleLabel.Show();
                label_RomScanLabel.Show();
                progressBar_ROMs.Show();
                button_CancelScan.Show();
                ResetWorkStatus();
            }
            else
            {
                groupBox_ProgressBar.Hide();
                label_GameConsoleLabel.Hide();
                progressBar1.Hide();
                label_RomScanLabel.Hide();
                progressBar_ROMs.Hide();
                button_CancelScan.Hide();
            }
            Show();
            Update();
        }
        private string GetImageListFile(string systemName, int index)
        {
            string imageListSuffix = index == 1 ? ".ImageList1.GameLauncher" : ".ImageList2.GameLauncher";
            return Path.Combine(dataDirPath, $"Cache_{systemName}{imageListSuffix}");
        }
        private void DeleteImageList(string systemName)
        {
            if (systemName == null)
                return;
            string imageList1Path = GetImageListFile(systemName, 1);
            string imageList2Path = GetImageListFile(systemName, 2);
            if (File.Exists(imageList1Path))
                File.Delete(imageList1Path);
            if (File.Exists(imageList2Path))
                File.Delete(imageList2Path);
            previousCollections.Remove(systemName);
        }
        private void DeleteImageList(int systemID) => DeleteImageList(GetSystemNameByID(systemID));
        private void DeleteImageList()
        {
            List<string> SystemNames = GetSystemNames(false);
            foreach (string systemName in SystemNames)
                DeleteImageList(systemName);
        }
        private PreviousCollectedImages GetSavedCollectionData(string systemName)
        {
            if (systemName.Length > 0)
            {
                string imageList1Path = GetImageListFile(systemName, 1);
                string imageList2Path = GetImageListFile(systemName, 2);
                if (File.Exists(imageList1Path) && File.Exists(imageList2Path))
                {
                    PreviousCollectedImages previousCollectedImages = new PreviousCollectedImages();
                    previousCollectedImages.list1 = SerializableImageList.Load(imageList1Path, Properties.Settings.Default.largeIconSize);
                    previousCollectedImages.list2 = SerializableImageList.Load(imageList2Path, Properties.Settings.Default.smallIconSize);
                    if (previousCollectedImages.list1 != null && previousCollectedImages.list2 != null)
                        return previousCollectedImages;
                }
            }
            return null;
        }
        private void SaveCollectionData(string systemName, PreviousCollectedImages previousCollectedImages)
        {
            DeleteImageList(systemName);
            string imageList1Path = GetImageListFile(systemName, 1);
            string imageList2Path = GetImageListFile(systemName, 2);
            SerializableImageList.Save(previousCollectedImages.list1, imageList1Path);
            SerializableImageList.Save(previousCollectedImages.list2, imageList2Path);
        }
        private void TaskToProcessImageList(string imgFile, int setId, int listID, int i)
        {
            if (setId == 0)
                tmpImageList1[listID].Images.Add(romList[index: i].Title, System.Drawing.Image.FromFile(imgFile));
            else
                tmpImageList2[listID].Images.Add(romList[index: i].Title, System.Drawing.Image.FromFile(imgFile));
        }
        // Note: Below results show that there's very little difference when using more than 6 threads for Properties.Settings.Default.MaxNumberOfPairThreadsPerList
        // Performance for 1959 Roms (NES)
        // 1  Threads = 26.56 seconds
        // 2  Threads = 17.51 seconds
        // 4  Threads = 15.56 seconds
        // 6  Threads = 14.46 seconds
        // 8  Threads = 14.44 seconds
        // 10 Threads = 14.44 seconds
        private void TaskToProcessImageList(int setId, int listID, int startPos, int lastPos)
        {
            string imgFile = "";
            int i = 0;
            try
            {
                if (tmpImageList1 == null || tmpImageList2 == null)
                    return;
                for (i = startPos; i < lastPos; i++)
                {
                    imgFile = romList[index: i].ImagePath;
                    try
                    {
                        TaskToProcessImageList(imgFile, setId, listID, i);
                    }
                    catch (Exception ex)
                    {
                        DbErrorLogging(ex, $"TaskToProcessImageList exception thrown '{ex.Message}' while processing bad image file {imgFile}! Removing this file from the database.", $"Var(setId={setId}, listID={listID}, startPos={startPos}, lastPos={lastPos}, imgFile={imgFile}, i={i})");
                        UpdateDB($"DELETE FROM Images WHERE FilePath = \"{imgFile}\"");
                        UpdateDB($"UPDATE Roms SET ImagePath = \"\" WHERE ImagePath = \"{imgFile}\"");
                        imgFile = GetDefaultImageBuildPath();
                        TaskToProcessImageList(imgFile, setId, listID, i);
                    }
                }
            }
            catch (Exception ex)
            {
                DbErrorLogging(ex, $"TaskToProcessImageList exception thrown '{ex.Message}' while processing image file {imgFile}!", $"Var(setId={setId}, listID={listID}, startPos={startPos}, lastPos={lastPos}, imgFile={imgFile}, i={i})");
            }
        }
        public bool DisplaySystemIcons(string systemName, bool resetPagination = true, int multithreadMethod = 1, bool isFiltered = false) // ToDo: After full testing is complete, only keep the Parallel.For, and maybe the none threading logic.  Do delete the Task threading logic.
        {
            bool oneSystem = IsSpecificSystemSelected(systemName); // systemName.Length > 0 && systemName != ITEM_ANY;
            if (!oneSystem && resetPagination)
                DisableAdvanceOptions();
            else
                SetAdvanceOptions();
            if (resetPagination)
            {
                numericUpDown_Paginator.Value = 0;
                numericUpDown_Paginator.Enabled = false;
            }
            using (new CursorWait())
            {
                textBoxStatus.Text = $"Collecting ROM data for {systemName}. Please standby....";
                if (oneSystem && Properties.Settings.Default.usePreviousCollectionCache && previousCollections.ContainsKey(systemName))
                {
                    //Now assigning First imageList as LargeImageList to the ListView...
                    myListView.LargeImageList = previousCollections[systemName].list1;
                    //assigning the second imageList as SmallImageList to the ListView...   
                    myListView.SmallImageList = previousCollections[systemName].list2;
                    romList = previousCollections[systemName].roms;
                    if (romList.Count > Properties.Settings.Default.MaxRomCountBeforeSettingPagination && resetPagination)
                    {
                        int MaxRange = (((int)numericUpDown_Paginator.Value) + 1) * Properties.Settings.Default.MaxPaginationPerPage;
                        numericUpDown_Paginator.Enabled = true;
                        if (numericUpDown_Paginator.Value == 0)
                            numericUpDown_Paginator.Maximum = romList.Count / Properties.Settings.Default.MaxPaginationPerPage;
                        romList.RemoveRange(MaxRange, romList.Count - MaxRange);
                    }
                }
                else
                {
                    if (oneSystem && isFiltered == false && !db.Get(systemName, ref romList))
                        return false;
                    else if (systemName == ITEM_FAVORITE) 
                    {
                        List<Rom> romListFavorite = new List<Rom>();
                        if (!db.Get(ref romListFavorite, "WHERE Favorite = true") || romListFavorite.Count == 0)
                            return false;
                        romList = romListFavorite;
                    }
                    // ToDo: add cache for pagination using value of numericUpDown_Paginator.Value
                    if (resetPagination)
                    {
                        if (romList.Count > Properties.Settings.Default.MaxRomCountBeforeSettingPagination)
                        {
                            int MaxRange = (((int)numericUpDown_Paginator.Value) + 1) * Properties.Settings.Default.MaxPaginationPerPage;
                            numericUpDown_Paginator.Enabled = true;
                            if (numericUpDown_Paginator.Value == 0)
                                numericUpDown_Paginator.Maximum = romList.Count / Properties.Settings.Default.MaxPaginationPerPage;
                            romList.RemoveRange(MaxRange, romList.Count - MaxRange);
                        }
                        else
                        {
                            numericUpDown_Paginator.Value = 0;
                            numericUpDown_Paginator.Enabled = false;
                        }
                    }
                    PreviousCollectedImages previousCollectedImages = GetSavedCollectionData(systemName);
                    if (previousCollectedImages != null && previousCollectedImages.list1.Images.Count == romList.Count)
                    {
                        myListView.LargeImageList = previousCollectedImages.list1;
                        myListView.SmallImageList = previousCollectedImages.list2;
                    }
                    else
                    {
                        System.Windows.Forms.ImageList myImageList1 = new ImageList();
                        myImageList1.ImageSize = new Size(Properties.Settings.Default.largeIconSize, Properties.Settings.Default.largeIconSize);
                        System.Windows.Forms.ImageList myImageList2 = new ImageList();
                        myImageList2.ImageSize = new Size(Properties.Settings.Default.smallIconSize, Properties.Settings.Default.smallIconSize);
                        if (Properties.Settings.Default.MaxNumberOfPairThreadsPerList > 0 && romList.Count > Properties.Settings.Default.MaxNumberOfPairThreadsPerList * 3)
                        {
                            startPosList = new List<int>();
                            tmpImageList1 = new ImageList[Properties.Settings.Default.MaxNumberOfPairThreadsPerList];
                            tmpImageList2 = new ImageList[Properties.Settings.Default.MaxNumberOfPairThreadsPerList];
                            for (int i = 0; i < Properties.Settings.Default.MaxNumberOfPairThreadsPerList; ++i)
                            {
                                startPosList.Add(romList.Count / Properties.Settings.Default.MaxNumberOfPairThreadsPerList * i);

                                ImageList tmpImage1 = new ImageList();
                                tmpImage1.ImageSize = new Size(Properties.Settings.Default.largeIconSize, Properties.Settings.Default.largeIconSize);
                                tmpImageList1[i] = tmpImage1;
                                ImageList tmpImage2 = new ImageList();
                                tmpImage2.ImageSize = new Size(Properties.Settings.Default.smallIconSize, Properties.Settings.Default.smallIconSize);
                                tmpImageList2[i] = tmpImage2;
                            }
                            startPosList.Add(romList.Count);
                            if (multithreadMethod == 0)
                            {
                                for (int idx = 0; idx < Properties.Settings.Default.MaxNumberOfPairThreadsPerList; idx++)
                                {
                                    TaskToProcessImageList(0, idx, startPosList[idx], startPosList[idx + 1]);
                                    TaskToProcessImageList(1, idx, startPosList[idx], startPosList[idx + 1]);
                                }
                            }
                            else if (multithreadMethod == 1)
                            {
                                Parallel.For(0, Properties.Settings.Default.MaxNumberOfPairThreadsPerList, idx =>
                                {
                                    TaskToProcessImageList(0, idx, startPosList[idx], startPosList[idx + 1]);
                                    TaskToProcessImageList(1, idx, startPosList[idx], startPosList[idx + 1]);
                                });
                            }
                            else
                            {
                                System.Threading.Tasks.Task[] tasks = new System.Threading.Tasks.Task[Properties.Settings.Default.MaxNumberOfPairThreadsPerList * 2];
                                for (int i = 0; i < Properties.Settings.Default.MaxNumberOfPairThreadsPerList; ++i)
                                {
                                    int idx = i; // Need to use temporary variable for System.Threading.Tasks.Task thread logic to work.
                                    tasks[idx] = System.Threading.Tasks.Task.Factory.StartNew(() => TaskToProcessImageList(0, idx, startPosList[idx], startPosList[idx + 1]));
                                    tasks[idx + Properties.Settings.Default.MaxNumberOfPairThreadsPerList] = System.Threading.Tasks.Task.Factory.StartNew(() => TaskToProcessImageList(1, idx, startPosList[idx], startPosList[idx + 1]));
                                }
                                System.Threading.Tasks.Task.WaitAll(tasks);
                            }
                            for (int i = 0; i < Properties.Settings.Default.MaxNumberOfPairThreadsPerList; ++i)
                            {
                                foreach (System.Drawing.Image img in tmpImageList1[i].Images)
                                    myImageList1.Images.Add(img);
                                foreach (System.Drawing.Image img in tmpImageList2[i].Images)
                                    myImageList2.Images.Add(img);
                            }
                        }
                        else
                        {
                            // Implementation to do this without multithreading when there's only a few ROM's to process.
                            for (int i = 0; i < romList.Count; i++)
                                myImageList1.Images.Add(romList[index: i].Title, System.Drawing.Image.FromFile(romList[i].ImagePath));
                            for (int i = 0; i < romList.Count; i++)
                                myImageList2.Images.Add(romList[index: i].Title, System.Drawing.Image.FromFile(romList[i].ImagePath));
                        }
                        //Now assigning First imageList as LargeImageList to the ListView...
                        myListView.LargeImageList = myImageList1;
                        //assigning the second imageList as SmallImageList to the ListView...
                        myListView.SmallImageList = myImageList2;
                    }
                    textBoxStatus.Text = $"Setting titles for the {romList.Count} games (ROM's)";
                    myListView.Items.Clear();
                    // Here's a bottleneck which slows down display of over 1500 ROM's
                    for (int i = 0; i < romList.Count; i++)
                        myListView.Items.Add(romList[i].FilePath, romList[i].Title, i);
                    if (romList.Count <= Properties.Settings.Default.MaxRomCountBeforeSettingPagination && oneSystem
                        && (previousCollectedImages == null || previousCollectedImages.list1.Images.Count != romList.Count))
                    {
                        PreviousCollectedData previousCollectedData = new PreviousCollectedData();
                        previousCollectedData.list1 = myListView.LargeImageList;
                        previousCollectedData.list2 = myListView.SmallImageList;
                        previousCollectedData.roms = romList;
                        if (Properties.Settings.Default.usePreviousCollectionCache)
                            previousCollections[systemName] = previousCollectedData;
                        SaveCollectionData(systemName, previousCollectedData);
                    }
                }

                textBoxStatus.Text = $"Returned {romList.Count} games.";
            }
            return true;
        }
        public string[] GetEmulatorsBasePaths()
        {
            if (Properties.Settings.Default.EmulatorsBasePath.Contains(";"))
                return Properties.Settings.Default.EmulatorsBasePath.Split(';');
            if (Properties.Settings.Default.EmulatorsBasePath.Contains(","))
                return Properties.Settings.Default.EmulatorsBasePath.Split(',');
            string[] dirs = new string[1];
            dirs[0] = Properties.Settings.Default.EmulatorsBasePath;
            return dirs;
        }
        public static bool DirectoriesExists(string dirs)
        {
            if (IsNullOrEmpty(dirs))
                return false;
            if (dirs.Contains(";"))
            {
                string[] directories = dirs.Split(';');
                foreach (var dir in directories)
                    if (Directory.Exists(dir))
                        return true;
            }
            if (dirs.Contains(","))
            {
                string[] directories = dirs.Split(',');
                foreach (var dir in directories)
                    if (Directory.Exists(dir))
                        return true;
            }
            else
                return Directory.Exists(dirs);
            return false;
        }
        public static string GetEmulatorsBasePath_sub()
        {
            if (DirectoriesExists(Properties.Settings.Default.EmulatorsBasePath))
                return Properties.Settings.Default.EmulatorsBasePath;
            foreach (var path in COMMON_EMULATOR_PATHS)
            {
                if (File.Exists(path))
                    return path;
                if (File.Exists($"{path}s"))
                    return $"{path}s";
            }
            return @"C:\Emulators";
        }
        public static string GetEmulatorsBasePath()
        {
            string emulatorsBasePath = GetEmulatorsBasePath_sub();
            if (IsNullOrEmpty(Properties.Settings.Default.EmulatorsBasePath))
                Properties.Settings.Default.EmulatorsBasePath = emulatorsBasePath;
            return emulatorsBasePath;
        }
        public string GetDefaultImageBuildPath() => GetPropertyDataPath(DEFAULTIMAGEFILENAME);
        public string GetDefaultImagePath_sub(string dbPath)
        {
            string imgPath = Properties.Settings.Default.DefaultImagePath;
            if (Properties.Settings.Default.DefaultImagePath != null && Properties.Settings.Default.DefaultImagePath.Length > 0)
            {
                if (File.Exists(Properties.Settings.Default.DefaultImagePath))
                    return Properties.Settings.Default.DefaultImagePath;
            }
            string defaultImagePath = GetDefaultImageBuildPath(); 
            if (File.Exists(defaultImagePath))
                return defaultImagePath;
            string dbPathDir = Path.GetDirectoryName(dbPath);
            defaultImagePath = Path.Combine(dbPathDir, DEFAULTIMAGEFILENAME); // This allows user defined default image if image located same directory as DB.
            if (File.Exists(defaultImagePath))
                return defaultImagePath;
            dbPathDir = Path.GetDirectoryName(dbPathDir);
            defaultImagePath = Path.Combine(dbPathDir, DEFAULTIMAGEFILENAME); // This allows user defined default image if image located in parent directory of DB base directory.
            return File.Exists(defaultImagePath) ? defaultImagePath : Path.Combine(binDirPath, DEFAULTIMAGEFILENAME);
        }
        public string GetDefaultImagePath(string dbPath)
        {
            string defaultImagePath = GetDefaultImagePath_sub(dbPath);
            if (IsNullOrEmpty(Properties.Settings.Default.DefaultImagePath))
                Properties.Settings.Default.DefaultImagePath = defaultImagePath;
            return defaultImagePath;
        }
        public void RescanSelectedSystem(string systemName = null, bool doImageScan = false)
        {
            if (systemName == null)
                systemName = toolStripComboBoxSystem.Text;
            string romDir = GetGameSystemRomPath(systemName);
            string systemPath = romDir == null ? "" : Path.GetDirectoryName(romDir);
            if (romDir == null || !Directory.Exists(systemPath))
            {
                ErrorMessage($"Can not perform a scan for emulator {systemName} because invalid path\n\"{systemPath}\"", "Path Error", "RescanSelectedSystem", "Either romDir is null, or system path does NOT exists");
                return;
            }
            DeleteImageList(systemName);
            EmulatorExecutables emulatorExecutables = GetEmulatorPaths(systemPath);
            label_GameConsoleLabel.Text = $"Rescanning path {systemPath} for new ROM's";
            textBoxStatus.Text = $"Rescanning path {systemPath} for new ROM's";
            textBoxStatus.Update();
            DisplayOrHideProgressGroup(true);
            InitializeRomsInDatabaseForSystem_Arg arg = new InitializeRomsInDatabaseForSystem_Arg();
            arg.emulatorDir = systemPath;
            arg.emulatorExecutables = emulatorExecutables;
            arg.isMainThread = false;
            arg.scanImageDir = doImageScan;
            arg.hideGroup = true;
            BeginInvoke(new MyDelegateInitializeRomsInDatabaseForSystem(DelegateInitializeRomsInDatabaseForSystem), GetDelegateAction(this, arg));
        }
        public void ScanForNewRomsOnAllSystems(bool doImageScan = false)
        {
            foreach (var systemName in toolStripComboBoxSystem.Items)
                RescanSelectedSystem(systemName.ToString(), doImageScan);
        }
        public bool ValidatePropertiesSettings(bool checkPathsExists = false)
        {
            bool validationSuccess = true;
            bool updatePropertiesSettings = false;
            // The following is only needed if there's data corruption
            /////////////////////////////////////////////////////////////////////////////
            if (Properties.Settings.Default.largeIconSize > 256)
            {
                Properties.Settings.Default.largeIconSize = 256;
                updatePropertiesSettings = true;
            }
            else if (Properties.Settings.Default.largeIconSize < 8)
            {
                Properties.Settings.Default.largeIconSize = 64;
                updatePropertiesSettings = true;
            }
            if (Properties.Settings.Default.smallIconSize > 256)
            {
                Properties.Settings.Default.smallIconSize = 64;
                updatePropertiesSettings = true;
            }
            else if (Properties.Settings.Default.smallIconSize < 8)
            {
                Properties.Settings.Default.smallIconSize = 8;
                updatePropertiesSettings = true;
            }
            if (Properties.Settings.Default.MaxNumberOfPairThreadsPerList > 10)
            {
                Properties.Settings.Default.MaxNumberOfPairThreadsPerList = 10;
                updatePropertiesSettings = true;
            }
            else if (Properties.Settings.Default.MaxNumberOfPairThreadsPerList < 0)
            {
                Properties.Settings.Default.MaxNumberOfPairThreadsPerList = 0;
                updatePropertiesSettings = true;
            }
            /////////////////////////////////////////////////////////////////////////////
            if (checkPathsExists)
            {
                if (!File.Exists(Properties.Settings.Default.DbPath))
                {
                    Properties.Settings.Default.DbPath = null;
                    Properties.Settings.Default.DbPath = GetDbPath();
                    updatePropertiesSettings = true;
                }
                if (!File.Exists(Properties.Settings.Default.DefaultImagePath))
                {
                    Properties.Settings.Default.DefaultImagePath = null;
                    Properties.Settings.Default.DefaultImagePath = GetDefaultImagePath(Properties.Settings.Default.DbPath);
                    updatePropertiesSettings = true;
                }
                if (!DirectoriesExists(Properties.Settings.Default.EmulatorsBasePath))
                {
                    string badEmulatorsBasePath = Properties.Settings.Default.EmulatorsBasePath;
                    Properties.Settings.Default.EmulatorsBasePath = GetEmulatorsBasePath();
                    if (DirectoriesExists(Properties.Settings.Default.EmulatorsBasePath))
                    {
                        if (!RedoDbInit($"Emulator path does NOT exists\n\"{badEmulatorsBasePath}\"\nDo you want to erase GameLauncher database and perform a rescan using the following path:\n\"{Properties.Settings.Default.EmulatorsBasePath}\"", "Invalid Emulator Path!!!"))
                        {
                            validationSuccess = false;
                            Properties.Settings.Default.EmulatorsBasePath = badEmulatorsBasePath;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Emulator path does NOT exists\n\"{badEmulatorsBasePath}\"\nLaunching emulators with correct ROM/game path may fail.\nThis can be fixed by using the Settings option to select a valid path, and perform a rescan.", "Invalid Emulator Path", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        validationSuccess = false;
                        Properties.Settings.Default.EmulatorsBasePath = badEmulatorsBasePath;
                    }
                    updatePropertiesSettings = true;
                }
            }
            if (updatePropertiesSettings)
                Properties.Settings.Default.Save();
            return validationSuccess;
        }
        private void DisableAdvanceOptions() => SetAdvanceOptions(true, true);
        private void SetAdvanceOptions(bool disableAdvanceOptions = false, bool contextMenuOnDisable = false)
        {
            if (disableAdvanceOptions || Properties.Settings.Default.disableAdvanceOptions)
            {
                toolStripMenuItem_ROMParentMenu.Enabled = false;
                changeViewToolStripMenuItem.Enabled = false;
                toolStripMenuItem_ImagesParentMenu.Enabled = false;
                toolStripMenuItem_MiscUtilParentMenu.Enabled = false;
                contextMenuStrip1.Enabled = contextMenuOnDisable;
                toolStripMenuItemChangeDefaultEmulator.Enabled = false;
                toolStripMenuItem_AddNewGameSystemEmulator.Enabled = false;
            }
            else
            {
                toolStripMenuItem_ROMParentMenu.Enabled = true;
                changeViewToolStripMenuItem.Enabled = true;
                toolStripMenuItem_ImagesParentMenu.Enabled = true;
                toolStripMenuItem_MiscUtilParentMenu.Enabled = true;
                contextMenuStrip1.Enabled = true;
                toolStripMenuItemChangeDefaultEmulator.Enabled = true;
                toolStripMenuItem_AddNewGameSystemEmulator.Enabled = true;
            }
        }
        private string GetEmulatorStartScanPath()
        {
            SelectFolderDialog fbd = new SelectFolderDialog();
            fbd.Description = "Enter base path to start emulator scanning.";
            return fbd.ShowDialog() == DialogResult.OK ? fbd.SelectedPath : null;
        }
        private Rom GetSelectedROM(bool displayErrorMessageIfMissing = true)
        {
            foreach (ListViewItem item in myListView.SelectedItems)
            {
                string key = item.Name;
                foreach(Rom rom in romList)
                {
                    if (rom.FilePath == key)
                        return rom;
                }
            }
            if (displayErrorMessageIfMissing)
                MessageBox.Show("Can't find selected game, because of data change. The changed data has caused image list to be invalid. Try changing combo system selection, or restarting program to reinitialize ROM image list with the changed data.", "Invalid Image List");
            return null;
        }
        private GameSystem GetGameSystem(int systemID) => GetGameSystem(GetSystemNameByID(systemID));
        private GameSystem GetGameSystem(string systemName = null)
        {
            List<GameSystem> gameSystems = null;
            db.Get(ref gameSystems, $"WHERE Name like \"{(systemName == null ? toolStripComboBoxSystem.Text : systemName)}\"");
            return gameSystems.Count > 0 ? gameSystems[0] : null;
        }
        private bool IsSupportedCompressFile(string romFile) => SUPPORTED_COMPRESSION_FILE.Contains(Path.GetExtension(romFile).ToLower());
        private void PlaySelectedRom(TriBoolean decompress = TriBoolean.None)
        {
            Rom rom = GetSelectedROM();
            PlaySelectedRom(rom, -1, decompress);
        }
        public enum TriBoolean
        {
            None,
            False,
            True,
        }
        public void PlaySelectedRom(Rom rom, int PreferredEmulatorID = -1, TriBoolean decompress = TriBoolean.None)
        {
            if (rom == null)
                return;
            lock (myListView)
            {
                if (!File.Exists(rom.FilePath))
                {
                    ErrorMessage($"The selected ROM file no longer exists:\n'{rom.FilePath}'", "ROM not exists!", "PlaySelectedRom");
                    return;
                }
                int preferredEmulator = PreferredEmulatorID == -1 ? (rom.PreferredEmulatorID > 0 ? rom.PreferredEmulatorID : 1) : PreferredEmulatorID;
                string emulatorExecutable = GetEmulatorExecutable(rom.System, preferredEmulator);
                if (emulatorExecutable.Length == 0)
                {
                    ErrorMessage($"There is no emulator for index emulator index {preferredEmulator}\nUse the GameSystem Editor to set a valid emulator executable.", "Missing Emulator!!!", "PlaySelectedRom");
                    return;
                }
                if (!File.Exists(emulatorExecutable))
                {
                    ErrorMessage($"Missing emulator executable {emulatorExecutable}\nUse the GameSystem Editor to set a valid emulator executable.", "Missing Emulator Executable!!!", "PlaySelectedRom", "Path does not exist.");
                    return;
                }
                waitingShellExecuteToComplete = true;
                if (Properties.Settings.Default.useJoystickController && noJoystickFound == false && threadJoyStick != null)
                {
                    //threadJoyStick.Abort();
                    threadJoyStickAborting = true;
                }
                db.Update(new Mru(rom.FilePath, DateTime.Now.ToString("yyyyMMDDHHmm")));
                FormWindowState prevWinState = this.WindowState;
                this.WindowState = FormWindowState.Minimized;
                GameSystem gameSystem = GetGameSystem(rom.System);
                bool emulatorRequiresDecompression = EmulatorRequiresDecompression(emulatorExecutable, gameSystem.DecompressFile[preferredEmulator - 1] ? 1 : 0);
                if (decompress != TriBoolean.None)
                    emulatorRequiresDecompression = decompress == TriBoolean.True;
                if (emulatorRequiresDecompression && IsSupportedCompressFile(rom.FilePath))
                    DecompressFileToTemporaryFolder(rom.FilePath, emulatorExecutable);
                else
                    ExecuteEmulator(rom.FilePath, emulatorExecutable);
                waitingShellExecuteToComplete = false;
                this.WindowState = prevWinState;
                SetMRU();
                if (Properties.Settings.Default.useJoystickController && noJoystickFound == false)
                {
                    threadJoyStickAborting = false;
                    threadJoyStick = new Thread(PollJoystick);
                    threadJoyStick.Start();
                }
            }
        }
        public void ExecuteEmulator(string romZipFile, string emulatorExecutable)
        {
            try
            {
                Process process = Process.Start(new ProcessStartInfo($"\"{emulatorExecutable}\"", $"\"{romZipFile}\"")
                {
                    UseShellExecute = true
                });
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                ErrorMessage(ex, $"ExecuteEmulator exception thrown \"{ex.Message}\" for emulatorExecutable ({emulatorExecutable}) and ROM file {romZipFile}!", "ExecuteEmulator Error", $"Input Arg(romZipFile={romZipFile}, emulatorExecutable={emulatorExecutable})");
            }
        }
        private static string GetParentName(string filePath, string defaultValue = "")
        { // Note: To get full parent name, use System.IO.Path.GetDirectoryName
            DirectoryInfo parent = System.IO.Directory.GetParent(filePath);
            return parent == null ? defaultValue : parent.Name;
        }
        private static string GetParent_Parent(string filePath, bool getFullPath = false)
        {
            string parentName = "";
            if (getFullPath)
            {
                parentName = System.IO.Path.GetDirectoryName(filePath);
                if (!IsEmptyOrNull(parentName)) 
                {
                    try
                    {
                        string parentParent = System.IO.Path.GetDirectoryName(parentName);
                        if (!IsEmptyOrNull(parentParent))
                            return parentParent;
                    }
                    catch { }
                    return parentName;
                }
                return "";
            }
            DirectoryInfo parent = System.IO.Directory.GetParent(filePath);
            if (parent != null)
            {
                DirectoryInfo parentParent = parent.Parent;
                parentName = parentParent != null ? $"{parentParent.Name}_{parent.Name}" : $"{parent.Name}";
                parentName = parentName.Replace(":", "_");
                parentName = parentName.Replace("\\", "_");
            }
            return parentName;
        }
        public static string GetTempDirSuffix(string filePath, int index = -1)
        {
            string parentName = GetParent_Parent(filePath);
            return index > -1 ? $"{parentName}_{System.IO.Path.GetFileName(filePath)}_{index}" : $"{parentName}_{System.IO.Path.GetFileName(filePath)}";
        }
        public void DecompressFileToTemporaryFolder(string romZipFile, string emulatorExecutable)
        {
            int qtyFilesInZip = 0;
            string preferredExtension = EmulatorPreferredExtension(emulatorExecutable);
            string firstFileExtracted = null;
            string romFile = null;
            string extRom = Path.GetExtension(romZipFile).ToLower();
            using (TempDirStorage tempDirStorage = new TempDirStorage(GetTempDirSuffix(romZipFile)))
            {
                if (SUPPORTED_COMPRESSION_FILE.Contains(extRom))
                {
                    using (IDecompressArchive archive = CompressArchive.Open(romZipFile))
                    {
                        archive.ExtractToDirectory(tempDirStorage.tempDir);
                        foreach (var name in archive.GetNames())
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempDirStorage.tempDir, name));
                            if (Directory.Exists(destinationPath))
                                continue;
                            if (firstFileExtracted == null)
                                firstFileExtracted = destinationPath;
                            string ext = Path.GetExtension(destinationPath);
                            if (ext.Equals(preferredExtension, StringComparison.OrdinalIgnoreCase))
                                romFile = destinationPath;
                            ++qtyFilesInZip;
                        }
                    }
                }
                if (romFile == null && firstFileExtracted != null)
                    romFile = firstFileExtracted;
                if (romFile != null)
                    ExecuteEmulator(romFile, emulatorExecutable);
                else
                    ErrorMessage($"Could not find ROM file in compress file {romZipFile}","No File Found", "DecompressFileToTemporaryFolder", $"emulatorExecutable={emulatorExecutable}");
            }
        }
        private void AddToFilter_AutoCompleteCustomSource(string text)
        {
            if (text.Length < 3 || myListView.Items.Count == 0)
                return;
            if (Properties.Settings.Default.AutoCompleteCustomSourceLiveUpdate)
                toolStripTextBox_Filter.AutoCompleteCustomSource.Add(text); //This keeps randomly triggering crash, so it's disabled by default

            // Maybe ToDo: Consider adding a counter or timestamp to table FilterAutoCompleteCustomSource, so as to remove value not used recently or often
            // ToDo: Add spell checker like Hunspell to check spelling before adding text to DB
            UpdateDB($"INSERT OR REPLACE INTO FilterAutoCompleteCustomSource (Source) VALUES (\"{text}\")");
        }
        #endregion /////////////////////////////////////////////////////////////////////////////////
        #region Event merthods
        private void myListView_ItemMouseHover(Object sender, ListViewItemMouseHoverEventArgs e)
        {
            if (romList.Count == 0)
                return;
            Rom rom = romList[e.Item.Index];
            e.Item.ToolTipText = $"FileName='{rom.FilePath}'";
            string QtyPlayers = "";
            if (rom.QtyPlayers > 1)
                QtyPlayers = $"MultiPlayer={rom.QtyPlayers}; ";
            string Version = "";
            if (rom.Version.Length > 0)
                Version = $"; Ver={rom.Version}";
            string Status = "";
            if (rom.Status.Length > 0)
                Status = $"; Ver={rom.Status}";
            string NotesCore = "";
            if (rom.NotesCore.Length > 0)
                NotesCore = $"; Ver={rom.NotesCore}";
            string NotesUser = "";
            if (rom.NotesUser.Length > 0)
                NotesUser = $"; Ver={rom.NotesUser}";
            textBoxStatus.Text = $"{QtyPlayers}FileName='{rom.FilePath}'; FileSize={rom.RomSize}; ImagePath='{rom.ImagePath}'{Version}{Status}{NotesCore}{NotesUser}";
        }
        private void myListViewContextMenu_Play_Click(object sender, EventArgs e)=> PlaySelectedRom();
        private void myListViewContextMenu_RenameROM_Click(object sender, EventArgs e) => RenameROM();
        private void myListViewContextMenu_DeleteROM_Click(object sender, EventArgs e) => DeleteROM();
        private void myListViewContextMenu_AssignPreferredEmulator_Click(object sender, EventArgs e) => AssignPreferredEmulator();
        private void myListViewContextMenu_ChangeTitle_Click(object sender, EventArgs e) => ChangeTitle();
        private void myListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = myListView.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }
        private void myListView_OnDbClick(object sender, EventArgs e) => PlaySelectedRom();
        private void myListView_OnFormClosing(object sender, FormClosingEventArgs e)
        {
            threadJoyStickAborting = true;
            Properties.Settings.Default.Maximised = WindowState == FormWindowState.Maximized;
            SavePropertySettingsToDB();
            Properties.Settings.Default.Save();
            TempDirStorage.DeleteTempMultithreadDir();
        }
        //public bool IsKeyPress(System.Windows.Forms.Keys keys = Keys.None) => (System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Alt & System.Windows.Forms.Keys.Control & System.Windows.Forms.Keys.Shift) == keys;
        public bool IsKeyPress(System.Windows.Forms.Keys keys = Keys.None)
        {
            int mods = (int)System.Windows.Forms.Control.ModifierKeys;
            int sp_keys = (int)System.Windows.Forms.Keys.Alt + (int)System.Windows.Forms.Keys.Control + (int)System.Windows.Forms.Keys.Shift;
            return  (mods & sp_keys) == (int)keys;
        }
        private void myListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Divide)
            {
                if (toolStripComboBoxSystem.SelectedIndex == 0)
                    toolStripComboBoxSystem.SelectedIndex = toolStripComboBoxSystem.Items.Count - 1;
                else
                    toolStripComboBoxSystem.SelectedIndex -= 1;
            }
            else if (e.KeyCode == Keys.Multiply)
            {
                if (toolStripComboBoxSystem.SelectedIndex == toolStripComboBoxSystem.Items.Count - 1)
                    toolStripComboBoxSystem.SelectedIndex = 0;
                else
                    toolStripComboBoxSystem.SelectedIndex += 1;
            }
            else if (e.KeyCode == Keys.F5 && IsKeyPress(System.Windows.Forms.Keys.Control))
                FilterRoms(); // Code never reaches this point, because of GUI shortcut.  This is just here as a reminder that this key combo is taken.
            else if (e.KeyCode == Keys.F5 && IsKeyPress(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Alt))
                FilterRoms();// ToDo: Repurpose this key combination, since this feature already has a key combo. (ctrl-F5)
            else if (e.KeyCode == Keys.F5 && IsKeyPress())
            {
                if (IsRepeat("F5", MAX_SECONDS_TO_WAIT_F5))
                    return;
                PlaySelectedRom();
            }
            else if (e.KeyCode == Keys.F6 && IsKeyPress())
            {
                if (IsRepeat("F6"))
                    return;
                if (this.WindowState == FormWindowState.Normal)
                    this.WindowState = FormWindowState.Maximized;
                else if (this.WindowState == FormWindowState.Maximized)
                    this.WindowState = FormWindowState.Normal;
            }
            else if (e.KeyCode == Keys.F7 && IsKeyPress())
            {
                if (IsRepeat("F7"))
                    return;
                this.WindowState = FormWindowState.Minimized;
            }
            else if (e.KeyCode == Keys.F8 && IsKeyPress())
            {
                if (IsRepeat("F8"))
                    return;
                if (toolStripComboBoxIconDisplay.SelectedIndex == 2)
                    toolStripComboBoxIconDisplay.SelectedIndex = 0;
                else
                    toolStripComboBoxIconDisplay.SelectedIndex += 1;
            }
            else if (e.KeyCode == Keys.F9 && IsKeyPress())
            {
                if (IsRepeat("F9"))
                    return;
                if (toolStripComboBoxIconDisplay.SelectedIndex == 0)
                    toolStripComboBoxIconDisplay.SelectedIndex = 2;
                else
                    toolStripComboBoxIconDisplay.SelectedIndex -= 1;
            }
            else if (e.KeyCode == Keys.F10 && IsKeyPress())
                toolStripComboBoxIconDisplay.SelectedIndex = 0;
            else if (e.KeyCode == Keys.F11 && IsKeyPress())
                toolStripComboBoxIconDisplay.SelectedIndex = 1;
            else if (e.KeyCode == Keys.F12 && IsKeyPress())
                toolStripComboBoxIconDisplay.SelectedIndex = 2;
            else if (e.KeyCode == Keys.Escape && IsKeyPress())
                cancelWork = true;
            else if (e.KeyCode == Keys.Pause && IsKeyPress())
                pauseWork = !pauseWork;
            else if (e.KeyCode == Keys.Pause && IsKeyPress(System.Windows.Forms.Keys.Control))
                pauseWork = true;
            else if (e.KeyCode == Keys.Pause && IsKeyPress(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift))
                pauseWork = false;
            else if (e.KeyCode == Keys.Pause && IsKeyPress(System.Windows.Forms.Keys.Shift))
                pauseInSeconds = 60;
            else if (e.KeyCode == Keys.Pause && IsKeyPress(System.Windows.Forms.Keys.Alt))
                pauseInSeconds = 60 * 10; // 10 Minutes
            else if (e.KeyCode == Keys.Pause && IsKeyPress(System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift))
                pauseInSeconds = 60 * 60; // 1 Hour
            else if (e.KeyCode == Keys.Pause && IsKeyPress(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift))
                pauseInSeconds = 60 * 60 * 10; // 10 Hour
            else if (e.KeyCode == Keys.E && IsKeyPress(System.Windows.Forms.Keys.Shift))
                ResetAllFilters(true);
            else if (e.KeyCode == Keys.E && IsKeyPress(System.Windows.Forms.Keys.Control))
                OpenFileLocation();
            else if (Properties.Settings.Default.disableAdvanceOptions == false)
            { // Make sure to put all context menu items below (except F5)
                if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.Control || e.KeyCode == Keys.ShiftKey)
                {

                }
                else if (e.KeyCode == Keys.Delete)
                    DeleteROM();
                else if (e.KeyCode == Keys.F5 && IsKeyPress(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift))
                    PlaySelectedRom(TriBoolean.False);
                else if (e.KeyCode == Keys.F5 && IsKeyPress(System.Windows.Forms.Keys.Shift))
                    PlaySelectedRom(TriBoolean.True);
                else if (e.KeyCode == Keys.C && IsKeyPress(System.Windows.Forms.Keys.Control))
                    CopyTitleToClipboard();
                else if (e.KeyCode == Keys.V && IsKeyPress(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift))
                    AssignClipboardImage(true);
                else if (e.KeyCode == Keys.V && IsKeyPress(System.Windows.Forms.Keys.Control))
                    AssignClipboardImage();
                else if (e.KeyCode == Keys.I && IsKeyPress(System.Windows.Forms.Keys.Control))
                    ChangeAssignedImage();
                else if (e.KeyCode == Keys.F1 && IsKeyPress(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift))
                    JumpToBrowser("https://www.google.com/search?q={title}", true);
                else if (e.KeyCode == Keys.F1 && IsKeyPress(System.Windows.Forms.Keys.Shift))
                    JumpToBrowser("https://www.google.com/search?q={title}+{system}", true);
                else if (e.KeyCode == Keys.F1 && IsKeyPress(System.Windows.Forms.Keys.Control))
                    JumpToBrowser("https://www.gamesdatabase.org/list.aspx?in=1&searchtext={title}&searchtype=1", true);
                else if (e.KeyCode == Keys.F3 && IsKeyPress())
                    JumpToBrowser("https://www.google.com/search?q={title}+site:launchbox-app.com", true);
                else if (e.KeyCode == Keys.F1 && IsKeyPress())
                    BrowseGameWikipediaPage();
                else if (e.KeyCode == Keys.F2 && IsKeyPress())
                    RenameROM();
                else if (e.KeyCode == Keys.F3 && IsKeyPress(System.Windows.Forms.Keys.Control))
                    JumpToBrowser("https://www.google.com/search?q={title}+Images+site:launchbox-app.com&udm=2", true);
                else if (e.KeyCode == Keys.A && IsKeyPress(System.Windows.Forms.Keys.Control))
                    AddRomToFavorite();
                else if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.P) && IsKeyPress(System.Windows.Forms.Keys.Alt))
                    ChangeViewROMDetails();
                else if ((e.KeyCode == Keys.G && IsKeyPress(System.Windows.Forms.Keys.Control)) || (e.KeyCode == Keys.F4 && IsKeyPress()))
                {
                    toolStripComboBox_GamesWithWithoutImage.Text = GAMES_WITHOUT_IMAGE;
                    FilterRoms();
                }
                else if ((e.KeyCode == Keys.G && IsKeyPress(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)) || (e.KeyCode == Keys.F4 && IsKeyPress(System.Windows.Forms.Keys.Control)))
                {
                    toolStripComboBox_GamesWithWithoutImage.Text = GAMES_WITH_IMAGE;
                    FilterRoms();
                }
                else if ((e.KeyCode == Keys.G && IsKeyPress(System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)) || (e.KeyCode == Keys.F4 && IsKeyPress(System.Windows.Forms.Keys.Shift)))
                {
                    toolStripComboBox_GamesWithWithoutImage.Text = ITEM_ANY;
                    FilterRoms();
                }
                else if (e.KeyCode == Keys.P && IsKeyPress(System.Windows.Forms.Keys.Control))
                    AssignPreferredEmulator();
                else if (e.KeyCode == Keys.T && IsKeyPress(System.Windows.Forms.Keys.Control))
                    ChangeTitle();
            }
        }
        private void button_CancelScan_Click(object sender, EventArgs e)
        {
            cancelWork = true;
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Settings form_Settings = new Form_Settings();
            form_Settings.ShowDialog();
            if (form_Settings.IconSizeChanged)
                CreateCacheForDisplaySystemIcons(toolStripComboBoxSystem.Text);
            if (form_Settings.EmulatorsBasePathChanged)
                RedoDbInit($"Emulator path has changed to\n\"{Properties.Settings.Default.EmulatorsBasePath}\"\nDo you want to erase GameLauncher database and perform a new scan?","Rescan?", MessageBoxIcon.Question);
           if (!File.Exists(Properties.Settings.Default.DbPath))
            {
                connection = Db.CreateConnection($"Filename={Properties.Settings.Default.DbPath}");
                connection.Open();
                CreateAndInitSqlTables();
            }
            PopulateValidRomTypes();
            UpdateRomAndImageSubfolder();
            SetAdvanceOptions();
            SetImageFolderWatcher();
        }
        private void toolStripMenuItemChangeDefaultEmulator_Click(object sender, EventArgs e) => SetEmulatorExecute();
        private void System_Change(object sender, EventArgs e)
        {
            SavePersistenceVariable("toolStripComboBoxSystem", toolStripComboBoxSystem.Text);
            if (formInitiated)
                FilterRoms();
            else
                DisplaySystemIcons(toolStripComboBoxSystem.Text);
            if (toolStripTextBox_Filter.Text.Length > 0)
                FilterOutRomsFromList(true);
        }
        private void Display_Change(object sender, EventArgs e)
        {
            SavePersistenceVariable("toolStripComboBoxIconDisplay", toolStripComboBoxIconDisplay.Text);
            // Excluding View.SmallIcon;
            if (toolStripComboBoxIconDisplay.SelectedIndex == 0)
                myListView.View = View.LargeIcon;
            else if (toolStripComboBoxIconDisplay.SelectedIndex == 1)
                myListView.View = View.List;
            else if (toolStripComboBoxIconDisplay.SelectedIndex == 2)
                myListView.View = View.Tile;
        }
        private void FilterOutRomsFromList(bool redisplayImageListFirst, bool useRegex, bool searchAllRomsInAllSystems = false)
        {
            try
            {
                if (redisplayImageListFirst || useRegex)
                    DisplaySystemIcons(toolStripComboBoxSystem.Text);
                Regex rg = new Regex(toolStripTextBox_Filter.Text);
                for (int i = 0; i < romList.Count; ++i)
                {
                    bool regexMatch = true;
                    bool searchMatch = true;
                    if (useRegex)
                    {
                        MatchCollection matchedAuthors = rg.Matches(romList[i].Title);
                        regexMatch = matchedAuthors.Count > 0;
                    }
                    else
                        searchMatch = romList[i].Title.Contains(toolStripTextBox_Filter.Text, StringComparison.OrdinalIgnoreCase);
                    if (!searchMatch || !regexMatch)
                    {
                        int index = myListView.Items.IndexOfKey(romList[i].FilePath);
                        if (index != -1)
                            myListView.Items.RemoveAt(index);
                    }
                }
                AddToFilter_AutoCompleteCustomSource(toolStripTextBox_Filter.Text);
            }
            catch (Exception ex)
            {
                DbErrorLogging(ex, $"FilterOutRomsFromList exception thrown \"{ex.Message}\" for text {toolStripTextBox_Filter.Text}!", $"Input Arg(redisplayImageListFirst={redisplayImageListFirst}, useRegex={useRegex})");
            }
            lastSearchStr = toolStripTextBox_Filter.Text;
        }
        private void FilterOutRomsFromList(bool alwaysRedisplayImageListFirst = false, bool searchAllRomsInAllSystems = false)
        {
            if (IsKeyPress(System.Windows.Forms.Keys.Shift))
            {
                toolStripTextBox_Filter.Text = "";
                alwaysRedisplayImageListFirst = true;
            }
            try
            {
                bool redisplayImageListFirst = alwaysRedisplayImageListFirst || !(toolStripTextBox_Filter.Text.Length > lastSearchStr.Length && toolStripTextBox_Filter.Text.StartsWith(lastSearchStr));
                if (alwaysRedisplayImageListFirst == false && ((toolStripTextBox_Filter.Text.Length == 0 && lastSearchStr.Length == 0) || toolStripTextBox_Filter.Text.Equals(lastSearchStr)))
                    return;
                bool useRegex = IsKeyPress(System.Windows.Forms.Keys.Control);
                // ToDo: Add logic to skip the following logic if less than 2 seconds have passed
                string[] RegexKeys = { "$", "^", "|", "*", ".", "?", "+", "[", "]", "(", ")", "\\" };
                foreach (string key in RegexKeys)
                {
                    if (toolStripTextBox_Filter.Text.Contains(key))
                    {
                        useRegex = true;
                        break;
                    }
                }
                FilterOutRomsFromList(redisplayImageListFirst, useRegex, searchAllRomsInAllSystems);
            }
            catch (Exception ex)
            {
                DbErrorLogging(ex, $"FilterOutRomsFromList exception thrown \"{ex.Message}\" for text {toolStripTextBox_Filter.Text}!", $"Input Arg(alwaysRedisplayImageListFirst={alwaysRedisplayImageListFirst})");
            }
        }
        private void toolStripTextBox_Filter_Change(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBox_Filter.Text.Length >= lastSearchStr.Length)
                    FilterOutRomsFromList();
                else
                {
                    if (newFilterValue.Length == 0)
                        FilterOutRomsFromList();
                    lastSearchStr = "";
                }
            }
            catch (Exception ex)
            {
                DbErrorLogging(ex, $"toolStripTextBox_Filter_Change exception thrown \"{ex.Message}\" for text {toolStripTextBox_Filter.Text}!");
            }
            newFilterValue = toolStripTextBox_Filter.Text;
        }
        private void toolStripTextBox_Filter_Click(object sender, EventArgs e) => FilterOutRomsFromList();
        private void toolStripTextBox_Filter_DbClick(object sender, EventArgs e)=> FilterOutRomsFromList(true);

        private void toolStripMenuItem_GoogleImageAtLaunchBox_Click(object sender, EventArgs e) => JumpToBrowser("https://www.google.com/search?q={title}+Images+site:launchbox-app.com&udm=2", true);
        private void toolStripMenuItemSearchAll_Click(object sender, EventArgs e) 
        {
            if (IsKeyPress(System.Windows.Forms.Keys.Shift))
                toolStripTextBox_Filter.Text = "";
            if (toolStripTextBox_Filter.Text.Length == 0)
                return;
            bool useRegex = IsKeyPress(System.Windows.Forms.Keys.Control);
            List<Rom> romListAllSystem = new List<Rom>();
            db.Get(-1, ref romListAllSystem, $"%{toolStripTextBox_Filter.Text}%");
            const int MaxAllowedResults = 1000;
            if (romListAllSystem.Count > MaxAllowedResults)
            {
                WarnMessage($"Found too many results ({romListAllSystem.Count}) for text \"{toolStripTextBox_Filter.Text}\"\nMax allowed is {MaxAllowedResults}.", $"Found {romListAllSystem.Count}");
                return;
            }
            if (romListAllSystem.Count == 0) 
            {
                WarnMessage($"No results found for text \"{toolStripTextBox_Filter.Text}\"", "Found 0");
                return; 
            }
            romList = romListAllSystem;
            DisplaySystemIcons("");
        }
        private void ToolStripMenuItemChangeViewROMDetails_Click(object sender, EventArgs e) => ChangeViewROMDetails();
        private void changeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameSystem gameSystem = GetGameSystem();
            Form_GameSystemEditor gameSystemEditor = new Form_GameSystemEditor(gameSystem);
            gameSystemEditor.ShowDialog();
            if (gameSystemEditor.Ok)
                db.Update(gameSystem);
        }
        private void toolStripMenuItem_RegexRenameFiles_Click(object sender, EventArgs e)
        {
            Form_RegexRenameFiles formRegexRenameFiles = new Form_RegexRenameFiles();
            formRegexRenameFiles.ShowDialog();
        }
        private void toolStripMenuItemRescanAllRoms_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main(DelegateRedoDbInit), GetDelegateAction(this, null));
        private void toolStripMenuItemScanNewRomsAllSystems_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main(DelegateScanForNewRomsOnAllSystems), GetDelegateAction(this, false));
        private void toolStripMenuItemScanSelectedSystemNewRoms_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main(DelegateRescanSelectedSystem), GetDelegateAction(this, false));
        private void toolStripMenuItem_CleanScanSelectedSystemNewRoms_Click(object sender, EventArgs e)
        {
            DialogResult results = MessageBox.Show($"Are you sure you want to clean and rescan all ROM's for game console system {toolStripComboBoxSystem.Text}?\nThis will first remove all {toolStripComboBoxSystem.Text} ROM details from GameLauncher database, which may include any user edited information?", "Delete ROM's from DB", MessageBoxButtons.YesNo);
            if (results == DialogResult.No)
                return;
            db.DeleteAllRomsInSystem(GetSystemIndex(toolStripComboBoxSystem.Text));
            BeginInvoke(new MyDelegateForm_Main(DelegateRescanSelectedSystem), GetDelegateAction(this, false));
        }
        private void toolStripMenuItem_ScanSelectedSystemNewImages_Click(object sender, EventArgs e) => GetImagesAndWait(GetGameSystemImagePath());
        private void toolStripMenuItem_ImageSearchSelectedDir_Click(object sender, EventArgs e) => ImageSearchSelectedDir();
        private void toolStripMenuItem_CreateImageListCache_Click(object sender, EventArgs e)=>BeginInvoke(new MyDelegateForm_Main(DelegateCreateCacheForDisplaySystemIcons), GetDelegateAction(this, true));
        private void toolStripMenuItem_ScanSelectedSystemRomsAndImages_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main(DelegateRescanSelectedSystem), GetDelegateAction(this, true));
        private void toolStripMenuItem_ScanAllSystemsNewRomsAndImages_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main(DelegateScanForNewRomsOnAllSystems), GetDelegateAction(this, true));
        private void toolStripMenuItem_DeleteDupImagesNotInDB_Click(object sender, EventArgs e)
        {
            DialogResult results = MessageBox.Show($"Do you want to delete all image files that are not in the GameLauncher database?\nClick YES for silent deletion.\nClick NO to DELETE the image files but give a prompt before each deletion.\nClick CANCEL to exit this option (No Deletions).", "Delete Image", MessageBoxButtons.YesNoCancel);
            if (results == DialogResult.Cancel)
                return;
            BeginInvoke(new MyDelegateForm_Main(DelegateDeleteImageFilesNotInDatabase), GetDelegateAction(this, results == DialogResult.Yes));
        }
        private void toolStripMenuItem_DeleteDupRomsByTitleSameSystem_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main_DeleteDuplicateBy(DelegateDeleteDuplicateRomFilesInDatabase), GetDelegateAction(this, DeleteDuplicateBy.DuplicateTitleInSameSystem));
        private void toolStripMenuItem_DeleteDupRomsByTitleAnySystem_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main_DeleteDuplicateBy(DelegateDeleteDuplicateRomFilesInDatabase), GetDelegateAction(this, DeleteDuplicateBy.DuplicateTitleInAnySystem));
        private void toolStripMenuItem_DeleteDupRomsByChecksum_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main_DeleteDuplicateBy(DelegateDeleteDuplicateRomFilesInDatabase), GetDelegateAction(this, DeleteDuplicateBy.DuplicateChecksum));
        private void toolStripMenuItem_DeleteDupRomsByNameSimplifiedSameSystem_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main_DeleteDuplicateBy(DelegateDeleteDuplicateRomFilesInDatabase), GetDelegateAction(this, DeleteDuplicateBy.DuplicateNameSimplifiedInSameSystem));
        private void toolStripMenuItem_DeleteDupRomsByNameSimplifiedAnySystem_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main_DeleteDuplicateBy(DelegateDeleteDuplicateRomFilesInDatabase), GetDelegateAction(this, DeleteDuplicateBy.DuplicateNameSimplifiedInAnySystem));
        private void toolStripMenuItem_DeleteDupRomsByNameOrgSameSystem_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main_DeleteDuplicateBy(DelegateDeleteDuplicateRomFilesInDatabase), GetDelegateAction(this, DeleteDuplicateBy.DuplicateNameOrgInSameSystem));
        private void toolStripMenuItem_DeleteDupRomsByNameOrgAnySystem_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main_DeleteDuplicateBy(DelegateDeleteDuplicateRomFilesInDatabase), GetDelegateAction(this, DeleteDuplicateBy.DuplicateNameOrgInAnySystem));
        private void toolStripMenuItem_DeleteDupRomsByCompressedSameSystem_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main_DeleteDuplicateBy(DelegateDeleteDuplicateRomFilesInDatabase), GetDelegateAction(this, DeleteDuplicateBy.DuplicateCompressedInSameSystem));
        private void toolStripMenuItem_DeleteDupRomsByCompressedAnySystem_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main_DeleteDuplicateBy(DelegateDeleteDuplicateRomFilesInDatabase), GetDelegateAction(this, DeleteDuplicateBy.DuplicateCompressedInAnySystem));
        private void toolStripMenuItem_PopulateGameDetailsDB_Click(object sender, EventArgs e)
        {
            qtyTotalChanges = 0;
            using (new CursorWait())
            {
                if (!AddXmlGameDataToGameDetails(GetPropertyDataPath("Atari 2600.xml")))
                    return;
                if (!AddXmlGameDataToGameDetails(GetPropertyDataPath("Neo Geo.xml")))
                    return;
                if (!AddXmlGameDataToGameDetails(GetPropertyDataPath("Nintendo 64.xml")))
                    return;
                if (!AddXmlGameDataToGameDetails(GetPropertyDataPath("Nintendo DS.xml")))
                    return;
                if (!AddXmlGameDataToGameDetails(GetPropertyDataPath("Nintendo Entertainment System.xml")))
                    return;
                if (!AddXmlGameDataToGameDetails(GetPropertyDataPath("Nintendo Game Boy Advance.xml")))
                    return;
                if (!AddXmlGameDataToGameDetails(GetPropertyDataPath("Sega Genesis.xml")))
                    return;
                if (!AddXmlGameDataToGameDetails(GetPropertyDataPath("Sega Master System.xml")))
                    return;
                if (!AddXmlGameDataToGameDetails(GetPropertyDataPath("Sony Playstation.xml")))
                    return;
                if (!AddXmlGameDataToGameDetails(GetPropertyDataPath("Super Nintendo Entertainment System.xml")))
                    return;
                if (!AddN64RdxToGameDetails(GetPropertyDataPath("N64.rdx")))
                    return;
                if (!AddMultiplayerDetailsToGameDetails(GetPropertyDataPath("MultiplayerGameList.Atari.txt")))
                    return;
                if (!AddMultiplayerDetailsToGameDetails(GetPropertyDataPath("MultiplayerGameList.NES.txt")))
                    return;
                if (!AddMultiplayerDetailsToGameDetails(GetPropertyDataPath("MultiplayerGameList.Sega.txt")))
                    return;
                if (!AddMultiplayerDetailsToGameDetails(GetPropertyDataPath("MultiplayerGameList.SNES.txt")))
                    return;
                SendStatus($"Update complete with a total of {qtyTotalChanges} items added to DB", true);
            }
        }
        private void toolStripMenuItem_AddGameDetailsToGameLauncherDb_Click(object sender, EventArgs e)
        {
            List<Rom> rom_list = new List<Rom>();
            List<string> gameDetailsSystemNames = GetGameDetailsSystemNames();
            Dictionary<int, string> systemNamesAndID = GetSystemNamesAndID();
            using (new CursorWait(taskbarProcess, progressBar_BottomRight))
            {
                if (!db.Get(ref rom_list))
                    return;
                ResetWorkStatus(rom_list.Count);
                foreach (Rom rom in rom_list)
                {
                    if (DidCancelButtonGetPressed(true))
                    {
                        SendStatus($"AddGameDetailsToGameLauncherDb cancelled with only processing {qtyProcess} of {rom_list.Count} ROM's.", true);
                        break;
                    }
                    string fileName = Path.GetFileName(rom.FilePath);
                    IncProgress();
                    SendStatus($"Processing ROM {rom.Title}; {qtyProcess} out of {rom_list.Count}", true);
                    //if (rom.Title.Contains("1080 Snowboarding", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    fileName = Path.GetFileName(rom.FilePath);
                    //}
                    string system = Contains(systemNamesAndID[rom.System], gameDetailsSystemNames);
                    int prev_qtyChanges = qtyChanges;
                    if (system.Length != 0 && UpdateRom(rom, $"WHERE FileName like \"{fileName}\" and System = \"{system}\""))
                        ++qtyChanges;
                    else if (UpdateRom(rom, $"WHERE FileName like \"{fileName}\""))
                        ++qtyChanges;
                    else if (system.Length != 0 && UpdateRom(rom, $"WHERE Title like \"{rom.Title}\" and System = \"{system}\""))
                        ++qtyChanges;
                    else if (system.Length != 0 && UpdateRom(rom, $"WHERE NameSimplified like \"{rom.NameSimplified}\" and System = \"{system}\""))
                        ++qtyChanges;
                    else if (system.Length != 0 && UpdateRom(rom, $"WHERE Compressed like \"{rom.Compressed}\" and System = \"{system}\""))
                        ++qtyChanges;
                    else if (UpdateRom(rom, $"WHERE Title like \"{rom.Title}\""))
                        ++qtyChanges;
                    else if (UpdateRom(rom, $"WHERE NameSimplified like \"{rom.NameSimplified}\""))
                        ++qtyChanges;
                    else if (UpdateRom(rom, $"WHERE Compressed like \"{rom.Compressed}\""))
                        ++qtyChanges;
                    if (prev_qtyChanges == qtyChanges && !rom.Title.Contains("&"))
                    {
                        SendStatus($"Could not find information for game {rom.Title} in GameDetails database", true);
                    }
                }
                SendStatus($"Update complete with {qtyChanges} ROM's updated in DB out of {rom_list.Count}", true);
                if (qtyChanges > 0)
                    PopulateDynamicComboBoxMenu();
            }
        }
        public static string GetDictItem(ref Dictionary<int, string> dict, int index, string s = "") => dict.ContainsKey(index) ? dict[index] : s;
        private void toolStripMenuItem_SearchMatchingImage_Click(object sender, EventArgs e) => SearchMatchingImage(SearchAlgorithm.ExactMatch);
        private void toolStripMenuItem_ResetYearAndRatingFieldsInDB_Click(object sender, EventArgs e)
        {
            UpdateDB("UPDATE Roms SET Year = 0, Rating = \"\" WHERE Rating IS NULL OR Year IS NULL");
            textBoxStatus.Text = "Update complete. Removed NULL values from DB for fields Year and Rating";
        }
        private void toolStripMenuItem_ResetRomTitleCompress_Click(object sender, EventArgs e)
        {
            List<Rom> rom_list = new List<Rom>();
            using (new CursorWait(taskbarProcess, progressBar_BottomRight))
            {
                if (!db.Get(ref rom_list))
                    return;
                ResetWorkStatus(rom_list.Count);
                foreach (Rom rom in rom_list)
                {
                    if (DidCancelButtonGetPressed(true))
                    {
                        SendStatus($"ResetRomTitleCompress cancelled with only processing {qtyProcess} of {rom_list.Count} ROM's.", true);
                        return;
                    }
                    IncProgress();
                    ++qtyTotalProcess;
                    SendStatus($"Processing ROM {rom.Title}; {qtyProcess} out of {rom_list.Count}", true);
                    string Compressed = rom.Compressed;
                    string Title = rom.Title;
                    string NameOrg = rom.NameOrg;
                    string NameSimplified = rom.NameSimplified;
                    rom.NameOrg = GetFileNameWithoutExtensionAndWithoutBin(rom.FilePath);
                    rom.NameSimplified = ConvertToNameSimplified(rom.NameOrg);
                    rom.Compressed = ConvertToCompress(rom.NameOrg);
                    rom.Title = ConvertToTitle(rom.NameOrg);
                    if (!rom.NameOrg.Equals(NameOrg) || !rom.NameSimplified.Equals(NameSimplified) || !rom.Compressed.Equals(Compressed) || !rom.Title.Equals(Title))
                    {
                        SendStatus($"Updating ROM {rom.Title} compress field from \"{Compressed}\" to \"{rom.Compressed}\"; {qtyProcess} out of {rom_list.Count}", true);
                        ++qtyChanges;
                        ++qtyTotalChanges;
                        db.Update(rom);
                    }
                }
                SendStatus($"Updated {qtyChanges} ROM's in DB out of {rom_list.Count}", true);
            }
        }
        private void toolStripMenuItem_ResetImageTitleCompressInDB_Click(object sender, EventArgs e)
        {
            List<GameImage> gameImages = null;
            using (new CursorWait(taskbarProcess, progressBar_BottomRight))
            {
                if (!db.Get(ref gameImages))
                    return;
                ResetWorkStatus(gameImages.Count);
                foreach (GameImage gameImage in gameImages)
                {
                    if (DidCancelButtonGetPressed(true))
                    {
                        SendStatus($"ResetImageTitleCompressInDB cancelled with only processing {qtyProcess} of {gameImages.Count} images.", true);
                        return;
                    }
                    IncProgress();
                    ++qtyTotalProcess;
                    SendStatus($"Processing GameImage {gameImage.NameSimplified}; {qtyProcess} out of {gameImages.Count}", true);
                    string Title = gameImage.Title;
                    string Compressed = gameImage.Compressed;
                    string NameOrg = gameImage.NameOrg;
                    string NameSimplified = gameImage.NameSimplified;
                    gameImage.NameOrg = GetFileNameWithoutExtensionAndWithoutBin(gameImage.FilePath);
                    gameImage.NameSimplified = ConvertToNameSimplified(gameImage.NameOrg);
                    gameImage.Compressed = ConvertToCompress(gameImage.NameOrg);
                    gameImage.Title = ConvertToTitle(gameImage.NameOrg);
                    if (!gameImage.Title.Equals(Title) || !gameImage.NameOrg.Equals(NameOrg) || !gameImage.NameSimplified.Equals(NameSimplified) || !gameImage.Compressed.Equals(Compressed))
                    {
                        SendStatus($"Updating GameImage {NameOrg} to Title={gameImage.Title}; NameOrg={gameImage.NameOrg}; Compressed={gameImage.Compressed}; NameSimplified={gameImage.NameSimplified}; {qtyProcess} out of {gameImages.Count}", true);
                        ++qtyChanges;
                        ++qtyTotalChanges;
                        db.Update(gameImage);
                    }
                }
                SendStatus($"Updated {qtyChanges} GameImage's in DB out of {gameImages.Count}", true);
            }
        }
        private void toolStripMenuItem_ResetCompressNames_Click(object sender, EventArgs e)
        {
            toolStripMenuItem_ResetRomTitleCompress_Click(sender, e);
            toolStripMenuItem_ResetImageTitleCompressInDB_Click(sender, e);
            toolStripMenuItem_SearchMatchingImage_Click(sender, e);
        }
        private void OpenRecentFile(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            string filePath = (string)menuItem.Tag;
            PlaySelectedRom(db.Get_Rom(filePath));
        }
        private void numericUpDown_Paginator_Changed(object sender, EventArgs e)
        {
            int MinRange = (int)numericUpDown_Paginator.Value * Properties.Settings.Default.MaxPaginationPerPage;
            int SystemID = GetSystemIndex(toolStripComboBoxSystem.Text);
            db.Get(SystemID, ref romList, "", "", $"limit {MinRange} , {Properties.Settings.Default.MaxPaginationPerPage} ");
            DisplaySystemIcons("", false);
        }

        private void toolStripMenuItem_CopyTitleToClipboard_Click(object sender, EventArgs e) => CopyTitleToClipboard();
        private void toolStripMenuItem_ConvertRomToZip_Click(object sender, EventArgs e) => CompressFilesInSelectedFolder("zip");
        private void toolStripMenuItem_ConvertRomTo7z_Click(object sender, EventArgs e) => CompressFilesInSelectedFolder("7z");
        private void toolStripMenuItem_ConvertRomToTar_Click(object sender, EventArgs e) => CompressFilesInSelectedFolder("tar");
        private void toolStripMenuItem_ConvertRomToGzip_Click(object sender, EventArgs e) => CompressFilesInSelectedFolder("gz");
        private void toolStripMenuItem_ConvertRomToBZ2_Click(object sender, EventArgs e) => CompressFilesInSelectedFolder("bz2");
        private void toolStripMenuItem_ConvertRomToLZ_Click(object sender, EventArgs e) => CompressFilesInSelectedFolder("lz");
        private void toolStripMenuItem_ConvertRomToRAR_Click(object sender, EventArgs e) => CompressFilesInSelectedFolder("rar");
        private void toolStripMenuItem_ConvertPngToJpg_Click(object sender, EventArgs e) => ConvertFilesToJpg();
        private void toolStripMenuItem_CompressAllRoms_Click(object sender, EventArgs e) => CompressFilesInSelectedFolder(true, true);
        private void toolStripMenuItem_ConvertAllPngToJpg_Click(object sender, EventArgs e) => ConvertFilesToJpg(true, true);
        void Filter_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (toolStripTextBox_Filter.Text.Length > 0 && toolStripTextBox_Filter.Text.Equals(newFilterValue))
                {
                    newFilterValue = "";
                }
            }
            catch 
            {
            }
        }
        private void CreateChecksumForRoms(int count, Rom rom)
        {
            if (DidCancelButtonGetPressed(false))
            {
                SendStatus($"CreateChecksumForRoms cancelled with only processing {qtyProcess} of {count} ROM's.", false);
                return;
            }
            ++qtyProcess;
            ++qtyTotalProcess;
            SendStatus($"Processing ROM {rom.Title}; {qtyProcess} out of {count}", false);
            string Checksum = rom.Checksum;
            string CompressChecksum = rom.CompressChecksum;
            if (Checksum != null && CompressChecksum != null && Checksum.Length > 0 && CompressChecksum.Length > 0)
                return;
            rom.Checksum = GetChecksumStr(rom.FilePath);
            rom.CompressChecksum = GetRomCompressChecksum(rom.FilePath, true);
            if (!rom.Checksum.Equals(Checksum) || !rom.CompressChecksum.Equals(CompressChecksum))
            {
                SendStatus($"Updating ROM {rom.Title} Checksum field from \"{Checksum}\" to \"{rom.Checksum}\"; {qtyProcess} out of {count}", false);
                ++qtyChanges;
                ++qtyTotalChanges;
                db.Update(rom);
            }
        }
        private int CreateChecksumForRoms(ref List<Rom> rom_list, bool doMultithread = false) // Keep doMultithread false, because there's a bug in multithread implementation. Multithread code creates many files with the same checksum
        {
            int count = rom_list.Count;
            using (new CursorWait())
            {
                if (doMultithread)
                {
                    Parallel.ForEach(rom_list, (rom, loopState) =>
                    {
                        System.Windows.Forms.Application.DoEvents();
                        if (DidCancelButtonGetPressed(doMultithread))
                            loopState.Stop();
                        CreateChecksumForRoms(count, rom);
                        System.Windows.Forms.Application.DoEvents();
                    });
                }
                else
                {
                    foreach(var rom in romList)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        if (DidCancelButtonGetPressed(doMultithread))
                            break;
                        CreateChecksumForRoms(count, rom);
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
                    SendStatus($"Updated {qtyChanges} ROM's in DB out of {rom_list.Count}", true);
            }
            return count;
        }
        private void CreateChecksumForRoms(bool allSystems)
        {
            ResetWorkStatus();
            if (allSystems)
            {
                List<Rom> rom_list = new List<Rom>();
                if (!db.Get(ref rom_list, "WHERE Checksum IS NULL OR CompressChecksum IS NULL OR Checksum = \"\" OR CompressChecksum = \"\""))
                    return;
                qtyWork = CreateChecksumForRoms(ref rom_list);
            }
            else
                qtyWork = CreateChecksumForRoms(ref romList);
            System.Windows.Forms.Application.DoEvents();
            SendStatus($"Updated {qtyChanges} ROM's in DB out of {qtyWork}; Processed {qtyProcess} ROM's", true);
        }
        private void toolStripMenuItem_CreateChecksumForRomsSelectSystem_Click(object sender, EventArgs e) => CreateChecksumForRoms(false);
        private void toolStripMenuItem_CreateChecksumForRomsAllSystem_Click(object sender, EventArgs e) => CreateChecksumForRoms(true);
        private void toolStripMenuItem_CreateChecksumForImagesAllSystem_Click(object sender, EventArgs e) => CreateChecksumForImagesAllSystem();
        private void toolStripMenuItem_PopulateGameDetailsAndAddItToGameLauncher_Click(object sender, EventArgs e)
        {
            toolStripMenuItem_PopulateGameDetailsDB_Click(sender,e);
            toolStripMenuItem_AddGameDetailsToGameLauncherDb_Click(sender,e);
        }
        private void toolStripMenuItem_ResetAllFilters_Click(object sender, EventArgs e) => ResetAllFilters(true);
        private void toolStripMenuItem_DeleteDupRomsByCompressChecksum_Click(object sender, EventArgs e) => BeginInvoke(new MyDelegateForm_Main_DeleteDuplicateBy(DelegateDeleteDuplicateRomFilesInDatabase), GetDelegateAction(this, DeleteDuplicateBy.DuplicateCompressChecksum));
        private void toolStripMenuItem_BrowseGameWikipediaPage_Click(object sender, EventArgs e) => BrowseGameWikipediaPage();

        private void toolStripMenuItem_ResetChecksumForAllSystems_Click(object sender, EventArgs e)
        {
            UpdateDB("UPDATE Roms SET Checksum = \"\";UPDATE Roms SET CompressChecksum = \"\";");
            MessageBox.Show("Cleared ALL checksum and compress-checksum fields in database.");
        }

        private void toolStripMenuItem_ResetChecksumForSelectSystem_Click(object sender, EventArgs e)
        {
            UpdateDB($"UPDATE Roms SET Checksum = \"\" WHERE System = {GetSystemIndex(toolStripComboBoxSystem.Text)};UPDATE Roms SET CompressChecksum = \"\" WHERE System = {GetSystemIndex(toolStripComboBoxSystem.Text)};");
            MessageBox.Show($"Cleared checksum and compress-checksum fields for system {toolStripComboBoxSystem.Text} in database.");
        }

        private void toolStripMenuItem_BackupMainDBTables_Click(object sender, EventArgs e) => BackupDb(BackupType.MainTables);
        private void toolStripMenuItem_BackupRomTable_Click(object sender, EventArgs e) => BackupDb(BackupType.Roms);
        private void toolStripMenuItem_BackupImageTable_Click(object sender, EventArgs e) => BackupDb(BackupType.Images);
        private void toolStripMenuItem_BackupGameSystemTable_Click(object sender, EventArgs e) => BackupDb(BackupType.GameSystem);
        private void toolStripMenuItem_RestoreMainDBTables_Click(object sender, EventArgs e) => RestoreDb(BackupType.MainTables);
        private void toolStripMenuItem_RestoreRomTable_Click(object sender, EventArgs e) => RestoreDb(BackupType.Roms);
        private void toolStripMenuItem_RestoreImageTable_Click(object sender, EventArgs e) => RestoreDb(BackupType.Images);
        private void toolStripMenuItem_RestoreGameSystemTable_Click(object sender, EventArgs e) => RestoreDb(BackupType.GameSystem);
        private void toolStripMenuItem_Emulator1_Click(object sender, EventArgs e) => PlaySelectedRom(GetSelectedROM(),1);
        private void toolStripMenuItem_Emulator2_Click(object sender, EventArgs e) => PlaySelectedRom(GetSelectedROM(),2);
        private void toolStripMenuItem_Emulator3_Click(object sender, EventArgs e) => PlaySelectedRom(GetSelectedROM(),3);
        private void toolStripMenuItem_Emulator4_Click(object sender, EventArgs e) => PlaySelectedRom(GetSelectedROM(),4);
        private void toolStripMenuItem_Emulator5_Click(object sender, EventArgs e) => PlaySelectedRom(GetSelectedROM(),5);
        private void toolStripMenuItem_Emulator6_Click(object sender, EventArgs e) => PlaySelectedRom(GetSelectedROM(),6);
        private void toolStripMenuItem_Emulator7_Click(object sender, EventArgs e) => PlaySelectedRom(GetSelectedROM(),7);
        private void toolStripMenuItem_Emulator8_Click(object sender, EventArgs e) => PlaySelectedRom(GetSelectedROM(),8);
        private void toolStripMenuItem_Emulator9_Click(object sender, EventArgs e) => PlaySelectedRom(GetSelectedROM(),9);
        private void toolStripMenuItem_Emulator10_Click(object sender, EventArgs e) => PlaySelectedRom(GetSelectedROM(),10);
        private void toolStripMenuItem_AddNewGameSystemEmulator_Click(object sender, EventArgs e)
        {
            SelectFolderDialog dialog = new SelectFolderDialog(System.IO.Path.GetDirectoryName(Properties.Settings.Default.DbPath), "Select one or more new Game-System (Emulator) directories to add and scan.");
            if (dialog.ShowDialog(true) == DialogResult.OK)
            {
                foreach (string emulatorDir in dialog.FolderNames)
                    ScanANewEmulatorToDatabase(emulatorDir);
                InfoMessage($"Completed data collection for {dialog.FolderNames.Count} emulator path(s).", "Process Complete");
            }
        }
        private void Filter_OnChanged(object sender, EventArgs e) => FilterRoms();
        private void toolStripMenuItem_GameDatabaseOrg_Click(object sender, EventArgs e) => JumpToBrowser("https://www.gamesdatabase.org/list.aspx?in=1&searchtext={title}&searchtype=1", true);
        private void toolStripMenuItem_OpenFileLocation_Click(object sender, EventArgs e) => OpenFileLocation();
        private void toolStripMenuItem_AddRomToFavorite_Click(object sender, EventArgs e) => AddRomToFavorite();
        private void toolStripMenuItemFindImagesAndFindMatches_Click(object sender, EventArgs e) => ImageSearchSelectedDir(true);
        private void Watcher_OnChanged(object sender, FileSystemEventArgs e) => WatcherChanged(e);
        private void Watcher_OnCreated(object sender, FileSystemEventArgs e) => WatcherCreated(e);
        private void Watcher_OnDeleted(object sender, FileSystemEventArgs e) => WatcherDeleted(e);
        private void Watcher_OnRenamed(object sender, RenamedEventArgs e) => WatcherRenamed(e);
        private void toolStripMenuItem_CreateImageChecksumForMissingChecksum_Click(object sender, EventArgs e) => CreateChecksumForImagesAllSystem(true);
        private void toolStripMenuItem_RemoveImagesMissingInFilesystem_Click(object sender, EventArgs e) => RemoveImagesMissingInFilesystem();
        private void toolStripMenuItem_RemoveRomsMissingInFilesystem_Click(object sender, EventArgs e) => RemoveRomsMissingInFilesystem();
        private void toolStripMenuItem_RemoveMissingRomsAndImagesFromDB_Click(object sender, EventArgs e)
        {
            RemoveRomsMissingInFilesystem();
            RemoveImagesMissingInFilesystem();
        }
        private void toolStripMenuItem_GoogleTitleAndSystem_Click(object sender, EventArgs e) => JumpToBrowser("https://www.google.com/search?q={title}+{system}", true);
        private void toolStripMenuItem_GoogleTitle_Click(object sender, EventArgs e) => JumpToBrowser("https://www.google.com/search?q={title}", true);
        private void toolStripMenuItemRefreshFilter_Click(object sender, EventArgs e) => FilterRoms();
        private void toolStripMenuItem_AssignClipboardImage_Click(object sender, EventArgs e) => AssignClipboardImage();
        private void toolStripMenuItem_ChangeAssignedImage_Click(object sender, EventArgs e) => ChangeAssignedImage();
        private void toolStripMenuItem_GoogleLaunchBox_Click(object sender, EventArgs e) => JumpToBrowser("https://www.google.com/search?q={title}+site:launchbox-app.com", true);
        private void toolStripMenuItem_DecompressSelectedSystem_Click(object sender, EventArgs e) => DecompressFiles(true);
        private void toolStripMenuItem_DecompressSelectedSystemsAndUpdateDB_Click(object sender, EventArgs e) => DecompressFiles(true, true);
        private void toolStripMenuItem_DecompressSelectedAndUpdateDBAndDeleteCompress_Click(object sender, EventArgs e) => DecompressFiles(true, true, true);
        private void toolStripMenuItem_DecompressSelectToFolder_Click(object sender, EventArgs e) => DecompressFiles(true, false, false, true);
        private void toolStripMenuItem_DecompressAllSystems_Click(object sender, EventArgs e) => DecompressFiles(false);
        private void toolStripMenuItem_DecompressAllSystemsAndUpdateDB_Click(object sender, EventArgs e) => DecompressFiles(false, true);
        private void toolStripMenuItem_DecompressAllAndUpdateDBAndDeleteCompress_Click(object sender, EventArgs e) => DecompressFiles(false, true, true);
        private void toolStripMenuItem_DecompressAllToFolder_Click(object sender, EventArgs e) => DecompressFiles(false, false, false, true);
        private void toolStripMenuItem_SelectDirToDecompressToFiles_Click(object sender, EventArgs e)
        {
            SelectFolderDialog dialog = new SelectFolderDialog(System.IO.Path.GetDirectoryName(Properties.Settings.Default.DbPath), "Select directory having compress files which need to be decompress.");
            if (dialog.ShowDialog(false) == DialogResult.OK)
                DecompressFiles(dialog.FolderName, false, false, false);
        }
        private void toolStripMenuItem_SelectDirToDecompressToDir_Click(object sender, EventArgs e)
        {
            SelectFolderDialog dialog = new SelectFolderDialog(System.IO.Path.GetDirectoryName(Properties.Settings.Default.DbPath), "Select directory having compress files which need to be decompress to associated sub-folder having same name as compress file.");
            if (dialog.ShowDialog(false) == DialogResult.OK)
                DecompressFiles(dialog.FolderName, false, false, true);
        }
        private void toolStripMenuItem_ConvertDblExtToSingleExt_Click(object sender, EventArgs e)
        {
            SelectFolderDialog fbd = new SelectFolderDialog();
            fbd.SelectedPath = Properties.Settings.Default.lastDirectoryUserSelected;
            fbd.Description = "Enter path to convert double file extension to single extension";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.lastDirectoryUserSelected = fbd.SelectedPath;
                ConvertDblExtToSingleExt(fbd.SelectedPath);
            }
        }
        private void toolStripMenuItem_ForSelectSystemConvertDblExtToSingleExt_Click(object sender, EventArgs e) => ConvertDblExtToSingleExt(GetGameSystemRomPath(),true);

        private void toolStripMenuItem_RenameLaunchBoxROMFiles_Click(object sender, EventArgs e)
        {
            ///Notes:
            ///Check why 'CSI Unsolved' not matching
            /// Methods to find matching file names
            /// #1 Take the separate words in the file name, and search for file having all the keywords.
            /// #2 Stop grabbing keywords when there's a period found in the string
            /// #3 Remove words like "the, a, and, und, wo" from the keyword list search.
            using (new CursorWait(taskbarProcess, progressBar_BottomRight))
            {
                using (SqliteConnection connection_GameLauncher = Db.CreateConnection($"Filename={GetPropertyDataPath(gameLauncherDbPath)}"))
                {
                    connection_GameLauncher.Open();
                    string romPath = $"{gameLauncherPath}\\Games";
                    string[] files = Directory.GetFiles(romPath, "*", SearchOption.AllDirectories);
                    ResetWorkStatus(files.Length);
                    foreach (string file in files)
                    {
                        if (DidCancelButtonGetPressed(true))
                        {
                            SendStatus($"RenameLaunchBoxROMFiles cancelled with only processing {qtyProcess} of {qtyWork} files in folder {romPath}.", true);
                            return;
                        }
                        IncProgress();
                        string filename = System.IO.Path.GetFileName(file);
                        SendStatus($"Processing file {filename}; {qtyProcess} out of {qtyWork}", true);
                        Rom rom = db.Get_Rom($"%{filename}");
                        if (rom == null)
                            SendStatus($"Warning: Could not find associated file {filename} in GameLauncher DB; {qtyProcess} out of {qtyWork}", true);
                        else if (IsEmptyOrNull(rom.ImagePath) || rom.ImagePath.EndsWith(DEFAULTIMAGEFILENAME, StringComparison.OrdinalIgnoreCase))
                        {
                            SendStatus($"Searching for similar game to file name {filename}; {qtyProcess} out of {qtyWork}", true);
                            string sql = CreateSqlQueryFindSimilarNames(rom.Title);
                            if (IsEmptyOrNull(sql))
                                SendStatus($"File name has to few keywords for accurate lookup {filename}; {qtyProcess} out of {qtyWork}", true);
                            else
                            {
                                string fileNewTitle = GetFirstColStr(sql, connection_GameLauncher);
                                if (IsEmptyOrNull(fileNewTitle))
                                {
                                    SendStatus($"Could not find similar file to {filename}; {qtyProcess} out of {qtyWork}", true);
                                }
                                else
                                {
                                    SendStatus($"Found title {fileNewTitle} which is similar to {filename}; {qtyProcess} out of {qtyWork}", true);
                                    string ext = Path.GetExtension(file);
                                    string gameDir = System.IO.Path.GetDirectoryName(file);
                                    fileNewTitle = string.Join(" ", fileNewTitle.Split(Path.GetInvalidFileNameChars()));
                                    string fileNewName = $"{gameDir}\\{fileNewTitle}{ext}";
                                    if (!file.Equals(fileNewName, StringComparison.OrdinalIgnoreCase) && !File.Exists(fileNewName))
                                    {
                                        File.Move(file, fileNewName);
                                        ++qtyChanges;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            ClearProgress();
            InfoMessage($"Rename process complete. Processed {qtyProcess} files and renamed {qtyChanges} files.", "Complete");
        }
        private void toolStripMenuItem_ReplaceRomGeneralImageWithImportedImages_Click(object sender, EventArgs e)
        {
            SelectFolderDialog dialog = new SelectFolderDialog(System.IO.Path.GetDirectoryName(Properties.Settings.Default.DbPath), 
                $"Select directory to import images to replace images in general image folder {defaultImagePath}.");
            if (dialog.ShowDialog(true) == DialogResult.OK)
            {
                Properties.Settings.Default.lastDirectoryUserSelected = dialog.FolderNames[0];
                foreach (string folderName in dialog.FolderNames)
                    GetImages(folderName, true, true, SearchOption.AllDirectories, true, true, true);
                InfoMessage($"Completed image scan for directory {dialog.FolderNames[0]}.", "Process Complete");
            }
        }
        private void toolStripMenuItem_RemoveImagesNotPartOfSystem_Click(object sender, EventArgs e)
        {
            using (new CursorWait(taskbarProcess, progressBar_BottomRight))
            {
                List<GameSystem> gameSystems = null;
                db.Get(ref gameSystems);
                ResetWorkStatus(gameSystems.Count, true);
                foreach (GameSystem gameSystem in gameSystems)
                {
                    IncProgress();
                    using (SqlReader reader = new SqlReader($"SELECT FilePath from Roms WHERE FilePath like '{gameSystem.RomDirPath}\\%' AND ImagePath NOT LIKE '{gameSystem.ImageDirPath}\\%' AND {GetWhereMissingImageNot()}"))
                    {
                        while (reader.Read())
                        {
                            string FilePath = reader.GetString("FilePath");
                            Rom rom = db.Get_Rom(FilePath);
                            rom.ImagePath = "";
                            ++qtySet2Changes;
                            SendStatus($"Updating ROM {rom.Title}; Change count = {qtySet2Changes}.", true);
                            db.Update(rom);
                        }
                    }
                }
            }
            SendStatus($"RemoveImagesNotPartOfSystem complete with {qtySet2Changes} ROM's updated.", true);
        }
        private void toolStripMenuItem_ImportImagesFromDir_Click(object sender, EventArgs e) => ImportImagesFromDir(false);
        private void toolStripMenuItem_ImportImagesManual_Click(object sender, EventArgs e) => ImportImagesFromDir(true);
        private void toolStripMenuItem_ImportImagesToAllSystemsManual_Click(object sender, EventArgs e) => ImportImagesFromDir(true, true);
        private void toolStripMenuItem_ClearDbErrorLogging_Click(object sender, EventArgs e)
        {
            EraseOldErrorLogging();
            SendStatus("Database error logging has been erased.");
        }
        private void toolStripMenuItem_CreateRomChecksumForMissingChecksum_Click(object sender, EventArgs e) => CreateChecksumForRomsAllSystem(true);
        private void toolStripMenuItem_CreateImageAndRomChecksumForMissingChecksum_Click(object sender, EventArgs e)
        {
            CreateChecksumForRomsAllSystem(true);
            if (!DidCancelButtonGetPressed(true))
                CreateChecksumForImagesAllSystem(true);
        }
        private void toolStripMenuItem_CopyRoms_UserChangesTable_Click(object sender, EventArgs e) => RestoreDb(BackupType.Roms_UserChanges_CopyToRoms);
        private void toolStripMenuItem_SearchMatchingImageConfirm_Click(object sender, EventArgs e) => SearchMatchingImage(SearchAlgorithm.CloseMatch);
        private void toolStripMenuItemFindImagesAndFindMatchesConfirm_Click(object sender, EventArgs e) => ImageSearchSelectedDir(true, true);
        private void toolStripMenuItem_ResetAlternateNameTitleCompress_Click(object sender, EventArgs e)
        {
            List<AlternateNames> list = null;
            using (new TempConnection())
            {
                using (new CursorWait(taskbarProcess, progressBar_BottomRight))
                {
                    if (!db.Get(ref list, Db.connection_AlternateNames))
                        return;
                    ResetWorkStatus(list.Count);
                    foreach (AlternateNames n in list)
                    {
                        if (DidCancelButtonGetPressed(true))
                        {
                            SendStatus($"ResetAlternateNameTitleCompress cancelled with only processing {qtyProcess} of {list.Count} ROM's.", true);
                            return;
                        }
                        IncProgress();
                        ++qtyTotalProcess;
                        SendStatus($"Processing AlternateNames {n.Alternate}; {qtyProcess} out of {list.Count}", true);
                        string Compressed = n.Compressed;
                        string Title = n.Title;
                        string NameSimplified = n.NameSimplified;
                        n.NameSimplified = ConvertToNameSimplified(n.Alternate);
                        n.Compressed = ConvertToCompress(n.Alternate);
                        n.Title = ConvertToTitle(n.Alternate);
                        if (!n.NameSimplified.Equals(NameSimplified) || !n.Compressed.Equals(Compressed) || !n.Title.Equals(Title))
                        {
                            SendStatus($"Updating AlternateNames {n.Alternate} compress field from \"{Compressed}\" to \"{n.Compressed}\"; {qtyProcess} out of {list.Count}", true);
                            ++qtyChanges;
                            ++qtyTotalChanges;
                            db.Update(n, Db.connection_AlternateNames);
                        }
                    }
                    SendStatus($"Updated {qtyChanges} AlternateNames in DB out of {list.Count}", true);
                }
            }
        }
        private void toolStripMenuItem_PopulateExternalImageDB_Click(object sender, EventArgs e)
        {
            using (new TempConnection())
            {
                SelectFolderDialog fbd = new SelectFolderDialog();
                fbd.SelectedPath = Properties.Settings.Default.lastDirectoryUserSelected;
                fbd.Description = "Enter path to search images to add to ImageExternal database.";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.lastDirectoryUserSelected = fbd.SelectedPath;
                    using (new CursorWait(taskbarProcess, progressBar_BottomRight))
                    {
                        string[] imgFiles = GetFilesByExtensions(fbd.SelectedPath, SearchOption.AllDirectories, SUPPORTED_IMAGE_FILES);
                        ResetWorkStatus(imgFiles.Length, true);
                        ResetProgressBar(imgFiles.Length, true);
                        foreach (var imgFile in imgFiles)
                        {
                            if (DidCancelButtonGetPressed(true))
                            {
                                SendStatus($"GetImages cancelled with only processing {qtyProcess} of {imgFiles.Length} image files.", true);
                                return;
                            }
                            IncProgress();
                            AddImagePathToDb(imgFile, true, true, false, false,
                                false, false, false, null, false, Db.connection_ImageExternal);
                            SendStatus($"Processing image file {imgFile}; {qtyProcess} of {imgFiles.Length}", true);
                        }
                        InfoMessage($"Image scan complete for path {fbd.SelectedPath}.");
                    }
                }
            }
        }
        private void toolStripMenuItem_SearchMatchingImageConfirm2_Click(object sender, EventArgs e) => SearchMatchingImage(SearchAlgorithm.SimilarMatch);
        private void toolStripMenuItem_GamesWithoutImage_Click(object sender, EventArgs e)
        {
            toolStripComboBox_GamesWithWithoutImage.Text = GAMES_WITHOUT_IMAGE;
            FilterRoms();
        }
        private void toolStripMenuItem_GamesWithImage_Click(object sender, EventArgs e)
        {
            toolStripComboBox_GamesWithWithoutImage.Text = GAMES_WITH_IMAGE;
            FilterRoms();
        }
        private void toolStripMenuItem_GamesWithOrWithoutImage_Click(object sender, EventArgs e)
        {
            toolStripComboBox_GamesWithWithoutImage.Text = ITEM_ANY;
            FilterRoms();
        }
        private void toolStripMenuItem_SearchMatchingImageConfirm3_Click(object sender, EventArgs e) => SearchMatchingImage(SearchAlgorithm.AnyPartMatch);
        private void toolStripMenuItem_SearchMatchingImage1_Click(object sender, EventArgs e) => SearchMatchingImage(SearchAlgorithm.CloseMatch,false);
        private void toolStripMenuItem_SearchMatchingImageConfirm0_Click(object sender, EventArgs e) => SearchMatchingImage(SearchAlgorithm.ExactMatch);

        private void toolStripMenuItem_SearchMatchingImageConfirm4_Click(object sender, EventArgs e) => SearchMatchingImage(SearchAlgorithm.FarMatch);
    }// End of Form1 class
    #endregion
}
using Microsoft.Data.Sqlite;

using SharpDX.DirectInput;

using Shell32;

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;

using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace GameLauncher
{
    public partial class Form1 : Form
    {
        public readonly string[] validRoms = { "3ds", "app", "bin", "car", "dsi", "gb", "gba", "gbc", "gcm", "gen", "gg", "ids", "iso", "md", "n64", "nds", "ngc", "ngp", "nsp", "pce", "rom", "sfc", "smc", "smd", "sms", "srl", "v64", "vpk", "wad", "xci", "xiso", "z64" };
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
        public static void RegexRenameFiles(string dir, string pattern, string replaceStr)
        {
            if (!Directory.Exists(dir))
                return;
            string[] files = Directory.GetFiles(dir);
            foreach (string f in files)
            {
                string newName = Regex.Replace(f, pattern, replaceStr);
                if (!newName.ToLower().Equals(f.ToLower()) && !File.Exists(newName))
                    File.Move(f, newName);
            }
        }
        private const int MaxNumberThreadPerList = 6; // ToDo: Make this user changeable but with maximum of 20
        // Note: Below results show that there's very little difference when using more than 6 threads
        // Performance for 1959 Roms (NES)
        // 1  Threads = 26.56 seconds
        // 2  Threads = 17.51 seconds
        // 4  Threads = 15.56 seconds
        // 6  Threads = 14.46 seconds
        // 8  Threads = 14.44 seconds
        // 10 Threads = 14.44 seconds
        // 16 Threads = 14.44 seconds
        // 20 Threads = 14.44 seconds
        static ImageList[] tmpImageList1 = null;
        static ImageList[] tmpImageList2 = null;
        static List<int> StartPosList = null;
        static List<Rom> RomList = new List<Rom>();
        class ProcessImageListThreadDetails
        {
            public int setID;
            public int listID;
            public int startPos;
            public int lastPos;
        }
        static ProcessImageListThreadDetails[] processImageListThreadDetails = new ProcessImageListThreadDetails[MaxNumberThreadPerList * 2];

        static void ProcessImageListTask(int setId, int listID, int startPos, int lastPos)
        {
            if (tmpImageList1 == null || tmpImageList2 == null)
                return;
            Console.WriteLine($"Starting set{setId} with list{listID}. Start pos={startPos} and last pos={lastPos}.");
            for (int i = startPos; i < lastPos; i++)
            {
                if (setId == 0)
                    tmpImageList1[listID].Images.Add(System.Drawing.Image.FromFile(RomList[index: i].ImagePath));
                else
                    tmpImageList2[listID].Images.Add(System.Drawing.Image.FromFile(RomList[i].ImagePath));

            }
            if (setId == 0)
                Console.WriteLine($"Completed set{setId}, list{listID} with count = {tmpImageList1[listID].Images.Count}.");
            else
                Console.WriteLine($"Completed set{setId}, list{listID} with count = {tmpImageList2[listID].Images.Count}.");
        }
        static void ProcessImageListTask(int idx) => ProcessImageListTask(processImageListThreadDetails[idx].setID, processImageListThreadDetails[idx].listID, processImageListThreadDetails[idx].startPos, processImageListThreadDetails[idx].lastPos);
        static void ProcessImageList1Task0() => ProcessImageListTask(0);
        static void ProcessImageList1Task1() => ProcessImageListTask(1);
        static void ProcessImageList1Task2() => ProcessImageListTask(2);
        static void ProcessImageList1Task3() => ProcessImageListTask(3);
        static void ProcessImageList1Task4() => ProcessImageListTask(4);
        static void ProcessImageList1Task5() => ProcessImageListTask(5);
        static void ProcessImageList1Task6() => ProcessImageListTask(6);
        static void ProcessImageList1Task7() => ProcessImageListTask(7);
        static void ProcessImageList1Task8() => ProcessImageListTask(8);
        static void ProcessImageList1Task9() => ProcessImageListTask(9);
        static void ProcessImageList1Task10() => ProcessImageListTask(10);
        static void ProcessImageList1Task11() => ProcessImageListTask(11);
        static void ProcessImageList1Task12() => ProcessImageListTask(12);
        static void ProcessImageList1Task13() => ProcessImageListTask(13);
        static void ProcessImageList1Task14() => ProcessImageListTask(14);
        static void ProcessImageList1Task15() => ProcessImageListTask(15);
        static void ProcessImageList1Task16() => ProcessImageListTask(16);
        static void ProcessImageList1Task17() => ProcessImageListTask(17);
        static void ProcessImageList1Task18() => ProcessImageListTask(18);
        static void ProcessImageList1Task19() => ProcessImageListTask(19);
        static void ProcessImageList2Task0() => ProcessImageListTask(MaxNumberThreadPerList + 0);
        static void ProcessImageList2Task1() => ProcessImageListTask(MaxNumberThreadPerList + 1);
        static void ProcessImageList2Task2() => ProcessImageListTask(MaxNumberThreadPerList + 2);
        static void ProcessImageList2Task3() => ProcessImageListTask(MaxNumberThreadPerList + 3);
        static void ProcessImageList2Task4() => ProcessImageListTask(MaxNumberThreadPerList + 4);
        static void ProcessImageList2Task5() => ProcessImageListTask(MaxNumberThreadPerList + 5);
        static void ProcessImageList2Task6() => ProcessImageListTask(MaxNumberThreadPerList + 6);
        static void ProcessImageList2Task7() => ProcessImageListTask(MaxNumberThreadPerList + 7);
        static void ProcessImageList2Task8() => ProcessImageListTask(MaxNumberThreadPerList + 8);
        static void ProcessImageList2Task9() => ProcessImageListTask(MaxNumberThreadPerList + 9);
        static void ProcessImageList2Task10() => ProcessImageListTask(MaxNumberThreadPerList + 10);
        static void ProcessImageList2Task11() => ProcessImageListTask(MaxNumberThreadPerList + 11);
        static void ProcessImageList2Task12() => ProcessImageListTask(MaxNumberThreadPerList + 12);
        static void ProcessImageList2Task13() => ProcessImageListTask(MaxNumberThreadPerList + 13);
        static void ProcessImageList2Task14() => ProcessImageListTask(MaxNumberThreadPerList + 14);
        static void ProcessImageList2Task15() => ProcessImageListTask(MaxNumberThreadPerList + 15);
        static void ProcessImageList2Task16() => ProcessImageListTask(MaxNumberThreadPerList + 16);
        static void ProcessImageList2Task17() => ProcessImageListTask(MaxNumberThreadPerList + 17);
        static void ProcessImageList2Task18() => ProcessImageListTask(MaxNumberThreadPerList + 18);
        static void ProcessImageList2Task19() => ProcessImageListTask(MaxNumberThreadPerList + 19);

        public List<GameSystem> GameSystems = new List<GameSystem>();
        SqliteConnection connection = null;
        private string ConvertToSimplifiedName(string name,
                bool removeNumbers = false, bool doHighCompres = false, bool removeBracesAndSuffixData = true, 
                bool trimSpaces = true, bool removeRomPrefixIndexes = true, bool removeApostropheEs = true, 
                bool convertSuffixRomanNumToDec = true, bool removeNonAlphaNum = true, bool removeApostrophe = true, 
                bool makeLowerCase = true, bool replaceUnderScoreWithSpace = false, bool removeRedundant = false, 
                string removeSpecificStrValue = "", string replacementStr = "") // removeSpecificStrValue and replacementStr is for possible future requirements, or for end user customization.
        {
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
            
            if (removeRomPrefixIndexes == true) // Do before filtering spaces and non-alpha-num because that will hinder finding prefix index number
                name = Regex.Replace(name, @"^[0-9][0-9][0-9][0-9][\s_]-[\s_]", ""); // Remove prefix index numbers. IE: "1234 - GameName" to "GameName"

            if (doHighCompres || replaceUnderScoreWithSpace)
                name = name.Replace("_", " ");

            if (doHighCompres  || removeRedundant)
            {
                name = Regex.Replace(name, @"(?i)[\s_]DSi?\+?([\s_\(\[])", "$1");
                name = Regex.Replace(name, @"(?i)\sDS$", "");
                name = Regex.Replace(name, @"(?i)N?64", "");
                name = Regex.Replace(name, @"(?i)3D", "");
                name = Regex.Replace(name, @"(?i)^NES\s", "");
            }
            if (doHighCompres)
            {
                removeNumbers = true;
                name = Regex.Replace(name, @"(?i)([a-zA-Z][a-zA-Z])'?s\b", "$1");
                name = Regex.Replace(name, @"(?i)^A\s", "");
                name = Regex.Replace(name, @"(?i)^The\s", "");
                name = Regex.Replace(name, @"(?i)\s[\,-\.]\sA\s", "");
                name = Regex.Replace(name, @"(?i)\s[\,-\.]\sThe\s", "");
                name = Regex.Replace(name, @"(?i)[\,-\.]\s?A", "");
                name = Regex.Replace(name, @"(?i)[\,-\.]\s?The", "");
                name = Regex.Replace(name, @"(?i)\sand\s", "");
                name = Regex.Replace(name, @"(?i)\sin\s", "");
                name = Regex.Replace(name, @"(?i)\sVersion\s", "");
                name = Regex.Replace(name, @"(?i)\sEdition\s", "");
                name = Regex.Replace(name, @"(?i)\sPart\s", "");
            }

            if (convertSuffixRomanNumToDec == true)// Do before filtering spaces because it will hinder finding suffix roman numbers
            {
                const string strStringContains5_Or_10 = "VXvx";
                if (name.IndexOfAny(strStringContains5_Or_10.ToCharArray()) != -1)
                {// Only do this extra work if V or X is in the string.
                    if (name.Contains("x", StringComparison.OrdinalIgnoreCase))
                    {
                        name = Regex.Replace(name, @"(?i)\sXIX", "19");         // IE: "GameName XIX" to "GameName 19"
                        name = Regex.Replace(name, @"(?i)\sXVIII", "18");       // IE: "GameName XVIII" to "GameName 18"
                        name = Regex.Replace(name, @"(?i)\sXVII", "17");        // IE: "GameName XVII" to "GameName 17"
                        name = Regex.Replace(name, @"(?i)\sXVI", "16");         // IE: "GameName XVI" to "GameName 16"
                        name = Regex.Replace(name, @"(?i)\sXV", "15");          // IE: "GameName XV" to "GameName 15"
                        name = Regex.Replace(name, @"(?i)\sXIV", "14");         // IE: "GameName XI" to "GameName 14"
                        name = Regex.Replace(name, @"(?i)\sXIII", "13");        // IE: "GameName XIII" to "GameName 13"
                        name = Regex.Replace(name, @"(?i)\sXII", "12");         // IE: "GameName XII" to "GameName 12"
                        name = Regex.Replace(name, @"(?i)\sXI", "11");          // IE: "GameName XI" to "GameName 11"
                        name = Regex.Replace(name, @"(?i)\sX$", "10");          // IE: "GameName X" to "GameName 10"
                        name = Regex.Replace(name, @"(?i)\sX\s\-\s", "10 - ");  // IE: "GameName X" to "GameName 10 - "
                        name = Regex.Replace(name, @"(?i)\sIX", "9");           // IE: "GameName IX" to "GameName 9"
                    }
                    if (name.Contains("v", StringComparison.OrdinalIgnoreCase))
                    {
                        name = Regex.Replace(name, @"(?i)\sVIII", "8");         // IE: "GameName VIII" to "GameName 8"
                        name = Regex.Replace(name, @"(?i)\sVII", "7");          // IE: "GameName VII" to "GameName 7"
                        name = Regex.Replace(name, @"(?i)\sVI", "6");           // IE: "GameName VI" to "GameName 6"
                        name = Regex.Replace(name, @"(?i)\sIV", "4");           // IE: "GameName IV" to "GameName 4"
                        name = Regex.Replace(name, @"(?i)\sV$", "5");           // IE: "GameName V" to "GameName 5"
                        name = Regex.Replace(name, @"(?i)\sV\s\-\s", "5 - ");   // IE: "GameName V" to "GameName 5 - "
                    }
                }
                if (name.Contains(" I", StringComparison.OrdinalIgnoreCase))
                {
                    name = Regex.Replace(name, @"(?i)\sIII", "3");              // IE: "GameName III" to "GameName 3"
                    name = Regex.Replace(name, @"(?i)\sII", "2");               // IE: "GameName II" to "GameName 2"
                    name = Regex.Replace(name, @"(?i)\sI$", "1");               // IE: "GameName I" to "GameName 1"
                    name = Regex.Replace(name, @"(?i)\sI\s\-\s", "1 - ");       // IE: "GameName I" to "GameName 1 - "
                }
            }

            if (removeNumbers == true) // Do this after roman numeral conversion so that roman numerals are removed as well.
                name = Regex.Replace(name, @"[0-9]", "");//IE: "GameName 2" to "GameName"

            if (makeLowerCase == true)
                name = name.ToLower();
            
            if (removeApostropheEs) // Do before filtering non-alpha-num because that will hinder finding 's
                name = name.Replace("'s", "");//IE: "Dini's Soccer" to "Dini Soccer"

            if (removeNonAlphaNum == true)
                name = Regex.Replace(name, @"[^0-9a-zA-Z]", "");//IE: "WarioWare - Touched!" to "WarioWareTouched"
            else
            {// There's no need to do this logic if removeNonAlphaNum==true, and non-alpha-num characters have been removed.
                if (removeApostrophe)
                    name = name.Replace("'", "");//IE: "Baseball '99" to "Baseball 99"
                if (trimSpaces == true)
                    name = name.Trim(' ');//IE: "GameName " to "GameName"
            }
 
            return name;
        }
        private string GetFirstColStr(string sql, string defaultValue = "")
        {
            // On failure, returns default value
            SqliteCommand command = new SqliteCommand(sql, connection);
            SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                var firstColumn = reader.GetString(0);
                return firstColumn;
            }
            return defaultValue;
        }
        private int GetFirstColInt(string sql, int defaultValue = -1)
        {
            // On failure, returns default value
            SqliteCommand command = new SqliteCommand(sql, connection);
            SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                var idx = reader.GetInt32(0);
                return idx;
            }
            return defaultValue; 
        }
        private (int, string) GetIntAndStr(string sql, int defaultInt = -1, string defaultStr = null)
        {
            // On failure, returns default values
            SqliteCommand command = new SqliteCommand(sql, connection);
            SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int i = reader.GetInt32(0);
                string s = reader.GetString(1);
                return (i, s);
            }
            return (defaultInt, defaultStr);
        }
        private bool UpdateDB(string sql)
        {
            SqliteCommand command = new SqliteCommand(sql, connection);
            var x = command.ExecuteNonQuery();
            return x > 0;
        }
        private int GetImageIndexByPath(string FilePath) => GetFirstColInt($"SELECT ID FROM Images WHERE FilePath like \"{FilePath}\"");
        private (int, string) GetImageIndexByName(string NameSimplified, string NameOrg, string Title, string Compressed)
        {
            string sql = $"SELECT ID, FilePath FROM Images WHERE NameSimplified like \"{NameSimplified}\"";
            string imgPath = "";
            int idx = 0;
            (idx, imgPath) = GetIntAndStr(sql);
            if (idx < 0)
            {
                // Lets try something else to find a matching image file.
                if (NameOrg.Contains("-"))
                {
                    string NameAfterHyphen = NameOrg.Substring(NameOrg.IndexOf("-") + 1);
                    string NameAfterHyphenSimple = ConvertToSimplifiedName(NameAfterHyphen);
                    sql = $"SELECT ID, FilePath FROM Images WHERE NameSimplified like \"{NameAfterHyphenSimple}\"";
                    (idx, imgPath) = GetIntAndStr(sql);
                    if (idx < 0)
                    {
                        string nameWithNoNumbers = ConvertToSimplifiedName(NameAfterHyphen, true);
                        sql = $"SELECT ID, FilePath FROM Images WHERE NameSimplified like \"{nameWithNoNumbers}\"";
                        (idx, imgPath) = GetIntAndStr(sql);
                    }
                }
                if (idx < 0)
                {
                    string nameWithNoNumbers = ConvertToSimplifiedName(NameSimplified, true); // Remove numbers from the name. It's better to have an image to the older version of the game, than non at all.
                    if (!nameWithNoNumbers.Equals(NameSimplified))
                    {
                        sql = $"SELECT ID, FilePath FROM Images WHERE NameSimplified like \"{nameWithNoNumbers}\"";
                        (idx, imgPath) = GetIntAndStr(sql);
                    }
                }

                if (idx < 0 && NameOrg.StartsWith("A ", StringComparison.CurrentCultureIgnoreCase))
                {
                    string Name = NameOrg.Substring(1);
                    sql = $"SELECT ID, FilePath FROM Images WHERE NameSimplified like \"{Name}%\"";
                    (idx, imgPath) = GetIntAndStr(sql);
                }
                if (idx < 0 && NameOrg.StartsWith("The ", StringComparison.CurrentCultureIgnoreCase))
                {
                    string Name = NameOrg.Substring(3);
                    sql = $"SELECT ID, FilePath FROM Images WHERE NameSimplified like \"{Name}%\"";
                    (idx, imgPath) = GetIntAndStr(sql);
                }
                if (idx < 0)
                {
                    sql = $"SELECT ID, FilePath FROM Images WHERE NameOrg like \"{NameOrg}\"";
                    (idx, imgPath) = GetIntAndStr(sql);
                }
                if (idx < 0)
                {
                    sql = $"SELECT ID, FilePath FROM Images WHERE NameSimplified like \"{NameOrg}\"";
                    (idx, imgPath) = GetIntAndStr(sql);
                }
                if (idx < 0)
                {
                    sql = $"SELECT ID, FilePath FROM Images WHERE Compressed like \"{Compressed}\"";
                    (idx, imgPath) = GetIntAndStr(sql);
                }
                if (idx < 0)
                {
                    sql = $"SELECT ID, FilePath FROM Images WHERE Compressed like \"%{Compressed}%\"";
                    (idx, imgPath) = GetIntAndStr(sql);
                }
                if (idx < 0)
                {
                    sql = $"SELECT ID, FilePath FROM Images WHERE NameOrg like \"%{Title}%\"";
                    (idx, imgPath) = GetIntAndStr(sql);
                }
            }
            return (idx, imgPath);
        }
        private int GetSystemIndex(string Name) => GetFirstColInt($"SELECT ID FROM GameSystems WHERE Name = \"{Name}\"");
        private int AddImageToDb(string imgFile, bool checkIfInDb = false, bool checkIfExists = false)
        {
            if (checkIfExists && !File.Exists(imgFile))
                return -1;
            if (checkIfInDb) 
            {
                int imageID = GetFirstColInt($"SELECT ID FROM Images WHERE FilePath like \"{imgFile}\"");
                return imageID;
            }
            string NameOrg = Path.GetFileNameWithoutExtension(imgFile);
            string NameSimplified = ConvertToSimplifiedName(NameOrg);
            string Compressed = ConvertToSimplifiedName(NameOrg, true, true);
            string sql = $"INSERT OR REPLACE INTO Images (NameSimplified, NameOrg, Compressed, FilePath) VALUES (\"{NameSimplified}\", \"{NameOrg}\", \"{Compressed}\", \"{imgFile}\")";
            SqliteCommand command = new SqliteCommand(sql, connection);
            int x = command.ExecuteNonQuery();
            int id = GetImageIndexByPath(imgFile);
            return id;
        }
        private int GetImageID(string imgFile) => AddImageToDb(imgFile, true);
        private void GetImages(string imgPath)
        {
            if (Directory.Exists(imgPath))
            {
                string[] imgFiles = Directory.GetFiles(imgPath, "*.png");
                foreach (string f in imgFiles)
                    AddImageToDb(f);
            }
        }
        public static string GetShortcutTargetFile(string shortcutFilename)
        {
            string pathOnly = System.IO.Path.GetDirectoryName(shortcutFilename);
            string filenameOnly = System.IO.Path.GetFileName(shortcutFilename);

            // This requires a COM Reference to Shell32 (Microsoft Shell Controls And Automation).
            Shell shell = new Shell();
            Folder folder = shell.NameSpace(pathOnly);
            FolderItem folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null && folderItem.IsLink)
            {
                Shell32.ShellLinkObject link = (Shell32.ShellLinkObject)folderItem.GetLink;
                return link.Path;
            }

            return null;
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
                    emulatorPaths.EmulatorPaths[index] = execFilePath;
                    index += 1;
                    if (index == EmulatorExecutables.MAX_EMULATOR_EXECUTABLES)
                        return emulatorPaths;
                }
            }
            string[] execFiles = Directory.GetFiles(path, "*.exe", SearchOption.AllDirectories);
            foreach (string f in execFiles)
            {
                emulatorPaths.EmulatorPaths[index] = f;
                index += 1;
                if (index == EmulatorExecutables.MAX_EMULATOR_EXECUTABLES)
                    return emulatorPaths;
            }
            return index == 0 ? null : emulatorPaths;
        }
        private int CreateSqlTables()
        {
            const string sql =
                    "CREATE TABLE \"GameSystems\" (\r\n\t\"Name\"\tTEXT NOT NULL UNIQUE,\r\n\t\"ID\"\tINTEGER NOT NULL UNIQUE,\r\n\t\"RomDirPath\"\tTEXT NOT NULL,\r\n\t\"ImageDirPath\"\tTEXT NOT NULL,\r\n\t\"EmulatorPath1\"\tTEXT NOT NULL,\r\n\t\"EmulatorPath2\"\tTEXT,\r\n\t\"EmulatorPath3\"\tTEXT,\r\n\t\"EmulatorPath4\"\tTEXT,\r\n\t\"EmulatorPath5\"\tTEXT,\r\n\t\"EmulatorPath6\"\tTEXT,\r\n\t\"EmulatorPath7\"\tTEXT,\r\n\t\"EmulatorPath8\"\tTEXT,\r\n\t\"EmulatorPath9\"\tTEXT,\r\n\t\"EmulatorPath10\"\tTEXT,\r\n\tPRIMARY KEY(\"ID\" AUTOINCREMENT)\r\n);\r\n" +
                    "CREATE TABLE \"Images\" (\r\n\t\"NameSimplified\"\tTEXT NOT NULL,\r\n\t\"NameOrg\"\tTEXT NOT NULL,\r\n\t\"Compressed\"\tTEXT NOT NULL,\r\n\t\"FilePath\"\tTEXT NOT NULL UNIQUE,\r\n\t\"ID\"\tINTEGER NOT NULL UNIQUE,\r\n\tPRIMARY KEY(\"ID\" AUTOINCREMENT)\r\n);\r\n" +
                    "CREATE TABLE \"PersistenceVariables\" (\r\n\t\"Name\"\tTEXT NOT NULL UNIQUE,\r\n\t\"Value\"\tTEXT,\r\n\t\"ValueInt\"\tINTEGER,\r\n\tPRIMARY KEY(\"Name\")\r\n);\r\n" +
                    "CREATE TABLE \"Roms\" (\r\n\t\"Title\"\tTEXT NOT NULL,\r\n\t\"NameSimplified\"\tTEXT NOT NULL,\r\n\t\"NameOrg\"\tTEXT NOT NULL,\r\n\t\"Compressed\"\tTEXT NOT NULL,\r\n\t\"System\"\tINTEGER NOT NULL,\r\n\t\"FilePath\"\tTEXT NOT NULL UNIQUE,\r\n\t\"PreferredEmulator\"\tINTEGER NOT NULL DEFAULT 0,\r\n\t\"ImageID\"\tINTEGER DEFAULT -1,\r\n\t\"ImagePath\"\tTEXT,\r\n\t\"QtyPlayers\"\tINTEGER NOT NULL DEFAULT 1,\r\n\t\"Status\"\tTEXT,\r\n\t\"Region\"\tTEXT,\r\n\t\"Developer\"\tTEXT,\r\n\t\"ReleaseDate\"\tTEXT,\r\n\t\"RomSize\"\tINTEGER,\r\n\t\"Genre\"\tTEXT,\r\n\t\"NotesCore\"\tTEXT,\r\n\t\"NotesUser\"\tTEXT,\r\n\t\"FileFormat\"\tTEXT,\r\n\t\"Version\"\tTEXT,\r\n\t\"Description\"\tTEXT,\r\n\t\"Language\"\tTEXT,\r\n\tPRIMARY KEY(\"FilePath\")\r\n);\r\n" +
                    "\r\n";
            var command = new SqliteCommand(sql, connection);
            var x = command.ExecuteNonQuery();
            return x;
        }
        public string GetPersistenceVariable(string name, string defaultValue = "")
        {
            if (connection == null)
                return defaultValue;
            string sql = $"SELECT Value FROM PersistenceVariables WHERE Name = \"{name}\"";
            return GetFirstColStr(sql, defaultValue);
        }
        public int GetPersistenceVariable(string name, int defaultValue)
        {
            if (connection == null)
                return defaultValue;
            string sql = $"SELECT ValueInt FROM PersistenceVariables WHERE Name = \"{name}\"";
            return GetFirstColInt(sql, defaultValue);
        }
        private void SetPersistenceVariable(string name, string value)
        {
            if (connection == null)
                return;
            var sql = $"INSERT OR REPLACE INTO PersistenceVariables (Name, Value) VALUES (\"{name}\", \"{value}\")";
            var command = new SqliteCommand(sql, connection);
            var x = command.ExecuteNonQuery();
        }
        private void SetPersistenceVariable(string name, int valueInt)
        {
            if (connection == null)
                return;
            var sql = $"INSERT OR REPLACE INTO PersistenceVariables (Name, ValueInt) VALUES (\"{name}\", \"{valueInt}\")";
            var command = new SqliteCommand(sql, connection);
            var x = command.ExecuteNonQuery();
        }
        public string GetEmulatorExecutable(string systemName, int emulatorID)
        {
            string sql = $"SELECT EmulatorPath{emulatorID} FROM GameSystems WHERE Name = \"{systemName}\"";
            return GetFirstColStr(sql);
        }
        public string GetPattern(string value, string pattern, char trimLeftChars = '\0', char trimRightChars = '\0', bool defaultEmptyStr = false)
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
        private Rom GetRomDetails(string romFilePath, int systemIndex, long RomSize)
        {
            string NameOrg = Path.GetFileNameWithoutExtension(romFilePath); // f.Substring(imgPath.Length + 1);
            string NameSimplified = ConvertToSimplifiedName(NameOrg);
            string Compressed = ConvertToSimplifiedName(NameOrg, true, true);
            string Title = ConvertToSimplifiedName(NameOrg, 
                false, false, true, false, true, 
                false, false, false, false, false,
                true);
            string Status = "";
            string NotesCore = "";
            int imageID = 0;
            string imagePath = "";
            (imageID, imagePath) = GetImageIndexByName(NameSimplified, NameOrg, Title, Compressed);
            string Region = "unknown";
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

            // Check if name includes ROM version
            string Version = GetPattern(NameOrg, @"\(V[0-9]+\.[0-9]+\)", '(', ')', true);
            if (NameOrg.Contains("(REV", StringComparison.OrdinalIgnoreCase))
            {
                string RevInfo = GetPattern(NameOrg, @"\(REV[0-9]+\)", '(', ')');
                if (RevInfo != null)
                    Version += $"[REV{RevInfo}]";
            }
            if (NameOrg.Contains("(PRG", StringComparison.OrdinalIgnoreCase))
            {
                string RevInfo = GetPattern(NameOrg, @"\(PRG[0-9]+\)", '(', ')');
                if (RevInfo != null)
                    Version += $"[PRG-REV{RevInfo}]";
            }
            if (NameOrg.Contains("(Beta)", StringComparison.OrdinalIgnoreCase))
                Version += "[Beta]";
            if (NameOrg.Contains("(Proto)", StringComparison.OrdinalIgnoreCase))
                Version += "[Prototype ]";

            // ToDo: Add logic to read Project64.rdb and Project64.rdx to get rom details
            //       Add logic to read file MultiplayerGameList.txt to get quantity of player details.

            Rom rom = new Rom(NameSimplified, systemIndex, romFilePath, NameOrg, Title, Compressed, imageID,0,imagePath,0,Region,"", RomSize, "", NotesCore, "","","",Status,Version,"",Language);
            return rom;
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
                string NameSimplified = ConvertToSimplifiedName(line);
                string sql = $"UPDATE Roms SET QtyPlayers = 4 WHERE NameSimplified = \"{NameSimplified}\"";
                if (addInfoToNotesInsteadOfPlayerQty)
                    sql = $"UPDATE Roms SET NotesUser = \"Possible Multiplayer Game\" WHERE NameSimplified = \"{NameSimplified}\"";
                var command = new SqliteCommand(sql, connection);
                var x = command.ExecuteNonQuery();
            }
        }
        public const int MINIMUM_ROM_SIZE = 10000; // NES has "Demo Boy 2" which is 1948 bytes. Next smallest is "NES PowerPad Test Cart" at 2,598 bytes.
        public const int MINIMUM_ZIP_SIZE = 1000;
        private string romSubFolderName = @"\roms";
        private string imageSubFolderName = @"\images";
        private void InitializeRomsInDatabaseForSystem(string emulatorDir, EmulatorExecutables emulatorPaths)
        {
            string romPath = emulatorDir + romSubFolderName;
            string imgPath = emulatorDir + imageSubFolderName;
            //EmulatorExecutables emulatorPaths = GetEmulatorPaths(emulatorDir);
            if (Directory.Exists(romPath) && emulatorPaths != null)
            {
                //string Name = emulatorDir.Substring(startingPath.Length + 1);
                string Name = Path.GetFileName(emulatorDir);
                var sql = $"INSERT OR REPLACE INTO GameSystems (Name, RomDirPath, ImageDirPath, EmulatorPath1, EmulatorPath2, EmulatorPath3, EmulatorPath4, EmulatorPath5, EmulatorPath6, EmulatorPath7, EmulatorPath8, EmulatorPath9, EmulatorPath10) VALUES " +
                    $"(\"{Name}\", \"{romPath}\", \"{imgPath}\", \"{emulatorPaths.EmulatorPaths[0]}\", \"{emulatorPaths.EmulatorPaths[1]}\", \"{emulatorPaths.EmulatorPaths[2]}\", \"{emulatorPaths.EmulatorPaths[3]}\", \"{emulatorPaths.EmulatorPaths[4]}\", \"{emulatorPaths.EmulatorPaths[5]}\", \"{emulatorPaths.EmulatorPaths[6]}\", \"{emulatorPaths.EmulatorPaths[7]}\", \"{emulatorPaths.EmulatorPaths[8]}\", \"{emulatorPaths.EmulatorPaths[9]}\")";
                var command = new SqliteCommand(sql, connection);
                var x = command.ExecuteNonQuery();
                int systemIndex = GetSystemIndex(Name);
                Console.WriteLine($"The game system '{Name}' has been created successfully into row {systemIndex}");
                lock (GameSystems)
                {
                    GameSystems.Add(new GameSystem(Name, romPath, imgPath, emulatorPaths, systemIndex));
                }
                if (Directory.Exists(imgPath))
                    GetImages(imgPath);

                string[] romFiles = Directory.GetFiles(romPath);
                foreach (string f in romFiles)
                {
                    long RomSize = new System.IO.FileInfo(f).Length;
                    string ext = Path.GetExtension(f).ToLower().TrimStart('.');
                    if (ext.Equals("sav"))
                        continue;
                    if (!validRoms.Contains(ext.ToLower()) && RomSize < MINIMUM_ROM_SIZE)
                    {
                        if (ext.Equals("zip") || ext.Equals("bin"))
                        { // There are some Atari ROM's that are smaller than 2K
                            if (RomSize < MINIMUM_ZIP_SIZE)
                                continue;
                        }
                        else
                            continue;
                    }
                    Rom rom = GetRomDetails(f, systemIndex, RomSize);
                    sql = $"INSERT OR REPLACE INTO Roms (NameSimplified, System, FilePath, NameOrg, Title, Compressed, ImageID, ImagePath, Region, Language, Status, Version, NotesCore, RomSize, PreferredEmulator, QtyPlayers, Developer, ReleaseDate, Genre, NotesUser, FileFormat, Description) VALUES" +
                        $" (\"{rom.NameSimplified}\", {systemIndex}, \"{f}\", \"{rom.NameOrg}\", \"{rom.Title}\", \"{rom.Compressed}\", {rom.ImageID}, \"{rom.ImagePath}\", \"{rom.Region}\", \"{rom.Language}\", \"{rom.Status}\", \"{rom.Version}\", \"{rom.NotesCore}\", {rom.RomSize}, {rom.PreferredEmulatorID}, {rom.QtyPlayers}, \"{rom.Developer}\", \"{rom.ReleaseDate}\", \"{rom.Genre}\", \"{rom.NotesUser}\", \"{rom.FileFormat}\", \"{rom.Description}\")";
                    command = new SqliteCommand(sql, connection);
                    x = command.ExecuteNonQuery();
                }
                GetMultiplayerRomData(emulatorDir);
            }
        }
        class SystemScanTaskData
        {
            public string emulatorDir;
            public EmulatorExecutables emulatorExecutables;
        }
        private bool InitializeRomsInDatabase(string startingPath = @"C:\Emulators", string romSubFolder = @"\roms", string imageSubFolder = @"\images", bool createTables = false, int doMultithread = 0)
        {
            romSubFolderName = romSubFolder;
            imageSubFolderName = imageSubFolder;
            Stopwatch totalProcessWatch = System.Diagnostics.Stopwatch.StartNew();
            Stopwatch databaseCollectionWatch = System.Diagnostics.Stopwatch.StartNew();
            if (createTables) 
                CreateSqlTables();
            // Check for master image path. Note: These images can apply to any game in any system which matches truncated name
            string masterImgPath = startingPath + imageSubFolderName;
            if (Directory.Exists(masterImgPath))
                GetImages(masterImgPath);

            string[] emulatorDirs = Directory.GetDirectories(startingPath, "*", SearchOption.TopDirectoryOnly);
            SystemScanTaskData[] systemScanTaskDatas = new SystemScanTaskData[emulatorDirs.Length];
            Stopwatch findEmulatorExecuteProcessWatch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < emulatorDirs.Length; ++i)
            {
                systemScanTaskDatas[i] = new SystemScanTaskData();
                systemScanTaskDatas[i].emulatorDir = emulatorDirs[i];
                systemScanTaskDatas[i].emulatorExecutables = GetEmulatorPaths(emulatorDirs[i]);
            }
            findEmulatorExecuteProcessWatch.Stop();
            TimeSpan findEmulatorExecuteElapsed = findEmulatorExecuteProcessWatch.Elapsed;
            if (doMultithread == 1)
            {
                Task[] tasks = new Task[emulatorDirs.Length];
                for (int i = 0; i < emulatorDirs.Length; ++i)
                {
                    int ii = i;
                    tasks[i] = Task.Run(() => InitializeRomsInDatabaseForSystem(emulatorDirs[ii], systemScanTaskDatas[ii].emulatorExecutables));
                }
                Task.WaitAll(tasks);
            }
            else if (doMultithread == 2)
            {
                Parallel.ForEach(systemScanTaskDatas, systemScanTaskData =>
                {
                    InitializeRomsInDatabaseForSystem(systemScanTaskData.emulatorDir, systemScanTaskData.emulatorExecutables);
                });
            }
            else
            {
                foreach (SystemScanTaskData systemScanTaskData in systemScanTaskDatas)
                {
                    InitializeRomsInDatabaseForSystem(systemScanTaskData.emulatorDir, systemScanTaskData.emulatorExecutables);
                }
            }
            databaseCollectionWatch.Stop();
            TimeSpan databaseCollectioElapsed = databaseCollectionWatch.Elapsed;
            Stopwatch imageListCollectionWatch = System.Diagnostics.Stopwatch.StartNew();
            foreach (GameSystem system in GameSystems) 
            {
                DeleteImageList(system.Name);
                DisplaySystemIcons(system.Name);
            }
            imageListCollectionWatch.Stop();
            totalProcessWatch.Stop();
            TimeSpan imageListCollectionElapsed = imageListCollectionWatch.Elapsed;
            TimeSpan totalProcessElapsed = totalProcessWatch.Elapsed;
            MessageBox.Show($"Completed data collection DB collection time = {databaseCollectioElapsed.ToString(@"hh\:mm\:ss")} and ImageList time = {imageListCollectionElapsed.ToString(@"mm\:ss")}\nTotal time = {totalProcessElapsed.ToString(@"hh\:mm\:ss")}\nfindEmulatorExecuteElapsed={findEmulatorExecuteElapsed.ToString(@"hh\:mm\:ss")}", "Process Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }
        private int GetRoms(string SystemName)
        {
            RomList = new List<Rom>();
            int SystemID = GetSystemIndex(SystemName);
            if (SystemID < 0)
                return -1;
            var command = connection.CreateCommand();
            string fieldNames = "NameSimplified, NameOrg, System, FilePath, PreferredEmulator, ImageID, ImagePath, QtyPlayers, Status, Region, Developer, ReleaseDate, RomSize, Genre, NotesCore, NotesUser, FileFormat, Version, Description, Language, Title, Compressed";
            command.CommandText = $"SELECT {fieldNames} FROM Roms WHERE System = \"{SystemID}\" ORDER BY NameSimplified";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string NameSimplified = reader.GetString(0);
                    string NameOrg = reader.GetString(1);
                    int System = reader.GetInt32(2);
                    string FilePath = reader.GetString(3);
                    int PreferredEmulator = reader.GetInt32(4);
                    int ImageID = reader.GetInt32(5);
                    string ImagePath = reader.GetString(6);
                    int QtyPlayers = reader.GetInt32(7);
                    string Status = reader.GetString(8);
                    string Region = reader.GetString(9);
                    string Developer = reader.GetString(10);
                    string ReleaseDate = reader.GetString(11);
                    int RomSize = reader.GetInt32(12);
                    string Genre = reader.GetString(13);
                    string NotesCore = reader.GetString(14);
                    string NotesUser = reader.GetString(15);
                    string FileFormat = reader.GetString(16);
                    string Version = reader.GetString(17);
                    string Description = reader.GetString(18);
                    string Language = reader.GetString(19);
                    string Title = reader.GetString(20);
                    string Compressed = reader.GetString(21);
                    if (ImageID < 0 || ImagePath.Length == 0)
                    {
                        ImagePath = defaultImagePath;
                    }

                    RomList.Add(new Rom(NameSimplified, System,FilePath, NameOrg, Title, Compressed, ImageID,
                        PreferredEmulator, ImagePath, QtyPlayers, Region, 
                        Developer, RomSize, Genre, NotesCore, NotesUser, 
                        FileFormat, ReleaseDate, Status, Version, Description, Language));
                }
            }
            return RomList.Count;
        }
        private string GetImageListFile(string systemName, int index)
        {
            string imageListSuffix = index == 1 ? ".ImageList1.GameLauncher" : ".ImageList2.GameLauncher";
            return Path.Combine(dataDirPath, $"Cache_{systemName}{imageListSuffix}");
        }
        private void DeleteImageList(string systemName)
        {
            string imageList1Path = GetImageListFile(systemName, 1);
            string imageList2Path = GetImageListFile(systemName, 2);
            if (File.Exists(imageList1Path))
                File.Delete(imageList1Path);
            if (File.Exists(imageList2Path))
                File.Delete(imageList2Path);
            previousCollections.Remove(systemName);
        }
        private PreviousCollectedImages GetSavedCollectionData(string systemName)
        {
            string imageList1Path = GetImageListFile(systemName, 1);
            string imageList2Path = GetImageListFile(systemName, 2);
            if (File.Exists(imageList1Path) && File.Exists(imageList2Path))
            {
                PreviousCollectedImages previousCollectedImages = new PreviousCollectedImages();
                previousCollectedImages.list1 = SerializableImageList.Load(imageList1Path, largeIconSize);
                previousCollectedImages.list2 = SerializableImageList.Load(imageList2Path, smallIconSize);
                if (previousCollectedImages.list1 != null && previousCollectedImages.list2 != null)
                    return previousCollectedImages;
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
        private string dataDirPath = null;
        private string binDirPath = null;
        private string defaultImagePath = null;
        private Dictionary<string, PreviousCollectedData> previousCollections = new Dictionary<string, PreviousCollectedData>();
        private const int largeIconSize = 128;
        private const int smallIconSize = 32;
        public bool DisplaySystemIcons(string systemName, bool usePreviousCollectionCache = false, bool doMultithreading = true)
        {
            textBoxStatus.Text = $"Collecting ROM data for {systemName}. Please standby....";
            if (usePreviousCollectionCache && previousCollections.ContainsKey(systemName))
            {
                //Now assigning First imageList as LargeImageList to the ListView...
                myListView.LargeImageList = previousCollections[systemName].list1;
                //assigning the second imageList as SmallImageList to the ListView...   
                myListView.SmallImageList = previousCollections[systemName].list2;
                RomList = previousCollections[systemName].roms;
            }
            else
            {
                if (GetRoms(systemName) < 1)
                    return false;
                PreviousCollectedImages previousCollectedImages = GetSavedCollectionData(systemName);
                if (previousCollectedImages != null && previousCollectedImages.list1.Images.Count == RomList.Count)
                {
                    myListView.LargeImageList = previousCollectedImages.list1;
                    //myListView.LargeImageList.ImageSize = new Size(largeIconSize, largeIconSize);
                    myListView.SmallImageList = previousCollectedImages.list2;
                }
                else 
                {
                    //Take imagelist for store images to use in listview.
                    System.Windows.Forms.ImageList myImageList1 = new ImageList();
                    //set the image size of which size images will be displayed in the listview.
                    myImageList1.ImageSize = new Size(largeIconSize, largeIconSize);
                    //Take another imageList to also display in the listview..
                    System.Windows.Forms.ImageList myImageList2 = new ImageList();
                    //set the image size smaller than the first imageList...
                    myImageList2.ImageSize = new Size(smallIconSize, smallIconSize);
                    if (doMultithreading && RomList.Count > MaxNumberThreadPerList * 3)
                    { // !!!!! There's a bug in this code. Don't use until issue is resolve !!!!!
                        StartPosList = new List<int>();
                        tmpImageList1 = new ImageList[MaxNumberThreadPerList];
                        tmpImageList2 = new ImageList[MaxNumberThreadPerList];
                        for (int i = 0; i < MaxNumberThreadPerList; ++i)
                        {
                            StartPosList.Add(RomList.Count / MaxNumberThreadPerList * i);

                            ImageList tmpImage1 = new ImageList();
                            tmpImage1.ImageSize = new Size(largeIconSize, largeIconSize);
                            tmpImageList1[i] = tmpImage1;
                            ImageList tmpImage2 = new ImageList();
                            tmpImage2.ImageSize = new Size(smallIconSize, smallIconSize);
                            tmpImageList2[i] = tmpImage2;
                        }
                        StartPosList.Add(RomList.Count);
                        Task[] tasks = new Task[MaxNumberThreadPerList * 2];
                        for (int idx = 0; idx < MaxNumberThreadPerList; ++idx)
                        {
                            processImageListThreadDetails[idx] = new ProcessImageListThreadDetails();
                            processImageListThreadDetails[idx + MaxNumberThreadPerList] = new ProcessImageListThreadDetails();
                            processImageListThreadDetails[idx].setID = 0;
                            processImageListThreadDetails[idx + MaxNumberThreadPerList].setID = 1;
                            processImageListThreadDetails[idx].listID = idx;
                            processImageListThreadDetails[idx + MaxNumberThreadPerList].listID = idx;
                            processImageListThreadDetails[idx].startPos = StartPosList[idx];
                            processImageListThreadDetails[idx + MaxNumberThreadPerList].startPos = StartPosList[idx];
                            processImageListThreadDetails[idx].lastPos = StartPosList[idx + 1];
                            processImageListThreadDetails[idx + MaxNumberThreadPerList].lastPos = StartPosList[idx + 1];
                            switch (idx)
                            {
                                case 0:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task0());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task0());
                                    break;
                                case 1:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task1());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task1());
                                    break;
                                case 2:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task2());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task2());
                                    break;
                                case 3:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task3());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task3());
                                    break;
                                case 4:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task4());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task4());
                                    break;
                                case 5:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task5());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task5());
                                    break;
                                case 6:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task6());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task6());
                                    break;
                                case 7:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task7());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task7());
                                    break;
                                case 8:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task8());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task8());
                                    break;
                                case 9:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task9());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task9());
                                    break;
                                case 10:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task10());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task10());
                                    break;
                                case 11:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task11());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task11());
                                    break;
                                case 12:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task12());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task12());
                                    break;
                                case 13:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task13());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task13());
                                    break;
                                case 14:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task14());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task14());
                                    break;
                                case 15:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task15());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task15());
                                    break;
                                case 16:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task16());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task16());
                                    break;
                                case 17:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task17());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task17());
                                    break;
                                case 18:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task18());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task18());
                                    break;
                                case 19:
                                    tasks[idx] = Task.Run(() => ProcessImageList1Task19());
                                    tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageList2Task19());
                                    break;
                            }
                            // Both below method crashed!!! Do not use!!!
                            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            //tasks[idx] = Task.Run(() => ProcessImageListTask(0, idx, StartPosList[idx], StartPosList[idx + 1]));
                            //tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageListTask(1, idx, StartPosList[idx], StartPosList[idx + 1]));

                            //tasks[idx] =                            Task.Factory.StartNew(() => ProcessImageListTask(0, idx, StartPosList[idx], StartPosList[idx + 1]));
                            //tasks[idx + MaxNumberThreadPerList] =   Task.Factory.StartNew(() => ProcessImageListTask(1, idx, StartPosList[idx], StartPosList[idx + 1]));
                            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        }
                        //Task.WaitAll(tasks[1]);
                        Task.WaitAll(tasks);
                        for (int i = 0; i < MaxNumberThreadPerList; ++i)
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
                        for (int i = 0; i < RomList.Count; i++)
                            myImageList1.Images.Add(System.Drawing.Image.FromFile(RomList[i].ImagePath));
                        for (int i = 0; i < RomList.Count; i++)
                            myImageList2.Images.Add(System.Drawing.Image.FromFile(RomList[i].ImagePath));
                    }
                    //Now assigning First imageList as LargeImageList to the ListView...
                    myListView.LargeImageList = myImageList1;
                    //assigning the second imageList as SmallImageList to the ListView...
                    myListView.SmallImageList = myImageList2;
                }
                PreviousCollectedData previousCollectedData = new PreviousCollectedData();
                previousCollectedData.list1 = myListView.LargeImageList;
                previousCollectedData.list2 = myListView.SmallImageList;
                previousCollectedData.roms = RomList;
                if (usePreviousCollectionCache)
                    previousCollections[systemName] = previousCollectedData;
                if (previousCollectedImages == null || previousCollectedImages.list1.Images.Count != RomList.Count)
                    SaveCollectionData(systemName, previousCollectedData);
            }

            textBoxStatus.Text = $"Setting titles for the {RomList.Count} games (ROM's) found for {systemName}";
            myListView.Items.Clear();
            // Here's the bottleneck.
            // ToDo: Need logic to improve performance here..
            for (int i = 0; i < RomList.Count; i++)
                 myListView.Items.Add(RomList[i].Title, i);
            textBoxStatus.Text = $"{RomList.Count} Games (ROM's) found for {systemName}";
            return true;
        }
        public void InitializeDbConnection(string dbPath)
        {
            if (dbPath == null || !File.Exists(dbPath))
            {
                MessageBox.Show($"Error: Can not find {GameLauncherDbName}.\nExpected to find file in following path\n{dbPath}\nPlease select option to set database ({GameLauncherDbName}) location.", "Missing database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            connection = new SqliteConnection($"Filename={dbPath}");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"SELECT Name FROM GameSystems ORDER BY Name";
            List<string> SystemNames = new List<string> ();
            comboBoxSystem.Items.Clear();
            while (SystemNames.Count < 1)
            {
                SystemNames.Clear();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        SystemNames.Add(name);
                        comboBoxSystem.Items.Add(name);
                        Console.WriteLine($"System name , {name}!");
                    }
                }

                if (SystemNames.Count < 1 && InitializeRomsInDatabase(GetEmulatorsBasePath()) == false)
                    return;
            }

            string LastSystemSelected = GetPersistenceVariable("comboBoxSystem");
            comboBoxSystem.Text = LastSystemSelected != null && LastSystemSelected.Length != 0 ? LastSystemSelected : SystemNames[0];
            string LastIconDisplaySelected = GetPersistenceVariable("comboBoxIconDisplay");
            comboBoxIconDisplay.Text = LastIconDisplaySelected != null && LastIconDisplaySelected.Length != 0 ? LastIconDisplaySelected : "Large Icons";
        }
        public static readonly string[] CommonEmulatorPaths = { @"C:\Emulator", @"C:\GameEmulator", @"C:\RetroGameEmulator", @"C:\RetroEmulator", @"C:\Game", @"C:\Retro", @"C:\RetroGame", @"C:\GameRetro" };
        public static readonly string DATA_SUBPATH = @"data\";
        public static readonly string GameLauncher_SUBPATH = @"GameLauncher\";
        public const string GameLauncherDbName = "GameLauncher.db";
        public string GetDbPath_sub()
        {
            if (Properties.Settings.Default.DbPath != null && Properties.Settings.Default.DbPath.Length > 0)
            {
                if (File.Exists(Properties.Settings.Default.DbPath))
                    return Properties.Settings.Default.DbPath;
            }
            string dbPath = Path.Combine($"{binDirPath}{DATA_SUBPATH}", GameLauncherDbName);
            if (File.Exists(dbPath))
                return dbPath;
            foreach (var path in CommonEmulatorPaths)
            {
                dbPath = Path.Combine(path, GameLauncherDbName);
                if (File.Exists(dbPath))
                    return dbPath;
                dbPath = Path.Combine($"{path}s", GameLauncherDbName);
                if (File.Exists(dbPath))
                    return dbPath;

                dbPath = Path.Combine(path, $"{DATA_SUBPATH}{GameLauncherDbName}");
                if (File.Exists(dbPath))
                    return dbPath;
                dbPath = Path.Combine($"{path}s", $"{DATA_SUBPATH}{GameLauncherDbName}");
                if (File.Exists(dbPath))
                    return dbPath;

                dbPath = Path.Combine(path, $"{GameLauncher_SUBPATH}{DATA_SUBPATH}{GameLauncherDbName}");
                if (File.Exists(dbPath))
                    return dbPath;
                dbPath = Path.Combine($"{path}s", $"{GameLauncher_SUBPATH}{DATA_SUBPATH}{GameLauncherDbName}");
                if (File.Exists(dbPath))
                    return dbPath;
            }
            dbPath = Path.Combine(dataDirPath, GameLauncherDbName);
            return dbPath;
        }
        public string GetDbPath()
        {
            string dbPath = GetDbPath_sub();
            if (Properties.Settings.Default.DbPath == null || Properties.Settings.Default.DbPath.Length == 0)
                Properties.Settings.Default.DbPath = dbPath;
            return dbPath;
        }
        public string GetEmulatorsBasePath_sub()
        {
            if (Properties.Settings.Default.EmulatorsBasePath != null && Properties.Settings.Default.EmulatorsBasePath.Length > 0)
            {
                if (Directory.Exists(Properties.Settings.Default.EmulatorsBasePath))
                    return Properties.Settings.Default.EmulatorsBasePath;
            }
            foreach (var path in CommonEmulatorPaths)
            {
                if (File.Exists(path))
                    return path;
                if (File.Exists($"{path}s"))
                    return $"{path}s";
            }
            return @"C:\Emulator";
        }
        public string GetEmulatorsBasePath()
        {
            string emulatorsBasePath = GetEmulatorsBasePath_sub();
            if (Properties.Settings.Default.EmulatorsBasePath == null || Properties.Settings.Default.EmulatorsBasePath.Length == 0)
                Properties.Settings.Default.EmulatorsBasePath = emulatorsBasePath;
            return emulatorsBasePath;
        }
        public string GetDefaultImagePath_sub(string dbPath)
        {
            if (Properties.Settings.Default.DefaultImagePath != null && Properties.Settings.Default.DefaultImagePath.Length > 0)
            {
                if (Directory.Exists(Properties.Settings.Default.DefaultImagePath))
                    return Properties.Settings.Default.DefaultImagePath;
            }
            const string DEFAULTIMAGEFILENAME = "GameController.png";
            string defaultImagePath = Path.Combine(dataDirPath, DEFAULTIMAGEFILENAME);  // This allows user defined default image
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
            if (Properties.Settings.Default.DefaultImagePath == null || Properties.Settings.Default.DefaultImagePath.Length == 0)
                Properties.Settings.Default.DefaultImagePath = defaultImagePath;
            return defaultImagePath;
        }
        public static Form1 form1 = null;
        public static Thread threadJoyStick = null;
        public static bool threadJoyStickAborting = false;
        public Form1()
        {
            InitializeComponent();
            comboBoxIconDisplay.Items.Add("Large Icons");
            comboBoxIconDisplay.Items.Add("Small Icons");
            comboBoxIconDisplay.Items.Add("Tiles");
            comboBoxIconDisplay.Text = "Large Icons";
            // RegexRenameFiles(@"C:\Emulator\NintendoDS\roms", "(roms\\\\)[0-9][0-9][0-9][0-9][\\s_]-[\\s_](.*)", "$1$2"); // ToDo: Delete this line after testing!!!!!!!!!!!
            binDirPath = AppDomain.CurrentDomain.BaseDirectory;
            dataDirPath = AppDomain.CurrentDomain.BaseDirectory; // ToDo: Make this user configurable.
            string dbPath = GetDbPath();
            if (dbPath != null && Directory.Exists(System.IO.Path.GetDirectoryName(dbPath)))
                dataDirPath = System.IO.Path.GetDirectoryName(dbPath);
            defaultImagePath = GetDefaultImagePath(dbPath); // Make sure to do this before InitializeDbConnection
            InitializeDbConnection(dbPath);
            form1 = this;
            threadJoyStick = new Thread(PollJoystick);
            threadJoyStick.Start();
        }
        public static string MiscData = "";
        public static readonly int GamePadDown  = 18000;
        public static readonly int GamePadUp    = 0;
        public static readonly int GamePadLeft  = 27000;
        public static readonly int GamePadRight = 9000;
        private static Dictionary<string, long> dictAvoidRepeat = new Dictionary<string, long>();
        private static bool IsRepeat(string keys, long MaxSecondsToWait = MaxSecondsToWaitDefault)
        {
            keys = $"__{keys}";
            DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
            long now = dto.ToUnixTimeSeconds();
            if (dictAvoidRepeat.ContainsKey(keys) && dictAvoidRepeat[keys] + MaxSecondsToWait > now)
                return true;
            dictAvoidRepeat[keys] = now;
            return false;
        }
        private static bool Send_Keys(string keys, long MaxSecondsToWait = MaxSecondsToWaitDefault)
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
                Console.WriteLine("No joystick/Gamepad found.");
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
                if (threadJoyStickAborting == true)
                    return;
                if (WaitingShellExecuteToComplete)
                    continue;
                //foreach (Joystick js in sticks) 
                //{
                //    JoystickUpdate lastState = js.GetBufferedData().Last();
                //    if (lastState.Offset == JoystickOffset.X)
                //    {
                //        form1.textBoxStatus.Text = lastState.Value.ToString();
                //    }
                //}
                MiscData = "";
                var data = joystick.GetBufferedData();
                foreach (JoystickUpdate state in data)
                {
                    if (state.Offset == JoystickOffset.X)
                    {
                        MiscData += "[x]"; // 1st Joystick left and down
                    }
                    else if (state.Offset == JoystickOffset.Y)
                    {
                        MiscData += "[y]"; // 1st Joystick right and up
                    }
                    else if (state.Offset == JoystickOffset.Z)
                    {
                        MiscData += "[z]"; // Lower trigger [left]val=34000>, [right]val=31000>
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
                        MiscData += "[RotationX]"; // 2nd Joystick left and right
                    }
                    else if (state.Offset == JoystickOffset.RotationY)
                    {
                        MiscData += "[RotationY]";// 2nd Joystick up and down
                    }
                    else if (state.Offset == JoystickOffset.RotationZ)
                    {
                        MiscData += "[RotationZ]";
                    }
                    else if (state.Offset == JoystickOffset.Buttons0)
                    {
                        MiscData += "[Buttons0]"; // A button
                        if (!Send_Keys("{F5}", MaxSecondsToWait_F5))
                            break;
                    }
                    else if (state.Offset == JoystickOffset.Buttons1)
                    {
                        MiscData += "[Buttons1]"; // B button
                        if (!Send_Keys("{TAB}"))
                            break;
                    }
                    else if (state.Offset == JoystickOffset.Buttons2)
                    {
                        MiscData += "[Buttons2]"; // X button
                        Send_Keys("{PGDN}");
                    }
                    else if (state.Offset == JoystickOffset.Buttons3)
                    {
                        MiscData += "[Buttons3]"; // Y button
                        Send_Keys("{PGUP}");
                    }
                    else if (state.Offset == JoystickOffset.Buttons4)
                    {
                        MiscData += "[Buttons4]"; // Left upper trigger
                        if (!Send_Keys("{HOME}"))
                            break;
                    }
                    else if (state.Offset == JoystickOffset.Buttons5)
                    {
                        MiscData += "[Buttons5]"; // Right upper trigger
                        if (!Send_Keys("{F8}"))
                            break;
                    }
                    else if (state.Offset == JoystickOffset.Buttons6)
                    {
                        MiscData += "[Buttons6]"; // Back/Select button
                        System.Windows.Forms.Application.Exit();
                    }
                    else if (state.Offset == JoystickOffset.Buttons7)
                    {
                        MiscData += "[Buttons7]"; // Start button
                        if (!Send_Keys("{F5}", MaxSecondsToWait_F5))
                            break;
                    }
                    else if (state.Offset == JoystickOffset.PointOfViewControllers0)
                    { // PointOfViewControllers0, [up]val=0, [down]val=18000,[left]val=27000,[right]val=9000
                        MiscData += $"[PointOfViewControllers0 {state.Value}]";
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
                        MiscData += "[Buttons8]"; // 1st Joystick center button
                        if (!Send_Keys("{F6}"))
                            break;
                    }
                    else if (state.Offset == JoystickOffset.Buttons9)
                    {
                        MiscData += "[Buttons9]"; // 2nd Joystick center button
                        if (!Send_Keys("{F4}"))
                            break;
                    }
                    else
                    {
                        MiscData += "[misc]";
                    }
                }
            }
        }
        private string GetEmulatorStartScanPath()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Enter base path to start emulator scanning.";

            return fbd.ShowDialog() == DialogResult.OK ? fbd.SelectedPath : null;
        }
        private Rom GetSelectedROM()
        {
            List<int> selectedRoms = new List<int>();
            foreach (ListViewItem item in myListView.SelectedItems)
            {
                selectedRoms.Add(item.Index);
            }
            foreach (ListViewItem item in myListView.SelectedItems)
                return RomList[item.Index];
            return null;
        }
        public static bool WaitingShellExecuteToComplete = false;
        private void PlaySelectedRom()
        {
            lock (myListView) ;
            WaitingShellExecuteToComplete = true;
            foreach (ListViewItem item in myListView.SelectedItems)
            {// ToDo: Add logic so that when a ROM file has been renamed via context menu, that the rom.FilePath is fetched from the DB until GameLauncher has restarted or selected game console has changed.
                Rom rom = RomList[item.Index];
                if (!File.Exists(rom.FilePath))
                {
                    MessageBox.Show($"Error: The selected ROM file no longer exists:\n'{rom.FilePath}'", "ROM not exists!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int preferredEmulator = rom.PreferredEmulatorID > 0 ? rom.PreferredEmulatorID : 1;
                string emulatorExecutable = GetEmulatorExecutable(comboBoxSystem.Text, preferredEmulator);
                string execCommand = $"\"{emulatorExecutable}\" \"{rom.FilePath}\"";
                var prevWinState = this.WindowState;
                this.WindowState = FormWindowState.Minimized;
                Process process = Process.Start(new ProcessStartInfo($"\"{emulatorExecutable}\"", $"\"{rom.FilePath}\"")
                {
                    UseShellExecute = true
                });
                process.WaitForExit();
                WaitingShellExecuteToComplete = false;
                this.WindowState = prevWinState;
                break; // Only get the first item....
            }
        }
        private void myListView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void myListView_ItemMouseHover(Object sender, ListViewItemMouseHoverEventArgs e)
        {
            Rom rom = RomList[e.Item.Index];
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
        private void myListViewContextMenu_Play_Click(object sender, EventArgs e)
        {
            PlaySelectedRom();
        }
        private void myListViewContextMenu_RenameROM_Click(object sender, EventArgs e)
        {
            Rom rom = GetSelectedROM();
            string ext =  Path.GetExtension(rom.FilePath);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"ROM File (*{ext})|*{ext}|All Files (*.*)|*.*";
            saveFileDialog.Title = "Enter new ROM file name";
            saveFileDialog.FileName = rom.FilePath;
            saveFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(rom.FilePath);
            saveFileDialog.CheckFileExists = false;
            DialogResult results = saveFileDialog.ShowDialog();
            if (results == DialogResult.Cancel)
                return;
            if (saveFileDialog.FileName == "")
            {
                MessageBox.Show($"Error: Entered invalid file name for ROM file\n'{rom.Title}'", "Invalid Name!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (saveFileDialog.FileName.ToLower().Equals(rom.FilePath.ToLower()))
            {
                MessageBox.Show($"Nothing to do, because the new file name is the same as the old file name:\n{saveFileDialog.FileName}", "Same Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool needToDelRomFromDbBeforeUpdate = File.Exists(saveFileDialog.FileName);
            File.Move(rom.FilePath, saveFileDialog.FileName);
            if (needToDelRomFromDbBeforeUpdate) 
                UpdateDB($"DELETE FROM Roms WHERE FilePath = \"{saveFileDialog.FileName}\"");
            UpdateDB($"UPDATE Roms SET FilePath = \"{saveFileDialog.FileName}\" WHERE FilePath = \"{rom.FilePath}\"");
            MessageBox.Show($"ROM '{rom.Title}' renamed to '{saveFileDialog.FileName}'.");
            DeleteImageList(comboBoxSystem.Text);
        }
        private void myListViewContextMenu_DeleteROM_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Keys mods = System.Windows.Forms.Control.ModifierKeys;
            Rom rom = GetSelectedROM();
            if ((mods & System.Windows.Forms.Keys.Control) > 0 ||  // If Alt key is held while selecting deletion option, than do silent delete.
                MessageBox.Show($"Are you sure you want to DELETE ROM with title\n'{rom.Title}'\nand having file name\n'{rom.FilePath}'", "Delete ROM", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                File.Delete(rom.FilePath);
                UpdateDB($"DELETE FROM Roms WHERE FilePath = \"{rom.FilePath}\"");
                DeleteImageList(comboBoxSystem.Text);
            }
        }
        private void myListViewContextMenu_AssignPreferredEmulator_Click(object sender, EventArgs e)
        {// ToDo:
            Rom rom = GetSelectedROM();
            if (rom == null)
                return;
            int preferredEmulator = rom.PreferredEmulatorID > 0 ? rom.PreferredEmulatorID : 1;
            string emulatorExecutable = GetEmulatorExecutable(comboBoxSystem.Text, preferredEmulator);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"Emulator Executable File (*.exe)|*.exe|All Files (*.*)|*.*";
            saveFileDialog.Title = "Select preferred emulator executable";
            saveFileDialog.FileName = emulatorExecutable;
            saveFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(emulatorExecutable);
            saveFileDialog.CheckFileExists = true;
            DialogResult results = saveFileDialog.ShowDialog();
            if (results == DialogResult.Cancel)
                return;
            if (saveFileDialog.FileName == "" || !File.Exists(saveFileDialog.FileName))
            {
                MessageBox.Show($"Error: Entered invalid file name for emulator", "Invalid Name!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (saveFileDialog.FileName.ToLower().Equals(rom.FilePath.ToLower()))
            {
                MessageBox.Show($"Nothing to do, because the new file name is the same as the old file name:\n{saveFileDialog.FileName}", "Same Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Check if selected emulator already exists in DB
            int emulatorIndex = -1;
            int lastPopulatedEmulator = -1;
            for (int i = 1; i < 11; i++)
            {
                string sql = $"SELECT EmulatorPath{i} FROM GameSystems WHERE Name like \"{comboBoxSystem.Text}\"";
                string s = GetFirstColStr(sql);
                if (s != null && s.Length > 0)
                {
                    if (s.ToLower().Equals(saveFileDialog.FileName.ToLower()))
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
                    MessageBox.Show($"Error: Can not set emulator because there's already 10 emulators associated with game console system {comboBoxSystem.Text}", "Can not set emulator!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                emulatorIndex = lastPopulatedEmulator + 1;
                UpdateDB($"UPDATE GameSystems SET EmulatorPath{emulatorIndex} = \"{saveFileDialog.FileName}\" WHERE Name like \"{comboBoxSystem.Text}\"");
            }
            if (emulatorIndex != -1)
            {
                UpdateDB($"UPDATE Roms SET PreferredEmulator = {emulatorIndex} WHERE FilePath LIKE \"{rom.FilePath}\"");
                MessageBox.Show($"Emulator updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void myListView_button_Rescan_Click(object sender, EventArgs e)
        {// ToDo: 
        }
        private void myListViewContextMenu_ChangeAssignedImage_Click(object sender, EventArgs e)
        {// ToDo: Finish below code for selecting a different image file.....
            Rom rom = GetSelectedROM();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"Image File (*.png,*.bmp)|*.png;*.bmp|All Files (*.*)|*.*";
            saveFileDialog.Title = $"Select new image file to assign for ROM '{rom.Title}'";
            saveFileDialog.FileName = rom.ImagePath;
            saveFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(rom.ImagePath);
            saveFileDialog.CheckFileExists = true;
            saveFileDialog.OverwritePrompt = false;
            DialogResult results = saveFileDialog.ShowDialog();
            if (results == DialogResult.Cancel)
                return;
            if (saveFileDialog.FileName == "")
            {
                MessageBox.Show("Error: Entered invalid file name.", "Invalid Name!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (saveFileDialog.FileName.ToLower().Equals(rom.ImagePath.ToLower()))
            {
                MessageBox.Show($"Nothing to do, because the new file name is the same as the old file name:\n{saveFileDialog.FileName}", "Same Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int imageID = GetImageID(saveFileDialog.FileName);
            UpdateDB($"UPDATE Roms SET ImagePath = \"{saveFileDialog.FileName}\", ImageID = {imageID} WHERE FilePath = \"{rom.FilePath}\"");
            MessageBox.Show($"ROM '{rom.Title}' associated image changed. The change will not be seen on the list until restarting GameLauncher or until changing game console selection.");
            DeleteImageList(comboBoxSystem.Text);
        }
        private void myListViewContextMenu_ChangeTitle_Click(object sender, EventArgs e)
        {// ToDo: using Microsoft.VisualBasic;
        }
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
        private void myListView_OnDbClick(object sender, EventArgs e)
        {
            PlaySelectedRom();
        }
        private void comboBoxSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPersistenceVariable("comboBoxSystem", comboBoxSystem.Text);
            DisplaySystemIcons(comboBoxSystem.Text);
        }
        private void comboBoxIconDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPersistenceVariable("comboBoxIconDisplay", comboBoxIconDisplay.Text);
            // Excluding View.SmallIcon;
            if (comboBoxIconDisplay.SelectedIndex == 0)
                myListView.View = View.LargeIcon;
            else if (comboBoxIconDisplay.SelectedIndex == 1)
                myListView.View = View.List;
            else if (comboBoxIconDisplay.SelectedIndex == 2)
                myListView.View = View.Tile;

        }

        private void myListView_OnFormClosing(object sender, FormClosingEventArgs e)
        {
            threadJoyStickAborting = true;
        }
        const long MaxSecondsToWait_F5 = 5;
        const long MaxSecondsToWaitDefault = 2;
        private void myListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (comboBoxSystem.SelectedIndex == 0)
                    comboBoxSystem.SelectedIndex = comboBoxSystem.Items.Count - 1;
                else
                    comboBoxSystem.SelectedIndex -= 1;
            }
            else if (e.KeyCode == Keys.F3)
            {
                if (comboBoxSystem.SelectedIndex == comboBoxSystem.Items.Count - 1)
                    comboBoxSystem.SelectedIndex = 0;
                else
                    comboBoxSystem.SelectedIndex += 1;
            }
            else if (e.KeyCode == Keys.F5)
            {
                if (IsRepeat("F5", MaxSecondsToWait_F5))
                    return;
                PlaySelectedRom();
            }
            else if (e.KeyCode == Keys.F6)
            {
                if (IsRepeat("F6"))
                    return;
                if (this.WindowState == FormWindowState.Normal)
                    this.WindowState = FormWindowState.Maximized;
                else if (this.WindowState == FormWindowState.Maximized)
                    this.WindowState = FormWindowState.Normal;
            }
            else if (e.KeyCode == Keys.F7)
            {
                if (IsRepeat("F7"))
                    return;
                this.WindowState = FormWindowState.Minimized;
            }
            else if (e.KeyCode == Keys.F8)
            {
                if (IsRepeat("F8"))
                    return;
                if (comboBoxIconDisplay.SelectedIndex == 2)
                    comboBoxIconDisplay.SelectedIndex = 0;
                else
                    comboBoxIconDisplay.SelectedIndex += 1;
            }
            else if (e.KeyCode == Keys.F9)
            {
                if (IsRepeat("F9"))
                    return;
                if (comboBoxIconDisplay.SelectedIndex == 0)
                    comboBoxIconDisplay.SelectedIndex = 2;
                else
                    comboBoxIconDisplay.SelectedIndex -= 1;
            }
            else if (e.KeyCode == Keys.F10)
                comboBoxIconDisplay.SelectedIndex = 0;
            else if (e.KeyCode == Keys.F11)
                comboBoxIconDisplay.SelectedIndex = 1;
            else if (e.KeyCode == Keys.F12)
                comboBoxIconDisplay.SelectedIndex = 2;
        }
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
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
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
    public class Rom
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
        public int ImageID;
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
        public Rom(string nameTruncated, int systemID, string path, string nameOrg, string title, string compressed, int id = -1, int preferredEmulatorID = 0, string imagePath = null, int qtyPlayers = 0, string region = null, string developer = null, long romSize = 0, string genre = null, string notesCore = null, string notesUser = null, string fileFormat = null, string releaseDate = null, string status = null, string version = null, string description = null, string language = null)
        {
            NameSimplified = nameTruncated;
            System = systemID;
            FilePath = path;
            NameOrg = nameOrg;
            Title = title;
            Compressed = compressed;
            ImageID = id;
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
        }
    }
    public class GameImage
    {
        public string NameSimplified;
        public string NameOrg;
        public string Compressed;
        public string FilePath;
        public int ImageID;
        public GameImage(string nameTruncated, string path, string nameOrg, string compressed, int id = -1)
        {
            NameSimplified = nameTruncated;
            FilePath = path;
            NameOrg = nameOrg;
            Compressed = compressed;
            ImageID = id;
        }
    }
    [Serializable()]
    public class FlatImage
    {
        public FlatImage(System.Drawing.Image image, string key)
        {
            _image = image;
            _key = key == null ? "" : key;
        }
        public FlatImage()
        {
            _image = null;
            _key = "";
        }
        public System.Drawing.Image _image { get; set; }
        public string _key { get; set; }
    }
    public class SerializableImageList
    {
        public SerializableImageList()
        {
        }
        public static bool Save(ImageList imageList, FileStream stream, bool closeBeforeExit = false)
        {
            List<FlatImage> fis = new List<FlatImage>();
            for (int index = 0; index < imageList.Images.Count; index++)
                fis.Add(new FlatImage(imageList.Images[index], imageList.Images.Keys[index]));
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, fis);
            if (closeBeforeExit)
                stream.Close();
            return true;
        }
        public static bool Save(ImageList imageList, string filePath)
        {
            FileStream stream = File.OpenWrite(filePath);
            return Save(imageList, stream, true);
        }
        public static ImageList Load(FileStream stream, int imgSize, bool doMultiThread, bool closeBeforeExit = false)
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(imgSize, imgSize);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                List<FlatImage> ilc = formatter.Deserialize(stream) as List<FlatImage>;
                for (int index = 0; index < ilc.Count; index++)
                {
                    System.Drawing.Image i = ilc[index]._image;
                    string key = ilc[index]._key;
                    imageList.Images.Add(key as string, i);
                }
                if (closeBeforeExit)
                    stream.Close();
                return imageList;
            }
            catch { }
            if (closeBeforeExit)
                stream.Close();
            return null;
        }
        public static ImageList Load(string filePath, int imgSize, bool doMultiThread = false)
        {
            FileStream stream = File.OpenRead(filePath);
            return Load(stream, imgSize, doMultiThread, true);
        }
    }
}
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace GameLauncher
{
    public partial class Form1 : Form
    {
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
        static ImageList[] tmpImageList1 = null;
        static ImageList[] tmpImageList2 = null;
        static List<int> StartPosList = null;
        static List<Rom> RomList = new List<Rom>();

        static void ProcessImageListTask(int setId, int listID, int startPos, int lastPos)
        {
            Console.WriteLine($"Starting set{setId} with list{listID}. Start pos={startPos} and last pos={lastPos}.");
            for (int i = startPos; i < lastPos; i++)
            {
                if (setId == 0)
                    tmpImageList1[listID].Images.Add(System.Drawing.Image.FromFile(RomList[i].ImagePath));
                else
                    tmpImageList2[listID].Images.Add(System.Drawing.Image.FromFile(RomList[i].ImagePath));

            }
            if (setId == 0)
                Console.WriteLine($"Completed set{setId}, list{listID} with count = {tmpImageList1[listID].Images.Count}.");
            else
                Console.WriteLine($"Completed set{setId}, list{listID} with count = {tmpImageList2[listID].Images.Count}.");
        }
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
            var command = new SqliteCommand(sql, connection);
            var reader = command.ExecuteReader();
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
            var command = new SqliteCommand(sql, connection);
            var reader = command.ExecuteReader();
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
            var command = new SqliteCommand(sql, connection);
            var reader = command.ExecuteReader();
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
            var command = new SqliteCommand(sql, connection);
            var x = command.ExecuteNonQuery();
            return x > 0;
        }
        private int GetImageIndexByPath(string FilePath)
        {
            string sql = $"SELECT ID FROM Images WHERE FilePath = \"{FilePath}\"";
            return GetFirstColInt(sql);

        }
        private (int, string) GetImageIndexByName(string NameSimplified, string NameOrg, string Title, string Compressed)
        {
            string sql = $"SELECT ID, FilePath FROM Images WHERE NameSimplified = \"{NameSimplified}\"";
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
                    sql = $"SELECT ID, FilePath FROM Images WHERE NameSimplified = \"{NameAfterHyphenSimple}\"";
                    (idx, imgPath) = GetIntAndStr(sql);
                    if (idx < 0)
                    {
                        string nameWithNoNumbers = ConvertToSimplifiedName(NameAfterHyphen, true);
                        sql = $"SELECT ID, FilePath FROM Images WHERE NameSimplified = \"{nameWithNoNumbers}\"";
                        (idx, imgPath) = GetIntAndStr(sql);
                    }
                }
                if (idx < 0)
                {
                    string nameWithNoNumbers = ConvertToSimplifiedName(NameSimplified, true); // Remove numbers from the name. It's better to have an image to the older version of the game, than non at all.
                    if (!nameWithNoNumbers.Equals(NameSimplified))
                    {
                        sql = $"SELECT ID, FilePath FROM Images WHERE NameSimplified = \"{nameWithNoNumbers}\"";
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
                    sql = $"SELECT ID, FilePath FROM Images WHERE Compressed = \"{Compressed}\"";
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
        private int GetSystemIndex(string Name)
        {
            string sql = $"SELECT ID FROM GameSystems WHERE Name = \"{Name}\"";
            return GetFirstColInt(sql);
        }
        private void GetImages(string imgPath)
        {
            if (Directory.Exists(imgPath))
            {
                string[] imgFiles = Directory.GetFiles(imgPath, "*.png");
                foreach (string f in imgFiles)
                {
                    string NameOrg = Path.GetFileNameWithoutExtension(f);
                    string NameSimplified = ConvertToSimplifiedName(NameOrg);
                    string Compressed = ConvertToSimplifiedName(NameOrg, true, true);
                    string sql = $"INSERT OR REPLACE INTO Images (NameSimplified, NameOrg, Compressed, FilePath) VALUES (\"{NameSimplified}\", \"{NameOrg}\", \"{Compressed}\", \"{f}\")";
                    var command = new SqliteCommand(sql, connection);
                    var x = command.ExecuteNonQuery();
                    int id = GetImageIndexByPath(f);
                }
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
        private bool InitializeRomsInDatabase(string startingPath = @"C:\Emulators", string romSubFolderName = @"\roms", string imageSubFolderName = @"\images", bool createTables = false)
        {
            if (createTables) 
                CreateSqlTables();
            // Check for master image path. Note: These images can apply to any game in any system which matches truncated name
            string masterImgPath = startingPath + imageSubFolderName;
            if (Directory.Exists(masterImgPath))
                GetImages(masterImgPath);

            DirectoryInfo dirinfo = new DirectoryInfo(startingPath);
            string[] emulatorDirs = Directory.GetDirectories(startingPath, "*", SearchOption.TopDirectoryOnly);
            foreach (string emulatorDir in emulatorDirs)
            {
                string romPath = emulatorDir + romSubFolderName;
                string imgPath = emulatorDir + imageSubFolderName;
                EmulatorExecutables emulatorPaths = GetEmulatorPaths(emulatorDir);
                if (Directory.Exists(romPath) && emulatorPaths != null)
                {
                    string Name = emulatorDir.Substring(startingPath.Length + 1);
                    var sql = $"INSERT OR REPLACE INTO GameSystems (Name, RomDirPath, ImageDirPath, EmulatorPath1, EmulatorPath2, EmulatorPath3, EmulatorPath4, EmulatorPath5, EmulatorPath6, EmulatorPath7, EmulatorPath8, EmulatorPath9, EmulatorPath10) VALUES " +
                        $"(\"{Name}\", \"{romPath}\", \"{imgPath}\", \"{emulatorPaths.EmulatorPaths[0]}\", \"{emulatorPaths.EmulatorPaths[1]}\", \"{emulatorPaths.EmulatorPaths[2]}\", \"{emulatorPaths.EmulatorPaths[3]}\", \"{emulatorPaths.EmulatorPaths[4]}\", \"{emulatorPaths.EmulatorPaths[5]}\", \"{emulatorPaths.EmulatorPaths[6]}\", \"{emulatorPaths.EmulatorPaths[7]}\", \"{emulatorPaths.EmulatorPaths[8]}\", \"{emulatorPaths.EmulatorPaths[9]}\")";
                    var command = new SqliteCommand(sql, connection);
                    var x = command.ExecuteNonQuery();
                    int systemIndex = GetSystemIndex(Name);
                    Console.WriteLine($"The game system '{Name}' has been created successfully into row {systemIndex}");
                    GameSystems.Add(new GameSystem(Name, romPath, imgPath, emulatorPaths, systemIndex));
                    if (Directory.Exists(imgPath))
                        GetImages(imgPath);

                    string[] romFiles = Directory.GetFiles(romPath);
                    foreach (string f in romFiles)
                    {
                        long RomSize = new System.IO.FileInfo(f).Length;
                        string ext = Path.GetExtension(f).ToLower();
                        if (ext.Equals("sav"))
                            continue;
                        if (RomSize < MINIMUM_ROM_SIZE)
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
            command.CommandText = $"SELECT {fieldNames} FROM Roms WHERE System = \"{SystemID}\"";
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
                        ImagePath = dataDirPath + @"\GameController.png"; // Assembly.GetEntryAssembly().Location
                    }

                    RomList.Add(new Rom(NameSimplified, System,FilePath, NameOrg, Title, Compressed, ImageID,
                        PreferredEmulator, ImagePath, QtyPlayers, Region, 
                        Developer, RomSize, Genre, NotesCore, NotesUser, 
                        FileFormat, ReleaseDate, Status, Version, Description, Language));
                }
            }
            return RomList.Count;
        }
        private PreviousCollectedImages GetSavedCollectionData(string systemName)
        {
            string imageList1Path = Path.Combine(dataDirPath, $"GameLauncher_{systemName}.ImageList1.collection");
            string imageList2Path = Path.Combine(dataDirPath, $"GameLauncher_{systemName}.ImageList2.collection");
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
            string imageList1Path = Path.Combine(dataDirPath, $"GameLauncher_{systemName}.ImageList1.collection");
            string imageList2Path = Path.Combine(dataDirPath, $"GameLauncher_{systemName}.ImageList2.collection");
            SerializableImageList.Save(previousCollectedImages.list1, imageList1Path);
            SerializableImageList.Save(previousCollectedImages.list2, imageList2Path);
        }
        private string dataDirPath = null;
        private string binDirPath = null;
        private Dictionary<string, PreviousCollectedData> previousCollections = new Dictionary<string, PreviousCollectedData>();
        private const int largeIconSize = 128;
        private const int smallIconSize = 32;
        private const int MaxNumberThreadPerList = 2; // ToDo: Make this user changeable
        public bool DisplaySystemIcons(string systemName, bool usePreviousCollectionCache = false)
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
                    if (false && RomList.Count > MaxNumberThreadPerList * 3)
                    { // !!!!! There's a bug in this code. Don't use until issue is resolve !!!!!
                        StartPosList = new List<int>();
                        tmpImageList1 = new ImageList[MaxNumberThreadPerList];
                        tmpImageList2 = new ImageList[MaxNumberThreadPerList];
                        for (int i = 0; i < MaxNumberThreadPerList; ++i)
                        {
                            StartPosList.Add(RomList.Count / MaxNumberThreadPerList * i);

                            //ImageList tmpImage1 = new ImageList();
                            //tmpImage1.ImageSize = new Size(largeIconSize, largeIconSize);
                            tmpImageList1[i] = myImageList1; // tmpImage1;
                                                             //ImageList tmpImage2 = new ImageList();
                                                             //tmpImage2.ImageSize = new Size(smallIconSize, smallIconSize);
                            tmpImageList2[i] = myImageList2; // tmpImage2;
                        }
                        StartPosList.Add(RomList.Count);
                        Task[] tasks = new Task[MaxNumberThreadPerList * 2];
                        for (int idx = 0; idx < MaxNumberThreadPerList; ++idx)
                        {
                            tasks[idx] = Task.Run(() => ProcessImageListTask(0, idx, StartPosList[idx], StartPosList[idx + 1]));
                            tasks[idx + MaxNumberThreadPerList] = Task.Run(() => ProcessImageListTask(1, idx, StartPosList[idx], StartPosList[idx + 1]));

                            //tasks[idx] =                            Task.Factory.StartNew(() => ProcessImageListTask(0, idx, StartPosList[idx], StartPosList[idx + 1]));
                            //tasks[idx + MaxNumberThreadPerList] =   Task.Factory.StartNew(() => ProcessImageListTask(1, idx, StartPosList[idx], StartPosList[idx + 1]));
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
            // ListViewItemToolTipChanged; SetItemText(item.Index, 0, item.Text);
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
            command.CommandText = @"SELECT Name FROM GameSystems";
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
        public string GetDbPath()
        {
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
        public string GetEmulatorsBasePath()
        {
            foreach (var path in CommonEmulatorPaths)
            {
                if (File.Exists(path))
                    return path;
                if (File.Exists($"{path}s"))
                    return $"{path}s";
            }
            return @"C:\Emulator";
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
            InitializeDbConnection(dbPath);
            form1 = this;
            threadJoyStick = new Thread(PollJoystick);
            threadJoyStick.Start();
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public static string MiscData = "";
        public static readonly int GamePadDown  = 18000;
        public static readonly int GamePadUp    = 0;
        public static readonly int GamePadLeft  = 27000;
        public static readonly int GamePadRight = 9000;
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
                        SendKeys.SendWait("{END}");
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
                        SendKeys.SendWait("{ENTER}");
                    }
                    else if (state.Offset == JoystickOffset.Buttons1)
                    {
                        MiscData += "[Buttons1]"; // B button
                        SendKeys.SendWait("{TAB}");
                    }
                    else if (state.Offset == JoystickOffset.Buttons2)
                    {
                        MiscData += "[Buttons2]"; // X button
                        SendKeys.SendWait("{PGDN}");
                    }
                    else if (state.Offset == JoystickOffset.Buttons3)
                    {
                        MiscData += "[Buttons3]"; // Y button
                        SendKeys.SendWait("{PGUP}");
                    }
                    else if (state.Offset == JoystickOffset.Buttons4)
                    {
                        MiscData += "[Buttons4]"; // Left upper trigger
                        SendKeys.SendWait("{HOME}");
                    }
                    else if (state.Offset == JoystickOffset.Buttons5)
                    {
                        MiscData += "[Buttons5]"; // Right upper trigger
                        SendKeys.SendWait("{HOME}");
                    }
                    else if (state.Offset == JoystickOffset.Buttons6)
                    {
                        MiscData += "[Buttons6]"; // Back/Select button
                        SendKeys.SendWait("{ALT}{F4}");
                    }
                    else if (state.Offset == JoystickOffset.Buttons7)
                    {
                        MiscData += "[Buttons7]"; // Start button
                    }
                    else if (state.Offset == JoystickOffset.PointOfViewControllers0)
                    { // PointOfViewControllers0, [up]val=0, [down]val=18000,[left]val=27000,[right]val=9000
                        MiscData += $"[PointOfViewControllers0 {state.Value}]";
                        if (GamePadUp == state.Value)
                        {
                            SendKeys.SendWait("{UP}");
                        }
                        else if (GamePadDown == state.Value)
                        {
                            SendKeys.SendWait("{DOWN}");
                        }
                        else if (GamePadLeft == state.Value)
                        {
                            SendKeys.SendWait("{LEFT}");
                        }
                        else if (GamePadRight == state.Value)
                        {
                            SendKeys.SendWait("{RIGHT}");
                        }
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
            foreach (ListViewItem item in myListView.SelectedItems)
                return RomList[item.Index];
            return null;
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
            
            //System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            //messageBoxCS.AppendFormat("{0} = {1}", "Item", e.Item);
            //messageBoxCS.AppendLine();
            //MessageBox.Show(messageBoxCS.ToString(), $"ItemMouseHover Event ID{e.Item.Index}");
        }
        private void myListViewContextMenu_Play_Click(object sender, EventArgs e)
        {
            myListView_OnDbClick(sender, e);
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
            }
        }
        private void myListViewContextMenu_AssignPreferredEmulator_Click(object sender, EventArgs e)
        {// ToDo: 
            //if (e.Alt)
            //{
            //    MessageBox.Show("Alt key was pressed!");
            //}
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
            // ToDo: Add logic to see if the image is in the DB, and if so, get the imageID. If not in DB, than add it, and than get the imageID.
            UpdateDB($"UPDATE Roms SET ImagePath = \"{saveFileDialog.FileName}\", ImageID = -1 WHERE FilePath = \"{rom.FilePath}\"");
            MessageBox.Show($"ROM '{rom.Title}' associated image changed. The change will not be seen on the list until restarting GameLauncher or until changing game console selection.");
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
                Process.Start(new ProcessStartInfo($"\"{emulatorExecutable}\"", $"\"{rom.FilePath}\"")
                {
                    UseShellExecute = true
                });
                break; // Only get the first item....
            }
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
using Microsoft.Data.Sqlite;

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties;
using static System.Data.Entity.Infrastructure.Design.Executor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GameLauncher
{
    public class Db
    {
        #region Data members and constructor
        private static Form_Main form_Main = null;
        public const string UNIT_TEST_DB_NAME = "UnitTestDatabase.db";
        public const string UNIT_TEST_TITLE = "Unit Testing Error!!!";
        public const string UNIT_TEST_PROCESS = "Db.UnitTestDbClass()";
        private Microsoft.Data.Sqlite.SqliteConnection unit_test_conn = null;
        private const int MAX_TEST_ITEMS = 20;
        public int UnitTestFailCount { get; private set; } = 0;
        public static SqliteConnection connection_memory = null;
        public static SqliteConnection connection_AlternateNames = null;
        public static SqliteConnection connection_ImageExternal = null;
        public const string ALTERNATENAMES_DB_NAME = "GamesAlternateName.db";
        public const string IMAGE_EXTERNAL_DB_NAME = "ImageExternal.db";
        public Db(Form_Main form_Main) 
        { 
            Db.form_Main = form_Main;
        }
        #endregion
        #region Misc functions
        public bool Update(string sql, SqliteConnection conn)
        {
            if (conn == null)
                conn = unit_test_conn == null ? Form_Main.connection : unit_test_conn;
            System.Windows.Forms.Application.DoEvents();
            return Form_Main.UpdateDB(sql, conn);
        }
        public bool ErrorMsg(string msg, string title = null, string processName = null, string circumstances = "", int errorCode = 0)
        {
            Form_Main.ErrorMessage(msg, title == null ? UNIT_TEST_TITLE : title, processName == null ? UNIT_TEST_PROCESS : processName, circumstances, errorCode);
            return false;
        }
        public bool ErrorMsg(string msg, int errorCode, string circumstances = "") => ErrorMsg(msg, UNIT_TEST_TITLE, UNIT_TEST_PROCESS, circumstances, errorCode);
        public List<string> GetList(string sql, SqliteConnection conn = null)
        {
            if (conn == null)
                conn = Form_Main.connection;
            Form_Main.InfoLogging($"db.GetList called with sql = {sql}", false);
            List<string> returnValues = new List<string>();
            try
            {
                using (SqlReader reader = new SqlReader(sql, conn))
                {
                    while (reader.Read())
                    {
                        string item = reader.GetString(0);
                        item.Trim(' ');
                        if (item.Length > 0)
                            returnValues.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Form_Main.ErrorLogging($"db.GetList exception thrown \"{ex.Message}\"!\nsql={sql}");
            }
            return returnValues;
        }
        public static SqliteConnection ConnectToAlternateNames()
        {
            connection_AlternateNames = CreateConnection($"Filename={form_Main.GetPropertyDataPath(ALTERNATENAMES_DB_NAME)}");
            connection_AlternateNames.Open();
            return connection_AlternateNames;
        }
        public static void CloseAlternateNames()
        {
            if (connection_AlternateNames == null)
                return;
            connection_AlternateNames.Dispose();
            connection_AlternateNames = null;
        }
        public static SqliteConnection ConnectToImageExternal()
        {
            connection_ImageExternal = CreateConnection($"Filename={form_Main.GetPropertyDataPath(IMAGE_EXTERNAL_DB_NAME)}");
            connection_ImageExternal.Open();
            return connection_ImageExternal;
        }
        public static void CloseImageExternal()
        {
            if (connection_ImageExternal == null)
                return;
            connection_ImageExternal.Dispose();
            connection_ImageExternal = null;
        }
        public static SqliteConnection CreateConnection(string connectionString)
        {
            SqliteConnection conn = new SqliteConnection(connectionString);
            conn.CreateFunction("Levenshtein", (string source1, string source2) => StrCompare.GetDifference(source1, source2));
            conn.CreateFunction("Difference", (string source1, string source2) => StrCompare.Difference(source1, source2));
            conn.CreateFunction("VerySimilar", (string source1, string source2) => StrCompare.VerySimilar(source1, source2));
            conn.CreateFunction("Similar", (string source1, string source2) => StrCompare.Similar(source1, source2));
            conn.CreateFunction("SomeWhatSimilar", (string source1, string source2) => StrCompare.SomeWhatSimilar(source1, source2));
            conn.CreateFunction("SlightlySimilar", (string source1, string source2) => StrCompare.SlightlySimilar(source1, source2));
            conn.CreateFunction("HardlySimilar", (string source1, string source2) => StrCompare.HardlySimilar(source1, source2));
            conn.CreateFunction("NotSimilar", (string source1, string source2) => StrCompare.NotSimilar(source1, source2));
            conn.CreateFunction("HowSimilar", (string source1, string source2) => StrCompare.HowSimilar(source1, source2, false));

            conn.CreateFunction("PhraseVerySimilar", (string source1, string source2) => StrCompare.PhraseVerySimilar(source1, source2));
            conn.CreateFunction("PhraseSimilar", (string source1, string source2) => StrCompare.PhraseSimilar(source1, source2));
            conn.CreateFunction("PhraseSomeWhatSimilar", (string source1, string source2) => StrCompare.PhraseSomeWhatSimilar(source1, source2));
            conn.CreateFunction("PhraseSlightlySimilar", (string source1, string source2) => StrCompare.PhraseSlightlySimilar(source1, source2));
            conn.CreateFunction("PhraseHardlySimilar", (string source1, string source2) => StrCompare.PhraseHardlySimilar(source1, source2));
            return conn;
        }
        public static void UnitTestCreatedFunctions()
        {
            // connection_memory = new SqliteConnection(":memory:");
            Form_Main.InfoLogging("This is a test............");
            using (SqlReader reader = new SqlReader("select title, Difference('Adventur',title)  as c1, length(title) as tlen from roms where c1 < length(title)", Form_Main.connection))
            {
                while (reader.Read())
                {
                    string title = reader.GetString("title");
                    string c1 = reader.GetString("c1");
                    string tlen = reader.GetString("tlen");
                    Form_Main.InfoLogging($"title = {title}, c1 = {c1}, tlen = {tlen}");
                }
            }
            using (SqlReader reader = new SqlReader("select title, Similar('Adventur',title) as c1, VerySimilar('Adventur',title) as c2 from roms where c1 = 1", Form_Main.connection))
            {
                while (reader.Read())
                {
                    string title = reader.GetString("title");
                    string c1 = reader.GetString("c1");
                    string c2 = reader.GetString("c2");
                    Form_Main.InfoLogging($"title = {title}, c1 = {c1}");
                }
            }
            using (SqlReader reader = new SqlReader("select title, SomeWhatSimilar('Adventur',title) as c1, Similar('Adventur',title) as c2 from roms where c1 = 1 and c2 = 0", Form_Main.connection))
            {
                while (reader.Read())
                {
                    string title = reader.GetString("title");
                    string c1 = reader.GetString("c1");
                    string c2 = reader.GetString("c2");
                    Form_Main.InfoLogging($"title = {title}, c1 = {c1}");
                }
            }
            using (SqlReader reader = new SqlReader("select title, SlightlySimilar('Adventur',title) as c1, SomeWhatSimilar('Adventur',title) as c2 from roms where c1 = 1 and c2 = 0", Form_Main.connection))
            {
                while (reader.Read())
                {
                    string title = reader.GetString("title");
                    string c1 = reader.GetString("c1");
                    string c2 = reader.GetString("c2");
                    Form_Main.InfoLogging($"title = {title}, c1 = {c1}");
                }
            }
            using (SqlReader reader = new SqlReader("select title, PhraseSomeWhatSimilar('Super Mario',title) as c1, PhraseSimilar('Super Mario',title) as c2 from roms where c1 = 1", Form_Main.connection))
            {
                while (reader.Read())
                {
                    string title = reader.GetString("title");
                    string c1 = reader.GetString("c1");
                    string c2 = reader.GetString("c2");
                    Form_Main.InfoLogging($"title = {title}, c1 = {c1}");
                }
            }
            Form_Main.InfoLogging("Test complete");
        }
        public bool UnitTestDbClass()
        {
            #region Get data from default database first so there's something to add to test database
            List<Rom> RomsSrc = null;
            if (!Get(ref RomsSrc, MAX_TEST_ITEMS, 0))
                return ErrorMsg("Rom list get from default database failed!");
            List<GameImage> GameImagesSrc = null;
            if (!Get(ref GameImagesSrc, MAX_TEST_ITEMS))
                return ErrorMsg("GameImage list get from default database failed!");
            List<Roms_UserChanges> Roms_UserChangessSrc = null;
            if (!Get(ref Roms_UserChangessSrc))
            {
                // If none available, lets add some
                Roms_UserChangessSrc = new List<Roms_UserChanges>();
                foreach(Rom r in RomsSrc)
                {
                    Roms_UserChangessSrc.Add(new Roms_UserChanges(r.FilePath, $"Fake User Title Change - {r.Title}", r.PreferredEmulatorID, r.ImagePath, r.QtyPlayers, r.Status, r.Region, r.Developer, r.ReleaseDate, r.Genre, r.NotesCore, $"Fake User NotesUser Change - {r.NotesUser}", r.Version, r.Description, r.Language, r.Year, r.Rating, r.Publisher, r.WikipediaURL, r.PlayCount, r.Favorite, r.Disable, r.Hide));
                }
            }
            List<Mru> MrusSrc = null;
            if (!Get(ref MrusSrc))
            {
                // If none available, lets add some
                MrusSrc = new List<Mru>();
                foreach (Rom r in RomsSrc)
                {
                    MrusSrc.Add(new Mru(r.FilePath, $"{r.ReleaseDate}"));
                }
            }
            List<InitialDefaultEmulatorAttributes> InitialDefaultEmulatorAttributessSrc = null;
            if (!Get(ref InitialDefaultEmulatorAttributessSrc))
                return ErrorMsg("InitialDefaultEmulatorAttributes list get from default database failed!");
            List<ImageToRomMatchAlwaysNo> ImageToRomMatchAlwaysNosSrc = null;
            if (!Get(ref ImageToRomMatchAlwaysNosSrc))
            {
                // If none available, lets add some
                ImageToRomMatchAlwaysNosSrc = new List<ImageToRomMatchAlwaysNo>();
                int fakeImgChecksum = 0;
                foreach (Rom r in RomsSrc)
                {
                    ImageToRomMatchAlwaysNosSrc.Add(new ImageToRomMatchAlwaysNo(r.Checksum, $"FakeImageChecksum_xxxxxxxxxxxxxxxxxxxxxx_{++fakeImgChecksum}"));
                }
            }
            List<GameSystem> GameSystemsSrc = null;
            if (!Get(ref GameSystemsSrc))
                return ErrorMsg("GameSystem list get from default database failed!");
            List<GameDetails> GameDetailssSrc = null;
            if (!Get(ref GameDetailssSrc, MAX_TEST_ITEMS))
                return ErrorMsg("GameDetails list get from default database failed!");
            List<AlternateNames> AlternateNamessSrc = null;
            using (connection_AlternateNames = ConnectToAlternateNames())
            {
                if (!Get(ref AlternateNamessSrc, MAX_TEST_ITEMS))
                 return ErrorMsg("AlternateNames list get from default database failed!");
            }
            connection_AlternateNames = null;
            #endregion /////////////////////////////////////////////////////////////////////////////////
            string databaseTestFileName = form_Main.GetPropertyDataPath(UNIT_TEST_DB_NAME);
            if (File.Exists(databaseTestFileName))
                File.Delete(databaseTestFileName);
            using (unit_test_conn = CreateConnection($"Filename={databaseTestFileName}"))
            {
                unit_test_conn.Open();
                #region Create table test
                if (!Update(Form_Main.SQL_CREATETABLES, unit_test_conn))
                    return ErrorMsg("Failed to create main GameLauncher database table in unit testing!!!");
                if (!Update(Form_Main.SQL_GAMEDETAILS_DB_CREATETABLES, unit_test_conn))
                    return ErrorMsg("Failed to create GameDetails database table in unit testing!!");
                if (!Update(Form_Main.SQL_ALTERNATENAMES_DB_CREATETABLES, unit_test_conn))
                    return ErrorMsg("Failed to create AlternateNames database table in unit testing!!");
                #endregion
                #region Add data to test database test
                foreach (Rom item in RomsSrc)
                    Update(item, unit_test_conn);
                foreach (GameImage item in GameImagesSrc)
                    Update(item, unit_test_conn);
                foreach (Roms_UserChanges item in Roms_UserChangessSrc)
                    Update(item, unit_test_conn);
                foreach (Mru item in MrusSrc)
                    Update(item, unit_test_conn);
                foreach (InitialDefaultEmulatorAttributes item in InitialDefaultEmulatorAttributessSrc)
                    Update(item, unit_test_conn);
                foreach (ImageToRomMatchAlwaysNo item in ImageToRomMatchAlwaysNosSrc)
                    Update(item, unit_test_conn);
                foreach (GameSystem item in GameSystemsSrc)
                    Update(item, unit_test_conn);
                foreach (GameDetails item in GameDetailssSrc)
                    Update(item, unit_test_conn);
                foreach (AlternateNames item in AlternateNamessSrc)
                    Update(item, unit_test_conn);
                #endregion
                UnitTestFailCount = 0;
                #region List test
                List<Rom> Roms = null;
                if (!Get(ref Roms, "", "", unit_test_conn))
                    return ErrorMsg("Rom list get failed!");

                List<GameImage> GameImages = null;
                if (!Get(ref GameImages, "", unit_test_conn))
                    return ErrorMsg("GameImage list get failed!");

                List<Roms_UserChanges> Roms_UserChangess = null;
                if (!Get(ref Roms_UserChangess, "", unit_test_conn))
                    return ErrorMsg("Roms_UserChanges list get failed!");

                List<Mru> Mrus = null;
                if (!Get(ref Mrus, "", unit_test_conn))
                    return ErrorMsg("Mru list get failed!");

                List<InitialDefaultEmulatorAttributes> InitialDefaultEmulatorAttributess = null;
                if (!Get(ref InitialDefaultEmulatorAttributess, "", unit_test_conn))
                    return ErrorMsg("InitialDefaultEmulatorAttributes list get failed!");

                List<ImageToRomMatchAlwaysNo> ImageToRomMatchAlwaysNos = null;
                if (!Get(ref ImageToRomMatchAlwaysNos, "", unit_test_conn))
                    return ErrorMsg("ImageToRomMatchAlwaysNo list get failed!");

                List<GameSystem> GameSystems = null;
                if (!Get(ref GameSystems, "", unit_test_conn))
                    return ErrorMsg("GameSystem list get failed!");

                List<GameDetails> GameDetailss = null;
                if (!Get(ref GameDetailss, "", unit_test_conn))
                    return ErrorMsg("GameDetails list get failed!");

                List<AlternateNames> AlternateNamess = null;
                if (!Get(ref AlternateNamess, "", unit_test_conn))
                    return ErrorMsg("AlternateNames list get failed!");
                #endregion /////////////////////////////////////////////////////////////////////////////////
                #region Single item get test
                Rom rom = null;
                if (!Get(ref rom, RomsSrc[0].FilePath, RomsSrc[0].Title, unit_test_conn))
                    return ErrorMsg("Rom item get failed!");
                if (rom.FilePath != RomsSrc[0].FilePath)
                    return ErrorMsg($"Rom item get failed to get correct item. Expected '{RomsSrc[0].FilePath}' but received '{rom.FilePath}'!");
                if (!Get(ref rom, RomsSrc[1].FilePath, "", unit_test_conn))
                    return ErrorMsg("Rom item get failed!");
                if (rom.FilePath != RomsSrc[1].FilePath)
                    return ErrorMsg($"Rom item get failed to get correct item. Expected '{RomsSrc[1].FilePath}' but received '{rom.FilePath}'!");

                GameImage gameImage = null;
                if (!Get(ref gameImage, GameImagesSrc[0].FilePath, unit_test_conn))
                    return ErrorMsg("GameImage item get failed!");
                if (gameImage.FilePath != GameImagesSrc[0].FilePath)
                    return ErrorMsg($"GameImage item get failed to get correct item. Expected '{GameImagesSrc[0].FilePath}' but received '{gameImage.FilePath}'!");

                Roms_UserChanges roms_UserChanges = null;
                if (!Get(ref roms_UserChanges, Roms_UserChangessSrc[0].FilePath, unit_test_conn))
                    return ErrorMsg("Roms_UserChanges item get failed!");
                if (roms_UserChanges.FilePath != Roms_UserChangessSrc[0].FilePath)
                    return ErrorMsg($"Roms_UserChanges item get failed to get correct item. Expected '{Roms_UserChangessSrc[0].FilePath}' but received '{roms_UserChanges.FilePath}'!");

                Mru mru = null;
                if (!Get(ref mru, MrusSrc[0].FilePath, unit_test_conn))
                    return ErrorMsg("Mru item get failed!");
                if (mru.FilePath != MrusSrc[0].FilePath)
                    return ErrorMsg($"Mru item get failed to get correct item. Expected '{MrusSrc[0].FilePath}' but received '{mru.FilePath}'!");

                InitialDefaultEmulatorAttributes initialDefaultEmulatorAttributes = null;
                if (!Get(ref initialDefaultEmulatorAttributes, InitialDefaultEmulatorAttributessSrc[0].EmulatorExecutable, unit_test_conn))
                    return ErrorMsg("InitialDefaultEmulatorAttributes item get failed!");
                if (initialDefaultEmulatorAttributes.EmulatorExecutable != InitialDefaultEmulatorAttributessSrc[0].EmulatorExecutable)
                    return ErrorMsg($"InitialDefaultEmulatorAttributes item get failed to get correct item. Expected '{InitialDefaultEmulatorAttributessSrc[0].EmulatorExecutable}' but received '{initialDefaultEmulatorAttributes.EmulatorExecutable}'!");

                ImageToRomMatchAlwaysNo imageToRomMatchAlwaysNo = null;
                if (!Get(ref imageToRomMatchAlwaysNo, ImageToRomMatchAlwaysNosSrc[0].RomChecksum, ImageToRomMatchAlwaysNosSrc[0].ImageChecksum, unit_test_conn))
                    return ErrorMsg("ImageToRomMatchAlwaysNo item get failed!");
                if (imageToRomMatchAlwaysNo.RomChecksum != ImageToRomMatchAlwaysNosSrc[0].RomChecksum)
                    return ErrorMsg($"ImageToRomMatchAlwaysNo item get failed to get correct item. Expected '{ImageToRomMatchAlwaysNosSrc[0].RomChecksum}' but received '{imageToRomMatchAlwaysNo.RomChecksum}'!");

                GameSystem gameSystem = null;
                if (!Get(ref gameSystem, GameSystemsSrc[0].ID, unit_test_conn))
                    return ErrorMsg("GameSystem item get failed!");
                if (gameSystem.ID != GameSystemsSrc[0].ID)
                    return ErrorMsg($"GameSystem item get failed to get correct item. Expected '{GameSystemsSrc[0].ID}' but received '{gameSystem.ID}'!");

                GameDetails gameDetails = null;
                if (!Get(ref gameDetails, GameDetailssSrc[0].Title, GameDetailssSrc[0].System, unit_test_conn))
                    return ErrorMsg("GameDetails item get failed!");
                if (gameDetails.Title != GameDetailssSrc[0].Title)
                    return ErrorMsg($"GameDetails item get failed to get correct item. Expected '{GameDetailssSrc[0].Title}' but received '{gameDetails.Title}'!");

                AlternateNames alternateNames = null;
                if (!Get(ref alternateNames, AlternateNamessSrc[0].DefaultName, AlternateNamessSrc[0].Alternate, unit_test_conn))
                    return ErrorMsg("AlternateNames item get failed!");
                if (alternateNames.DefaultName != AlternateNamessSrc[0].DefaultName)
                    return ErrorMsg($"AlternateNames item get failed to get correct item. Expected '{AlternateNamessSrc[0].DefaultName}' but received '{alternateNames.DefaultName}'!");
                #endregion
                #region Delete item test
                if (!Delete(rom, unit_test_conn))
                    return ErrorMsg("Rom delete item failed!");

                if (!Delete(gameImage, unit_test_conn))
                    return ErrorMsg("gameImage delete item failed!");

                if (!Delete(roms_UserChanges, unit_test_conn))
                    return ErrorMsg("roms_UserChanges delete item failed!");

                if (!Delete(mru, unit_test_conn))
                    return ErrorMsg("mru delete item failed!");

                if (!Delete(initialDefaultEmulatorAttributes, unit_test_conn))
                    return ErrorMsg("initialDefaultEmulatorAttributes delete item failed!");

                if (!Delete(imageToRomMatchAlwaysNo, unit_test_conn))
                    return ErrorMsg("imageToRomMatchAlwaysNo delete item failed!");

                if (!Delete(gameSystem, unit_test_conn))
                    return ErrorMsg("gameSystem delete item failed!");

                if (!Delete(gameDetails, unit_test_conn))
                    return ErrorMsg("gameDetails delete item failed!");

                if (!Delete(alternateNames, unit_test_conn))
                    return ErrorMsg("alternateNames delete item failed!");
                #endregion ///////////////////////////////////////////////////////////////////////////////
            }
            unit_test_conn = null;
            return true;
        }
        #endregion
        #region Get Single item
        public bool Get(ref Rom item, string FilePath, string Title = "", Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            string where = $" WHERE FilePath LIKE \"{FilePath}\" and Title LIKE \"{Title}%\"";
            List<Rom> list = null;
            if (!Get(ref list, where, "", conn))
                return false;
            item = list[0];
            return true;
        }
        public bool Get(ref GameImage item, string FilePath, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            string where = $" WHERE FilePath LIKE \"{FilePath}\"";
            List<GameImage> list = null;
            if (!Get(ref list, where, conn))
                return false;
            item = list[0];
            return true;
        }
        public bool Get(ref Roms_UserChanges item, string FilePath, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            string where = $" WHERE FilePath LIKE \"{FilePath}\"";
            List<Roms_UserChanges> list = null;
            if (!Get(ref list, where, conn))
                return false;
            item = list[0];
            return true;
        }
        public bool Get(ref Mru item, string FilePath, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            string where = $" WHERE FilePath LIKE \"{FilePath}\"";
            List<Mru> list = null;
            if (!Get(ref list, where, conn))
                return false;
            item = list[0];
            return true;
        }
        public bool Get(ref InitialDefaultEmulatorAttributes item, string EmulatorExecutable, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            string where = $" WHERE EmulatorExecutable LIKE \"{EmulatorExecutable}\"";
            List<InitialDefaultEmulatorAttributes> list = null;
            if (!Get(ref list, where, conn))
                return false;
            item = list[0];
            return true;
        }
        public bool Get(ref ImageToRomMatchAlwaysNo item, string RomChecksum, string ImageChecksum, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            string where = $" WHERE RomChecksum LIKE \"{RomChecksum}\" and ImageChecksum LIKE \"{ImageChecksum}\"";
            List<ImageToRomMatchAlwaysNo> list = null;
            if (!Get(ref list, where, conn))
                return false;
            item = list[0];
            return true;
        }
        public bool Get(ref GameSystem item, int ID, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            string where = $" WHERE ID LIKE \"{ID}\"";
            List<GameSystem> list = null;
            if (!Get(ref list, where, conn))
                return false;
            item = list[0];
            return true;
        }
        public bool Get(ref GameSystem item, string ID, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            string where = $" WHERE ID LIKE \"{ID}\"";
            List<GameSystem> list = null;
            if (!Get(ref list, where, conn))
                return false;
            item = list[0];
            return true;
        }
        public bool Get(ref GameDetails item, string Title, string System = "", Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (conn == null)
                conn = unit_test_conn == null ? Form_Main.connection_GameDetails : unit_test_conn;
            string where = $" WHERE Title LIKE \"{Title}\" and System LIKE \"{System}%\" ";
            List<GameDetails> list = null;
            if (!Get(ref list, where, conn))
                return false;
            item = list[0];
            return true;
        }
        public bool Get(ref AlternateNames item, string DefaultName, string Alternate = "", Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (conn == null)
                conn = unit_test_conn == null ? connection_AlternateNames : unit_test_conn;
            string where = $" WHERE DefaultName LIKE \"{DefaultName}\" and Alternate LIKE \"{Alternate}%\"";
            List<AlternateNames> list = null;
            if (!Get(ref list, where, conn))
                return false;
            item = list[0];
            return true;
        }
        #endregion /////////////////////////////////////////////////////////////////////////////////
        #region Get that returns type
        public Rom Get_Rom(string FilePath, string Title = "", Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            Rom item = null;
            return !Get(ref item, FilePath, Title, conn) ? null : item;
        }
        public GameImage Get_GameImage(string FilePath, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            GameImage item = null;
            return !Get(ref item, FilePath, conn) ? null : item;
        }
        public Roms_UserChanges Get_Roms_UserChanges(string FilePath, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            Roms_UserChanges item = null;
            return !Get(ref item, FilePath, conn) ? null : item;
        }
        public Mru Get_Mru(string FilePath, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            Mru item = null;
            return !Get(ref item, FilePath, conn) ? null : item;
        }
        public InitialDefaultEmulatorAttributes Get_InitialDefaultEmulatorAttributes(string EmulatorExecutable, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            InitialDefaultEmulatorAttributes item = null;
            return !Get(ref item, EmulatorExecutable, conn) ? null : item;
        }
        public ImageToRomMatchAlwaysNo Get_ImageToRomMatchAlwaysNo(string RomChecksum, string ImageChecksum, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            ImageToRomMatchAlwaysNo item = null;
            return !Get(ref item, RomChecksum, ImageChecksum, conn) ? null : item;
        }
        public GameSystem Get_GameSystem(string ID, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            GameSystem item = null;
            return !Get(ref item, ID, conn) ? null : item;
        }
        public GameDetails Get_GameDetails(string Title, string System = "", Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (conn == null)
                conn = unit_test_conn == null ? Form_Main.connection_GameDetails : unit_test_conn;
            GameDetails item = null;
            return !Get(ref item, Title, System, conn) ? null : item;
        }
        public AlternateNames Get_AlternateNames(string DefaultName, string Alternate = "", Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (conn == null)
                conn = unit_test_conn == null ? connection_AlternateNames : unit_test_conn;
            AlternateNames item = null;
            return !Get(ref item, DefaultName, Alternate, conn) ? null : item;
        }
        public EmulatorAttributes Get_EmulatorAttributes(string filePath, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            InitialDefaultEmulatorAttributes item = null;
            return !WhereGet(ref item, $" WHERE EmulatorExecutable LIKE \"{filePath}\"", conn)
                ? null
                : new EmulatorAttributes(item.EmulatorExecutable, item.DecompressFile != 0, item.NotSupported != 0, item.PreferredExtension);
        }
        #endregion /////////////////////////////////////////////////////////////////////////////////
        #region Where Get Item
        public bool WhereGet(ref Rom item, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            List<Rom> items = null;
            if (!Get(ref items, where, "", conn))
                return false;
            item = items[0];
            return true;
        }
        public bool WhereGet(ref GameImage item, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            List<GameImage> items = null;
            if (!Get(ref items, where, conn))
                return false;
            item = items[0];
            return true;
        }
        public bool WhereGet(ref Roms_UserChanges item, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            List<Roms_UserChanges> items = null;
            if (!Get(ref items, where, conn))
                return false;
            item = items[0];
            return true;
        }
        public bool WhereGet(ref Mru item, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            List<Mru> items = null;
            if (!Get(ref items, where, conn))
                return false;
            item = items[0];
            return true;
        }
        public bool WhereGet(ref InitialDefaultEmulatorAttributes item, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            List<InitialDefaultEmulatorAttributes> items = null;
            if (!Get(ref items, where, conn))
                return false;
            item = items[0];
            return true;
        }
        public bool WhereGet(ref ImageToRomMatchAlwaysNo item, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            List<ImageToRomMatchAlwaysNo> items = null;
            if (!Get(ref items, where, conn))
                return false;
            item = items[0];
            return true;
        }
        public bool WhereGet(ref GameSystem item, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            List<GameSystem> items = null;
            if (!Get(ref items, where, conn))
                return false;
            item = items[0];
            return true;
        }
        public bool WhereGet(ref GameDetails item, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (conn == null)
                conn = unit_test_conn == null ? Form_Main.connection_GameDetails : unit_test_conn;
            List<GameDetails> items = null;
            if (!Get(ref items, where, conn))
                return false;
            item = items[0];
            return true;
        }
        public bool WhereGet(ref AlternateNames item, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (conn == null)
                conn = unit_test_conn == null ? connection_AlternateNames : unit_test_conn;
            List<AlternateNames> items = null;
            if (!Get(ref items, where, conn))
                return false;
            item = items[0];
            return true;
        }
        #endregion /////////////////////////////////////////////////////////////////////////////////
        #region Get List
        public bool Get(string SystemName, ref List<Rom> items)
        {
            int SystemID = form_Main.GetSystemIndex(SystemName);
            return Get(SystemID, ref items);
        }
        public bool Get(int SystemID, ref List<Rom> items, string titleSearch = "", string Where = "", string Limit = "", Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            string where = $"WHERE System = \"{SystemID}\" ";
            if (SystemID < 0)
            {
                where = titleSearch.Length > 0 ? $"WHERE Title like \"{titleSearch}\"" : "";
            }
            if (Where.Length > 0)
                where = Where;
            return Get(ref items, where, Limit, conn);
        }
        public bool Get(string sql, ref List<Rom> items, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, "", "", conn, sql);
        public bool Get(ref List<Rom> items, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, "", "", conn);
        public bool Get(ref List<Rom> items, int Limit, int offset = 0, string where = "", Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, where, $"limit {offset} , {Limit} ", conn);
        public bool Get(ref List<Rom> items, string where, string Limit = "", Microsoft.Data.Sqlite.SqliteConnection conn = null, string sql = null)
        {
            items = new List<Rom>();
            if (sql == null)
                sql = $"SELECT * FROM Roms {where} ORDER BY NameSimplified {Limit}";
            using (SqlReader reader = new SqlReader(sql, conn))
            {
                while (reader.Read())
                {
                    string NameSimplified = reader.GetString("NameSimplified");
                    string NameOrg = reader.GetString("NameOrg");
                    int System = reader.GetInt("System");
                    string FilePath = reader.GetString("FilePath");
                    int PreferredEmulator = reader.GetInt("PreferredEmulator");
                    string ImagePath = reader.GetString("ImagePath", form_Main.defaultImagePath);
                    int QtyPlayers = reader.GetInt("QtyPlayers");
                    string Status = reader.GetString("Status");
                    string Region = reader.GetString("Region");
                    string Developer = reader.GetString("Developer");
                    string ReleaseDate = reader.GetString("ReleaseDate");
                    int RomSize = reader.GetInt("RomSize");
                    string Genre = reader.GetString("Genre");
                    string NotesCore = reader.GetString("NotesCore");
                    string NotesUser = reader.GetString("NotesUser");
                    string FileFormat = reader.GetString("FileFormat");
                    string Version = reader.GetString("Version");
                    string Description = reader.GetString("Description");
                    string Language = reader.GetString("Language");
                    string Title = reader.GetString("Title");
                    string Compressed = reader.GetString("Compressed");
                    string Checksum = reader.GetString("Checksum");
                    string CompressChecksum = reader.GetString("CompressChecksum");
                    int Year = reader.GetInt("Year");
                    string Rating = reader.GetString("Rating");
                    string Publisher = reader.GetString("Publisher");
                    string WikipediaURL = reader.GetString("WikipediaURL");
                    float StarRating = reader.GetFloat("StarRating");
                    int StarRatingVoteCount = reader.GetInt("StarRatingVoteCount");
                    bool Favorite = reader.GetBool("Favorite");
                    bool Disable = reader.GetBool("Disable");
                    int PlayCount = reader.GetInt("PlayCount");
                    bool Hide = reader.GetBool("Hide");

                    items.Add(new Rom(NameSimplified, System, FilePath, NameOrg, Title, Compressed,
                        PreferredEmulator, ImagePath, QtyPlayers, Region,
                        Developer, RomSize, Genre, NotesCore, NotesUser,
                        FileFormat, ReleaseDate, Status, Version,
                        Description, Language, Year, Rating, Checksum, CompressChecksum, Publisher, WikipediaURL,
                        StarRating, StarRatingVoteCount, PlayCount, Favorite, Disable, Hide));
                }
            }
            return items.Count > 0;
        }
        public bool Get(string sql, ref List<GameImage> items, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, "", "", conn, sql);
        public bool Get(ref List<GameImage> items, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, "", "", conn);
        public bool Get(ref List<GameImage> items, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, where, "", conn);
        public bool Get(ref List<GameImage> items, int Limit, int offset = 0, string where = "", Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, where, $"limit {offset} , {Limit} ", conn);
        public bool Get(ref List<GameImage> items, string where, string Limit, Microsoft.Data.Sqlite.SqliteConnection conn = null, string sql = null)
        {
            items = new List<GameImage>();
            if (sql == null) 
                sql = $"SELECT * FROM Images {where} ORDER BY NameSimplified  {Limit}";
            using (SqlReader reader = new SqlReader(sql, conn))
            {
                while (reader.Read())
                {
                    string Title = reader.GetString("Title");
                    string NameSimplified = reader.GetString("NameSimplified");
                    string NameOrg = reader.GetString("NameOrg");
                    string Compressed = reader.GetString("Compressed");
                    string FilePath = reader.GetString("FilePath");
                    string Checksum = reader.GetString("Checksum");
                    items.Add(new GameImage(Title, NameSimplified, FilePath, NameOrg, Compressed, Checksum));
                }
            }
            return items.Count > 0;
        }
        public bool Get(ref List<Roms_UserChanges> items, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, "", "", conn);
        public bool Get(ref List<Roms_UserChanges> items, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, where, "", conn);
        public bool Get(ref List<Roms_UserChanges> items, int Limit, int offset = 0, string where = "", Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, where, $"limit {offset} , {Limit} ", conn);
        public bool Get(ref List<Roms_UserChanges> items, string where, string Limit, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            items = new List<Roms_UserChanges>();
            using (SqlReader reader = new SqlReader($"SELECT * FROM Roms_UserChanges {where} ORDER BY FilePath {Limit}", conn))
            {
                while (reader.Read())
                {
                    string FilePath = reader.GetString("FilePath");
                    string Title = reader.GetString("Title");
                    int PreferredEmulator = reader.GetInt("PreferredEmulator");
                    string ImagePath = reader.GetString("ImagePath", form_Main.defaultImagePath);
                    int QtyPlayers = reader.GetInt("QtyPlayers");
                    string Status = reader.GetString("Status");
                    string Region = reader.GetString("Region");
                    string Developer = reader.GetString("Developer");
                    string ReleaseDate = reader.GetString("ReleaseDate");
                    string Genre = reader.GetString("Genre");
                    string NotesCore = reader.GetString("NotesCore");
                    string NotesUser = reader.GetString("NotesUser");
                    string Version = reader.GetString("Version");
                    string Description = reader.GetString("Description");
                    string Language = reader.GetString("Language");
                    int Year = reader.GetInt("Year");
                    string Rating = reader.GetString("Rating");
                    string Publisher = reader.GetString("Publisher");
                    string WikipediaURL = reader.GetString("WikipediaURL");
                    int PlayCount = reader.GetInt("PlayCount");
                    bool Favorite = reader.GetBool("Favorite");
                    bool Disable = reader.GetBool("Disable");
                    bool Hide = reader.GetBool("Hide");

                    items.Add(new Roms_UserChanges(FilePath, Title, PreferredEmulator, ImagePath, QtyPlayers, Status, Region,
                        Developer, ReleaseDate, Genre, NotesCore, NotesUser, Version, Description, Language, Year, Rating, Publisher, WikipediaURL,
                        PlayCount, Favorite, Disable, Hide));
                }
            }
            return items.Count > 0;
        }
        public bool Get(ref List<Mru> items, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, "", "", conn);
        public bool Get(ref List<Mru> items, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, where, "", conn);
        public bool Get(ref List<Mru> items, int Limit, int offset = 0, string where = "", Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, where, $"limit {offset} , {Limit} ", conn);
        public bool Get(ref List<Mru> items, string where, string Limit, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            items = new List<Mru>();
            using (SqlReader reader = new SqlReader($"SELECT * FROM MRU {where} ORDER BY DateLastUsed DESC {Limit}", conn))
            {
                while (reader.Read())
                {
                    string FilePath = reader.GetString("FilePath");
                    string DateLastUsed = reader.GetString("DateLastUsed");
                    items.Add(new Mru(FilePath, DateLastUsed));
                }
            }
            return items.Count > 0;
        }
        public bool Get(ref List<InitialDefaultEmulatorAttributes> items, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, "", conn);
        public bool Get(ref List<InitialDefaultEmulatorAttributes> items, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            items = new List<InitialDefaultEmulatorAttributes>();
            using (SqlReader reader = new SqlReader($"SELECT * FROM InitialDefaultEmulatorAttributes {where} ORDER BY EmulatorExecutable DESC", conn))
            {
                while (reader.Read())
                {
                    string EmulatorExecutable = reader.GetString("EmulatorExecutable");
                    int DecompressFile = reader.GetInt("DecompressFile");
                    int NotSupported = reader.GetInt("NotSupported");
                    string PreferredExtension = reader.GetString("PreferredExtension");
                    items.Add(new InitialDefaultEmulatorAttributes(EmulatorExecutable, DecompressFile, NotSupported, PreferredExtension));
                }
            }
            return items.Count > 0;
        }
        public bool Get(ref List<ImageToRomMatchAlwaysNo> items, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, "", conn);
        public bool Get(ref List<ImageToRomMatchAlwaysNo> items, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            items = new List<ImageToRomMatchAlwaysNo>();
            using (SqlReader reader = new SqlReader($"SELECT * FROM ImageToRomMatchAlwaysNo {where} ORDER BY RomChecksum DESC", conn))
            {
                while (reader.Read())
                {
                    string RomChecksum = reader.GetString("RomChecksum");
                    string ImageChecksum = reader.GetString("ImageChecksum");
                    items.Add(new ImageToRomMatchAlwaysNo(RomChecksum, ImageChecksum));
                }
            }
            return items.Count > 0;
        }
        public bool Get(ref List<GameSystem> items, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, "", "", conn);
        public bool Get(ref List<GameSystem> items, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, where, "", conn);
        public bool Get(ref List<GameSystem> items, int Limit, int offset = 0, string where = "", Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, where, $"limit {offset} , {Limit} ", conn);
        public bool Get(ref List<GameSystem> items, string where, string Limit, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            items = new List<GameSystem>();
            using (SqlReader reader = new SqlReader($"SELECT * FROM GameSystems {where} {Limit}", conn))
            {
                while (reader.Read())
                {
                    string Name = reader.GetString("Name");
                    string ImageDirPath = reader.GetString("ImageDirPath");
                    string RomDirPath = reader.GetString("RomDirPath");
                    int ID = reader.GetInt("ID");
                    int PlayCount = reader.GetInt("PlayCount");
                    bool Initiated = reader.GetBool("Initiated");
                    bool Hide = reader.GetBool("Hide");
                    EmulatorExecutables emulatorExecutables = new EmulatorExecutables();
                    emulatorExecutables.EmulatorPaths[0] = reader.GetString("EmulatorPath1");
                    emulatorExecutables.EmulatorPaths[1] = reader.GetString("EmulatorPath2");
                    emulatorExecutables.EmulatorPaths[2] = reader.GetString("EmulatorPath3");
                    emulatorExecutables.EmulatorPaths[3] = reader.GetString("EmulatorPath4");
                    emulatorExecutables.EmulatorPaths[4] = reader.GetString("EmulatorPath5");
                    emulatorExecutables.EmulatorPaths[5] = reader.GetString("EmulatorPath6");
                    emulatorExecutables.EmulatorPaths[6] = reader.GetString("EmulatorPath7");
                    emulatorExecutables.EmulatorPaths[7] = reader.GetString("EmulatorPath8");
                    emulatorExecutables.EmulatorPaths[8] = reader.GetString("EmulatorPath9");
                    emulatorExecutables.EmulatorPaths[9] = reader.GetString("EmulatorPath10");

                    emulatorExecutables.DecompressFile[0] = reader.GetBool("DecompressFile1");
                    emulatorExecutables.DecompressFile[1] = reader.GetBool("DecompressFile2");
                    emulatorExecutables.DecompressFile[2] = reader.GetBool("DecompressFile3");
                    emulatorExecutables.DecompressFile[3] = reader.GetBool("DecompressFile4");
                    emulatorExecutables.DecompressFile[4] = reader.GetBool("DecompressFile5");
                    emulatorExecutables.DecompressFile[5] = reader.GetBool("DecompressFile6");
                    emulatorExecutables.DecompressFile[6] = reader.GetBool("DecompressFile7");
                    emulatorExecutables.DecompressFile[7] = reader.GetBool("DecompressFile8");
                    emulatorExecutables.DecompressFile[8] = reader.GetBool("DecompressFile9");
                    emulatorExecutables.DecompressFile[9] = reader.GetBool("DecompressFile10");

                    emulatorExecutables.NotSupported[0] = reader.GetBool("NotSupported1");
                    emulatorExecutables.NotSupported[1] = reader.GetBool("NotSupported2");
                    emulatorExecutables.NotSupported[2] = reader.GetBool("NotSupported3");
                    emulatorExecutables.NotSupported[3] = reader.GetBool("NotSupported4");
                    emulatorExecutables.NotSupported[4] = reader.GetBool("NotSupported5");
                    emulatorExecutables.NotSupported[5] = reader.GetBool("NotSupported6");
                    emulatorExecutables.NotSupported[6] = reader.GetBool("NotSupported7");
                    emulatorExecutables.NotSupported[7] = reader.GetBool("NotSupported8");
                    emulatorExecutables.NotSupported[8] = reader.GetBool("NotSupported9");
                    emulatorExecutables.NotSupported[9] = reader.GetBool("NotSupported10");

                    emulatorExecutables.PreferredExtension[0] = reader.GetString("PreferredExtension1");
                    emulatorExecutables.PreferredExtension[1] = reader.GetString("PreferredExtension2");
                    emulatorExecutables.PreferredExtension[2] = reader.GetString("PreferredExtension3");
                    emulatorExecutables.PreferredExtension[3] = reader.GetString("PreferredExtension4");
                    emulatorExecutables.PreferredExtension[4] = reader.GetString("PreferredExtension5");
                    emulatorExecutables.PreferredExtension[5] = reader.GetString("PreferredExtension6");
                    emulatorExecutables.PreferredExtension[6] = reader.GetString("PreferredExtension7");
                    emulatorExecutables.PreferredExtension[7] = reader.GetString("PreferredExtension8");
                    emulatorExecutables.PreferredExtension[8] = reader.GetString("PreferredExtension9");
                    emulatorExecutables.PreferredExtension[9] = reader.GetString("PreferredExtension10");

                    items.Add(new GameSystem(Name, RomDirPath, ImageDirPath, emulatorExecutables, ID, PlayCount, Hide, Initiated));
                }
            }
            return items.Count > 0;
        }
        public bool Get(ref List<GameDetails> items, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, "", "", conn);
        public bool Get(ref List<GameDetails> items, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, where, "", conn);
        public bool Get(ref List<GameDetails> items, int Limit, int offset = 0, string where = "", Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, where, $"limit {offset} , {Limit} ", conn);
        public bool Get(ref List<GameDetails> items, string where, string Limit, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (conn == null)
                conn = unit_test_conn == null ? Form_Main.connection_GameDetails : unit_test_conn;
            items = new List<GameDetails>();
            using (SqlReader reader = new SqlReader($"SELECT * FROM GameDetails {where} ORDER BY System  {Limit}", conn))
            {
                while (reader.Read())
                {
                    string System = reader.GetString("System");
                    string Title = reader.GetString("Title");
                    string NameSimplified = reader.GetString("NameSimplified");
                    string Compressed = reader.GetString("Compressed");
                    int QtyPlayers = reader.GetInt("QtyPlayers");
                    int Year = reader.GetInt("Year");
                    string Status = reader.GetString("Status");
                    string ImageFileName = reader.GetString("ImageFileName");
                    string Region = reader.GetString("Region");
                    string Developer = reader.GetString("Developer");
                    string ReleaseDate = reader.GetString("ReleaseDate");
                    string Genre = reader.GetString("Genre");
                    string NotesCore = reader.GetString("NotesCore");
                    string NotesUser = reader.GetString("NotesUser");
                    string FileFormat = reader.GetString("FileFormat");
                    string Version = reader.GetString("Version");
                    string Description = reader.GetString("Description");
                    string Language = reader.GetString("Language");
                    string Rating = reader.GetString("Rating");
                    string Publisher = reader.GetString("Publisher");
                    float StarRating = reader.GetFloat("StarRating");
                    int StarRatingVoteCount = reader.GetInt("StarRatingVoteCount");
                    string FileName = reader.GetString("FileName");
                    string WikipediaURL = reader.GetString("WikipediaURL");
                    string ID = reader.GetString("ID");
                    string PlayMode = reader.GetString("PlayMode");

                    items.Add(new GameDetails(System,
                        Title,
                        NameSimplified,
                        Compressed,
                        QtyPlayers,
                        Developer,
                        ReleaseDate,
                        Year,
                        Genre,
                        Status,
                        ImageFileName,
                        Region,
                        NotesCore,
                        NotesUser,
                        FileFormat,
                        Version,
                        Description,
                        Language,
                        Rating,
                        Publisher,
                        FileName,
                        StarRating,
                        StarRatingVoteCount,
                        WikipediaURL,
                        ID,
                        PlayMode));
                }
            }
            return items.Count > 0;
        }
        public bool Get(ref List<AlternateNames> items, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, "", "", conn);
        public bool Get(ref List<AlternateNames> items, string where, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, where, "", conn);
        public bool Get(ref List<AlternateNames> items, int Limit, int offset = 0, string where = "", Microsoft.Data.Sqlite.SqliteConnection conn = null) => Get(ref items, where, $"limit {offset} , {Limit} ", conn);
        public bool Get(ref List<AlternateNames> items, string where, string Limit, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (conn == null)
                conn = unit_test_conn == null ? connection_AlternateNames : unit_test_conn;
            items = new List<AlternateNames>();
            using (SqlReader reader = new SqlReader($"SELECT * FROM AlternateNames {where} ORDER BY DefaultName  {Limit}", conn))
            {
                while (reader.Read())
                {
                    string DefaultName = reader.GetString("DefaultName");
                    string Alternate = reader.GetString("Alternate");
                    string Title = reader.GetString("Title");
                    string NameSimplified = reader.GetString("NameSimplified");
                    string Compressed = reader.GetString("Compressed");

                    items.Add(new AlternateNames(DefaultName, Alternate, Title, NameSimplified, Compressed));
                }
            }
            return items.Count > 0;
        }
        #endregion /////////////////////////////////////////////////////////////////////////////////
        #region Update item
        public bool Update(Rom rom, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Update(rom, false, false, conn);
        public bool Update(Rom rom, bool resetNames, bool update_Roms_UserChanges = false, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (rom == null)
                return false;
            if (resetNames)
            {
                rom.NameOrg = Form_Main.GetFileNameWithoutExtensionAndWithoutBin(rom.FilePath);
                rom.NameSimplified = form_Main.ConvertToNameSimplified(rom.NameOrg);
                rom.Compressed = form_Main.ConvertToCompress(rom.NameOrg);
                rom.Title = form_Main.ConvertToTitle(rom.NameOrg);
            }
            string sql = $"INSERT OR REPLACE INTO Roms " +
                $"(NameSimplified, System, FilePath, NameOrg, Title, Compressed, ImagePath, Region, Language, Status, Version, NotesCore, RomSize, " +
                $"PreferredEmulator, QtyPlayers, Developer, ReleaseDate, Genre, NotesUser, FileFormat, Description, Checksum, CompressChecksum, Rating, Year," +
                $"Publisher, WikipediaURL, StarRating, StarRatingVoteCount, PlayCount, Hide, Favorite, Disable) VALUES" +
            $" (\"{rom.NameSimplified}\", {rom.System}, \"{rom.FilePath}\", \"{rom.NameOrg}\", \"{rom.Title}\", \"{rom.Compressed}\", \"{rom.ImagePath}\", \"{rom.Region}\", \"{rom.Language}\", \"{rom.Status}\", \"{rom.Version}\", \"{rom.NotesCore}\", {rom.RomSize}, " +
            $"{rom.PreferredEmulatorID}, {rom.QtyPlayers}, \"{rom.Developer}\", \"{rom.ReleaseDate}\", \"{rom.Genre}\", \"{rom.NotesUser}\", \"{rom.FileFormat}\", \"{rom.Description}\", \"{rom.Checksum}\", \"{rom.CompressChecksum}\", \"{rom.Rating}\", {rom.Year}," +
            $"\"{rom.Publisher}\", \"{rom.WikipediaURL}\", {rom.StarRating}, {rom.StarRatingVoteCount}, {rom.PlayCount}, {rom.Hide}, {rom.Favorite}, {rom.Disable} )";
            bool returnValue = Update(sql, conn);
            if (returnValue && update_Roms_UserChanges)
            {
                sql = "INSERT OR REPLACE INTO Roms_UserChanges " +
                "(Title, FilePath, PreferredEmulator, ImagePath, QtyPlayers, Status, Region, Developer, ReleaseDate, Genre, NotesCore, NotesUser, " +
                " Version, Description, Language, Year, Rating, Publisher, WikipediaURL, PlayCount, Favorite, Disable, Hide) VALUES" +
                $" (\"{rom.Title}\", \"{rom.FilePath}\", {rom.PreferredEmulatorID}, \"{rom.ImagePath}\", {rom.QtyPlayers}, \"{rom.Status}\", " +
                $"\"{rom.Region}\", \"{rom.Developer}\", \"{rom.ReleaseDate}\", \"{rom.Genre}\", \"{rom.NotesCore}\", \"{rom.NotesUser}\", " +
                $"\"{rom.Version}\", \"{rom.Description}\", \"{rom.Language}\", {rom.Year}, \"{rom.Rating}\", \"{rom.Publisher}\", " +
                $"\"{rom.WikipediaURL}\", {rom.PlayCount}, {rom.Favorite}, {rom.Disable}, {rom.Hide} )";
                return Update(sql, conn);
            }
            return returnValue;
        }
        public bool Update(Roms_UserChanges item, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (item == null)
                return false;
            string sql = "INSERT OR REPLACE INTO Roms_UserChanges " +
            "(Title, FilePath, PreferredEmulator, ImagePath, QtyPlayers, Status, Region, Developer, ReleaseDate, Genre, NotesCore, NotesUser, " +
            " Version, Description, Language, Year, Rating, Publisher, WikipediaURL, PlayCount, Favorite, Disable, Hide) VALUES" +
            $" (\"{item.Title}\", \"{item.FilePath}\", {item.PreferredEmulatorID}, \"{item.ImagePath}\", {item.QtyPlayers}, \"{item.Status}\", " +
            $"\"{item.Region}\", \"{item.Developer}\", \"{item.ReleaseDate}\", \"{item.Genre}\", \"{item.NotesCore}\", \"{item.NotesUser}\", " +
            $"\"{item.Version}\", \"{item.Description}\", \"{item.Language}\", {item.Year}, \"{item.Rating}\", \"{item.Publisher}\", " +
            $"\"{item.WikipediaURL}\", {item.PlayCount}, {item.Favorite}, {item.Disable}, {item.Hide} )";
            return Update(sql, conn);
        }
        public bool Update(GameSystem gameSystem, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (gameSystem == null)
                return false;
            string sql = "INSERT OR REPLACE INTO GameSystems (Name, ID, ImageDirPath, RomDirPath, PlayCount, Initiated, Hide " +
                ", EmulatorPath1, EmulatorPath2, EmulatorPath3, EmulatorPath4, EmulatorPath5, EmulatorPath6, EmulatorPath7, EmulatorPath8, EmulatorPath9, EmulatorPath10 " +
                ", DecompressFile1, DecompressFile2, DecompressFile3, DecompressFile4, DecompressFile5, DecompressFile6, DecompressFile7, DecompressFile8, DecompressFile9, DecompressFile10 " +
                ", NotSupported1, NotSupported2, NotSupported3, NotSupported4, NotSupported5, NotSupported6, NotSupported7, NotSupported8, NotSupported9, NotSupported10 " +
                ", PreferredExtension1, PreferredExtension2, PreferredExtension3, PreferredExtension4, PreferredExtension5, PreferredExtension6, PreferredExtension7, PreferredExtension8, PreferredExtension9, PreferredExtension10 " +
                ") VALUES" +
            $" (\"{gameSystem.Name}\", {gameSystem.ID}, \"{gameSystem.ImageDirPath}\", \"{gameSystem.RomDirPath}\", {gameSystem.PlayCount}, {gameSystem.Initiated}, {gameSystem.Hide} " +
            $", \"{gameSystem.EmulatorPaths[0]}\", \"{gameSystem.EmulatorPaths[1]}\", \"{gameSystem.EmulatorPaths[2]}\", \"{gameSystem.EmulatorPaths[3]}\", \"{gameSystem.EmulatorPaths[4]}\" " +
            $", \"{gameSystem.EmulatorPaths[5]}\", \"{gameSystem.EmulatorPaths[6]}\", \"{gameSystem.EmulatorPaths[7]}\", \"{gameSystem.EmulatorPaths[8]}\", \"{gameSystem.EmulatorPaths[9]}\" " +
            $", {gameSystem.DecompressFile[0]}, {gameSystem.DecompressFile[1]}, {gameSystem.DecompressFile[2]}, {gameSystem.DecompressFile[3]}, {gameSystem.DecompressFile[4]} " +
            $", {gameSystem.DecompressFile[5]}, {gameSystem.DecompressFile[6]}, {gameSystem.DecompressFile[7]}, {gameSystem.DecompressFile[8]}, {gameSystem.DecompressFile[9]} " +
            $", {gameSystem.NotSupported[0]}, {gameSystem.NotSupported[1]}, {gameSystem.NotSupported[2]}, {gameSystem.NotSupported[3]}, {gameSystem.NotSupported[4]} " +
            $", {gameSystem.NotSupported[5]}, {gameSystem.NotSupported[6]}, {gameSystem.NotSupported[7]}, {gameSystem.NotSupported[8]}, {gameSystem.NotSupported[9]} " +
            $", \"{gameSystem.PreferredExtension[0]}\", \"{gameSystem.PreferredExtension[1]}\", \"{gameSystem.PreferredExtension[2]}\", \"{gameSystem.PreferredExtension[3]}\", \"{gameSystem.PreferredExtension[4]}\" " +
            $", \"{gameSystem.PreferredExtension[5]}\", \"{gameSystem.PreferredExtension[6]}\", \"{gameSystem.PreferredExtension[7]}\", \"{gameSystem.PreferredExtension[8]}\", \"{gameSystem.PreferredExtension[9]}\" " +
            $")";
            return Update(sql, conn);
        }
        public bool Update(AlternateNames alternateNames, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (alternateNames == null)
                return false;
            if (conn == null)
                conn = unit_test_conn == null ? connection_AlternateNames : unit_test_conn;
            if (alternateNames == null)
                return false;
            string sql = "INSERT OR REPLACE INTO AlternateNames (DefaultName, Alternate, Title, NameSimplified, Compressed ) VALUES" +
            $" (\"{alternateNames.DefaultName.Replace("\"", "")}\", \"{alternateNames.Alternate.Replace("\"", "")}\", \"{alternateNames.Title.Replace("\"", "")}\", \"{alternateNames.NameSimplified}\", \"{alternateNames.Compressed}\" " +
            $")";
            return Update(sql, conn);
        }
        public bool Update(GameImage gameImage, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (gameImage == null)
                return false;
            string sql = "INSERT OR REPLACE INTO Images (Title, NameSimplified, NameOrg, Compressed, Checksum, FilePath) VALUES" +
            $" (\"{gameImage.Title}\", \"{gameImage.NameSimplified}\", \"{gameImage.NameOrg}\", \"{gameImage.Compressed}\", \"{gameImage.Checksum}\", \"{gameImage.FilePath}\")";
            return Update(sql, conn);
        }
        public bool Update(Mru item, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (item == null)
                return false;
            string sql = $"INSERT OR REPLACE INTO MRU (FilePath, DateLastUsed) VALUES (\"{item.FilePath}\", \"{item.DateLastUsed}\")";
            return Update(sql, conn);
        }
        public bool Update(EmulatorAttributes item, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (item == null)
                return false;
            string sql = "INSERT OR REPLACE INTO InitialDefaultEmulatorAttributes (EmulatorExecutable, DecompressFile, NotSupported, PreferredExtension) VALUES" +
                $" (\"{item.EmulatorExecutable}\", {item.DecompressFile}, {item.NotSupported}, \"{item.PreferredExtension}\")";
            return Update(sql, conn);
        }
        public bool Update(InitialDefaultEmulatorAttributes item, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (item == null)
                return false;
            string sql = $"INSERT OR REPLACE INTO InitialDefaultEmulatorAttributes (EmulatorExecutable, DecompressFile, NotSupported, PreferredExtension) VALUES (\"{item.EmulatorExecutable}\", {item.DecompressFile}, {item.NotSupported}, \"{item.PreferredExtension}\")";
            return Update(sql, conn);
        }
        public bool Update(ImageToRomMatchAlwaysNo item, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (item == null)
                return false;
            string sql = $"INSERT OR REPLACE INTO ImageToRomMatchAlwaysNo (RomChecksum, ImageChecksum) VALUES (\"{item.RomChecksum}\", \"{item.ImageChecksum}\")";
            return Update(sql, conn);
        }
        public bool Update(GameDetails item, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (item == null)
                return false;
            string sql = $"INSERT OR REPLACE INTO GameDetails " +
                            $"(NameSimplified, System, Title, Compressed, Region, Language, Status, Version, NotesCore, " +
                            $"QtyPlayers, Developer, ReleaseDate, Genre, NotesUser, FileFormat, Description, Rating, Year," +
                            $"Publisher, WikipediaURL, StarRating, StarRatingVoteCount, ImageFileName, FileName, ID) VALUES" +
                        $" (\"{item.NameSimplified}\", \"{item.System}\", \"{item.Title}\", \"{item.Compressed}\", \"{item.Region}\", \"{item.Language}\", \"{item.Status}\", \"{item.Version}\", \"{item.NotesCore}\", " +
                        $"{item.QtyPlayers}, \"{item.Developer}\", \"{item.ReleaseDate}\", \"{item.Genre}\", \"{item.NotesUser}\", \"{item.FileFormat}\", \"{item.Description}\", \"{item.Rating}\", {item.Year}," +
                        $"\"{item.Publisher}\", \"{item.WikipediaURL}\", {item.StarRating}, {item.StarRatingVoteCount}, \"{item.ImageFileName}\" , \"{item.FileName}\" , \"{item.ID}\" )";
            return Update(sql, conn);
        }
        #endregion /////////////////////////////////////////////////////////////////////////////////
        #region Delete item
        public bool Delete(Rom item, Microsoft.Data.Sqlite.SqliteConnection conn = null) => DeleteRom(item.FilePath, item.Title, conn);
        public bool Delete(GameImage item, Microsoft.Data.Sqlite.SqliteConnection conn = null) => DeleteImage(item.FilePath, conn);
        public bool Delete(Roms_UserChanges item, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Update($"DELETE FROM Roms_UserChanges WHERE FilePath like \"{item.FilePath}\"", conn);
        public bool Delete(Mru item, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Update($"DELETE FROM MRU WHERE FilePath like \"{item.FilePath}\"", conn);
        public bool Delete(InitialDefaultEmulatorAttributes item, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Update($"DELETE FROM InitialDefaultEmulatorAttributes WHERE EmulatorExecutable like \"{item.EmulatorExecutable}\"", conn);
        public bool Delete(ImageToRomMatchAlwaysNo item, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Update($"DELETE FROM ImageToRomMatchAlwaysNo WHERE RomChecksum like \"{item.RomChecksum}\" and ImageChecksum like \"{item.ImageChecksum}\"", conn);
        public bool Delete(GameSystem item, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Update($"DELETE FROM GameSystems WHERE ID = {item.ID}", conn);
        public bool Delete(GameDetails item, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Update($"DELETE FROM GameDetails WHERE Title like \"{item.Title}\" and System like \"{item.System}\"", Form_Main.connection_GameDetails);
        public bool Delete(AlternateNames item, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Update($"DELETE FROM AlternateNames WHERE DefaultName like \"{item.DefaultName}\" and Alternate like \"{item.Alternate}\"", conn == null ? (unit_test_conn == null ? connection_AlternateNames : unit_test_conn) : conn);
        public bool DeleteRom(string filePath, string title = "", Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            return !string.IsNullOrEmpty(filePath) && Update($"DELETE FROM Roms WHERE FilePath like \"{filePath}\" and Title like \"{title}%\"", conn);
        }
        public bool DeleteImage(string filePath, Microsoft.Data.Sqlite.SqliteConnection conn = null)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;
            bool returnValue = Update($"DELETE FROM Images WHERE FilePath like \"{filePath}\"", conn);
            Update($"UPDATE Roms SET ImagePath = \"\" WHERE ImagePath like \"{filePath}\"", conn);
            return returnValue;
        }
        public bool DeleteAllRomsInSystem(int systemID, Microsoft.Data.Sqlite.SqliteConnection conn = null) => Update($"DELETE FROM Roms WHERE System = \"{systemID}\"", conn);
        #endregion /////////////////////////////////////////////////////////////////////////////////
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
    public class InitialDefaultEmulatorAttributes
    {
        public string EmulatorExecutable;
        public int DecompressFile;
        public int NotSupported;
        public string PreferredExtension;
        public InitialDefaultEmulatorAttributes(string EmulatorExecutable, int DecompressFile, int NotSupported, string PreferredExtension)
        {
            this.EmulatorExecutable = EmulatorExecutable;
            this.DecompressFile = DecompressFile;
            this.NotSupported = NotSupported;
            this.PreferredExtension = PreferredExtension;
        }
    }
    public class ImageToRomMatchAlwaysNo
    {
        public string RomChecksum;
        public string ImageChecksum;
        public ImageToRomMatchAlwaysNo(string RomChecksum, string ImageChecksum)
        {
            this.RomChecksum = RomChecksum;
            this.ImageChecksum = ImageChecksum;
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
    public class ErrorLog
    {
        public string Process;
        public string Message;
        public int Code;
        public string Circumstances;
        public string Stack;
        public ErrorLog(string Process, string Message, int Code, string Circumstances, string Stack)
        {
            this.Process = Process;
            this.Message = Message;
            this.Code = Code;
            this.Circumstances = Circumstances;
            this.Stack = Stack;
        }
    }
    public class PersistenceVariables
    {
        public string Name;
        public string Value;
        public int ValueInt;
        public PersistenceVariables(string Name, string Value, int ValueInt)
        {
            this.Name = Name;
            this.Value = Value;
            this.ValueInt = ValueInt;
        }
    }
    public class TempConnection : IDisposable
    {
        public TempConnection(bool connectImageExternal = false, bool connectAlternateNames = true)
        {   
            if (connectAlternateNames)
                Db.ConnectToAlternateNames();
            if (connectImageExternal)
                Db.ConnectToImageExternal();
        }
        public void Dispose()
        {
            Db.CloseAlternateNames();
            Db.CloseImageExternal();
        }
    }
    #endregion /////////////////////////////////////////////////////////////////////////////////
}

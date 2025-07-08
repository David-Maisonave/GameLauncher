using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher
{
    public class EmulatorExecutables
    {
        public const int MAX_EMULATOR_EXECUTABLES = 10;
        public string[] EmulatorPaths = new string[MAX_EMULATOR_EXECUTABLES] { "", "", "", "", "", "", "", "", "", "" };
        public bool[] DecompressFile = new bool[MAX_EMULATOR_EXECUTABLES] { false, false, false, false, false, false, false, false, false, false };
        public bool[] NotSupported = new bool[MAX_EMULATOR_EXECUTABLES] { false, false, false, false, false, false, false, false, false, false };
        public string[] PreferredExtension = new string[MAX_EMULATOR_EXECUTABLES] { "", "", "", "", "", "", "", "", "", "" };
    }
    public class GameSystem : EmulatorExecutables
    {
        public string Name;
        public string RomDirPath;
        public string ImageDirPath;
        public int ID;
        public int PlayCount;
        public bool Initiated;
        public bool Hide;
        public GameSystem(string name, string romPath, string imgPath, EmulatorExecutables paths, int ID, int PlayCount, bool Hide, bool Initiated) 
        {
            Name = name;
            RomDirPath = romPath;
            ImageDirPath = imgPath;
            EmulatorPaths = paths.EmulatorPaths;
            DecompressFile = paths.DecompressFile;
            NotSupported = paths.NotSupported;
            PreferredExtension = paths.PreferredExtension;
            this.ID = ID;
            this.PlayCount = PlayCount;
            this.Hide = Hide;
            this.Initiated = Initiated;
        }
    }
}

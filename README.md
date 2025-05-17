# GameLauncher
A free frontend emulator displaying associated game boxart image for each game.
This launcher is similar to [SimpleLauncher](https://github.com/drpetersonfernandes/SimpleLauncher), but GameLauncher has more automation for finding matching images associated with ROM's.
GameLauncher does NOT try to install emulators, ROM's, or associated images. It can find image matches for over 90% of the available ROM's even if the file names are not an exact match.

![GameLauncher](https://github.com/user-attachments/assets/38fbb66d-529f-4a70-9aee-a7458131300d)

#### Image Files
The current version of GameLauncher depends on users downloading the images which can be found in the following links:
https://github.com/libretro-thumbnails/libretro-thumbnails
https://www.osboxes.org/virtualbox-images/

When first running GameLauncher, it has to perform a file scan to search for the ROM files and associated image files. This can take up to an hour if the filesystem has 5000 ROM's or more.

#### Default Search Points
By default, it will search for ROM files in the following folders:
````
C:\Game
C:\Games
C:\Emulator
C:\Emulators
C:\RetroGameEmulator
C:\RetroGameEmulators
C:\RetroEmulator
C:\RetroEmulators
C:\Retro
C:\RetroGame
C:\RetroGames
C:\GameRetro
````
If not found in above paths, user can change the search folder using the Settings option.

#### Expected Filesystem Tree
GameLauncher expects the following file system tree structure:

![EmulatorFileTree](https://github.com/user-attachments/assets/ac1f01ca-5fde-41e1-94eb-3bd1f3f9ca47)

#### Tested Emulators 
GameLauncher has been tested with the following game console systems and associated emulators:
````
Atari2600  (C:\Emulator\Atari2600\Stella - PLAY!.exe)
GameboyAdvance  (C:\Emulator\GameboyAdvance\visualboyadvance-m.exe)
Nintendo64  (C:\Emulator\Nintendo64\Project64\Project64.exe)
NintendoNES  (C:\Emulator\NintendoNES\VirtuaNES.exe)
NintendoSNES  (C:\Emulator\NintendoSNES\zsnesw.exe)
NintendoSNES  (C:\Emulator\NintendoSNES\zsnesw.exe)
SegaGenesis  (C:\Emulator\SegaGenesis\Sega Mega Drive Fusion.exe)
SegaMasterSystem  (C:\Emulator\SegaMasterSystem\Fusion.exe)
````
It should work with any modern emulator that can take the ROM file path as a command line argument.

It doesn't work on emulators like (NeoRAGEx.exe) which fail to take command line arguments.

# Details
### ROM and Image search
GameLauncher creates a SQLite database that contains all the ROM and image files found in the filesystem from a given base directory.
It sets a title for each ROM base of the ROM file name excluding common ROM codes.
Example: Auto-Upturn (Sachen) [U][!].zip, would get a title of 'Auto-Upturn'
The database also store a compress title name, which removes non-alphanum characters. It does this for both the ROM files and the image files so that it's easier to find matching image file for assocaited ROM files.
GameLauncher searches for image files under each game console system directory, and it aslo searches for image files under the main directory. ``C:\Emulators\Images``.
It will try to match a ROM file first with it's associated game console image directory, and if it doesn't find it there, it will then look under the common image directory.
Users can optionally just have one image directory for all the game console systems.
### Emulator executable search
GameLauncher searches for either an executable or a link file under each game console system directory.  This is done durring the initial filesystem scan.
It will store up to 10 different emulators in the database, and by default, it will use the first executable or link file found to launch associated ROM file.
If a game console system directory does not have any executables or links, the directory will be ignored, and no ROM's will be scanned.
### Main Window Usage
The main window displays an alphabetical image list. Double clicking the image will launch the emulator and pass the associated ROM.
Hovering the mouse cursor over the image will cause the status bar in the bottom of the screen to get updated with the ROM details.
Right clicking ther image will display the following context menu:

![ContextMenu](https://github.com/user-attachments/assets/36276ce7-7621-4529-8321-aed9479664d3)

### Settings
The settings dialog window can be used to change emulator starting directory search path and other associated options.
Most of these options do not take affect until a full Rescan is performmed.

<img width="303" alt="Settings" src="https://github.com/user-attachments/assets/11cb39ce-9167-4f67-a6f7-40d64bccfce2" />

When setting up this menu for children, "Disable Advance Options" should be checked.
### Advance Options
The advance option window can be used to perform a full rescan as well as other more advance options.

<img width="233" alt="AdvanceOptions" src="https://github.com/user-attachments/assets/bec48139-a431-43f0-b5ed-390a5068b78a" />

## ToDo
Below are future plan updates.
* Add option under advance option to change the default emulator for a given game console system.
* ROM details editor.


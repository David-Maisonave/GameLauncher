# GameLauncher
* A free frontend emulator displaying associated game boxart image for each game.
* GameLauncher can be controlled using standard Xbox controller.
* This launcher is similar to [SimpleLauncher](https://github.com/drpetersonfernandes/SimpleLauncher), but GameLauncher has more automation for finding matching images associated with ROM's.
* GameLauncher does NOT try to install emulators, ROM's, or associated images. It can find image matches for over 90% of the available ROM's even if the file names are not an exact match.

![GameLauncher](https://github.com/user-attachments/assets/38fbb66d-529f-4a70-9aee-a7458131300d)

#### Image Files
A full collection of ROM associated image files can be downloaed using the following link:
[Images.zip](http://axter.com/GameLauncher/Images.zip)

Image files can also be downloaded from the following links:
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

![DirTreeExample](https://github.com/user-attachments/assets/ab5ab245-5e7d-427a-b590-af84cd24904f)

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
* Example: Auto-Upturn (Sachen) [U][!].zip, would get a title of **Auto-Upturn**

The database also store a compress title name, which removes non-alphanumeric characters. It does this for both the ROM files and the image files so that it's easier to find matching image file for associated ROM files.
GameLauncher searches for image files under each game console system directory, and it also searches for image files under the main directory. ``C:\Emulators\Images``.
It will try to match a ROM file first with it's associated game console image directory, and if it doesn't find it there, it will then look under the common image directory.
Users can optionally just have one image directory for all the game console systems.
### Emulator executable search
GameLauncher searches for either an executable or a link file under each game console system directory.  This is done during the initial filesystem scan.
It will store up to 10 different emulators in the database, and by default, it will use the first executable or link file found to launch associated ROM file.
If a game console system directory does not have any executables or links, the directory will be ignored, and no ROM's will be scanned.
### Main Window Usage
The main window displays an alphabetical image list. Double clicking the image will launch the emulator and pass the associated ROM.
Hovering the mouse cursor over the image will cause the status bar in the bottom of the screen to get updated with the ROM details.
Right clicking the image will display the following context menu:

![ContextMenu](https://github.com/user-attachments/assets/36276ce7-7621-4529-8321-aed9479664d3)

The combobox in the upper left corner is used to select the desired game console system.

![SystemSelectionMenuOption](https://github.com/user-attachments/assets/528c9673-7114-4f3c-aebd-eb5ba18c61c0)

The second combobox is used to select the desired icon size and image view structure.

![IconViewOption](https://github.com/user-attachments/assets/db29ca0d-71ae-4697-9eaf-b0f4c64425ac)

Example image view with small icon selected:

![smallIconImageView](https://github.com/user-attachments/assets/0341e286-3d55-4150-b8a3-5893a183abe3)

Using the **Settings** option the user can change the small icons to display between 8x8 to 64x64.
Example 64x64 size.

![smallIconImageViewAt64x64](https://github.com/user-attachments/assets/373e05bf-69a4-49cd-a5a5-39a670848366)

Using the **Settings** option the user can change the large icons to display between 64x64 to 256x256.

### Settings
The settings dialog window can be used to change emulator starting directory search path and other associated options.
Most of these options do not take affect until a full Rescan is performed.

<img width="303" alt="Settings" src="https://github.com/user-attachments/assets/11cb39ce-9167-4f67-a6f7-40d64bccfce2" />



When setting up this menu for children, "Disable Advance Options" should be checked.
### Advance Options
The advance option window can be used to perform a full rescan as well as other more advance options.

<img width="233" alt="AdvanceOptions" src="https://github.com/user-attachments/assets/bec48139-a431-43f0-b5ed-390a5068b78a" />

Most of the options are self explanatory.
The checksum options are used to help detect duplicate image and ROM files. Changes to these options don't take affect until a full rescan is performed.

## ToDo
Below are possible future plan updates.
* [0.9.1 ???] Add support for multiple source paths for searching games. Item has been added but not tested.
* [1.0.0] Filter out string "2-in-1 - " and "3-in-1 - " from front of name in compressed name for ROM's in DB.
* [1.0.0] Filter out string "Disney's" and "LEGO" from front of name in compressed name for ROM's in DB.
* [1.0.0] Add Year to ROM database
* [1.0.0] Add rating to ROM database
* [1.0.0] Add pagination to game console systems having over 2000 ROM's.
* [1.0.0] Add option to advance menu to compress all ROM files to zip.
* Add option to view preview/play image over box-art image.
* Add option to Settings to allow changing default **images** and **roms** sub folder.
* Add option to password protected advance option.
* Add Favorites option to the system combobox, where the favorites list the last 100 ROM's played, and listed in order of last played.
* Add multithreading progressbar code for when database is initialized. Each system scan should have two threads. One for ROM scans and the other for image scans.

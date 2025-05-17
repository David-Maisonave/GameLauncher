# GameLauncher
A frontend emulator displaying associated game boxart image for each game.

![GameLauncher](https://github.com/user-attachments/assets/38fbb66d-529f-4a70-9aee-a7458131300d)

It can associate over 90% of the avialble images with the ROM files found listed under the emulator even if the names are not an exact match.
The current version of GameLauncher depends on users downloading the images which can be found in the following links:
https://github.com/libretro-thumbnails/libretro-thumbnails
https://www.osboxes.org/virtualbox-images/

A future version will include some of the images.
When first running GameLauncher, it has to perform a file scan to search for the ROM files and associated image files. This can take up to an hour if the filesystem has 5000 ROM's or more.
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
GameLauncher expects the following file system tree structure:

![EmulatorFileTree](https://github.com/user-attachments/assets/ac1f01ca-5fde-41e1-94eb-3bd1f3f9ca47)




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

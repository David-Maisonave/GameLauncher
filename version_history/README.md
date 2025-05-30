##### This page was added starting on version 0.2.0 to keep track of newly added features between versions.
### 0.9
- First release
### 0.9.1
- Option under advance option to change the default emulator for a given game console system.
- Convert main window combobox to menu strip.
- Decompression option for emulators like DuckStation which don't support ZIP files.
- Filter option.
- Option to context menu to open link to launchbox-app.com with the ROM title name, or open google link with title. Example: https://www.google.com/search?q=Disney+Aladdin+site%253Alaunchbox-app.com
- Decompression support for ".7z", ".rar", ".tar", ".gz", ".bz2", and ".lz"
- ROM details editor.
- GameSystem editor.
- Option to clean and rescan ROM's for selected game console system.
- Move options in advance option dialog to the main menu.
- Support for multiple source paths for searching games. This is included in 0.9.1, but it has not be fully tested.
### 0.9.2
- Fixed bug with missing sqlite DLL.
- General image search
- Selected system image search
- Fixed bug with scan selected rom and image search where it was performing full scan.
### 1.0
- Added GameDetails.db database which contains ROM details pulled from other sources.
- Made both options for delete duplicate roms by title deselect largest size ROM by default.
- Added following options to menu [Misc Util]->[Database Util]
    - Reset title and compress DB
	- Reset compress names in ROM's and images DB
	- Populate GameDetails DB
	- Add GameDetails data to GameLauncher DB
	- Search for image matches for games missing image
- 
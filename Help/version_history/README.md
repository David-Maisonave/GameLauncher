##### This page was added starting on version 0.2.0 to keep track of newly added features between versions.
### 0.9
- First release
### 0.9.1
- Option under advance option to change the default emulator for a given game console system.
- Convert main window combobox to menu strip.
- Decompression option for emulators like DuckStation which don't support ZIP files.
- Filter option.
- Option to context menu to open link to launchbox-app.com with the ROM title name, or open google link with title. Example: https://www.google.com/search?q=Disney+Aladdin+site%253Alaunchbox-app.com
- Decompression support for ".zip", ".7z", ".rar", ".tar", ".gz", ".bz2", and ".lz"
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
- Added MRU. Added recent games menu option to main menu
- Added option to main menu to compress all ROM files in selected folder. Include support for following compression types (".zip", ".7z", ".rar", ".tar", ".gz", ".bz2", and ".lz").
- Added support to convert PNG files to JPG files.
- Added logic to filter out string "2-in-1 - " and "3-in-1 - " from front of name in compressed name for ROM's in DB.
- Added logic to filter out string "Disney's" and "LEGO" from front of name in compressed name for ROM's in DB.
- Added Year and Rating field to ROM database.
- Added pagination to game console systems having over 1500 ROM's.
- Added option to change default images and roms sub folder.
- Added option to associate preview images vs box-art images.
- Added option to delete duplicate ROM files based on NameSimplified, NameOrg, or Compressed name.
### 1.0.1
- Fixed bug when decompressing files.
- Added Year and Rating fields to the ROM Details editor.
- Added "Only Remove from DB" option to the ROM's-Delete form.
- Added fields (StarRating, StarRatingVoteCount, WikipediaURL, and Publisher) to GameDetails database.
- Added implementation to fetch game details from XML files and add it to the GameDetails database.
- Added multithreading logic to TempDirStorage which allows decompressing multiple files in multithread environment.
- Added CreateChecksumForRoms option to main menu. This allows checksums to be created after database initialization.
- Added filter by Rating, Qty-Players, Star-Rating, Genre, Language, Region, Release-Decade, and Developers.
- Added All-System option to system combobox, and have it use pagination.
- Added Favorite option to ROM's table.
- Added Favorites option to the system combobox, with pagination support.
- Added GameLauncher database backup and restore support.
- Added option to add new game-systems (emulators) after database has been initialized.
- Added option to automatically convert image files to JPG file during scan for new images. This option is disabled by default, and must be enabled from the Settings dialog window.
- Added support to convert BMP files to JPG.
- Added filter for ROM's with or without associated image files.
- Removed ConsolidatedFiles binary from the build, because it was triggering a bogus malware warning on sourceforge.
### 1.0.2
- Added option to open File-Explorer with the selected ROM file selected in Explorer.
- Added 'add to favorite' menu option.
- Added Decompress ROM file option.
- Added Taskbar progressbar feature.
- Added logging file logic.
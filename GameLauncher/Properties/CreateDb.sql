CREATE TABLE "GameSystems" (  "Name" TEXT NOT NULL UNIQUE,  "ID" INTEGER NOT NULL UNIQUE,  "RomDirPath" TEXT NOT NULL,  "ImageDirPath" TEXT NOT NULL,  "EmulatorPath1" TEXT NOT NULL,  "EmulatorPath2" TEXT,  "EmulatorPath3" TEXT,  "EmulatorPath4" TEXT,  "EmulatorPath5" TEXT,  "EmulatorPath6" TEXT,  "EmulatorPath7" TEXT,  "EmulatorPath8" TEXT,  "EmulatorPath9" TEXT,  "EmulatorPath10" TEXT,  PRIMARY KEY("ID" AUTOINCREMENT) );
CREATE TABLE "Roms" (  "Title" TEXT NOT NULL,  "NameSimplified" TEXT NOT NULL,  "NameOrg" TEXT NOT NULL,  "Compressed" TEXT NOT NULL,  "System" INTEGER NOT NULL,  "FilePath" TEXT NOT NULL UNIQUE,  "PreferredEmulator" INTEGER NOT NULL DEFAULT 0,  "ImagePath" TEXT,  "QtyPlayers" INTEGER NOT NULL DEFAULT 1,  "Status" TEXT,  "Region" TEXT,  "Developer" TEXT,  "ReleaseDate" TEXT,  "RomSize" INTEGER,  "Genre" TEXT,  "NotesCore" TEXT,  "NotesUser" TEXT,  "FileFormat" TEXT,  "Version" TEXT,  "Description" TEXT,  "Language" TEXT,  "Year" INTEGER,  "Rating" TEXT,  "Checksum" TEXT,  "CompressChecksum" TEXT,  "Publisher" TEXT,  "WikipediaURL" TEXT,  "StarRating" REAL,  "StarRatingVoteCount" INTEGER,  "Favorite" BOOLEAN,  "Disable" BOOLEAN,  PRIMARY KEY("FilePath") );
CREATE TABLE "Images" (
	"Title"	TEXT NOT NULL,
	"NameSimplified"	TEXT NOT NULL,
	"NameOrg"	TEXT NOT NULL,
	"Compressed"	TEXT NOT NULL,
	"FilePath"	TEXT NOT NULL UNIQUE,
	"Checksum"	TEXT
);
CREATE TABLE "PersistenceVariables" (  "Name" TEXT NOT NULL UNIQUE,  "Value" TEXT,  "ValueInt" INTEGER,  PRIMARY KEY("Name") );
CREATE TABLE "Roms_UserChanges" (  "Title" TEXT NOT NULL,  "FilePath" TEXT NOT NULL UNIQUE,  "PreferredEmulator" INTEGER NOT NULL DEFAULT 0,  "ImagePath" TEXT,  "QtyPlayers" INTEGER NOT NULL DEFAULT 1,  "Status" TEXT,  "Genre" TEXT,  "NotesCore" TEXT,  "NotesUser" TEXT,  "Description" TEXT,  PRIMARY KEY("FilePath") );
CREATE TABLE "EmulatorAttributes" (  "EmulatorExecutable" TEXT NOT NULL UNIQUE,  "DecompressFile" NUMERIC DEFAULT 0,  "NotSupported" INTEGER DEFAULT 0,  "PreferredExtension" TEXT,  PRIMARY KEY("EmulatorExecutable") );
CREATE TABLE "ErrorLog" (  "Process" TEXT NOT NULL,  "Message" TEXT NOT NULL,  "Code" INTEGER NOT NULL,  "Circumstances" TEXT,  "Stack" TEXT );
CREATE TABLE "FilterAutoCompleteCustomSource" (  "Source" TEXT NOT NULL UNIQUE,  PRIMARY KEY("Source") );
CREATE TABLE "MRU" (  "FilePath" TEXT NOT NULL UNIQUE,  "DateLastUsed" TEXT NOT NULL,  PRIMARY KEY("FilePath") );

CREATE TABLE "GameDetails" (
	"System"	TEXT NOT NULL,
	"Title"	TEXT NOT NULL,
	"NameSimplified"	TEXT NOT NULL,
	"Compressed"	TEXT NOT NULL,
	"QtyPlayers"	NUMERIC NOT NULL DEFAULT 0,
	"Year"	INTEGER NOT NULL DEFAULT 0,
	"Status"	TEXT NOT NULL DEFAULT "",
	"ImageFileName"	TEXT NOT NULL DEFAULT "",
	"Region"	TEXT NOT NULL DEFAULT "",
	"Developer"	TEXT NOT NULL DEFAULT "",
	"ReleaseDate"	TEXT NOT NULL DEFAULT "",
	"Genre"	TEXT NOT NULL DEFAULT "",
	"NotesCore"	TEXT NOT NULL DEFAULT "",
	"NotesUser"	TEXT NOT NULL DEFAULT "",
	"FileFormat"	TEXT NOT NULL DEFAULT "",
	"Version"	TEXT NOT NULL DEFAULT "",
	"Description"	TEXT NOT NULL DEFAULT "",
	"Language"	TEXT NOT NULL DEFAULT "",
	"Rating"	TEXT NOT NULL DEFAULT "",
	"Publisher"	TEXT NOT NULL DEFAULT "",
	"WikipediaURL"	TEXT NOT NULL DEFAULT "",
	"StarRating"	REAL NOT NULL DEFAULT 0,
	"StarRatingVoteCount"	INTEGER NOT NULL DEFAULT 0,
	"FileName"	TEXT NOT NULL DEFAULT "",
	"ID"	TEXT NOT NULL DEFAULT "",
	PRIMARY KEY("System","Title")
);
CREATE TABLE "Roms" (
	"Title"	TEXT NOT NULL,
	"NameSimplified"	TEXT NOT NULL,
	"NameOrg"	TEXT NOT NULL,
	"Compressed"	TEXT NOT NULL,
	"System"	INTEGER NOT NULL,
	"FilePath"	TEXT NOT NULL UNIQUE,
	"PreferredEmulator"	INTEGER NOT NULL DEFAULT 0,
	"ImagePath"	TEXT,
	"QtyPlayers"	INTEGER NOT NULL DEFAULT 1,
	"Status"	TEXT,
	"Region"	TEXT,
	"Developer"	TEXT,
	"ReleaseDate"	TEXT,
	"RomSize"	INTEGER,
	"Genre"	TEXT,
	"NotesCore"	TEXT,
	"NotesUser"	TEXT,
	"FileFormat"	TEXT,
	"Version"	TEXT,
	"Description"	TEXT,
	"Language"	TEXT,
	"Year"	INTEGER,
	"Rating"	TEXT,
	"Checksum"	TEXT,
	"CompressChecksum"	TEXT,
	"Publisher"	TEXT,
	"WikipediaURL"	TEXT,
	"StarRating"	REAL,
	"StarRatingVoteCount"	INTEGER,
	PRIMARY KEY("FilePath")
);

ALTER TABLE Roms ADD Favorite Boolean; 
ALTER TABLE Roms ADD Disable Boolean; 
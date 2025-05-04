// ConsolidateFiles.cpp : This file contains the 'main' function. Program execution begins and ends there.
// Example Nintendo 64 rom types zip,n64,v64,z64,rom,bin,pal,usa,jap,m64
// Example Nintendo NES rom types zip,nes,7z,unf,nez,unif,fds,nsf,nsfe
// Example SNES rom types zip,smc,sfc,fig,swf,bin
// Example Gameboy Advance rom types zip,gba,7z,srl
// Example Atari 2600 rom types zip,7z,bin,rom,a26,car,xex,atr
// Example Sega Master System rom types zip,7z,sms,bin
// Example Sega Mega Drive (Sega Genesis) rom types zip,7z,bin,md,gen,smd
// Example Neo Geo rom types zip,7z,ngp

// Command lines used and tested for different game emulators
// "C:\Emulators\Nintendo 64\all_roms" "C:\Emulators\Nintendo 64\roms" zip,n64,v64,z64 /n /i /z
// "C:\Emulators\Gameboy Advance\games_and_videos\all_roms" "C:\Emulators\Gameboy Advance\roms" zip,gba,7z,srl /n /i /z /p


#include <iostream>
#include <sstream>
#include <cstring>
#include <string.h>
#include <string>
#include <algorithm>
#include <vector>
#include <map>
#include <set>
#include <filesystem>
#include <fstream>
#include <cctype>
#include <cstdlib>
using namespace std;

//  "C:\Program Files\7-Zip\7z.exe" a -tzip AGS-PGAE.zip AGS-PGAE.n64
//  "C:\Program Files\7-Zip\7z.exe" a -tzip E:\MyPrograms\TestFiles\t\AGS-PGAE.zip E:\MyPrograms\TestFiles\t\AGS-PGAE.n64
string z7zip = "C:\\Program Files\\7-Zip\\7z.exe"; // C:\Program Files\7-Zip\7z.exe
void Help(bool ShowHelp, int argc);
vector<string> parse_comma_separated_string(const string& input);

map<string, int> FileTypeMapStrKey;
map<int, string> FileTypeMapIntKey;
bool IgnoreFilenameBracketSection = false;
bool OnlyCompareLetterAndNumbers = false;
bool FilterOutPrefixNumbers = false;

string toLower(string s) {
	for (auto& c : s)	c = tolower(c);
	return s;
}

class FileDetails {
private:
	string BaseNameOrg;
public:
	const string FullPath;
	const string BaseName;
	const string Ext;
	const int ExtIndx;
	FileDetails(const string &fullpath, const string &basenameorg, const string &ext) 
		: FullPath(fullpath), BaseNameOrg(basenameorg), BaseName(ProcName(toLower(basenameorg))),
		Ext(toLower(ext)), ExtIndx(FileTypeMapStrKey[Ext])
	{
		// std::cout << BaseName << std::endl;
	}
	string GetBaseNameOrg() const { return BaseNameOrg; }
	bool operator < (const FileDetails& filedetails) const
	{
		if (BaseName == filedetails.BaseName)
			return ExtIndx < filedetails.ExtIndx;
		return (BaseName < filedetails.BaseName);
	}
private:
	string ProcName(string s) {
		if (FilterOutPrefixNumbers) {
			if (isdigit(s[0])) {
				if (s.size() > 7 && isdigit(s[0]) && isdigit(s[1]) && isdigit(s[2]) && isdigit(s[3]) &&
					s[4] == ' ' && s[5] == '-' && s[6] == ' ') {
					s = s.substr(7);
					BaseNameOrg = BaseNameOrg.substr(7);
				}
			}
		}
		if (IgnoreFilenameBracketSection) {
			//size_t p = s.find("(V");
			//if (p == string::npos)
			//	p = s.find("(M");
			size_t p = s.find("(");
			if (p != string::npos) {
				s = s.substr(0, p);
			}
			p = s.find("[");
			if (p != string::npos) {
				s = s.substr(0, p);
			}
		}
		if (OnlyCompareLetterAndNumbers) {
			string tmp = s;
			s = "";
			for (auto& c : tmp) {
				if (isdigit(c) || isalpha(c))
					s += c;
			}
		}
		return s;
	}
};

//void testExecute() {
//	z7zip = "\"" + z7zip + "\"";
//	for (int i = 0; i < 99; ++i) {
//		string s = "start \"\" \"C:\\Program Files\\7-Zip\\7z.exe\" a -tzip \"E:\\MyPrograms\\TestFiles\\Consolidated\\A Bug's Life (U) [!].zip\" \"E:\\MyPrograms\\TestFiles\\Consolidated\\A Bug's Life (U) [!].v64\"";
//		int r = system(s.c_str());
//		cout << "results = " << r << endl;
//		r = 0;
//	}
//}

int main(int argc, char* argv[])
{
	// cout << "Number of arguments: " << argc << endl;
	bool ShowHelp = (argc > 1 && (argv[1][0] == '?' || (strlen(argv[1]) > 1 && argv[1][1] == '?'))) ? true : false;
	if (argc < 4 || ShowHelp)
		Help(ShowHelp, argc);

	string SourceDir = argv[1], DestDir = argv[2], FileTypeListStr = argv[3];
	if (DestDir[DestDir.size() - 1] != '\\')
		DestDir += "\\";
	string DirToCreate = DestDir;
	DirToCreate[DirToCreate.size() - 1] = 0;
	if (!filesystem::exists(DirToCreate))
		filesystem::create_directory(DirToCreate);

	vector<string> FileTypeList = parse_comma_separated_string(FileTypeListStr);
	for (unsigned i = 0; i < FileTypeList.size(); ++i) {
		string ext = toLower(FileTypeList[i]);
		if (ext[0] != '.')
			ext = "." + ext;
		FileTypeMapStrKey[ext] = i;
		FileTypeMapIntKey[i] = ext;
	}
	bool MakeTargetFileZip = false;

	for (int i = 1; i < argc; ++i) {
		if (_strcmpi(argv[i], "\\z") == 0 || _strcmpi(argv[i], "/z") == 0)
			MakeTargetFileZip = true;
		if (_strcmpi(argv[i], "\\i") == 0 || _strcmpi(argv[i], "/i") == 0)
			IgnoreFilenameBracketSection = true;
		if (_strcmpi(argv[i], "\\n") == 0 || _strcmpi(argv[i], "/n") == 0)
			OnlyCompareLetterAndNumbers = true;
		if (_strcmpi(argv[i], "\\p") == 0 || _strcmpi(argv[i], "/p") == 0)
			FilterOutPrefixNumbers = true;
	}

	if (MakeTargetFileZip && !filesystem::exists(z7zip)) {
		cout << "Can not compress (zip) files because 7z is not installed in following path:" << endl;
		cout << z7zip << endl;
		cout << "" << endl;
		Help(true, argc);
	}
	z7zip = "\"" + z7zip + "\"";
	
	multiset<FileDetails> FullFileList;
	for (const auto& entry : filesystem::recursive_directory_iterator(SourceDir)) {
		filesystem::path fileInfo = entry.path();
		if (FileTypeMapStrKey.find(toLower(fileInfo.extension().string())) != FileTypeMapStrKey.end()) {
			FileDetails filedetails(entry.path().string(), fileInfo.stem().string(), fileInfo.extension().string());
			FullFileList.insert(filedetails);
			// cout << " Valid Type " << entry.path() << endl;
		}
		//else
			//cout << " Skipping " << entry.path() << endl;
	}
	cout << "***********************************" << endl;
	cout << "Copying files......................" << endl << endl;
	set<string> FilesAdded;
	vector<string> FilesThatGotCompressed;
	for (multiset<FileDetails>::iterator it = FullFileList.begin(); it != FullFileList.end(); ++it) {
		if (FilesAdded.find(it->BaseName) == FilesAdded.end()) {
			string SrcFile = it->FullPath;
			string DestFile = DestDir + it->GetBaseNameOrg() + it->Ext;
			cout << "Copying file to " << DestFile << endl;
			filesystem::copy_file(SrcFile, DestFile);
			if (MakeTargetFileZip && it->Ext != ".zip" && it->Ext != ".7z") {
				string DestZipFile = DestDir + it->GetBaseNameOrg() + ".zip";
				string ZipCmd = "start \"Compress File\" /wait /b " + z7zip + " a -tzip \"" + DestZipFile + "\" \"" + DestFile + "\"";
				system(ZipCmd.c_str());
				if (filesystem::exists(DestZipFile)) {
					FilesThatGotCompressed.push_back(it->FullPath);
					remove(DestFile.c_str());
				}
			}
			FilesAdded.insert(it->BaseName);
		}
	}
	if (FilesThatGotCompressed.size() > 0) {
		cout << "List of files which got compressed." << endl << endl;
		for (vector<string>::iterator it = FilesThatGotCompressed.begin(); it != FilesThatGotCompressed.end(); ++it) {
			cout << *it << endl;
		}
	}
	cout << "***********************************" << endl << endl;
	return 0;
}

vector<string> parse_comma_separated_string(const string& input) 
{
	vector<string> result;
	stringstream ss(input);
	string token;
	while (getline(ss, token, ',')) 
		result.push_back(token);
	return result;
}

void Help(bool ShowHelp, int argc)
{
	if (ShowHelp == false) {
		cout << "Missing required arguments. Need minimum of 3 arguments, but only received " << argc-1 << endl;
		cout << "For more details on how to used this program use command line argument ?" << endl;
	}
	else
	{
		cout << "The primary purpose of this program is to consolidate assorted file types into one folder." << endl;
		cout << "This program was originally created for consolidating game emulator ROM files, but it can be used for any file types." << endl;
		cout << "The program will copy only one copy of each file which has a unique base file name (excluding file extension)." << endl;
		cout << "If there are multiple copies of a file, it will copy the first file type available that is in the beginning of the file type list." << endl;
		cout << "" << endl;
		cout << "" << endl;
		cout << "*********************************************************" << endl;
		cout << "If command line argument /i is used, then when comparing file names, the program will ignore the part of the filename that is wrapped with () or []." << endl;
		cout << "In below examples (E),[!],(U),(J),(GC) are ignored." << endl;
		cout << "		Airboarder 64 (E) [!]" << endl;
		cout << "		Banjo-Tooie (U) [!]" << endl;
		cout << "		Spy Vs Spy (U)" << endl;
		cout << "		Binary Land (J)" << endl;
		cout << "		Spelunker 2 - Yuusha heno Chousen (J) [b1][T+Eng0.9_Sarysa]" << endl;
		cout << "		Legend of Zelda, The - Ocarina of Time - Master Quest (U) (GC) [!]" << endl;
		cout << "		Star Wars (J) (Namco) [T+Eng_Mukimuki&Shinta]" << endl;
		cout << "" << endl;
		//cout << "In following examples, (V1.1), (V1.2), and (M3) are NOT ignored." << endl;
		//cout << "		Blast Corps (U) (V1.1) [!]" << endl;
		//cout << "		Cruis'n USA (U) (V1.2) [!]" << endl;
		//cout << "		Doom 64 (U) (V1.1) [!]" << endl;
		//cout << "		F-1 Pole Position 64 (U) (M3) [!]" << endl;
		//cout << "" << endl;
		cout << "" << endl;
		cout << "*********************************************************" << endl;
		cout << "If command line argument /n is used, then only numbers and letters are compared." << endl;
		cout << "*********************************************************" << endl;
		cout << "If command line argument /p is used, then 4 digit prefix file names have the prefix remove. Example: '2170 - Evergirl.zip' is converted to 'Evergirl.zip'" << endl;
		cout << "*********************************************************" << endl;
		cout << "If command line argument /z is used, then it will compress all the files to a zip file, except 7z file extension. 7z file types are always copied as-is." << endl;
		cout << "The /z option uses 7z, and it only works if 7z is installed in the following path: C:\\Program Files\\7-Zip" << endl;
		cout << "" << endl;
		cout << "" << endl;
		cout << "*********************************************************" << endl;
		cout << "*********************************************************" << endl;
		cout << "" << endl;
		cout << "" << endl;
	}
	cout << "Example usage: " << endl;
	cout << "               ConsolidateFiles.exe SourceDir DestDir CommaSepFileTypeList" << endl;
	cout << "               ConsolidateFiles.exe \"C:\\MyParentDirWithMixFiles\" \"C:\\MyTargetDirToConsolidate\" zip,n64,v64,z64,rom,bin" << endl;
	cout << "               ConsolidateFiles.exe \"C:\\MySrcRomDir\" \"C:\\DestDir\" zip,nes,7z" << endl;
	cout << "" << endl;
	cout << "" << endl;
	exit(ShowHelp ? 0 : -1);
}
// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file

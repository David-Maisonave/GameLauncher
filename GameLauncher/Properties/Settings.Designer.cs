﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GameLauncher.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.14.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string DbPath {
            get {
                return ((string)(this["DbPath"]));
            }
            set {
                this["DbPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string DefaultImagePath {
            get {
                return ((string)(this["DefaultImagePath"]));
            }
            set {
                this["DefaultImagePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string EmulatorsBasePath {
            get {
                return ((string)(this["EmulatorsBasePath"]));
            }
            set {
                this["EmulatorsBasePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool usePreviousCollectionCache {
            get {
                return ((bool)(this["usePreviousCollectionCache"]));
            }
            set {
                this["usePreviousCollectionCache"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("6")]
        public int MaxNumberOfPairThreadsPerList {
            get {
                return ((int)(this["MaxNumberOfPairThreadsPerList"]));
            }
            set {
                this["MaxNumberOfPairThreadsPerList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("128")]
        public int largeIconSize {
            get {
                return ((int)(this["largeIconSize"]));
            }
            set {
                this["largeIconSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("32")]
        public int smallIconSize {
            get {
                return ((int)(this["smallIconSize"]));
            }
            set {
                this["smallIconSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool useJoystickController {
            get {
                return ((bool)(this["useJoystickController"]));
            }
            set {
                this["useJoystickController"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool disableAdvanceOptions {
            get {
                return ((bool)(this["disableAdvanceOptions"]));
            }
            set {
                this["disableAdvanceOptions"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Maximised {
            get {
                return ((bool)(this["Maximised"]));
            }
            set {
                this["Maximised"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string RegexRenameFilesDir {
            get {
                return ((string)(this["RegexRenameFilesDir"]));
            }
            set {
                this["RegexRenameFilesDir"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string RegexRenameFilesPattern {
            get {
                return ((string)(this["RegexRenameFilesPattern"]));
            }
            set {
                this["RegexRenameFilesPattern"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string RegexRenameFilesReplace {
            get {
                return ((string)(this["RegexRenameFilesReplace"]));
            }
            set {
                this["RegexRenameFilesReplace"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DoRomChecksum {
            get {
                return ((bool)(this["DoRomChecksum"]));
            }
            set {
                this["DoRomChecksum"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DoImageChecksum {
            get {
                return ((bool)(this["DoImageChecksum"]));
            }
            set {
                this["DoImageChecksum"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public int MaxRetryDbExecute {
            get {
                return ((int)(this["MaxRetryDbExecute"]));
            }
            set {
                this["MaxRetryDbExecute"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public int DbRetryTimeoutInSeconds {
            get {
                return ((int)(this["DbRetryTimeoutInSeconds"]));
            }
            set {
                this["DbRetryTimeoutInSeconds"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool SHA256OverMD5 {
            get {
                return ((bool)(this["SHA256OverMD5"]));
            }
            set {
                this["SHA256OverMD5"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DoZipChecksum {
            get {
                return ((bool)(this["DoZipChecksum"]));
            }
            set {
                this["DoZipChecksum"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool AutoCompleteCustomSourceLiveUpdate {
            get {
                return ((bool)(this["AutoCompleteCustomSourceLiveUpdate"]));
            }
            set {
                this["AutoCompleteCustomSourceLiveUpdate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("16")]
        public int MaxMRU {
            get {
                return ((int)(this["MaxMRU"]));
            }
            set {
                this["MaxMRU"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("500")]
        public int MaxPaginationPerPage {
            get {
                return ((int)(this["MaxPaginationPerPage"]));
            }
            set {
                this["MaxPaginationPerPage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1500")]
        public int MaxRomCountBeforeSettingPagination {
            get {
                return ((int)(this["MaxRomCountBeforeSettingPagination"]));
            }
            set {
                this["MaxRomCountBeforeSettingPagination"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ValidROMTypes {
            get {
                return ((string)(this["ValidROMTypes"]));
            }
            set {
                this["ValidROMTypes"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string lastDirectoryUserSelected {
            get {
                return ((string)(this["lastDirectoryUserSelected"]));
            }
            set {
                this["lastDirectoryUserSelected"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("roms")]
        public string romSubFolderName {
            get {
                return ((string)(this["romSubFolderName"]));
            }
            set {
                this["romSubFolderName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("images")]
        public string imageSubFolderName {
            get {
                return ((string)(this["imageSubFolderName"]));
            }
            set {
                this["imageSubFolderName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool PreviewOverBoxArt {
            get {
                return ((bool)(this["PreviewOverBoxArt"]));
            }
            set {
                this["PreviewOverBoxArt"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool AutoConvertImageFilesToJpg {
            get {
                return ((bool)(this["AutoConvertImageFilesToJpg"]));
            }
            set {
                this["AutoConvertImageFilesToJpg"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool AutoDeleteOldImageFileAfterConversion {
            get {
                return ((bool)(this["AutoDeleteOldImageFileAfterConversion"]));
            }
            set {
                this["AutoDeleteOldImageFileAfterConversion"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool WatchImageFolderForChanges {
            get {
                return ((bool)(this["WatchImageFolderForChanges"]));
            }
            set {
                this["WatchImageFolderForChanges"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool OnlyImportConvertedImageFiles {
            get {
                return ((bool)(this["OnlyImportConvertedImageFiles"]));
            }
            set {
                this["OnlyImportConvertedImageFiles"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int LoggingLevel {
            get {
                return ((int)(this["LoggingLevel"]));
            }
            set {
                this["LoggingLevel"] = value;
            }
        }
    }
}

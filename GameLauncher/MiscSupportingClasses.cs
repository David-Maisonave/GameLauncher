﻿using Microsoft.Data.Sqlite;
using Microsoft.WindowsAPICodePack.Taskbar;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLauncher
{
    #region Generic classes
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
    public class CursorWait : IDisposable
    {
        private TaskbarManager taskbarProcess = null;
        private System.Windows.Forms.ProgressBar miniProgressBar = null;
        public CursorWait(bool appStarting = false, bool applicationCursor = false, TaskbarManager taskbarProcess = null) => Init(appStarting, applicationCursor, taskbarProcess, null);
        public CursorWait(TaskbarManager taskbarProcess, bool appStarting = false, bool applicationCursor = false) => Init(appStarting, applicationCursor, taskbarProcess, null);
        public CursorWait(System.Windows.Forms.ProgressBar miniProgressBar, bool appStarting = false, bool applicationCursor = false) => Init(appStarting, applicationCursor, null, miniProgressBar);
        public CursorWait(TaskbarManager taskbarProcess, System.Windows.Forms.ProgressBar miniProgressBar, bool appStarting = false, bool applicationCursor = false) => Init(appStarting, applicationCursor, taskbarProcess, miniProgressBar);
        private void Init(bool appStarting, bool applicationCursor, TaskbarManager taskbarProcess, System.Windows.Forms.ProgressBar miniProgressBar)
        {
            this.taskbarProcess = taskbarProcess;
            this.miniProgressBar = miniProgressBar;
            if (miniProgressBar != null)
                miniProgressBar.Show();
            Cursor.Current = appStarting ? Cursors.AppStarting : Cursors.WaitCursor;
            if (applicationCursor)
                Application.UseWaitCursor = true;
        }
        public void Dispose()
        {
            Cursor.Current = Cursors.Default;
            Application.UseWaitCursor = false;
            if (taskbarProcess != null)
                taskbarProcess.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.NoProgress);
            if (miniProgressBar != null)
                miniProgressBar.Hide();
        }
    }
    #endregion /////////////////////////////////////////////////////////////////////////////////
    #region Data packets supporting classes
    public class SystemScanTaskData
    {
        public string systemName;
        public string emulatorDir;
        public EmulatorExecutables emulatorExecutables;
    }
    public class PreviousCollectedImages
    {
        public ImageList list1;
        public ImageList list2;
    }
    public class PreviousCollectedData : PreviousCollectedImages
    {
        public List<Rom> roms;
    }
    public enum DeleteDuplicateBy
    {
        None = 0,
        DuplicateChecksum,
        DuplicateCompressChecksum,
        DuplicateTitleInAnySystem,
        DuplicateTitleInSameSystem,
        DuplicateNameSimplifiedInAnySystem,
        DuplicateNameSimplifiedInSameSystem,
        DuplicateNameOrgInAnySystem,
        DuplicateNameOrgInSameSystem,
        DuplicateCompressedInAnySystem,
        DuplicateCompressedInSameSystem,
    }
    public class InitializeRomsInDatabaseForSystem_Arg
    {
        public string emulatorDir;
        public EmulatorExecutables emulatorExecutables;
        public bool isMainThread;
        public bool scanImageDir;
        public bool hideGroup;
    }
    #endregion /////////////////////////////////////////////////////////////////////////////////
}
//void deleteMeCodeTempForTesting(string key, string name, int value)
//{
//    ListViewItem lvi = new ListViewItem();
//    System.Windows.Forms.ProgressBar pb = new System.Windows.Forms.ProgressBar();
//    lvi.SubItems[0].Text = name;
//    lvi.SubItems.Add(name);
//    lvi.SubItems.Add(value.ToString());
//    lvi.SubItems.Add("---");
//    lvi.SubItems.Add(key);            // LV has 3 cols; this wont show
//    lvi.BackColor = SystemColors.ScrollBar;
//    lvi.SubItems[1].Text = value.ToString();
//    lvi.SubItems[2].Text = key;
//    ListViewSubItem lvsi = new ListViewSubItem();
//    lvsi.Text = key;
//    lvi.SubItems.Add(lvsi);
//    listView_ProgressBarList.Items.Add(lvi);

//    Rectangle r = lvi.SubItems[2].Bounds;
//    pb.SetBounds(r.X, r.Y, r.Width, r.Height);
//    pb.Minimum = 1;
//    pb.Maximum = 10;
//    pb.Value = value;
//    pb.Name = key;
//    pb.Visible = true;
//    listView_ProgressBarList.Controls.Add(pb);
//    pb.Parent = listView_ProgressBarList;
//    pb.Show();
//    pb.Update();
//    listView_ProgressBarList.Update();
//}
//public System.Windows.Forms.ProgressBar LvAddProgB(System.Windows.Forms.ListView LV, int LVII, int LVColI, string lvName)
//{
//    Rectangle SizeR = default(Rectangle);
//    System.Windows.Forms.ProgressBar ProgBar = new System.Windows.Forms.ProgressBar();

//    SizeR = LV.Items[LVII].Bounds;
//    SizeR.Width = LV.Columns[LVColI].Width;
//    if (LVColI > 0)
//    {
//        SizeR.X = SizeR.X + LV.Columns[LVColI - 1].Width;
//    }
//    ProgBar.Parent = LV;
//    ProgBar.Name = lvName;
//    ProgBar.SetBounds(SizeR.X, SizeR.Y, SizeR.Width, SizeR.Height);
//    ProgBar.Visible = true;
//    ProgBar.Maximum = 1000;
//    ProgBar.Step = 1;

//    return ProgBar;
//}
//void deleteMeCodeTempForTesting()
//{
//    DisplayOrHideProgressGroup(true);
//    //deleteMeCodeTempForTesting("A", "Ziggy", 1);
//    //deleteMeCodeTempForTesting("B", "Zacky", 2);
//    //deleteMeCodeTempForTesting("C", "Zoey", 3);
//    //deleteMeCodeTempForTesting("D", "Zeke", 4);
//    for (int x = 0; x < 3; ++x)
//    {
//        ListViewItem item = new ListViewItem();
//        item.Text = "d.Name";
//        item.SubItems.Add("                 ");
//        listView_ProgressBarList.Items.Add(item);
//        listView_ProgressBarList.Controls.Add(LvAddProgB(listView_ProgressBarList, item.Position.X + item.Bounds.Width, item.Position.Y, "Lview" + x.ToString()));
//    }
//}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace ModbusWPFproject
{
    /// <summary>
    /// Логика взаимодействия для WP_Editor_DirectoryName_Input.xaml
    /// </summary>
    public partial class WP_Editor_DirectoryName_Input : Window
    {
        WP_Editor_VM vm_editor = new WP_Editor_VM();
        string fullpath = string.Empty;

        public WP_Editor_DirectoryName_Input(string path)
        {
            InitializeComponent();

            this.DataContext = vm_editor;
            fullpath = path;

            vm_editor.WP_Editor_Directoryname_Input_Textblock = "#000000";
            //WP_Editor_Filename_Input_Grid.PreviewMouseLeftButtonDown += (s, e) => DragMove();
        }

        private void WP_Editor_Directoryname_Input_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (vm_editor.WP_Editor_Directoryname_Input_Name != null)
                {
                    Directory.CreateDirectory(System.IO.Path.Combine(fullpath, vm_editor.WP_Editor_Directoryname_Input_Name));
                    
                    this.Close();
                }
                else vm_editor.WP_Editor_Directoryname_Input_Textblock = "#FF0000";
            }
            catch { vm_editor.WP_Editor_Directoryname_Input_Textblock = "#FF0000"; }
        }

        private void WP_Editor_Directoryname_Input_Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WP_Editor_Directoryname_Input_Textbox_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("FreeVK");
            if (pname.Length == 0)
            {
                string pathToExe = "FreeVK.exe";
                Process process = new Process();
                process.StartInfo.FileName = pathToExe;
                process.Start();
            }
            else
            {

            }
        }

        public class WP_Editor_VM : INotifyPropertyChanged
        {
            private string? _WP_Editor_Directoryname_Input_Name;
            private object? _WP_Editor_Directoryname_Input_Textblock;

            public string WP_Editor_Directoryname_Input_Name
            {
                get
                {
                    return _WP_Editor_Directoryname_Input_Name!;
                }
                set
                {
                    _WP_Editor_Directoryname_Input_Name = value;
                    NotifyPropertyChanged("WP_Editor_Directoryname_Input_Name");
                }
            }

            public object WP_Editor_Directoryname_Input_Textblock
            {
                get
                {
                    return _WP_Editor_Directoryname_Input_Textblock!;
                }
                set
                {
                    _WP_Editor_Directoryname_Input_Textblock = value;
                    NotifyPropertyChanged("WP_Editor_Directoryname_Input_Textblock");
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            private void NotifyPropertyChanged(String propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

        }
    }
}

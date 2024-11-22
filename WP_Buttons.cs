using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Reflection.PortableExecutable;
using System.Windows.Data;
using System.Timers;
using System.Windows.Threading;
using System.Threading;
using System.Reflection.Emit;
using System.IO.Ports;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using EasyModbus;
using System.Diagnostics;
using System.Globalization;
using static EasyModbus.ModbusClient;
using System.Runtime.InteropServices;
using Gu.Wpf.DataGrid2D;
using static MaterialDesignThemes.Wpf.Theme;
using static ModbusWPFproject.Window1;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Documents;
using System.Windows.Media.Effects;
using Microsoft.Xaml.Behaviors;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;
using System.Collections.ObjectModel;

namespace ModbusWPFproject
{
    public partial class Window1 : Window
    {
        Stopwatch stopwatch = new Stopwatch();
        private void WP_Button_SwitchOn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            stopwatch.Restart();            
        }

        private void WP_Button_SwitchOn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds >= 10)
            {
                if (flag_Stanok_SwitchOn == false)
                {
                    flag_Stanok_SwitchOn = true;
                    vm.Textblock_Button_SwitchOn = "ВЫКЛЮЧИТЬ СТАНОК";
                }
                else
                {
                    flag_Stanok_SwitchOn = false;
                    vm.Textblock_Button_SwitchOn = "ВКЛЮЧИТЬ СТАНОК";
                }
            }
        }
        public void DIAG_Gen_txt_button_click(object sender, RoutedEventArgs e)
        {
            /*ModClientGenerator.WriteSingleRegister(108,Convert.ToInt32(DIAG_Gen_txt.Text));*/
            send_data[8] = Convert.ToInt32(DIAG_Gen_txt.Text);
        }
        private void DIAG_Menu_Button_Sensors_PreviewTouchDown(object sender, MouseButtonEventArgs e)
        {
            vm.DIAG_Sensors_Menu_Visibility = Visibility.Visible;
            vm.DIAG_Gen_Menu_Visibility = Visibility.Hidden;
            
            DIAG_DG2_Update();
        }
        private void DIAG_Menu_Button_Generator_PreviewTouchDown(object sender, MouseButtonEventArgs e)
        {
            vm.DIAG_Sensors_Menu_Visibility = Visibility.Hidden;
            vm.DIAG_Gen_Menu_Visibility = Visibility.Visible;
        }
        private void DIAG_Menu_Button_CloseApp_PreviewTouchDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }
        public void Combobox_ModeSettings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selected = Combobox_ModeSettings.SelectedIndex;

            if (selected != -1)
            {
                Datagrid_ModeGenerator_Update(selected);
            }
        }
        private void Datagrid_Settings_Mode_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            Open_Virtual_Keyboard();
        }
        private void DataGrid_Settings_Mode_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int index = Datagrid_Settings_Mode.Items.IndexOf(Datagrid_Settings_Mode.CurrentItem);
            
            int selectedCombo = Combobox_ModeSettings.SelectedIndex;

            var editedCell = e.EditingElement as System.Windows.Controls.TextBox;

            bool flag_status = false;
            
            short newValue = 0;

            FrameworkElement element = e.Column.GetCellContent(e.Row);

            try
            {
                newValue = Convert.ToInt16(editedCell.Text);
            }
            catch
            {
                newValue = 0;
                (element.Parent as DataGridCell).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFAFAC");
                WP_Show_Msg_Window("value_wrong");
                return;
            }

            if (editedCell != null)
            {
                switch (index)
                {
                    case 0:
                        if (newValue >= 0 & newValue <= 250)
                        {
                            modeGenerator.listModeGenerators[selectedCombo].ustavkaVolts = newValue;
                            SendModeDataToController();
                            flag_status = true;
                        }
                        else
                        {
                            flag_status = false;
                            WP_Show_Msg_Window("ustavka_wrong");
                        }
                        break;
                    case 1:
                        if (newValue >= 1 & newValue <= 500)
                        {
                            modeGenerator.listModeGenerators[selectedCombo].frequancyPulse = newValue;

                            flag_status = true;
                        }
                        else
                        {
                            flag_status = false;
                            WP_Show_Msg_Window("frequency_wrong");
                        }
                        break;
                    case 2:
                        if (newValue >= 1.05 & newValue <= 6.6)
                        {
                            modeGenerator.listModeGenerators[selectedCombo].dutyCycle = newValue;
                            flag_status = true;
                        }
                        else
                        {
                            flag_status = false;
                            WP_Show_Msg_Window("dutycycle_wrong");
                        }
                        break;
                    case 3:
                        if (newValue >= 0 & newValue <= 100)
                        {
                            modeGenerator.listModeGenerators[selectedCombo].operatingCurrent = newValue;
                            flag_status = true;
                        }
                        else
                        {
                            flag_status = false;
                            WP_Show_Msg_Window("operatingcurrent_wrong");
                        }
                        break;
                    case 4:
                        if (newValue >= 0 & newValue <= 15)
                        {
                            modeGenerator.listModeGenerators[selectedCombo].arsonCurrent = newValue;
                            flag_status = true;
                        }
                        else
                        {
                            flag_status = false;
                            WP_Show_Msg_Window("arsonCurrent_wrong");
                        }
                        break;
                    case 5:
                        if (newValue >= 0 & newValue <= 20)
                        {
                            modeGenerator.listModeGenerators[selectedCombo].proctectiveCurrent = newValue;
                            flag_status = true;
                        }
                        else
                        {
                            flag_status = false;
                            WP_Show_Msg_Window("proctectiveCurrent_wrong");
                        }
                        break;
                    case 6:
                        if (newValue >= 10 & newValue <= 20000)
                        {
                            modeGenerator.listModeGenerators[selectedCombo].packageDuration = newValue;
                            flag_status = true;
                        }
                        else
                        {
                            flag_status = false;
                            WP_Show_Msg_Window("packageDuration_wrong");
                        }
                        break;
                    case 7:
                        if (newValue >= 10 & newValue <= 20000)
                        {
                            modeGenerator.listModeGenerators[selectedCombo].pauseDuration = newValue;
                            flag_status = true;
                        }
                        else
                        {
                            flag_status = false;
                            WP_Show_Msg_Window("pauseDuration_wrong");
                        }
                        break;
                    case 8:
                        if (newValue >= 0 & newValue <= 100)
                        {
                            modeGenerator.listModeGenerators[selectedCombo].titaniumPulseDuration = newValue;
                            flag_status = true;
                        }
                        else
                        {
                            flag_status = false;
                            WP_Show_Msg_Window("titaniumPulseDuration_wrong");
                        }
                        break;
                    case 9:
                        if (newValue >= 0 & newValue <= 3)
                        {
                            modeGenerator.listModeGenerators[selectedCombo].capacitorCapacity = newValue;
                            flag_status = true;
                        }
                        else
                        {
                            flag_status = false;
                            WP_Show_Msg_Window("capacitorCapacity_wrong");
                        }
                        break;
                    case 10:
                        if (newValue >= 10 & newValue <= 20000)
                        {
                            modeGenerator.listModeGenerators[selectedCombo].relaxationDistance = newValue;
                            SendModeDataToController();
                            flag_status = true;
                        }
                        else
                        {
                            flag_status = false;
                            WP_Show_Msg_Window("relaxationDistance_wrong");
                        }
                        break;                    
                    case 11:
                        if (newValue >= 10 & newValue <= 20000)
                        {
                            modeGenerator.listModeGenerators[selectedCombo].relaxationDuration = newValue;
                            SendModeDataToController();
                            flag_status = true;
                        }
                        else
                        {
                            flag_status = false;
                            WP_Show_Msg_Window("relaxationDuration_wrong");
                        }
                        break;
                    case 12:
                        if (newValue >= 10 & newValue <= 20000)// <<<<<<<<<<<<<<<<<<<<<<<<<<<< - указать пределы нормальные
                        {
                            modeGenerator.listModeGenerators[selectedCombo].gainFactor = newValue;
                            SendModeDataToController();
                            flag_status = true;
                        }
                        else
                        {
                            flag_status = false;
                            WP_Show_Msg_Window("gainFactor_wrong");
                        }
                        break;
                }

                SendReadedModesToGenerator();
                Datagrid_ModeGeneratorRightNow_Update();

                if (flag_status)
                {
                    (element.Parent as DataGridCell).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#B1E9B4");
                }
                else
                {
                    (element.Parent as DataGridCell).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFAFAC");
                }
            }
        }

        public void btnLoadPRG_Click(object sender, RoutedEventArgs e)
        {
            DisplayFilesInDirectory(default_path, WP_LoadPRGPreview);
            WP_LoadPRGPreview_RTB.Document.Blocks.Clear();

            vm.WP_LoadPRG_Open = true;
            vm.WP_General_BlurRadius = 20;
            vm.WP_Window_ChoosePRG_Visibility = Visibility.Visible;
        }        
        public void WP_LoadPRG_Close_Click(object sender, RoutedEventArgs e)
        {
            vm.WP_LoadPRG_Open = false;
            vm.WP_General_BlurRadius = 0;
            vm.WP_Window_ChoosePRG_Visibility = Visibility.Hidden;
        }        
        public void WP_LoadPRG_Button_Go_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = WP_LoadPRGPreview.SelectedItem as TreeViewItem;

            if (WP_LoadPRGPreview.SelectedItem is TreeViewItem)
            {
                string fullpath = GetPathToSelectedItem(selectedItem) + ".txt";

                OpenData((string)selectedItem.Header,fullpath);
            }

            vm.WP_LoadPRG_Open = false;
            vm.WP_General_BlurRadius = 0;
            vm.WP_Window_ChoosePRG_Visibility = Visibility.Hidden;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if ((vm.Labels_State == "ПАУЗА") | (vm.Labels_State == "РАБОТА"))
            {
                index = 0;

                send_data[3] = 3;
                send_data[4] = 2;

                flag_end_of_data = false;
                if (flag_go_cmd) flag_go_cmd = false;
            }
        }
        private void SliderSupply_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            send_data[5] = Convert.ToInt32(((Slider)sender).Value);
        }

        private void SliderSetting_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            send_data[7] = Convert.ToInt32(((Slider)sender).Value);
        }

        private void Buttons_Colors_Reset()
        {
            flag_Button_Mode_Auto_Pressed = false;
            flag_Button_Mode_1FRAME_Pressed = false;
            flag_Button_Mode_1AXIS_Pressed = false;
            flag_Button_Mode_Manual_Pressed = false;
            flag_Button_Mode_Measure_Pressed = false;
            flag_Button_Mode_Editor_Pressed = false;
            flag_Button_Mode_Settings_Pressed = false;
            flag_Button_Mode_Diagnostics_Pressed = false;

            vm.button_mode_auto_color = "#FF76ABAE";
            vm.button_mode_auto_borderbrush = "#FF76ABAE";

            vm.button_1frame_color = "#FF76ABAE";
            vm.button_1frame_color_borderbrush = "#FF76ABAE";

            vm.button_1axis_color = "#FF76ABAE";
            vm.button_1axis_color_borderbrush = "#FF76ABAE";

            vm.button_manual_color = "#FF76ABAE";
            vm.button_manual_color_borderbrush = "#FF76ABAE";

            vm.button_measure_color = "#FF76ABAE";
            vm.button_measure_color_borderbrush = "#FF76ABAE";

            vm.button_findnull_color = "#FF76ABAE";
            vm.button_findnull_color_borderbrush = "#FF76ABAE";

            vm.button_editor_color = "#FF639194";
            vm.button_settings_color = "#FF639194";

            vm.button_editor_color_borderbrush = "#FF639194";
            vm.button_settings_color_borderbrush = "#FF639194";
        }
        private void Button_mode_AUTO_Click(object sender, RoutedEventArgs e)
        {
            Buttons_Colors_Reset();

            if (flag_Button_Mode_Auto_Pressed == false)
            {
                vm.Grid_Bottom_CMD_GEN_Visibility = Visibility.Visible;
                vm.Grid_Upper_AxisNulling_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_Manual_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_MDI_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_FINDNULL_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_MEASURE_Visibility = Visibility.Hidden;

                vm.WorkPanel_General_Visible = Visibility.Visible;
                vm.WorkPanel_Editor_Visible = Visibility.Hidden;

                vm.Menu_Manual_Bottom_Visibility = Visibility.Hidden;
                vm.Menu_Bottom_Visibility = Visibility.Visible;

                vm.Menu_Editor_Bottom_Visibility = Visibility.Hidden;

                send_data[2] = 2;

                vm.button_mode_auto_borderbrush = "#FFBFB091";
                vm.button_mode_auto_color = "#FFBFB091";

                flag_Button_Mode_Auto_Pressed = true;
            }
            else
            {

                flag_Button_Mode_Auto_Pressed = false;
            }
        }
        private void Button_mode_1FRAME_Click(object sender, RoutedEventArgs e)
        {
            Buttons_Colors_Reset();

            if (flag_Button_Mode_1FRAME_Pressed == false)
            {
                vm.Grid_Bottom_CMD_GEN_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_MDI_Visibility = Visibility.Visible;
                vm.Grid_Bottom_FINDNULL_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_MEASURE_Visibility = Visibility.Hidden;

                vm.Grid_Upper_AxisNulling_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_Manual_Visibility = Visibility.Hidden;

                vm.WorkPanel_General_Visible = Visibility.Visible;
                vm.WorkPanel_Editor_Visible = Visibility.Hidden;

                vm.Menu_Manual_Bottom_Visibility = Visibility.Hidden;
                vm.Menu_Bottom_Visibility = Visibility.Visible;

                vm.Menu_Editor_Bottom_Visibility = Visibility.Hidden;

                send_data[2] = 4;

                vm.button_1frame_color = "#FFBFB091";
                vm.button_1frame_color_borderbrush = "#FFBFB091";

                flag_Button_Mode_1FRAME_Pressed = true;
            }
            else
            {

                flag_Button_Mode_1FRAME_Pressed = false;
            }
        }

        private void Button_mode_1AXIS_Click(object sender, RoutedEventArgs e)
        {
            Buttons_Colors_Reset();

            if (flag_Button_Mode_1AXIS_Pressed == false)
            {
                vm.Grid_Bottom_Manual_Visibility = Visibility.Hidden;
                vm.Grid_Upper_AxisNulling_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_CMD_GEN_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_MDI_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_FINDNULL_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_MEASURE_Visibility = Visibility.Hidden;

                vm.WorkPanel_General_Visible = Visibility.Visible;
                vm.WorkPanel_Editor_Visible = Visibility.Hidden;

                vm.Menu_Manual_Bottom_Visibility = Visibility.Hidden;
                vm.Menu_Bottom_Visibility = Visibility.Visible;

                vm.Menu_Editor_Bottom_Visibility = Visibility.Hidden;

                vm.button_1axis_color = "#FFBFB091";
                vm.button_1axis_color_borderbrush = "#FFBFB091";

                flag_Button_Mode_1AXIS_Pressed = true;
            }
            else
            {
                flag_Button_Mode_1AXIS_Pressed = false;
            }
        }

        private void Button_mode_MANUAL_Click(object sender, RoutedEventArgs e)
        {
            Buttons_Colors_Reset();

            if (flag_Button_Mode_Manual_Pressed == false)
            {
                send_data[2] = 1;

                vm.Grid_Bottom_CMD_GEN_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_Manual_Visibility = Visibility.Visible;
                vm.Grid_Upper_AxisNulling_Visibility = Visibility.Visible;
                vm.Grid_Bottom_MDI_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_FINDNULL_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_MEASURE_Visibility = Visibility.Hidden;

                vm.WorkPanel_General_Visible = Visibility.Visible;
                vm.WorkPanel_Editor_Visible = Visibility.Hidden;

                vm.Menu_Manual_Bottom_Visibility = Visibility.Visible;
                vm.Menu_Bottom_Visibility = Visibility.Hidden;

                vm.Menu_Editor_Bottom_Visibility = Visibility.Hidden;

                vm.button_manual_color = "#FFBFB091";
                vm.button_manual_color_borderbrush = "#FFBFB091";

                flag_Button_Mode_Manual_Pressed = true;
            }
            else
            {
                flag_Button_Mode_Manual_Pressed = false;
            }
        }

        private void Button_mode_MEASURE_Click(object sender, RoutedEventArgs e)
        {
            Buttons_Colors_Reset();

            if (flag_Button_Mode_Measure_Pressed == false)
            {
                vm.Grid_Bottom_CMD_GEN_Visibility = Visibility.Hidden;
                vm.Grid_Upper_AxisNulling_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_Manual_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_MDI_Visibility = Visibility.Hidden;
                vm.WorkPanel_Editor_Visible = Visibility.Hidden;
                vm.Menu_Manual_Bottom_Visibility = Visibility.Hidden;      
                vm.Menu_Editor_Bottom_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_FINDNULL_Visibility = Visibility.Hidden;
                
                vm.Grid_Bottom_MEASURE_Visibility = Visibility.Visible;
                vm.Menu_Bottom_Visibility = Visibility.Visible;
                vm.WorkPanel_General_Visible = Visibility.Visible;

                vm.WP_Measure_FindCorner_Menu_Visibility = Visibility.Hidden;
                vm.WP_Measure_FindAngle_Menu_Visibility = Visibility.Hidden;
                vm.WP_Measure_FindCenter_Menu_Visibility = Visibility.Hidden;

                vm.DataGrid_Measure_Result_Visibility = Visibility.Hidden;

                vm.button_measure_color = "#FFBFB091";
                vm.button_measure_color_borderbrush = "#FFBFB091";

                flag_Button_Mode_Measure_Pressed = true;
            }
            else
            {

                flag_Button_Mode_Measure_Pressed = false;
            }
        }

        private void Button_mode_FINDNULL_Click(object sender, RoutedEventArgs e)
        {
            Buttons_Colors_Reset();

            vm.WorkPanel_General_Visible = Visibility.Visible;
            vm.WorkPanel_Editor_Visible = Visibility.Hidden;

            vm.Grid_Bottom_MDI_Visibility = Visibility.Hidden;
            vm.Menu_Editor_Bottom_Visibility = Visibility.Hidden;
            vm.Menu_Manual_Bottom_Visibility = Visibility.Hidden;
            
            vm.Grid_Bottom_CMD_GEN_Visibility = Visibility.Hidden;
            vm.Grid_Upper_AxisNulling_Visibility = Visibility.Hidden;
            vm.Grid_Bottom_Manual_Visibility = Visibility.Hidden;

            vm.Grid_Bottom_FINDNULL_Visibility = Visibility.Visible;
            vm.Grid_Bottom_MEASURE_Visibility = Visibility.Hidden;
            vm.Menu_Bottom_Visibility = Visibility.Visible;

            if (flag_Button_Mode_FindNull_Pressed == false)
            {       
                send_data[2] = 3;
                flag_go_home = true;

                vm.button_findnull_color = "#FFBFB091";
                vm.button_findnull_color_borderbrush = "#FFBFB091";

                flag_Button_Mode_FindNull_Pressed = true;
            }
            else
            {
                flag_go_home = false;
                vm.Color_Y = Brushes.Transparent;
                vm.Color_Z = Brushes.Transparent;
                vm.Color_X = Brushes.Transparent;
                vm.button_START_enabled = false;

                flag_Button_Mode_FindNull_Pressed = false;
            }
        }
        private void Button_EDITOR_Click(object sender, RoutedEventArgs e)
        {
            Buttons_Colors_Reset();

            TreeViewUtils.ClearTreeViewSelection(WP_fileTreeView);

            if (flag_Button_Mode_Editor_Pressed == false)
            {
                vm.Grid_Bottom_CMD_GEN_Visibility = Visibility.Visible;
                vm.Grid_Upper_AxisNulling_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_Manual_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_MDI_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_FINDNULL_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_MEASURE_Visibility = Visibility.Hidden;

                vm.WorkPanel_General_Visible = Visibility.Hidden;
                vm.WorkPanel_Editor_Visible = Visibility.Visible;

                vm.Menu_Manual_Bottom_Visibility = Visibility.Hidden;

                vm.Menu_Manual_Bottom_Visibility = Visibility.Visible;
                vm.Menu_Bottom_Visibility = Visibility.Hidden;

                vm.Menu_Editor_Bottom_Visibility = Visibility.Visible;

                //vm.button_editor_color = "#FFBFB091";
                //vm.button_editor_color_borderbrush = "#FFBFB091";

                flag_Button_Mode_Editor_Pressed = true;

                DisplayFilesInDirectory(default_path, WP_fileTreeView);
                
                WP_RichTextBoxEditor.Document.Blocks.Clear();
            }
            else
            {

                flag_Button_Mode_Editor_Pressed = false;
            }
        }

        private void Button_SETTINGS_Click(object sender, RoutedEventArgs e)
        {
            Buttons_Colors_Reset();

            if (flag_Button_Mode_Settings_Pressed == false)
            {
/*                vm.Grid_Bottom_CMD_GEN_Visibility = Visibility.Visible;
                vm.Grid_Upper_AxisNulling_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_Manual_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_MDI_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_FINDNULL_Visibility = Visibility.Hidden;
                vm.Grid_Bottom_MEASURE_Visibility = Visibility.Hidden;*/

                //vm.WorkPanel_General_Visible = Visibility.Hidden;
                //vm.WorkPanel_Editor_Visible = Visibility.Hidden;

                //vm.Menu_Manual_Bottom_Visibility = Visibility.Visible;
                
                //vm.Menu_Bottom_Visibility = Visibility.Hidden;
                //vm.Menu_Editor_Bottom_Visibility = Visibility.Hidden;
                //vm.button_settings_color = "#FFBFB091";
                //vm.button_settings_color_borderbrush = "#FFBFB091";

                vm.WP_Settings_Open = true;
                vm.WP_DIAG_Open = false;
                vm.WP_General_BlurRadius = 20;
                vm.WP_Window_ChoosePRG_Visibility = Visibility.Hidden;
                vm.WP_Window_Settings_Visibility = Visibility.Visible;
                vm.WP_Window_Diag_Visibility = Visibility.Hidden;

                flag_Button_Mode_Settings_Pressed = true;

            }
            else
            {

                flag_Button_Mode_Settings_Pressed = false;
            }
        }

    public void WP_Settings_Close_Click(object sender, RoutedEventArgs e)
    {
        vm.WP_Settings_Open = false;
        vm.WP_General_BlurRadius = 0;
        vm.WP_Window_Settings_Visibility = Visibility.Hidden;
    }
    public void WP_DIAG_Close_Click(object sender, RoutedEventArgs e)
    {
        vm.WP_DIAG_Open = false;
        vm.WP_General_BlurRadius = 0;
        vm.WP_Window_Diag_Visibility = Visibility.Hidden;
    }
        public void Btn_Close_App(object sender, RoutedEventArgs e)
        {
            ModClientGenerator.Disconnect();
            ModClient.Disconnect();
            Environment.Exit(0);
        }
        private void Button_DIAG_Click(object sender, RoutedEventArgs e)
        {
            Buttons_Colors_Reset();

            if (flag_Button_Mode_Diagnostics_Pressed == false)
            {
                /*                vm.Grid_Bottom_CMD_GEN_Visibility = Visibility.Visible;
                                vm.Grid_Upper_AxisNulling_Visibility = Visibility.Hidden;
                                vm.Grid_Bottom_Manual_Visibility = Visibility.Hidden;
                                vm.Grid_Bottom_MDI_Visibility = Visibility.Hidden;
                                vm.Grid_Bottom_FINDNULL_Visibility = Visibility.Hidden;
                                vm.Grid_Bottom_MEASURE_Visibility = Visibility.Hidden;

                                vm.WorkPanel_General_Visible = Visibility.Hidden;
                                vm.WorkPanel_Editor_Visible = Visibility.Hidden;

                                vm.Menu_Manual_Bottom_Visibility = Visibility.Visible;

                                vm.Menu_Bottom_Visibility = Visibility.Hidden;
                                vm.Menu_Editor_Bottom_Visibility = Visibility.Hidden;

                                vm.WP_Window_ChoosePRG_Visibility = Visibility.Hidden;

                                vm.WP_Window_Settings_Visibility = Visibility.Hidden;*/

                //vm.button_settings_color = "#FFBFB091";
                //vm.button_settings_color_borderbrush = "#FFBFB091";

                vm.WP_DIAG_Open = true;
                vm.WP_Settings_Open = false;
                vm.WP_General_BlurRadius = 20;
                vm.WP_Window_ChoosePRG_Visibility = Visibility.Hidden;
                vm.WP_Window_Settings_Visibility = Visibility.Hidden;
                vm.WP_Window_Diag_Visibility = Visibility.Visible;

                flag_Button_Mode_Diagnostics_Pressed = true;
            }
            else
            {
                flag_Button_Mode_Diagnostics_Pressed = false;
            }
        }
        private void Button_Measure_FindCorner_Click(object sender, RoutedEventArgs e)
        {
            vm.WP_Measure_FindAngle_Menu_Visibility = Visibility.Hidden;
            vm.WP_Measure_FindCenter_Menu_Visibility= Visibility.Hidden;
            vm.WP_Measure_FindCorner_Menu_Visibility = Visibility.Visible;
            vm.DataGrid_Measure_Result_Visibility = Visibility.Hidden;
        }
        private void Button_Measure_FindCentrCircle_Click(object sender, RoutedEventArgs e)
        {
            vm.WP_Measure_FindAngle_Menu_Visibility = Visibility.Hidden;
            vm.WP_Measure_FindCenter_Menu_Visibility = Visibility.Visible;
            vm.WP_Measure_FindCorner_Menu_Visibility = Visibility.Hidden;
            vm.DataGrid_Measure_Result_Visibility = Visibility.Hidden;
        }
        private void Button_Measure_FindAngle_Click(object sender, RoutedEventArgs e)
        {
            vm.WP_Measure_FindAngle_Menu_Visibility = Visibility.Visible;
            vm.WP_Measure_FindCenter_Menu_Visibility = Visibility.Hidden;
            vm.WP_Measure_FindCorner_Menu_Visibility = Visibility.Hidden;
            vm.DataGrid_Measure_Result_Visibility = Visibility.Hidden;
        }
        
        private void WP_Editor_Button_CreateFile_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = WP_fileTreeView.SelectedItem as TreeViewItem;

            string fileDirectory,fullpath;

            try
            {
                if (selectedItem != null)
                {
                    fullpath = GetPathToSelectedItem(selectedItem);

                    WP_Editor_Filename_Input wp_editor_filename = new WP_Editor_Filename_Input(fullpath);
                    wp_editor_filename.ShowDialog();
                }
                else
                {
                    WP_Editor_Filename_Input wp_editor_filename = new WP_Editor_Filename_Input(default_path);
                    wp_editor_filename.ShowDialog();
                }
                

                WP_Show_Msg_Window("saved");

                RefreshTreeView();
                WP_RichTextBoxEditor.Document.Blocks.Clear();
            }
            catch
            {
                WP_Show_Msg_Window("error");
            }
        }
        private void WP_Editor_Button_CopyFiles_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = WP_fileTreeView.SelectedItem as TreeViewItem;

            try
            {
                if (WP_fileTreeView.SelectedItem is TreeViewItem)
                {
                    string fullpath = GetPathToSelectedItem(selectedItem);

                    string fileContent = File.ReadAllText(fullpath + ".txt");
                    string fileDirectory = Path.GetDirectoryName(fullpath);

                    WP_Editor_CopyingFiles wp_editor_filename = new WP_Editor_CopyingFiles(fullpath + ".txt", fileDirectory);
                    wp_editor_filename.ShowDialog();

                    WP_Show_Msg_Window("saved");

                    RefreshTreeView();
                    WP_RichTextBoxEditor.Document.Blocks.Clear();
                }
            }
            catch
            {
                WP_Show_Msg_Window("error");
            }
        }
        private void WP_Editor_Button_CreateDirectory_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = WP_fileTreeView.SelectedItem as TreeViewItem;
            string fullpath;

            try
            {
                if (selectedItem == null)
                {
                    WP_Editor_DirectoryName_Input wp_editor_directoryname = new WP_Editor_DirectoryName_Input(default_path) { Owner = this };
                    wp_editor_directoryname.ShowDialog();
                }
                else
                {
                    if (WP_fileTreeView.SelectedItem is TreeViewItem)
                    {
                        fullpath = GetPathToSelectedItem(selectedItem);
                        WP_Editor_DirectoryName_Input wp_editor_directoryname = new WP_Editor_DirectoryName_Input(default_path) { Owner = this };
                        wp_editor_directoryname.ShowDialog();
                    }
                }     
                
                WP_Show_Msg_Window("directorycreated");

                RefreshTreeView();
                WP_RichTextBoxEditor.Document.Blocks.Clear();
            }
            catch
            {
                WP_Show_Msg_Window("error");
            }
        }        
        private void WP_Editor_Button_RemoveDirectory_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = WP_fileTreeView.SelectedItem as TreeViewItem;

            try
            {
                if (WP_fileTreeView.SelectedItem is TreeViewItem)
                {
                    string fullpath = GetPathToSelectedItem(selectedItem);
                   
                    string selectedName = selectedItem.Header.ToString();

                    Directory.Delete(fullpath, true);

                    WP_Show_Msg_Window("directorydeleted");

                    RefreshTreeView();
                    WP_RichTextBoxEditor.Document.Blocks.Clear();
                }
            }
            catch 
            {
                WP_Show_Msg_Window("error");
            }
        }
        private void WP_Editor_Button_RemoveFile_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = WP_fileTreeView.SelectedItem as TreeViewItem;

            try
            {
                if (WP_fileTreeView.SelectedItem is TreeViewItem)
                {
                    string fullpath = GetPathToSelectedItem(selectedItem);
                    
                    if (selectedItem == null) return;

                    string selectedName = selectedItem.Header.ToString();

                    File.Delete(fullpath + ".txt");

                    WP_Show_Msg_Window("filedeleted");

                    RefreshTreeView();
                    WP_RichTextBoxEditor.Document.Blocks.Clear();
                }
            }
            catch
            {
                WP_Show_Msg_Window("error");
            }
        }
        private void WP_Measure_Go_Click(object sender, RoutedEventArgs e)
        {
            vm.DataGrid_Measure_Result_Visibility = Visibility.Visible;
        }

        private void WP_Editor_Save_File_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = WP_fileTreeView.SelectedItem as TreeViewItem;

            try 
            {
                if (WP_fileTreeView.SelectedItem is TreeViewItem)
                {
                    string fullpath = GetPathToSelectedItem(selectedItem);

                    string selectedName = selectedItem.Header.ToString();

                    string richText = new TextRange(WP_RichTextBoxEditor.Document.ContentStart, WP_RichTextBoxEditor.Document.ContentEnd).Text;

                    File.WriteAllText(fullpath + ".txt", richText);

                    WP_Show_Msg_Window("saved");

                    RefreshTreeView();
                }
            }
            catch
            {
                WP_Show_Msg_Window("error");
            }
        }
        private string GetPathToSelectedItem(TreeViewItem item)
        {
            string path = item.Header.ToString();
            string path2 = String.Empty;

            while (item.Parent is TreeViewItem parent)
            {
                path = Path.Combine((string)parent.Header, path);
                path2 = Path.GetFullPath(path);

                item = parent;
            }

            return path2;
        }
    
    private void LoadPRGTreeView_SelectedItem_Touch(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = WP_LoadPRGPreview.SelectedItem as TreeViewItem;

            try
            {
                if (selectedItem != null)
                {
                    string fullpath = GetPathToSelectedItem(selectedItem);
                    string fileDirectory = Path.GetDirectoryName(fullpath);

                    string fileContent = File.ReadAllText(Path.Combine(fileDirectory,(string)selectedItem.Header + ".txt"));

                    FlowDocument flowDoc = new FlowDocument(new Paragraph(new Run(fileContent)));
                    
                    WP_LoadPRGPreview_RTB.Document = flowDoc;
                }
            }
            catch
            {
                if ((string)selectedItem.Header == "Главная директория") { WP_LoadPRGPreview_RTB.Document.Blocks.Clear(); return; }

                if (Directory.Exists(Path.Combine(default_path, (string)selectedItem.Header)) == true)
                {
                    WP_LoadPRGPreview_RTB.Document.Blocks.Clear();
                }

                else
                {
                    WP_Show_Msg_Window("error");
                }
            }
        }
        private void fileTreeView_SelectedItem_Touch(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = WP_fileTreeView.SelectedItem as TreeViewItem;

            try
            {
                if (selectedItem != null)
                {
                    string fullpath = GetPathToSelectedItem(selectedItem);
                    string fileDirectory = Path.GetDirectoryName(fullpath);

                    string fileContent = File.ReadAllText(Path.Combine(fileDirectory, (string)selectedItem.Header + ".txt"));

                    FlowDocument flowDoc = new FlowDocument(new Paragraph(new Run(fileContent)));

                    WP_RichTextBoxEditor.Document = flowDoc;
                }

            }
            catch
            {
                if ((string)selectedItem.Header == "Главная директория") { WP_RichTextBoxEditor.Document.Blocks.Clear(); return; }

                if (Directory.Exists(Path.Combine(default_path,(string)selectedItem.Header)) == true)
                {
                    WP_RichTextBoxEditor.Document.Blocks.Clear();
                }
                else
                {
                   
                    WP_Show_Msg_Window("error");            
                }
            }
        }
        private void WP_Editor_Button_RenameFile_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = WP_fileTreeView.SelectedItem as TreeViewItem;

            if (WP_fileTreeView.SelectedItem is TreeViewItem)
            {            
                string fullpath = GetPathToSelectedItem(selectedItem);
                string fileDirectory = Path.GetDirectoryName(fullpath);

                WP_Editor_RenameFile window_rename = new WP_Editor_RenameFile((string)selectedItem.Header, fileDirectory);
                window_rename.ShowDialog();

                WP_Show_Msg_Window("renamed");
            }

            RefreshTreeView();
            WP_RichTextBoxEditor.Document.Blocks.Clear();

        }
        public void RefreshTreeView()
        {
            
            WP_fileTreeView.Items.Clear();

            DirectoryInfo directoryInfo = new DirectoryInfo(default_path);

            if (directoryInfo.Exists)
            {
                WP_fileTreeView.Items.Add(CreateDirectoryNode(directoryInfo));
            }
        }
        
        public void DisplayFilesInDirectory(string path, TreeView treeView)
        {
            treeView.Items.Clear();

            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            if (directoryInfo.Exists)
            {
                treeView.Items.Add(CreateDirectoryNode(directoryInfo));
            }
        }

        private static TreeViewItem CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeViewItem { Header = directoryInfo.Name };

            foreach (var file in directoryInfo.GetFiles())
            {
                directoryNode.Items.Add(new TreeViewItem { Header = Path.GetFileNameWithoutExtension(file.Name) });
            }

            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryNode.Items.Add(CreateDirectoryNode(directory));
            }

            return directoryNode;
        }

        private void radioAuto_Checked(object sender, RoutedEventArgs e)
        {
            send_data[2] = 2;
        }

        private void radioMDI_Checked(object sender, RoutedEventArgs e)
        {
            send_data[2] = 4;
        }
        private void radioMeasure_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("niet");
        }

        private void radioHome_Checked(object sender, RoutedEventArgs e)
        {
            send_data[2] = 3;
            flag_go_home = true;
        }
        private void radioHome_UnChecked(object sender, RoutedEventArgs e)
        {
            flag_go_home = false;
            vm.Color_Y = Brushes.Transparent;
            vm.Color_Z = Brushes.Transparent;
            vm.Color_X = Brushes.Transparent;
            vm.button_START_enabled = false;
        }

        private void radioManual_Checked(object sender, RoutedEventArgs e)
        {
            send_data[2] = 1;
        }

        private void btnUnloadPRG_Click(object sender, RoutedEventArgs e)
        {
            index = 0;
            send_data[4] = 2;
            cmdvm.CMD_Array = start;
            programm_setting_vm.Programm_Setting_Mode_Array = start_generator_modes;
            Datagrid_ModeGenerator_Update(-2); // обнулить датагрид

            vm.OpenedProgramName = "";
            programm_setting_vm.Combobox_ModeSetting_Enabled = "False";
            vm.button_UNLOAD_enabled = "False";
            vm.button_START_enabled = "False";
            vm.button_PAUSE_enabled = "False";
        }

        private void btnGoConnect_Click(object sender, RoutedEventArgs e)
        {
            if (btnsPressed[0, 0] == 0) // если кнопка Связь не нажата
            {
                flag_GoDisconnect = false;
                btnsPressed[0, 0] = 1;

                //ModClient.SerialPort = combobox.SelectedItem.ToString();
                ModClient.Connect();

                if (ModClient.Connected == true)
                {
                    Thread.Sleep(100); // ждем пока обновятся регистры
                    ElementsThatDoesntNeedToAlwaysUpdate();

                    Thread WaitingThread = new Thread(() => WaitingLoop()); // запуск ожидания 4го бита 51 рега
                    WaitingThread.Start();

                    enableUIelements(1);
                    //btnGoConnect.BorderBrush = Brushes.Green;
                }
                else
                {
                    flag_GoDisconnect = true;
                    MessageBox.Show("Соединение не установлено!", "Ошибка");
                    btnsPressed[0, 0] = 0;
                    Disconnect();
                }
            }
            else
            {
                flag_GoDisconnect = true;

                enableUIelements(0);

                //btnGoConnect.BorderBrush = Brushes.Red;
                btnsPressed[0, 0] = 0;
            }


        }

        private void btnCloseApp_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if ((registers[50] & 0b001) != 0)
            {
                send_data[3] = 2;
            }
        }
        private void X_choose(object sender, RoutedEventArgs e)
        {
            if (flag_go_home)
            {
                vm.Color_X = color_green;
                vm.Color_Y = Brushes.Transparent;
                vm.Color_Z = Brushes.Transparent;

                flag_home_x_choosed = true;
                flag_home_y_choosed = false;
                flag_home_z_choosed = false;

                vm.button_START_enabled = true;
                send_data[6] = 1;
            }
        }
        private void Y_choose(object sender, RoutedEventArgs e)
        {
            if (flag_go_home)
            {
                vm.Color_Y = color_green;
                vm.Color_X = Brushes.Transparent;
                vm.Color_Z = Brushes.Transparent;

                flag_home_y_choosed = true;
                flag_home_x_choosed = false;
                flag_home_z_choosed = false;

                vm.button_START_enabled = true;
                send_data[6] = 2;
            }
        }
        private void Z_choose(object sender, RoutedEventArgs e)
        {
            if (flag_go_home)
            {
                vm.Color_Z = color_green;
                vm.Color_X = Brushes.Transparent;
                vm.Color_Y = Brushes.Transparent;

                flag_home_z_choosed = true;
                flag_home_y_choosed = false;
                flag_home_x_choosed = false;

                vm.button_START_enabled = true;
                send_data[6] = 4;
            }
        }
        private void Touch_Open_Virtual_Keyboard(object sender, TouchEventArgs e)
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
        private void Open_Virtual_Keyboard()
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
        private void btnXplus_MouseDown(object sender, TouchEventArgs e)
        {
            send_data[1] |= 0b1;
        }
        private void btnXplus_MouseUp(object sender, TouchEventArgs e)
        {
            send_data[1] &= (~0b1);
        }
        private void btnXmin_MouseDown(object sender, TouchEventArgs e)
        {
            send_data[1] |= 0b10;
        }
        private void btnXmin_MouseUp(object sender, TouchEventArgs e)
        {
            send_data[1] &= (~0b10);
        }
        private void btnYmin_MouseDown(object sender, TouchEventArgs e)
        {
            send_data[1] |= 0b1000;
        }
        private void btnYmin_MouseUp(object sender, TouchEventArgs e)
        {
            send_data[1] &= (~0b1000);
        }
        private void btnYplus_MouseDown(object sender, TouchEventArgs e)
        {
            send_data[1] |= 0b100;
        }
        private void btnYplus_MouseUp(object sender, TouchEventArgs e)
        {
            send_data[1] &= (~0b100);
        }
        private void btnZplus_MouseDown(object sender, TouchEventArgs e)
        {
            send_data[1] |= 0b10000;
        }
        private void btnZplus_MouseUp(object sender, TouchEventArgs e)
        {
            send_data[1] &= (~0b10000);
        }
        private void btnZmin_MouseDown(object sender, TouchEventArgs e)
        {
            send_data[1] |= 0b100000;
        }
        private void btnZmin_MouseUp(object sender, TouchEventArgs e)
        {
            send_data[1] &= (~0b100000);
        }
        private void btnCplus_MouseDown(object sender, TouchEventArgs e)
        {
            send_data[1] |= 0b10000;
        }
        private void btnCplus_MouseUp(object sender, TouchEventArgs e)
        {
            send_data[1] &= (~0b10000);
        }
        private void btnCmin_MouseDown(object sender, TouchEventArgs e)
        {
            send_data[1] |= 0b100000;
        }
        private void btnCmin_MouseUp(object sender, TouchEventArgs e)
        {
            send_data[1] &= (~0b100000);
        }
        private void btnAplus_MouseDown(object sender, TouchEventArgs e)
        {
            send_data[1] |= 0b10000;
        }
        private void btnAplus_MouseUp(object sender, TouchEventArgs e)
        {
            send_data[1] &= (~0b10000);
        }
        private void btnAmin_MouseDown(object sender, TouchEventArgs e)
        {
            send_data[1] |= 0b100000;
        }
        private void btnAmin_MouseUp(object sender, TouchEventArgs e)
        {
            send_data[1] &= (~0b100000);
        }
        private void btnBplus_MouseDown(object sender, TouchEventArgs e)
        {
            send_data[1] |= 0b10000;
        }
        private void btnBplus_MouseUp(object sender, TouchEventArgs e)
        {
            send_data[1] &= (~0b10000);
        }
        private void btnBmin_MouseDown(object sender, TouchEventArgs e)
        {
            send_data[1] |= 0b100000;
        }
        private void btnBmin_MouseUp(object sender, TouchEventArgs e)
        {
            send_data[1] &= (~0b100000);
        }
        private void btnSPDswitch_Click(object sender, RoutedEventArgs e)
        {
            if (btnsPressed[3, 0] == 0)
            {
                //btnSPDswitch.Content = "II";
                btnsPressed[3, 0] = 1;

                send_data[1] |= 0b1000000000000;
            }
            else
            {
                //btnSPDswitch.Content = "I";
                btnsPressed[3, 0] = 0;

                send_data[1] &= (~0b1000000000000);
            }
        }

        private void btnSmth2_Click(object sender, RoutedEventArgs e)
        {
            if (btnsPressed[1, 0] == 0)
            {
                //btnSmth2.Background = Brushes.LightGreen;
                btnsPressed[1, 0] = 1;

                send_data[1] = 1;
            }
            else
            {
                //btnSmth2.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x67, 0x3A, 0xB7));
                btnsPressed[1, 0] = 0;

                send_data[1] = 1;
            }
        }

        private void btnSmth1_Click(object sender, RoutedEventArgs e)
        {
            if (btnsPressed[2, 0] == 0)
            {
                //btnSmth1.Background = Brushes.LightGreen;
                btnsPressed[2, 0] = 1;
            }
            else
            {
               // btnSmth1.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x67, 0x3A, 0xB7));
                btnsPressed[2, 0] = 0;
            }
        }

        private void Work_X_goNull_Click(object sender, RoutedEventArgs e)
        {
            send_data[0] = 1;
        }

        private void Work_Y_goNull_Click(object sender, RoutedEventArgs e)
        {
            send_data[0] = 2;
        }

        private void Work_Z_goNull_Click(object sender, RoutedEventArgs e)
        {
            send_data[0] = 4;
        }
        private void CheckBox1_Checked(object sender, RoutedEventArgs e) //power
        {
            send_data[1] |= 0x4000;
        }
        private void CheckBox1_Unchecked(object sender, RoutedEventArgs e) //power
        {
            send_data[1] &= (~0x4000);
        }
        private void CheckBox2_Checked(object sender, RoutedEventArgs e) //adaptive
        {
            send_data[1] |= 0x2000;
        }
        private void CheckBox2_Unchecked(object sender, RoutedEventArgs e) //adaptive
        {
            send_data[1] &= (~0x2000);
        }
        private void CheckBox3_Checked(object sender, RoutedEventArgs e) //relax
        {
            send_data[1] |= 0x8000;
        }
        private void CheckBox3_Unchecked(object sender, RoutedEventArgs e) //relax
        {
            send_data[1] &= (~0x8000);
        }
        private void Buttontest_Click(object sender, RoutedEventArgs e)
        {
            //send_data[8] = Convert.ToInt32(textboxTest108.Text);
        }
        private void btnReloadCOM_Click(object sender, RoutedEventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            //combobox.ItemsSource = ports;
        }

        private void btn_array_hw2_click(object sender, RoutedEventArgs e)
        {
            GetReg(200, 2);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
       

            if (ModClient.Connected == true)
            {
                
                do
                {
                    if (flag_go_home)
                    {
                        send_data[3] = 1;
                        break;
                    }

                    if (index == 0 & flag_go_cmd == false)
                    {
                        flag_start_btn_pressed = true;
                        Datagrid_ModeGeneratorRightNow_Update();

                        //flag_file_loaded = false;
                        break;
                    }
                    else if (vm.Labels_State == "ПАУЗА")
                    {
                        send_data[3] = 1;
                        break;
                    }
                }
                while (false);
            }
            else
            {
                MessageBox.Show("Соединение не установлено!", "Ошибка");
                Disconnect();
            }
        }

        private void btnInput_Click(object sender, RoutedEventArgs e)
        {
            if (ModClient.Connected == true)
            {
                if (registers[52] == 4)
                {
                    string[] arrayInput = new string[1];

                    arrayInput[0] = TextBox_CMD_MDI.Text;

                    listView2.ListX = FindCoordinates(arrayInput, 'x', 'X');
                    listView2.ListY = FindCoordinates(arrayInput, 'y', 'Y');
                    listView2.ListZ = FindCoordinates(arrayInput, 'z', 'Z');
                    listView2.ListA = FindCoordinates(arrayInput, 'a', 'A');
                    listView2.ListB = FindCoordinates(arrayInput, 'b', 'B');
                    listView2.ListC = FindCoordinates(arrayInput, 'c', 'C');
                    listView2.ListF = FindCoordinates(arrayInput, 'f', 'F');
                    listView2.ListR = FindCoordinates(arrayInput, 'r', 'R');
                    listView2.BitMaskList = FindListNumbers(arrayInput);

                    List<int> list = new List<int>() { 1 };
                    //listView2.ListGCode = list;
                    listView2.ListGCode = FindErrorsInString(arrayInput);

                    SendWritedCMDData();

                    flag_go_cmd = true;
                }
            }
            else
            {
                MessageBox.Show("Соединение не установлено!", "Ошибка");
                Disconnect();
            }


        }
    }
}
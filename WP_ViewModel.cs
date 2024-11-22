using Gu.Wpf.DataGrid2D;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ModbusWPFproject
{
    public partial class Window1 : Window
    {
        public class WP_Panel_VM : INotifyPropertyChanged
        {
            //переменные
            private string? _textblock_Button_SwitchOn;

            private string? _coordinates_machine_x;
            private string? _coordinates_machine_y;
            private string? _coordinates_machine_z;

            private string? _coordinates_work_x;
            private string? _coordinates_work_y;
            private string? _coordinates_work_z;

            private string? _registers50;
            private string? _registers52;

            private bool? _WP_LoadPRG_Open;
            private bool? _WP_Settings_Open;
            private bool? _WP_DIAG_Open;

            private int _sliders_setting;
            private int _sliders_supply;

            private int _WP_General_BlurRadius;

            private int _labels_mode;
            private int _labels_podacha;
            private int _labels_ustavka;

            private string? _labels_speed;
            private string? _labels_state;
            private string? _labels_test;
            private string? _labels_variables;

            private int _txbtest;
            private float _progressbarTok_value;
            private int _progressbar_Percentage_value;

            private object? _radiotest;

            private string? _button_mode_auto_color;
            private string? _button_mode_auto_borderbrush;

            private object? _button_start_color;
            private object? _button_stop_color;
            private object? _button_pause_color;
            private object? _button_1frame_color;
            private object? _button_1axis_color;
            private object? _button_manual_color;
            private object? _button_measure_color;
            private object? _button_findnull_color;
            private object? _button_editor_color;
            private object? _button_settings_color;

            private object? _button_pause_color_borderbrush;
            private object? _button_stop_color_borderbrush;
            private object? _button_start_color_borderbrush;
            private object? _button_1frame_color_borderbrush;
            private object? _button_1axis_color_borderbrush;
            private object? _button_measure_color_borderbrush;
            private object? _button_manual_color_borderbrush;
            private object? _button_findnull_color_borderbrush;
            private object? _button_editor_color_borderbrush;
            private object? _button_settings_color_borderbrush;

            private object? _labels_color_x;
            private object? _labels_color_y;
            private object? _labels_color_z;

            private object? _button_X_null_visible;
            private object? _button_Y_null_visible;
            private object? _button_Z_null_visible;

            private object? _button_start_enabled;
            private object? _button_pause_enabled;
            private object? _button_stop_enabled;

            private object? _button_loadPRG_enabled;
            private object? _button_unloadPRG_enabled;

            private object? _progressbar_visible;

            private object? _grid_bottom_cmd_gen_visibility;
            private object? _Grid_Bottom_MDI_Visibility;
            private object? _grid_bottom_manual_visibility;
            private object? _grid_upper_axisnulling_visibility;
            private object? _Grid_Bottom_FINDNULL_Visibility;
            private object? _Grid_Bottom_MEASURE_Visibility;


            private object? _Menu_Manual_Bottom_Visibility;
            private object? _Menu_Bottom_Visibility;

            private object? _WorkPanel_General_Visible;
            private object? _WorkPanel_Editor_Visible;

            private object? _WP_Window_ChoosePRG_Visibility;

            private object? _WP_Measure_FindCorner_Menu_Visibility;
            private object? _WP_Measure_FindAngle_Menu_Visibility;
            private object? _WP_Measure_FindCenter_Menu_Visibility;
            private object? _DataGrid_Measure_Result_Visibility;

            private object? _WP_Window_Settings_Visibility;
            private object? _WP_Window_Diag_Visibility;

            private object? _WP_Menu_IsEnabled;
            private object? _Menu_Bottom_IsEnabled;
            private object? _Menu_Manual_Bottom_IsEnabled;

            private object? _Menu_Editor_Bottom_Visibility;
            private object? _Menu_Editor_Bottom_IsEnabled;

            private object? _slider_supply_enabled;
            private object? _slider_setting_enabled;

            private object? _button_X_min_enabled;
            private object? _button_X_plus_enabled;
            private object? _button_Y_plus_enabled;
            private object? _button_Y_min_enabled;
            private object? _button_Z_plus_enabled;
            private object? _button_Z_min_enabled;

            private object? _button_SPD_switch_enabled;
            private object? _button_smth1_enabled;
            private object? _button_smth2_enabled;
            
            private object? _Button_DIAG_Enabled;
            private object? _Button_SETTINGS_Enabled;
            private object? _Button_EDITOR_Enabled;

            private object? _Button_AUTO_Enabled;
            private object? _Button_1AXIS_Enabled;
            private object? _Button_1FRAME_Enabled;
            private object? _Button_MEASURE_Enabled;
            private object? _Button_FINDNULL_Enabled;
            private object? _Button_MANUAL_Enabled;

            private object? _DIAG_Sensors_Menu_Visibility;
            private object? _DIAG_Gen_Menu_Visibility;

            private string? _Indication_Progressbar_Background;
            private string? _Indication_Progressbar_Borderbrush;
            private string? _Indication_Progressbar_Foreground;

            private string? _OpenedProgramName;
            private string? _Gen_Mode_Num_Now;
            private string? _Indication_Frame;
            private string? _Indication_V;
            private string? _Indication_F;

            private string? _Grid_GEN_Params_polarity;
            private string? _Grid_GEN_Params_ustavka;
            private string? _Grid_GEN_Params_freq;
            private string? _Grid_GEN_Params_skv;
            private string? _Grid_GEN_Params_workTok;
            private string? _Grid_GEN_Params_arsonTok;
            private string? _Grid_GEN_Params_guardTok;
            private string? _Grid_GEN_Params_package_durability;
            private string? _Grid_GEN_Params_pause_durability;
            private string? _Grid_GEN_Params_titanium_impulse;
            private string? _Grid_GEN_Params_capacity;
            private string? _Grid_GEN_Params_relaxation_dist;
            private string? _Grid_GEN_Params_relaxation_period;
            private string? _Grid_GEN_Params_gainFactor;
            private object? _Grid_Gen_Params_Visibility;

            private float _Indication_R_l;
            private float _Indication_R_r;

            public ICommand WP_Button_Smth1 { get; private set; }
            public ICommand WP_Button_Open_Keyboard { get; private set; }
            public ICommand WP_Button_LoadPRG { get; private set; }

            public WP_Panel_VM()
            {
                WP_Button_Smth1 = new RelayCommand(WP_Button_Smth1_Execute);
                WP_Button_Open_Keyboard = new RelayCommand(WP_Button_Open_Keyboard_Execute);
            }

            private void WP_Button_Smth1_Execute(object parameter)
            {
                Environment.Exit(0);
            }            
            private void WP_Button_Open_Keyboard_Execute(object parameter)
            {
                Process[] pname = Process.GetProcessesByName("FreeVK");
                if (pname.Length == 0) Process.Start("FreeVK.exe");
            }
            public string Grid_GEN_Params_gainFactor
            {
                get
                {
                    return _Grid_GEN_Params_gainFactor!;
                }
                set
                {
                    _Grid_GEN_Params_gainFactor = value;
                    NotifyPropertyChanged("Grid_GEN_Params_gainFactor");
                }
            } 
            public string Grid_GEN_Params_relaxation_period
            {
                get
                {
                    return _Grid_GEN_Params_relaxation_period!;
                }
                set
                {
                    _Grid_GEN_Params_relaxation_period = value;
                    NotifyPropertyChanged("Grid_GEN_Params_relaxation_period");
                }
            } 
            public string Grid_GEN_Params_relaxation_dist
            {
                get
                {
                    return _Grid_GEN_Params_relaxation_dist!;
                }
                set
                {
                    _Grid_GEN_Params_relaxation_dist = value;
                    NotifyPropertyChanged("Grid_GEN_Params_relaxation_dist");
                }
            } 
            public string Grid_GEN_Params_capacity
            {
                get
                {
                    return _Grid_GEN_Params_capacity!;
                }
                set
                {
                    _Grid_GEN_Params_capacity = value;
                    NotifyPropertyChanged("Grid_GEN_Params_capacity");
                }
            } 
            public string Grid_GEN_Params_titanium_impulse
            {
                get
                {
                    return _Grid_GEN_Params_titanium_impulse!;
                }
                set
                {
                    _Grid_GEN_Params_titanium_impulse = value;
                    NotifyPropertyChanged("Grid_GEN_Params_titanium_impulse");
                }
            } 
            public string Grid_GEN_Params_pause_durability
            {
                get
                {
                    return _Grid_GEN_Params_pause_durability!;
                }
                set
                {
                    _Grid_GEN_Params_pause_durability = value;
                    NotifyPropertyChanged("Grid_GEN_Params_pause_durability");
                }
            } 
            public string Grid_GEN_Params_package_durability
            {
                get
                {
                    return _Grid_GEN_Params_package_durability!;
                }
                set
                {
                    _Grid_GEN_Params_package_durability = value;
                    NotifyPropertyChanged("Grid_GEN_Params_package_durability");
                }
            } 
            public string Grid_GEN_Params_guardTok
            {
                get
                {
                    return _Grid_GEN_Params_guardTok!;
                }
                set
                {
                    _Grid_GEN_Params_guardTok = value;
                    NotifyPropertyChanged("Grid_GEN_Params_guardTok");
                }
            } 
            public string Grid_GEN_Params_arsonTok
            {
                get
                {
                    return _Grid_GEN_Params_arsonTok!;
                }
                set
                {
                    _Grid_GEN_Params_arsonTok = value;
                    NotifyPropertyChanged("Grid_GEN_Params_arsonTok");
                }
            } 
            public string Grid_GEN_Params_workTok
            {
                get
                {
                    return _Grid_GEN_Params_workTok!;
                }
                set
                {
                    _Grid_GEN_Params_workTok = value;
                    NotifyPropertyChanged("Grid_GEN_Params_workTok");
                }
            } 
            public string Grid_GEN_Params_skv
            {
                get
                {
                    return _Grid_GEN_Params_skv!;
                }
                set
                {
                    _Grid_GEN_Params_skv = value;
                    NotifyPropertyChanged("Grid_GEN_Params_skv");
                }
            } 
            public string Grid_GEN_Params_freq
            {
                get
                {
                    return _Grid_GEN_Params_freq!;
                }
                set
                {
                    _Grid_GEN_Params_freq = value;
                    NotifyPropertyChanged("Grid_GEN_Params_freq");
                }
            } 
            public string Grid_GEN_Params_ustavka
            {
                get
                {
                    return _Grid_GEN_Params_ustavka!;
                }
                set
                {
                    _Grid_GEN_Params_ustavka = value;
                    NotifyPropertyChanged("Grid_GEN_Params_ustavka");
                }
            } 
            public string Grid_GEN_Params_polarity
            {
                get
                {
                    return _Grid_GEN_Params_polarity!;
                }
                set
                {
                    _Grid_GEN_Params_polarity = value;
                    NotifyPropertyChanged("Grid_GEN_Params_polarity");
                }
            }
            public string Indication_Progressbar_Background
            {
                get
                {
                    return _Indication_Progressbar_Background!;
                }
                set
                {
                    _Indication_Progressbar_Background = value;
                    NotifyPropertyChanged("Indication_Progressbar_Background");
                }
            }
            public string Indication_Progressbar_Foreground
            {
                get
                {
                    return _Indication_Progressbar_Foreground!;
                }
                set
                {
                    _Indication_Progressbar_Foreground = value;
                    NotifyPropertyChanged("Indication_Progressbar_Foreground");
                }
            }
            public string Indication_Progressbar_Borderbrush
            {
                get
                {
                    return _Indication_Progressbar_Borderbrush!;
                }
                set
                {
                    _Indication_Progressbar_Borderbrush = value;
                    NotifyPropertyChanged("Indication_Progressbar_Borderbrush");
                }
            }
            public string Textblock_Button_SwitchOn
            {
                get
                {
                    return _textblock_Button_SwitchOn!;
                }
                set
                {
                    _textblock_Button_SwitchOn = value;
                    NotifyPropertyChanged("Textblock_Button_SwitchOn");
                }
            }
            public string button_mode_auto_color
            {
                get
                {
                    return _button_mode_auto_color!;
                }
                set
                {
                    _button_mode_auto_color = value;
                    NotifyPropertyChanged("button_mode_auto_color");
                }
            }            
            public object Grid_Gen_Params_Visibility
            {
                get
                {
                    return _Grid_Gen_Params_Visibility!;
                }
                set
                {
                    _Grid_Gen_Params_Visibility = value;
                    NotifyPropertyChanged("Grid_Gen_Params_Visibility");
                }
            }
            public object button_START_color
            {
                get
                {
                    return _button_start_color!;
                }
                set
                {
                    _button_start_color = value;
                    NotifyPropertyChanged("button_START_color");
                }
            }
            
            public object button_STOP_color
            {
                get
                {
                    return _button_stop_color!;
                }
                set
                {
                    _button_stop_color = value;
                    NotifyPropertyChanged("button_STOP_color");
                }
            }
            
            public object button_PAUSE_color
            {
                get
                {
                    return _button_pause_color!;
                }
                set
                {
                    _button_pause_color = value;
                    NotifyPropertyChanged("button_PAUSE_color");
                }
            }
            public object button_1frame_color
            {
                get
                {
                    return _button_1frame_color!;
                }
                set
                {
                    _button_1frame_color = value;
                    NotifyPropertyChanged("button_1frame_color");
                }
            }
            public object button_1axis_color
            {
                get
                {
                    return _button_1axis_color!;
                }
                set
                {
                    _button_1axis_color = value;
                    NotifyPropertyChanged("button_1axis_color");
                }
            }
            public object button_manual_color
            {
                get
                {
                    return _button_manual_color!;
                }
                set
                {
                    _button_manual_color = value;
                    NotifyPropertyChanged("button_manual_color");
                }
            }
            public object button_measure_color
            {
                get
                {
                    return _button_measure_color!;
                }
                set
                {
                    _button_measure_color = value;
                    NotifyPropertyChanged("button_measure_color");
                }
            }
            public object button_findnull_color
            {
                get
                {
                    return _button_findnull_color!;
                }
                set
                {
                    _button_findnull_color = value;
                    NotifyPropertyChanged("button_findnull_color");
                }
            }
            public object button_editor_color
            {
                get
                {
                    return _button_editor_color!;
                }
                set
                {
                    _button_editor_color = value;
                    NotifyPropertyChanged("button_editor_color");
                }
            }
            public object button_settings_color
            {
                get
                {
                    return _button_settings_color!;
                }
                set
                {
                    _button_settings_color = value;
                    NotifyPropertyChanged("button_settings_color");
                }
            }

            public string button_mode_auto_borderbrush
            {
                get
                {
                    return _button_mode_auto_borderbrush!;
                }
                set
                {
                    _button_mode_auto_borderbrush = value;
                    NotifyPropertyChanged("button_mode_auto_borderbrush");
                }
            }
            public object button_START_color_borderbrush
            {
                get
                {
                    return _button_start_color_borderbrush!;
                }
                set
                {
                    _button_start_color_borderbrush = value;
                    NotifyPropertyChanged("button_START_color_borderbrush");
                }
            }

            public object button_STOP_color_borderbrush
            {
                get
                {
                    return _button_stop_color_borderbrush!;
                }
                set
                {
                    _button_stop_color_borderbrush = value;
                    NotifyPropertyChanged("button_STOP_color_borderbrush");
                }
            }

            public object button_PAUSE_color_borderbrush
            {
                get
                {
                    return _button_pause_color_borderbrush!;
                }
                set
                {
                    _button_pause_color_borderbrush = value;
                    NotifyPropertyChanged("button_PAUSE_color_borderbrush");
                }
            }
            public object button_1axis_color_borderbrush
            {
                get
                {
                    return _button_1axis_color_borderbrush!;
                }
                set
                {
                    _button_1axis_color_borderbrush = value;
                    NotifyPropertyChanged("button_1axis_color_borderbrush");
                }
            }
            public object button_1frame_color_borderbrush
            {
                get
                {
                    return _button_1frame_color_borderbrush!;
                }
                set
                {
                    _button_1frame_color_borderbrush = value;
                    NotifyPropertyChanged("button_1frame_color_borderbrush");
                }
            }
            public object button_manual_color_borderbrush
            {
                get
                {
                    return _button_manual_color_borderbrush!;
                }
                set
                {
                    _button_manual_color_borderbrush = value;
                    NotifyPropertyChanged("button_manual_color_borderbrush");
                }
            }
            public object button_measure_color_borderbrush
            {
                get
                {
                    return _button_measure_color_borderbrush!;
                }
                set
                {
                    _button_measure_color_borderbrush = value;
                    NotifyPropertyChanged("button_measure_color_borderbrush");
                }
            }
            public object button_findnull_color_borderbrush
            {
                get
                {
                    return _button_findnull_color_borderbrush!;
                }
                set
                {
                    _button_findnull_color_borderbrush = value;
                    NotifyPropertyChanged("button_findnull_color_borderbrush");
                }
            }
            public object button_editor_color_borderbrush
            {
                get
                {
                    return _button_editor_color_borderbrush!;
                }
                set
                {
                    _button_editor_color_borderbrush = value;
                    NotifyPropertyChanged("button_editor_color_borderbrush");
                }
            }
            public object button_settings_color_borderbrush
            {
                get
                {
                    return _button_settings_color_borderbrush!;
                }
                set
                {
                    _button_settings_color_borderbrush = value;
                    NotifyPropertyChanged("button_settings_color_borderbrush");
                }
            }
            public float progressBar_Tok_value
            {
                get
                {
                    return _progressbarTok_value!;
                }
                set
                {
                    _progressbarTok_value = value;
                    NotifyPropertyChanged("progressBar_Tok_value");
                }
            }
            public object slider_Supply_enabled
            {
                get
                {
                    return _slider_supply_enabled!;
                }
                set
                {
                    _slider_supply_enabled = value;
                    NotifyPropertyChanged("slider_Supply_enabled");
                }
            }
            public object slider_Setting_enabled
            {
                get
                {
                    return _slider_setting_enabled!;
                }
                set
                {
                    _slider_setting_enabled = value;
                    NotifyPropertyChanged("slider_Setting_enabled");
                }
            }
            public object button_X_min_enabled
            {
                get
                {
                    return _button_X_min_enabled!;
                }
                set
                {
                    _button_X_min_enabled = value;
                    NotifyPropertyChanged("button_X_min_enabled");
                }
            }
            public object button_X_plus_enabled
            {
                get
                {
                    return _button_X_plus_enabled!;
                }
                set
                {
                    _button_X_plus_enabled = value;
                    NotifyPropertyChanged("button_X_plus_enabled");
                }
            }
            public object button_Y_plus_enabled
            {
                get
                {
                    return _button_Y_plus_enabled!;
                }
                set
                {
                    _button_Y_plus_enabled = value;
                    NotifyPropertyChanged("button_Y_plus_enabled");
                }
            }
            public object button_Y_min_enabled
            {
                get
                {
                    return _button_Y_min_enabled!;
                }
                set
                {
                    _button_Y_min_enabled = value;
                    NotifyPropertyChanged("button_Y_min_enabled");
                }
            }
            public object button_Z_plus_enabled
            {
                get
                {
                    return _button_Z_plus_enabled!;
                }
                set
                {
                    _button_Z_plus_enabled = value;
                    NotifyPropertyChanged("button_Z_plus_enabled");
                }
            }
            public object button_Z_min_enabled
            {
                get
                {
                    return _button_Z_min_enabled!;
                }
                set
                {
                    _button_Z_min_enabled = value;
                    NotifyPropertyChanged("button_Z_min_enabled");
                }
            }
            public object button_SPDswitch_enabled
            {
                get
                {
                    return _button_SPD_switch_enabled!;
                }
                set
                {
                    _button_SPD_switch_enabled = value;
                    NotifyPropertyChanged("button_SPDswitch_enabled");
                }
            }
            public object button_Smth1_enabled
            {
                get
                {
                    return _button_smth1_enabled!;
                }
                set
                {
                    _button_smth1_enabled = value;
                    NotifyPropertyChanged("button_Smth1_enabled");
                }
            }
            public object button_Smth2_enabled
            {
                get
                {
                    return _button_smth2_enabled!;
                }
                set
                {
                    _button_smth2_enabled = value;
                    NotifyPropertyChanged("button_Smth2_enabled");
                }
            }
            public object button_START_enabled
            {
                get
                {
                    return _button_start_enabled!;
                }
                set
                {
                    _button_start_enabled = value;
                    NotifyPropertyChanged("button_START_enabled");
                }
            }
            public object button_STOP_enabled
            {
                get
                {
                    return _button_stop_enabled!;
                }
                set
                {
                    _button_stop_enabled = value;
                    NotifyPropertyChanged("button_STOP_enabled");
                }
            }
            public object button_PAUSE_enabled
            {
                get
                {
                    return _button_pause_enabled!;
                }
                set
                {
                    _button_pause_enabled = value;
                    NotifyPropertyChanged("button_PAUSE_enabled");
                }
            }
            public object button_X_null_visible
            {
                get
                {
                    return _button_X_null_visible!;
                }
                set
                {
                    _button_X_null_visible = value;
                    NotifyPropertyChanged("button_X_null_visible");
                }
            }
            public object button_Y_null_visible
            {
                get
                {
                    return _button_Y_null_visible!;
                }
                set
                {
                    _button_Y_null_visible = value;
                    NotifyPropertyChanged("button_Y_null_visible");
                }
            }
            public object button_Z_null_visible
            {
                get
                {
                    return _button_Z_null_visible!;
                }
                set
                {
                    _button_Z_null_visible = value;
                    NotifyPropertyChanged("button_Z_null_visible");
                }
            }
            public object button_LOAD_enabled
            {
                get
                {
                    return _button_loadPRG_enabled!;
                }
                set
                {
                    _button_loadPRG_enabled = value;
                    NotifyPropertyChanged("button_LOAD_enabled");
                }
            }
            public object button_UNLOAD_enabled
            {
                get
                {
                    return _button_unloadPRG_enabled!;
                }
                set
                {
                    _button_unloadPRG_enabled = value;
                    NotifyPropertyChanged("button_UNLOAD_enabled");
                }
            }
            public object Button_AUTO_Enabled
            {
                get
                {
                    return _Button_AUTO_Enabled!;
                }
                set
                {
                    _Button_AUTO_Enabled = value;
                    NotifyPropertyChanged("Button_AUTO_Enabled");
                }
            }
            public object Button_1FRAME_Enabled
            {
                get
                {
                    return _Button_1FRAME_Enabled!;
                }
                set
                {
                    _Button_1FRAME_Enabled = value;
                    NotifyPropertyChanged("Button_1FRAME_Enabled");
                }
            }
            public object Button_1AXIS_Enabled
            {
                get
                {
                    return _Button_1AXIS_Enabled!;
                }
                set
                {
                    _Button_1AXIS_Enabled = value;
                    NotifyPropertyChanged("Button_1AXIS_Enabled");
                }
            }
            public object Button_MANUAL_Enabled
            {
                get
                {
                    return _Button_MANUAL_Enabled!;
                }
                set
                {
                    _Button_MANUAL_Enabled = value;
                    NotifyPropertyChanged("Button_MANUAL_Enabled");
                }
            }
            public object Button_MEASURE_Enabled
            {
                get
                {
                    return _Button_MEASURE_Enabled!;
                }
                set
                {
                    _Button_MEASURE_Enabled = value;
                    NotifyPropertyChanged("Button_MEASURE_Enabled");
                }
            }            
            public object Button_FINDNULL_Enabled
            {
                get
                {
                    return _Button_FINDNULL_Enabled!;
                }
                set
                {
                    _Button_FINDNULL_Enabled = value;
                    NotifyPropertyChanged("Button_FINDNULL_Enabled");
                }
            }
            public object WP_Menu_IsEnabled
            {
                get
                {
                    return _WP_Menu_IsEnabled!;
                }
                set
                {
                    _WP_Menu_IsEnabled = value;
                    NotifyPropertyChanged("WP_Menu_IsEnabled");
                }
            }
            public object progressBar_Visible
            {
                get
                {
                    return _progressbar_visible!;
                }
                set
                {
                    _progressbar_visible = value;
                    NotifyPropertyChanged("progressBar_Visible");
                }
            }
            public object WP_Window_ChoosePRG_Visibility
            {
                get
                {
                    return _WP_Window_ChoosePRG_Visibility!;
                }
                set
                {
                    _WP_Window_ChoosePRG_Visibility = value;
                    NotifyPropertyChanged("WP_Window_ChoosePRG_Visibility");
                }
            }            
            public object DIAG_Sensors_Menu_Visibility
            {
                get
                {
                    return _DIAG_Sensors_Menu_Visibility!;
                }
                set
                {
                    _DIAG_Sensors_Menu_Visibility = value;
                    NotifyPropertyChanged("DIAG_Sensors_Menu_Visibility");
                }
            }            
            public object DIAG_Gen_Menu_Visibility
            {
                get
                {
                    return _DIAG_Gen_Menu_Visibility!;
                }
                set
                {
                    _DIAG_Gen_Menu_Visibility = value;
                    NotifyPropertyChanged("DIAG_Gen_Menu_Visibility");
                }
            }
            public object WP_Measure_FindCorner_Menu_Visibility
            {
                get
                {
                    return _WP_Measure_FindCorner_Menu_Visibility!;
                }
                set
                {
                    _WP_Measure_FindCorner_Menu_Visibility = value;
                    NotifyPropertyChanged("WP_Measure_FindCorner_Menu_Visibility");
                }
            }            
            public object DataGrid_Measure_Result_Visibility
            {
                get
                {
                    return _DataGrid_Measure_Result_Visibility!;
                }
                set
                {
                    _DataGrid_Measure_Result_Visibility = value;
                    NotifyPropertyChanged("DataGrid_Measure_Result_Visibility");
                }
            }
            public object WP_Measure_FindCenter_Menu_Visibility
            {
                get
                {
                    return _WP_Measure_FindCenter_Menu_Visibility!;
                }
                set
                {
                    _WP_Measure_FindCenter_Menu_Visibility = value;
                    NotifyPropertyChanged("WP_Measure_FindCenter_Menu_Visibility");
                }
            }
            public object WP_Measure_FindAngle_Menu_Visibility
            {
                get
                {
                    return _WP_Measure_FindAngle_Menu_Visibility!;
                }
                set
                {
                    _WP_Measure_FindAngle_Menu_Visibility = value;
                    NotifyPropertyChanged("WP_Measure_FindAngle_Menu_Visibility");
                }
            }
            public object Grid_Bottom_CMD_GEN_Visibility
            {
                get
                {
                    return _grid_bottom_cmd_gen_visibility!;
                }
                set
                {
                    _grid_bottom_cmd_gen_visibility = value;
                    NotifyPropertyChanged("Grid_Bottom_CMD_GEN_Visibility");
                }
            }
            public object Grid_Bottom_MDI_Visibility
            {
                get
                {
                    return _Grid_Bottom_MDI_Visibility!;
                }
                set
                {
                    _Grid_Bottom_MDI_Visibility = value;
                    NotifyPropertyChanged("Grid_Bottom_MDI_Visibility");
                }
            }
            public object Grid_Bottom_Manual_Visibility
            {
                get
                {
                    return _grid_bottom_manual_visibility!;
                }
                set
                {
                    _grid_bottom_manual_visibility = value;
                    NotifyPropertyChanged("Grid_Bottom_Manual_Visibility");
                }
            }
            public object Grid_Upper_AxisNulling_Visibility
            {
                get
                {
                    return _grid_upper_axisnulling_visibility!;
                }
                set
                {
                    _grid_upper_axisnulling_visibility = value;
                    NotifyPropertyChanged("Grid_Upper_AxisNulling_Visibility");
                }
            }
            public object Menu_Bottom_Visibility
            {
                get
                {
                    return _Menu_Bottom_Visibility!;
                }
                set
                {
                    _Menu_Bottom_Visibility = value;
                    NotifyPropertyChanged("Menu_Bottom_Visibility");
                }
            }
            public object Menu_Bottom_IsEnabled
            {
                get
                {
                    return _Menu_Bottom_IsEnabled!;
                }
                set
                {
                    _Menu_Bottom_IsEnabled = value;
                    NotifyPropertyChanged("Menu_Bottom_IsEnabled");
                }
            }
            public object Menu_Manual_Bottom_Visibility
            {
                get
                {
                    return _Menu_Manual_Bottom_Visibility!;
                }
                set
                {
                    _Menu_Manual_Bottom_Visibility = value;
                    NotifyPropertyChanged("Menu_Manual_Bottom_Visibility");
                }
            }
            public object Menu_Manual_Bottom_IsEnabled
            {
                get
                {
                    return _Menu_Manual_Bottom_IsEnabled!;
                }
                set
                {
                    _Menu_Manual_Bottom_IsEnabled = value;
                    NotifyPropertyChanged("Menu_Manual_Bottom_IsEnabled");
                }
            }
            public object Button_DIAG_Enabled
            {
                get
                {
                    return _Button_DIAG_Enabled!;
                }
                set
                {
                    _Button_DIAG_Enabled = value;
                    NotifyPropertyChanged("Button_DIAG_Enabled");
                }
            }
            public object Button_SETTINGS_Enabled
            {
                get
                {
                    return _Button_SETTINGS_Enabled!;
                }
                set
                {
                    _Button_SETTINGS_Enabled = value;
                    NotifyPropertyChanged("Button_SETTINGS_Enabled");
                }
            }
            public object Button_EDITOR_Enabled
            {
                get
                {
                    return _Button_EDITOR_Enabled!;
                }
                set
                {
                    _Button_EDITOR_Enabled = value;
                    NotifyPropertyChanged("Button_EDITOR_Enabled");
                }
            }
            public object Menu_Editor_Bottom_Visibility
            {
                get
                {
                    return _Menu_Editor_Bottom_Visibility!;
                }
                set
                {
                    _Menu_Editor_Bottom_Visibility = value;
                    NotifyPropertyChanged("Menu_Editor_Bottom_Visibility");
                }
            }
            public object Grid_Bottom_FINDNULL_Visibility
            {
                get
                {
                    return _Grid_Bottom_FINDNULL_Visibility!;
                }
                set
                {
                    _Grid_Bottom_FINDNULL_Visibility = value;
                    NotifyPropertyChanged("Grid_Bottom_FINDNULL_Visibility");
                }
            }
            public object Grid_Bottom_MEASURE_Visibility
            {
                get
                {
                    return _Grid_Bottom_MEASURE_Visibility!;
                }
                set
                {
                    _Grid_Bottom_MEASURE_Visibility = value;
                    NotifyPropertyChanged("Grid_Bottom_MEASURE_Visibility");
                }
            }
            public object Menu_Editor_Bottom_IsEnabled
            {
                get
                {
                    return _Menu_Editor_Bottom_IsEnabled!;
                }
                set
                {
                    _Menu_Editor_Bottom_IsEnabled = value;
                    NotifyPropertyChanged("Menu_Editor_Bottom_IsEnabled");
                }
            }
            public object WP_Window_Settings_Visibility
            {
                get
                {
                    return _WP_Window_Settings_Visibility!;
                }
                set
                {
                    _WP_Window_Settings_Visibility = value;
                    NotifyPropertyChanged("WP_Window_Settings_Visibility");
                }
            }
            public object WP_Window_Diag_Visibility
            {
                get
                {
                    return _WP_Window_Diag_Visibility!;
                }
                set
                {
                    _WP_Window_Diag_Visibility = value;
                    NotifyPropertyChanged("WP_Window_Diag_Visibility");
                }
            }
            public object WorkPanel_General_Visible
            {
                get
                {
                    return _WorkPanel_General_Visible!;
                }
                set
                {
                    _WorkPanel_General_Visible = value;
                    NotifyPropertyChanged("WorkPanel_General_Visible");
                }
            }
            public object WorkPanel_Editor_Visible
            {
                get
                {
                    return _WorkPanel_Editor_Visible!;
                }
                set
                {
                    _WorkPanel_Editor_Visible = value;
                    NotifyPropertyChanged("WorkPanel_Editor_Visible");
                }
            }            
            /// 
            /// ЦВЕТА ФОНА ОСЕЙ
            /// 
            public object Color_X
            {
                get
                {
                    return _labels_color_x!;
                }
                set
                {
                    _labels_color_x = value;
                    NotifyPropertyChanged("Color_X");
                }
            }

            public object Color_Y
            {
                get
                {
                    return _labels_color_y!;
                }
                set
                {
                    _labels_color_y = value;
                    NotifyPropertyChanged("Color_Y");
                }
            }

            public object Color_Z
            {
                get
                {
                    return _labels_color_z!;
                }
                set
                {
                    _labels_color_z = value;
                    NotifyPropertyChanged("Color_Z");
                }
            }
            public bool? WP_Settings_Open
            {
                get
                {
                    return _WP_Settings_Open!;
                }
                set
                {
                    _WP_Settings_Open = value;
                    NotifyPropertyChanged("WP_Settings_Open");
                }
            }            
            public bool? WP_DIAG_Open
            {
                get
                {
                    return _WP_DIAG_Open!;
                }
                set
                {
                    _WP_DIAG_Open = value;
                    NotifyPropertyChanged("WP_DIAG_Open");
                }
            } 
            public bool? WP_LoadPRG_Open
            {
                get
                {
                    return _WP_LoadPRG_Open!;
                }
                set
                {
                    _WP_LoadPRG_Open = value;
                    NotifyPropertyChanged("WP_LoadPRG_Open");
                }
            }
            /// 
            /// КООРДИНАТЫ ОСЕЙ
            /// 
            public string Coordinates_Machine_X
            {
                get
                {
                    return _coordinates_machine_x!;
                }
                set
                {
                    _coordinates_machine_x = value;
                    NotifyPropertyChanged("Coordinates_Machine_X");
                }
            }

            public string Coordinates_Machine_Y
            {
                get
                {
                    return _coordinates_machine_y!;
                }
                set
                {
                    _coordinates_machine_y = value;
                    NotifyPropertyChanged("Coordinates_Machine_Y");
                }
            }

            public string Coordinates_Machine_Z
            {
                get
                {
                    return _coordinates_machine_z!;
                }
                set
                {
                    _coordinates_machine_z = value;
                    NotifyPropertyChanged("Coordinates_Machine_Z");
                }
            }

            public string Coordinates_Work_X
            {
                get
                {
                    return _coordinates_work_x!;
                }
                set
                {
                    _coordinates_work_x = value;
                    NotifyPropertyChanged("Coordinates_Work_X");
                }
            }

            public string Coordinates_Work_Y
            {
                get
                {
                    return _coordinates_work_y!;
                }
                set
                {
                    _coordinates_work_y = value;
                    NotifyPropertyChanged("Coordinates_Work_Y");
                }
            }

            public string Coordinates_Work_Z
            {
                get
                {
                    return _coordinates_work_z!;
                }
                set
                {
                    _coordinates_work_z = value;
                    NotifyPropertyChanged("Coordinates_Work_Z");
                }
            }

            public string Registers_50
            {
                get
                {
                    return _registers50!;
                }
                set
                {
                    _registers50 = value;
                    NotifyPropertyChanged("Registers_50");
                }
            }

            public string Registers_52
            {
                get
                {
                    return _registers52!;
                }
                set
                {
                    _registers52 = value;
                    NotifyPropertyChanged("Registers_52");
                }
            }
            public int WP_General_BlurRadius
            {
                get
                {
                    return _WP_General_BlurRadius;
                }
                set
                {
                    _WP_General_BlurRadius = value;
                    NotifyPropertyChanged("WP_General_BlurRadius");
                }
            }
            /// 
            /// ПОЛЗУНКИ
            /// 
            public int Sliders_Setting
            {
                get
                {
                    return _sliders_setting;
                }
                set
                {
                    _sliders_setting = value;
                    NotifyPropertyChanged("Sliders_Setting");
                }
            }

            public int Sliders_Supply
            {
                get
                {
                    return _sliders_supply;
                }
                set
                {
                    _sliders_supply = value;
                    NotifyPropertyChanged("Sliders_Supply");
                }
            }
            /// 
            /// ЛЕЙБЛЫ
            /// 
            public int Labels_Podacha
            {
                get
                {
                    return _labels_podacha;
                }
                set
                {
                    _labels_podacha = value;
                    NotifyPropertyChanged("Labels_Podacha");
                }
            }

            public int Labels_Ustavka
            {
                get
                {
                    return _labels_ustavka;
                }
                set
                {
                    _labels_ustavka = value;
                    NotifyPropertyChanged("Labels_Ustavka");
                }
            }

            public int Labels_Mode
            {
                get
                {
                    return _labels_mode;
                }
                set
                {
                    _labels_mode = value;
                    NotifyPropertyChanged("Labels_Mode");
                }
            }
            public string Labels_Speed
            {
                get
                {
                    return _labels_speed!;
                }
                set
                {
                    _labels_speed = "F " + value;
                    NotifyPropertyChanged("Labels_Speed");
                }
            }
            public string Labels_Test
            {
                get
                {
                    return _labels_test!;
                }
                set
                {
                    _labels_test = "40-41 - " + value;
                    NotifyPropertyChanged("Labels_Test");
                }
            }
            public string Labels_Variables
            {
                get
                {
                    return _labels_variables!;
                }
                set
                {
                    _labels_variables = value;
                    NotifyPropertyChanged("Labels_Variables");
                }
            }
            public string Labels_State
            {
                get
                {
                    return _labels_state!;
                }
                set
                {
                    _labels_state = value;
                    NotifyPropertyChanged("Labels_State");
                }
            }
            public string OpenedProgramName
            {
                get
                {
                    return _OpenedProgramName!;
                }
                set
                {
                    _OpenedProgramName = value;
                    NotifyPropertyChanged("OpenedProgramName");
                }
            }
            public string Gen_Mode_Num_Now
            {
                get
                {
                    return _Gen_Mode_Num_Now!;
                }
                set
                {
                    _Gen_Mode_Num_Now = value;
                    NotifyPropertyChanged("Gen_Mode_Num_Now");
                }
            }
            public string Indication_Frame
            {
                get
                {
                    return _Indication_Frame!;
                }
                set
                {
                    _Indication_Frame = value;
                    NotifyPropertyChanged("Indication_Frame");
                }
            }
            public string Indication_V
            {
                get
                {
                    return _Indication_V!;
                }
                set
                {
                    _Indication_V = value;
                    NotifyPropertyChanged("Indication_V");
                }
            }
            public string Indication_F
            {
                get
                {
                    return _Indication_F!;
                }
                set
                {
                    _Indication_F = value;
                    NotifyPropertyChanged("Indication_F");
                }
            }
            public float Indication_R_r
            {
                get
                {
                    return _Indication_R_r!;
                }
                set
                {
                    _Indication_R_r = value;
                    NotifyPropertyChanged("Indication_R_r");
                }
            }
            public float Indication_R_l
            {
                get
                {
                    return _Indication_R_l!;
                }
                set
                {
                    _Indication_R_l = value;
                    NotifyPropertyChanged("Indication_R_l");
                }
            }
            public int TextTest
            {
                get
                {
                    return _txbtest;
                }
                set
                {
                    _txbtest = value;
                    NotifyPropertyChanged("TextTest");
                }
            }
            public int progressbar_Percentage
            {
                get
                {
                    return _progressbar_Percentage_value;
                }
                set
                {
                    _progressbar_Percentage_value = value;
                    NotifyPropertyChanged("progressbar_Percentage");
                }
            }
            public object radioTest
            {
                get
                {
                    return _radiotest!;
                }
                set
                {
                    _radiotest = value;
                    NotifyPropertyChanged("radioTest");
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

        public class Debug_VM : INotifyPropertyChanged
        {
            private int[]? _debugregsarray;
            private string[]? _debugregsarray2;
            private int[]? _debugnumbsarray;

            public int[] DebugRegsArray
            {
                get
                {
                    return _debugregsarray!;
                }
                set
                {
                    _debugregsarray = value;
                    NotifyPropertyChanged("DebugRegsArray");
                }
            }

            public string[] DebugRegsArray2
            {
                get
                {
                    return _debugregsarray2!;
                }
                set
                {
                    _debugregsarray2 = value;
                    NotifyPropertyChanged("DebugRegsArray2");
                }
            }

            public int[] DebugNumbsArray
            {
                get
                {
                    return _debugnumbsarray!;
                }
                set
                {
                    _debugnumbsarray = value;
                    NotifyPropertyChanged("DebugNumbsArray");
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
        public class Generator_VM : INotifyPropertyChanged
        {
            private int[]? _generatorArray = new int[10];

            public int[] generatorArray
            {
                get
                {
                    return _generatorArray!;
                }
                set
                {
                    _generatorArray = value;
                    NotifyPropertyChanged("generatorArray");
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
        public class Debug_VM2 : INotifyPropertyChanged
        {
            private string[]? _debugregsarray2;

            public string[] DebugRegsArray2
            {
                get
                {
                    return _debugregsarray2!;
                }
                set
                {
                    _debugregsarray2 = value;
                    NotifyPropertyChanged("DebugRegsArray2");
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
        public class CMD_VM : INotifyPropertyChanged
        {
            private string[]? _cmd_array;
            private string? _RowColor;
            private int _numberOfFrame;
            private int _percentage;
            public string[] CMD_Array
            {
                get
                {
                    return _cmd_array!;
                }
                set
                {
                    _cmd_array = value;
                    NotifyPropertyChanged("CMD_Array");
                }
            }
            public string Row_Color
            {
                get
                {
                    return _RowColor!;
                }
                set
                {
                    _RowColor = value;
                    NotifyPropertyChanged("Row_Color");
                }
            }
            public int NumberOfFrame
            {
                get
                {
                    return _numberOfFrame;
                }
                set
                {
                    _numberOfFrame = value;
                    NotifyPropertyChanged("NumberOfFrame");
                }
            }
            public int PercentageOfFrame
            {
                get
                {
                    return _percentage;
                }
                set
                {
                    _percentage = value;
                    NotifyPropertyChanged("PercentageOfFrame");
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
        public class GEN_Params : INotifyPropertyChanged
        {
            private string[]? _gen_array;
            private string[]? _gen_ed_array;
            public string[] GEN_Params_Array
            {
                get
                {
                    return _gen_array!;
                }
                set
                {
                    _gen_array = value;
                    NotifyPropertyChanged("GEN_Params_Array");
                }
            }            
            public string[] GEN_Params_ed_Array
            {
                get
                {
                    return _gen_ed_array!;
                }
                set
                {
                    _gen_ed_array = value;
                    NotifyPropertyChanged("GEN_Params_ed_Array");
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
        public class Programm_Setting_Mode : INotifyPropertyChanged
        {
            private string[]? _Programm_Setting_Mode_Array;
            private object? _Combobox_ModeSetting_Enabled;
            public string[] Programm_Setting_Mode_Array
            {
                get
                {
                    return _Programm_Setting_Mode_Array!;
                }
                set
                {
                    _Programm_Setting_Mode_Array = value;
                    NotifyPropertyChanged("Programm_Setting_Mode_Array");
                }
            }
            public object Combobox_ModeSetting_Enabled
            {
                get
                {
                    return _Combobox_ModeSetting_Enabled!;
                }
                set
                {
                    _Combobox_ModeSetting_Enabled = value;
                    NotifyPropertyChanged("Combobox_ModeSetting_Enabled");
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
        public class Inform_Messages : INotifyPropertyChanged
        {
            private string[]? _Datagrid_Inform_Messages;
            public string[] Datagrid_Inform_Messages
            {
                get
                {
                    return _Datagrid_Inform_Messages!;
                }
                set
                {
                    _Datagrid_Inform_Messages = value;
                    NotifyPropertyChanged("Datagrid_Inform_Messages");
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
        public class Time_Data : INotifyPropertyChanged
        {
            private string? _time;
            private string? _date;
            public string TimeText
            {
                get
                {
                    return _time!;
                }
                set
                {
                    _time = value;
                    NotifyPropertyChanged("TimeText");
                }
            }            
            public string DateText
            {
                get
                {
                    return _date!;
                }
                set
                {
                    _date = value;
                    NotifyPropertyChanged("DateText");
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
    public class ModeGenerator_Item : ObservableObject
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
        private string _measure;
        public string Measure
        {
            get => _measure;
            set
            {
                _measure = value;
                OnPropertyChanged();
            }
        }
    }
    public class ModeNowGenerator_Item : ObservableObject
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
        private string _measure;
        public string Measure
        {
            get => _measure;
            set
            {
                _measure = value;
                OnPropertyChanged();
            }
        }
    }

    public class DIAG_Datagrid2_Item : ObservableObject
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
    }
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

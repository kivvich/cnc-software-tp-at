using EasyModbus;
using Microsoft.Xaml.Behaviors.Layout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;


namespace ModbusWPFproject
{
    public partial class Window1 : Window
    {

        //bitx
        ListView listView = new ListView();
        ListView listView2 = new ListView();
        //

        /* - - - - - - - - - - ViewModels - - - - - - - - - - - - */
        Debug_VM debugvm = new Debug_VM();
        CMD_VM cmdvm = new CMD_VM();
        Generator_VM genvm = new Generator_VM();
        WP_Panel_VM vm = new WP_Panel_VM();
        GEN_Params gen_vm = new GEN_Params();
        Time_Data time_vm = new Time_Data();
        Programm_Setting_Mode programm_setting_vm = new Programm_Setting_Mode();
        Inform_Messages inform_vm = new Inform_Messages();
        Debug_VM2 debugvm2 = new Debug_VM2();
        /* - - - - - - - - - - - - - - - - - - - - - - - - - - - */

        /* - - - - - - - - - - Чтение файлов - - - - - - - - - - - - */
        ProcessedDataFile dataFile = new ProcessedDataFile();
        ModeGenerator modeGenerator = new ModeGenerator();
        /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

        /* - - - - - - - - - - Отображение датагридов - - - - - - - - - - - - */
        private ObservableCollection<ModeGenerator_Item> datagridModeGeneratorItems;
        private ObservableCollection<ModeNowGenerator_Item> datagridModeNowGeneratorItems;
        private ObservableCollection<DIAG_Datagrid2_Item> DIAG_Datagrid2_Items;
        /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

        string default_path = @".\Главная директория\";

        string[] arrayDates;
        string[] arraywithThreeNulls = new string[3] { "", "", "" };

        int[] registers_generator = new int[10];

        /* - - - - - - - - - - Флаги - - - - - - - - - - - - */
        bool flag_GoDisconnect = false;
        bool flag_start_btn_pressed = false;
        bool flag_need_data = false;
        bool flag_need_data1 = false;
        bool flag_need_data2 = false;
        bool flag_end_of_data = false;
        bool flag_go_home = false;
        bool flag_go_cmd = false;
        bool flag_data_generator_go = false;

        bool flag_Stanok_SwitchOn;
        bool flag_Button_Mode_Auto_Pressed;
        bool flag_Button_Mode_1FRAME_Pressed;
        bool flag_Button_Mode_1AXIS_Pressed;
        bool flag_Button_Mode_Manual_Pressed;
        bool flag_Button_Mode_Measure_Pressed;
        bool flag_Button_Mode_FindNull_Pressed;
        bool flag_Button_Mode_Editor_Pressed;
        bool flag_Button_Mode_Settings_Pressed;
        bool flag_Button_Mode_Diagnostics_Pressed;

        bool flag_home_x_choosed = false;
        bool flag_home_y_choosed = false;
        bool flag_home_z_choosed = false;
        bool flag_disconnected = false;
        bool flag_file_loaded = false;
        bool run_program = false;
        /* - - - - - - - - - - - - - - - - - - - - - - - - - */

        /* - - - - - - - - - - - - Константы - - - - - - - - - - - - - */
        SolidColorBrush color_green = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8FD393"));

        string nt_file = @"kt1_init.txt";

        List<int> nulllist = new List<int>() { 0 };

        float ustavka_now_real = 0.0f;
        float ustavka_now_setted = 0.0f;

        string nt_file2 = @"init_write.txt";
        /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

        /* - - - - - - - - - - - - Переменные - - - - - - - - - - - - - */
        //test для отладки
        int countertest = 0;
        int switchtest = 0;

        //начальные параметры
        int index = 0;
        int number = 0;
        int index_of_mode_now = 0;
        int index_of_mode_now_previous = 0;
        /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

        //0 - связь
        //1 - что-то2
        //2 - что-то1
        //3 - скорость

        int[,] btnsPressed = new int[20, 1];

        int[] registers = new int[120];
        int[] registers_gen = new int[20];

        int[] UI_coordinates_machine_X = { 0, 0 };
        int[] UI_coordinates_machine_Y = { 0, 0 };
        int[] UI_coordinates_machine_Z = { 0, 0 };

        int[] UI_coordinates_work_X = { 0, 0 };
        int[] UI_coordinates_work_Y = { 0, 0 };
        int[] UI_coordinates_work_Z = { 0, 0 };

        int[] UI_label_float_test = { 0, 0 };
        int[] UI_label_float_Speed = { 0, 0 };

        int[] UI_label_Regulator = { 0, 0 };
        int[] UI_label_speed_F = { 0, 0 };
        int[] UI_label_speed_V = { 0, 0 };

        int[] send_data = new int[11];

        public string fileName { get; set; }

        ModbusClient ModClient = new ModbusClient();
        ModbusClient ModClientGenerator = new ModbusClient();

        //окна
        Window_Reconnect ConnectionErrorWindow = new Window_Reconnect();
    }
}

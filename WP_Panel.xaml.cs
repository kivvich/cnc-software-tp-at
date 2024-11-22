using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Timers;
using Microsoft.Win32;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using EasyModbus;
using System.Text.RegularExpressions;
using System.Data;
using System.Windows.Markup;
using System.Dynamic;
using System.Reflection.Emit;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Globalization;
using System.Security.Cryptography.Xml;
using System.IO.Ports;
using System.Diagnostics;
using System.Xml.Linq;
using System.Windows.Media.Effects;
using static EasyModbus.ModbusClient;
using System.Windows.Controls.Primitives;
using static MaterialDesignThemes.Wpf.Theme;

namespace ModbusWPFproject
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            ContextInitialize();
            LetsStart();
        }
        private void LetsStart()
        {
            string[] ports = SerialPort.GetPortNames();
            
            flag_GoDisconnect = false;

            //if (ports.Contains("COM8") & ports.Contains("COM6"))
            if (ports.Contains("COM8"))
            {
                ModClientGenerator.Baudrate = 38400;
                ModClientGenerator.SerialPort = "COM3";
                ModClientGenerator.Parity = Parity.None;
                ModClientGenerator.StopBits = StopBits.One;
                ModClientGenerator.UnitIdentifier = 2;
                ModClientGenerator.NumberOfRetries = 3;
                ModClientGenerator.ConnectionTimeout = 1000;
                
                ModClient.SerialPort = "COM8";

                ModClient.Connect();
                ModClientGenerator.Connect();

                if (ModClient.Connected == true)
                {
                    Thread.Sleep(100); // ждем пока обновятся регистры
                    ElementsThatDoesntNeedToAlwaysUpdate();

                    Thread WaitingThread = new Thread(() => WaitingLoop()); // Запуск чтения с МК
                    WaitingThread.Start();

                    Thread GeneratorThread = new Thread(() => ReadGenerator()); // Запуск чтения с гены
                    GeneratorThread.Start();

                    enableUIelements(1);
                    //btnGoConnect.BorderBrush = Brushes.Green;
                }
            }
            else
            {
                flag_GoDisconnect = true;

                Thread ConnectWaitingThread = new Thread(() => ConnectWaiting());
                ConnectWaitingThread.Start();

                ModClientGenerator.Disconnect();
                ModClient.Disconnect();

            }
        }

        public void ConnectWaiting()
        {
            while (true)
            {
                Thread.Sleep(1000); // проверяем раз в 1 nс
                string[] ports = SerialPort.GetPortNames();

                if (ports.Contains("COM8") & ports.Contains("COM3"))
                {
                    ModClientGenerator.Baudrate = 38400;
                    ModClientGenerator.SerialPort = "COM3";
                    ModClientGenerator.Parity = Parity.None;
                    ModClientGenerator.StopBits = StopBits.One;
                    ModClientGenerator.UnitIdentifier = 2;
                    ModClientGenerator.NumberOfRetries = 3;
                    ModClientGenerator.ConnectionTimeout = 1000;

                    ModClient.SerialPort = "COM8";

                    try
                    {
                        ModClient.Connect();
                        ModClientGenerator.Connect();
                    }
                    catch
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            this.Effect = new BlurEffect();

                            ConnectionErrorWindow.Show();
                            ConnectionErrorWindow.Focus();
                            flag_disconnected = true;
                        }));
                        ModClient.Disconnect();
                        ModClientGenerator.Disconnect();
                    }

                    if (ModClient.Connected == true)
                    {
                        Thread.Sleep(100); // ждем пока обновятся регистры
                        ElementsThatDoesntNeedToAlwaysUpdate();

                        flag_GoDisconnect = false;

                        Thread WaitingThread = new Thread(() => WaitingLoop()); // бесконечный цикл ожидания
                        WaitingThread.Start();

                        enableUIelements(1);

                        Dispatcher.Invoke(new Action(() =>
                        {
                            ConnectionErrorWindow.Close();
                            this.Effect = null;
                            flag_disconnected = false;
                        }
                        ));
                        break;
                    }
                }
                else
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        this.Effect = new BlurEffect();

                        ConnectionErrorWindow.Show();
                        ConnectionErrorWindow.Focus();
                        flag_disconnected = true;
                    }));
                }
            }
        }

        private void ElementsThatDoesntNeedToAlwaysUpdate()
        {
            //ползунки
            vm.Sliders_Setting = registers[107];
            vm.Sliders_Supply = registers[105];
            //радиокнопки
            switch (registers[52])
            {


                default: break;
            }
        }

        private void enableUIelements(int a)
        {
            if (a == 0)
            {
                vm.button_LOAD_enabled = "False";
                vm.button_START_enabled = "False";
                vm.button_X_min_enabled = "False";
                vm.button_X_plus_enabled = "False";
                vm.button_Y_min_enabled = "False";
                vm.button_Y_plus_enabled = "False";
                vm.button_Z_min_enabled = "False";
                vm.button_Z_plus_enabled = "False";
                vm.button_SPDswitch_enabled = "False";
                vm.button_Smth1_enabled = "False";
                vm.button_Smth2_enabled = "False";
                vm.slider_Setting_enabled = "False";
                vm.slider_Supply_enabled = "False";
            }
            else if (a == 1)
            {
                vm.button_LOAD_enabled = "True";
                vm.button_X_min_enabled = "True";
                vm.button_X_plus_enabled = "True";
                vm.button_Y_min_enabled = "True";
                vm.button_Y_plus_enabled = "True";
                vm.button_Z_min_enabled = "True";
                vm.button_Z_plus_enabled = "True";
                vm.button_SPDswitch_enabled = "True";
                vm.button_Smth1_enabled = "True";
                vm.button_Smth2_enabled = "True";
                vm.slider_Setting_enabled = "True";
                vm.slider_Supply_enabled = "True";
            }
        }

        private void Disconnect()
        {
            ModClient.Disconnect();
            //VisualValuesReset();
            enableUIelements(0); // пересадить отключение элементов на MVVM!!!!!!!! а то колхоз
        }
        public class DataItem
        {
            public string OkFrame { get; set; }
        }
        private void ColorRow(System.Windows.Controls.DataGrid dg)
        {
            SolidColorBrush brush = Brushes.LightGreen;

            DataGridRow row = (DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(cmdvm.NumberOfFrame);

            if (row != null)
            {
                if (cmdvm.NumberOfFrame > 0)
                {
                    row.Background = brush;

                    DataGridRow row_previous = (DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(cmdvm.NumberOfFrame - 1);
                    row_previous.Background = Brushes.Transparent;

                    //SendNewModeToGen();

                    Datagrid_CMD.ScrollIntoView(Datagrid_CMD.Items[cmdvm.NumberOfFrame + 3]);
                }

                else if ((registers[50] & 0b111) == 0b001)
                {
                    row.Background = brush;

                    //SendNewModeToGen();
                }
            }
        }

        private void VisualValuesDisplay()
        {
            try
            {
                if (ModClient.Connected == true)
                {
                    ModClient.UnitIdentifier = 1;

                    /* - - - - - - - - - - Выбор оси - - - - - - - - - - - - */

                    if (flag_go_home == false)
                    {
                       
                        if (registers[52] == 1)
                        {
                            vm.button_X_null_visible = Visibility.Visible;
                            vm.button_Y_null_visible = Visibility.Visible;
                            vm.button_Z_null_visible = Visibility.Visible;

                            switch (registers[53])
                            {
                                case 1:
                                    vm.Color_X = Brushes.Silver;
                                    vm.Color_Y = Brushes.Transparent;
                                    vm.Color_Z = Brushes.Transparent;
                                    break;

                                case 2:
                                    vm.Color_Y = Brushes.Silver;
                                    vm.Color_X = Brushes.Transparent;
                                    vm.Color_Z = Brushes.Transparent;
                                    break;

                                case 4:
                                    vm.Color_X = Brushes.Transparent;
                                    vm.Color_Y = Brushes.Transparent;
                                    vm.Color_Z = Brushes.Silver;
                                    break;

                                default:
                                    vm.Color_X = Brushes.Transparent;
                                    vm.Color_Y = Brushes.Transparent;
                                    vm.Color_Z = Brushes.Transparent;
                                    break;
                            }
                        }
                        else
                        {
                            vm.Color_X = Brushes.Transparent;
                            vm.Color_Y = Brushes.Transparent;
                            vm.Color_Z = Brushes.Transparent;

                            vm.button_X_null_visible = Visibility.Hidden;
                            vm.button_Y_null_visible = Visibility.Hidden;
                            vm.button_Z_null_visible = Visibility.Hidden;
                        }
                    }

                    /* - - - - - - - - - - Отслеживание состояний - - - - - - - - - - - - */

                    switch (registers[50] & 0b111)
                    {
                        case 0b000:
                            vm.Labels_State = "ГОТОВ";

                            vm.button_STOP_enabled = "False";
                            vm.button_PAUSE_enabled = "False";
                            vm.button_LOAD_enabled = "True";
                            vm.Button_AUTO_Enabled = "True";
                            vm.Button_1FRAME_Enabled = "True";
                            vm.Button_MANUAL_Enabled = "True";
                            vm.Button_1AXIS_Enabled = "True";
                            vm.Button_FINDNULL_Enabled = "True";
                            vm.Button_MEASURE_Enabled = "True";
                            vm.Button_SETTINGS_Enabled = "True";
                            vm.Button_EDITOR_Enabled = "True";
                            vm.Button_DIAG_Enabled = "True";
                            break;

                        case 0b001:
                            vm.Labels_State = "РАБОТА";

                            vm.button_STOP_enabled = "True";
                            vm.button_PAUSE_enabled = "True";
                            vm.button_LOAD_enabled = "False";
                            vm.button_LOAD_enabled = "False";
                            vm.Button_AUTO_Enabled = "False";
                            vm.Button_1FRAME_Enabled = "False";
                            vm.Button_MANUAL_Enabled = "False";
                            vm.Button_1AXIS_Enabled = "False";
                            vm.Button_FINDNULL_Enabled = "False";
                            vm.Button_MEASURE_Enabled = "False";

                            if (registers[52] == 2)
                            {
                                vm.Button_AUTO_Enabled = "True";
                            }
                            if (registers[52] == 1)
                            {
                                vm.Button_MANUAL_Enabled = "True";
                            }
                            if (registers[52] == 4)
                            {
                                vm.Button_1FRAME_Enabled = "True";
                            }

                            ColorRow(Datagrid_CMD);

                            vm.progressBar_Visible = "True";
                            break;

                        case 0b010:
                            vm.Labels_State = "КОНЕЦ";

                            vm.button_STOP_enabled = "False";
                            vm.button_PAUSE_enabled = "False";
                            vm.button_LOAD_enabled = "True";
                            vm.Button_1FRAME_Enabled = "True";
                            vm.Button_MANUAL_Enabled = "True";
                            vm.Button_1AXIS_Enabled = "True";
                            vm.Button_FINDNULL_Enabled = "True";
                            vm.Button_MEASURE_Enabled = "True";
                            //vm.progressBar_Visible = "False";
                            if (flag_go_cmd) flag_go_cmd = false;
                            break;
                            
                        case 0b100:
                            vm.Labels_State = "ПАУЗА";
                            vm.button_PAUSE_enabled = "False";
                            vm.button_LOAD_enabled = "False";
                            vm.button_START_enabled = "True";
                            break;
                    }

                    /* - - - - - - - - - - Инициализация координат - - - - - - - - - - - - */

                    UI_coordinates_machine_X[0] = registers[12];
                    UI_coordinates_machine_X[1] = registers[13];

                    UI_coordinates_machine_Y[0] = registers[14];
                    UI_coordinates_machine_Y[1] = registers[15];

                    UI_coordinates_machine_Z[0] = registers[16];
                    UI_coordinates_machine_Z[1] = registers[17];

                    UI_coordinates_work_X[0] = registers[18];
                    UI_coordinates_work_X[1] = registers[19];

                    UI_coordinates_work_Y[0] = registers[20];
                    UI_coordinates_work_Y[1] = registers[21];

                    UI_coordinates_work_Z[0] = registers[22];
                    UI_coordinates_work_Z[1] = registers[23];

                    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
    
                    UI_label_float_test[0] = registers[40];
                    UI_label_float_test[1] = registers[41];

                    UI_label_Regulator[0] = registers[42];
                    UI_label_Regulator[1] = registers[43];

                    UI_label_speed_F[0] = registers[24];
                    UI_label_speed_F[1] = registers[25];

                    UI_label_float_Speed[0] = registers[24];
                    UI_label_float_Speed[1] = registers[25];

                    vm.Labels_Podacha = registers[105];
                    vm.Labels_Ustavka = registers[107];
                    vm.Labels_Mode = registers[52];

                    vm.Indication_F = (ModbusClient.ConvertRegistersToFloat(UI_label_speed_F)).ToString("0", System.Globalization.CultureInfo.InvariantCulture);
                    
                    vm.Indication_Frame = ((registers[56] + 1).ToString() + " (" + registers[62] + "%)");

                    vm.Indication_V = index_of_mode_now.ToString();

                    vm.Labels_Test = ModbusClient.ConvertRegistersToFloat(UI_label_float_test).ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);

                    vm.progressbar_Percentage = registers[61];
                    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

                    /* - - - - - - - - - - Отображение регулятора - - - - - - - - - - - - */

                    if (ModbusClient.ConvertRegistersToFloat(UI_label_Regulator) > 0)
                    {
                        vm.Indication_R_r = ModbusClient.ConvertRegistersToFloat(UI_label_Regulator);
                        vm.Indication_R_l = 0;
                    }
                    else
                    {
                        vm.Indication_R_l = Math.Abs(ModbusClient.ConvertRegistersToFloat(UI_label_Regulator));
                        vm.Indication_R_r = 0;
                    }

                    /* - - - - - - - - - - Прогрессбар напряжения - - - - - - - - - - - - */

                    vm.progressBar_Tok_value = ModbusClient.ConvertRegistersToFloat(UI_label_float_test);
                    ustavka_now_real = ModbusClient.ConvertRegistersToFloat(UI_label_float_test);

                    if (modeGenerator.listModeGenerators != null)
                    {
                        ustavka_now_setted = modeGenerator.listModeGenerators[index_of_mode_now].ustavkaVolts;

                        if (ustavka_now_real < (ustavka_now_setted - 15) | ustavka_now_real > (ustavka_now_setted + 15))
                        {
                            vm.Indication_Progressbar_Foreground = "#D36D6A";
                            vm.Indication_Progressbar_Background = "#B1D5D7";
                            vm.Indication_Progressbar_Borderbrush = "#266E71";
                        }
                        else if (ustavka_now_real < (ustavka_now_setted - 5) | ustavka_now_real > (ustavka_now_setted + 5))
                        {
                            vm.Indication_Progressbar_Foreground = "#A68138";
                            vm.Indication_Progressbar_Background = "#B1D5D7";
                            vm.Indication_Progressbar_Borderbrush = "#266E71";
                        }
                        else
                        {
                            vm.Indication_Progressbar_Foreground = "#266E71";
                            vm.Indication_Progressbar_Background = "#B1D5D7";
                            vm.Indication_Progressbar_Borderbrush = "#266E71";
                        }
                    }
                    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

                    /* - - - - - - - - - - Индикация - - - - - - - - - - - - */
                    vm.Labels_Speed = ModbusClient.ConvertRegistersToFloat(UI_label_float_Speed).ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
                    vm.Labels_Variables = "idx = " + index.ToString() + ", n = " + number.ToString();
                    cmdvm.NumberOfFrame = registers[55];
                    /* - - - - - - - - - - Отображение координат - - - - - - - - - - - - */
                    vm.Coordinates_Machine_X = ModbusClient.ConvertRegistersToFloat(UI_coordinates_machine_X).ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
                    vm.Coordinates_Machine_Y = ModbusClient.ConvertRegistersToFloat(UI_coordinates_machine_Y).ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
                    vm.Coordinates_Machine_Z = ModbusClient.ConvertRegistersToFloat(UI_coordinates_machine_Z).ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);

                    vm.Coordinates_Work_X = ModbusClient.ConvertRegistersToFloat(UI_coordinates_work_X).ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
                    vm.Coordinates_Work_Y = ModbusClient.ConvertRegistersToFloat(UI_coordinates_work_Y).ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
                    vm.Coordinates_Work_Z = ModbusClient.ConvertRegistersToFloat(UI_coordinates_work_Z).ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
                    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

                    /* - - - - - - - - - - Отображение часов - - - - - - - - - - - - */
                    time_vm.TimeText = DateTime.Now.ToString("t");
                    time_vm.DateText = DateTime.Now.ToString("d");
                    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

                    /* - - - - - - - - - - Обновление информации в момент переключения режима - - - - - - - - - - - - */
                    if (index_of_mode_now_previous != index_of_mode_now)
                    {
                        Datagrid_ModeGeneratorRightNow_Update();
                        SendModeDataToController();
                        index_of_mode_now_previous = index_of_mode_now;
                    }
                    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
                }
            }
            catch
            {
                Thread ConnectWaitingThread = new Thread(() => ConnectWaiting());
                ConnectWaitingThread.Start();
            }
        }

        private void VisualValuesReset()
        {
            //модули
            vm.Labels_Podacha = 0;
            vm.Labels_Ustavka = 0;
            vm.Labels_Mode = 0;
            //радиокнопки

            //координаты
            vm.Coordinates_Machine_X = "0.0000";
            vm.Coordinates_Machine_Y = "0.0000";
            vm.Coordinates_Machine_Z = "0.0000";

            vm.Coordinates_Work_X = "0.0000";
            vm.Coordinates_Work_Y = "0.0000";
            vm.Coordinates_Work_Z = "0.0000";

            vm.Labels_Test = "0.0000";
            vm.Labels_State = "OFF";
            
        }

    }

    class TreeViewUtils
    {
        public static void ClearTreeViewSelection(TreeView tv)
        {
            if (tv != null)
                ClearTreeViewItemsControlSelection(tv.Items, tv.ItemContainerGenerator);
        }
        private static void ClearTreeViewItemsControlSelection(ItemCollection ic, ItemContainerGenerator icg)
        {
            if ((ic != null) && (icg != null))
                for (int i = 0; i < ic.Count; i++)
                {
                    TreeViewItem tvi = icg.ContainerFromIndex(i) as TreeViewItem;
                    if (tvi != null)
                    {
                        ClearTreeViewItemsControlSelection(tvi.Items, tvi.ItemContainerGenerator);
                        tvi.IsSelected = false;
                    }
                }
        }
    }

    // класс для нумерации регистров в отладке
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }

    public class RowToIndexConverter : MarkupExtension, IValueConverter
    {
        static RowToIndexConverter? converter;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DataGridRow? row = value as DataGridRow;
            if (row != null)
                return row.GetIndex();
            else
                return -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (converter == null) converter = new RowToIndexConverter();
            return converter;
        }

        public RowToIndexConverter() { }
    }
    public class RowToIndexConverter2 : MarkupExtension, IValueConverter
    {
        static RowToIndexConverter2? converter;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DataGridRow? row = value as DataGridRow;
            if (row != null)
            {

                return row.GetIndex() + 1;
            }

            else
                return -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (converter == null) converter = new RowToIndexConverter2();
            return converter;
        }

        public RowToIndexConverter2() { }
    }
}

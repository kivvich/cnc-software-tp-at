using EasyModbus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace ModbusWPFproject
{
    public partial class Window1 : Window
    {            
        public ObservableCollection<GEN_Params> DatagridGENparams { get; set; }
        string[] start = new string[1] { "Файл с программой не загружен" };
        string[] start_generator_modes = new string[1] { null};
        private void ContextInitialize()
        {
            //UI
            vm.WP_Menu_IsEnabled = "True";
            vm.Textblock_Button_SwitchOn = "ВКЛЮЧИТЬ СТАНОК";
            vm.button_X_null_visible = Visibility.Hidden;
            vm.button_UNLOAD_enabled = "False";
            vm.button_PAUSE_enabled = "False";
            vm.button_START_enabled = "False";
            vm.button_STOP_enabled = "False";
            vm.Labels_State = "СВЯЗЬ";
            vm.progressBar_Visible = "False";
            programm_setting_vm.Combobox_ModeSetting_Enabled = "False";

            vm.WorkPanel_Editor_Visible = Visibility.Hidden;
            vm.Grid_Bottom_Manual_Visibility = Visibility.Hidden;
            vm.Grid_Upper_AxisNulling_Visibility = Visibility.Hidden;
            vm.Menu_Manual_Bottom_Visibility = Visibility.Hidden;
            vm.Menu_Manual_Bottom_IsEnabled = "False";
            vm.WP_Menu_IsEnabled = "True";
            vm.Grid_Bottom_FINDNULL_Visibility = Visibility.Hidden;
            vm.Grid_Bottom_MEASURE_Visibility = Visibility.Hidden;
            //vm.WP_Measure_FindCorner_Menu_Visibility = Visibility.Hidden;
            vm.Grid_Bottom_MDI_Visibility = Visibility.Hidden;
            vm.Menu_Editor_Bottom_Visibility = Visibility.Hidden;

            vm.WP_Window_ChoosePRG_Visibility = Visibility.Hidden;
            vm.WP_Window_Settings_Visibility = Visibility.Hidden;
            vm.Grid_Gen_Params_Visibility = Visibility.Hidden;

            vm.Grid_Bottom_CMD_GEN_Visibility = Visibility.Visible;
            vm.WorkPanel_General_Visible = Visibility.Visible;
            vm.Menu_Bottom_Visibility = Visibility.Visible;
            vm.Menu_Bottom_IsEnabled = "True";

            //Диагностика
            vm.DIAG_Sensors_Menu_Visibility = Visibility.Hidden;
            vm.DIAG_Gen_Menu_Visibility = Visibility.Hidden;

            vm.Indication_Progressbar_Foreground = "#266E71";
            vm.Indication_Progressbar_Background = "#B1D5D7";
            vm.Indication_Progressbar_Borderbrush = "#266E71";

            cmdvm.CMD_Array = start;
            programm_setting_vm.Programm_Setting_Mode_Array = start_generator_modes;
            cmdvm.Row_Color = "True";

            //цвета кнопок
            Buttons_Colors_Reset();  
            
            //тестовый вывод в датагрид с сообщениями
            List<string> DatagridInform = new List<string>()
            {
                "Станок включен","Насос активен","Всё сломалось"
            };

            inform_vm.Datagrid_Inform_Messages = DatagridInform.ToArray();

            Datagrid_CMD.EnableRowVirtualization = false;

            DataContext = vm;
            Datagrid_CMD.DataContext = cmdvm;
            
            TimeText.DataContext = time_vm;
            DateText.DataContext = time_vm;

            Combobox_ModeSettings.DataContext = programm_setting_vm;
            DataGrid_Inform.DataContext = inform_vm;
        }
        public void Datagrid_ModeGenerator_Update(int selected)
        {
            if (selected != -2)
            {
                datagridModeGeneratorItems = new ObservableCollection<ModeGenerator_Item>
                {
                    new ModeGenerator_Item { Name = "Уставка", Measure = "В", Value = modeGenerator.listModeGenerators[selected].ustavkaVolts },
                    new ModeGenerator_Item { Name = "Частота", Measure = "кГц", Value = modeGenerator.listModeGenerators[selected].frequancyPulse },
                    new ModeGenerator_Item { Name = "Скважность", Measure = " ", Value = Convert.ToInt32(modeGenerator.listModeGenerators[selected].dutyCycle) },
                    new ModeGenerator_Item { Name = "Рабочий ток", Measure = "А", Value = modeGenerator.listModeGenerators[selected].operatingCurrent },
                    new ModeGenerator_Item { Name = "Ток поджига", Measure = "А", Value = modeGenerator.listModeGenerators[selected].arsonCurrent },
                    new ModeGenerator_Item { Name = "Защитный ток", Measure = "А", Value = modeGenerator.listModeGenerators[selected].proctectiveCurrent },
                    new ModeGenerator_Item { Name = "Длительность пакета", Measure = "мкс", Value = modeGenerator.listModeGenerators[selected].packageDuration },
                    new ModeGenerator_Item { Name = "Длительность паузы", Measure = "мкс", Value = modeGenerator.listModeGenerators[selected].pauseDuration },
                    new ModeGenerator_Item { Name = "Титановый импульс", Measure = "мкс", Value = modeGenerator.listModeGenerators[selected].titaniumPulseDuration },
                    new ModeGenerator_Item { Name = "Ёмкость конденсатора", Measure = " ", Value = modeGenerator.listModeGenerators[selected].capacitorCapacity },
                    new ModeGenerator_Item { Name = "Дист. релаксации", Measure = "мм",  Value = Convert.ToInt32(modeGenerator.listModeGenerators[selected].relaxationDistance) },
                    new ModeGenerator_Item { Name = "Период релаксации", Measure = "мм",  Value = Convert.ToInt32(modeGenerator.listModeGenerators[selected].relaxationDuration) },
                    new ModeGenerator_Item { Name = "Коэф. усиления", Measure = "",  Value = Convert.ToInt32(modeGenerator.listModeGenerators[selected].gainFactor) },
                };
            }
            else
            {
                datagridModeGeneratorItems = null;
            }
            Datagrid_Settings_Mode.ItemsSource = datagridModeGeneratorItems;
        }

        public void Datagrid_ModeGeneratorRightNow_Update()
        {
            if (modeGenerator.listModeGenerators != null & (index_of_mode_now != 0))
            {
                vm.Gen_Mode_Num_Now = "(Режим E" + modeGenerator.numSetSetting[index_of_mode_now-1] + ")";
            }
            else { vm.Gen_Mode_Num_Now = "(Режима нет)"; }

            int[] skv_float = { 0, 0 };

            skv_float[0] = registers_gen[2];
            skv_float[1] = registers_gen[3];

            int[] lr_float = { 0, 0 };

            lr_float[0] = registers[60];
            lr_float[1] = registers[61];

            if (index_of_mode_now >= 0)
            {
                vm.Grid_Gen_Params_Visibility = Visibility.Visible;
                vm.Grid_GEN_Params_polarity = registers_gen[15].ToString();
                vm.Grid_GEN_Params_gainFactor = registers_gen[15].ToString(); // <<<<<<<<<<<<-- надо правильный регистр указать
                vm.Grid_GEN_Params_ustavka = registers[58].ToString();
                vm.Grid_GEN_Params_freq = registers_gen[1].ToString();
                vm.Grid_GEN_Params_skv = ModbusClient.ConvertRegistersToFloat(skv_float).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                vm.Grid_GEN_Params_workTok = registers_gen[6].ToString();
                vm.Grid_GEN_Params_arsonTok = registers_gen[7].ToString();
                vm.Grid_GEN_Params_guardTok = registers_gen[8].ToString();
                vm.Grid_GEN_Params_package_durability = registers_gen[4].ToString();
                vm.Grid_GEN_Params_pause_durability = registers_gen[5].ToString();
                vm.Grid_GEN_Params_titanium_impulse = registers_gen[13].ToString();
                vm.Grid_GEN_Params_capacity = registers_gen[14].ToString();
                vm.Grid_GEN_Params_relaxation_dist = ModbusClient.ConvertRegistersToFloat(lr_float).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                vm.Grid_GEN_Params_relaxation_period = registers[59].ToString();
            }
            else
            {
                vm.Grid_Gen_Params_Visibility = Visibility.Hidden;
            }
        }
        public void DIAG_DG2_Update()
        {
            int[] lr_float = { 0, 0 };

            lr_float[0] = registers[60];
            lr_float[1] = registers[61];

            DIAG_Datagrid2_Items = new ObservableCollection<DIAG_Datagrid2_Item>
            {
                new DIAG_Datagrid2_Item { Name = "Numb", Value = registers[58].ToString() },
                new DIAG_Datagrid2_Item { Name = "Tr", Value = registers[59].ToString() },
                new DIAG_Datagrid2_Item { Name = "Lr", Value = ModbusClient.ConvertRegistersToFloat(lr_float).ToString() },
                new DIAG_Datagrid2_Item { Name = "0 reg GEN", Value = registers_gen[0].ToString() },

            };
            
            Datagrid_Diag_Sensors_DG2.ItemsSource = DIAG_Datagrid2_Items;
        }
    }

}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using static EasyModbus.ModbusClient;
using EasyModbus;
using System.Diagnostics.PerformanceData;
using System.Windows.Media.Effects;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ModbusWPFproject
{
    public partial class Window1 : Window
    {
        [STAThread]
       
        public void OpenData(string filename, string path)
        {
            List<string> generator_modes = new List<string>();

            vm.OpenedProgramName = "(" + Path.GetFileNameWithoutExtension(filename) + ")";

            arrayDates = CreateArray(path);

            index = 0;

            List<string> list = new List<string>();

            list = CreateSettingsArray(arrayDates);

            modeGenerator = FillModegenerator(list);
            dataFile = ProcessDataInFile(arrayDates, modeGenerator);

            cmdvm.CMD_Array = dataFile.listCurrentString.Concat(arraywithThreeNulls).ToArray();

            for (int i = 1; i < modeGenerator.listModeGenerators.Count + 1; i++)
            {
                generator_modes.Add("Режим Е" + modeGenerator.numSetSetting[i-1]);
            }

            programm_setting_vm.Programm_Setting_Mode_Array = generator_modes.ToArray();

            SendReadedModesToGenerator();
            SendModeDataToController();
            

            if (filename != null)
            {
                vm.button_UNLOAD_enabled = "True";
                vm.button_START_enabled = "True";
                programm_setting_vm.Combobox_ModeSetting_Enabled = "True";
            }
            else
            {
                WP_Show_Msg_Window("nodata");
            }
        }
        public void SendModeDataToController()
        {
            int[] modesArray = new int[10];

            for (int i = 0; i < modeGenerator.listModeGenerators.Count; i++)
            {
                Thread.Sleep(10);
                /* - - Уставка - - */
                modesArray[0] = modeGenerator.listModeGenerators[i].ustavkaVolts;

                /* - - Длительность пакета - - */
                modesArray[1] = modeGenerator.listModeGenerators[i].relaxationDuration;

                /* - - Дистанция релаксации - - */
                modesArray[2] = ConvertFloatToRegisters(modeGenerator.listModeGenerators[i].relaxationDistance)[0];
                modesArray[3] = ConvertFloatToRegisters(modeGenerator.listModeGenerators[i].relaxationDistance)[1];

                SendMultipleReg(2000 + 10 * i, modesArray);
            }
        }
        public void SendReadedModesToGenerator()
        {
            flag_data_generator_go = true;
            Thread.Sleep(30);
            int[] modesArray = new int[20];

            for (int i = 0; i < modeGenerator.listModeGenerators.Count; i++)
            {
                Thread.Sleep(10);
                /* - - Частота - - */
                modesArray[0] = modeGenerator.listModeGenerators[i].frequancyPulse;

                /* - - Скважность - - */
                modesArray[1] = ConvertFloatToRegisters(modeGenerator.listModeGenerators[i].dutyCycle)[0];
                modesArray[2] = ConvertFloatToRegisters(modeGenerator.listModeGenerators[i].dutyCycle)[1];

                /* - - Длительность пакета - - */
                modesArray[3] = modeGenerator.listModeGenerators[i].packageDuration;

                /* - - Длительность паузы - - */
                modesArray[4] = modeGenerator.listModeGenerators[i].pauseDuration;

                /* - - Рабочий ток - - */
                modesArray[5] = modeGenerator.listModeGenerators[i].operatingCurrent;

                /* - - Поджиговый ток - - */
                modesArray[6] = modeGenerator.listModeGenerators[i].arsonCurrent;

                /* - - Защитный ток - - */
                modesArray[7] = modeGenerator.listModeGenerators[i].proctectiveCurrent;

                /* - - Длительность импульса - - */
                modesArray[11] = modeGenerator.listModeGenerators[i].titaniumPulseDuration;

                /* - - Мощность титанового импульса - - */
                modesArray[12] = modeGenerator.listModeGenerators[i].capacitorCapacity;

                /* - - Мощность вибратора - - */
                //modesArray[14] = modeGenerator.listModeGenerators[i - 1].pauseDuration;

                /* - - Полярность - - */
                //modesArray[15] = modeGenerator.listModeGenerators[i - 1].pauseDuration;

                SendMultipleRegGenerator(40 + 20 * i, modesArray);
            } 
            /* - - Количество режимов - - */
            ModClientGenerator.WriteSingleRegister(27, modeGenerator.listModeGenerators.Count);

            flag_data_generator_go = false;
        }

        private void SendNewModeToGen()
        {
            index_of_mode_now = Convert.ToInt16(modeGenerator.numSetSetting.IndexOf(Convert.ToInt16(registers[57])));
            //ModClientGenerator.WriteSingleRegister(28, index_of_mode_now);
        }

        private void SendMultipleRegGenerator(int adress, int[] value)
        {
            if (ModClientGenerator.Connected == true)
            {
                ModClientGenerator.WriteMultipleRegisters(adress, value);
            }
            else
            {
                Disconnect();
            }
        }

        public int SendOpenedData(int idx, uint n)
        {
            //flag_file_loaded = true;

            int[] FramesArray = new int[30];

            for (int i = 0; i < n; i++)
            {
                FramesArray[0] = Convert.ToInt32(dataFile.Listkadrov.GMove[idx]);
                FramesArray[1] = Convert.ToInt32(dataFile.Listkadrov.BitMask_1[idx]);

                FramesArray[6] = ConvertFloatToRegisters(dataFile.Listkadrov.X[idx], RegisterOrder.HighLow)[1];
                FramesArray[7] = ConvertFloatToRegisters(dataFile.Listkadrov.X[idx], RegisterOrder.HighLow)[0];

                FramesArray[8] = ConvertFloatToRegisters(dataFile.Listkadrov.Y[idx], RegisterOrder.HighLow)[1];
                FramesArray[9] = ConvertFloatToRegisters(dataFile.Listkadrov.Y[idx], RegisterOrder.HighLow)[0];

                FramesArray[10] = ConvertFloatToRegisters(dataFile.Listkadrov.Z[idx], RegisterOrder.HighLow)[1];
                FramesArray[11] = ConvertFloatToRegisters(dataFile.Listkadrov.Z[idx], RegisterOrder.HighLow)[0];

                FramesArray[12] = ConvertFloatToRegisters(dataFile.Listkadrov.A[idx], RegisterOrder.HighLow)[1];
                FramesArray[13] = ConvertFloatToRegisters(dataFile.Listkadrov.A[idx], RegisterOrder.HighLow)[0];

                FramesArray[14] = ConvertFloatToRegisters(dataFile.Listkadrov.B[idx], RegisterOrder.HighLow)[1];
                FramesArray[15] = ConvertFloatToRegisters(dataFile.Listkadrov.B[idx], RegisterOrder.HighLow)[0];

                FramesArray[16] = ConvertFloatToRegisters(dataFile.Listkadrov.C[idx], RegisterOrder.HighLow)[1];
                FramesArray[17] = ConvertFloatToRegisters(dataFile.Listkadrov.C[idx], RegisterOrder.HighLow)[0];

                FramesArray[18] = ConvertFloatToRegisters(dataFile.Listkadrov.R[idx], RegisterOrder.HighLow)[1];
                FramesArray[19] = ConvertFloatToRegisters(dataFile.Listkadrov.R[idx], RegisterOrder.HighLow)[0];

                FramesArray[4] = ConvertFloatToRegisters(dataFile.Listkadrov.F[idx], RegisterOrder.HighLow)[1];
                FramesArray[5] = ConvertFloatToRegisters(dataFile.Listkadrov.F[idx], RegisterOrder.HighLow)[0];

                FramesArray[21] = ConvertFloatToRegisters(dataFile.Listkadrov.N[idx], RegisterOrder.HighLow)[1];
                FramesArray[20] = ConvertFloatToRegisters(dataFile.Listkadrov.N[idx], RegisterOrder.HighLow)[0];

                FramesArray[23] = modeGenerator.numSetSetting.IndexOf(dataFile.Listkadrov.E[idx])+1;


                /*                
                if (modeGenerator.numSetSetting.IndexOf(dataFile.Listkadrov.E[idx]) != -1)
                { 
                    index_of_mode_now = modeGenerator.numSetSetting.IndexOf(dataFile.Listkadrov.E[idx]);

                    FramesArray[24] = modeGenerator.listModeGenerators[index].relaxationDuration;
                    FramesArray[25] = modeGenerator.listModeGenerators[index].ustavkaVolts;

                    FramesArray[26] = ConvertFloatToRegisters(modeGenerator.listModeGenerators[index].relaxationDistance, RegisterOrder.HighLow)[1];
                    FramesArray[27] = ConvertFloatToRegisters(modeGenerator.listModeGenerators[index].relaxationDistance, RegisterOrder.HighLow)[0];

                }*/

                number = dataFile.Listkadrov.N[idx];

                SendMultipleReg(200 + 30 * i, FramesArray);

                idx++;

                if (idx == dataFile.Listkadrov.N.Count)
                {
                    flag_end_of_data = true;
                    return idx;
                }
            }
            return idx;
        }

        public void ReadGenerator()
        {
            while (true) 
            {
                if (flag_data_generator_go == false)
                {
                    Thread.Sleep(40);
                    registers_gen = ModClientGenerator.ReadHoldingRegisters(0, 20);

                    ModClientGenerator.WriteSingleRegister(28, registers[57]);
                   
                    index_of_mode_now = registers_gen[0];
                }
            }
        }

        public void WaitingLoop()
        {
            uint n;

            while (true)
            {
                Thread.Sleep(30); // проверяем раз в 30мс

                countertest++;
                send_data[10] = countertest;
                vm.TextTest = countertest;

                try
                {
                    registers = ModClient.ReadHoldingRegisters(0, 120);

                    SendReg(101, send_data[1]);

/*                    if (flag_data_generator_go == false) registers_gen = ModClientGenerator.ReadHoldingRegisters(0, 20);

                    ModClientGenerator.WriteSingleRegister(28, registers[57]);

                    index_of_mode_now = registers_gen[0];*/

                    
                    //для корректной отправки с кнопок
                    switch (switchtest)
                    {
                        case 0:
                            if (send_data[0] != 0)
                            {
                                SendReg(100, send_data[0]);
                                send_data[0] = 0;
                            }
                            break;
                        case 1:

                            break;
                        case 2:
                            SendReg(102, send_data[2]);
                            break;
                        case 3:
                            if (send_data[3] != 0)
                            {
                                SendReg(103, send_data[3]);
                                send_data[3] = 0;
                            }
                            break;
                        case 4:
                            if (send_data[4] != 0)
                            {
                                SendReg(104, send_data[4]);
                                send_data[4] = 0;
                            }
                            break;
                        case 5:
                            SendReg(105, send_data[5]);
                            break;
                        case 6:
                            SendReg(106, send_data[6]);
                            break;
                        case 7:
                            SendReg(107, send_data[7]);
                            break;
                        case 8:
                            SendReg(108, send_data[8]);
                            break;
                        case 10:
                            SendReg(110, send_data[10]);
                            break;

                        default:
                            break;
                    }

                    switchtest++;

                    if (switchtest >= 11) { switchtest = 0; }

                    //обновление датагридов
                    debugvm.DebugRegsArray = registers;
                    genvm.generatorArray = registers_generator;

                    //выталкиваем UI из этого потока
                    Dispatcher.Invoke(new Action(() =>
                    {
                        VisualValuesDisplay();
                    }
                    ));


                    //отсоединение
                    if (flag_GoDisconnect == true)
                    {
                        ModClient.Disconnect();
                        //VisualValuesReset();
                        break;
                    }

                    //инициализация
                    if ((registers[51] & 0b100000000) == 0)
                    {
                        string[] params_array = new string[6];
                        int[] data = new int[12];
                        params_array = File.ReadAllLines(nt_file2);

                        for (int i = 0; i < params_array.Length; i++)
                        {
                            float Xfloat = Convert.ToSingle(params_array[i]);
                            data[i * 2 + 1] = ConvertFloatToRegisters(Xfloat, RegisterOrder.HighLow)[0];
                            data[i * 2] = ConvertFloatToRegisters(Xfloat, RegisterOrder.HighLow)[1];
                        }

                        SendMultipleReg(120, data);
                        SendReg(104, 4);
                        SendReg(102, 1);
                    }

                    //запись для дальнейшей инициализации
                    if ((registers[51] & 0b10000000) != 0)
                    {
                        if (registers[52] == 1)
                        {
                            int[] regsarray = new int[12];
                            int[] temp = new int[2];
                            float value = 0;

                            List<string> regs_list = new List<string>();

                            regsarray = ModClient.ReadHoldingRegisters(70, 12);

                            for (int i = 0; i < regsarray.Length; i += 2)
                            {
                                temp[0] = regsarray[i];
                                temp[1] = regsarray[i + 1];

                                value = ModbusClient.ConvertRegistersToFloat(temp);

                                regs_list.Add(value.ToString());
                            }

                            File.WriteAllLines(nt_file2, regs_list);
                            SendReg(104, 5);
                        }
                    }

                    //отправка кадров 
                    //    idx++;
                    if (((registers[51] & 0b01) != 0))
                    {
                        flag_need_data = true;
                    }
                    else { flag_need_data = false; }

                    if (((registers[51] & 0b100) != 0))
                    {
                        flag_need_data1 = true;
                    }
                    else { flag_need_data1 = false; }

                    if ((flag_start_btn_pressed == true) & (index == 0) & (flag_need_data == true))
                    {
                        n = 50;

                        index = SendOpenedData(index, n);

                        SendReg(104, 1);
                        SendReg(103, 1);

                        run_program = true;
                        flag_start_btn_pressed = false;
                    }
                    //  else if ((flag_need_data == true)&(run_program == true))
                    else
                    if ((flag_need_data1 == true) & (flag_need_data == true) & (index > 0) & (flag_end_of_data == false))
                    {
                        n = 25;

                        if (flag_need_data2 == false)
                        {
                            index = SendOpenedData(index, n);
                            //ModClientGenerator.WriteSingleRegister(28, modeGenerator.numSetSetting.IndexOf(Convert.ToInt16(registers[57])));
                            SendReg(104, 1);
                            flag_need_data2 = true;
                        }
                    }
                    else
                    {
                        flag_need_data2 = false;
                    }
                }
                catch
                {
                    Thread ConnectWaitingThread = new Thread(() => ConnectWaiting());
                    ConnectWaitingThread.Start();
                    break;
                }
                // return idx;
            }

        }

        private void SendReg(int adress, int value)
        {
            if (ModClient.Connected == true)
            {
                ModClient.UnitIdentifier = 1;
                ModClient.WriteSingleRegister(adress, value);
            }
            else
            {
                //MessageBox.Show("Соединение не установлено!", "Ошибка");
                Disconnect();
            }
        }

        private void SendMultipleReg(int adress, int[] value)
        {
            if (ModClient.Connected == true)
            {
                ModClient.UnitIdentifier = 1;
                ModClient.WriteMultipleRegisters(adress, value);
            }
            else
            {
                //MessageBox.Show("Соединение не установлено!", "Ошибка");
                Disconnect();
            }
        }

        private Task<int[]> ReadRegAsyncGenerator(int address, int quantity)
        {
            return Task.Run(() =>
            {
                return ModClientGenerator.ReadHoldingRegisters(address, quantity);
            });
        }

        private Task<int[]> ReadRegAsync(int address, int quantity)
        {
            return Task.Run(() =>
            {

                return ModClient.ReadHoldingRegisters(address, quantity);
            });
        }

        private async void SendMultipleRegAsync(int address, int[] value)
        {
            await Task.Run(() =>
            {
                ModClient.WriteMultipleRegisters(address, value);
            });
        }

        private int GetReg(int adress, int value)
        {
            int x = 0;
            if (ModClient.Connected == true)
            {
                int[] getReg = ModClient.ReadHoldingRegisters(adress, value);
                x = getReg[0];
            }
            else
            {
                //MessageBox.Show("Соединение не установлено!", "Ошибка");
                Disconnect();
            }
            return x;
        }
        public void SendWritedCMDData()
        {
            if ((GetReg(51, 1) & 0b01) == 1)
            {
                int[] CmdFramesArray = new int[30];

                CmdFramesArray[0] = Convert.ToInt32(listView2.ListGCode[0]);
                CmdFramesArray[1] = Convert.ToInt32(listView2.BitMaskList[0]);

                CmdFramesArray[6] = ConvertFloatToRegisters(listView2.ListX[0], RegisterOrder.HighLow)[1];
                CmdFramesArray[7] = ConvertFloatToRegisters(listView2.ListX[0], RegisterOrder.HighLow)[0];

                CmdFramesArray[8] = ConvertFloatToRegisters(listView2.ListY[0], RegisterOrder.HighLow)[1];
                CmdFramesArray[9] = ConvertFloatToRegisters(listView2.ListY[0], RegisterOrder.HighLow)[0];

                CmdFramesArray[10] = ConvertFloatToRegisters(listView2.ListZ[0], RegisterOrder.HighLow)[1];
                CmdFramesArray[11] = ConvertFloatToRegisters(listView2.ListZ[0], RegisterOrder.HighLow)[0];

                CmdFramesArray[12] = ConvertFloatToRegisters(listView2.ListA[0], RegisterOrder.HighLow)[1];
                CmdFramesArray[13] = ConvertFloatToRegisters(listView2.ListA[0], RegisterOrder.HighLow)[0];

                CmdFramesArray[14] = ConvertFloatToRegisters(listView2.ListB[0], RegisterOrder.HighLow)[1];
                CmdFramesArray[15] = ConvertFloatToRegisters(listView2.ListB[0], RegisterOrder.HighLow)[0];

                CmdFramesArray[16] = ConvertFloatToRegisters(listView2.ListC[0], RegisterOrder.HighLow)[1];
                CmdFramesArray[17] = ConvertFloatToRegisters(listView2.ListC[0], RegisterOrder.HighLow)[0];

                CmdFramesArray[18] = ConvertFloatToRegisters(listView2.ListR[0], RegisterOrder.HighLow)[1];
                CmdFramesArray[19] = ConvertFloatToRegisters(listView2.ListR[0], RegisterOrder.HighLow)[0];

                CmdFramesArray[4] = ConvertFloatToRegisters(listView2.ListF[0], RegisterOrder.HighLow)[1];
                CmdFramesArray[5] = ConvertFloatToRegisters(listView2.ListF[0], RegisterOrder.HighLow)[0];

                SendMultipleReg(200, CmdFramesArray);

                SendReg(104, 1);
                SendReg(103, 1);
            }
        }
    }


}

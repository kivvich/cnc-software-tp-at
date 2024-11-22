using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ModbusWPFproject
{
    /// <summary>
    /// Логика взаимодействия для WP_Window_OperationCancelled.xaml
    /// </summary>
    public partial class WP_Window_OperationCancelled : Window
    {
        WP_Editor_VM vm_editor = new WP_Editor_VM();
        public WP_Window_OperationCancelled(string MsgType)
        {
            InitializeComponent();

            this.DataContext = vm_editor;

            if (MsgType == "error") vm_editor.WP_Editor_OperationText_Textblock = "Операция невозможна";
            if (MsgType == "saved") vm_editor.WP_Editor_OperationText_Textblock = "Файл успешно сохранен";
            if (MsgType == "directorycreated") vm_editor.WP_Editor_OperationText_Textblock = "Директория успешно создана";
            if (MsgType == "filedeleted") vm_editor.WP_Editor_OperationText_Textblock = "Файл успешно удален";
            if (MsgType == "directorydeleted") vm_editor.WP_Editor_OperationText_Textblock = "Директория успешно удалена";
            if (MsgType == "renamed") vm_editor.WP_Editor_OperationText_Textblock = "Файл успешно переименован";
            if (MsgType == "nodata") vm_editor.WP_Editor_OperationText_Textblock = "Файл пуст";
            if (MsgType == "value_wrong") vm_editor.WP_Editor_OperationText_Textblock = "Введенное значение некорректно!";
            
            //работа с данными
            if (MsgType == "ustavka_wrong") vm_editor.WP_Editor_OperationText_Textblock = "Значение уставки вне предела!\n\nУкажите от 0 до 250!";
            if (MsgType == "frequency_wrong") vm_editor.WP_Editor_OperationText_Textblock = "Значение частоты вне предела!\n\nУкажите от 1 до 500!";
            if (MsgType == "dutycycle_wrong") vm_editor.WP_Editor_OperationText_Textblock = "Значение скважности вне предела!\n\nУкажите от 1.05 до 6.6!";
            if (MsgType == "operatingcurrent_wrong") vm_editor.WP_Editor_OperationText_Textblock = "Значение рабочего тока вне предела!\n\nУкажите от 0 до 100!";
            if (MsgType == "arsonCurrent_wrong") vm_editor.WP_Editor_OperationText_Textblock = "Значение подживого тока вне предела!\n\nУкажите от 0 до 15!";
            if (MsgType == "proctectiveCurrent_wrong") vm_editor.WP_Editor_OperationText_Textblock = "Значение частоты вне предела!\n\nУкажите от 0 до 20!";
            if (MsgType == "packageDuration_wrong") vm_editor.WP_Editor_OperationText_Textblock = "Значение длительности пакета\nвне предела!\nУкажите от 10 до 20000!";
            if (MsgType == "pauseDuration_wrong") vm_editor.WP_Editor_OperationText_Textblock = "Значение длительности паузы\nвне предела!\nУкажите от 10 до 20000!";
            if (MsgType == "titaniumPulseDuration_wrong") vm_editor.WP_Editor_OperationText_Textblock = "Значение длительности импульса\nвне предела!\nУкажите от 0 до 100!";
            if (MsgType == "capacitorCapacity_wrong") vm_editor.WP_Editor_OperationText_Textblock = "Значение ёмкости конденсатора\nвне предела!\nУкажите от 0 до 3!";
            if (MsgType == "relaxationDistance_wrong") vm_editor.WP_Editor_OperationText_Textblock = "Значение дистанции релаксации\nвне предела!\nУкажите от 10 до 20000!";
            if (MsgType == "relaxationDuration_wrong") vm_editor.WP_Editor_OperationText_Textblock = "Значение периода релаксации\nвне предела!\nУкажите от 10 до 20000!";
            if (MsgType == "gainFactor_wrong") vm_editor.WP_Editor_OperationText_Textblock = "Значение периода релаксации\nвне предела!\nУкажите от 10 до 20000!";
        }

        private void WP_Editor_Error_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public class WP_Editor_VM : INotifyPropertyChanged
        {
            private string? _WP_Editor_OperationText_Textblock;
            private object? _WP_Editor_Filename_Input_Textblock;

            public string WP_Editor_OperationText_Textblock
            {
                get
                {
                    return _WP_Editor_OperationText_Textblock!;
                }
                set
                {
                    _WP_Editor_OperationText_Textblock = value;
                    NotifyPropertyChanged("WP_Editor_OperationText_Textblock");
                }
            }

            public object WP_Editor_Filename_Input_Textblock
            {
                get
                {
                    return _WP_Editor_Filename_Input_Textblock!;
                }
                set
                {
                    _WP_Editor_Filename_Input_Textblock = value;
                    NotifyPropertyChanged("WP_Editor_Filename_Input_Textblock");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ModbusWPFproject
{
    public partial class Window1 : Window
    {
        public void WP_Show_Msg_Window(string typemsg)
        {
            WP_Window_OperationCancelled wp_editor_msg = new WP_Window_OperationCancelled(typemsg);
            wp_editor_msg.ShowDialog();
        }
    }
}

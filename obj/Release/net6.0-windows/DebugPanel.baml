   M S B A M L   `   `   `  ���� K  GModbusWPFproject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ModbusWPFprojectModbusWPFproject  !    ModbusWPFproject.DebugPanel   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:ModbusWPFproject       � Title$  
DebugPanel��$	��450��$	��800��.��� .+��� $��Center=�$��96��$��0,56,0,0q�
���   �$	��Top=�$	��392���� -    ��Name$ textCommand��$��Center=�$��0,90,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��370��$��22���� $#��Введите команду��$
��Left=�$��
210,68,0,0q�$	��Top=�$��98��$��14(� J� RenderTransformOrigin$ 0.5,0.5&�$!��Bahnschrift Light Condensed)�z�V� .~��� �� ��  �� Angle$ 0.643Q�T� �� -    ��Name$ BtnCmd��$'��!Отправить команду��$��Center=�$��	0,120,0,0q�$	��Top=�$	��370��$��23��$!��Bahnschrift Light Condensed)��� $��Center=�$	��100��$��	0,169,0,0q�
���   �$	��Top=�$	��392���� -   $ btnFileOpen��$��Открыть файл��$��Center=�$��	0,226,0,0q�$	��Top=�$	��102��$��24��$!��Bahnschrift Light Condensed)��� -   $ textFileOpened��$��	0,191,0,0q�$
 Wrap=�$	��Top=�$��20��$��Center=�$	��328���� $��Center=�$	��100��$��	0,296,0,0q�
���   �$	��Top=�$	��392���� -   $ 	textRegGo��$
��Left=�$��367,314,0,0q�$
 Wrap=�$	��Top=�$	��120��$��0��$��20��$!��Bahnschrift Light Condensed)��� $��Регистр��$
��Left=�$��311,312,0,0q�$	��Top=�$��26��$��52��$!��Bahnschrift Light Condensed)��� -   $ BtnSend��$��Отправить��$
��Left=�$��309,365,0,0q�$	��Top=�$	��176��$��23��$!��Bahnschrift Light Condensed)��� -   $ textRegValue��$
��Left=�$��367,335,0,0q�$
 Wrap=�$	��Top=�$	��120��$��0��$��22��$!��Bahnschrift Light Condensed)��� $��Значение��$
��Left=�$��311,335,0,0q�$	��Top=�$��26��$��52��$!��Bahnschrift Light Condensed)��� $��Отправка��$��Center=�$��	0,295,0,0q�$	��Top=�$��58��$��14(�$!��Bahnschrift Light Condensed)��� -   $ btnCloseForm��$��Х��$
��Left=�$��
552,10,0,0q�$	��Top=�$��16(�$��37��$��44��
   M S B A M L   `   `   `  ���� K  GModbusWPFproject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ModbusWPFprojectModbusWPFproject  !    ModbusWPFproject.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:ModbusWPFproject     _ [MaterialDesignThemes.Wpf, Version=5.0.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes     MaterialDesignBody#���   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���   � Title$ Modbus�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � WindowState$ 	Maximized=�$	��493��$	��958��.��� $��Center=�$��Center=�$	��493��$	��958�� � ColumnDefinitions �� $	��958 � � RowDefinitions �� .+��� $
��Left=�$	��154��$��321,123,0,0q�
���   �$	��Top=�$	��321�� �� RadiusX$ 2��	 �� RadiusY$	 2���� -   
 ��Name$
 textPort��$
��Left=�$��422,157,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��194��$
��COM8��$��33��$!��Bahnschrift Light Condensed)�$��20(��� $��Порт��$
��Left=�$��344,154,0,0q�$	��Top=�$��42��$��61��$!��Bahnschrift Light Condensed)�$��20(��� -    ��Name$ 
BtnConnect��$��Соединиться��$��Center=�$��	0,227,0,0q�$	��Top=�$	��272��$��36��$!��Bahnschrift Light Condensed)�$��20(��� -    ��Name$ 
textStatus��$��Статус��$
��Left=�$��344,190,0,0q�$	��Top=�$��28��$��74��$!��Bahnschrift Light Condensed)�$��20(��� -   $ textConnection��$1��+Соединение отсутствует��$
��Left=�$��418,190,0,0q�$	��Top=�$��20(�$��34��$	��195��
���  ��$!��Bahnschrift Light Condensed)���Z� �� $��Соединение��$
��Left=�$��438,128,0,0q�$	��Top=�$��88��$��20(�$!��Bahnschrift Light Condensed)�$��29���� -   $ btnFileOpenPanel��$%��Панель оператора��$
��Left=�$��321,296,0,0q�$	��Top=�$	��321��$��38��$!��Bahnschrift Light Condensed)� J� RenderTransformOrigin$ 0.5,0.5&�$��20(�${�False��z�V� .~��� �� ��  �� Angle$ 0Q�T� �� -   $ btnFileOpenDebugPanel��$!��Панель отладки��$
��Left=�$��321,351,0,0q�$	��Top=�$	��321��$��38��$!��Bahnschrift Light Condensed)�$ 0.5,0.5&�$��20(�${�False��z�V� .~��� �� �� $ 0Q�T� 
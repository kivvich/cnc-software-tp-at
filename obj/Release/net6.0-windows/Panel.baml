   M S B A M L   `   `   `  ���� K  GModbusWPFproject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ModbusWPFprojectModbusWPFproject  T PSystem.Memory, Version=6.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51@0clr-namespace:System.Text;assembly=System.MemorySystem.Text     ModbusWPFproject.Panel   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TGu.Wpf.DataGrid2D, Version=0.2.4.0, Culture=neutral, PublicKeyToken=34988ea4e73235be(
dataGrid2Dhttp://gu.se/DataGrid2D  *localclr-namespace:ModbusWPFproject  _ [MaterialDesignThemes.Wpf, Version=5.0.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  ;Text0clr-namespace:System.Text;assembly=System.Memory    � Title$  Panel�� � WindowStyle$
 None=�   MaterialDesignWindow#��[   � 
ResizeMode$ NoResize=� � WindowState$ 	Maximized=�$	��840��$
��1637��.��� ���	#FF31363F � RowDefinitions �� $��128.667 ��� $��711.333 � � ColumnDefinitions �� .+��� $��967,114,0,0q�$��Inter)�$��20(����	#FFEEEEEE$	��Top=�$
��Left=�$	��716��$	��518��$��2������ ���	#FFEEEEEEОтладка.��&   System.Windows.Controls.DataGrid  -     Name$ dataGrid��   FrozenColumnCount$ 1��$��
0,10,0,318q�$	��151��   AutoGenerateColumns$ False��$
��Left=����� *DebugRegsArray+	   RowStyle	 �� 
 �� 
TargetType)  #System.Windows.Controls.DataGridRow"
  .���  �� Property �@J�IsHitTestVisible �� Value$ False��   Columns 0  *System.Windows.Controls.DataGridTextColumn  1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� 	Converter *   $ModbusWPFproject.RowToIndexConverter   �� RelativeSource ��  �� AncestorType"    ,  &System.Windows.Controls.DataGridColumn   Header$ Значение�� ��  �� UpdateSourceTrigger$ PropertyChanged=�  -   $ dataGridregs2��$ 1��$��1136,49,0,336q�$	��152��$ False��$
��Left=�$��1������ *generatorArray+	 �� "
  .���  �@J�IsHitTestVisible$ False��    ��     �� "    $ Значение G�� �� $ PropertyChanged=��� $��720,114,0,0q�$��Inter)�$��20(����	#FFEEEEEE$	��Top=�$
��Left=�$	��234��$	��216��$��2������ ���	#FFEEEEEEРежимы�� $��
52,114,0,0q�$��Inter)�$��20(����	#FFEEEEEE$	��Top=�$
��Left=�$	��234��
�������$��2������ ���	#FFEEEEEE'%Машинные координаты�� -    ��Name$ btnCloseApp��$
��Left=�$��1375,652,0,0q�$	��Top=�$��48(�$��43��$��48�����	#FFDC3D3D���	#FFB73A3A$��0,0,0,0q����	#FFEEEEEE$��1����0  *MaterialDesignThemes.Wpf.PackIconExtension  *GuyFawkesMask+   Size 30�� -   $��X��$
��Left=�$��	77,32,0,0q�$	��Top=�$��36(�$��1������ *	Color_X+$ PropertyChanged=��� -    ��Name$ textCoordMachineX��$
��Left=�$��
118,32,0,0q�$	��Top=�$��36(�$��1������ *Coordinates_Machine_X+�� -   $��Y��$
��Left=�$��	77,84,0,0q�$	��Top=�$��36(�$��1������ *	Color_Y+$ PropertyChanged=��� -   $ textCoordMachineY��$
��Left=�$��
118,84,0,0q�$	��Top=�$��36(�$��1������ *Coordinates_Machine_Y+�� -   $��Z��$
��Left=�$��
77,133,0,0q�$	��Top=�$��36(�$��1������ *	Color_Z+$ PropertyChanged=��� -	   $ textCoordMachineZ��$
��Left=�$��118,133,0,0q�$	��Top=�$��36(�$��1������ *Coordinates_Machine_Z+�� $��322,114,0,0q�$��Inter)�$��20(�$	��Top=�$
��Left=�$	��234��$	��365�����	#FFEEEEEE
�������$��2������ ���	#FFEEEEEE%#Рабочие координаты�� $��X��$
��Left=�$��
349,32,0,0q�$	��Top=�$��36(�$��1������ *	Color_X+$ PropertyChanged=��� -
   $ textCoordWorkX��$
��Left=�$��
404,32,0,0q�$	��Top=�$��36(�$��1������ *Coordinates_Work_X+�� $��Y��$
��Left=�$��
349,84,0,0q�$	��Top=�$��36(�$��1������ *	Color_Y+$ PropertyChanged=��� -   $ textCoordWorkY��$
��Left=�$��
404,84,0,0q�$	��Top=�$��36(�$��1������ *Coordinates_Work_Y+�� $��Z��$
��Left=�$��349,133,0,0q�$	��Top=�$��36(�$��1������ *	Color_Z+$ PropertyChanged=��� -   $ textCoordWorkZ��$
��Left=�$��404,133,0,0q�$	��Top=�$��36(�$��1������ *Coordinates_Work_Z+�� -   $ 
btnLoadPRG��$��Загрузить��$��Inter)�$
��Left=�$	��Top=�$��80��$	��138��$��22(�$��	52,22,0,0q����	#FF76ABAE���	#FF76ABAE���	#FFEEEEEE$��0,0,0,0q�{��� *button_LOAD_enabled+�� -   $ btnUnloadPRG��$��Выгрузить��$��Inter)�$
��Left=�$	��Top=�$��80��$	��140��$��22(�$��
203,22,0,0q����	#FF76ABAE���	#FF76ABAE���	#FFEEEEEE$��0,0,0,0q�{��� *button_UNLOAD_enabled+	� -    	�Name$ 	radioAuto��$��Inter)�$
��AUTO��$
��Left=�$��
736,38,0,0q�$	��Top=�$��24(�$
��Bold"����	#FF653FA7 d� 	IsChecked$ FalseY�$��1��{��� *radiobutton_AUTO_enabled+	� -   $ radioMDI��$��Inter)�$	��MDI��$
��Left=�$��736,103,0,0q�$	��Top=�$��24(�$
��Bold"�$��1��{��� *radiobutton_MDI_enabled+	� -   $ 	radioHOME��$��Inter)�$
��HOME��$
��Left=�$��736,171,0,0q�$	��Top=�$��24(�$
��Bold"�$��1��{��� *radiobutton_HOME_enabled+	� -   $ radioManual��$��Inter)�$��MANUAL��$
��Left=�$��
736,72,0,0q�$	��Top=�$��24(�$
��Bold"�$��1��{��� *radiobutton_MANUAL_enabled+	� -   $ radioMeasure��$��Inter)�$��MEASURE��$
��Left=�$��736,137,0,0q�$	��Top=�$��24(�$
��Bold"�$��1��{��� *radiobutton_MEASURE_enabled+�� $��720,245,0,0q�$��Inter)�$��20(����	#FFEEEEEE$	��Top=�$
��Left=�$	��124��$	��216��$��1������ ���	#FFEEEEEEПодача�� -    ��Name$ sliderSupply��$
��Left=�$��725,322,0,0q�$	��Top=�$	��206�� �� TickPlacement$
 None=� � Maximum$
 1000Q�$��31�� � SmallChange$ 10Q� � LargeChange$	 100Q� �� TickFrequency$ 10Q�$��1�� � Value �� *Sliders_Supply+{��� *slider_Supply_enabled+�� $2��,0                     500               1000��$
��Left=�$��725,295,0,0q�$	��Top=�$��14(�$	��211��$��1���� $��720,396,0,0q�$��Inter)�$��20(����	#FFEEEEEE$	��Top=�$
��Left=�$	��124��$	��216��$��1������ ���	#FFEEEEEEУставка (В)�� -   $ sliderSetting��$
��Left=�$��725,473,0,0q�$	��Top=�$	��206��$
 None=�$	 200Q�$��31��$ 1Q�$ 0Q�  �� IsSnapToTickEnabled$  False��$��1�� �� *Sliders_Setting+{��� *slider_Setting_enabled+�� $/��)0         50        100      150      200��$
��Left=�$��725,446,0,0q�$	��Top=�$��14(�$	��211��$��1���� -   $ btnStart��$��Запуск��$��Inter)�$
��Left=�$	��Top=�$��41��$	��216��$��22(�$��720,542,0,0q����	#FF56AB4D���	#FF77C183���	#FFEEEEEE$��1��{��� *button_START_enabled+�� -   $ btnStop��$��Стоп��$��Inter)�$
��Left=�$	��Top=�$��42��$	��216��$��22(�$��720,659,0,0q����	#FFBB5959���	#FFB73A3A���	#FFEEEEEE$��1��{��� *button_STOP_enabled+�� -   $ btnGoConnect��$��
Связь��$
��Left=�$��1427,674,0,0q�$	��Top=�$��8(�$��21��$��24��$��Center=�$��Center=�$��0,0,0,0q�
���  �����	#DD000000${�False��$��1������ ! �� EndPoint$! 0.5,1&�" �� 
StartPoint$" 0.5,0&�.��� # � Color$# 	#FFE2E2E2��� $# 	#FFBEBEBE��$ � Offset$$ 1Q��� -   $ textPodacha��$
��Left=�$��839,252,0,0q�$	��Top=����	#DDFFFFFF$��22(�$��38��$��92��$��0,0,0,0q�$��Right=�$��1������ *Labels_Podacha+�� -   $ textUstavka��$
��Left=�$��839,404,0,0q�$	��Top=�$��22(����	#DDFFFFFF$��36��$��92��$��Right=�$��0,0,0,0q�$��1������ *Labels_Ustavka+�� -   $ textMODE��$
��Left=�$��906,117,0,0q�$	��Top=����	#DDFFFFFF$��22(�$��38��$��2��$��27������ *Labels_Mode+�� -   $ btnPause��$��
Пауза��$��Inter)�$
��Left=�$	��Top=�$��42��$	��216��$��22(�$��720,601,0,0q����	#FFD0BA70���	#FFB39843���	#FFEEEEEE$��1��{��� *button_PAUSE_enabled+�� -   $ btnXplus��$��X+��$
��Left=�$	��Top=�$��52��$��52��$��16(�$��1136,463,0,0q�$��Normal"�$��1�����	#FFEEEEEE{��� *button_X_plus_enabled+�� -   $ btnXmin��$��X-��$
��Left=�$	��Top=�$��52��$��52��$��16(�$��994,463,0,0q�$��Normal"�$��1�����	#FFEEEEEE{��� *button_X_min_enabled+�� -   $ btnYplus��$��Y+��$
��Left=�$	��Top=�$��52��$��52��$��16(�$��1066,396,0,0q�$��Normal"�$��1�����	#FFEEEEEE{��� *button_Y_plus_enabled+�� -    $ btnYmin��$��Y-��$
��Left=�$	��Top=�$��52��$��52��$��16(�$��1066,531,0,0q�$��Normal"�$��1�����	#FFEEEEEE{��� *button_Y_min_enabled+�� -!   $ btnZplus��$��Z+��$
��Left=�$	��Top=�$��52��$��52��$��16(�$��1136,396,0,0q�$��Normal"�$��1�����	#FFEEEEEE{��� *button_Z_plus_enabled+�� -"   $ btnZmin��$��Z-��$
��Left=�$	��Top=�$��52��$��52��$��16(�$��1136,531,0,0q�$��Normal"�$��1�����	#FFEEEEEE{��� *button_Z_min_enabled+�� -#   $ btnSPDswitch��$��I��$
��Left=�$	��Top=�$��52��$��52��$��16(�$��1066,463,0,0q�$��Normal"�$��1�����	#FFEEEEEE{��� *button_SPDswitch_enabled+�� -$   $ btnSmth2��$��C+��$
��Left=�$	��Top=�$��52��$��52��$��16(�$��994,396,0,0q�$��Normal"����	#FF76ABAE$��1�����	#FFEEEEEE{��� *button_Smth2_enabled+�� -%   $ btnSmth1��$��C-��$
��Left=�$	��Top=�$��52��$��52��$��16(�$��994,531,0,0q�$��Normal"�$��1�����	#FFEEEEEE{��� *button_Smth1_enabled+�� -&   $ textBox50reg��$
��Left=�$��1212,397,0,0q�$	��Top=�$	��245��$��20(�$��32��$��0,0,0,0q�$��1������ *Registers_50+�� -'   $ textBox52reg��$
��Left=�$��1212,429,0,0q�$	��Top=�$	��245��$��20(�$��34��$��0,0,0,0q�$��1������ *Registers_52+�� -(   $ Work_X_goNull��$��X��$
��Left=�$��
623,45,0,0q�$	��Top=�% � Cursor$
% Hand��$��1��y��� *button_X_null_visible+�� -)   $ Work_Y_goNull��$��X��$
��Left=�$��
623,96,0,0q�$	��Top=�$��1��y��� *button_Y_null_visible+�� -*   $ Work_Z_goNull��$��X��$
��Left=�$��623,145,0,0q�$	��Top=�$��1��y��� *button_Z_null_visible+  -+   $ datagridCompilate��$��1296,47,0,0q�$ False��$	��159��$	��326��$
��Left=�$	��Top=�$��1��   $	 №��&   Width5  /System.Windows.Controls.DataGridLengthConverter$& 45 '   FontSize$' 9Q� �� *numPosition+  $ Ошибка��$	& 500  �� *stringError+�� -,   $ 	labelTest��$
��Left=�$��1212,458,0,0q�$	��Top=�$	��169��$��34��$��20(�$��0,0,0,0q�$��1������ *Labels_Test+  --   $ Datagrid_CmdLines��(   HeadersVisibility$
( None=�$��
52,308,0,0q�$ False��$	��635��$	��393��$
��Left=�$	��Top=����	#FFEEEEEE$��1������ *	CMD_Array+	 �� "
  .���  �@J�IsHitTestVisible$ False��    ��  +	   %ModbusWPFproject.RowToIndexConverter2	   �� "     �� $ PropertyChanged=�   �� *PercentageOfFrame+$ PropertyChanged=�4
  .System.Windows.Controls.DataGridTemplateColumn
  $	& 150 ) 
  CellTemplate) �� .R�� .+�� * �Name$* progressbar��+ � Minimum$+ 0Q�$	 100Q�$��20�� �� *PercentageOfFrame+, �� Mode$, OneWay=�$ PropertyChanged=��� $��Center=�$��Center=����� *PercentageOfFrame+- �� StringFormat$
- {0}%���� -.   . ��Name$. combobox��$
��Left=�$��1427,652,0,0q�$	��Top=�$��52��$��21��$��16(�${�False��$��1���� -/   $ btnReloadCOM��$	��↵��$
��Left=�$��1455,674,0,0q�$	��Top=�$��16(�$��21��$��24�����	#FF673AB7$��	16,0,16,0q����	#FF673AB7${�False��$��1������ $! 0.5,1&�$" 0.5,0&�.��� $# 	#FFE2E2E2��� $# 	#FFBEBEBE��$$ 1Q��� -0   $ 	textSTATE��$
��Left=�$��1136,14,0,0q�$	��Top=�$��72(�$��79��$	��349��$��0,0,0,0q�$��Right=����	#FFEEEEEE���� *Labels_State+�� -1   $ 	textSPEED��$
��Left=�$��1212,499,0,0q�$	��Top=�$��22(�$��34��$	��211��$��0,0,0,0q�$��1������ *Labels_Speed+�� -2   $��txtBoxInput��$
��Left=�$��
52,245,0,0q�/ �� TextWrapping$
/ Wrap=�$	��Top=�$	��544��$��46��$��20(�$��Yu Gothic UI)����	#FFEEEEEE$
��Left=�$��Center=�$��0,0,0,0q�$��1���� -3   $��btnInput��$��Go��$��Inter)�$
��Left=�$��607,245,0,0q�$	��Top=�$��80��$��45��$��1�����	#FFEEEEEE�� -4   $ labelVariables��$
��Left=�$��1212,538,0,0q�$	��Top=�$��1������ *Labels_Variables+�� -5   0 ��Name$0 checkbox_Power1��$��Power��$
��Left=�$��994,601,0,0q�$	��Top=�$��18(�$��0,0,0,3q�$ FalseY�$��1���� -6   $0 checkbox_Power2��$��Adaptive Supply��$
��Left=�$��994,622,0,0q�$	��Top=�$��18(�$��0,0,0,3q�$��1���� -7   $0 checkbox_Power3��$��
Relaxation��$
��Left=�$��994,643,0,0q�$	��Top=�$��18(�$��0,0,0,3q�$��1���� -8   1 ��Name$1 textboxTest108��$
��Left=�$��1036,670,0,0q�$
/ Wrap=�$	��108��$	��Top=�$��37��$��1���� -9   $��go��$
��Left=�$��1084,670,0,0q�$	��Top=�$��25��$��36��$��
0,-0.2,0,0q�$��1�����	#FFEEEEEE�� -:   $1 txbtest��$
��Left=�$��994,670,0,0q�$
/ Wrap=�$	��Top=�$��33��$��1������ *
TextTest+� -;   $* progressbarTok��$
��Left=�$��14��$��1212,478,0,0q�$��1��$	��Top=�$	��152�����	#FF76ABAE$	 200Q� �� *progressBar_Tok_value+
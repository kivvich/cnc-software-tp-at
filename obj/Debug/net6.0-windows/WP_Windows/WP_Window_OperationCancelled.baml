   M S B A M L   `   `   `  ����K  GModbusWPFproject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ModbusWPFprojectModbusWPFproject  5   	   3    -ModbusWPFproject.WP_Window_OperationCancelled   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   L HWindowExtras.Wpf, Version=1.0.29.0, Culture=neutral, PublicKeyToken=null7winex+https://github.com/yariker/WindowExtras.Wpf  5   	   *localclr-namespace:ModbusWPFproject  5   	     � Title$8  2Ошибка выполнения операции��5	   	   $	��250��6,   $	��450��69    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � AllowsTransparency$
 True��5   	   
������   WindowExtras.Wpf.WindowEx   WindowShadow 5      +  %WindowExtras.Wpf.Shadows.WindowShadow  5   
      OffsetY$ 0Q�6      Opacity$	 0.2Q�6)      Radius$ 60Q�67   .��5      �� ���#EEEEEE	 �� CornerRadius$	 20��6"   ._�5   
   �� $	 20��6   $��7q�6#   $��1q�6.   
�������._�5      � -   5        
 �Name$
 WP_Editor_Error_Grid��5       � RowDefinitions 5      �� 5      $��* �6$   �� 5      $
��0.8* �6$   .+�5      �� $��22(�6O   $��Inter)�6]   $��Center=�6�   $��0��6�   $��Center=�6�   $��0,30,0,0q�6�    �� TextAlignment$ Center=�6  ��6   �� *#!WP_Editor_OperationText_Textblock+��6p   �� *)'WP_Editor_Directoryname_Input_Textblock+6  �� 5      -   5         ��Name$ WP_Editor_Error_Button_Go��5      $��22(�6<   $��Inter)�6J   
�������$	��100��6p   $��50��6|   $��Center=�6�   $��Center=�6�   $��0,0,0,30q�6�   $
��ОК��6�   $��1��6�   5      5      5      5       
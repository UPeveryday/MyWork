   M S B A M L   `   `   `  ����D  @SCEEC.TTM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:SCEEC.TTM	SCEEC.TTM  5   	        SCEEC.TTM.JobSettingWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:SCEEC.TTM     5   	   -   5           
WizardForm#���   5   	     � Title$  JobSettingWindow��5	   	   $	��600��6"   $
��1000��6/   $��18(�6<   $
��1000��6J   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � -   5        $��0��5       � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5      �� $��Resources/job.png��6   $��20��6/   $��0��6;   $��5q�6K   �� 5      -   5         ��Name$ TitleTextBlock��5      $��试验任务单��60   $��1��6=   $��5q�6M    	 Title#���  6X   �� 5      -   5         ��Name$ minimumButton��5      $��2��6,   $��35��6<   .��5      �� $��Resources/minimum.png��6   $��10q�67   5      �� 5      -   5        $ maximumButton��5      $��3��6,   $��35��6<   .��5      �� -   5         ��Name$ maximumButtonImage��5      $��Resources/maximum.png��64   $��10q�6S   5      �� 5       -   5        $ closeButton��5       $��4��6*   $��35��6:   .��5!      �� $��Resources/close.png��6   $��10q�65   5"      5#      � 5$   
   $��1��6    5%      �� 5&      $��4* �6#   �� 5'      $��5* �6#   .+�5)      �� $��Visible=�6   $��0��6I   .@�5*      �� .��5+      � $��20,5,5,5q�6    5,      �� 5-      $
��Auto �6/   �� 5.      $��* �6/    50      �� 51      $
��Auto �6,   �� 52      $
��Auto �6,   .+�54      �� $��变压器出厂序号: ��6$   $��0��65   $��0��6B   $��5q�6R   �� 55      -   5         ��Name$  TransformerSerialNoTextBox��55   "   $	��150��6F    �� 
IsReadOnly . $��0��6g   $��1��6t   $��5q�6�   56      57      �� 58      .��59      � $��20,5,5,5q�6    5:      �� 5;      $
��Auto �6/   �� 5<      $��* �6/    5>      �� 5?      $
��Auto �6,   �� 5@      $
��Auto �6,   .+�5B      �� $��试验任务单名称: ��6$   $��0��65   $��0��6B   $��5q�6R   �� 5C      -	   5        $ JobNameTextBox��5C   "   $	��150��6:   $��0��6I   $��1��6V   $��5q�6f   5D      5E      �� 5F      .��5G      9� $��绕组绝缘性能试验��6    9� 
IsExpanded . .��5H      �� $��20,5,5,5q�6%   .+�5I      �� -
   5        	 ��Name$!	 WindingDCInsulationCheckBox��5I   '   $A��;直流绝缘性能(绝缘电阻、吸收比及极化指数)��6L   
 d� 	IsChecked$

 TrueY�6l   $��25��6}   $��5q�6�   6�   �� 5J      $��35,0,0,0q�6)   .+�5K   "   � .+�5L   &   �� $��测试电压: ��60   $��5q�6>   �� 5M   &   -   5         ��Name$% DCInsulationTestVoltageComboBox��5M   /    �� SelectedIndex$ 9��6X   .@�5N   *   �� $
��250V��67   �� 5O   *   $
��500V��67   �� 5P   *   $��1.0kV��67   �� 5Q   *   $��1.5kV��67   �� 5R   *   $��2.0kV��67   �� 5S   *   $��2.5kV��67   �� 5T   *   $��3.0kV��67   �� 5U   *   $��3,5kV��67   �� 5V   *   $��4.0kV��67   �� 5W   *   $��5.0kV��67   �� 5X   *   $��7.5kV��67   �� 5Y   *   $
��10kV��67   5Z   '   5[   #   � 5\   "   .+�5]   &   �� $ ��绝缘电阻通过下限: ��60   $��5q�6B   �� 5^   &   -   5        $ DCInsulationResThTextBox��5^   .   $��3��6P    �� TextAlignment$ Right=�6Y   $��50��6o   $��5q�6z   �� 5_   &   $	��GΩ��60   $��5q�6:   5`   #   � 5a   "   .+�5b   &   �� $��吸收比通过下限: ��60   $��5q�6A   �� 5c   &   -   5        $ DCInsulationARTextBox��5c   .   $	��1.3��6M   $ Right=�6X   $��50��6n   $��5q�6y   5d   #   �� 5e   "   -   5        $#	 HVWindingDCInsulationCheckBox��5e   +   $��高压绕组��6R   $

 TrueY�6a   $��25��6r   $��5q�6~   6�   �� 5f   "   -   5        $#	 MVWindingDCInsulationCheckBox��5f   +   $��中压绕组��6R   $

 TrueY�6a   $��25��6r   $��5q�6~   6�   �� 5g   "   -   5        $#	 LVWindingDCInsulationCheckBox��5g   +   $��低压绕组��6R   $

 TrueY�6a   $��25��6r   $��5q�6~   6�   5h      �� 5i      -   5        $ 	 WindingCapacitanceCheckBox��5i   '   $*��$绕组电容量及介质损耗因数��6K   $

 TrueY�6b   $��25��6s   $��5q�6   6�   �� 5j      $��35,0,0,0q�6)   .+�5k   "   � .+�5l   &   �� $��测试电压: ��60   $��5q�6>   �� 5m   &   -   5        $$ CapacitanceTestVoltageComboBox��5m   /   $ 0��6W   .@�5n   *   �� $��0.5 kV��67   �� 5o   *   $��1.0 kV��67   �� 5p   *   $��1.5 kV��67   �� 5q   *   $��2.0 kV��67   �� 5r   *   $��2.5 kV��67   �� 5s   *   $��3.0 kV��67   �� 5t   *   $��3.5 kV��67   �� 5u   *   $��4.0 kV��67   �� 5v   *   $��4.5 kV��67   �� 5w   *   $��5.0 kV��67   �� 5x   *   $��5.5 kV��67   �� 5y   *   $��6.0 kV��67   �� 5z   *   $��6.5 kV��67   �� 5{   *   $��7.0 kV��67   �� 5|   *   $��7.5 kV��67   �� 5}   *   $��8.0 kV��67   �� 5~   *   $��8.5 kV��67   �� 5   *   $��9.0 kV��67   �� 5�   *   $��9.5 kV��67   �� 5�   *   $��10 kV��67   5�   '   5�   #   �� 5�   "   -   5        $"	 HVWindingCapacitanceCheckBox��5�   +   $��高压绕组��6Q   $

 TrueY�6`   $��25��6q   $��5q�6}   6�   �� 5�   "   -   5        $"	 MVWindingCapacitanceCheckBox��5�   +   $��中压绕组��6Q   $

 TrueY�6`   $��25��6q   $��5q�6}   6�   �� 5�   "   -   5        $"	 LVWindingCapacitanceCheckBox��5�   +   $��低压绕组��6Q   $

 TrueY�6`   $��25��6q   $��5q�6}   6�   5�      5�      5�      5�      �� 5�      .��5�      9� $��绕组导通性能试验��6    . .��5�      �� $��20,5,5,5q�6%   .+�5�      �� -   5        $!	 WindingDCResistanceCheckBox��5�   '   $��绕组直流电阻��6L   $

 TrueY�6]   $��25��6n   $��5q�6z   6�   �� 5�      $��35,0,0,0q�6)   .+�5�   "   � .+�5�   &   �� $��测试电流: ��60   $��5q�6>   �� 5�   &   -   5        $! DCResistanceCurrentComboBox��5�   /   $ 0��6T   .@�5�   *   �� $	��1 A��67   �� 5�   *   $	��3 A��67   �� 5�   *   $
��10 A��67   5�   '   5�   #   �� 5�   "   -   5        $#	 HVWindingDCResistanceCheckBox��5�   +   $��高压绕组��6R   $

 TrueY�6a   $��25��6r   $��5q�6~   6�   �� 5�   "   -   5        $#	 MVWindingDCResistanceCheckBox��5�   +   $��中压绕组��6R   $

 TrueY�6a   $��25��6r   $��5q�6~   6�   �� 5�   "   -   5        $#	 LVWindingDCResistanceCheckBox��5�   +   $��低压绕组��6R   $

 TrueY�6a   $��25��6r   $��5q�6~   6�   5�      5�      5�      5�      �� 5�      -   5         ��Name$ BushingListBoxItem��5�      .��5�      9� $��套管绝缘性能试验��6    . .��5�      �� $��20,5,5,5q�6%   .+�5�      � .+�5�   "   �� $��测试电压: ��6,   $��5q�6:   �� 5�   "   -   5        $, &BushingDCInsulationTestVoltageComboBox��5�   +   $ 9��6[   .@�5�   &   �� $
��250V��63   �� 5�   &   $
��500V��63   �� 5�   &   $��1.0kV��63   �� 5�   &   $��1.5kV��63   �� 5�   &   $��2.0kV��63   �� 5�   &   $��2.5kV��63   �� 5�   &   $��3.0kV��63   �� 5�   &   $��3,5kV��63   �� 5�   &   $��4.0kV��63   �� 5�   &   $��5.0kV��63   �� 5�   &   $��7.5kV��63   �� 5�   &   $
��10kV��63   5�   #   5�      �� 5�      -   5        $!	 BushingDCInsulationCheckBox��5�   '   $$��套管末屏对地绝缘电阻��6L   $

 TrueY�6a   $��25��6r   $��5q�6~   6�   � 5�      .+�5�   "   �� $��测试电压: ��6,   $��5q�6:   �� 5�   "   -   5        $+ %BushingCapacitanceTestVoltageComboBox��5�   +   $ 0��6Z   .@�5�   &   �� $��0.5 kV��63   �� 5�   &   $��1.0 kV��63   �� 5�   &   $��1.5 kV��63   �� 5�   &   $��2.0 kV��63   �� 5�   &   $��2.5 kV��63   �� 5�   &   $��3.0 kV��63   �� 5�   &   $��3.5 kV��63   �� 5�   &   $��4.0 kV��63   �� 5�   &   $��4.5 kV��63   �� 5�   &   $��5.0 kV��63   �� 5�   &   $��5.5 kV��63   �� 5�   &   $��6.0 kV��63   �� 5�   &   $��6.5 kV��63   �� 5�   &   $��7.0 kV��63   �� 5�   &   $��7.5 kV��63   �� 5�   &   $��8.0 kV��63   �� 5�   &   $��8.5 kV��63   �� 5�   &   $��9.0 kV��63   �� 5�   &   $��9.5 kV��63   �� 5�   &   $��10 kV��63   5�   #   5�      �� 5�      -   5        $ 	 BushingCapacitanceCheckBox��5�   '   $1��+绕组-末屏电容量及介质损耗因数��6K   $

 TrueY�6e   $��25��6v   $��5q�6�   6�   5�      5�      5�      �� 5�      -    5        $ OLTCListBoxItem��5�      .��5�      9� $��有载分接开关试验��6    . .��5�      �� $��20,5,5,5q�6%   .+�5�      �� -!   5        $	 OLTCCheckBox��5�   '   $'��!对有载分接开关进行试验��6=   $

 TrueY�6S   $��25��6d   $��5q�6p   6�   � 5�      -"   5         �Name$ OLTCRangeWrapPanel��5�   (   $��35,0,0,0q�6D   .+�5�   "   �� $&�� 试验位置范围: 主分接± ��6,   �� 5�   "   -#   5        $ OLTCRangeTextBox��5�   *   $��8��6D   $��30��6M   $ Right=�6[    �� IsInputMethodEnabled .  6�   �� 5�   "   $�� 位置��6,   5�      �� 5�      -$   5        $	 OLTCDCResistanceCheckBox��5�   '   $��分接位置直流电阻��6I   $

 TrueY�6\   $��25��6m   $��40,5,5,5q�6y   6�   �� 5�      -%   5        $	 OLTCSwitchingCheckBox��5�   '   $*��$分接开关动特性及过渡电阻��6F   $

 TrueY�6]   $��25��6n   $��40,5,5,5q�6z   6�   5�      5�      5�      5�      � 5�      $��1��6    5�      �� 5�      $
��184* �6'   �� 5�      $	��93* �6'    5�      �� 5�      $
��Auto �6$   �� 5�      $��* �6$   .+�5�      �� $��试验操作列表：��6   $��5q�6+   $��0��66   $��2��6C   � 5�      $��0,5,5,5q�6   $��Right=�6-   $��1��6I   .+�5�      �� $��试验项目数量：��6    �� 5�      -&   5        $ RemainingItemNumLabel��5�       ��6?   ��  �� Path$ Items.Count� �� ElementName$ WorkList��5�      �� 5�      -'   5         ��Name$ WorkList��5�      $��Visible=�6,   $��1��6_   $��2��6l   5�      5�      5�      � 5�   
   $��3��6    5�      �� 5�      $��* �6#   �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   .+�5�      �� $��0��6   $��0,5,0,0q�6)   $��Center=�6:   .+�5�      � -(   5         �Name$ LaunchProgressBar��5�      $��18��69   ���	#FF3E3E42
��� � ����	#FF3E3E425�      �� 5      -)   5        $ 	IsMessage��5      $��Center=�6/   $��	5,-18,5,5q�6L   6a   5     �� 5     -*   5        $ OKButton��5     $��3��6'   .��5     �� $��确定��6   $��20,10,20,10q�6&   5     �� 5     -+   5        $ DataInsertButton��5     $��1��6/   $	��180��6^   $��Right=�6j   .��5	     �� $��导出Upan数据��6   $��20,10,20,10q�6,   5
     �� 5     -,   5        $ 
UpanButton��5     $��2��6)   $	��120��6R   $��Right=�6^   .��5     �� $��
U盘下单��6   $��20,10,20,10q�6(   5     �� 5     --   5        $ CancelButton��5     $��4��6+   $��Right=�6;   $��0,0,-0.2,-0.2q�6W   $��76��6n   .��5     �� $��取消��6   $��20,10,20,10q�6&   5     5     5     5     
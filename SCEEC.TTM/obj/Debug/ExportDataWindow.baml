   M S B A M L   `   `   `  ����D  @SCEEC.TTM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:SCEEC.TTM	SCEEC.TTM  5   	        SCEEC.TTM.ExportDataWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:SCEEC.TTM     5   	   -   5           
WizardForm#���   5   	     � Title$  ExportDataWindow��5	   	   $	��800��6"   $	��600��6.   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � -   5        $��0��5       � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5      �� $��Resources/reports.png��6   $��20��63   $��0��6?   $��5q�6O   �� 5      -   5         ��Name$ TitleTextBlock��5      $��试验结果��60   $��1��6<   $��5q�6L   $��20��6W    	 Title#���  6c   �� 5      -   5         ��Name$ minimumButton��5      $��2��6,   $��35��6<   .��5      �� $��Resources/minimum.png��6   $��10q�67   5      �� 5      -   5        $ maximumButton��5      $��3��6,   $��35��6<   .��5      �� -   5         ��Name$ maximumButtonImage��5      $��Resources/maximum.png��64   $��10q�6S   5      �� 5       -   5        $ closeButton��5       $��4��6*   $��35��6:   .��5!      �� $��Resources/close.png��6   $��10q�65   5"      5#      �� 5D   
   -   5         ��Name$ ResultListBox��5D      $��1��6)   $��5q�66   $��18(�6A   5F      � 5G   
   $��2��6    5H      �� 5I      $��* �6#   �� 5J      $
��Auto �6#   �� 5K      $
��Auto �6#   .+�5M      �� -	   5        $ OKButton��5M      $��1��6'   $��4��67   .��5N      �� $��确认��6   $��20,10,20,10q�6&   5O      �� 5P      -
   5        $ CancelButton��5P      $��2��6+   $��5��6;   .��5Q      �� $��取消��6   $��20,10,20,10q�6&   5R      5S      5T      5U      
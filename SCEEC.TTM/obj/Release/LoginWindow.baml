   M S B A M L   `   `   `  ���� D  @SCEEC.TTM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:SCEEC.TTM	SCEEC.TTM      SCEEC.TTM.LoginWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:SCEEC.TTM     -     �Name$
  clos�� � Title$ 用户登录��$	��160��$	��300��$��18(� � 
ResizeMode$ NoResize=� � Icon$&  Resources/administratorBlack.png�� � WindowStartupLocation$ CenterScreen=�   
WizardForm#���   .���  � RowDefinitions �� $
��Auto ��� $��1* ��� $
��Auto ��� $��1* ��� $
��Auto �.+�� -   $��0�� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $!��Resources/administrator.png��$��20��$��0��$��5q��� -    ��Name$ TitleTextBlock��$��登录��$��1��$��5q�$��20�� 	 Title#���  �� -    ��Name$ closeButton��$��4��$��35��.���� $��Resources/close.png��$��10q�� $��2�� �� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* � �� $��* ��� $
��Auto ��� $
��Auto ��� $��* �.+��� $
��22.8��$!��Resources/administrator.png��	 � Cursor$
	 None��$��1��$��1��$��5q��� $��用户名：��$��1��$��2��$��5q��� -   
 ��Name$
 UsernameTextBox��$��24��$��1��$��1��$��3��$��5q��� $
��22.8��$��Resources/password.png��$��3��$��1��$��5q��� $��	密码：��$��3��$��2��$��5q�G� -    G�Name$ PasswordTextBox��$	��150��$��24��$��2��$��3��$��3��$��5q�� $��4�� �� $��* ��� $
��Auto ��� $��* �.+��� -   $	��200��$��3��$��1��
�������.���� $��登录��$��20,10,20,10q�
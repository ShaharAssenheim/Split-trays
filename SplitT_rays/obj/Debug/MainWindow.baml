   M S B A M L   `   `   `  ����F  BSplit_Trays, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Split_TraysSplit_Trays  5   	       Split_Trays.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Split_Trays     5   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  Split Trays��5      
������� � WindowStartupLocation$ CenterScreen=�6A    � WindowState$ 	Maximized=�6�   ��6�   ��  �� Source# Z����6�   �� # Z�� � 	Resources 5	         CenterGridHeaderStyle �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Property �� Value*  $System.Windows.Controls.DataGridCell��%�   &	        '  L     �� 5
   
   "  6.   .�5      ��  � ��$ Center=�6;   5      �� 5   
   "  6   .�5      ��  �@��TextAlignment$ Center=�68   5      5      .��5      (�  (� Stretch$
 Fill=�6   	 (� StretchDirection$
	 Both=�6   .��5   
   � .+�5      �� 
 �� Orientation$
 Vertical=�6   .+�5      �� $
 
Horizontal=�6   ���#A7C0CF$	��120��6K   .+�5      �� $��/Resources/logo.png��6   $	��118��69   $��55��6E   $��
10,30,0,41q�6Q   �� 5      -   5         ��Name$ Header��5      $��Split Trays��6,   $��	730,0,0,0q�6B   $
��Auto��6U   
�������$��Center=�6u   $��48(�6�   �� 5      -   5        $��
740,0,10,0q�5      $��70��61   $��70��6<    � ToolTip$
 Exit��6I   ��6k   Z� ��5      �� 5       �� ImageSource$ /Resources/exit.png��6)    j� Stretch$
 Fill=�6K   5      5      �� 5      -   5         ��Name$ MyTab��5      $��16(�6,   ���	#FFE2E1E1.@�5      �� $��Split Trays��6   .��5      �� $
 Vertical=�6%   $	��900��6<   .+�5       �� $
 
Horizontal=�6)   $��0,20,0,0q�6B   .+�5!   "   �� $	��WH:��6(   $��20(�66   $
��Bold"�6D   $��Center=�6V   $��50,0,0,0q�6y   �� 5"   "   -   5         ��Name$ WHTxt��5"   *   $	��200��69   $��10,0,0,0q�6E   $�� ��6W   $��1q�6t   $��Center=�6�   $��20(�6�   
�������6�   �� 5#   "   -   5         ��Name$ StartBtn��5#   )   $��20,0,0,0q�6;   $��Go��6M    + 'MaterialDesignFloatingActionLightButton#��[ 6Z   $ Go��6�   ���	#FF6887DC���	#FF6887DC$��33��6�   $��31��6�   $
��Bold"�6  $��Center=�6  �� 5$   "   -   5         ��Name$ 	MenuItems��5$   -   $��80,0,0,0q�6@   $y�Hidden=�6R   $
 
Horizontal=�6f   .+�5%   &   �� $��PL No:��6,   $��20(�6=   $
��Bold"�6K   $��80,0,0,0q�6^   $��Center=�6p   �� 5&   &   -   5        $ PLTxt��5&   ,   $�� ��6;   $	��100��6F   $��20(�6R   $
��Bold"�6`   $��10,0,0,0q�6s   ���	#DDFF3A3A$��Center=�6�   �� 5'   &   $��Total Skids:��6,   $��20(�6C   $
��Bold"�6Q   $��30,0,0,0q�6d   $��Center=�6v   �� 5(   &   -	   5        $ TotalSkidsTxt��5(   ,   $�� ��6C   $��80��6N   $��20(�6Y   $
��Bold"�6g   $��10,0,0,0q�6z   ���	#DDFF3A3A$��Center=�6�   �� 5)   &   $��Total Splits:��6,   $��20(�6D   $
��Bold"�6R   $��30,0,0,0q�6e   $��Center=�6w   �� 5*   &   -
   5        $ Total_SplitsTxt��5*   ,   $�� ��6E   $��80��6P   $��20(�6[   $
��Bold"�6i   $��10,0,0,0q�6|   ���	#DDFF3A3A$��Center=�6�   �� 5+   &   $��Total Shortages:��6,   $��20(�6G   $
��Bold"�6U   $��30,0,0,0q�6h   $��Center=�6z   �� 5,   &   -   5        $ Total_Shortages��5,   ,   $�� ��6E   $��80��6P   $��20(�6[   $
��Bold"�6i   $��10,0,0,0q�6|   ���	#DDFF3A3A$��Center=�6�   �� 5-   &   $��Full Traceability:��6,   $��20(�6I   $
��Bold"�6W   $��30,0,0,0q�6j   $��Center=�6|   �� 5.   &   -   5        $ TraceTxt��5.   ,   $�� ��6>   $��80��6I   $��20(�6T   $
��Bold"�6b   $��10,0,0,0q�6u   ���	#DDFF3A3A$��Center=�6�   5/   #   50      �� 51      $
 
Horizontal=�6)   $��10,0,0,0q�6B   � 52      -   5         �Name$
 PBar��52   *   $y�	Collapsed=�68   $��	0,100,0,0q�6O    % !MaterialDesignCircularProgressBar#��[ 6b    � Value$ 0Q�6�    � IsIndeterminate . $	��100��6�   $	��100��6�   �� 53      $
 
Horizontal=�6)   $��0,20,0,0q�6B   .+�54   "   &   System.Windows.Controls.DataGrid  -   5          Name$ Grid1��54   +   $y�Hidden=�6:      SelectionUnit$ FullRow=�6N   $��20,0,0,0q�6~   $
��Left=�6�      	RowHeight$ 30Q�6�      CanUserSortColumns .     CanUserAddRows .     AutoGenerateColumns .  $	��760��6	  $��18(�6   %� IsVirtualizing .     HorizontalGridLinesBrush
 �   �   VerticalGridLinesBrush
 �   �   GridLinesVisibility$	 All=�6�  $
��1450��6�  $
��Bold"�6�  ��6f   ��     RowStyle  55   &   �� 56   *   )  #System.Windows.Controls.DataGridRow"  60   .�57   .   ��  � �� 6K   �� *
RowColor+�� 58   .    � ��$ Center=�6Y   �� 59   .    � ��$ Center=�6T   5:   +   !   Columns! 5<   &   0  *System.Windows.Controls.DataGridTextColumn  5=   *   ,  &System.Windows.Controls.DataGridColumn"   Header$" ID��6=   #   Width5  /System.Windows.Controls.DataGridLengthConverter$
# Auto 6`   $   
IsReadOnly$ . %   HeaderStyle#% [  6�   1  +System.Windows.Controls.DataGridBoundColumn&   Binding& 6I   �� *ID+6�     5>   *   $
" Item��6=   $
# Auto 6d   $ . #% [  6�   & 6K   �� *Item+6�     5?   *   $
" Size��6=   $
# Auto 6d   $ . #% [  6�   & 6K   �� *Size+6�     5@   *   $	" Lot��6=   $
# Auto 6b   $ . #% [  6�   & 6J   �� *Lot+6�     5A   *   $" 
Kit Demand��6=   $
# Auto 6o   $ . #% [  6�   & 6Q   �� *	KitDemand+6�     5B   *   $	" Qty��6=   $
# Auto 6b   $ . #% [  6�   & 6J   �� *Qty+6�     5C   *   $" Remarks��6=   $
# Auto 6j   $ . #% [  6�   & 6N   �� *	Remarks+6�     5D   *   $" Excess��6=   $
# Auto 6h   $ . #% [  6�   & 6M   �� *Excess+6�     5E   *   $" Line Demand��6=   '   
Visibility$' Hidden=�6R   $
# Auto 6�   $ . #% [  6�   & 6f   �� *
LineDemand+6�     5F   *   $" 	Date Code��6=   $
# Auto 6m   $ . #% [  6�   & 6P   �� *
DateCode+6�     5G   *   $" Tray Qty��6=   $
# Auto 6k   $ . #% [  6�   & 6O   �� *	TrayQty+6�     5H   *   $" 	Tray Name��6=   $	# 280 6i   $ . #% [  6�   & 6P   �� *Tray+6�     5I   *   $" Location��6=   $
# Auto 6l   $ . #% [  6�   & 6O   �� *
Location+6�     5J   *   $" 
PBR Demand��6=   $# * 6q   $ . #% [  6�   & 6Q   �� *Description+6�   5L   #   �� 5M   "   -   5        ( ��Name$( StickBox��5M   +   $y�Hidden=�6=     MaterialDesignCardGroupBox#���  6Q   $	��760��6�   $��20,0,0,0q�6�   $	��390��6�   $��Center=�6�   $	��Top=�6�   .	  (MaterialDesignThemes.Wpf.ColorZoneAssist) 	  Mode$) Custom=�6�   * 	  
Foreground
* �����.��5N   &   �� $
 Vertical=�61   .+�5O   *   �� -   5        $ StickHeader��5O   2   $	��350��6G   $��50��6S   $�� ��6_   $��1q�6g   $��Center=�6{   $��Center=�6�   $��26(�6�     5P   *   -   5        $ 	GridStick��5P   3   $y�Visible=�6F   $
 Cell=�6[   $��5,10,0,0q�6�   $
��Left=�6�   $ 30Q�6�    .   .   .  $	��540��6  $��18(�6    .  
 �   �
 �   �$	 All=�6�  $	��355��6�  ��6p   ��   5Q   .   �� 5R   2   "  68   .�5S   6   ��  � �� 6S   �� *
RowColor+�� 5T   6    � ��$ Center=�6a   �� 5U   6    � ��$ Center=�6\   5V   3   ! 5X   .     5Y   2   $	" Lot��6E   $
# Auto 6j   $ . #% [  6�   & 6R   �� *Lot+6�     5Z   2   $" WH��6E   $
# Auto 6h   $ . #% [  6�   & 6Q   �� *WH+6�     5[   2   $	" Qty��6E   $
# Auto 6j   $ . #% [  6�   & 6R   �� *Qty+6�     5\   2   $" 	Packaging��6E   $# * 6v   $ . #% [  6�   & 6X   �� *	Packaging+6�   5^   +   �� 5_   *   -   5        $ Gll_Note��5_   0   $y�Hidden=�6B   $��No Reel Recorded��6V   $��0,10,0,0q�6q   $��Center=�6�   
�������$��20(�6�   �� 5`   *   -   5        $��60��5`   1   $	��300��6=   $��Send Stick Notification��6e   $��0,12,0,0q�6�   $��24(�6�   5a   '   5b   #   5c      5d      5e      �� 5f      $��Merge Trays��6   .��5g      �� $
 Vertical=�6%   $	��900��6<   .+�5h      �� $
 
Horizontal=�6)   $��0,20,0,0q�6B   .+�5i   "   �� $	��WH:��6(   $��20(�66   $
��Bold"�6D   $��Center=�6V   $��50,0,0,0q�6y   �� 5j   "   -   5        $ WHTxt3��5j   *   $	��200��6:   $��10,0,0,0q�6F   $�� ��6X   $��1q�6v   $��Center=�6�   $��20(�6�   
�������6�   �� 5k   "   -   5        $ 	StartBtn3��5k   )   $��20,0,0,0q�6<   $��Go��6N   #��[ 6[   $ Go��6�   ���	#FF6887DC���	#FF6887DC$��33��6�   $��31��6�   $
��Bold"�6  $��Center=�6  �� 5l   "   $��PL No:��6(   $��20(�69   $
��Bold"�6G   $��80,0,0,0q�6Z   $��Center=�6l   �� 5m   "   -   5        $ PLTxt3��5m   (   $�� ��68   $	��100��6C   $��20(�6O   $
��Bold"�6]   $��10,0,0,0q�6p   ���	#DDFF3A3A$��Center=�6�   �� 5n   "   $��Total Skids:��6(   $��20(�6?   $
��Bold"�6M   $��30,0,0,0q�6`   $��Center=�6r   �� 5o   "   -   5        $ TotalSkidsTxt3��5o   (   $�� ��6@   $	��100��6K   $��20(�6W   $
��Bold"�6e   $��10,0,0,0q�6x   ���	#DDFF3A3A$��Center=�6�   �� 5p   "   $��Total Optional Merges:��6(   $��20(�6I   $
��Bold"�6W   $��30,0,0,0q�6j   $��Center=�6|   �� 5q   "   -   5        $ OptionalMrgesTxt3��5q   (   $�� ��6C   $	��100��6N   $��20(�6Z   $
��Bold"�6h   $��10,0,0,0q�6{   ���	#DDFF3A3A$��Center=�6�   �� 5r   "   $��Full Traceability:��6(   $��20(�6E   $
��Bold"�6S   $��30,0,0,0q�6f   $��Center=�6x   �� 5s   "   -   5        $ 	TraceTxt3��5s   (   $�� ��6;   $	��100��6F   $��20(�6R   $
��Bold"�6`   $��10,0,0,0q�6s   ���	#DDFF3A3A$��Center=�6�   �� 5t   "   $��Sort By:��6(   $��20(�6;   $
��Bold"�6I   $��30,0,0,0q�6\   $��Center=�6n   �� 5u   "   -   5        $ SortByPLTxt3��5u   (   $�� ��6>   $	��150��6I   $��20(�6U   $
��Bold"�6c   $��10,0,0,0q�6v   ���	#DDFF3A3A$��Center=�6�   5v      � 5w      -   5        $ PBar3��5w   *   $y�	Collapsed=�69   $��	0,100,0,0q�6P   #��[ 6c   $ 0Q�6�    . $	��100��6�   $	��100��6�     5x      -   5        $ Grid3��5x   '   $y�Hidden=�66   $
 Cell=�6J   $��0,20,0,0q�6w   $ 30Q�6�    .   .   .  $	��760��6�   $��20(�6�    .  
 �   �
 �   �$	 All=�6k  $
��1800��6�  $
��Bold"�6�  ��6_   ��   5y   "   �� 5z   &   "  6,   .�5{   *   ��  � �� 6G   �� *
RowColor+�� 5|   *    � ��$ Center=�6U   �� 5}   *    � ��$ Center=�6P   5~   '   ! 5�   "     5�   &   $" Merge ID��69   $
# Auto 6b   $ . #% [  6�   & 6K   �� *ID+6�     5�   &   $
" Item��69   $	# 200 6`   $ . #% [  6   & 6G   �� *Item+6     5�   &   $
" Size��69   $
# Auto 6`   $ . #% [  6�   & 6G   �� *Size+6�     5�   &   $	" Lot��69   $	# 200 6^   $ . #% [  6}   & 6F   �� *Lot+6}     5�   &   $" Demand��69   $	# 100 6g   $ . #% [  6�   & 6I   �� *	KitDemand+6�     5�   &   $	" Qty��69   $
# Auto 6^   $ . #% [  6~   & 6F   �� *Qty+6~     5�   &   $" Remark��69   $	# 200 6e   $ . #% [  6�   & 6I   �� *	Remarks+6�     5�   &   $" Excess��69   $
# Auto 6d   $ . #% [  6�   & 6I   �� *Excess+6�     5�   &   $" 	Date Code��69   $
# Auto 6i   $ . #% [  6�   & 6L   �� *
DateCode+6�     5�   &   $" Tray Qty��69   $
# Auto 6g   $ . #% [  6�   & 6K   �� *	TrayQty+6�     5�   &   $	" MPN��69   $
# Auto 6^   $ . #% [  6~   & 6F   �� *MPN+6~     5�   &   $" M.Lot��69   $# * 6a   $ . #% [  6~   & 6H   �� *MLot+6~   5�      5�      5�      �� 5�      $��Poly-Cap��6   .��5�      �� $
 Vertical=�6%   $	��900��6<   .+�5�      �� $
 
Horizontal=�6)   $��0,20,0,0q�6B   .+�5�   "   �� $	��WH:��6(   $��20(�66   $
��Bold"�6D   $��Center=�6V   $��50,0,0,0q�6y   �� 5�   "   -   5        $ WHTxt2��5�   *   $	��200��6:   $��10,0,0,0q�6F   $�� ��6X   $��1q�6v   $��Center=�6�   $��20(�6�   
�������6�   �� 5�   "   -   5        $ 	StartBtn2��5�   )   $��20,0,0,0q�6<   $��Go��6N   #��[ 6[   $ Go��6�   ���	#FF6887DC���	#FF6887DC$��33��6�   $��31��6�   $
��Bold"�6  $��Center=�6  �� 5�   "   -   5        $ 
MenuItems2��5�   -   $��80,0,0,0q�6A   $y�Hidden=�6S   $
 
Horizontal=�6g   .+�5�   &   �� $��PL No:��6,   $��20(�6=   $
��Bold"�6K   $��80,0,0,0q�6^   $��Center=�6p   �� 5�   &   -    5        $ PLTxt2��5�   ,   $�� ��6<   $	��100��6G   $��20(�6S   $
��Bold"�6a   $��10,0,0,0q�6t   ���	#DDFF3A3A$��Center=�6�   �� 5�   &   $��Total Skids:��6,   $��20(�6C   $
��Bold"�6Q   $��30,0,0,0q�6d   $��Center=�6v   �� 5�   &   -!   5        $ TotalSkidsTxt2��5�   ,   $�� ��6D   $��80��6O   $��20(�6Z   $
��Bold"�6h   $��10,0,0,0q�6{   ���	#DDFF3A3A$��Center=�6�   �� 5�   &   $��Total Splits:��6,   $��20(�6D   $
��Bold"�6R   $��30,0,0,0q�6e   $��Center=�6w   �� 5�   &   -"   5        $ Total_SplitsTxt2��5�   ,   $�� ��6F   $��80��6Q   $��20(�6\   $
��Bold"�6j   $��10,0,0,0q�6}   ���	#DDFF3A3A$��Center=�6�   �� 5�   &   $��Total Shortages:��6,   $��20(�6G   $
��Bold"�6U   $��30,0,0,0q�6h   $��Center=�6z   �� 5�   &   -#   5        $ Total_Shortages2��5�   ,   $�� ��6F   $��80��6Q   $��20(�6\   $
��Bold"�6j   $��10,0,0,0q�6}   ���	#DDFF3A3A$��Center=�6�   �� 5�   &   $��Full Traceability:��6,   $��20(�6I   $
��Bold"�6W   $��30,0,0,0q�6j   $��Center=�6|   �� 5�   &   -$   5        $ 	TraceTxt2��5�   ,   $�� ��6?   $��80��6J   $��20(�6U   $
��Bold"�6c   $��10,0,0,0q�6v   ���	#DDFF3A3A$��Center=�6�   5�   #   5�      �� 5�      $
 
Horizontal=�6)   $��10,0,0,0q�6B   � 5�      -%   5        $ PBar2��5�   *   $y�	Collapsed=�69   $��	0,100,0,0q�6P   #��[ 6c   $ 0Q�6�    . $	��100��6�   $	��100��6�     5�      -&   5        $ Grid2��5�   '   $y�Hidden=�66   $
 Cell=�6J   $��0,20,0,0q�6w   $ 30Q�6�    .   .   .  $	��760��6�   $��20(�6�    .  
 �   �
 �   �$	 All=�6k  $
��1800��6�  $
��Bold"�6�  ��6_   ��   5�   "   �� 5�   &   "  6,   .�5�   *   ��  � �� 6G   �� *
RowColor+�� 5�   *    � ��$ Center=�6U   �� 5�   *    � ��$ Center=�6P   5�   '   ! 5�   "     5�   &   $" ID��69   $
# Auto 6\   $ . #% [  6|   & 6E   �� *ID+6|     5�   &   $
" Item��69   $
# Auto 6`   $ . #% [  6�   & 6G   �� *Item+6�     5�   &   $
" Size��69   $
# Auto 6`   $ . #% [  6�   & 6G   �� *Size+6�     5�   &   $	" Lot��69   $	# 250 6^   $ . #% [  6}   & 6F   �� *Lot+6}     5�   &   $" 
Kit Demand��69   $
# Auto 6k   $ . #% [  6�   & 6M   �� *	KitDemand+6�     5�   &   $	" Qty��69   $
# Auto 6^   $ . #% [  6~   & 6F   �� *Qty+6~     5�   &   $" Real Excess��69   $
# Auto 6m   $ . #% [  6�   & 6N   �� *
RealExcess+6�     5�   &   $" 10 Precent Excess��69   $
# Auto 6o   $ . #% [  6�   & 6T   �� *Excess+6�     5�   &   $" Remarks��69   $
# Auto 6f   $ . #% [  6�   & 6J   �� *	Remarks+6�     5�   &   $" Line Demand��69   $' Visible=�6N   $
# Auto 6�   $ . #% [  6�   & 6c   �� *LineDemand_Poly+6�     5�   &   $" 	Date Code��69   $
# Auto 6i   $ . #% [  6�   & 6L   �� *
DateCode+6�     5�   &   $	" MPN��69   $' Hidden=�6F   $	# 300 6r   $ . #% [  6�   & 6Z   �� *MPN+6�     5�   &   $" M.Lot��69   $' Hidden=�6H   $	# 300 6u   $ . #% [  6�   & 6\   �� *MLot+6�     5�   &   $" Customer��69   $# * 6h   $ . #% [  6�   & 6K   �� *
Customer+6�   5�      5�      5�      �� 5�      $��Split Trays PBR��6   .��5�      �� $
 Vertical=�6%   $	��900��6<   .+�5�      �� $
 
Horizontal=�6)   $��0,20,0,0q�6B   .+�5�   "   �� $	��WH:��6(   $��20(�66   $
��Bold"�6D   $��Center=�6V   $��50,0,0,0q�6y   �� 5�   "   -'   5        $ WHTxt4��5�   *   $	��200��6:   $��10,0,0,0q�6F   $�� ��6X   $��1q�6u   $��Center=�6�   $��20(�6�   
�������6�   �� 5�   "   -(   5        $ 	StartBtn4��5�   )   $��20,0,0,0q�6<   $��Go��6N   #��[ 6[   $ Go��6�   ���	#FF6887DC���	#FF6887DC$��33��6�   $��31��6�   $
��Bold"�6  $��Center=�6  �� 5�   "   -)   5        $ 
MenuItems4��5�   -   $��80,0,0,0q�6A   $y�Hidden=�6S   $
 
Horizontal=�6g   .+�5�   &   �� $��PL No:��6,   $��20(�6=   $
��Bold"�6K   $��80,0,0,0q�6^   $��Center=�6p   �� 5�   &   -*   5        $ PLTxt4��5�   ,   $�� ��6<   $	��100��6G   $��20(�6S   $
��Bold"�6a   $��10,0,0,0q�6t   ���	#DDFF3A3A$��Center=�6�   �� 5�   &   $��Total Skids:��6,   $��20(�6C   $
��Bold"�6Q   $��30,0,0,0q�6d   $��Center=�6v   �� 5�   &   -+   5        $ TotalSkidsTxt4��5�   ,   $�� ��6D   $��80��6O   $��20(�6Z   $
��Bold"�6h   $��10,0,0,0q�6{   ���	#DDFF3A3A$��Center=�6�   �� 5�   &   $��Total Splits:��6,   $��20(�6D   $
��Bold"�6R   $��30,0,0,0q�6e   $��Center=�6w   �� 5�   &   -,   5        $ Total_SplitsTxt4��5�   ,   $�� ��6F   $��80��6Q   $��20(�6\   $
��Bold"�6j   $��10,0,0,0q�6}   ���	#DDFF3A3A$��Center=�6�   �� 5�   &   $��Total Shortages:��6,   $��20(�6G   $
��Bold"�6U   $��30,0,0,0q�6h   $��Center=�6z   �� 5�   &   --   5        $ Total_Shortages4��5�   ,   $�� ��6F   $��80��6Q   $��20(�6\   $
��Bold"�6j   $��10,0,0,0q�6}   ���	#DDFF3A3A$��Center=�6�   �� 5�   &   $��Full Traceability:��6,   $��20(�6I   $
��Bold"�6W   $��30,0,0,0q�6j   $��Center=�6|   �� 5�   &   -.   5        $ 	TraceTxt4��5�   ,   $�� ��6?   $��80��6J   $��20(�6U   $
��Bold"�6c   $��10,0,0,0q�6v   ���	#DDFF3A3A$��Center=�6�   5�   #   5�      �� 5�      $
 
Horizontal=�6)   $��10,0,0,0q�6B   � 5�      -/   5        $ PBar4��5�   *   $y�	Collapsed=�69   $��	0,100,0,0q�6P   #��[ 6c   $ 0Q�6�    . $	��100��6�   $	��100��6�   �� 5�      $
 
Horizontal=�6)   $��0,20,0,0q�6B   .+�5�   "     -0   5        $ Grid4��5�   +   $y�Visible=�6:   $ FullRow=�6O   $��20,0,0,0q�6   $
��Left=�6�   $ 30Q�6�    .   .   .  $	��760��6
  $��18(�6   .  
 �   �
 �   �$	 All=�6�  $
��1850��6�  $
��Bold"�6�  ��6g   ��   5�   &   �� 5�   *   "  60   .�5�   .   ��  � �� 6K   �� *
RowColor+�� 5�   .    � ��$ Center=�6Y   �� 5�   .    � ��$ Center=�6T   5�   +   ! 5�   &     5�   *   $" ID��6=   $
# Auto 6`   $ . #% [  6�   & 6I   �� *ID+6�     5�   *   $
" Item��6=   $
# Auto 6d   $ . #% [  6�   & 6K   �� *Item+6�     5�   *   $
" Size��6=   $
# Auto 6d   $ . #% [  6�   & 6K   �� *Size+6�     5�   *   $	" Lot��6=   $
# Auto 6b   $ . #% [  6�   & 6J   �� *Lot+6�     5�   *   $" 
Kit Demand��6=   $
# Auto 6o   $ . #% [  6�   & 6Q   �� *	KitDemand+6�     5�   *   $	" Qty��6=   $
# Auto 6b   $ . #% [  6�   & 6J   �� *Qty+6�     5�   *   $" Remarks��6=   $
# Auto 6j   $ . #% [  6�   & 6N   �� *	Remarks+6�     5�   *   $" Excess��6=   $
# Auto 6h   $ . #% [  6�   & 6M   �� *Excess+6�     5�   *   $" Line Demand��6=   $' Hidden=�6R   $
# Auto 6�   $ . #% [  6�   & 6f   �� *
LineDemand+6�     5�   *   $" 	Date Code��6=   $
# Auto 6m   $ . #% [  6�   & 6P   �� *
DateCode+6�     5�   *   $" Tray Qty��6=   $
# Auto 6k   $ . #% [  6�   & 6O   �� *	TrayQty+6�     5�   *   $" 	Tray Name��6=   $
# Auto 6i   $ . #% [  6�   & 6P   �� *Tray+6�     5�   *   $" Location��6=   $
# Auto 6l   $ . #% [  6�   & 6O   �� *
Location+6�     5�   *   $" 
PBR Demand��6=   $
# Auto 6q   $ . #% [  6�   & 6Q   �� *Description+6�     5�   *   $" Last Location��6=   $# * 6v   $ . #% [  6�   & 6T   �� *Last_Location+6�   5�   #   5�      5�      5�      5�      5�      5�      5�      5�      
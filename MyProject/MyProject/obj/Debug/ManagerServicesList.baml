   M S B A M L   `   `   `  ����  	MyProject#    MyProject.ManagerServicesList   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Услуги��5   	   $	��600��6   $	��800��6%   ���#fff � WindowStartupLocation$ CenterScreen=�6C   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   �� $��Список услуг��6   $��24(�6(   $
��Bold"�66   $��Center=�6H   $��0��6c   �� 5   
   ���#fff �� CornerRadius$ 10��6#    �� Padding$ 20q�65   $��1��6B   ._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ ServicesDataGrid��5         AutoGenerateColumns .     
IsReadOnly .    HeadersVisibility$ Column=�5   )   	   GridLinesVisibility$
	 None=�5      
   ColumnHeaderHeight$
 40Q�62      	RowHeight$ 35Q�5      $��14(�6&      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *ServiceName+6[     5      $ Описание��6)   $ * 6[    6;   �� *Description+6[     5      $ Цена��6)   $
 0.7* 6b    67   �� *Price+ �� StringFormat$ N2��6b   4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Удалить��6-      CellTemplate 5      �� 5      .R�5   "   �� -   5        $��Удалить��5   )   $��80��6;   
���  ��
�������6j   5      5      5      5       �� 5"   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��2��6K   $��0,10,0,0q�6X   .+�5#      �� -   5        $ ��Редактировать��5#      $	��150��6-   $��45��69   $
��10,0q�6E   ���#2196F3
�������$��SemiBold"�6{   6�   �� 5$      -   5        $��
Назад��5$      $	��150��6%   $��45��61   ���#840B29���#fff$��SemiBold"�6d   6z   5%      5&      5'      
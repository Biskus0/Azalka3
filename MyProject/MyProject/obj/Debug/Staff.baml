   M S B A M L   `   `   `  ����  	MyProject    MyProject.Staff   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Персонал��5   	   $	��600��6   $	��800��6'   ���#fff � WindowStartupLocation$ CenterScreen=�6E   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   �� $0��*Информация о персонале��6   $��24(�62   $
��Bold"�6@   $��Center=�6R   $��0��6m   �� 5   
   ���#fff �� CornerRadius$ 10��6#    �� Padding$ 20q�65   $��1��6B   ._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ StaffDataGrid��5         AutoGenerateColumns .     
IsReadOnly .    HeadersVisibility$ Column=�5   )   	   GridLinesVisibility$
	 None=�5      
   ColumnHeaderHeight$
 40Q�62      	RowHeight$ 35Q�5      $��14(�6&      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Имя��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *	StaffName+6T     5      $ Должность��6)   $ * 6Z    6<   �� *	StaffRole+6Z     5      $ Зарплата��6)   $
 0.7* 6l    6;   �� *StaffSalary+ �� StringFormat$ N2��6l     5      $ Телефон��6)   $ 1* 6Y    6:   �� *
StaffPhone+6Y   5      5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��2��6K   $��0,10,0,0q�6X   .+�5      �� -   5        $ ��Редактировать��5      $	��150��6-   $��45��69   $
��10,0q�6E   ���#2196F3
�������$��SemiBold"�6{   6�   �� 5      -   5        $��
Назад��5      $	��150��6%   $��45��61   ���#840B29���#fff$��SemiBold"�6d   6z   5      5       5!      
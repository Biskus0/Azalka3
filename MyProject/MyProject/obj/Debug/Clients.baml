   M S B A M L   `   `   `  ����  	MyProject    MyProject.Clients   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$)  #Клиент мероприятия��5   	   $	��600��6$   $	��800��61   ���#fff � WindowStartupLocation$ CenterScreen=�6O   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   �� $)��#Клиент мероприятия��6   $��24(�6.   $
��Bold"�6<   $��Center=�6N   $��0��6i   �� 5   
   ���#fff �� CornerRadius$ 10��6#    �� Padding$ 20q�65   $��1��6B   ._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ ClientsDataGrid��5         AutoGenerateColumns .     RowBackground �#fff   HeadersVisibility$ Column=�6a   	   GridLinesVisibility$
	 None=�6|   
   ColumnHeaderHeight$
 40Q�6�      	RowHeight$ 35Q�6�   $��14(�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Имя клиента��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.33* 6]   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *
ClientName+6]     5      $! Номер телефона��6)   $ 0.33* 6a    6A   �� *ClientPhone+6a     5      $ 
Почта��6)   $ 0.33* 6R    68   �� *Email+6R   5      5      �� 5   
   -   5        $��
Назад��5      $	��150��6!   $��45��6-   ���#840B29���#fff$��SemiBold"�6`   $��Center=�6v    � Cursor$
 Hand��6�   $��2��6�   6�   5      5      
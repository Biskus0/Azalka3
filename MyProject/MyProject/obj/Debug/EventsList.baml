   M S B A M L   `   `   `  ����  	MyProject    MyProject.EventsList   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$)  #Список мероприятий��5   	   $	��600��6$   $	��800��61   ���#fff � WindowStartupLocation$ CenterScreen=�6O   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   �� $)��#Список мероприятий��6   $��24(�6.   $
��Bold"�6<   $��Center=�6N   $��0��6i   �� 5   
   ���#fff �� CornerRadius$ 10��6#    �� Padding$ 5q�65   $��1��6A   ._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ EventsDataGrid��5         AutoGenerateColumns .     RowBackground �#fff   HeadersVisibility$ Column=�6`   	   GridLinesVisibility$
	 None=�6{   
   ColumnHeaderHeight$
 40Q�6�      	RowHeight$ 35Q�6�   $��14(�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.2* 6Q   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *	EventID+6Q     5      $ Название��6)   $ 1* 6Y    6;   �� *	EventName+6Y     5      $% Дата мероприятия��6)   $ 1* 6|    6C   �� *	EventDate+ �� StringFormat$ 
yyyy-MM-dd��6|     5      $ Зал��6)   $ 1* 6W    66   �� *LocationName+6W     5      $ Адрес локации��6)   $ 1* 6d    6@   �� *LocationAddress+6d     5      $ Услуги��6)   $ 1* 6V    69   �� *
Services+6V     5      $ Персонал��6)   $ 1* 6U    6;   �� *Staff+6U   5      5      �� 5   
   -   5        $��
Выход��5      $	��150��6!   $��45��6-   ���#840B29���#fff$��SemiBold"�6`   $��Center=�6v    � Cursor$
 Hand��6�   $��2��6�   6�   5      5      
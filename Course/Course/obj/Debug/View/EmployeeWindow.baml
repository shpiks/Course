   M S B A M L   `   `   `  ����A  =Course, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Course.ViewCourse.View  5   	   5clr-namespace:Course.ValidationCourse.Validation  5   	        Course.View.EmployeeWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Course.View     5   	   2local1clr-namespace:Course.Validation     5   	     � Title$2  ,Информация о сотруднике��5	   	   $	��250��6)   $	��400��66    � WindowStartupLocation$ CenterOwner=�5
   	    � 
ResizeMode$ NoResize=�5   	    � 	Resources 5       �� Source�� 5   
   $! Styles/MainWindowStyle.xaml?�6   %    .��5      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      .+�5   
   �� $��Фамилия��6   $��Center=�5      $��Center=�5       �� 5!   
   $��Имя��6   $��1��6   $��Center=�5"      $��Center=�5#      �� 5$   
   $��Отчество��6   $��2��6$   $��Center=�5%      $��Center=�5&      �� 5'   
   $��Должность��6   $��3��6%   $��Center=�5(      $��Center=�5)      �� 5*   
   $��Звание��6   $��4��6"   $��Center=�5+      $��Center=�5,      �� 5/   
   -   5        $��TBEmployeeLastName��5/         errorTemplate =� ErrorTemplate# [  50      $��1��6H   $��30��51      .��6   ��52      �� 53       �� Path$ Employee.LastName�6   	 �� ValidatesOnDataErrors	 . 
 �� UpdateSourceTrigger$
 PropertyChanged=�6P    �� ValidationRules 54      ,  &Course.Validation.MinimumCharacterRule  55         MinimumCharacters$ 2��66   57      .��58      59      �� 5;   
   -   5        $��TBEmployeeFirstName��5;      # [  5<      $��1��5=      $��1��6"   $��30��5>      .��6   ��5?      �� 5@      $ Employee.FirstName�6   	 . $
 PropertyChanged=�5B       �� Mode$ TwoWay=�5C       5D        5E      $ 2��66   5G      .��5H      5I      �� 5K   
   -   5        $��TBEmployeePatronymic��5K      # [  5L      $��1��5M      $��2��6"   $��30��5N      .��6   ��5O      �� 5P      $ Employee.Patronymic�6   	 . $
 PropertyChanged=�6R    5Q        5R      $ 2��66   5T      .��5U      5V      �� 5Y   
   $��1��5[      $��3��6#    �� SelectedIndex$ 0��5\      ��5Y      �� *PositionList+$ TwoWay=� �� SelectedItem 5Z      �� *Employee.Position+5\      �� 5`   
   $��1��5b      $��4��6#   $ 1��5c      ��5`      �� *
RankList+ 5a      �� *Employee.Rank+5c      �� 5e   
   $��Center=�6    �� Orientation$ 
Horizontal=�5f      $��5��5g      $��1��6"   .+�5h      ��  �� 	IsDefault . $��60��6'   $��5q�5i      ��5j      �� *AcceptCommand+{�5k      `� 5l       ( $InverseAndBooleansToBooleanConverter `� 	Converter# [ 6#    `� Mode$ TwoWay=�6e   .1�5m      ��  �� ElementName$ TBEmployeeLastName��6"   $ (Validation.HasError)�6C   �� 5n      $ TBEmployeePatronymic��6"   $ (Validation.HasError)�6E   �� 5o      $ TBEmployeeFirstName��6"   $ (Validation.HasError)�6D   5p      .��5r   $   OK5s      �� 5t       �� IsCancel . $��60��6%   $��5q�5u      ��5v      �� *ExitCommand+.��65   Отмена6=   5w      5y      5z      
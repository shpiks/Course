   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      A =Course, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Course.ViewCourse.View 5      5clr-namespace:Course.ValidationCourse.Validation 5	   	       Course.View.MaterialWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      %localclr-namespace:Course.View  5      ,local1clr-namespace:Course.Validation  5	   	     � Title$0  *Информация о материале��5      $	��410��6&   $	��450��63    � WindowStartupLocation$ CenterOwner=�5   	    � 
ResizeMode$ NoResize=�5   	    � 	Resources 5       �� Source�� 5   
   $! Styles/MainWindowStyle.xaml?�6   %    .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $��80 �6   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5    
   �� 5!      $	��100 �6   �� 5"      .+�5%   
   �� $��Единая Книга��6   $��Center=�5&      $��Center=�5'      �� 5(   
   $��Фабула��6   $��1��6"   $��Center=�5)      $��Center=�5*      �� 5+   
   $%��Дата регистрации��6   $��2��6,   $��Center=�5,      $��Center=�5-      �� 5.   
   $��Срок��6   $��3��6    $��Center=�5/      $��Center=�50      �� 51   
   $��Продление��6   $��4��6%   $��Center=�52      $��Center=�53      �� 55   
   $��Заявитель��6   $��5��6%   $��Center=�56      $��Center=�57      �� 58   
   $��Перспектива��6   $��6��6'   $��Center=�59      $��Center=�5:      �� 5;   
   $��Решение��6   $��7��6#   $��Center=�5<      $��Center=�5=      �� 5>   
   $��Исполнен��6   $��8��6$   $��Center=�5?      $��Center=�5@      �� 5C   
   -   5        $��TBMaterialNumberEK��5C      $��1��5E      $��30��5F      ��5D      �� *Material.NumberEK+ �� UpdateSourceTrigger$ PropertyChanged=�5F      �� 5G   
   $��1��5H      $��1��6"    �� TextWrapping$
 Wrap=�5I      $��70��5J      "	 �� VerticalScrollBarVisibility$
	 Auto=�5K      ��5G      �� *Material.Story+5K      (  "System.Windows.Controls.DatePicker  5[   
   -   5        $��DPDateRegistration��5[      $��1��5\      $��2��6&   
   IsTodayHighlighted
 . $��Center=�5^      |�.     SelectedDate 5a      �� 5b       �� Path$! Material.DateOfRegistration�6    �� ValidatesOnDataErrors . $ PropertyChanged=�5d       �� Mode$ TwoWay=�5e       �� ValidationRules 5g      * $Course.Validation.RegistrationDPRule  5h      5j      .   (System.Windows.Interactivity.Interaction   Triggers 5m      /   )System.Windows.Interactivity.EventTrigger  5n         	EventName$ SelectedDateChanged��6!   .   (System.Windows.Interactivity.TriggerBase   Actions. 5o      6   0System.Windows.Interactivity.InvokeCommandAction     Command 5p      �� *ChangeTermCommand+   CommandParameter 5q      �� *Material.DateOfRegistration+5r      5t        5z   
   -   5        $��DPTerm��5z      $��1��5{      $��3��6%   $��Center=�5|       5~      �� 5      $ Material.DateOfTerm�6    . $ PropertyChanged=�5�      $ TwoWay=�5�       5�      &  Course.Validation.DateOfTermRule  5�      5�      5�      �� 5�   
   -   5        $��CBExtension��5�      $��1��5�      $��4��6#   $
��Left=�5�      $��Center=�5�       d� 	IsChecked 5�      �� *Material.Extension+��5�      �� *ChangeExtensionCommand+��5�      �� *Material.Extension+�� 5�   
   $��Center=�6    �� Orientation$ 
Horizontal=�62   $��5��6K   $��1��6X   .+�5�      �� $��20��6   $	��200��6&    �� DisplayMemberPath$ LastName��5�      ��5�      �� *victimsList+ �� SelectedItem 5�      �� *SelectedVictim+$ TwoWay=��� 5�      $��Изменить��6   $��60��6,   $��5q�5�      ��5�      �� *EditVictimCommand+��5�      �� *SelectedVictim+5�      �� 5�   
   $��1��5�      $��6��6#   ��5�      �� *PerspectiveList+ 5�      �� *Material.Perspective+5�   #   �� 5�   
   -   5         ��Name$ 
CBDecision��5�      $��1��5�      $��7��6#   ��5�      �� *DecisionList+ 5�      �� *Material.Decision+5�   #   �� 5�   
   $��1��5�      $��8��6#   $
��Left=�5�      $��Center=�5�       5�      �� * Material.ExecutedOrNotExecuted+{�5�      ��  �� ElementName$ 
CBDecision��$ SelectedIndex�   ConverterDesisionCB �� 	Converter# [  5�      �� 5�   
   $��Center=�6   $ 
Horizontal=�5�      $��9��5�      $��1��6"   .+�5�      ��  �� 	IsDefault . $��60��6'   $��5q�5�      ��5�      �� *AcceptCommand+{�5�      `� 5�       ( $InverseAndBooleansToBooleanConverter `� 	Converter# [ 6#    `� Mode$ TwoWay=�6e   .1�5�      �� $ TBMaterialNumberEK��6"   $ (Validation.HasError)�6C   �� 5�      $ DPDateRegistration��6"   $ (Validation.HasError)�6C   �� 5�      $ DPTerm��6"   $ (Validation.HasError)�67   5�      .��5�   $   OK5�      �� 5�       �� IsCancel . $��60��6%   $��5q�5�      ��5�      �� *ExitCommand+.��65   Отмена5�      5�      5�      5�      
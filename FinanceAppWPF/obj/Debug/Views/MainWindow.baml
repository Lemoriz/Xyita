   M S B A M L   `   `   `  ����H  DFinanceAppWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TestTest  5   	   1clr-namespace:Test.ViewModelsTest.ViewModels  5   	       Test.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #testclr-namespace:Test     5   	   4
viewModelsclr-namespace:Test.ViewModels     5   	   ���#22b473  � Title$  My finances��5
   	   $	��640��6   $	��360��6*    � WindowStartupLocation$ CenterScreen=�5   	    � DataContext 5      "   Test.ViewModels.DayViewModel  5   
    � 	Resources 5       �� 
TargetType �� Property �� Value��%Q   '��       �� 5   
   " ��6   .�5      ��  � ��$ 1q�6'   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $	��25* �6   �� 5      $
��524* �6   �� 5      $	��11* �6   �� 5      $	��49* �6    � ColumnDefinitions 5   
   �� 5       $
�� 15* �6   �� 5!      $
��320* �6   �� 5"      $	��17* �6   .+�5%   
   �� -   5        $��scroll��5%   *   
�������$��Hidden=�68   $��Hidden=�5&      $��1��6>   	 �� PanningMode$	 HorizontalOnly=�6N   $��1��6k   �� 5(   
   ���#faf8f2$��3��6#   $��1��60   �� 5)   
   ���#faf8f2$��3��6#   $��2��60   �� 5*   
   ���#faf8f2$��3��6#   $��0��60   �� 5,   
   $
��Left=�6/   $��3��6J   $	��Top=�6W   $��80��5-      $��31��6   $��0,10,0,0q�6(   $��1��6:   
 J� RenderTransformOrigin$
 0.062,0.516&�6J   ��5,      ��  �� Path$
 Name�5-   J   5.      5/      
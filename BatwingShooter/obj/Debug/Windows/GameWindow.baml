   M S B A M L   `   `   `  ����  BatwingShooter'    !BatwingShooter.Windows.GameWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
GameWindow��5   	   $	��600��5   	   $
��1000��5   	    � WindowState$ Normal=�5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      �� $��5q�6   
���iii� �� CornerRadius$ 15��5       �� Padding$ 15q�5      ._�5   
   �  � RowDefinitions 5      �� 5      $��50 �6    �� 5      .+�5      �� $��0��6    �� TextAlignment$ Center=�5      $��Center=�5      $��24(�5      ?=Use to arrows to move, SPACE to fire and ENTER for Play/Pause5   X   �� 5      $��1��6   ��5      �� 5       �� ImageSource$ /Images/gotham-city.png��6!   ._�5      �� -   5        $��
GameCanvas��5      5      5      5      5       5!      
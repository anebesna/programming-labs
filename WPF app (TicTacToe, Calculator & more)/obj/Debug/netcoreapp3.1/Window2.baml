   M S B A M L   `   `   `  ����>  :lab, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:lablab  5   	       lab.Window2   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:lab     5   	     � Title$  	TicTacToe��5   	   $	��600��6   $	��550��6(    � Icon$h b\\Mac\Home\Downloads\976599-all-in-one-appliances-desktop-display-electronics-pc-screen_106534.ico��5	   	   ��5
      �� 5   
    �� ImageSource$&  \\Mac\Home\Downloads\Wnf38X.jpeg��6    j� Stretch$
 Fill=�6D   .��5      � .+�5   
   � -   5         �Name$
 Grid��5      $��68,59,68,93q�6    � RowDefinitions 5      �� 5      6"   �� 5      6"   �� 5      6"   �� 5      6"   �� 5      6"    � ColumnDefinitions 5      �� 5      6%   �� 5      6%   �� 5      6%   �� 5      6%   �� 5      6%   .+�5      �� -   5        $�� ��5      ���	#FFF9C4C4$��0��6_   $��0��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5      -   5        $�� ��5      ���	#FFF9C4C4$��0��6_   $��1��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5      -   5        $�� ��5      ���	#FFF9C4C4$��0��6_   $��2��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5       -   5        $�� ��5       ���	#FFF9C4C4$��0��6_   $��3��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5!      -   5        $�� ��5!      ���	#FFF9C4C4$��0��6_   $��4��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5"      -   5        $�� ��5"      ���	#FFF9C4C4$��1��6_   $��0��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5#      -   5        $�� ��5#      ���	#FFF9C4C4$��1��6_   $��1��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5$      -	   5        $�� ��5$      ���	#FFF9C4C4$��1��6_   $��2��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5%      -
   5        $�� ��5%      ���	#FFF9C4C4$��1��6_   $��3��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5&      -   5        $�� ��5&      ���	#FFF9C4C4$��1��6_   $��4��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5'      -   5        $�� ��5'      ���	#FFF9C4C4$��2��6_   $��0��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5(      -   5        $�� ��5(      ���	#FFF9C4C4$��2��6_   $��1��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5)      -   5        $�� ��5)      ���	#FFF9C4C4$��2��6_   $��2��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5*      -   5        $�� ��5*      ���	#FFF9C4C4$��2��6_   $��3��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5+      -   5        $�� ��5+      ���	#FFF9C4C4$��2��6_   $��4��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5,      -   5        $�� ��5,      ���	#FFF9C4C4$��3��6_   $��0��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5-      -   5        $�� ��5-      ���	#FFF9C4C4$��3��6_   $��1��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5.      -   5        $�� ��5.      ���	#FFF9C4C4$��3��6_   $��2��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 5/      -   5        $�� ��5/      ���	#FFF9C4C4$��3��6_   $��3��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 50      -   5        $�� ��50      ���	#FFF9C4C4$��3��6_   $��4��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 51      -   5        $�� ��51      ���	#FFF9C4C4$��4��6_   $��0��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 52      -   5        $�� ��52      ���	#FFF9C4C4$��4��6_   $��1��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 53      -   5        $�� ��53      ���	#FFF9C4C4$��4��6_   $��2��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 54      -   5        $�� ��54      ���	#FFF9C4C4$��4��6_   $��3��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   �� 55      -   5        $�� ��55      ���	#FFF9C4C4$��4��6_   $��4��6l   $��Yu Gothic UI Light)�6|   $��60(�6�   ���	#FF162D50��6I   Z� 6�   56      �� 57   
   -   5        $��222,516,222,0q�57   &   ���	#FF162D50$��
Вихід��6T   ���	#FFD5E5FF$��Yu Gothic UI Semilight)�6�   $��22(�6�   $��37��6�   $	��Top=�6�   ��6d   Z�  � 	Resources 58       �� 
TargetType	 �� Property
 �� Value��%_   '��       �� 59      " ��6   .�5:      �� 	 �@��CornerRadius$
 15��65   5;      5<      5=      �� 5>   
   -   5        $��222,10,222,0q�5>   (   ���	#FF162D50$��Restart��6U   ���	#FFD5E5FF$��Yu Gothic UI Semilight)�6�   $��22(�6�   $��37��6�   $	��Top=�6�   ��6g   Z�  5?      ��%_   '��       �� 5@      " ��6   .�5A      �� 	 �@��CornerRadius$
 15��65   5B      5C      5D      �� 5E   
   -   5        $��label��5E      $�� ��6   $
��Left=�6(   $��68,8,0,0q�6C   $	��Top=�6U   $��Yu Gothic UI Semilight)�6m   
�������$��22(�6�   5G      5H      
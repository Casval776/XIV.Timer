   M S B A M L   `   `   `  ����H  DFFXIV_TimerV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:FFXIV_TimerV2FFXIV_TimerV2  5   	       FFXIV_TimerV2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:FFXIV_TimerV2     5   	     � Title$  FFXIV Timer��5   	   $	��350��6   $	��525��6*    � WindowStyle$
 None=�5	   	    � AllowsTransparency . 
������ .��5
      � -   5        .+�5   
   �� -   5        $��TransparencyValueBox��5       �� TextAlignment$ Right=�6�   $��40��6�   $��132,3,352.8,317.4q�6�   
������ ��6.   ��  �� ElementName$ TransSlider�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   -   5         ��Name$ TransSlider��5      $
��Left=�6&   $��	10,10,0,0q�6A   $	��Top=�6T   $	��117��6l    �� TickFrequency$ 5Q�6x   	 � Maximum$		 100Q�6�   
 �� TickPlacement$
 TopLeft=�6�    �� IsSnapToTickEnabled . 6�   5      5      
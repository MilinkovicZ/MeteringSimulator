   M S B A M L   `   `   `  ����I  ENetworkService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:NetworkServiceNetworkService  5   	   ;"clr-namespace:NetworkService.ViewsNetworkService.Views  5   	   C&clr-namespace:NetworkService.ViewModelNetworkService.ViewModel  5   	       NetworkService.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:NetworkService     5   	   4views"clr-namespace:NetworkService.Views     5   	   <	viewModel&clr-namespace:NetworkService.ViewModel     5   	     � Title$  
MainWindow��5
   	   $	��750��6   $	��850��6)    � DataContext 5      2   ,NetworkService.ViewModel.MainWindowViewModel  5   
    � 	Resources 5       �� DataType7   1NetworkService.ViewModel.NetworkEntitiesViewModel*   $NetworkService.Views.NetworkEntities6   0NetworkService.ViewModel.NetworkDisplayViewModel)   #NetworkService.Views.NetworkDisplay8   2NetworkService.ViewModel.MeasurementGraphViewModel+   %NetworkService.Views.MeasurementGraph��%�   (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      5       J� InputBindings 5      �� 5   
    �� 	Modifiers$
 Ctrlf�5      
 �� Key$	 Tab��5       �� Command 5      �� *ShortcutCommand+5      .��5"      �  � RowDefinitions 5#   
   �� 5$      $
��Auto �6   �� 5%      $
��Auto �6   .+�5(   
   ]� $��0��6   .+�5)      o� $	��Top=�6   .@�5*      m� $��Network Entities��6   	 m� CommandParameter$	 	NetEntity��6[   
 m� Command
 65   �� *NavigationCommand+6[   m� 5+      $��Network Display��6   $	 
NetDisplay��6Z   
 64   �� *NavigationCommand+6Z   m� 5,      $��Measurement Graph��6   $	 MesGraph��6\   
 66   �� *NavigationCommand+6\   5-      5.      � 50   
   $��1��6   .+�51      �� ��6   �� *CurrentViewModel+52      54      56      
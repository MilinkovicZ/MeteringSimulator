   M S B A M L   `   `   `  ����I  ENetworkService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:NetworkService.ViewsNetworkService.Views  5      C&clr-namespace:NetworkService.ViewModelNetworkService.ViewModel  5      ;"clr-namespace:NetworkService.ModelNetworkService.Model  5      *    $NetworkService.Views.NetworkEntities   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:NetworkService.Views     5      <	viewModel&clr-namespace:NetworkService.ViewModel     5      3data"clr-namespace:NetworkService.Model     5        � DataContext  5      7   1NetworkService.ViewModel.NetworkEntitiesViewModel  5   
   .��5      �� 
�������$��0,0,-142,-149.4q�6    .+�5   
   �� 
 �� Top$ 80��6    �� Left$ 80��6!   .+�5      &   System.Windows.Controls.DataGrid     SelectionMode$ Single=�66    �� TextAlignment$ Center=�6o      CanUserResizeColumns .     CanUserReorderColumns .     
IsReadOnly .    AutoGenerateColumns .  $	��218��6�   $	��790��6  $	 125��6  $ 4��6#  ��6   �� *Filter+	 �� SelectedItem	 6M   �� *
Selected+
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 60 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *Id+6L     5      $ Naziv��6)   $	 120 6R    68   �� *Naziv+6R     5      $	 Tip��6)   $	 120 6V    66   �� *Tip.ImeTipa+6V     5      $ Vrednost��6)   $ 80 6h    6;   �� *Temperatura+ �� Mode$ TwoWay=�6h     5      $  ��6)   $ * 63   5      �� 5      $��ID��6   $��44��6!   $	 354��6,   $ 4��6=   $��25��6M   �� 5      $��50��6/   $ 87��6:   $	 358��6K   ��6   �� *	Temp.Id+6K   �� 5      
���  ��$	 142��6T   $	 360��6f   ��6   �� *Temp.ValidationErrors[Id]+6f   �� 5!      $��Naziv��6   $	 384��6$   $ 4��65   $��26��6E   $��44��6Q   �� 5"      $��50��62   $ 87��6=   $	 388��6N   ��6   �� *
Temp.Naziv+6N   �� 5#      
���  ��$	 142��6W   $	 389��6i   ��6   �� *Temp.ValidationErrors[Naziv]+6i   �� 5$      
���  ��$ 87��6K   $	 410��6\   ��6   �� *DuplicateIdError+6\   �� 5&      $	��Tip��6   $ 4��6"   $	 425��62   $��44��6C   �� 5'      -   5         ��Name$ cmbTip��5'       �� DisplayMemberPath$ ImeTipa��6'    �� SelectedIndex$ 0��6C    �� 
IsEditable .  $	��110��6�   $ 87��6�   $	 428��6�   ��6h   �� *Tipovi+ �� SelectedValue 6�   �� *
Temp.Tip+6�   �� 5(      $	��129��6;   $��Center=�6G   $��81��6d   $	 358��6p   $	 434��6�   ��6   �� *Temp.Tip.IzvorSlike+6�   �� 5*      $	��Add��6   $��Center=�5+      $��60��5,       J� RenderTransformOrigin$ 3.302,-1.681&�5-   8   $	 288��6]   $	 360��6o   ��6   �� *
AddCommand+6o   �� 5/      $��Delete��6   $��Center=�50      $��60��51      $	 288��52   ;   $	 391��6M   ��6   �� *DeleteCommand+6M   �� 56      $
��Type��6   $ 29��6#   $ 25��64   $��26��6D   $��36��6P   �� 57      $��ID��6   $ 29��6!   $ 56��62   $��36��6B   �� 58      -   5        $ cbFilter��58       .  $ ImeTipa��6[   $ 87��6�   $ 25��6�   $	��110��6�   $��26��6�   ��6<   �� *Tipovi+ 6w   �� *	FilterT+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6  �� 59      -   5         ��Name$ tbFilter��59      $	��110��6O   $ 87��6[   $ 56��6l   $��26��6|   ��6(   �� *
FilterID+$ TwoWay=�6|   �� 5:      -   5         ��Name$ btClear��5:      $��Center=�6H   $��Filter��6e   $��26��6v   $��70��6�   $	 283��6�   $ 24��6�   ��6&   �� *FilterCommand+6�   �� 5;      -   5        $ btFilter��5;      $��Center=�6N   $��Clear��6k   $��26��6{   $��70��6�   $	 283��6�   $ 60��6�   ��6'   �� *ClearFilterCommand+6�   	� 5<      -   5         	�Name$ rbMore��5<   y   $��>��6   $	 239��6�   $ 31��6�   $��15��6�   $��29��6�    d� 	IsChecked 6)   �� *	FilterV+$ TwoWay=�$ PropertyChanged=�6�   	� 5=      -   5        $ rbLess��5=   y   $��<��6   $	 239��6�   $ 62��6�    6)   �� *	FilterM+$ TwoWay=�$ PropertyChanged=�6�   � 5A      .+�5B      	� $��Nevalidne Vrednosti��6   $
��Left=�6�   $	 407��6�   $ 30��6�   $	��130��6�   $��383,64,-383.4,-52.6q�6�    6<   �� *FilterOutOfRange+$ TwoWay=�$ PropertyChanged=�6�   	� 5C      $��Validne Vrednosti��6   $
��Left=�6�   $	 407��6�   $ 62��6�   $	��130��6�   $��383,29,-383.4,-18.6q�6�    6:   �� *FilterInRange+$ TwoWay=�$ PropertyChanged=�6�   5D      5E      �� 5F   
   $��Network Entities��6   $��30(�6+   $
��Bold"�69   $	 316��6K   $ 30��6]   5G       J� InputBindings 5I      �� 5J   
    �� 	Modifiers$
 Ctrlf�5K      
 �� Key$ F��5L       �� Command 5J      �� *FilterCommand+5L      �� 5M   
   $
 Ctrlf�5N      $ G��5O       5M      �� *ClearFilterCommand+5O      �� 5P   
   $
 Ctrlf�5Q      $ D��5R       5P      �� *DeleteCommand+5R      �� 5S   
   $
 Ctrlf�5T      $ P��5U       5S      �� *
AddCommand+5U      �� 5V   
   $
 Ctrlf�5W      $ Z��5X       5V      �� *UndoCommand+5X      5[      
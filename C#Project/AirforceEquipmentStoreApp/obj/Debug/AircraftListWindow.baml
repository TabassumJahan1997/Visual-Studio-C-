   M S B A M L   `   `   `  ????T  PAirforceEquipmentStoreApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AirforceEquipmentStoreAppAirforceEquipmentStoreApp  5   	   2    ,AirforceEquipmentStoreApp.AircraftListWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:AirforceEquipmentStoreApp     5   	     ? Title$  AircraftListWindow??5   	   $	??641??6$   $	??930??61   ???	#FF05021D ? AllowsTransparency .  ? WindowStyle$
 None=?6n    ? WindowStartupLocation$ CenterScreen=?6?   .??5	      ? .+?5   
   ?? $	??641??6   $	??Top=?6   ???	#FF132840 J? Opacity$	 0.8Q?6M   .+?5      ?? -   5         ??Name$ lstAircraftInfo??5      $	??525??60    ?? Left$ 37??6=   
 ?? Top$ 64??6N   $	??846??6^   ???	#FF316191???	#FF090A0C
???   ?$??Perpetua)?6?   $??20(?6?    ?? View 5      ?? 5      .P?5      ?? 	 ?? CellTemplate	 5      ?? 5   "   .R?5   &   ?? $
??Left=?6,   $
??Auto??6G   $	??Top=?6U   $	??120??6m   ??6y   ?? *Imagesource+5   #   5      ?? 5      $??Sl no.??6)   
 ?? Width$
 40??69    ?? DisplayMemberBinding 6D   ?? *SlNo+?? 5      $??Aircraft??6)   $	
 130??6;    6G   ?? *AircraftName+?? 5      $??Origin??6)   $	
 120??69    6E   ?? *Origin+?? 5      $
??Type??6)   $	
 130??67    6C   ?? *Type+?? 5      $??Status??6)   $	
 100??69    6E   ?? *Status+?? 5#      $
??Edit??6)   	 5$      ?? 5%   "   .R?5&   &   ?? $??AircraftInfoEdit??6r   -   5        $??30??5&   -   $??50??69   
?????? $??2q?6^   ??6?   ?? ??5'   *   ?? 5(   .    ?? ImageSource$; 5/Images/pngtree-vector-edit-icon-png-image_890175.jpg??69    j? Stretch$ Uniform=?6}   5*   '   5+   #   5-      ?? 5/      $??Delete??6)   	 50      ?? 51   "   .R?52   &   ?? $??AircraftInfoDelete??6r   -   5        $??30??52   -   $??50??69   
?????? $??2q?6^   ??6?   ?? ??53   *   ?? 54   .   $ /Images/download.png??69   $ Uniform=?6\   56   '   57   #   59      ?? 5;      $
??View??6)   	 5<      ?? 5=   "   .R?5>   &   ??  ??Name$ AircraftInfoView??6r   -   5        $??30??5>   -   $??50??69   
?????? $??2q?6^   ??6?   ?? ??5?   *   ?? 5@   .   $! Images/view-icon-png-17.jpg??69   $ Uniform=?6c   5B   '   5C   #   5E      5G      5I      5M      ?? 5N   
   -   5        $ btnBack??5N      $
??Back??6"   $
??Left=?61   $	??Top=?6L   $	??202??6d   $??37??6p   ???	#FF0B1927$	 0.8Q?6?   
???????$??Perpetua)?6?   $??26(?6?   $
??Bold"?6?   $??
63,594,0,0q?6    ? 	Resources 5O       ?? 
TargetType ?? Property ?? Value??%_   '??       ?? 5P      " ??6   .?5Q      ??  ?@??CornerRadius$ 12??65   5R      5S      5T      ?? 5U   
   -   5        $ btnExit??5U      $
??Exit??6"   $
??Left=?61   $	??202??6L   ???	#FF0B1927$	 0.8Q?6o   
???????$??Perpetua)?6?   $??26(?6?   $
??Bold"?6?   $??660,594,0,10q?6?    5V      ??%_   '??       ?? 5W      " ??6   .?5X      ??  ?@??CornerRadius$ 12??65   5Y      5Z      5[      ?? 5\   
   $
??Left=?6   $??
352,10,0,0q?6/   $
??Wrap=?6C   $??Aircraft Information??6W   
???????$??Perpetua)?6?   $??34(?6?   $	??Top=?6?   $??41??6?   $	??284??6?   $??	Underline{?6?   ?? 5]   
   -   5         ??Name$ txtTotalCount??5]      $
??Left=?6)   $??26??6D   $??
760,26,0,0q?6P    ?? TextWrapping$
 Wrap=?6d   $??TextBox??6x   $	??Top=?6?   $	??142??6?   $??Perpetua)?6?   $??18(?6?    ?? TextAlignment$ Center=?6?   
?????? 
???????5^      5`      5a      
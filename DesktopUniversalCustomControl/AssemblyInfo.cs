using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsPrefix("http://chance-wpf/2020/xaml", "chance")]

[assembly: XmlnsDefinition("http://chance-wpf/2020/xaml", "DesktopUniversalCustomControl")]
[assembly: XmlnsDefinition("http://chance-wpf/2020/xaml", "DesktopUniversalCustomControl.CustomComponent")]
[assembly: XmlnsDefinition("http://chance-wpf/2020/xaml", "DesktopUniversalCustomControl.CustomView.MsgDlg")]

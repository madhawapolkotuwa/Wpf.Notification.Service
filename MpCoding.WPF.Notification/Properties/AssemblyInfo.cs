using System.Runtime.InteropServices;
using System.Windows.Markup;
using System.Windows;

// In SDK-style projects such as this one, several assembly attributes that were historically
// defined in this file are now automatically added during build and populated with
// values defined in project properties. For details of which attributes are included
// and how to customise this process see: https://aka.ms/assembly-info-properties


// Setting ComVisible to false makes the types in this assembly not visible to COM
// components.  If you need to access a type in this assembly from COM, set the ComVisible
// attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM.

[assembly: Guid("237fed9a-74f0-41f1-96b2-7614a059f4e7")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsPrefix("http://schemas.mpcoding.com/mpcoding/wpf/notification", "WPFNotification")]

[assembly: XmlnsDefinition("http://schemas.mpcoding.com/mpcoding/wpf/notification", "MpCoding.WPF.Notification.Dictionaries")]
[assembly: XmlnsDefinition("http://schemas.mpcoding.com/mpcoding/wpf/notification", "MpCoding.WPF.Notification.Controls")]
[assembly: XmlnsDefinition("http://schemas.mpcoding.com/mpcoding/wpf/notification", "MpCoding.WPF.Notification.Converters")]
[assembly: XmlnsDefinition("http://schemas.mpcoding.com/mpcoding/wpf/notification", "MpCoding.WPF.Notification.Enums")]
[assembly: XmlnsDefinition("http://schemas.mpcoding.com/mpcoding/wpf/notification", "MpCoding.WPF.Notification.Servicers")]
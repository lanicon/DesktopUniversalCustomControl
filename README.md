# CustomControl
## 介绍
C#WPF自定义控件集，也支持WinFrom，项目基于.netFrameWork4.6.1,完全原生开发，没有使用任何第三方控件，可以直接用在你的项目中，该项目会一直更新，后面会移植到.net Core 3.1上.
***

**如果我的代码对您有帮助，请给厨子一个星星,谢谢，您的赞赏是我最大的动力**
***
## 使用方法
- 在WPF中使用方法：
```XAML
xmlns:cus="clr-namespace:CustomControl.CustomComponent;assembly=CustomControl
```
- 在WinForm中使用方法：
项目中添加WPF用户控件，然后在xaml文件中引入以下即可
```XAML
xmlns:cus="clr-namespace:CustomControl.CustomComponent;assembly=CustomControl
```
**WPF项目和WinForm项目都需要引入CustomControl这个依赖库，并且把库文件放到项目生成文件夹下面即可，即与（.exe）程序同目录**
***
## 接下来展现每个组件的效果，并且您可以进一步自定义和扩展
### ImageButton
![效果](Resource/Images/imageBtn.gif)

MessageBox消息窗口
ImageButton、CustomComboBox、CustomComboBoxItem、CustomPopupEx
CustomPasswordBox、CustomProgressBar、CustomTextControl、QrCodeControl、
SwitchControl

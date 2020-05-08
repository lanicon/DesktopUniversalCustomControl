# DesktopCustomControl
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

## 接下来展现每个组件的效果，您可以在此基础上进一步自定义和扩展

### 1\. ImageButton （图片按钮控件）
![效果](Resource/Demo/ImageButton.gif)

可以这样使用得到上面的结果，以下写的属性是添加的依赖属性：
```xaml
示例代码
<cus:ImageButton Width="300" Height="40" 
                ImageWidth="30" ImageHeight="30" ImageButtonSource="tom.jpg" ImageVisibility="Visible"
                ImageButtonContent="TOM" Foreground="Red" CornerRadius="10" Background="GreenYellow" OverBackground="BlueViolet"/>
```
属性说明：
- ImageButtonSource 图片资源路径
- ImageButtonContent 内容
- ImageVisibility 图片是否可见
- ImageWidth/ImageHeight 长宽
- OverBackground 鼠标浮动背景色
- CornerRadius 圆角
***

### 2\. CustomTextControl （文本输入控件）
![效果](Resource/Demo/CustomTextControl.gif)
```xaml
示例代码
<cs:CustomTextControl Width="300" Height="40" 
CornerRadius="10" TextInputType="digitAndLetter" TextPlaceHolder="请输入"/>
```
```     
    /// <summary>
    /// 文本输入类型
    /// </summary>
    public enum TextInputType  
    {
        defaultText = 0,//普通文本(包括汉字)
        digit = 1, //仅实数
        letter = 2,  //仅字母
        digitOrLetterLine = 3, //数字、字母或下划线
        digitAndLetter = 4, //数字和字母(密码使用)
        chinese = 5,  //汉字
    }
```
属性说明：
- CornerRadius 圆角
- TextPlaceHolder占位符提示
- TextInputType 文本输入类型
***

### 3\. SwitchControl（开关控件）
![效果](Resource/Demo/SwitchControl.gif)
```
示例代码
<cus:SwitchControl Width="100" Height="30" Foreground="Red" SwitchContent="关"
                    SwitchOpenBackground="BlueViolet" SwicthCloseBackground="YellowGreen"/>
```
属性说明：
- CornerRadius 圆角
- SwitchContent 内容
- SwitchOpenBackground  打开的背景颜色
- SwicthCloseBackground  关闭的背景颜色
***

### 4\. QRCodeControl（二维码控件）
这个控件需要在vs中下载一个QRCoder包就可以使用了

![效果](Resource/Demo/QRCodeControl.gif)

属性说明：
- CornerRadius 圆角
- QRCodeImage  二维码图片,这个不要设置
- QRCodeContent  二维码内容
- QRCodeIcon  二维码图标
- QrCodeIconSizePercent 图标百分比大小，建议小于20，大了无法扫码
- QrCodeIconBorderWidth 图标外边框厚度大小
- QrCodePixelsPerModule 二维码像素
- Foreground  二维码前置颜色
- Background  二维码背景色
***

### 5\.CustomComboBox (可编辑下拉控件)
![效果](Resource/Demo/CustomComboBox.gif)
```
示例代码
<cus:CustomComboBox Width="300" Height='40' ToolTip="下拉框"
                    CornerRadius="10" IsEditable="False"
                    ComboBoxListBackground="BlueViolet" 
                    ComboxBoxItemMouseOverBackground="Yellow"
                    ToggleButtonBackground="Green">
    <cus:CustomComboBoxItem Content="中国"></cus:CustomComboBoxItem>
    <cus:CustomComboBoxItem Content="UK"></cus:CustomComboBoxItem>
    <cus:CustomComboBoxItem Content="America"></cus:CustomComboBoxItem>
</cus:CustomComboBox>
```
- CornerRadius 圆角
- ComboBoxListBackground 下拉框背景颜色
- ComboxBoxItemMouseOverBackground 鼠标悬浮时背景颜色
- ToggleButtonBackground 按钮框填充色
***

### 6\. MutilComboBoxControl （多选可编辑下拉控件）
继承CustomComboBox ，支持text、checkbox、button

![效果](Resource/Demo/MutilComboBoxControl.gif)
```
示例代码
<cus:MutilComboBoxControl Width="300" Height='40' x:Name="mcb" ItemsSource="{Binding list}" 
                            IsEditable="True" StrokeLineColor="Yellow" AddDeleteFun="True"
                            cus:MutilComboBoxControl.SelectedType="MutilItem"/>
```
- StrokeLineColor 删除X符号的颜色
- AddDeleteFun 是否添加删除按钮
- SelectedType 表示只有Checked点击有效，否则表示整个子项点击有效
***


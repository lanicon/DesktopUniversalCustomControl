using DesktopUniversalCustomControl.ExposedMethod;
using DesktopUniversalCustomControl.Service.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopUniversalCustomControl.CustomComponent
{
    /// <summary>
    /// ChartControlView
    /// 图表控件
    /// </summary>
    public class ChartControlView : Control
    {
        static ChartControlView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChartControlView), new FrameworkPropertyMetadata(typeof(ChartControlView)));
        }

        public ChartControlView()
        {

        }


        //旧数据
        public static readonly DependencyProperty ChartDataCollectionProperty =
            DependencyProperty.Register("ChartDataCollection", typeof(PointCollection), typeof(ChartControlView), 
                new PropertyMetadata(default(PointCollection), OnChartDataCollectionChanged));

        //新数据
        public static readonly DependencyProperty ChartNewDataCollectionProperty =
            DependencyProperty.RegisterAttached("ChartNewDataCollection", typeof(PointCollection), typeof(ChartControlView),
                new PropertyMetadata(default(PointCollection)));

        public PointCollection ChartDataCollection
        {
            get { return (PointCollection)GetValue(ChartDataCollectionProperty); }
            set { SetValue(ChartDataCollectionProperty, value); }
        }

        public static PointCollection GetChartNewDataCollection(DependencyObject obj)
        {
            return (PointCollection)obj.GetValue(ChartNewDataCollectionProperty);
        }

        public static void SetChartNewDataCollection(DependencyObject obj, PointCollection value)
        {
            obj.SetValue(ChartNewDataCollectionProperty, value);
        }

        //数据改变时发生
        private static void OnChartDataCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d != null && d is ChartControlView)
            {
                var chartControl = d as ChartControlView;
                if (chartControl.ChartDataCollection != null)
                    chartControl.GetNewPointDataCollection(chartControl);
            }
        }
        /// <summary>
        /// 由X轴刻度值计算得到新的坐标集合
        /// </summary>
        /// <param name="chartControl"></param>
        private void GetNewPointDataCollection(ChartControlView chartControl)
        {
            Console.WriteLine(chartControl.ChartDataCollection);
            PointCollection pointDataCollection = new PointCollection(chartControl.ChartDataCollection.Count);
            for (int i = 0; i < chartControl.ChartDataCollection.Count; i++)
            {
                pointDataCollection.Add(new Point(chartControl.CenterPoint.X + chartControl.ChartDataCollection[i].X * chartControl.XTickValueInterval,
                                                  chartControl.CenterPoint.Y - chartControl.ChartDataCollection[i].Y * chartControl.YTickValueInterval));
            }
            SetChartNewDataCollection(chartControl, pointDataCollection);

            //拷贝
            //chartControl.ChartDataCollection = chartControl.ChartDataCollection.Clone();
            //chartControl.ChartDataCollection.Clear();
            //chartControl.ChartDataCollection = new PointCollection(pointDataCollection);
            Console.WriteLine("计算");
            Console.WriteLine(GetChartNewDataCollection(chartControl));
        }


        /// <summary>
        /// 坐标原点
        /// </summary>
        public Point CenterPoint
        {
            get { return (Point)GetValue(CenterPointProperty); }
            set { SetValue(CenterPointProperty, value); }
        }

        public static readonly DependencyProperty CenterPointProperty =
            DependencyProperty.Register("CenterPoint", typeof(Point), typeof(ChartControlView), new PropertyMetadata(default(Point), OnTicksChanged));


        /// <summary>
        /// X轴长度
        /// </summary>
        public Point XEndPoint
        {
            get { return (Point)GetValue(XEndPointProperty); }
            set { SetValue(XEndPointProperty, value); }
        }

        public static readonly DependencyProperty XEndPointProperty =
            DependencyProperty.Register("XEndPoint", typeof(Point), typeof(ChartControlView), new PropertyMetadata(default(Point), OnTicksChanged));


        /// <summary>
        /// Y轴长度
        /// </summary>
        public Point YEndPoint
        {
            get { return (Point)GetValue(YEndPointProperty); }
            set { SetValue(YEndPointProperty, value); }
        }

        public static readonly DependencyProperty YEndPointProperty =
            DependencyProperty.Register("YEndPoint", typeof(Point), typeof(ChartControlView), new PropertyMetadata(default(Point), OnTicksChanged));


        /// <summary>
        /// X刻度数
        /// </summary>
        public int XCoordinateTicks
        {
            get { return (int)GetValue(XCoordinateTicksProperty); }
            set { SetValue(XCoordinateTicksProperty, value); }
        }

        public static readonly DependencyProperty XCoordinateTicksProperty =
            DependencyProperty.Register("XCoordinateTicks", typeof(int), typeof(ChartControlView), new PropertyMetadata(5, OnTicksChanged));


        /// <summary>
        /// Y刻度数
        /// </summary>
        public int YCoordinateTicks
        {
            get { return (int)GetValue(YCoordinateTicksProperty); }
            set { SetValue(YCoordinateTicksProperty, value); }
        }

        public static readonly DependencyProperty YCoordinateTicksProperty =
            DependencyProperty.Register("YCoordinateTicks", typeof(int), typeof(ChartControlView), new PropertyMetadata(5, OnTicksChanged));


        private static void OnTicksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null && d is ChartControlView)
            {
                var chartControl = d as ChartControlView;
                //chartControl.ApplyTemplate();
                //Console.WriteLine("1111===================1111");
            }
        }


        /// <summary>
        /// X轴平均刻度值
        /// </summary>
        public double XTickValueInterval
        {
            get { return (double)GetValue(XTickValueIntervalProperty); }
            set { SetValue(XTickValueIntervalProperty, value); }
        }

        public static readonly DependencyProperty XTickValueIntervalProperty =
            DependencyProperty.Register("XTickValueInterval", typeof(double), typeof(ChartControlView), new PropertyMetadata(default(double)));


        /// <summary>
        /// Y轴平均刻度值
        /// </summary>
        public double YTickValueInterval
        {
            get { return (double)GetValue(YTickValueIntervalProperty); }
            set { SetValue(YTickValueIntervalProperty, value); }
        }

        public static readonly DependencyProperty YTickValueIntervalProperty =
            DependencyProperty.Register("YTickValueInterval", typeof(double), typeof(ChartControlView), new PropertyMetadata(default(double)));


        /// <summary>
        /// 坐标轴颜色
        /// </summary>
        public Brush StrokeCoordinate
        {
            get { return (Brush)GetValue(StrokeCoordinateProperty); }
            set { SetValue(StrokeCoordinateProperty, value); }
        }

        public static readonly DependencyProperty StrokeCoordinateProperty =
            DependencyProperty.Register("StrokeCoordinate", typeof(Brush), typeof(ChartControlView), new PropertyMetadata(Brushes.Red));


        /// <summary>
        /// 坐标轴厚度
        /// </summary>
        public double StrokeThicknessCoordinate
        {
            get { return (double)GetValue(StrokeThicknessCoordinateProperty); }
            set { SetValue(StrokeThicknessCoordinateProperty, value); }
        }

        public static readonly DependencyProperty StrokeThicknessCoordinateProperty =
            DependencyProperty.Register("StrokeThicknessCoordinate", typeof(double), typeof(ChartControlView), new PropertyMetadata(2.0));


        /// <summary>
        /// X轴描述
        /// </summary>
        public string XAxisDescription
        {
            get { return (string)GetValue(XAxisDescriptionProperty); }
            set { SetValue(XAxisDescriptionProperty, value); }
        }

        public static readonly DependencyProperty XAxisDescriptionProperty =
            DependencyProperty.Register("XAxisDescription", typeof(string), typeof(ChartControlView), new PropertyMetadata(default(string)));


        /// <summary>
        /// XAxisMargin
        /// </summary>
        public Thickness XAxisMargin
        {
            get { return (Thickness)GetValue(XAxisMarginProperty); }
            set { SetValue(XAxisMarginProperty, value); }
        }

        public static readonly DependencyProperty XAxisMarginProperty =
            DependencyProperty.Register("XAxisMargin", typeof(Thickness), typeof(ChartControlView), new PropertyMetadata(default(Thickness)));


        /// <summary>
        /// Y轴描述
        /// </summary>
        public string YAxisDescription
        {
            get { return (string)GetValue(YAxisDescriptionProperty); }
            set { SetValue(YAxisDescriptionProperty, value); }
        }

        public static readonly DependencyProperty YAxisDescriptionProperty =
            DependencyProperty.Register("YAxisDescription", typeof(string), typeof(ChartControlView), new PropertyMetadata(default(string)));


        /// <summary>
        /// YAxisMargin
        /// </summary>
        public Thickness YAxisMargin
        {
            get { return (Thickness)GetValue(YAxisMarginProperty); }
            set { SetValue(YAxisMarginProperty, value); }
        }

        public static readonly DependencyProperty YAxisMarginProperty =
            DependencyProperty.Register("YAxisMargin", typeof(Thickness), typeof(ChartControlView), new PropertyMetadata(default(Thickness)));


        /// <summary>
        /// X轴刻度描述 
        /// </summary>
        [TypeConverter(typeof(StringToListTypeConver))]
        public List<string> XTickDescription
        {
            get { return (List<string>)GetValue(XTickDescriptionProperty); }
            set { SetValue(XTickDescriptionProperty, value); }
        }

        public static readonly DependencyProperty XTickDescriptionProperty =
            DependencyProperty.Register("XTickDescription", typeof(List<string>), typeof(ChartControlView), new PropertyMetadata(default(List<string>), TicksDescriptionChanged)); 


        /// <summary>
        /// XTicksMargin
        /// </summary>
        public Thickness XTicksMargin
        {
            get { return (Thickness)GetValue(XTicksMarginProperty); }
            set { SetValue(XTicksMarginProperty, value); }
        }

        public static readonly DependencyProperty XTicksMarginProperty =
            DependencyProperty.Register("XTicksMargin", typeof(Thickness), typeof(ChartControlView), new PropertyMetadata(default(Thickness)));


        /// <summary>
        /// Y轴刻度描述 
        /// </summary>
        [TypeConverter(typeof(StringToListTypeConver))]
        public List<string> YTickDescription
        {
            get { return (List<string>)GetValue(YTickDescriptionProperty); }
            set { SetValue(YTickDescriptionProperty, value); }
        }

        public static readonly DependencyProperty YTickDescriptionProperty =
            DependencyProperty.Register("YTickDescription", typeof(List<string>), typeof(ChartControlView), new PropertyMetadata(default(List<string>), TicksDescriptionChanged));


        /// <summary>
        /// YTicksMargin
        /// </summary>
        public Thickness YTicksMargin
        {
            get { return (Thickness)GetValue(YTicksMarginProperty); }
            set { SetValue(YTicksMarginProperty, value); }
        }

        public static readonly DependencyProperty YTicksMarginProperty =
            DependencyProperty.Register("YTicksMargin", typeof(Thickness), typeof(ChartControlView), new PropertyMetadata(default(Thickness)));


        //刻度描述更改时
        private static void TicksDescriptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null && d is ChartControlView)
            {
                var chartControl = d as ChartControlView;
                //chartControl.OnApplyTemplate();
                Console.WriteLine("XTickValueInterval=" + chartControl.XTickValueInterval);
                Console.WriteLine("YTickValueInterval=" + chartControl.YTickValueInterval);
            }
        }


        /// <summary>
        /// 图标类型
        /// </summary>
        public ChartType ChartType
        {
            get { return (ChartType)GetValue(ChartTypeProperty); }
            set { SetValue(ChartTypeProperty, value); }
        }

        public static readonly DependencyProperty ChartTypeProperty =
            DependencyProperty.Register("ChartType", typeof(ChartType), typeof(ChartControlView), 
                new PropertyMetadata(default(ChartType), ChartTypeChanged));

        private static void ChartTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chartControl = d as ChartControlView;
            if (chartControl.TemplateCanvas != null)
            {
                ChartType type = (ChartType)e.NewValue;
                OnDrawMap(type, chartControl, chartControl.TemplateCanvas);
            }
        }

        //画图
        private static void OnDrawMap(ChartType type, ChartControlView chartControl, Canvas TemplateCanvas)
        {
            foreach (var canvas in chartControl.TemplateRoot.Children)
            {
                (canvas as Canvas).Visibility = Visibility.Visible;
            }

            var standPoints = GetChartNewDataCollection(chartControl);
            Canvas OtherHistogram = chartControl.Template.FindName("otherHistogram", chartControl) as Canvas;
            Canvas Fangraph = chartControl.Template.FindName("Fangraph", chartControl) as Canvas;
            switch (type)
            {
                case ChartType.PolyLine: //折线图
                    {
                        TemplateCanvas.Children.Remove(OtherHistogram);
                        //TemplateCanvas.Children.Remove(OtherFangraph);
                        foreach (var item in TemplateCanvas.Children)
                        {
                            Console.WriteLine("子项" + item.GetType().Name);
                        }
                    }
                    break;
                case ChartType.Cylinder:
                    {
                        TemplateCanvas.Children.Remove(OtherHistogram);
                    }
                    break;
                case ChartType.Histogram:  //直方图
                    {
                        Path Histogram = TemplateCanvas.FindName("Histogram") as Path;
                        double width = 2 * chartControl.XTickValueInterval / 3;
                        //double intervalHeight = chartControl.YTickValueInterval;                       
                        PointCollection RightTopPoints = standPoints;
                        PointCollection LeftTopPoints = new PointCollection();
                        for (int i = 0; i < standPoints.Count; i++)
                        {
                            LeftTopPoints.Add(new Point(standPoints[i].X - width, standPoints[i].Y));
                        }
                        PointCollection LeftBottomPoints = new PointCollection();
                        for (int i = 0; i < standPoints.Count; i++)
                        {
                            LeftBottomPoints.Add(new Point(standPoints[i].X - width, chartControl.CenterPoint.Y));
                        }
                        PointCollection RightBottomPoints = new PointCollection();
                        for (int i = 0; i < standPoints.Count; i++)
                        {
                            RightBottomPoints.Add(new Point(standPoints[i].X, chartControl.CenterPoint.Y));
                        }

                        string data = string.Empty;
                        for (int i = 0; i < standPoints.Count; i++)
                        {
                            string data1 = $"M {RightTopPoints[i].X},{RightTopPoints[i].Y} {LeftTopPoints[i].X},{LeftTopPoints[i].Y}";
                            string data2 = $" {LeftBottomPoints[i].X},{LeftBottomPoints[i].Y} {RightBottomPoints[i].X},{RightBottomPoints[i].Y} ";
                            data += data1 + data2;
                        }
                        
                        Histogram.Data = Geometry.Parse(data);
                        
                        for (int i = 0; i < chartControl.ChartDataCollection.Count; i++)
                        {
                            TextBlock textBlock = new TextBlock();
                            textBlock.Text = chartControl.ChartDataCollection[i].Y.ToString();
                            textBlock.Foreground = Brushes.White;
                            textBlock.TextAlignment = TextAlignment.Center;
                            textBlock.Margin = new Thickness(standPoints[i].X - 30, standPoints[i].Y - 20, 0, 0);
                            OtherHistogram.Children.Add(textBlock);
                        }
                        if(!TemplateCanvas.Children.Contains(OtherHistogram))
                            TemplateCanvas.Children.Add(OtherHistogram);

                        Console.WriteLine("DATA:" + data);
                        Console.WriteLine("柱形图：" + Histogram.Name);
                    }
                    break;
                case ChartType.Dottedgram:
                    break;
                case ChartType.Fangraph:  //饼图
                    {
                        foreach (var canvas in chartControl.TemplateRoot.Children)
                        {
                            if ((canvas as Canvas).Name.Equals("drawChart"))
                                (canvas as Canvas).Visibility = Visibility.Visible;
                            else
                                (canvas as Canvas).Visibility = Visibility.Collapsed;
                        }

                        //圆的方程(x-a)^2 + (y-b)^2 = r^2
                        List<double> anglesList = new List<double>(); //角度
                        double sumY = 0;
                        foreach (var p in chartControl.ChartDataCollection)
                        {
                            sumY += p.Y;
                        }
                        foreach (var p in chartControl.ChartDataCollection)
                        {
                            anglesList.Add(p.Y / sumY * 2 * Math.PI);
                        }

                        //圆上分割点
                        List<Point> points = new List<Point>();
                        //圆中心点
                        //Point center = new Point(0, 0);
                        Point center = new Point(chartControl.Width / 2, chartControl.Height / 2);
                        //圆的半径
                        double r = chartControl.Width > chartControl.Height ? 2 / 5D * chartControl.Height : 2 / 5D * chartControl.Width;
                        double angle = 0D;
                        for (int i = 0; i < chartControl.ChartDataCollection.Count; i++)
                        {                        
                            angle += anglesList[i];
                            double x = center.X + r * Math.Cos(angle);
                            double y = center.Y + r * Math.Sin(angle);
                            points.Add(new Point(x, y));
                        }

                        Path ellipsePath = new Path();
                        ellipsePath.Data = new EllipseGeometry(center, r, r);
                        //ellipsePath.Stroke = Brushes.Red;
                        //ellipsePath.StrokeThickness = 2;
                        Fangraph.Children.Add(ellipsePath);

                        Brush[] brushes = new Brush[7] { Brushes.Red, Brushes.BlueViolet, Brushes.Yellow, Brushes.Blue, Brushes.SaddleBrown, Brushes.Purple, Brushes.BlueViolet };
                        for (int i = 0; i < points.Count; i++)
                        {
                            Path path = new Path();
                            string source = "";
                            if (i == points.Count - 1)
                                source = $"M {points[i].X},{points[i].Y} A {r},{r} 50 0 1 {points[0].X},{points[0].Y} L {center.X},{center.Y}";
                            else
                                source = $"M {points[i].X},{points[i].Y} A {r},{r} 50 0 1 {points[i + 1].X},{points[i + 1].Y} L {center.X},{center.Y}";
                            path.Data = Geometry.Parse(source);
                            path.Fill = brushes[i];
                            Fangraph.Children.Add(path);
                        }
                        ////画小圆
                        //for (int i = 0; i < points.Count; i++)
                        //{
                        //    Path path = new Path();
                        //    path.Data = new EllipseGeometry(points[i], 10, 10);
                        //    path.Fill = brushes[i];
                        //    Fangraph.Children.Add(path);
                        //}

                        ////如果原点为左上角，则需要平移
                        //TranslateTransform transform = new TranslateTransform(300, 300);
                        //Fangraph.RenderTransform = transform;

                        //int roateAngle = 180;
                        //while (true)
                        //{
                        //    roateAngle += 1;
                        //    RotateTransform rotateTransform = new RotateTransform(roateAngle, 300, 300);
                        //    Fangraph.RenderTransform = rotateTransform;
                        //    Thread.Sleep(50);
                        //}

                        //DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
                        //Duration duration = new Duration(TimeSpan.FromSeconds(3));
                        //DiscreteDoubleKeyFrame discreteDoubleKeyFrame = new DiscreteDoubleKeyFrame(roateAngle);
                        //doubleAnimationUsingKeyFrames.Duration = duration;
                        //doubleAnimationUsingKeyFrames.KeyFrames.Add(discreteDoubleKeyFrame);
                        //AnimationClock clock = doubleAnimationUsingKeyFrames.CreateClock();
                        //Fangraph.ApplyAnimationClock(RenderTransformProperty, clock);                       
                    }
                    break;
                default:
                    break;
            }
        }

        private Canvas TemplateCanvas; //大画板
        private Grid TemplateRoot; //
        /// <summary>
        /// 重写ChartControlView控件
        /// </summary>
        public override void OnApplyTemplate()
        {
            TemplateRoot = this.Template.FindName("TemplateRoot", this) as Grid;
            Canvas coordinateCanvas = this.Template.FindName("coordinateSystem", this) as Canvas;
            CoordinateSystem coordinateSystem = coordinateCanvas.FindName("coordinate") as CoordinateSystem;
            Canvas xCanvas = this.Template.FindName("xTicks", this) as Canvas;
            Canvas yCanvas = this.Template.FindName("yTicks", this) as Canvas;
            TemplateCanvas = this.Template.FindName("drawChart", this) as Canvas;                     
            Console.WriteLine("DrawChart==" + TemplateCanvas.Name);

            Canvas Fangraph = this.Template.FindName("Fangraph", this) as Canvas;
            Console.WriteLine("Fangraph==" + Fangraph.Name);

            //Canvas coordinateCanvas = ChartView.GetTemplateChild("coordinateSystem") as Canvas;
            //CoordinateSystem coordinateSystem = coordinateCanvas.FindName("coordinate") as CoordinateSystem;
            //Canvas xCanvas = ChartView.GetTemplateChild("xTicks") as Canvas;
            //Canvas yCanvas = ChartView.GetTemplateChild("yTicks") as Canvas;

            this.XTickValueInterval = coordinateSystem.XTickValue;
            this.YTickValueInterval = coordinateSystem.YTickValue;
            Console.WriteLine("TemplateXTickValueInterval=" + this.XTickValueInterval);
            Console.WriteLine("TemplateYTickValueInterval=" + this.YTickValueInterval);
            GetNewPointDataCollection(this);
            OnDrawMap(ChartType, this, TemplateCanvas);

            //动态增加X刻度描述
            if (this.XTickDescription != null)
            {
                for (int i = 0; i < this.XTickDescription.Count(); i++)
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = this.XTickDescription[i];
                    textBlock.Foreground = Brushes.White;
                    textBlock.TextAlignment = TextAlignment.Center;
                    textBlock.Margin = this.XTicksMargin;
                    xCanvas.Children.Add(textBlock);
                    xCanvas.RegisterName("xTick_tb" + i + 1, textBlock);
                    Canvas.SetLeft(textBlock, coordinateSystem.XTicks[i].X);
                    Canvas.SetTop(textBlock, coordinateSystem.XTicks[i].Y);
                }
            }
            //动态增加Y刻度描述
            if (this.YTickDescription != null)
            {
                for (int i = 0; i < this.YTickDescription.Count; i++)
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = this.YTickDescription[i];
                    textBlock.Foreground = Brushes.White;
                    textBlock.TextAlignment = TextAlignment.Center;
                    textBlock.Margin = this.YTicksMargin;
                    yCanvas.Children.Add(textBlock);
                    yCanvas.RegisterName("yTick_tb" + i + 1, textBlock);
                    Canvas.SetLeft(textBlock, coordinateSystem.YTicks[i].X);
                    Canvas.SetTop(textBlock, coordinateSystem.YTicks[i].Y);
                }
            }
        }
    }

    public enum ChartType
    {
        PolyLine = 1, //折线图
        Dottedgram = 2, //虚线图
        Histogram = 3, //直方图       
        Fangraph = 4, //饼图
        Cylinder = 5, //圆柱图
    }
}

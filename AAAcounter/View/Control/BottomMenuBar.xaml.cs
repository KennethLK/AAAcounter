using AAAcounter.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace AAAcounter.View
{
    public sealed partial class BottomMenuBar : UserControl
    {
        private Storyboard _creatStoryboard;
        private Storyboard _expandStoryboard;
        private const int _transDuration = 150;

        private bool _isExpand;

        /// <summary>
        /// 是否显示详情
        /// </summary>

        public Action<bool> OnBarStateChangedAction;

        public BottomMenuBar()
        {
            this.InitializeComponent();
        }

        #region  设定数据源
        /// <summary>
        /// 设定数据源
        /// </summary> 
        public void SetSource(List<BottomMenuItem> source)
        {
            // 创建消失动画
            _creatStoryboard = new Storyboard();

            QuadraticEase ease = new QuadraticEase() { EasingMode = EasingMode.EaseIn };
            foreach (var item in StackPanel_Bar.Children)
            {
                StoryboardHelper.CreatAnimation(item, _creatStoryboard, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)", _transDuration, 0, ease, false);
                StoryboardHelper.CreatAnimation(item, _creatStoryboard, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)", _transDuration, 0, ease, false);
                StoryboardHelper.CreatAnimation(item, _creatStoryboard, "(UIElement.Opacity)", _transDuration, 0, ease, false);
            }

            // 完成
            _creatStoryboard.Completed += (e1, e2) =>
            {
                _creatStoryboard = new Storyboard();

                foreach (Grid item in StackPanel_Bar.Children)
                    item.Children.Clear();

                StackPanel_Bar.Children.Clear(); //清空老的

                // 创建新的
                foreach (var item in source)
                    CreatChild(item, _creatStoryboard);

                _creatStoryboard.Begin();
            };
            _creatStoryboard.Begin();
        }

        //创建动画
        private void CreatChild(BottomMenuItem item, Storyboard sb)
        {
            Grid grid = new Grid();
            grid.Width = 58;
            grid.Children.Add(item.Icon);
            grid.Background = new SolidColorBrush(Colors.Transparent);
            grid.PointerReleased += (e1, e2) => 
            {
                grid.Opacity = 1;
            };
            grid.PointerPressed += (e1, e2) =>
            {
                grid.Opacity = 0.3; 
                if (_isExpand)  SetIsExpand(false);

                SafeRaise.Raise(item.TappedAction);
            };

            TextBlock text = new TextBlock();
            text.Opacity = 0;
            text.Text = item.Lable;
            text.VerticalAlignment = VerticalAlignment.Bottom;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 6;
            text.Foreground = new SolidColorBrush(Colors.Black);
            text.Margin = new Thickness(0, 0, 0, -5);
            grid.Children.Add(text);
            StackPanel_Bar.Children.Insert(0, grid);

            grid.Opacity = 0;
            grid.RenderTransformOrigin = new Point(0.5, 0.5);
            grid.RenderTransform = new CompositeTransform() { ScaleY = 0, ScaleX = 0 };

            EasingFunctionBase ease = new QuarticEase() { EasingMode = EasingMode.EaseOut };
            StoryboardHelper.CreatAnimation(grid, _creatStoryboard, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)", _transDuration, 1, ease, false);
            StoryboardHelper.CreatAnimation(grid, _creatStoryboard, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)", _transDuration, 1, ease, false);
            StoryboardHelper.CreatAnimation(grid, _creatStoryboard, "(UIElement.Opacity)", _transDuration, 1, ease, false);
        }
        #endregion

        #region 展开与关闭 
        // 点击更多
        private void More_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SetIsExpand(!_isExpand);
        }

        // 点击空白
        private void Canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (_isExpand)
                SetIsExpand(false);
            else
                Canvas_Touch.Visibility = Visibility.Collapsed;  //保护代码
        }

        /// <summary>
        /// 设定是否展开
        /// </summary> 
        private void SetIsExpand(bool isExpand)
        {
            _isExpand = isExpand;
            Canvas_Touch.Visibility = isExpand ? Visibility.Visible : Visibility.Collapsed;

            _expandStoryboard = new Storyboard();
            StoryboardHelper.CreatAnimation(Grid_Root, _expandStoryboard, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)",
                300, isExpand ? -12 : 0, new QuarticEase() { EasingMode = EasingMode.EaseOut }, false);

            foreach (Grid item in StackPanel_Bar.Children)
            {
                StoryboardHelper.CreatAnimation(item.Children[1], _expandStoryboard, "(UIElement.Opacity)", _transDuration,
                    isExpand ? 1 : 0, new PowerEase() { EasingMode = EasingMode.EaseOut }, false);
            }

            _expandStoryboard.Begin();

            SafeRaise.Raise<bool>(OnBarStateChangedAction, isExpand);
        }
        #endregion
    }

    /// <summary>
    /// 文字项
    /// </summary>
    public class BottomMenuItem
    {
        /// <summary>
        /// 文字信息
        /// </summary>
        public string Lable { get; set; }

        /// <summary>
        /// Icon图标
        /// </summary>
        public IconElement Icon { get; set; }

        /// <summary>
        /// 发生点击
        /// </summary>
        public Action TappedAction { get; set; }
    }
}

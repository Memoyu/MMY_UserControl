/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：CNavigationBar.cs
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2019/9/29 15:58:58 
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MMY_UserControlDemo.Controls
{
    public class CNavigationBar : FlowLayoutPanel
    {

        private List<FlowLayoutPanel> childList = new List<FlowLayoutPanel>();//子类集合
        private List<FlowLayoutPanel> parentList = new List<FlowLayoutPanel>();//父类集合

        public event MouseEventHandler ChildItemMouseClick = null;//子项鼠标事件
        public event MouseEventHandler ParentItemMouseClick = null;//父项鼠标事件

        public CNavigationBar()
        {
            this.AutoScroll = true;
            this.Width = 170;
            this.BackColor = Color.FromArgb(20, 30, 39);
            SetScrollBar(this.Handle, 1, 0);//隐藏下、右滚动条
            SetScrollBar(this.Handle, 0, 0);
        }

        #region 设置

        private List<INavigationBarOption> _menuItems;
        public List<INavigationBarOption> MenuItems
        {
            get { return _menuItems; }
            set
            {
                _menuItems = value;
                CreateMenuItemPanels(_menuItems, this, _parentHeight, false);
            }
        }
        /// <summary>
        /// 设置子项的字体
        /// </summary>
        [DefaultValue(typeof(Font), "微软雅黑,9pt")]
        [Description("子项的字体")]
        public Font ChildItemFont
        {
            get { return _childItemFont; }
            set { _childItemFont = value; }
        }
        private Font _childItemFont = new Font(new FontFamily("微软雅黑"), 9);

        /// <summary>
        /// 设置父项的字体
        /// </summary>
        [DefaultValue(typeof(Font), "微软雅黑,11pt,style=Bold")]
        [Description("父项的字体")]
        public Font ParentItemFont
        {
            get { return _parentItemFont; }
            set { _parentItemFont = value; }
        }
        private Font _parentItemFont = new Font(new FontFamily("微软雅黑"), 11 , FontStyle.Bold);

        /// <summary>
        /// 设置子项的四边边距
        /// </summary>
        [DefaultValue(typeof(Padding), "10,0,0,0")]
        [Description("设置子项的四边边距")]
        public Padding ChiluItemMargin
        {
            get { return _childItemMargin; }
            set { _childItemMargin = value; }
        }
        private Padding _childItemMargin = new Padding(10,0,0,0);

        /// <summary>
        /// 父节点的背景颜色
        /// </summary>
        [DefaultValue(typeof(Color), "29, 43, 54")]
        [Description("父节点的背景颜色")]
        public Color ParentBackColor
        {
            get { return _parentBackColor; }
            set { _parentBackColor = value; }
        }
        private Color _parentBackColor = Color.FromArgb(29, 43, 54);

        /// <summary>
        /// 子节点的背景颜色
        /// </summary>
        [DefaultValue(typeof(Color), "20, 30, 39")]
        [Description("子节点的背景颜色")]
        public Color ChildBackColor
        {
            get { return _childBackColor; }
            set { _childBackColor = value; }
        }
        private Color _childBackColor = Color.FromArgb(20, 30, 39);


        /// <summary>
        /// 父节点的高度
        /// </summary>
        [DefaultValue(typeof(int), "40")]
        [Description("父节点的高度")]
        public int ParentHeight
        {
            get { return _parentHeight; }
            set { _parentHeight = value; }
        }
        private int _parentHeight = 40;


        /// <summary>
        /// 子级节点的高度
        /// </summary>
        [DefaultValue(typeof(int), "30")]
        [Description("子节点的高度")]
        public int ChildHeight
        {
            get { return _childHeight; }
            set { _childHeight = value; }
        }
        private int _childHeight = 30;

        /// <summary>
        /// 鼠标悬浮节点的背景颜色
        /// </summary>
        [DefaultValue(typeof(Color), "39, 51, 69")]
        [Description("鼠标悬浮节点的背景颜色")]
        public Color MouseHoverBackColor
        {
            get { return _mouseHoverBackColor; }
            set { _mouseHoverBackColor = value; }
        }
        private Color _mouseHoverBackColor = Color.FromArgb(39, 51, 69);


        /// <summary>
        /// 子节点被选中字体颜色
        /// </summary>
        [DefaultValue(typeof(Color), "28, 134, 238")]
        [Description("子节点被选中字体颜色")]
        public Color ChildSelectedForeColor
        {
            get { return _childSelectedForeColor; }
            set { _childSelectedForeColor = value; }
        }
        private Color _childSelectedForeColor = Color.FromArgb(28, 134, 238);
        #endregion

        #region 设置滚动条显示
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowScrollBar(IntPtr hWnd, int bar, int show);

        private class SubWindow : NativeWindow
        {
            private int m_Horz = 0;
            private int m_Show = 0;

            public SubWindow(int p_Horz, int p_Show)
            {
                m_Horz = p_Horz;
                m_Show = p_Show;
            }
            protected override void WndProc(ref Message m_Msg)
            {
                ShowScrollBar(m_Msg.HWnd, m_Horz, m_Show);
                base.WndProc(ref m_Msg);
            }
        }
        /// <summary>
        /// 设置滚动条是否显示 
        /// </summary>
        /// <param name="p_ControlHandle">句柄</param>
        /// <param name="p_Horz">0横 1列 3全部</param>
        /// <param name="p_Show">0隐 1显</param>
        public static void SetScrollBar(IntPtr p_ControlHandle, int p_Horz, int p_Show)
        {
            SubWindow _SubWindow = new SubWindow(p_Horz, p_Show);
            _SubWindow.AssignHandle(p_ControlHandle);
        }

        #endregion

        /// <summary>
        /// 折叠所有节点
        /// </summary>
        public void CollapseAllItem()
        {
            foreach (FlowLayoutPanel item in parentList)//折叠所有父节点
            {
                item.Height = _parentHeight;
                foreach (Control itemControl in item.Controls)
                {
                    if (itemControl is Label)
                    {
                        itemControl.Invalidate();
                        break;
                    }
                }

                item.Tag = false;
            }
        }

        /// <summary>
        /// 初始化所有子节点
        /// </summary>
        public void InitializaAllChildItem()
        {
            foreach (FlowLayoutPanel item in childList)
            {
                Label l = item.Controls[0] as Label;
                l.Font = _childItemFont;
                l.ForeColor = this.ForeColor;
            }
        }

        /// <summary>
        /// 创建导航栏项
        /// </summary>
        /// <param name="options">项的设置信息</param>
        /// <param name="parent">项的父容器</param>
        /// <param name="itemH">项的高度</param>
        /// <param name="isChildNode">该项是否输入最终子项</param>
        private void CreateMenuItemPanels(List<INavigationBarOption> options, FlowLayoutPanel parent, int itemH, bool isChildNode)
        {
            if (options == null || options.Count <= 0) return;
            foreach (INavigationBarOption option in options)
            {
                FlowLayoutPanel panel = CreatePanel(option, itemH, parent , isChildNode);
                parent.Controls.Add(panel);
                if (option.ChildItemsOption != null && option.ChildItemsOption.Count > 0)
                {
                    CreateMenuItemPanels(option.ChildItemsOption, panel, _childHeight, true);
                }
            }

        }
        /// <summary>
        /// 创建项中的控件
        /// </summary>
        /// <param name="option">项的设置信息</param>
        /// <param name="itemH">项高度</param>
        /// <param name="pPanel">项的父容器</param>
        /// <returns></returns>
        private FlowLayoutPanel CreatePanel(INavigationBarOption option, int itemH, FlowLayoutPanel pPanel, bool isChildNode)
        {
            CNavigationBar panel = new CNavigationBar();
            Label itemL = new Label();
            panel.AutoScroll = false;
            panel.Height = itemH;
            
            if (isChildNode)
            {
                panel.BackColor = _childBackColor;
                panel.Margin = _childItemMargin;
                childList.Add(panel);
            }
            else
            {
                panel.BackColor = _parentBackColor;
                panel.Margin = new Padding(0);
                parentList.Add(panel);
            }
            panel.Width = pPanel.Width;
            panel.Tag = false;

            if (isChildNode)
            {
                itemL.Font = _childItemFont;
            }
            else
            {
                itemL.Font = _parentItemFont;
            }

            itemL.Text = option.Title;
            itemL.TextAlign = ContentAlignment.MiddleCenter;
            itemL.BackColor = Color.Empty;
            itemL.ForeColor = this.ForeColor;
            itemL.Height = panel.Height;
            itemL.Width = panel.Width;
            itemL.Tag = option;
            //添加鼠标进入与离开事件
            itemL.MouseEnter += ItemL_MouseEnter;
            itemL.MouseLeave += ItemL_MouseLeave;
            //如果该节点的子节点不为空，且子节点数大于0
            if (option.ChildItemsOption != null && option.ChildItemsOption.Count > 0)
            {
                itemL.Paint += ItemL_Paint_Expand;//添加展开与折叠图案绘制
                itemL.MouseClick += ItemL_Parent_MouseClick;//展开与折叠事件
                if (ParentItemMouseClick != null)
                {
                    itemL.MouseClick += ParentItemMouseClick;
                }
            }
            else
            {
                itemL.MouseClick += ItemL_Childe_MouseClick;//更改选中状态
                if (ChildItemMouseClick != null)
                {
                    itemL.MouseClick += ChildItemMouseClick;
                }
            }
            panel.Controls.Add(itemL);
            return panel;
        }
        /// <summary>
        /// 鼠标离开
        /// </summary>
        private void ItemL_MouseLeave(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.BackColor = Color.Empty;
        }
        /// <summary>
        /// 鼠标进入
        /// </summary>
        private void ItemL_MouseEnter(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.BackColor = _mouseHoverBackColor;
        }
        /// <summary>
        /// 绘制展开或折叠标志图案
        /// </summary>
        private void ItemL_Paint_Expand(object sender, PaintEventArgs e)
        {
            Label label = sender as Label;
            FlowLayoutPanel panel = label.Parent as FlowLayoutPanel;
            Pen p = new Pen(this.ForeColor, 2);
            int startP = 5;
            int drawLW = 8;
            int lHeight = label.Height;
            if ((bool)panel.Tag)//折叠
            {
                e.Graphics.DrawLine(p, startP, lHeight / 2, startP + drawLW, lHeight / 2);
            }
            else//展开
            {
                e.Graphics.DrawLine(p, startP, lHeight / 2, startP + drawLW, lHeight / 2);
                e.Graphics.DrawLine(p, startP + drawLW / 2, lHeight / 2 + drawLW / 2, 5 + drawLW / 2, lHeight / 2 - drawLW / 2);
            }

        }
        /// <summary>
        /// 子节点单点击事件，被点中状态
        /// </summary>
        private void ItemL_Childe_MouseClick(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            INavigationBarOption option = label.Tag as INavigationBarOption;
            if (!childList.Contains(label.Parent as FlowLayoutPanel)) return;//如果子项中不包含该控件，则退出
            InitializaAllChildItem();//初始化所有子节点
            label.Font = new Font(_childItemFont.FontFamily, _childItemFont.Size + 1, FontStyle.Bold);
            label.ForeColor = _childSelectedForeColor;
        }
        /// <summary>
        /// 父节点单点击事件，展开或折叠
        /// </summary>
        private void ItemL_Parent_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Label label = sender as Label;
                FlowLayoutPanel p = label.Parent as FlowLayoutPanel;

                if ((bool)p.Tag)//折叠
                {
                    p.Height = label.Height;
                    label.Invalidate();
                    p.Tag = false;
                }
                else//展开
                {
                    CollapseAllItem();
                    p.Height = (p.Controls.Count - 1) * _childHeight + (p.Controls.Count - 1) * (_childItemMargin.Bottom + _childItemMargin.Top)  + _parentHeight;
                    label.Invalidate();
                    p.Tag = true;
                }
            }

        }

    }
    /// <summary>
    /// 菜单项属性
    /// </summary>
    public interface INavigationBarOption
    {
        string Title { get; set; }
        object Tag { get; set; }
        List<INavigationBarOption> ChildItemsOption { get; set; }
    }
}

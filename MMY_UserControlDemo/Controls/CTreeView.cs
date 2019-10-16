/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：CTreeView.cs
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2019/10/14 11:01:09 
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
    public class CTreeView : FlowLayoutPanel
    {
        private const int CE_ICON_X = 5;//展开，折叠图标X轴
        private const int TAG_ICON_X = 17;//标志图标X轴
        private const int TITLE_X = CE_ICON_X + TAG_ICON_X + 2;//标志图标X轴


        private Font _itemFont = null;
        

        private List<FlowLayoutPanel> _childList = new List<FlowLayoutPanel>();//子类集合
        private List<FlowLayoutPanel> _parentList = new List<FlowLayoutPanel>();//父类集合

        private Padding _parentPd = new Padding(0, 1, 0, 1);
        private Padding _childPd = new Padding(10, 2, 0, 2);

        public Action<object> ParentItemMouseClick = null;//父项鼠标事件
        public Action<object> ChildItemMouseClick = null;//子项鼠标事件
       


        public CTreeView()
        {
            this.AutoScroll = true;
            this.Width = 170;
            this.BackColor = Color.FromArgb(20, 30, 39);
            this.Tag = "main";//初始化底层容器的标志，容器内的Panel 的 Tag用于存储折叠展开状态
            SetScrollBar(this.Handle, 1, 0);//隐藏下、右滚动条
            SetScrollBar(this.Handle, 0, 0);
        }

        protected override void OnCreateControl()
        {
            _itemFont = this.Font;
            base.OnCreateControl();
        }

        /// <summary>
        /// 折叠所有节点
        /// </summary>
        public void CollapseAllItem()
        {
            foreach (FlowLayoutPanel item in _parentList)//折叠所有父节点
            {
                item.Height = _levelOneHeight;
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
        /// 折叠同级下的节点
        /// </summary>
        public void CollapseSameLVItem(FlowLayoutPanel p)
        {
            foreach (FlowLayoutPanel item in _parentList)//折叠所有父节点
            {
                if (p.Parent.Controls.Contains(item))
                {
                    item.Height = _levelOneHeight;
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
        }
        /// <summary>
        /// 初始化所有子节点
        /// </summary>
        public void InitializaAllChildItem()
        {
            foreach (FlowLayoutPanel item in _childList)
            {
                Label l = item.Controls[0] as Label;
                Label titleL = new Label();
                foreach (Control labelControl in l.Controls)
                {
                    if (labelControl is Label)
                    {
                        titleL = labelControl as Label;
                    }
                }
                titleL.Font = _itemFont;
                titleL.ForeColor = this.ForeColor;
            }
        }
        /// <summary>
        /// 初始化所有父节点
        /// </summary>
        public void InitializaAllParentItem()
        {
            foreach (FlowLayoutPanel item in _parentList)
            {
                foreach (Control itemControl in item.Controls)
                {
                    if (itemControl is Label)
                    {
                        Label l = item.Controls[0] as Label;
                        Label titleL = new Label();
                        foreach (Control labelControl in l.Controls)
                        {
                            if (labelControl is Label)
                            {
                                titleL = labelControl as Label;
                            }
                        }
                        titleL.Font = _itemFont;
                        titleL.ForeColor = this.ForeColor;
                    }
                    
                }
               
            }
        }

        #region 设置

        private List<IItemOption> _items;
        public List<IItemOption> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                CreateTreeViewItems(_items, this, _levelOneHeight, false);
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
        /// 是否显示图标
        /// </summary>
        [DefaultValue(typeof(bool), "false")]
        [Description("是否显示图标")]
        public bool IsShowIcon
        {
            get { return _isShowIcon; }
            set { _isShowIcon = value; }
        }
        private bool _isShowIcon = false;

        /// <summary>
        /// 父项是否可变更选中状态
        /// </summary>
        [DefaultValue(typeof(bool), "true")]
        [Description("是否显示图标")]
        public bool IsParentChangeState
        {
            get { return _isParentChangeState; }
            set { _isParentChangeState = value; }
        }
        private bool _isParentChangeState = true;

        /// <summary>
        /// 一级节点的高度
        /// </summary>
        [DefaultValue(typeof(int), "22")]
        [Description("一级节点的高度")]
        public int LevelOneHeight
        {
            get { return _levelOneHeight; }
            set { _levelOneHeight = value; }
        }
        private int _levelOneHeight = 22;


        /// <summary>
        /// 二级节点的高度
        /// </summary>
        [DefaultValue(typeof(int), "20")]
        [Description("二级节点的高度")]
        public int LevelTwoHeight
        {
            get { return _levelTwoHeight; }
            set { _levelTwoHeight = value; }
        }
        private int _levelTwoHeight = 20;

        /// <summary>
        /// 一级节点的背景颜色
        /// </summary>
        [DefaultValue(typeof(Color), "29, 43, 54")]
        [Description("一级节点的背景颜色")]
        public Color LevelOneBackColor
        {
            get { return _levelOneBackColor; }
            set { _levelOneBackColor = value; }
        }
        private Color _levelOneBackColor = Color.FromArgb(29, 43, 54);

        /// <summary>
        /// 二级节点的背景颜色
        /// </summary>
        [DefaultValue(typeof(Color), "29, 43, 54")]
        [Description("二级节点的背景颜色")]
        public Color LevelTwoBackColor
        {
            get { return _levelTwoBackColor; }
            set { _levelTwoBackColor = value; }
        }
        private Color _levelTwoBackColor = Color.FromArgb(29, 43, 54);

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
        /// 创建导航栏项
        /// </summary>
        /// <param name="options">项的设置信息</param>
        /// <param name="parent">项的父容器</param>
        /// <param name="itemH">项的高度</param>
        /// <param name="isChildNode">该项是否输入最终子项</param>
        private void CreateTreeViewItems(List<IItemOption> options, FlowLayoutPanel parent, int itemH, bool isChildNode)
        {
            if (options == null || options.Count <= 0) return;
            foreach (IItemOption option in options)
            {
                FlowLayoutPanel panel = CreateItem(option, itemH, parent, isChildNode);
                parent.Controls.Add(panel);
                if (option.ChildItemOption != null && option.ChildItemOption.Count > 0)
                {
                    CreateTreeViewItems(option.ChildItemOption, panel, _levelTwoHeight, true);
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
        private FlowLayoutPanel CreateItem(IItemOption option, int itemH, FlowLayoutPanel pPanel, bool isChildNode)
        {
            CTreeView panel = new CTreeView();//底层容器
            panel.AutoScroll = false;
            panel.Height = itemH;
            panel.Width = pPanel.Width;
            if (isChildNode)
            {
                panel.Margin = _childPd;
                panel.BackColor = _levelTwoBackColor;
                _childList.Add(panel);
            }
            else
            {
                panel.Margin = _parentPd;
                panel.BackColor = _levelOneBackColor;
                _parentList.Add(panel);
            }
            panel.Tag = false;

            Label itemLPanel = new Label();//底层label
            itemLPanel.Margin = new Padding(0);
            itemLPanel.Height = panel.Height;
            itemLPanel.Width = panel.Width;
            itemLPanel.Margin = new Padding(0);
            itemLPanel.Tag = option;

            PictureBox pb = new PictureBox();//图标

            pb.SizeMode = PictureBoxSizeMode.CenterImage;
            pb.Width = pb.Height = itemLPanel.Height - 2;
            pb.Image = option.ImageList.Images[option.ImageIndex];
            pb.Location = new Point(TAG_ICON_X, itemLPanel.Height / 2 - pb.Height / 2);



            Label itemL = new Label();//标题
            int pbW = 0;
            if (_isShowIcon)
            {
                pbW = pb.Width;
            }
            itemL.Location = new Point(TITLE_X + pbW, itemLPanel.Height / 2 - itemL.Height / 2);
            itemL.Font = _itemFont;
            itemL.Text = option.Title;
            itemL.TextAlign = ContentAlignment.MiddleLeft;
            itemL.ForeColor = this.ForeColor;

            //添加鼠标进入与离开事件
            itemLPanel.MouseEnter += ItemLPanel_MouseEnter;
            itemLPanel.MouseLeave += ItemLPanel_MouseLeave;
            pb.MouseEnter += Base_MouseEnter;
            pb.MouseLeave += Base_MouseLeave;
            itemL.MouseEnter += Base_MouseEnter;
            itemL.MouseLeave += Base_MouseLeave;

            //如果该节点的子节点不为空，且子节点数大于0
            if (option.ChildItemOption != null && option.ChildItemOption.Count > 0)
            {
                itemLPanel.Paint += ItemLPanel_Paint_Expand;//添加展开与折叠图案绘制
                itemLPanel.MouseClick += ItemLPanel_Parent_MouseClick;//展开与折叠事件
                itemL.MouseClick += Base_Parent_MouseClick;
                pb.MouseClick += Base_Parent_MouseClick;
            }
            else
            {
                itemLPanel.MouseClick += ItemLPanel_Child_MouseClick;//更改选中状态
                itemL.MouseClick += Base_Child_MouseClick;
                pb.MouseClick += Base_Child_MouseClick;
            }

            itemLPanel.Controls.Add(itemL);//添加到底层Label
            if (_isShowIcon)
            {
                itemLPanel.Controls.Add(pb);
            }
            panel.Controls.Add(itemLPanel);
            return panel;
        }
        /// <summary>
        /// 动态修改Panel的高度
        /// </summary>
        /// <param name="p">点击的Panel</param>
        private void ChangePanelHeight(FlowLayoutPanel p )
        {
            FlowLayoutPanel parentP = p.Parent as FlowLayoutPanel;//获取p的父节点
            if (!parentP.Tag.ToString().Equals("main"))
            {
                int PL = _levelOneHeight;
                int PH = p.Height;
                int PPH = _levelOneHeight + (_parentPd.Bottom + _parentPd.Top);//单个P中Panel高度
                int PCount = parentP.Controls.Count - 2;

                parentP.Height = PPH * PCount + PL + PH + 1;
            }
        }


        #region 底层Label事件
        /// <summary>
        /// 鼠标离开
        /// </summary>
        private void ItemLPanel_MouseLeave(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.BackColor = Color.Empty;
        }

        /// <summary>
        /// 鼠标进入
        /// </summary>
        private void ItemLPanel_MouseEnter(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.BackColor = _mouseHoverBackColor;
        }

        /// <summary>
        /// 绘制展开或折叠标志图案
        /// </summary>
        private void ItemLPanel_Paint_Expand(object sender, PaintEventArgs e)
        {
            Label label = sender as Label;
            FlowLayoutPanel panel = label.Parent as FlowLayoutPanel;
            Pen p = new Pen(this.ForeColor, 2);
            int startP = CE_ICON_X;
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
        /// 子级选项点击事件，改变节点被点中状态
        /// </summary>
        private void ItemLPanel_Child_MouseClick(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            Label titleL = new Label();
            foreach (Control labelControl in label.Controls)
            {
                if (labelControl is Label)
                {
                    titleL = labelControl as Label;
                }
            }
            //if (!_childList.Contains(label.Parent as FlowLayoutPanel)) return;//如果子项中不包含该控件，则退出
            InitializaAllChildItem();//初始化所有子节点
            InitializaAllParentItem();//初始化所有父节点
            titleL.Font = new Font(_itemFont.FontFamily, _itemFont.Size, FontStyle.Bold);
            titleL.ForeColor = Color.FromArgb(28, 134, 238);

            ChildItemMouseClick(label.Tag);
        }

        /// <summary>
        /// 一级选项点击事件，展开或折叠
        /// </summary>
        private void ItemLPanel_Parent_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Label label = sender as Label;
                FlowLayoutPanel p = label.Parent as FlowLayoutPanel;
                if (IsParentChangeState)
                {
                    ItemLPanel_Child_MouseClick(label, e);
                }
                if ((bool)p.Tag)//折叠
                {
                    p.Height = label.Height;
                    ChangePanelHeight(p);
                    label.Invalidate();
                    p.Tag = false;
                }
                else//展开
                {
                    CollapseSameLVItem(p);
                    p.Height = (p.Controls.Count - 1) * _levelTwoHeight + (p.Controls.Count - 1) * (_childPd.Bottom + _childPd.Top) + _levelOneHeight;
                    ChangePanelHeight(p);
                    label.Invalidate();
                    p.Tag = true;
                }

                if (ParentItemMouseClick == null)
                {
                    ChildItemMouseClick(label.Tag);
                }
                else
                {
                    ParentItemMouseClick(label.Tag);
                }
            }
        }
        #endregion

        #region 通用继承底层Label事件

        private void Base_MouseLeave(object sender, EventArgs e)
        {
            Control con = sender as Control;
            ItemLPanel_MouseLeave(con.Parent, e);
        }
        private void Base_MouseEnter(object sender, EventArgs e)
        {
            Control con = sender as Control;
            ItemLPanel_MouseEnter(con.Parent, e);
        }
        private void Base_Parent_MouseClick(object sender, MouseEventArgs e)
        {
            Control con = sender as Control;
            ItemLPanel_Parent_MouseClick(con.Parent, e);
        }
        private void Base_Child_MouseClick(object sender, MouseEventArgs e)
        {
            Control con = sender as Control;
            ItemLPanel_Child_MouseClick(con.Parent, e);
        }

        #endregion

    }

    public interface IItemOption
    {
        /// <summary>
        /// 标志
        /// </summary>
        object Tag { get; set; }
        /// <summary>
        /// 显示
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 显示图片集合
        /// </summary>
        ImageList ImageList { get; set; }
        /// <summary>
        /// 图片下标
        /// </summary>
        int ImageIndex { get; set; }
        /// <summary>
        /// 子项
        /// </summary>
        List<IItemOption> ChildItemOption { get; set; }
    }

}

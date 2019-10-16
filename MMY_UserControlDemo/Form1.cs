using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MMY_UserControlDemo.ControlOption;
using MMY_UserControlDemo.Controls;

namespace MMY_UserControlDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateNavi();
            CreateTreeView();
        }
        private void CreateTreeView()
        {
            List<IItemOption> item = new List<IItemOption>();

            for (int i = 0; i < 5; i++)
            {
                List<IItemOption> citems = new List<IItemOption>();
                IItemOption parentPanel = new TreeViewItemOption()
                {
                    Title = $"选项{i}",
                    Tag = i,
                    ImageList = imageList1,
                    ImageIndex = 0,
                };
                for (int j = 0; j < 5; j++)
                {
                    List<IItemOption> ccitems = new List<IItemOption>();
                    IItemOption cItem = new TreeViewItemOption()
                    {
                        Title = $"子选项{j}",
                        Tag = j,
                        ImageList = imageList1,
                        ImageIndex = 1,
                    };
                    for (int k = 0; k < 5; k++)
                    {
                        IItemOption ccItem = new TreeViewItemOption()
                        {
                            Title = $"子子选项{k}",
                            Tag = k,
                            ImageList = imageList1,
                            ImageIndex = 1,
                        };
                        
                        ccitems.Add(ccItem);
                    }

                    cItem.ChildItemOption = ccitems;
                    citems.Add(cItem);
                }

                parentPanel.ChildItemOption = citems;
                item.Add(parentPanel);
            }
            cTreeView1.ChildItemMouseClick = TreeView_ChildItemMouseClick;
            cTreeView1.Items = item;
        }
        private void TreeView_ChildItemMouseClick(object sender)
        {
            TreeViewItemOption option = sender as TreeViewItemOption;
            Console.WriteLine($"子节点，（我的Tag{option.Title}）");

        }
        private void CreateNavi()
        {

            List<INavigationBarOption> item = new List<INavigationBarOption>();
            for (int i = 0; i < 5; i++)
            {
                List<INavigationBarOption> citems = new List<INavigationBarOption>();
                INavigationBarOption parentPanel = new CNavigationBarOption
                {
                    Title = $"选项{i}",
                    Tag = i,
                };
                for (int j = 0; j < 5; j++)
                {
                    INavigationBarOption cItem = new CNavigationBarOption
                    {
                        Title = $"子选项{j}",
                        Tag = j,
                    };
                    citems.Add(cItem);
                }
                parentPanel.ChildItemsOption = citems;
                item.Add(parentPanel);
            }
            cNavigationBar1.ChildItemMouseClick += CNavigationBar1_ChildItemMouseClick;
            cNavigationBar1.MenuItems = item;
        }

        private void CNavigationBar1_ChildItemMouseClick(object sender, MouseEventArgs e)
        {
            Label l = sender as Label;
            if (e.Button == MouseButtons.Left)
            {
                Console.WriteLine($"子节点，（我的Tag{l.Tag}）");
            }
        }
    }
}

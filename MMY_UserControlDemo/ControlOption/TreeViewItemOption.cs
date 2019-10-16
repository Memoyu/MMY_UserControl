/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：TreeViewItemOption.cs
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2019/10/14 16:15:56 
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System.Collections.Generic;
using System.Windows.Forms;
using MMY_UserControlDemo.Controls;

namespace MMY_UserControlDemo.ControlOption
{
    public class TreeViewItemOption : IItemOption
    {
        public object Tag { get; set; }
        public string Title { get; set; }
        public ImageList ImageList { get; set; }
        public int ImageIndex { get; set; }
        public List<IItemOption> ChildItemOption { get; set; }

    }
}

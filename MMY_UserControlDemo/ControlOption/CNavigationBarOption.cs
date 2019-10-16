/**************************************************************************  
*   =================================
*   CLR版本  ：4.0.30319.42000
*   文件名称 ：CNavigationBarOption.cs
*   =================================
*   创 建 者 ：Memoyu
*   创建日期 ：2019/9/30 10:52:07 
*   功能描述 ：
*   =================================
*   修 改 者 ：
*   修改日期 ：
*   修改内容 ：
*   ================================= 
***************************************************************************/

using System.Collections.Generic;
using MMY_UserControlDemo.Controls;

namespace MMY_UserControlDemo.ControlOption
{
    public class CNavigationBarOption:INavigationBarOption
    {
        public string Title { get; set; }
        public object Tag { get; set; }
        public List<INavigationBarOption> ChildItemsOption { get; set; }
    }
}

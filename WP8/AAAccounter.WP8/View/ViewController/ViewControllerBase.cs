/* *
* ClassName: ViewControllerBase
* Description: 
*
* Author: 李靖
* Date: 5/26/2015 9:58:40 PM
* */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AAAccounter.View
{
    internal class ViewControllerBase<T> where T: Page
    {
        MainViewController _mainController = null;
        T _page = null;

        public ViewControllerBase(T page)
        {
            _page = page;
        }

        public virtual void Initialize(MainViewController vmain)
        {
            _mainController = vmain;
        }
    }
}

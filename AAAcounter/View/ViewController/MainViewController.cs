/* *
* ClassName: MainViewController
* Description: 
*
* Author: 李靖
* Date: 5/26/2015 9:58:21 PM
* */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAAcounter.Model;
using AAAcounter.Controller;

namespace AAAcounter.View
{

    public class MainViewController
    {
        private MainPage _page;
        private MainController _controller;

        public MainViewController(MainPage page)
        {
            _controller = new MainController();
            _page = page;
        }

        public async Task<bool> Login(string user, string pwd)
        {
            return await _controller.Login(user, pwd);
        }

        public async Task<bool> Register(string user, string pwd)
        {
            return await _controller.Register(user, pwd);
        }

        public Task<bool> CheckLogin()
        {
            return _controller.CheckLogin();
        }

        public void Logout()
        {
            _controller.Logout();
        }
    }
}

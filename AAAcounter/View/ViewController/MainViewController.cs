﻿/* *
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
using AAAccounter.Model;
using AAAccounter.Controller;

namespace AAAccounter.View
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

        public async Task<List<ConsumerModel>> GetConsumerList()
        {
            return await _controller.GetConsumerList();
        }

        public async Task<List<Consumption>> GetConsumptionList(ConsumerModel consumer = null)
        {
            return await _controller.GetConsumptionList(consumer);
        }

        public ConsumerModel Consumer { get { return _controller.Consummer; } }

        public async Task AddConsumption(Consumption con)
        {
            await _controller.AddConsumption(con);
        }

        public async Task SaveConsumptionItems(List<ConsumptionItemList> citems)
        {
            await _controller.SaveConsumptionItems(citems);
        }

        public void NavigateTo(Type type)
        {
            _page.NavigateTo(type, this);
        }

    }
}

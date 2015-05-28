/* *
* ClassName: Controller
* Description: 
*
* Author: 李靖
* Date: 5/26/2015 11:14:27 PM
* */

using AAAcounter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AAAcounter.Controller
{
    public class MainController
    {
        private DBModel _dbModel = new DBModel();
        private ConsumerModel _consumer = null;
        private bool _initialized = false;

        public MainController()
        {
            string path = @"aaacount.db";
            _dbModel.OpenAsync(path);
        }

        #region 获取数据
        public ConsumerModel Consummer
        {
            get {
                return _consumer;
            }
        }

        public async Task<List<ConsumerModel>> GetConsumerList()
        {
            Dictionary<string, object> condition = new Dictionary<string, object>();
            return await _dbModel.GetListAsync<ConsumerModel>(condition);
        }

        public async Task<List<ConsumerDetail>> GetConsumerDetailList()
        {
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add("ConsumerId", _consumer.Id);
            return await _dbModel.GetListAsync<ConsumerDetail>(condition);
        }

        public async Task<List<Consumption>> GetConsumptionList(ConsumerModel consumer)
        {
            Dictionary<string, object> condition = new Dictionary<string, object>();
            if (consumer != null)
            {
                condition.Add("ConsumerId", consumer.Id);
            }
            var consumerDetailList = await _dbModel.GetListAsync<ConsumerDetail>(condition);
            condition.Clear();
            var consumptionList = await _dbModel.GetListAsync<Consumption>(condition);
            var result = (from a in consumerDetailList
                          from b in consumptionList
                          where a.ConsumptionId == b.Id && a.ConsumerId == consumer.Id
                          select b).ToList();
            return await _dbModel.GetListAsync<Consumption>(condition);
        }

        public async Task<List<ConsumptionItemList>> ConsumptionDetaiItemlList(int consuptionId)
        {
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add("ConsumerId", _consumer.Id);
            return await _dbModel.GetListAsync<ConsumptionItemList>(condition);
        }

        //public async Task<List<ConsumerDetailItemList>> ConsumptionDetailConsumerList(int consuptionId)
        //{
        //    Dictionary<string, object> condition = new Dictionary<string, object>();
        //    return await _dbModel.GetListAsync<ConsumerDetailItemList>(condition);
        //}

        #endregion

        #region 登录退出
        public async Task<bool> Login(string user, string pwd)
        {
            pwd = EncryptPwd(pwd);
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add("Name", user);
            condition.Add("Password", pwd);
            _consumer = (await _dbModel.GetListAsync<ConsumerModel>(condition)).FirstOrDefault();
            if (_consumer == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> Register(string user, string pwd)
        {
            pwd = EncryptPwd(pwd);
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add("Name", user);
            condition.Add("Password", pwd);
            _consumer = (await _dbModel.GetListAsync<ConsumerModel>(condition)).FirstOrDefault();
            if (_consumer != null)
            {
                if (_consumer.Password == EncryptPwd(DefaultStrings.DEFAULT_PWD))
                {

                }
                return false;
            }
            else
            {
                _consumer = new ConsumerModel
                {
                    Balance = 0,
                    Name = user,
                    Password = pwd,
                    Saving = 0,
                    Spending = 0,
                };
                await _dbModel.InsertOneAsync<ConsumerModel>(_consumer);
                return true;
            }
        }

        public async Task<bool> CheckLogin()
        {
            await InitializeDatabase();
            Dictionary<string, object> condition = new Dictionary<string, object>();
            _consumer = (await _dbModel.GetListAsync<ConsumerModel>(condition)).FirstOrDefault();
            return _consumer != null;
        }

        public void Logout()
        {
            _consumer = null;
        }
        #endregion

        #region 初始化表
        private async Task InitializeDatabase()
        {
            if (_initialized) return;
            _initialized = true;
            await _dbModel.CreateTablesAsync<ConsumerModel, ConsumerDetail, Consumption, ConsumptionItemList>();
            //await _dbModel.CreateTableAsync<ConsumerDetailItemList>();
        }
        #endregion

        #region 算法
        string EncryptPwd(string pwd)
        {
            return MD5.Md5(MD5.Md5(pwd) + "L0gin");
        }
        #endregion
    }
}

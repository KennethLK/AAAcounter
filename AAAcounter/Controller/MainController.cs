/* *
* ClassName: Controller
* Description: 
*
* Author: 李靖
* Date: 5/26/2015 11:14:27 PM
* */

using AAAccounter.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace AAAccounter.Controller
{
    public class MainController
    {
        private DBModel _dbModel = new DBModel();
        private ConsumerModel _consumer = null;
        private bool _initialized = false;

        public MainController()
        {
            string path = DefaultConfig.DATABASE_NAME;
            _dbModel.OpenAsync(path);
        }

        #region 获取数据
        public ConsumerModel Consummer
        {
            get
            {
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

        public async Task<List<ConsumerModel>> InitialConsumers()
        {
            List<ConsumerModel> consumers = new List<ConsumerModel>();
            consumers.Add(new ConsumerModel
            {
                Balance = 0,
                CreateDate = DateTime.Now.GetMili1970(),
                Name = "李靖",
                Password = EncryptPwd(DefaultConfig.DEFAULT_PWD),
                Saving = 100,
                Spending = 0
            });
            consumers.Add(new ConsumerModel
            {
                Balance = 0,
                CreateDate = DateTime.Now.GetMili1970(),
                Name = "龚彦铭",
                Password = EncryptPwd(DefaultConfig.DEFAULT_PWD),
                Saving = 100,
                Spending = 0
            });
            consumers.Add(new ConsumerModel
            {
                Balance = 0,
                CreateDate = DateTime.Now.GetMili1970(),
                Name = "敬亮",
                Password = EncryptPwd(DefaultConfig.DEFAULT_PWD),
                Saving = 100,
                Spending = 0
            });
            consumers.Add(new ConsumerModel
            {
                Balance = 0,
                CreateDate = DateTime.Now.GetMili1970(),
                Name = "田龙",
                Password = EncryptPwd(DefaultConfig.DEFAULT_PWD),
                Saving = 100,
                Spending = 0
            });
            consumers.Add(new ConsumerModel
            {
                Balance = 0,
                CreateDate = DateTime.Now.GetMili1970(),
                Name = "刘明悦",
                Password = EncryptPwd(DefaultConfig.DEFAULT_PWD),
                Saving = 100,
                Spending = 0
            });
            consumers.Add(new ConsumerModel
            {
                Balance = 0,
                CreateDate = DateTime.Now.GetMili1970(),
                Name = "王池",
                Password = EncryptPwd(DefaultConfig.DEFAULT_PWD),
                Saving = 100,
                Spending = 0
            });
            consumers.Add(new ConsumerModel
            {
                Balance = 0,
                CreateDate = DateTime.Now.GetMili1970(),
                Name = "郝鹏",
                Password = EncryptPwd(DefaultConfig.DEFAULT_PWD),
                Saving = 100,
                Spending = 0
            });
            await _dbModel.InsertAllAsync<ConsumerModel>(consumers);
            return consumers;
        }

        public async Task<List<Consumption>> GetConsumptionList(ConsumerModel consumer)
        {
            if (consumer == null)
                consumer = _consumer;

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

        public async Task SaveConsumptionItems(List<ConsumptionItemList> citems)
        {
            await _dbModel.InsertAllAsync<ConsumptionItemList>(citems);
        }

        public async Task AddConsumption(Consumption con)
        {
            await _dbModel.InsertOneAsync<Consumption>(con);
        }

        public async Task<List<ConsumptionItemList>> ConsumptionDetaiItemlList(int consuptionId)
        {
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add("ConsumerId", _consumer.Id);
            return await _dbModel.GetListAsync<ConsumptionItemList>(condition);
        }

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
                await SaveLogon(_consumer);
                return true;
            }
        }

        public async Task<bool> Register(string user, string pwd)
        {
            pwd = EncryptPwd(pwd);
            Dictionary<string, object> condition = new Dictionary<string, object>();
            condition.Add(nameof(_consumer.Name), user);
            condition.Add(nameof(_consumer.Password), pwd);
            _consumer = (await _dbModel.GetListAsync<ConsumerModel>(condition)).FirstOrDefault();
            if (_consumer != null)
            {
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
            bool result = false;
            await InitializeDatabase();
            result = await LoadLogon();
            var consumers = await GetConsumerList();
            if (consumers.Count == 0)
            {
                consumers = await InitialConsumers();
            }
            return result;
        }

        private async Task SaveLogon(ConsumerModel consumer)
        {
            StorageFile configFile = await ApplicationData.Current.LocalFolder.GetFileAsync(DefaultConfig.CONFIG_FILE_NAME);
            await FileIO.WriteTextAsync(configFile, string.Format("{0}\r\n{1}" , consumer.Name, consumer.Password));
            configFile = null;
        }

        private async Task<bool> LoadLogon()
        {
            bool result = false;
            string configFileName = DefaultConfig.CONFIG_FILE_NAME;
            StorageFile configFile = null;
            string logonUser = "";
            string logonPwd = "";
            var finder = await ApplicationData.Current.LocalFolder.TryGetItemAsync(configFileName);
            if (finder != null)
            {
                configFile = finder as StorageFile;
                string content = await FileIO.ReadTextAsync(configFile, DefaultConfig.ENCODING);
                if (!string.IsNullOrEmpty(content))
                {
                    StringReader sr = new StringReader(content);
                    logonUser = sr.ReadLine();
                    if (!string.IsNullOrEmpty(logonUser))
                        logonPwd = sr.ReadLine();

                    configFile = null;

                    Dictionary<string, object> condition = new Dictionary<string, object>();
                    condition.Add("Name", logonUser);
                    condition.Add("Password", logonPwd);
                    _consumer = (await _dbModel.GetListAsync<ConsumerModel>(condition)).FirstOrDefault();
                    result = _consumer != null;
                }
            }
            else
            {
                configFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(configFileName);
            }
            finder = null;
            return result;
        }

        public async void Logout()
        {
            _consumer.Name = "";
            _consumer.Password = "";
            await SaveLogon(_consumer);
            _consumer = null;
        }
        #endregion

        #region 初始化表
        private async Task InitializeDatabase()
        {
            if (_initialized) return;
            _initialized = true;
            await _dbModel.CreateTablesAsync<ConsumerModel, ConsumerDetail, Consumption, ConsumptionItemList>();
        }
        #endregion

        #region 算法
        public string EncryptPwd(string pwd)
        {
            return MD5.Md5(MD5.Md5(pwd) + "L0gin");
        }
        #endregion
    }
}

using AAAcounter.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AAAcounter.Model
{
    /// <summary>
    /// Sqlite数据库实例
    /// </summary>
    public class DBModel : IDBModel
    {
        public DBModel()
        {
        }

        /// <summary>
        /// 异步数据连接
        /// </summary>
        private SQLite.SQLiteAsyncConnection _connAsync = null;

        /// <summary>
        /// /// 异步初始化指定数据库
        /// </summary> 
        public bool OpenAsync(string path)
        {
            try
            {
                var dbPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\" + path;
                _connAsync = new SQLite.SQLiteAsyncConnection(dbPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void GetDbPath()
        {
            
        }

        /// <summary>
        /// 创建数据表
        /// </summary> 
        public async Task<bool> CreateTableAsync<T>() where T : ITable, new()
        {
            try
            {
                if (_connAsync == null) return false;
                var result = await _connAsync.CreateTableAsync<T>();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建数据表
        /// </summary> 
        public async Task<bool> CreateTablesAsync<T, T1>()
            where T : ITable, new()
            where T1 : ITable, new()
        {
            try
            {
                if (_connAsync == null) return false;
                var result = await _connAsync.CreateTablesAsync<T, T1>();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建数据表
        /// </summary> 
        public async Task<bool> CreateTablesAsync<T, T1, T2, T3>()
            where T : ITable, new()
            where T1 : ITable, new()
            where T2 : ITable, new()
             where T3 : ITable, new()
        {
            try
            {
                if (_connAsync == null) return false;
                var result = await _connAsync.CreateTablesAsync<T, T1, T2, T3>();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 保存一条数据,必须写T的类型，不能减省
        /// </summary> 
        public async Task<bool> InsertOneAsync<T>(T item) where T : ITable
        {
            try
            {
                if (_connAsync == null) return false;
                var result = await _connAsync.InsertAsync(item, typeof(T));
                return result == 1;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 保存多条数据,必须写T的类型，不能减省
        /// </summary> 
        public async Task<bool> InsertAllAsync<T>(IEnumerable items) where T : ITable
        {
            try
            {
                if (_connAsync == null) return false;
                var result = await _connAsync.InsertAllAsync(items, typeof(T));
                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 保存多条数据,必须写T的类型，不能减省
        /// 可以保留记录的ID， 用于数据复制
        /// </summary> 
        public async Task<bool> IdentityInsertAllAsync<T>(IEnumerable items) where T : ITable
        {
            try
            {
                if (_connAsync == null) return false;
                var result = await _connAsync.IdentityInsertAllAsync(items, typeof(T));
                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据,必须写T的类型，不能减省
        /// </summary> 
        public async Task<bool> DeleteOneAsync<T>(T item) where T : ITable
        {
            try
            {
                if (_connAsync == null) return false;
                var result = await _connAsync.DeleteAsync(item, typeof(T));
                return result == 1;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除多条数据,必须写T的类型，不能减省
        /// </summary> 
        public async Task<bool> DeleteAllAsync<T>(IEnumerable items) where T : ITable
        {
            try
            {
                if (_connAsync == null) return false;
                var result = await _connAsync.DeleteAllAsync(items, typeof(T));
                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据,必须写T的类型，不能减省
        /// </summary> 
        public async Task<bool> UpdateOneAsync<T>(T item) where T : ITable
        {
            try
            {
                if (_connAsync == null) return false;
                var result = await _connAsync.UpdateAsync(item, typeof(T));
                return result == 1;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 更新多条数据,必须写T的类型，不能减省
        /// </summary> 
        public async Task<bool> UpdateAllAsync<T>(IEnumerable items) where T : ITable
        {
            try
            {
                if (_connAsync == null) return false;
                var result = await _connAsync.UpdateAllAsync(items, typeof(T));
                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 取得表中包含的数据数量,必须写T的类型，不能减省
        /// </summary> 
        public async Task<int> GetCountAsync<T>() where T : ITable
        {
            try
            {
                if (_connAsync == null) return 0;
                string sql = "select count(Id) from " + typeof(T).ToString();
                var result = await _connAsync.ExecuteScalarAsync<int>(sql);
                return result;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 取得表中包含的数据数量
        /// </summary> 
        public async Task<int> GetCountAsync<T>(Dictionary<string, object> condition)
        {
            try
            {
                if (_connAsync == null) return 0;
                string sql = GetCountStatement(typeof(T), condition);
                System.Diagnostics.Debug.WriteLine("--------------------  " + sql);
                var result = await _connAsync.ExecuteScalarAsync<int>(sql);
                return result;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary> 
        public async Task<List<T>> GetListAsync<T>(Dictionary<string, object> condition) where T : ITable, new()
        {
            try
            {
                if (_connAsync == null) return null;
                string sql = GetSelectStatement(typeof(T), condition);
                return await _connAsync.QueryAsync<T>(sql);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary> 
        public async Task<List<T>> GetListAsync<T>(Dictionary<string, object> condition, int from, int to) where T : ITable, new()
        {
            try
            {
                if (_connAsync == null) return null;
                string sql = GetSelectStatement(typeof(T), condition, from, to);
                return await _connAsync.QueryAsync<T>(sql);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 自定义条件查询Sql，用于一些不常用的联合查询
        /// </summary> 
        public async Task<List<T>> GetListAsync<T>(string sql) where T : ITable, new()
        {
            try
            {
                if (_connAsync == null) return null;
                return await _connAsync.QueryAsync<T>(sql);
            }
            catch
            {
                return null;
            }
        }


        #region 私有方法
        /// <summary>
        /// 生成获取数据行数的Sql语句
        /// </summary> 
        private string GetCountStatement(Type type, Dictionary<string, object> condition)
        {
            string sql = "select count(Id) from " + type.Name.ToString();
            if (condition != null && condition.Count > 0)
            {
                sql += " where ";
            }
            foreach (var item in condition)
            {
                sql += item.Key + "='" + item.Value + "' and ";
            }

            if (condition.Count > 0)
                sql = sql.Remove(sql.Length - 4);

            return sql;
        }

        /// <summary>
        /// 生成获取列表的Sql语句
        /// </summary> 
        private string GetSelectStatement(Type type, Dictionary<string, object> condition)
        {
            string sql = "select * from " + type.Name.ToString();
            if (condition != null && condition.Count > 0)
            {
                sql += " where ";
            }
            else
            {
                return sql;
            }

            foreach (var item in condition)
            {
                sql += item.Key + "='" + item.Value + "' and ";
            }
            if (condition.Count > 0)
                sql = sql.Remove(sql.Length - 4);

            return sql;
        }

        /// <summary>
        /// 生成获取数据指定行数的Sql语句
        /// </summary> 
        private string GetSelectStatement(Type type, Dictionary<string, object> condition, int from, int to)
        {
            string sql = GetSelectStatement(type, condition);
            int count = to - from;
            sql += " limit " + count + " offset " + from;
            return sql;
        }
        #endregion
    }
}

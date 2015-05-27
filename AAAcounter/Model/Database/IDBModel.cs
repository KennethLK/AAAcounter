using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAAcounter.Model
{
    /// <summary>
    /// 数据库处理基类
    /// </summary>
    public interface IDBModel
    {
        /// <summary>
        /// /// 异步初始化指定数据库
        /// </summary> 
        bool OpenAsync(string path);

        /// <summary>
        /// 创建数据表
        /// </summary> 
        Task<bool> CreateTableAsync<T>() where T : ITable, new();

        /// <summary>
        /// 创建数据表
        /// </summary>
        Task<bool> CreateTablesAsync<T, T1>()
            where T : ITable, new()
            where T1 : ITable, new();

        /// <summary>
        /// 创建数据表
        /// </summary>
        Task<bool> CreateTablesAsync<T, T1, T2, T3>()
              where T : ITable, new()
              where T1 : ITable, new()
              where T2 : ITable, new()
               where T3 : ITable, new();

        /// <summary>
        /// 保存一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> InsertOneAsync<T>(T item) where T : ITable;

        /// <summary>
        /// 添加多条数据
        /// </summary> 
        Task<bool> InsertAllAsync<T>(IEnumerable items) where T : ITable;

        /// <summary>
        /// 添加多条数据, 可以保留记录的ID， 用于数据复制
        /// </summary> 
        Task<bool> IdentityInsertAllAsync<T>(IEnumerable items) where T : ITable;

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> DeleteOneAsync<T>(T item) where T : ITable;


        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<bool> DeleteAllAsync<T>(IEnumerable items) where T : ITable;

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> UpdateOneAsync<T>(T item) where T : ITable;

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> UpdateAllAsync<T>(IEnumerable items) where T : ITable;

        /// <summary>
        /// 取得表中包含的数据数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<int> GetCountAsync<T>() where T : ITable;

        /// <summary>
        /// 取得表中包含的数据数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">过滤条件</param>
        /// <returns></returns>
        Task<int> GetCountAsync<T>(Dictionary<string, object> condition);

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">查询条件， 比如仅查询本地照片，仅查询云照片 </param>
        /// <returns></returns>
        Task<List<T>> GetListAsync<T>(Dictionary<string, object> condition)
            where T : ITable, new();

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">查询条件， 比如仅查询本地照片，仅查询云照片 </param>
        /// <param name="from"> 起始位置 </param>
        /// <param name="to"> 结束位置 </param>
        /// <returns></returns>
        Task<List<T>> GetListAsync<T>(Dictionary<string, object> condition, int from, int to)
            where T : ITable, new();

        /// <summary>
        /// 自定义条件查询Sql，用于一些不常用的联合查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<List<T>> GetListAsync<T>(string sql)
            where T : ITable, new();

        /* 过期方法
        #region 同步方法
        /// <summary>
        /// 打开指定数据库
        /// </summary>
        /// <param name="path">数据库文件地址</param>
        /// <returns></returns>
        bool Open(string path);

        /// <summary>
        /// 打开默认数据库
        /// </summary>
        /// <returns></returns>
        bool Open();

        /// <summary>
        /// 关闭数据库
        /// </summary>
        void Close();

        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool CreateTable<T>();

        /// <summary>
        /// 保存一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        bool InsertOne<T>(T item) where T : ITable;

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        bool UpdateOne<T>(T item) where T : ITable;

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        bool DeleteOne<T>(T item) where T : ITable;

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">查询条件， 比如仅查询本地照片，仅查询云照片 </param>
        /// <returns></returns>
        List<T> GetList<T>(Dictionary<string, object> condition)
            where T : ITable, new();

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">查询条件， 比如仅查询本地照片，仅查询云照片 </param>
        /// <param name="from"> 起始位置 </param>
        /// <param name="to"> 结束位置 </param>
        /// <returns></returns>
        List<T> GetList<T>(Dictionary<string, object> condition, int from, int to)
            where T : ITable, new();

        /// <summary>
        /// 自定义条件查询Sql，用于一些不常用的联合查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<T> GetList<T>(string sql) where T : new();

        /// <summary>
        /// 取得表中包含的数据数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        int GetCount<T>();

        /// <summary>
        /// 取得表中包含的数据数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">过滤条件</param>
        /// <returns></returns>
        int GetCount<T>(Dictionary<string, object> condition);

        #endregion
         * */
    }
}

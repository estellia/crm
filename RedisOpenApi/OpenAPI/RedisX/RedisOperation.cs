using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedisOpenAPIClient.MethodExtensions.StringExtensions;
using RedisOpenAPIClient.Common;
using RedisOpenAPIClient.MethodExtensions.NumberExtensions;
using RedisOpenAPIClient.MethodExtensions.ObjectExtensions;
using RedisOpenAPIClient.MethodExtensions.EnumExtensions;

namespace OpenAPI.RedisX
{
    public class RedisOperation
    {
        #region Class
        /// <summary>
        /// Redis / DB
        /// </summary>
        private IDatabase _DB = default(IDatabase);

        /// <summary>
        /// 默认过期时间
        /// </summary>
        private TimeSpan _defaultValidTimeSpan = default(TimeSpan);

        /// <summary>
        /// 构造函数  /  private
        /// </summary>
        private RedisOperation()
        { }

        /// <summary>
        /// 构造函数  /  public
        /// </summary>
        /// <param name="dbIndex">RedisDB,默认dbIndex =0</param>
        public RedisOperation(RedisDBEnum dbIndex)
        {
            _DB = Redis.DB(dbIndex.ToInt());
            _defaultValidTimeSpan = TimeSpan.FromHours(Redis.DefaultValidTimeSpan);
        }

        /// <summary>
        /// 获取字符串类型的keyvalue
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetKeyString(string key)
        {
            //
            if (!_IsExist(key))
            {
                return string.Empty;
            }

            ////
            //if (!_IsRightType(RedisDataEnum.String, key))
            //{
            //    return new T();
            //}

            //
            return _DB.StringGet((RedisKey)key, CommandFlags.HighPriority).ToString();
        }

        /// <summary>
        /// 设置字符串类型的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Insert(string key, string value)
        {
            if (key.IsNullStr() || value.IsNullStr())
            {
                return false;
            }

            //
            return _DB.StringSet((RedisKey)key, (RedisValue)value, _defaultValidTimeSpan, When.Always, CommandFlags.None);
        }

        /// <summary>
        /// 构造函数  /  public
        /// </summary>
        /// <param name="dbIndex">RedisDB,默认dbIndex =0</param>
        /// <param name="ValidTimeSpan">默认过期时间  单位：分钟</param>
        public RedisOperation(RedisDBEnum dbIndex, double ValidTimeSpan)
        {
            _DB = Redis.DB(dbIndex.ToInt());
            _defaultValidTimeSpan = TimeSpan.FromMinutes(ValidTimeSpan);
        }
        #endregion

        #region Common
        /// <summary>
        /// 是否存在 Key
        /// </summary>
        public bool IsExist(string key)
        {
            return _DB.KeyExists((RedisKey)key, CommandFlags.HighPriority);
        }

        /// <summary>
        ///  删除 Key
        /// </summary>
        public bool Delete(params string[] keys)
        {
            //
            if (!_IsExist(keys))
            {
                return true;
            }

            //
            if (keys.Length == 1)
            {
                return _DB.KeyDelete((RedisKey)keys[0], CommandFlags.None);
            }

            //
            if (keys.Length > 1)
            {
                return _DB.KeyDelete(keys.Select(it => (RedisKey)it).ToArray(), CommandFlags.None) > 0;
            }

            //
            return true;
        }

        /// <summary>
        /// 设置过期 Key
        /// </summary>
        /// <param name="validTimeSpan">有效时间段</param>
        public bool Expire(TimeSpan validTimeSpan, params string[] keys)
        {
            //
            if (validTimeSpan == null || validTimeSpan.Milliseconds == 0)
            {
                return false;
            }

            //
            if (!_IsExist(keys))
            {
                return false;
            }

            //
            if (keys.Length == 1)
            {
                return _DB.KeyExpire((RedisKey)keys[0], validTimeSpan, CommandFlags.None);
            }

            //
            if (keys.Length > 1)
            {
                var flag = false;
                foreach (var item in keys)
                {
                    var itemFlag = _DB.KeyExpire((RedisKey)item, validTimeSpan, CommandFlags.None);
                    if (itemFlag == true)
                    {
                        flag = true;
                    }
                }
                return flag;
            }

            //
            return false;
        }

        /// <summary>
        /// 重命名 Key
        /// </summary>
        public bool Rename(string oldKey, string newKey)
        {
            //
            if (!_IsExist(oldKey))
            {
                return false;
            }

            //
            if (_IsExist(newKey))
            {
                return false;
            }

            //
            return _DB.KeyRename(oldKey, newKey, When.Exists, CommandFlags.HighPriority);
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType(string key)
        {
            //
            if (!_IsExist(key))
            {
                return "NoData";
            }

            //
            var type = _DB.KeyType((RedisKey)key, CommandFlags.HighPriority);
            switch (type)
            {
                case RedisType.None:
                    return "NoData";
                case RedisType.String:
                    return "String";
                case RedisType.List:
                    return "List";
                case RedisType.Set:
                    return "Set";
                case RedisType.SortedSet:
                    return "SortedSet";
                case RedisType.Hash:
                    return "HashSet";
                case RedisType.Unknown:
                    return "Unknown";
            }

            //
            return "Unknown";
        }
        #endregion

        #region String
        /// <summary>
        /// 新增 string
        /// </summary>
        public bool InsertString<T>(string key, T value)
            where T : class,new()
        {
            //
            //if (_IsExist(key))
            //{
            //    return true;
            //}

            ////
            //if(!_IsRightType(redisd))

            //
            var val = value.JsonSerialize();

            //
            if (key.IsNullStr() || val.IsNullStr())
            {
                return false;
            }

            //
            return _DB.StringSet((RedisKey)key, (RedisValue)val, _defaultValidTimeSpan, When.Always, CommandFlags.None);
        }

        /// <summary>
        /// 新增 Batch[string]
        /// </summary>
        public bool InsertStringBatch<T>(Dictionary<string, T> keyValuePairs)
            where T : class,new()
        {
            //
            if (keyValuePairs == null)
            {
                return false;
            }

            //
            var flag = false;
            flag = _DB.StringSet(keyValuePairs.Select(it => new KeyValuePair<RedisKey, RedisValue>((RedisKey)(it.Key), (RedisValue)(it.Value.JsonSerialize()))).ToArray(), When.Always, CommandFlags.None);
            if (flag == true)
            {
                foreach (var item in keyValuePairs.Keys)
                {
                    _DB.KeyExpire((RedisKey)item, _defaultValidTimeSpan, CommandFlags.None);
                }
            }
            return flag;
        }

        /// <summary>
        /// 删除 string
        /// </summary>
        public bool DeleteString(string key)
        {
            ////
            //if (!_IsExist(key))
            //{
            //    return true;
            //}

            ////
            //if (!_IsRightType(RedisDataEnum.String, key))
            //{
            //    return false;
            //}

            //
            return _DB.KeyDelete((RedisKey)key, CommandFlags.None);
        }

        /// <summary>
        /// 删除 Batch[string]
        /// </summary>
        public bool DeleteStringBatch(params string[] keys)
        {
            //
            if (!_IsExist(keys))
            {
                return true;
            }

            //
            if (!_IsRightType(RedisDataEnum.String, keys))
            {
                return false;
            }

            //
            return _DB.KeyDelete(keys.Select(it => (RedisKey)it).ToArray(), CommandFlags.None) > 0;
        }

        /// <summary>
        /// 修改 string
        /// </summary>
        public bool UpdateString<T>(string key, T newValue)
            where T : class,new()
        {
            //
            if (key.IsNullStr() || newValue == null)
            {
                return false;
            }

            //
            if (_IsExist(key) && !_IsRightType(RedisDataEnum.String, key))
            {
                return false;
            }

            //
            _DB.KeyDelete((RedisKey)key, CommandFlags.None);
            return _DB.StringSet((RedisKey)key, (RedisValue)(newValue.JsonSerialize()), _defaultValidTimeSpan, When.NotExists, CommandFlags.None);
        }

        /// <summary>
        /// 修改 Batch[string]
        /// </summary>
        public bool UpdateStringBatch<T>(Dictionary<string, T> keyValuePairs)
            where T : class,new()
        {
            //
            if (keyValuePairs == null)
            {
                return false;
            }

            //
            if (_IsExist(keyValuePairs.Keys.ToArray()) && !_IsRightType(RedisDataEnum.String, keyValuePairs.Keys.ToArray()))
            {
                return false;
            }

            //
            _DB.KeyDelete(keyValuePairs.Keys.Select(it => (RedisKey)it).ToArray(), CommandFlags.None);
            var flag = false;
            flag = _DB.StringSet(keyValuePairs.Select(it => new KeyValuePair<RedisKey, RedisValue>((RedisKey)(it.Key), (RedisValue)(it.Value.JsonSerialize()))).ToArray(), When.Always, CommandFlags.None);
            if (flag == true)
            {
                foreach (var item in keyValuePairs.Keys)
                {
                    _DB.KeyExpire((RedisKey)item, _defaultValidTimeSpan, CommandFlags.None);
                }
            }
            return flag;
        }

        /// <summary>
        /// 查询 sting
        /// </summary>
        public T SelectString<T>(string key)
            where T : class ,new()
        {
            //
            if (!_IsExist(key))
            {
                return new T();
            }

            ////
            //if (!_IsRightType(RedisDataEnum.String, key))
            //{
            //    return new T();
            //}

            //
            return ((string)(_DB.StringGet((RedisKey)key, CommandFlags.HighPriority))).JsonDeserialize<T>();
        }

        /// <summary>
        /// 查询 Batch[string]
        /// </summary>
        public List<T> SelectStringBatch<T>(params string[] keys)
        {
            //
            if (!_IsExist(keys))
            {
                return new List<T>();
            }

            //
            if (!_IsRightType(RedisDataEnum.String, keys))
            {
                return new List<T>();
            }

            //
            return _DB.StringGet(keys.Select(it => (RedisKey)it).ToArray(), CommandFlags.HighPriority).Select(it => ((string)it).JsonDeserialize<T>()).ToList();
        }
        #endregion

        #region List
        /// <summary>
        /// 获取 List 长度
        /// </summary>
        public long GetListLength(string key)
        {
            //
            if (!_IsExist(key))
            {
                return 0;
            }

            //
            if (!_IsRightType(RedisDataEnum.List, key))
            {
                return 0;
            }

            //
            return _DB.ListLength(key, CommandFlags.HighPriority);
        }

        /// <summary>
        /// 新增 list[Queue[item]]
        /// </summary>
        public bool InsertListQueue<T>(string key, T value)
            where T : class,new()
        {
            //
            if (key.IsNullStr() || value == null)
            {
                return false;
            }

            ////
            //if (_IsExist(key) && !_IsRightType(RedisDataEnum.List, key))
            //{
            //    return false;
            //}

            //
            return _DB.ListLeftPush((RedisKey)key, (RedisValue)(value.JsonSerialize()), When.Always, CommandFlags.None) > 0;
            //if (flag == true)
            //{
            //    _DB.KeyExpire(key, _defaultValidTimeSpan, CommandFlags.None);
            //}
            //return flag;
        }

        /// <summary>
        /// 新增 list[Queue[item]]   批量
        /// </summary>
        public bool InsertListQueueBatch<T>(string key, List<T> values)
            where T : class,new()
        {
            //
            if (key.IsNullStr() || values == null || values.Count <= 0)
            {
                return false;
            }

            ////
            //if (_IsExist(key) && !_IsRightType(RedisDataEnum.List, key))
            //{
            //    return false;
            //}

            var vals = new List<RedisValue>();
            values.ForEach(it =>
            {
                vals.Add(it.JsonSerialize());
            });

            //
            //var flag = _DB.ListLeftPush((RedisKey)key, (RedisValue)(value.JsonSerialize()), When.Always, CommandFlags.None) > 0;
            return _DB.ListLeftPush((RedisKey)key, vals.ToArray(), CommandFlags.None) > 0;
            //if (flag == true)
            //{
            //    _DB.KeyExpire(key, _defaultValidTimeSpan, CommandFlags.None);
            //}
            //return flag;
        }

        /// <summary>
        /// 查询 list[Queue[item]]
        /// </summary>
        public T SelectListQueue<T>(string key)
            where T : class,new()
        {
            //
            if (!_IsExist(key))
            {
                return new T();
            }

            ////
            //if (!_IsRightType(RedisDataEnum.List, key))
            //{
            //    return new T();
            //}

            //
            if (_DB.ListLength(key, CommandFlags.HighPriority) <= 0)
            {
                return new T();
            }

            //
            return ((string)(_DB.ListRightPop(key, CommandFlags.None))).JsonDeserialize<T>();
        }

        /// <summary>
        /// 新增 list[Stack[item]]
        /// </summary>
        public bool InsertListStack<T>(string key, T value)
            where T : class,new()
        {
            //
            if (_IsExist(key) && !_IsRightType(RedisDataEnum.List, key))
            {
                return false;
            }

            //
            var val = value.JsonSerialize();

            //
            var flag = _DB.ListRightPush(key, val, When.Always, CommandFlags.None) > 0;
            if (flag == true)
            {
                _DB.KeyExpire(key, _defaultValidTimeSpan, CommandFlags.None);
            }
            return flag;
        }

        /// <summary>
        /// 查询 list[Stack[item]]
        /// </summary>
        public T SelectListStack<T>(string key)
            where T : class,new()
        {
            //
            if (!_IsExist(key))
            {
                return new T();
            }

            //
            if (!_IsRightType(RedisDataEnum.List, key))
            {
                return new T();
            }

            //
            if (_DB.ListLength(key, CommandFlags.HighPriority) <= 0)
            {
                return new T();
            }

            //
            return ((string)(_DB.ListRightPop(key, CommandFlags.None))).JsonDeserialize<T>();
        }
        #endregion

        #region Set
        /// <summary>
        /// 新增 set[item]
        /// </summary>
        public bool InsertSet<T>(string key, T value)
            where T : class,new()
        {
            //
            if (value == null)
            {
                return false;
            }

            //
            var val = value.JsonSerialize();

            //
            if (_IsExist(RedisDataEnum.Set, key, val))
            {
                return true;
            }

            //
            var flag = _DB.SetAdd((RedisKey)key, (RedisValue)val, CommandFlags.None);
            if (flag == true)
            {
                _DB.KeyExpire(key, _defaultValidTimeSpan, CommandFlags.None);
            }
            return flag;
        }

		public bool InsertSet(string key,string value)
			//where T : class, new() 
			{
			
			if (value == null) {
				return false;
			}

			//
			var val = value;

			//
			if (_IsExist(RedisDataEnum.Set, key, val)) {
				return true;
			}

			//
			var flag = _DB.SetAdd((RedisKey)key, (RedisValue)val, CommandFlags.None);
			if (flag == true) {
				_DB.KeyExpire(key, _defaultValidTimeSpan, CommandFlags.None);
			}
			return flag;
		}
		public bool ExistInSet(string key,string value)
			{
			bool result = _DB.SetContains((RedisKey)key, value, CommandFlags.None);
		
			return result;
		}
		public void DeleteSet(string key) {
			long count=_DB.SetLength((RedisKey)key,  CommandFlags.None);
			while(count>0) {
				_DB.SetPop((RedisKey)key, CommandFlags.None);
			}
		}
		/// <summary>
		/// 新增 set[Batch[item]]
		/// </summary>
		public bool InsertSetBatch<T>(string key, params T[] values)
            where T : class,new()
        {
            //
            if (values == null || values.Length == 0)
            {
                return false;
            }

            //
            if (!_IsRightType(RedisDataEnum.Set, key))
            {
                return false;
            }

            //
            var flag = false;
            flag = _DB.SetAdd((RedisKey)key, values.Select(it => (RedisValue)(it.JsonSerialize())).ToArray(), CommandFlags.None) > 0;
            if (flag == true)
            {
                _DB.KeyExpire(key, _defaultValidTimeSpan, CommandFlags.None);
            }
            return flag;
        }

        /// <summary>
        /// 删除 set[item]
        /// </summary>
        public bool DeleteSet<T>(string key, T value)
            where T : class,new()
        {
            //
            if (key.IsNullStr())
            {
                return true;
            }

            //
            if (!_IsRightType(RedisDataEnum.Set, key))
            {
                return false;
            }

            //
            var val = value.JsonSerialize();

            //
            if (!_IsExist(RedisDataEnum.Set, key, val))
            {
                return true;
            }

            //
            return _DB.SetRemove(key, val, CommandFlags.None);
        }

        /// <summary>
        /// 删除 set[Batch[item]]
        /// </summary>
        public bool DeleteSetBatch<T>(string key, params T[] values)
            where T : class,new()
        {
            //
            if (key.IsNullStr() || values == null || values.Length == 0)
            {
                return true;
            }

            //
            if (!_IsRightType(RedisDataEnum.Set, key))
            {
                return false;
            }

            //
            var vals = values.Select(it => it.JsonSerialize()).ToArray();

            //
            if (!_IsExist(RedisDataEnum.Set, key, vals))
            {
                return true;
            }

            //
            return _DB.SetRemove((RedisKey)key, vals.Select(it => (RedisValue)it).ToArray(), CommandFlags.None) > 0;
        }

        /// <summary>
        /// 修改 set[item]
        /// </summary>
        public bool UpdateSet<T>(string key, T value)
            where T : class,new()
        {
            //
            if (key.IsNullStr() || value == null)
            {
                return false;
            }

            //
            if (!_IsRightType(RedisDataEnum.Set, key))
            {
                return false;
            }

            //
            var val = value.JsonSerialize();

            //
            if (_IsExist(RedisDataEnum.Set, key, val))
            {
                _DB.SetRemove(key, val, CommandFlags.None);
            }

            //
            var flag = _DB.SetAdd((RedisKey)key, (RedisValue)val, CommandFlags.None);
            if (flag == true)
            {
                _DB.KeyExpire(key, _defaultValidTimeSpan, CommandFlags.None);
            }
            return flag;
        }

        /// <summary>
        /// 修改 set[Batch[item]]
        /// </summary>
        public bool UpdateSetBatch<T>(string key, params T[] values)
            where T : class,new()
        {
            //
            if (key.IsNullStr() || values == null || values.Length == 0)
            {
                return false;
            }

            //
            if (!_IsRightType(RedisDataEnum.Set, key))
            {
                return false;
            }

            //
            var vals = values.Select(it => it.JsonSerialize()).ToArray();

            //
            if (_IsExist(RedisDataEnum.Set, key, vals))
            {
                _DB.SetRemove((RedisKey)key, vals.Select(it => (RedisValue)it).ToArray(), CommandFlags.None);
            }

            //
            var flag = _DB.SetAdd(key, vals.Select(it => (RedisValue)it).ToArray(), CommandFlags.None) > 0;
            if (flag == true)
            {
                _DB.KeyExpire(key, _defaultValidTimeSpan, CommandFlags.None);
            }
            return flag;
        }

        /// <summary>
        /// 查询 set[Batch[item]]
        /// </summary>
        public List<T> SelectSetBatch<T>(string key)
            where T : class,new()
        {
            //
            if (!_IsExist(key))
            {
                return new List<T>();
            }

            //
            if (!_IsRightType(RedisDataEnum.Set, key))
            {
                return new List<T>();
            }

            //
            if (_DB.SetLength(key, CommandFlags.HighPriority) <= 0)
            {
                return new List<T>();
            }

            //
            return _DB.SetMembers(key, CommandFlags.HighPriority).Select(it => ((string)it).JsonDeserialize<T>()).ToList();
        }

        /// <summary>
        /// 查询 set[Paged[item]]
        /// </summary>
        /// <param name="filterStr">适配字符串</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="skipPage">跳过页数</param>
        public List<T> SelectSetPaged<T>(string key, int pageSize, int skipPage, string filterStr)
        {
            //
            if (!_IsExist(key))
            {
                return new List<T>();
            }

            //
            if (!_IsRightType(RedisDataEnum.Set, key))
            {
                return new List<T>();
            }

            //
            if (_DB.SetLength(key, CommandFlags.HighPriority) <= 0)
            {
                return new List<T>();
            }

            //var partten = filterStr.IsNullStr()?

            //
            return _DB.SetScan(key, (RedisValue)filterStr, pageSize, 0, skipPage, CommandFlags.None).Select(it => ((string)it).JsonDeserialize<T>()).ToList();
        }
        #endregion

        #region SortedSet
        /// <summary>
        /// 获取 sortedLenth 长度
        /// </summary>
        public long GetSortedLength(string key)
        {
            //
            if (!_IsExist(key))
            {
                return 0;
            }

            //
            if (!_IsRightType(RedisDataEnum.SortedSet, key))
            {
                return 0;
            }

            //
            return _DB.SortedSetLength(key, double.MinValue, double.MaxValue, Exclude.None, CommandFlags.HighPriority);
        }

        /// <summary>
        /// 新增 sortedSet[item]
        /// </summary>
        /// <param name="sortFlag">SortedSet,排序标识</param>
        public bool InsertSortedSet<T>(string key, T value, uint sortFlag)
            where T : class,new()
        {
            //
            if (key.IsNullStr() || value == null)
            {
                return false;
            }

            //
            if (_IsExist(key) && !_IsRightType(RedisDataEnum.SortedSet, key))
            {
                return false;
            }

            //
            var val = value.JsonSerialize();

            //
            var flag = _DB.SortedSetAdd(key, val, sortFlag, CommandFlags.None);
            if (flag == true)
            {
                _DB.KeyExpire(key, _defaultValidTimeSpan, CommandFlags.None);
            }
            return flag;
        }

        /// <summary>
        /// 删除 sortedSet[item]
        /// </summary>
        /// <param name="value">要删除的SortedSet[item]</param>
        public bool DeleteSortedSet<T>(string key, T value)
            where T : class,new()
        {
            //
            if (!_IsExist(key))
            {
                return true;
            }

            //
            if (!_IsRightType(RedisDataEnum.SortedSet, key))
            {
                return false;
            }

            //
            var val = value.JsonSerialize();

            //
            return _DB.SortedSetRemove(key, val, CommandFlags.None);
        }

        /// <summary>
        /// 修改 sortedSet[item]
        /// </summary>
        public bool UpdateSortedSet<T>(string key, T newValue, uint newSortFlag)
            where T : class,new()
        {
            //
            var val = newValue.JsonSerialize();

            //
            if (!_IsRightType(RedisDataEnum.SortedSet, key))
            {
                return false;
            }

            //
            if (_IsExist(key))
            {
                _DB.SortedSetRemove(key, val, CommandFlags.None);
            }

            //
            var flag = _DB.SortedSetAdd(key, val, newSortFlag, CommandFlags.None);
            if (flag == true)
            {
                _DB.KeyExpire(key, _defaultValidTimeSpan, CommandFlags.None);
            }
            return flag;
        }

        /// <summary>
        /// 查询 sortedSet[sortFlag[item]]
        /// </summary>
        public List<T> SelectSortedSet<T>(string key, uint sortFlagStart = uint.MinValue, uint sortFlagEnd = uint.MaxValue)
            where T : class,new()
        {
            //
            if (!_IsExist(key))
            {
                return new List<T>();
            }

            //
            if (!_IsRightType(RedisDataEnum.SortedSet, key))
            {
                return new List<T>();
            }

            //
            if (_DB.SortedSetLength(key, double.MinValue, double.MaxValue, Exclude.None, CommandFlags.HighPriority) <= 0)
            {
                return new List<T>();
            }

            //
            return _DB.SortedSetRangeByScore(key, sortFlagStart, sortFlagEnd, Exclude.None, Order.Ascending, 0, -1, CommandFlags.None).Select(it => ((string)it).JsonDeserialize<T>()).ToList();
        }

        /// <summary>
        /// 查询 sortedSet[Paged[item]]
        /// </summary>
        public List<SortedSetEntry> SelectSortedSetPaged(string key, int pageSize, int skipPage, string filterStr)
        {
            //
            if (!_IsExist(key))
            {
                return new List<SortedSetEntry>();
            }

            //
            if (!_IsRightType(RedisDataEnum.SortedSet, key))
            {
                return new List<SortedSetEntry>();
            }

            //
            if (_DB.SortedSetLength(key, double.MinValue, double.MaxValue, Exclude.None, CommandFlags.HighPriority) <= 0)
            {
                return new List<SortedSetEntry>();
            }

            //
            return _DB.SortedSetScan(key, (RedisValue)filterStr, pageSize, 0, skipPage, CommandFlags.None).ToList();
        }
        #endregion

        #region HashSet
        /// <summary>
        /// 是否存在  HashID
        /// </summary>
        public bool IsExistHashID(string key, string hashID)
        {
            //
            if (!_IsExist(key))
            {
                return false;
            }

            //
            return _DB.HashExists(key, hashID, CommandFlags.HighPriority);
        }

        /// <summary>
        /// 新增 hashSet[item]
        /// </summary>
        public bool InsertHashSet<T>(string key, string hashID, T value)
            where T : class,new()
        {
            //
            if (_DB.HashExists(key, hashID, CommandFlags.HighPriority))
            {
                return true;
            }

            //
            var val = value.JsonSerialize();

            //
            var flag = _DB.HashSet(key, hashID, val, When.NotExists, CommandFlags.None);
            if (flag == true)
            {
                _DB.KeyExpire(key, _defaultValidTimeSpan, CommandFlags.None);
            }
            return flag;
        }

        /// <summary>
        /// 删除 hashSet[item]
        /// </summary>
        public bool DeleteHashSet(string key, string hashID)
        {
            //
            if (!_DB.HashExists(key, hashID, CommandFlags.HighPriority))
            {
                return true;
            }

            //
            if (!_IsRightType(RedisDataEnum.HashSet, key))
            {
                return false;
            }

            //
            return _DB.HashDelete(key, hashID, CommandFlags.None);
        }

        /// <summary>
        /// 修改 hashSet[item]
        /// </summary>
        public bool UpdateHashSet<T>(string key, string hashID, T newValue)
            where T : class,new()
        {
            //
            if (!_IsRightType(RedisDataEnum.HashSet, key))
            {
                return false;
            }

            //
            if (_DB.HashExists(key, hashID, CommandFlags.HighPriority))
            {
                _DB.HashDelete(key, hashID, CommandFlags.None);
            }

            //
            var val = newValue.JsonSerialize();

            //
            var flag = _DB.HashSet(key, hashID, val, When.Always, CommandFlags.None);
            if (flag == true)
            {
                _DB.KeyExpire(key, _defaultValidTimeSpan, CommandFlags.None);
            }
            return flag;
        }

        /// <summary>
        /// 查询 hashSet[item]
        /// </summary>
        public T SelectHashSet<T>(string key, string hashID)
            where T : class,new()
        {
            //
            if (!_IsExist(key))
            {
                return new T();
            }

            //
            if (!_IsRightType(RedisDataEnum.HashSet, key))
            {
                return new T();
            }

            //
            return ((string)(_DB.HashGet(key, hashID, CommandFlags.None))).JsonDeserialize<T>();
        }

        /// <summary>
        /// 查询 hashSet[ALL[item]]
        /// </summary>
        public List<HashEntry> SelectHashSetALL(string key)
        {
            //
            if (!_IsExist(key))
            {
                return new List<HashEntry>();
            }

            //
            if (!_IsRightType(RedisDataEnum.HashSet, key))
            {
                return new List<HashEntry>();
            }

            //
            return _DB.HashGetAll(key, CommandFlags.None).ToList();
        }
        #endregion

        #region Private
        /// <summary>
        /// 是否存在 Keys
        /// </summary>
        private bool _IsExist(params string[] keys)
        {
            //
            if (keys == null || keys.Length == 0)
            {
                return false;
            }

            //
            if (keys.Length == 1)
            {
                return IsExist(keys[0]);
            }

            //
            if (keys.Length > 1)
            {
                var flag = false;
                foreach (var item in keys)
                {
                    if (IsExist(item))
                    {
                        flag = true;
                        break;
                    }
                }
                return flag;
            }

            //
            return false;
        }

        /// <summary>
        /// 是否存在 Key[items]
        /// </summary>
        private bool _IsExist(RedisDataEnum dataType, string key, params string[] values)
        {
            //
            if (!_IsExist(key))
            {
                return false;
            }

            //
            if (values == null || values.Length == 0)
            {
                return false;
            }

            //
            switch (dataType)
            {
                case RedisDataEnum.Set:
                    if (values.Length == 1)
                    {
                        return _DB.SetContains((RedisKey)key, (RedisValue)(values[0]), CommandFlags.HighPriority);
                    }
                    if (values.Length > 1)
                    {
                        var flag = false;
                        foreach (var item in values)
                        {
                            var itemFlag = _DB.SetContains((RedisKey)key, (RedisValue)item, CommandFlags.HighPriority);
                            if (itemFlag == true)
                            {
                                flag = true;
                            }
                        }
                        return flag;
                    }
                    break;
                case RedisDataEnum.SortedSet:
                    //if(values.Length==1)
                    //{
                    //    return _DB.SortedSetRemove()
                    //}
                    //if(values.Length>1)
                    //{

                    //}
                    break;
            }

            //
            return false;
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        private RedisDataEnum _DataType(string key)
        {
            //
            if (!_IsExist(key))
            {
                return RedisDataEnum.None;
            }

            //
            var type = _DB.KeyType((RedisKey)key, CommandFlags.HighPriority);
            switch (type)
            {
                case RedisType.None:
                    return RedisDataEnum.None;
                case RedisType.String:
                    return RedisDataEnum.String;
                case RedisType.List:
                    return RedisDataEnum.List;
                case RedisType.Set:
                    return RedisDataEnum.Set;
                case RedisType.SortedSet:
                    return RedisDataEnum.SortedSet;
                case RedisType.Hash:
                    return RedisDataEnum.HashSet;
                case RedisType.Unknown:
                    return RedisDataEnum.None;
            }

            //
            return RedisDataEnum.None;
        }

        /// <summary>
        /// 数据类型是否正确
        /// </summary>
        private bool _IsRightType(RedisDataEnum dataType, params string[] keys)
        {
            //
            if (dataType == RedisDataEnum.None)
            {
                return false;
            }

            //
            if (keys == null || keys.Length == 0)
            {
                return false;
            }

            //
            if (keys.Length == 1)
            {
                return _DataType(keys[0]) == dataType;
            }

            //
            if (keys.Length > 1)
            {
                var flag = true;
                foreach (var item in keys)
                {
                    var typeFlag = _DataType(item) == dataType;
                    if (typeFlag == false)
                    {
                        flag = false;
                        break;
                    }
                }
                return flag;
            }

            //
            return false;
        }
        #endregion
    }
}
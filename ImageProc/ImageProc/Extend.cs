

namespace System
{
    public static class Extend
    {

        /// <summary>
        /// 转换为INT类型  失败为0
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="def">转换失败的默认值</param>
        /// <returns></returns>
        public static int ToInt(this object _this, int def = 0)
        {
            return _this.ToNumber<int>(def);
        }

        /// <summary>
        /// 转换为数值类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_this"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static T ToNumber<T>(this object _this, T def = default(T)) where T : struct
        {
            if (_this == null || _this == DBNull.Value)
            {
                return def;
            }
            T result = def;
            if (_this is String)
            {
                try
                {
                    var _ret = Convert.ChangeType(_this, typeof(T));
                    return (T)_ret;
                }
                catch (Exception ex)
                {
                    return def;
                }
            }
            else
            {
                if (_this is Boolean)
                {
                    return (T)((bool)_this ? (object)1 : (object)0);
                }
                return (T)_this;
            }
        }

        public static string TrimStart(this string _this, string trimString)
        {
            string result = _this;
            while (result.StartsWith(trimString))
            {
                result = result.Substring(trimString.Length);
            }

            return result;
        }
    }
}
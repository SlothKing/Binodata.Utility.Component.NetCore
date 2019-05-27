using Binodata.Utility.Component.NetCore.Const;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binodata.Utility.Component.NetCore.AdapterModel
{
    public class ApiResult<T>
    {
        /// <summary>
        /// 結果集 結果內容值
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 成功失敗
        /// </summary>
        private bool Valid { get; set; }

        /// <summary>
        /// 回傳訊息(成功/錯誤)
        /// </summary>
        public string ResultMessage { get; set; }

        /// <summary>
        /// 回傳代碼(成功/錯誤)
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// 任務編號
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// 加密檢核訊息，回傳給Client，只有Client 本身能解密
        /// </summary>
        public string CypherCheckValue { get; set; }


        public static ApiResult<T> OK(T value)
        {
            return new ApiResult<T>
            {
                Valid = true,
                Value = value,
                ResultCode = 0
            };
        }

        public static ApiResult<T> Error(string errorMessage, T placeholder, int errorCode = (int)ErrorCode.Unknow)
        {
            return new ApiResult<T>
            {
                Valid = false,
                ResultMessage = errorMessage,
                ResultCode = errorCode
            };
        }

        public static ApiResult<T> Error(string errorMessage, int errorCode = (int)ErrorCode.Unknow)
        {
            return new ApiResult<T>
            {
                Valid = false,
                ResultMessage = errorMessage,
                ResultCode = errorCode
            };
        }

        public static ApiResult<T> Adapte(Optional<T> result)
        {
            ApiResult<T> apiResult = new ApiResult<T>();

            apiResult.ResultCode = result.ErrorCode;
            apiResult.ResultMessage = result.ErrorMessage;
            apiResult.Valid = result;
            apiResult.Value = result.Value;

            return apiResult;
        }


        /// <summary>
        /// 使用boolean 運算式可以使用此方法
        /// </summary>
        /// <param name="other"></param>
        public static implicit operator bool(ApiResult<T> other)
        {
            return other.Valid;
        }
    }
}

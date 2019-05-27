using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Binodata.Utility.Component.NetCore.AdapterModel
{
    public class Optional<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        private bool Valid { get; set; }

        /// <summary>
        /// 物件回傳
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 錯誤代碼
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 資料驗證錯誤訊息
        /// </summary>
        public IEnumerable<ModelStateError> ModelStateErrors { get; set; }

        /// <summary>
        /// 例外
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 是否為資料驗證錯誤
        /// </summary>
        public bool IsValidationError
        {
            get
            {
                return ModelStateErrors != null && ModelStateErrors.Any();
            }
        }

        /// <summary>
        /// 是否為例外錯誤
        /// </summary>
        public bool IsExceptionError
        {
            get
            {
                return Exception != null;
            }
        }

        /// <summary>
        /// Optional`T轉成Optional`U
        /// </summary>
        public Optional<U> As<U>()
        {
            return new Optional<U>
            {
                Valid = this.Valid,
                ErrorCode = this.ErrorCode,
                ErrorMessage = this.ErrorMessage,
                Exception = this.Exception,
                ModelStateErrors = this.ModelStateErrors
            };
        }

        /// <summary>
        /// Optional`T轉成Optional`U
        /// </summary>
        public Optional<U> As<U>(Func<T, U> func)
        {
            return new Optional<U>
            {
                Value = func(this.Value),
                Valid = this.Valid,
                ErrorCode = this.ErrorCode,
                ErrorMessage = this.ErrorMessage,
                Exception = this.Exception,
                ModelStateErrors = this.ModelStateErrors
            };
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Optional<T> Ok(T value)
        {
            return new Optional<T> { Valid = true, Value = value, ErrorMessage = string.Empty };
        }

        /// <summary>
        /// 失敗
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Optional<T> Error(string errorMessage)
        {
            return new Optional<T> { Valid = false, Value = default(T), ErrorMessage = errorMessage };
        }

        /// <summary>
        /// 失敗
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static Optional<T> Error(string errorMessage, int errorCode)
        {
            return new Optional<T> { Valid = false, Value = default(T), ErrorMessage = errorMessage, ErrorCode = errorCode };
        }

        /// <summary>
        /// 失敗
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="errorCode"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Optional<T> Error(string errorMessage, int errorCode, T value)
        {
            return new Optional<T> { Valid = false, Value = value, ErrorMessage = errorMessage, ErrorCode = errorCode };
        }

        /// <summary>
        /// 資料驗證錯誤，需由使用者排除
        /// </summary>
        public static Optional<T> ValidationError(string key, string errorMessage, int errorCode = 199)
        {
            return ValidationError(new ModelStateError[] { new ModelStateError() { Key = key, ErrorMessage = errorMessage } }, errorCode);
        }

        /// <summary>
        /// 資料驗證錯誤，需由使用者排除
        /// </summary>
        public static Optional<T> ValidationError(IEnumerable<ModelStateError> validationResults, int errorCode = 199)
        {
            return new Optional<T> { Valid = false, ErrorCode = errorCode, ModelStateErrors = validationResults };
        }

        /// <summary>
        /// 程式發生Exception
        /// </summary>
        public static Optional<T> ExceptionError(Exception exception, int errorCode = 199)
        {
            return new Optional<T> { Valid = false, ErrorCode = errorCode, Exception = exception };
        }

        /// <summary>
        /// Optional`U轉成Optional`T
        /// </summary>
        public static Optional<T> Adapt<U>(Optional<U> opt)
        {
            return opt.As<T>();
        }

        /// <summary>
        /// Optional`U轉成Optional`T
        /// </summary>
        public static Optional<T> Adapt<U>(Optional<U> opt, Func<U, T> func)
        {
            return opt.As(func);
        }

        /// <summary>
        /// 使用boolean 運算式可以使用此方法
        /// </summary>
        /// <param name="other"></param>
        public static implicit operator bool(Optional<T> other)
        {
            return other.Valid;
        }
    }
}

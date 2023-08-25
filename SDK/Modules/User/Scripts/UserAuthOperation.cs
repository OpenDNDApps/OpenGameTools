using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OGT.User
{
    public class UserAuthOperation
    {
    }

    public class UserAuthOperationRequest : UserAuthOperation
    {
        
    }

    public class UserAuthOperationResponse<T> where T : class
    {
        public T Payload;
    }

    public class UserLoginOperationResponse : UserAuthOperationResponse<UserLoginOperationPayload>
    {
        public string GUID { get; set; }
        public string SessionToken { get; set; }
        public bool NewlyCreated { get; set; }
    }

    public class UserLoginOperationPayload
    {
        public AccountData AccountData;
        public Dictionary<string, int> UserVirtualCurrency { get; set; }
        public Dictionary<string, UserData> UserData { get; set; }
        //public List<StatisticValue> PlayerStatistics { get; set; }
    }

    public class AccountData
    {
        public string Username;
    }

    public class UserAuthOperationError : UserAuthOperationResponse<UserLoginOperationError>
    {
        public string Message
        {
            get => Payload.Message;
            set => Payload.Message = value;
        }

        public ErrorCode Error
        {
            get => Payload.Code;
            set => Payload.Code = value;
        }
    }

    public class UserLoginOperationError
    {
        public string Message;
        public ErrorCode Code;
    }

    public enum ErrorCode
    {
        Unknown = -1,
    }
}

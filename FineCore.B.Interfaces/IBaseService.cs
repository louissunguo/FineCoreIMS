using FineCore.M.Shared;
using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace FineCore.B.Interfaces {
    [ServiceContract]
    public interface IBaseService {

        [DataMember]
        UserInf CurrentUser { get; }

        [OperationContract]
        bool CheckLogin(string userKeyField, string userKeyValue, string loginPwd);

    }
}

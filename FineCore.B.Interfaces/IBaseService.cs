using System;
using System.ServiceModel;

namespace FineCore.B.Interfaces {
    [ServiceContract]
    public interface IBaseService {

        [OperationContract]
        bool CheckLogin(string userKeyField, string userKeyValue, string loginPwd);

    }
}

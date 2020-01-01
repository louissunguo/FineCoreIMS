using FineCore.M;
using FineCore.M.Business;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace FineCore.B.Interfaces {
    [ServiceContract]
    public interface IApprover:IBaseService {

        [OperationContract] int Approve(Type modelType, int modelId);

        [OperationContract] Array GetFormsForApproval(Type modelType,bool? approved = null);

    }
}

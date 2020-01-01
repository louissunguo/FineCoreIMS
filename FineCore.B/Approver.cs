using FineCore.B.Interfaces;
using FineCore.M;
using System;
using System.Collections.Generic;

namespace FineCore.B {
    public class Approver : BaseService, IApprover {
        public int Approve(Type modelType, int modelId) {
            var approverId = CurrentUser.Id;

            return approverId;
        }

        public Array GetFormsForApproval(Type modelType, bool? approved = null) {
            return new List<BaseModel>().ToArray();
        }
    }
}

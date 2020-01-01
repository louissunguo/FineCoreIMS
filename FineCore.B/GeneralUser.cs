using FineCore.B.Interfaces;
using FineCore.M;
using FineCore.M.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace FineCore.B {
    public class GeneralUser :BaseService, IGeneralUser {
        
        public int Save(BaseModel model) {
            throw new NotImplementedException();
        }

        public int Update(Type modelType, Dictionary<string, object> properties, int modelId) {
            throw new NotImplementedException();
        }
    }
}

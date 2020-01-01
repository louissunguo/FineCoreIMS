using FineCore.M;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.Text;

namespace FineCore.B.Interfaces {
    [ServiceContract]
    public interface IGeneralUser : IBaseService {

        /// <summary>
        /// 保存对象
        /// </summary>
        /// <param name="model">对象</param>
        /// <returns>受影响的行数</returns>
        [OperationContract] int Save(BaseModel model);

        /// <summary>
        /// 更新指定类型的且Id=modelId的对象数据
        /// </summary>
        /// <param name="modelType">指定的对象类型</param>
        /// <param name="properties">要更新的属性字典（属性名，属性值）</param>
        /// <param name="modelId">对象Id</param>
        /// <returns>受影响的行数</returns>
        [OperationContract] int Update(Type modelType, Dictionary<string, object> properties, int modelId);
    }
}

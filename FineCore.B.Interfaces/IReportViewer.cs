using FineCore.M;
using FineCore.M.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace FineCore.B.Interfaces {
    public interface IReportViewer : IBaseService {

        List<FormModel> GetForms(List<ModelPropertyWithValue> propertyWithValues);

        IEnumerable<dynamic> GetDatas(List<string> tables, List<ModelPropertyWithValue> propertyWithValues);

    }
}

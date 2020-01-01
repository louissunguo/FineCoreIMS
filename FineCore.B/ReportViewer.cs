using FineCore.B.Interfaces;
using FineCore.M;
using FineCore.M.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace FineCore.B {
    public class ReportViewer : BaseService, IReportViewer {
        public IEnumerable<dynamic> GetDatas(List<string> tables, List<ModelPropertyWithValue> propertyWithValues) {

            return new List<dynamic>();
        }

        public List<FormModel> GetForms(List<ModelPropertyWithValue> propertyWithValues) {

            return new List<FormModel>() {  };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ComponentModel;
using System.ServiceModel.Web;
using IBDoeService.Entity;

namespace IBDoeService.Interface
{
    [ServiceContract]
    [Description("OrderBuilder Service")]
    public interface IIBDoeOBService
    {
        [WebGet(UriTemplate = "GetTemplates?distributorId={distributorId}")]
        [Description("Get the order templates")]
        ServiceResponse GetTemplates(int distributorId);

        [WebGet(UriTemplate = "GetTemplateDetail?templateId={templateId}")]
        [Description("Get the order templates")]
        ServiceResponse GetTemplateDetail(int templateId);
    }
}

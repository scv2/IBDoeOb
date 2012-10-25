using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace IBDoeService.Interface
{
    public interface IOrderBuilderLogic
    {
        XElement GetTemplates(int distributorID);
        XElement GetTemplateDetail(int templateId);
    }
}
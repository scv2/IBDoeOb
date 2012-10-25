using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IBDoeService.Interface;
using System.Xml.Linq;
using System.Web.Hosting;

namespace IBDoeService.Logic
{
    public class OrderBuilderLogic : IOrderBuilderLogic
    {
        public XElement GetTemplates(int distributorId)
        {
            XElement root = new XElement("ROOT");

            try
            {
                root = XDocument.Load(HostingEnvironment.MapPath("~/XML/Templates.xml")).Root;
            }
            catch (Exception ex)
            {
                throw;
            }

            return root;
        }

        public XElement GetTemplateDetail(int templateId)
        {
            XElement root = new XElement("ROOT");

            try
            {
                root = XDocument.Load(HostingEnvironment.MapPath("~/XML/TemplateDetail.xml")).Root;
            }
            catch (Exception ex)
            {
                throw;
            }

            return root;
        }
    }
}
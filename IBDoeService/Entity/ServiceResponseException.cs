using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace IBDoeService.Entity
{
    public class ServiceResponseException
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public XElement ToXElement()
        {
            return new XElement("Exception",
                new XElement("Code", this.Code),
                new XElement("Message", this.Message));
        }
    }
}

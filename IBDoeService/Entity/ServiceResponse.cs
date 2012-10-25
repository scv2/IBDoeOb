using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace IBDoeService.Entity
{
    public enum ResponseStatus
    {
        OK,
        Warning,
        Error,
        SessionTimeout
    }

    //[DataContract]
    [XmlRoot(ElementName = "Response")]
    public class ServiceResponse : IXmlSerializable
    {
        public ResponseStatus Status { get; private set; }
        public XElement Data { get; set; }
        public ServiceResponseException Exception { get; private set; }

        private static readonly Action<Exception> isExceptionNull = new Action<Exception>(a => { if (a == null) throw new ArgumentException("ex", "Exception object is required"); });

        public ServiceResponse() { }

        public ServiceResponse(XElement data)
        {
            this.Data = data;
            this.Status = ResponseStatus.OK;
        }

        public ServiceResponse(ResponseStatus status)
        {
            this.Status = status;
        }
        
        public XElement ToXElement()
        {
            return this.ToXElement(this.Data);
        }

        public XElement ToXElement(XElement data)
        {
            Func<XElement> dataElem = new Func<XElement>(() => data == null ? null : new XElement("Data", data.Nodes()));

            return new XElement("Response",
                new XAttribute("Status", this.Status.ToString()),
                dataElem());
        }

        public ServiceResponse(Exception ex)
        {
            isExceptionNull(ex);

            this.Status = ResponseStatus.Error;
            this.Exception = new ServiceResponseException()
            {
                Message = FlattenException(ex)
            };
        }

        private string FlattenException(Exception ex, int recursiveLevel = 0)
        {
            StringBuilder sb = new StringBuilder();

            AggregateException exAggr = ex as AggregateException;
            if (exAggr != null)
            {
                sb.Append(exAggr.Message);
                sb.AppendLine(" Total Aggregate Exceptions: " + exAggr.InnerExceptions.Count.ToString());

                for (int i = 0; i < exAggr.InnerExceptions.Count; i++)
                {
                    Exception ex1 = exAggr.InnerExceptions[i];
                    sb.AppendFormat("(Inner Exception #{0}) ", i);

                    if (ex1.InnerException != null)
                        sb.AppendLine("==> " + FlattenException(ex1.InnerException));
                    else
                        sb.AppendLine("==> " + ex1.ToString()); //ex1.Message);
                }
            }
            else
            {
                sb.AppendLine(ex.Message);

                if (ex.InnerException != null)
                {
                    sb.AppendLine(" ==> " + FlattenException(ex.InnerException));
                }
            }

            return sb.ToString();
        }

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("Status", this.Status.ToString());

            if (this.Exception != null)
            {
                writer.WriteStartElement("Exception");
                writer.WriteElementString("Code", this.Exception.Code.ToString());
                writer.WriteElementString("Message", this.Exception.Message);
                writer.WriteEndElement();
            }

            if (this.Data != null)
            {
                new XElement("Data", this.Data.Nodes()).WriteTo(writer);
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using IBDoeService.Entity;
using IBDoeService.Interface;
using IBDoeService.Logic;
using System.ServiceModel.Activation;

namespace IBDoeService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class IBDoeOBService : IIBDoeOBService
    {
        public ServiceResponse GetTemplates(int distributorId)
        {
            return ServiceHandler.Handle(() =>
            {
                return new OrderBuilderLogic().GetTemplates(distributorId);
            });
        }

        public ServiceResponse GetTemplateDetail(int templateId)
        {
            return ServiceHandler.Handle(() =>
            {
                return new OrderBuilderLogic().GetTemplateDetail(templateId);
            });
        }
    }
}

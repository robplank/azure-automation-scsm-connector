using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSM.AzureAutomation.WPF.Helpers;
namespace SCSM.AzureAutomation.WPF.WPFData
{
    public class ParameterMappingData
    {
        public ParameterMappingData (RunbookParameter parameter)
        {
            this.Name = parameter.Name;
            this.Type = parameter.Type;
            this.IsManadatory = parameter.IsMandatory;
            AutomationActivityProperties aap = new AutomationActivityProperties();
            
            ObjectHelpers helper = new ObjectHelpers();
            this.ClassProperty = helper.GetPropertiesNameOfClass(aap);
        }
        string Name { get; }

        string Type { get;  }

        bool IsManadatory { get;  }


        List<string> ClassProperty { get;  }
    }
}

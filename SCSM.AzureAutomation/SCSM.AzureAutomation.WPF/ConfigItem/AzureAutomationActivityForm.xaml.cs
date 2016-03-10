using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EnterpriseManagement.UI.Core.Connection;
using Microsoft.EnterpriseManagement.ConsoleFramework;
using Microsoft.EnterpriseManagement.Configuration;

using Microsoft.EnterpriseManagement.ServiceManager.SharedResources;

using Microsoft.EnterpriseManagement.UI.SdkDataAccess.DataAdapters;
using Microsoft.EnterpriseManagement.UI.SdkDataAccess;
using Microsoft.EnterpriseManagement.GenericForm;
using Microsoft.EnterpriseManagement;

//Requires Microsoft.EnterpriseManagement.UI.SMControls
using Microsoft.EnterpriseManagement.UI.WpfControls;    //Contains InstancePickerDialog, UserPicker, and ListPicker

// Requires Microsoft.EnterpriseManagement.UI.Foundation
using Microsoft.EnterpriseManagement.UI.DataModel;      //Contains IDataItem

//Requires Microsoft.EnterpriseManagement.UI.WpfViews
using Microsoft.EnterpriseManagement.UI.WpfViews;       //Contains FormEvents

//Requires Microsoft.EnterpriseManagement.UI.FormsInfra
using Microsoft.EnterpriseManagement.UI.FormsInfra;     //Contains PreviewFormCommandEventArgs

//Requires Microsoft.EnterpriseManagement.ServiceManager.Application.Common
using Microsoft.EnterpriseManagement.ServiceManager.Application.Common; //Contains ConsoleContextHelper
using Microsoft.EnterpriseManagement.Common;

namespace SCSM.AzureAutomation.WPF.ConfigItem
{
    /// <summary>
    /// Interaction logic for AzureAutomationActivityForm.xaml
    /// </summary>
    public partial class AzureAutomationActivityForm : UserControl
    {
        public AzureAutomationActivityForm()
        {
            InitializeComponent();
        }
    }
}

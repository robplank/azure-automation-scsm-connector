using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Controls;

using Microsoft.Win32;              //Has the RegistryKey class in it
//Requires Microsoft.EnterpriseManagement.Core reference
using Microsoft.EnterpriseManagement;
using Microsoft.EnterpriseManagement.Common;
using Microsoft.EnterpriseManagement.Configuration;
using Microsoft.EnterpriseManagement.ConnectorFramework;

//Requires Microsoft.EnterpriseManagement.UI.SdkDataAccess reference
using Microsoft.EnterpriseManagement.UI.SdkDataAccess;      // Has the ConsoleCommand class in it

//Requires Microsoft.EnterpriseManagement.UI.Foundation reference
using Microsoft.EnterpriseManagement.ConsoleFramework;

namespace SCSM.AzureAutomation.WPF.ConfigItem
{
    /// <summary>
    /// Interaction logic for SelectRunbook.xaml
    /// </summary>
    public partial class SelectRunbook : UserControl
    {
        public SelectRunbook(Guid gAAConnector)
        {
            InitializeComponent();
            LoadRunbooks(gAAConnector);
        }
        internal void LoadRunbooks(Guid gAAConnector)
        {
            String strServerName = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\System Center\\2010\\Service Manager\\Console\\User Settings", "SDKServiceMachine", "localhost").ToString();
            
            //Connect to the server
            EnterpriseManagementGroup emg = new EnterpriseManagementGroup(strServerName);
            ManagementPack mp = emg.GetManagementPack("SCSM.AzureAutomation", "ac1fe0583b6c84af", new Version("1.0.0.0"));
            ManagementPackClass classAARunbook = mp.GetClass("SCSM.AzureAutomation.Runbook");
            ManagementPackRelationship relRunbookHasConnector = emg.EntityTypes.GetRelationshipClass(SCSM.AzureAutomation.WPF.CustomControls.Resources.guidRunbookHasConnectorRelationship);
            
            //Create Datatable that will be used to select a runbook
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Created On");

            IList<EnterpriseManagementRelationshipObject<EnterpriseManagementObject>> listContainedObjects = emg.EntityObjects.GetRelationshipObjectsWhereTarget<EnterpriseManagementObject>(gAAConnector, ObjectQueryOptions.Default);

            foreach (EnterpriseManagementRelationshipObject<EnterpriseManagementObject> c in listContainedObjects)
            {
                if(c.RelationshipId == relRunbookHasConnector.Id)
                {
                    EnterpriseManagementObject emoRunbook = c.SourceObject;
                    DataRow newRow = dt.NewRow();
                    newRow["Name"] = emoRunbook[classAARunbook, "displayName"];
                    newRow["Created On"] = emoRunbook[classAARunbook, "CreatedDate"];
                    dt.Rows.Add(newRow);
                }
            }

            slvRunbooks.DataContext = dt.DefaultView;
        }
   
    }
}

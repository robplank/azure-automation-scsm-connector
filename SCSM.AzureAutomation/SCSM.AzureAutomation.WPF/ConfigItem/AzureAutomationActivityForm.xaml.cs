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
using Microsoft.EnterpriseManagement.UI.Extensions.Shared;
using Microsoft.EnterpriseManagement.UI.SdkDataAccess.DataAdapters;
using Microsoft.EnterpriseManagement.UI.SdkDataAccess;
using Microsoft.EnterpriseManagement.GenericForm;
using Microsoft.EnterpriseManagement;
using System.ComponentModel;
using System.ComponentModel.Design;
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
using SCSM.AzureAutomation.WPF.ConfigItem;
using System.Windows.Markup;
using SCSM.AzureAutomation.WPF.WPFData;

namespace SCSM.AzureAutomation.WPF.ConfigItem
{
    /// <summary>
    /// Interaction logic for AzureAutomationActivityForm.xaml
    /// </summary>

    public partial class AzureAutomationActivityForm : UserControl, IComponentConnector
    {
        private RelatedItemsPane _relatedItemsPane;
        
        private Parametereditor parameterEditor;
        private RelatedItemsPane configItemsPane;
        internal Grid gridselectRunbook;
        internal Grid Runbook;
        //private SchedulingTab sTab;
        //private IDataItem item;
        private IList<RunbookParameter> listParams;
        //internal TabItem tabGeneral;
        //internal Grid gridRunbook;
        //internal TabItem tabHistory;
        //internal TabItem tabRelatedItems;
        //internal Grid gridScheduling; 
        private DependencyPropertyDescriptor dpdConnector = DependencyPropertyDescriptor.FromProperty((DependencyProperty)SingleInstancePicker.InstanceProperty, typeof(SingleInstancePicker));
        EnterpriseManagementGroup emg;



        public AzureAutomationActivityForm()
        {
            InitializeComponent();
            _relatedItemsPane = new RelatedItemsPane(new ConfigItemRelatedItemsConfiguration());
            tabRelatedItems.Content = _relatedItemsPane;
            configItemsPane = new RelatedItemsPane(new ConfigItemRelatedItemsConfiguration());
            tabConfigItems.Content = configItemsPane;
        }
        public void LoadParameterEditor()
        {
            //this.parameterEditor = new Parametereditor(this.item, this.listParams);
            //this.gridParameterEditor.Children.Add((UIElement)this.parameterEditor);
        }
        //public bool IsTemplate
        //{
        //    get
        //    {
        //        return FormUtilities.get_Instance().IsFormInTemplateMode((FrameworkElement)this);
        //    }
        //}
        public void LoadParentWI(DependencyObject child)
        {
            FormWindow formWindow;
            try
            {
                DependencyObject parent = VisualTreeHelper.GetParent(child);

            }
            catch
            {

            }
        }

        private void buttonCreateConnector_Click(object sender, RoutedEventArgs e)
        {
            this.gridSelectRunbook.Visibility = Visibility.Visible;
            //this.pickerAAConnector.Instance.ToString();
        }

        private void buttonSelectRunbook_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void pickerAAConnector_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            //if(pickerAAConnector.Instance.ToString() != null)
            //{
            //    this.buttonSelectRunbook.IsEnabled = true;
            //    this.gridRunbook.Visibility = Visibility.Visible;


            //}
        }
        private void LoadDefault()
        {
            if (this.DataContext is IDataItem)
            {
                IDataItem instance = (this.DataContext as IDataItem);
                instance["CreatedDate"] = (object)DateTime.Now;
                instance["ActivityCreatedBy"] = Microsoft.EnterpriseManagement.UI.Extensions.Shared.ConsoleContextHelper.Instance.CurrentUser;

                //instance["DisplayName"] = this.DataContex
            }
        }
        void GetSession()
        {
            // Get the current session, more info: http://blogs.technet.com/servicemanager/archive/2010/02/11/tasks-part-1-tasks-overview.aspx
            IServiceContainer container = (IServiceContainer)FrameworkServices.GetService(typeof(IServiceContainer));

            Microsoft.EnterpriseManagement.UI.Core.Connection.IManagementGroupSession curSession =
                (Microsoft.EnterpriseManagement.UI.Core.Connection.IManagementGroupSession)container.GetService(typeof(Microsoft.EnterpriseManagement.UI.Core.Connection.IManagementGroupSession));
            if (curSession == null)
                throw new ValueUnavailableException("curSession is null");
            emg = curSession.ManagementGroup;
        }
        private void buttonUpdateParameters_Click(object sender, RoutedEventArgs e)
        {
            Guid grunbookclass = new Guid("98d7a1a1-650d-5645-caf7-d42ff445c991");
            IDataItem runbook = pickerRunbook.Instance as IDataItem;
            Guid grunbookID = new Guid(runbook["ID$"].ToString());
            ManagementPackClass mpcrunbook = emg.EntityTypes.GetClass(grunbookclass);
            EnterpriseManagementObject emorunbook = emg.EntityObjects.GetObject<EnterpriseManagementObject>(grunbookID, ObjectQueryOptions.Default);
            string Paramsjson = (emorunbook[mpcrunbook, "Parameters"]).ToString();
            List<RunbookParameter> RunbookParameterList = (List<RunbookParameter>)Newtonsoft.Json.JsonConvert.DeserializeObject(Paramsjson, typeof(List<RunbookParameter>));
            cmbtext1.ItemsSource = RunbookParameterList;
            cmbtext1.DisplayMemberPath = "Name";
            cmbtext2.ItemsSource = RunbookParameterList;
            cmbtext2.DisplayMemberPath = "Name";
            cmbtext3.ItemsSource = RunbookParameterList;
            cmbtext3.DisplayMemberPath = "Name";
            cmbtext4.ItemsSource = RunbookParameterList;
            cmbtext4.DisplayMemberPath = "Name";
            cmbtext5.ItemsSource = RunbookParameterList;
            cmbtext5.DisplayMemberPath = "Name";
            cmbtext6.ItemsSource = RunbookParameterList;
            cmbtext6.DisplayMemberPath = "Name";
            cmbtext7.ItemsSource = RunbookParameterList;
            cmbtext7.DisplayMemberPath = "Name";
            cmbtext8.ItemsSource = RunbookParameterList;
            cmbtext8.DisplayMemberPath = "Name";
            cmbtext9.ItemsSource = RunbookParameterList;
            cmbtext9.DisplayMemberPath = "Name";
            cmbtext10.ItemsSource = RunbookParameterList;
            cmbtext10.DisplayMemberPath = "Name";
            cmbint1.ItemsSource = RunbookParameterList;
            cmbint1.DisplayMemberPath = "Name";
            cmbint2.ItemsSource = RunbookParameterList;
            cmbint2.DisplayMemberPath = "Name";
            cmbint3.ItemsSource = RunbookParameterList;
            cmbint3.DisplayMemberPath = "Name";
            cmbint4.ItemsSource = RunbookParameterList;
            cmbint4.DisplayMemberPath = "Name";
            cmbint5.ItemsSource = RunbookParameterList;
            cmbint5.DisplayMemberPath = "Name";
            cmbbool1.ItemsSource = RunbookParameterList;
            cmbbool1.DisplayMemberPath = "Name";
            cmbbool2.ItemsSource = RunbookParameterList;
            cmbbool2.DisplayMemberPath = "Name";
            cmbbool3.ItemsSource = RunbookParameterList;
            cmbbool3.DisplayMemberPath = "Name";
            cmbbool4.ItemsSource = RunbookParameterList;
            cmbbool4.DisplayMemberPath = "Name";
            cmbbool5.ItemsSource = RunbookParameterList;
            cmbbool5.DisplayMemberPath = "Name";
            cmbdate1.ItemsSource = RunbookParameterList;
            cmbdate1.DisplayMemberPath = "Name";
            cmbdate2.ItemsSource = RunbookParameterList;
            cmbdate2.DisplayMemberPath = "Name";
            cmbdate3.ItemsSource = RunbookParameterList;
            cmbdate3.DisplayMemberPath = "Name";
            cmbdate4.ItemsSource = RunbookParameterList;
            cmbdate4.DisplayMemberPath = "Name";
            cmbdate5.ItemsSource = RunbookParameterList;
            cmbdate5.DisplayMemberPath = "Name";

            this.buttonUpdateParameters.Content = "Refresh Parameters";

        }
    }
    
}

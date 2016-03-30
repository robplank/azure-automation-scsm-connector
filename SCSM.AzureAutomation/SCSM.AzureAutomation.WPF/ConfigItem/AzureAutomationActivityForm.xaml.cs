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

        private void buttonUpdateParameters_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
    
}

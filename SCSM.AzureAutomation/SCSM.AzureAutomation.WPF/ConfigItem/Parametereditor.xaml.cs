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

using System.Windows.Markup;
using Microsoft.EnterpriseManagement.ConsoleFramework;

using Microsoft.EnterpriseManagement.UI.DataModel;
using Microsoft.EnterpriseManagement.UI.Extensions.Shared;
using Microsoft.EnterpriseManagement.UI.WpfControls;
using Microsoft.Windows.Controls;
using SCSM.AzureAutomation.WPF.WPFData;

namespace SCSM.AzureAutomation.WPF.ConfigItem
{
    /// <summary>
    /// Interaction logic for Parametereditor.xaml
    /// </summary>
    public partial class Parametereditor : UserControl, IComponentConnector
    {
        private IDataItem item;
        private IList<RunbookParameter> listRunbookParams;
        


        public Parametereditor()
        {
            InitializeComponent();
        }

        public Parametereditor(IDataItem dataContext, IList<RunbookParameter> parameters)
        {
            this.item = dataContext;
            this.listRunbookParams = parameters;
            this.InitializeComponent();
        }
    }
}

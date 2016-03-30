using System;
using System.Windows;


namespace SCSM.AzureAutomation.WPF.CustomControls
{
    /// <summary>
    /// Interaction logic for Resources.xaml
    /// </summary>
    public partial class Resources : ResourceDictionary
    {
        public static Guid guidRunbookHasConnectorRelationship = new Guid("");
        public Resources()
        {
            InitializeComponent();
        }
    }
}

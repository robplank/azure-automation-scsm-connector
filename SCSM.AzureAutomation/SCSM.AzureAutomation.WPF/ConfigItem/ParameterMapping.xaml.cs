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
using SCSM.AzureAutomation.WPF.WPFData;

namespace SCSM.AzureAutomation.WPF.ConfigItem
{
    /// <summary>
    /// Interaction logic for ParameterMapping.xaml
    /// </summary>
    public partial class ParameterMapping : UserControl
    {
        public ParameterMapping()
        {
            InitializeComponent();
            ComboBoxBuild();
            
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

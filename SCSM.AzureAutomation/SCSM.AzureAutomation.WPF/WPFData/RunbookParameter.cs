
namespace SCSM.AzureAutomation.WPF.WPFData
{
    public class RunbookParameter
    {

        public string Name { get; set;}
        public bool IsMandatory { get; set; }
        public string Type {get; set;}

        public string Value { get; set;}

        public int Position { get; set; }

        public RunbookParameter(string name, bool mandatory, string typename, int position)
        {
            this.Name = name;
            this.IsMandatory = mandatory;
            this.Type = typename;
            this.Position = position;
        }

        public RunbookParameter()
        {
        }
        
    }
}

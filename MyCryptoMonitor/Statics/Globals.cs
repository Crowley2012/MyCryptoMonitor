using System.ComponentModel;

namespace MyCryptoMonitor.Statics
{
    public class Globals
    {
        public enum Types {[Description("Email")] Email, [Description("Verizon")] Verizon, [Description("AT&T")] ATT, [Description("Sprint")] Sprint, [Description("Boost Mobile")] Boost, [Description("T-Mobile")] TMobile, [Description("US Cellular")] USCellular, [Description("Virgin Mobile")] VirginMobile }
        public enum Operators {[Description("Greater Than")] GreaterThan, [Description("Less Than")] LessThan }
    }
}

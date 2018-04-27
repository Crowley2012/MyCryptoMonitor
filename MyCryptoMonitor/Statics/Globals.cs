using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyCryptoMonitor.Statics
{
    public class Globals
    {
        public static string BackgroundColor { get; private set; }
        public static string InputBackgroundColor { get; private set; }
        public static string ButtonColor { get; private set; }
        public static string DisabledBackgroundColor { get; private set; }
        public static string FontColor { get; private set; }
        public static string PositiveColor { get; private set; }
        public static string NegativeColor { get; private set; }

        public static void SetupTheme()
        {
            switch (UserConfigService.Theme)
            {
                case "Default":
                    BackgroundColor = "#F0F0F0";
                    InputBackgroundColor = "#ffffff";
                    ButtonColor = "#e1e1e1";
                    DisabledBackgroundColor = "#f1f1f1";
                    FontColor = "#000000";
                    PositiveColor = "#008000";
                    NegativeColor = "#FF0000";
                    break;
                case "Dark":
                    BackgroundColor = "#252526";
                    InputBackgroundColor = "#333333";
                    ButtonColor = "#333333";
                    DisabledBackgroundColor = "#333333";
                    FontColor = "#94979c";
                    PositiveColor = "#008000";
                    NegativeColor = "#FF0000";
                    break;
                case "Royal":
                    BackgroundColor = "#000019";
                    InputBackgroundColor = "#000032";
                    ButtonColor = "#000023";
                    DisabledBackgroundColor = "#333333";
                    FontColor = "#94979c";
                    PositiveColor = "#ff0";
                    NegativeColor = "#FF0000";
                    break;
                case "Highlight":
                    BackgroundColor = "#212121";
                    InputBackgroundColor = "#333333";
                    ButtonColor = "#333333";
                    DisabledBackgroundColor = "#333333";
                    FontColor = "#94979c";
                    PositiveColor = "#ff80ab";
                    NegativeColor = "#ff1744";
                    break;
            }
        }

        public static void SetTheme(Control container)
        {
            container.BackColor = ColorTranslator.FromHtml(BackgroundColor);

            foreach (var control in GetAllTextBoxControls(container))
            {
                if (control.Tag != null)
                {
                    switch (control.Tag.ToString())
                    {
                        case "PositiveProfit":
                            control.ForeColor = ColorTranslator.FromHtml(PositiveColor);
                            continue;
                        case "NegativeProfit":
                            control.ForeColor = ColorTranslator.FromHtml(NegativeColor);
                            continue;
                    }
                }

                control.ForeColor = ColorTranslator.FromHtml(FontColor);

                if (control is TextBox)
                    ((TextBox)control).BackColor = ColorTranslator.FromHtml(InputBackgroundColor);

                if (control is ComboBox)
                    ((ComboBox)control).BackColor = ColorTranslator.FromHtml(InputBackgroundColor);

                if (control is Button)
                    ((Button)control).BackColor = ColorTranslator.FromHtml(ButtonColor);

                if (control is GroupBox)
                    ((GroupBox)control).BackColor = ((GroupBox)control).Enabled ? ColorTranslator.FromHtml(BackgroundColor) : ColorTranslator.FromHtml(DisabledBackgroundColor);

                if (control is MenuStrip)
                    ((MenuStrip)control).BackColor = ColorTranslator.FromHtml(BackgroundColor);
            }
        }

        public static IEnumerable<Control> GetAllTextBoxControls(Control container)
        {
            List<Control> controlList = new List<Control>();

            foreach (Control c in container.Controls)
            {
                controlList.AddRange(GetAllTextBoxControls(c));
                controlList.Add(c);
            }

            return controlList;
        }
    }
}

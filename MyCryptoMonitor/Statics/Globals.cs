using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyCryptoMonitor.Statics
{
    public class Globals
    {
        public static string BackgroundColor { get; set; } = "#252526";
        public static string AccentColor { get; set; } = "#94979c";
        public static string DisabledColor { get; set; } = "#333333";

        public static void SetTheme(Control container)
        {
            container.BackColor = ColorTranslator.FromHtml(BackgroundColor);

            foreach (var control in GetAllTextBoxControls(container))
            {
                control.ForeColor = ColorTranslator.FromHtml(AccentColor);

                if (control is TextBox)
                    ((TextBox)control).BackColor = ColorTranslator.FromHtml(BackgroundColor);

                if (control is ComboBox)
                    ((ComboBox)control).BackColor = ColorTranslator.FromHtml(BackgroundColor);

                if (control is Button)
                    ((Button)control).BackColor = ColorTranslator.FromHtml(BackgroundColor);

                if (control is GroupBox)
                    ((GroupBox)control).BackColor = ((GroupBox)control).Enabled ? ColorTranslator.FromHtml(BackgroundColor) : ColorTranslator.FromHtml(DisabledColor);

                if (control is MenuStrip)
                {
                    ((MenuStrip)control).BackColor = ColorTranslator.FromHtml(BackgroundColor);
                    ((MenuStrip)control).ForeColor = ColorTranslator.FromHtml(AccentColor);
                }
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

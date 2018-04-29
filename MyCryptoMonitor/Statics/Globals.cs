using MyCryptoMonitor.Objects;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyCryptoMonitor.Statics
{
    public class Globals
    {
        public static void SetTheme(Control container)
        {
            container.BackColor = ColorTranslator.FromHtml(UserConfigService.Theme.BackgroundColor);

            foreach (var control in GetAllTextBoxControls(container))
            {
                if (control.Tag != null)
                {
                    switch (control.Tag.ToString())
                    {
                        case "PositiveProfit":
                            control.ForeColor = ColorTranslator.FromHtml(UserConfigService.Theme.PositiveColor);
                            continue;
                        case "NegativeProfit":
                            control.ForeColor = ColorTranslator.FromHtml(UserConfigService.Theme.NegativeColor);
                            continue;
                    }
                }

                control.ForeColor = ColorTranslator.FromHtml(UserConfigService.Theme.FontColor);

                if (control is TextBox)
                    ((TextBox)control).BackColor = ColorTranslator.FromHtml(UserConfigService.Theme.InputColor);

                if (control is ComboBox)
                    ((ComboBox)control).BackColor = ColorTranslator.FromHtml(UserConfigService.Theme.InputColor);

                if (control is Button)
                    ((Button)control).BackColor = ColorTranslator.FromHtml(UserConfigService.Theme.ButtonColor);

                if (control is GroupBox)
                    ((GroupBox)control).BackColor = ((GroupBox)control).Enabled ? ColorTranslator.FromHtml(UserConfigService.Theme.BackgroundColor) : ColorTranslator.FromHtml(UserConfigService.Theme.DisabledColor);

                if (control is MenuStrip)
                    ((MenuStrip)control).BackColor = ColorTranslator.FromHtml(UserConfigService.Theme.BackgroundColor);
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceSettingsUserControl
{
    public partial class DeviceSettingsUserControl: UserControl
    {
        public DeviceSettingsUserControl()
        {
            InitializeComponent();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e) {
            foreach (var control in ((CheckBox)sender).Parent.Controls) {
                var tag = control.GetType().GetProperty("Tag").GetValue(control);
                if (tag != null && tag != "" && tag != "fan_control_enabled")
                    ((NumericUpDown)control).Enabled = ((CheckBox)sender).Checked;
            }
        }
    }
}

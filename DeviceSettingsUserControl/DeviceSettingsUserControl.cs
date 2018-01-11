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
        public event EventHandler ButtonResetToDefaultClicked;
        public event EventHandler ButtonResetAllClicked;
        public event EventHandler ButtonCopyToOthersClicked;

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

        private void buttonResetToDefault_Click(object sender, EventArgs e) {
            if (this.ButtonResetToDefaultClicked != null)
                this.ButtonResetToDefaultClicked(this, new EventArgs());
       }

        private void buttonResetAll_Click(object sender, EventArgs e) {
            if (this.ButtonResetAllClicked != null)
                this.ButtonResetAllClicked(this, new EventArgs());
        }

        private void buttonCopyToOthers_Click(object sender, EventArgs e) {
            if (this.ButtonCopyToOthersClicked != null)
                this.ButtonCopyToOthersClicked(this, new EventArgs());
        }

        private void checkBoxEthashPascalOverclockingEnabled_CheckedChanged(object sender, EventArgs e) {
            foreach (var control in ((CheckBox)sender).Parent.Controls) {
                var tag = control.GetType().GetProperty("Tag").GetValue(control);
                if (tag != null && tag != "" && tag != "ethash_pascal_overclocking_enabled")
                    ((NumericUpDown)control).Enabled = ((CheckBox)sender).Checked;
            }
        }

        private void checkBoxEthashOverclockingEnabled_CheckedChanged(object sender, EventArgs e) {
            foreach (var control in ((CheckBox)sender).Parent.Controls) {
                var tag = control.GetType().GetProperty("Tag").GetValue(control);
                if (tag != null && tag != "" && tag != "ethash_overclocking_enabled")
                    ((NumericUpDown)control).Enabled = ((CheckBox)sender).Checked;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) {
            foreach (var control in ((CheckBox)sender).Parent.Controls) {
                var tag = control.GetType().GetProperty("Tag").GetValue(control);
                if (tag != null && tag != "" && tag != "cryptonight_overclocking_enabled")
                    ((NumericUpDown)control).Enabled = ((CheckBox)sender).Checked;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e) {
            foreach (var control in ((CheckBox)sender).Parent.Controls) {
                var tag = control.GetType().GetProperty("Tag").GetValue(control);
                if (tag != null && tag != "" && tag != "lbry_overclocking_enabled")
                    ((NumericUpDown)control).Enabled = ((CheckBox)sender).Checked;
            }
        }

        private void checkBox5_CheckedChanged_1(object sender, EventArgs e) {
            foreach (var control in ((CheckBox)sender).Parent.Controls) {
                var tag = control.GetType().GetProperty("Tag").GetValue(control);
                if (tag != null && tag != "" && tag != "pascal_overclocking_enabled")
                    ((NumericUpDown)control).Enabled = ((CheckBox)sender).Checked;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            foreach (var control in ((CheckBox)sender).Parent.Controls) {
                var tag = control.GetType().GetProperty("Tag").GetValue(control);
                if (tag != null && tag != "" && tag != "neoscrypt_overclocking_enabled")
                    ((NumericUpDown)control).Enabled = ((CheckBox)sender).Checked;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e) {
            foreach (var control in ((CheckBox)sender).Parent.Controls) {
                var tag = control.GetType().GetProperty("Tag").GetValue(control);
                if (tag != null && tag != "" && tag != "lyra2rev2_overclocking_enabled")
                    ((NumericUpDown)control).Enabled = ((CheckBox)sender).Checked;
            }
        }
    }
}

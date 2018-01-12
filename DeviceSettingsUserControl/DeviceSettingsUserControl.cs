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
        public event EventHandler CheckedChanged;

        public DeviceSettingsUserControl()
        {
            InitializeComponent();
        }

        private void checkBoxFanControlEnabled_CheckedChanged(object sender, EventArgs e) {
            if (this.CheckedChanged != null)
                this.CheckedChanged(this, new EventArgs());
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

        private void checkBox7_CheckedChanged(object sender, EventArgs e) {
            if (this.CheckedChanged != null)
                this.CheckedChanged(this, new EventArgs());
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            if (this.CheckedChanged != null)
                this.CheckedChanged(this, new EventArgs());
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) {
            if (this.CheckedChanged != null)
                this.CheckedChanged(this, new EventArgs());
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e) {
            if (this.CheckedChanged != null)
                this.CheckedChanged(this, new EventArgs());
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e) {
            if (this.CheckedChanged != null)
                this.CheckedChanged(this, new EventArgs());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            if (this.CheckedChanged != null)
                this.CheckedChanged(this, new EventArgs());
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e) {
            if (this.CheckedChanged != null)
                this.CheckedChanged(this, new EventArgs());
        }
    }
}

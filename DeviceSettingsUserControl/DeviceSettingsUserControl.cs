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
        public event EventHandler ValueChanged;

        public DeviceSettingsUserControl()
        {
            InitializeComponent();
        }

        private void checkBoxFanControlEnabled_CheckedChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
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
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown52_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown27_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown26_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown28_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0EthashPascalThreads_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0EthashPascalIntensity_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0EthashPascalPascalIterations_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0EthashPascalPowerLimit_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0EthashPascalCoreClock_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0EthashPascalMemoryClock_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown24_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0EthashPascalMemoryVoltage_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0EthashThreads_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0EthashIntensity_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0EthashLocalWorkSize_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0CryptoNightThreads_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0CryptoNightRawIntensity_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0CryptoNightLocalWorkSize_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0LbryThreads_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0LbryIntensity_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0LbryLocalWorkSize_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown13_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown15_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown12_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown14_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown11_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0PascalThreads_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0PascalIntensity_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDownDevice0PascalLocalWorkSize_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown18_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown20_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown17_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown19_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown16_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown23_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown29_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown22_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown25_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown21_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown37_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown36_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown35_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown32_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown34_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown31_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown33_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown30_ValueChanged(object sender, EventArgs e) {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }
    }
}

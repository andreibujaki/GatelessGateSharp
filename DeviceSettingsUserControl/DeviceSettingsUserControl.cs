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

        private void textBoxSEQ_MISCx_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            string item = textBox.Text;
            var regex = (new System.Text.RegularExpressions.Regex(@"[^a-fA-F0-9]"));
            //int n = 0;
            //if (!int.TryParse(item, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.CurrentInfo, out n) && item != String.Empty) {
            if (regex.Match(item).Success && item != String.Empty) {
                textBox.Text = regex.Replace(item, "");
                textBox.SelectionStart = textBox.Text.Length;
            }
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void textBoxEthashPascalSEQ_MISC1_TextChanged(object sender, EventArgs e)
        {
            textBoxSEQ_MISCx_TextChanged(sender, e);
        }

        private void textBoxEthashPascalSEQ_MISC3_TextChanged(object sender, EventArgs e)
        {
            textBoxSEQ_MISCx_TextChanged(sender, e);
        }

        private void textBoxEthashPascalSEQ_MISC8_TextChanged(object sender, EventArgs e)
        {
            textBoxSEQ_MISCx_TextChanged(sender, e);
        }

        private void textBoxEthashSEQ_MISC1_TextChanged(object sender, EventArgs e)
        {
            textBoxSEQ_MISCx_TextChanged(sender, e);
        }

        private void textBoxEthashSEQ_MISC3_TextChanged(object sender, EventArgs e)
        {
            textBoxSEQ_MISCx_TextChanged(sender, e);
        }

        private void textBoxEthashSEQ_MISC8_TextChanged(object sender, EventArgs e)
        {
            textBoxSEQ_MISCx_TextChanged(sender, e);
        }

        private void textBoxCryptoNightSEQ_MISC1_TextChanged(object sender, EventArgs e)
        {
            textBoxSEQ_MISCx_TextChanged(sender, e);
        }

        private void textBoxCryptoNightSEQ_MISC3_TextChanged(object sender, EventArgs e)
        {
            textBoxSEQ_MISCx_TextChanged(sender, e);
        }

        private void textBoxCryptoNightSEQ_MISC8_TextChanged(object sender, EventArgs e)
        {
            textBoxSEQ_MISCx_TextChanged(sender, e);
        }

        private void textBoxNeoScryptSEQ_MISC1_TextChanged(object sender, EventArgs e)
        {
            textBoxSEQ_MISCx_TextChanged(sender, e);
        }

        private void textBoxNeoScryptSEQ_MISC3_TextChanged(object sender, EventArgs e)
        {
            textBoxSEQ_MISCx_TextChanged(sender, e);
        }

        private void textBoxNeoScryptSEQ_MISC8_TextChanged(object sender, EventArgs e)
        {
            textBoxSEQ_MISCx_TextChanged(sender, e);
        }

        private void checkBox4_CheckedChanged_1(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown40_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown42_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown39_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown43_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown45_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown44_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown48_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown49_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown47_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown46_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown41_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown38_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown55_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown56_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown54_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown53_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown51_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown50_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown61_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown62_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown60_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown59_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown58_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        private void numericUpDown57_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }
    }
}

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
            foreach (var checkBox in Utilities.FindAllChildrenByType<CheckBox>(this))
                checkBox.CheckedChanged += new System.EventHandler(control_ValueChanged);
            foreach (var numericUpDown in Utilities.FindAllChildrenByType<NumericUpDown>(this))
                numericUpDown.ValueChanged += new System.EventHandler(control_ValueChanged);
            foreach (var textBox in Utilities.FindAllChildrenByType<TextBox>(this))
                textBox.TextChanged += new System.EventHandler(control_ValueChanged);
        }

        private void control_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
            if (sender.GetType() == typeof(CheckBox)) {
                var checkBox = (CheckBox)sender;
                var regex = new System.Text.RegularExpressions.Regex(@"^(.*)_enabled$");
                if (checkBox.Tag != null && checkBox.Tag.GetType() == typeof(string) && regex.Match((string)checkBox.Tag).Success) {
                    var match = regex.Match((string)checkBox.Tag);
                    var target = match.Groups[1].Value;
                    foreach (var control in Utilities.FindAllChildrenByType<Control>(checkBox.Parent)) {
                        if ((string)control.Tag == target) {
                            checkBox.ForeColor = (checkBox.Checked ? System.Drawing.SystemColors.ControlText : System.Drawing.SystemColors.ControlDark);
                            control.Enabled = checkBox.Checked;
                            break;
                        }
                    }
                }
                regex = new System.Text.RegularExpressions.Regex(@"^(.*)_memory_timings_enabled$");
                if (checkBox.Tag != null && checkBox.Tag.GetType() == typeof(string) && regex.Match((string)checkBox.Tag).Success) {
                    regex = new System.Text.RegularExpressions.Regex(@"memory_timings");
                    foreach (var groupBox in Utilities.FindAllChildrenByType<GroupBox>(checkBox.Parent)) {
                        if (groupBox.Tag != null && groupBox.Tag.GetType() == typeof(string) && regex.Match((string)groupBox.Tag).Success)
                            groupBox.Enabled = checkBox.Checked;
                    }
                }
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
    }

    public static class Utilities
    {
        public static IEnumerable<T> FindAllChildrenByType<T>(this Control control)
        {
            IEnumerable<Control> controls = control.Controls.Cast<Control>();
            return controls
                .OfType<T>()
                .Concat<T>(controls.SelectMany<Control, T>(ctrl => FindAllChildrenByType<T>(ctrl)));
        }
    }
}

namespace GatelessGateSharp
{
    partial class MemoryTimingStrapForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxMemoryTimingStrap = new System.Windows.Forms.TextBox();
            this.buttonPasteFromClipboard = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(575, 70);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(494, 70);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxMemoryTimingStrap
            // 
            this.textBoxMemoryTimingStrap.Location = new System.Drawing.Point(12, 12);
            this.textBoxMemoryTimingStrap.Name = "textBoxMemoryTimingStrap";
            this.textBoxMemoryTimingStrap.Size = new System.Drawing.Size(505, 20);
            this.textBoxMemoryTimingStrap.TabIndex = 2;
            // 
            // buttonPasteFromClipboard
            // 
            this.buttonPasteFromClipboard.Location = new System.Drawing.Point(523, 11);
            this.buttonPasteFromClipboard.Name = "buttonPasteFromClipboard";
            this.buttonPasteFromClipboard.Size = new System.Drawing.Size(127, 23);
            this.buttonPasteFromClipboard.TabIndex = 3;
            this.buttonPasteFromClipboard.Text = "Paste from Clipboard";
            this.buttonPasteFromClipboard.UseVisualStyleBackColor = true;
            this.buttonPasteFromClipboard.Click += new System.EventHandler(this.buttonPasteFromClipboard_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(641, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Note: Due to the nature of on-the-fly memory timing mods, existing memory timing " +
    "straps for BIOS\'es may not work without modifications.";
            // 
            // MemoryTimingStrapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 105);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonPasteFromClipboard);
            this.Controls.Add(this.textBoxMemoryTimingStrap);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "MemoryTimingStrapForm";
            this.Text = "Memory Timing Strap";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.TextBox textBoxMemoryTimingStrap;
        private System.Windows.Forms.Button buttonPasteFromClipboard;
        private System.Windows.Forms.Label label1;
    }
}
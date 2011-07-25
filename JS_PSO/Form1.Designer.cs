namespace JS_PSO {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
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
        private void InitializeComponent() {
            this.swarmView1 = new JS_PSO.SwarmView();
            this.ResetButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // swarmView1
            // 
            this.swarmView1.BackColor = System.Drawing.Color.Black;
            this.swarmView1.Location = new System.Drawing.Point(13, 13);
            this.swarmView1.Name = "swarmView1";
            this.swarmView1.Size = new System.Drawing.Size(417, 326);
            this.swarmView1.TabIndex = 0;
            this.swarmView1.Load += new System.EventHandler(this.swarmView1_Load);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(437, 315);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 1;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 370);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.swarmView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private SwarmView swarmView1;
        private System.Windows.Forms.Button ResetButton;
    }
}


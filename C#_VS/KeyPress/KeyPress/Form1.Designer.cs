
namespace KeyPress
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_keyChar = new System.Windows.Forms.Label();
            this.lb_keyInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_keyChar
            // 
            this.lb_keyChar.AutoSize = true;
            this.lb_keyChar.Location = new System.Drawing.Point(51, 36);
            this.lb_keyChar.Name = "lb_keyChar";
            this.lb_keyChar.Size = new System.Drawing.Size(61, 20);
            this.lb_keyChar.TabIndex = 0;
            this.lb_keyChar.Text = "keyChar";
            // 
            // lb_keyInfo
            // 
            this.lb_keyInfo.AutoSize = true;
            this.lb_keyInfo.Location = new System.Drawing.Point(51, 89);
            this.lb_keyInfo.Name = "lb_keyInfo";
            this.lb_keyInfo.Size = new System.Drawing.Size(57, 20);
            this.lb_keyInfo.TabIndex = 1;
            this.lb_keyInfo.Text = "keyInfo";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lb_keyInfo);
            this.Controls.Add(this.lb_keyChar);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_keyChar;
        private System.Windows.Forms.Label lb_keyInfo;
    }
}


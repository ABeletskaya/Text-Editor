namespace TextEditorBel
{
    partial class EnterNameForm
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
            if (disposing && (components != null))
            {
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
            this.enterNameLbl = new System.Windows.Forms.Label();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.OKbtn = new System.Windows.Forms.Button();
            this.canselBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // enterNameLbl
            // 
            this.enterNameLbl.AutoSize = true;
            this.enterNameLbl.Location = new System.Drawing.Point(22, 26);
            this.enterNameLbl.Name = "enterNameLbl";
            this.enterNameLbl.Size = new System.Drawing.Size(80, 13);
            this.enterNameLbl.TabIndex = 0;
            this.enterNameLbl.Text = "Enter file name:";
            // 
            // nameTB
            // 
            this.nameTB.Location = new System.Drawing.Point(25, 59);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(222, 20);
            this.nameTB.TabIndex = 1;
            // 
            // OKbtn
            // 
            this.OKbtn.Location = new System.Drawing.Point(25, 96);
            this.OKbtn.Name = "OKbtn";
            this.OKbtn.Size = new System.Drawing.Size(99, 24);
            this.OKbtn.TabIndex = 2;
            this.OKbtn.Text = "OK";
            this.OKbtn.UseVisualStyleBackColor = true;
            this.OKbtn.Click += new System.EventHandler(this.OKbtn_Click);
            // 
            // canselBtn
            // 
            this.canselBtn.Location = new System.Drawing.Point(148, 96);
            this.canselBtn.Name = "canselBtn";
            this.canselBtn.Size = new System.Drawing.Size(99, 24);
            this.canselBtn.TabIndex = 3;
            this.canselBtn.Text = "Cancel";
            this.canselBtn.UseVisualStyleBackColor = true;
            this.canselBtn.Click += new System.EventHandler(this.canselBtn_Click);
            // 
            // EnterNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 135);
            this.Controls.Add(this.canselBtn);
            this.Controls.Add(this.OKbtn);
            this.Controls.Add(this.nameTB);
            this.Controls.Add(this.enterNameLbl);
            this.Name = "EnterNameForm";
            this.Text = "Enter Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label enterNameLbl;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.Button OKbtn;
        private System.Windows.Forms.Button canselBtn;
    }
}
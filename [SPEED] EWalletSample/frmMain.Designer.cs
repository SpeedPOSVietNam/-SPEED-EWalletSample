namespace _SPEED__EWalletSample
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.wbQRCode = new System.Windows.Forms.WebBrowser();
            this.cbWallet = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtTransact = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescript = new System.Windows.Forms.TextBox();
            this.btnGenQr = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRequest = new System.Windows.Forms.TextBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.bwCreateQRCode = new System.ComponentModel.BackgroundWorker();
            this.tmrCountDown = new System.Windows.Forms.Timer(this.components);
            this.txtStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // wbQRCode
            // 
            this.wbQRCode.IsWebBrowserContextMenuEnabled = false;
            this.wbQRCode.Location = new System.Drawing.Point(348, 9);
            this.wbQRCode.Margin = new System.Windows.Forms.Padding(4);
            this.wbQRCode.MinimumSize = new System.Drawing.Size(27, 25);
            this.wbQRCode.Name = "wbQRCode";
            this.wbQRCode.ScriptErrorsSuppressed = true;
            this.wbQRCode.ScrollBarsEnabled = false;
            this.wbQRCode.Size = new System.Drawing.Size(200, 200);
            this.wbQRCode.TabIndex = 1;
            this.wbQRCode.WebBrowserShortcutsEnabled = false;
            // 
            // cbWallet
            // 
            this.cbWallet.FormattingEnabled = true;
            this.cbWallet.Location = new System.Drawing.Point(95, 9);
            this.cbWallet.Name = "cbWallet";
            this.cbWallet.Size = new System.Drawing.Size(228, 24);
            this.cbWallet.TabIndex = 2;
            this.cbWallet.SelectedIndexChanged += new System.EventHandler(this.cbWallet_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "E-Wallet";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Amount";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(95, 48);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(228, 22);
            this.txtAmount.TabIndex = 5;
            this.txtAmount.Text = "10000";
            // 
            // txtTransact
            // 
            this.txtTransact.Location = new System.Drawing.Point(95, 83);
            this.txtTransact.Name = "txtTransact";
            this.txtTransact.Size = new System.Drawing.Size(119, 22);
            this.txtTransact.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Transact";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Description";
            // 
            // txtDescript
            // 
            this.txtDescript.Location = new System.Drawing.Point(95, 123);
            this.txtDescript.Name = "txtDescript";
            this.txtDescript.Size = new System.Drawing.Size(228, 22);
            this.txtDescript.TabIndex = 9;
            // 
            // btnGenQr
            // 
            this.btnGenQr.Location = new System.Drawing.Point(13, 163);
            this.btnGenQr.Name = "btnGenQr";
            this.btnGenQr.Size = new System.Drawing.Size(310, 46);
            this.btnGenQr.TabIndex = 11;
            this.btnGenQr.Text = "GENERATE";
            this.btnGenQr.UseVisualStyleBackColor = true;
            this.btnGenQr.Click += new System.EventHandler(this.btnGenQr_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Request";
            // 
            // txtRequest
            // 
            this.txtRequest.Location = new System.Drawing.Point(95, 252);
            this.txtRequest.Multiline = true;
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.Size = new System.Drawing.Size(473, 109);
            this.txtRequest.TabIndex = 13;
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(95, 373);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(473, 109);
            this.txtResponse.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 373);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Response";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(220, 83);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Auto Gen";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tmrCountDown
            // 
            this.tmrCountDown.Interval = 1000;
            this.tmrCountDown.Tick += new System.EventHandler(this.tmrCountDown_Tick);
            // 
            // txtStatus
            // 
            this.txtStatus.AutoSize = true;
            this.txtStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtStatus.Location = new System.Drawing.Point(327, 213);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(241, 17);
            this.txtStatus.TabIndex = 17;
            this.txtStatus.Text = "Transactions will be checked after 3s";
            this.txtStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtStatus.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 494);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtRequest);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnGenQr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescript);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTransact);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbWallet);
            this.Controls.Add(this.wbQRCode);
            this.Name = "frmMain";
            this.Text = "[SPEED] E-Wallet Sample";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbQRCode;
        private System.Windows.Forms.ComboBox cbWallet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtTransact;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescript;
        private System.Windows.Forms.Button btnGenQr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRequest;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker bwCreateQRCode;
        private System.Windows.Forms.Timer tmrCountDown;
        private System.Windows.Forms.Label txtStatus;
    }
}


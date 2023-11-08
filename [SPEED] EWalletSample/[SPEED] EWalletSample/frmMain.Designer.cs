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
            this.btnGenQr = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRequest = new System.Windows.Forms.TextBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.bwCreateQRCode = new System.ComponentModel.BackgroundWorker();
            this.tmrCountDown = new System.Windows.Forms.Timer(this.components);
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnRefund = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // wbQRCode
            // 
            this.wbQRCode.IsWebBrowserContextMenuEnabled = false;
            this.wbQRCode.Location = new System.Drawing.Point(457, 9);
            this.wbQRCode.Margin = new System.Windows.Forms.Padding(4);
            this.wbQRCode.MinimumSize = new System.Drawing.Size(27, 25);
            this.wbQRCode.Name = "wbQRCode";
            this.wbQRCode.ScriptErrorsSuppressed = true;
            this.wbQRCode.ScrollBarsEnabled = false;
            this.wbQRCode.Size = new System.Drawing.Size(200, 188);
            this.wbQRCode.TabIndex = 1;
            this.wbQRCode.WebBrowserShortcutsEnabled = false;
            // 
            // cbWallet
            // 
            this.cbWallet.FormattingEnabled = true;
            this.cbWallet.Location = new System.Drawing.Point(95, 9);
            this.cbWallet.Name = "cbWallet";
            this.cbWallet.Size = new System.Drawing.Size(321, 24);
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
            this.txtAmount.Size = new System.Drawing.Size(321, 22);
            this.txtAmount.TabIndex = 5;
            this.txtAmount.Text = "10000";
            // 
            // txtTransact
            // 
            this.txtTransact.Location = new System.Drawing.Point(95, 83);
            this.txtTransact.Name = "txtTransact";
            this.txtTransact.Size = new System.Drawing.Size(205, 22);
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
            // btnGenQr
            // 
            this.btnGenQr.Location = new System.Drawing.Point(20, 130);
            this.btnGenQr.Name = "btnGenQr";
            this.btnGenQr.Size = new System.Drawing.Size(119, 46);
            this.btnGenQr.TabIndex = 11;
            this.btnGenQr.Text = "GENERATE";
            this.btnGenQr.UseVisualStyleBackColor = true;
            this.btnGenQr.Click += new System.EventHandler(this.btnGenQr_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 350);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Request";
            // 
            // txtRequest
            // 
            this.txtRequest.Location = new System.Drawing.Point(95, 350);
            this.txtRequest.Multiline = true;
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.Size = new System.Drawing.Size(562, 116);
            this.txtRequest.TabIndex = 13;
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(95, 483);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(562, 122);
            this.txtResponse.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 483);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Response";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(306, 82);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Auto Gen";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(286, 130);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(130, 46);
            this.btnCheck.TabIndex = 18;
            this.btnCheck.Text = "CHECK STATUS";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnRefund
            // 
            this.btnRefund.Location = new System.Drawing.Point(145, 130);
            this.btnRefund.Name = "btnRefund";
            this.btnRefund.Size = new System.Drawing.Size(135, 46);
            this.btnRefund.TabIndex = 19;
            this.btnRefund.Text = "REFUND";
            this.btnRefund.UseVisualStyleBackColor = true;
            this.btnRefund.Click += new System.EventHandler(this.btnRefund_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(95, 218);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(562, 116);
            this.txtStatus.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "IPN";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 614);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRefund);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtRequest);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnGenQr);
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
        private System.Windows.Forms.Button btnGenQr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRequest;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker bwCreateQRCode;
        private System.Windows.Forms.Timer tmrCountDown;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnRefund;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label4;
    }
}


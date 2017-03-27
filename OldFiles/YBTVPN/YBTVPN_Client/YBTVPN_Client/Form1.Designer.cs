namespace YBTVPN_Client
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.txt_buffer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(12, 12);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(100, 21);
            this.txt_ip.TabIndex = 2;
            this.txt_ip.Text = "192.168.1.117";
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(12, 39);
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(100, 21);
            this.txt_port.TabIndex = 3;
            this.txt_port.Text = "52145";
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(12, 66);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(75, 23);
            this.btn_Connect.TabIndex = 4;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.Btn_Connect_Click);
            // 
            // txt_buffer
            // 
            this.txt_buffer.Location = new System.Drawing.Point(118, 12);
            this.txt_buffer.Name = "txt_buffer";
            this.txt_buffer.Size = new System.Drawing.Size(100, 21);
            this.txt_buffer.TabIndex = 5;
            this.txt_buffer.Text = "4096";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.txt_buffer);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.txt_port);
            this.Controls.Add(this.txt_ip);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.TextBox txt_buffer;
    }
}


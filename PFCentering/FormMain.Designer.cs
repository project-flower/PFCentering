namespace PFCentering
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonExec = new System.Windows.Forms.Button();
            this.buttonOneMore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonExec
            // 
            this.buttonExec.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonExec.Location = new System.Drawing.Point(12, 12);
            this.buttonExec.Name = "buttonExec";
            this.buttonExec.Size = new System.Drawing.Size(150, 46);
            this.buttonExec.TabIndex = 0;
            this.buttonExec.Text = "&Centering";
            this.buttonExec.UseVisualStyleBackColor = true;
            this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
            // 
            // buttonOneMore
            // 
            this.buttonOneMore.Location = new System.Drawing.Point(168, 12);
            this.buttonOneMore.Name = "buttonOneMore";
            this.buttonOneMore.Size = new System.Drawing.Size(75, 23);
            this.buttonOneMore.TabIndex = 1;
            this.buttonOneMore.Text = "&One More";
            this.buttonOneMore.UseVisualStyleBackColor = true;
            this.buttonOneMore.Click += new System.EventHandler(this.buttonOneMore_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 70);
            this.Controls.Add(this.buttonOneMore);
            this.Controls.Add(this.buttonExec);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "PFCentering";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonExec;
        private System.Windows.Forms.Button buttonOneMore;
    }
}


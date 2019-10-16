namespace MMY_UserControlDemo
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cNavigationBar1 = new MMY_UserControlDemo.Controls.CNavigationBar();
            this.cTreeView1 = new MMY_UserControlDemo.Controls.CTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // cNavigationBar1
            // 
            this.cNavigationBar1.AutoScroll = true;
            this.cNavigationBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(39)))));
            this.cNavigationBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.cNavigationBar1.ForeColor = System.Drawing.Color.White;
            this.cNavigationBar1.Location = new System.Drawing.Point(0, 0);
            this.cNavigationBar1.MenuItems = null;
            this.cNavigationBar1.Name = "cNavigationBar1";
            this.cNavigationBar1.Size = new System.Drawing.Size(170, 570);
            this.cNavigationBar1.TabIndex = 0;
            // 
            // cTreeView1
            // 
            this.cTreeView1.AutoScroll = true;
            this.cTreeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(39)))));
            this.cTreeView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.cTreeView1.ForeColor = System.Drawing.Color.White;
            this.cTreeView1.IsParentChangeState = false;
            this.cTreeView1.IsShowIcon = true;
            this.cTreeView1.Items = null;
            this.cTreeView1.Location = new System.Drawing.Point(781, 0);
            this.cTreeView1.Name = "cTreeView1";
            this.cTreeView1.Size = new System.Drawing.Size(170, 570);
            this.cTreeView1.TabIndex = 1;
            this.cTreeView1.Tag = "main";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "query_16x.png");
            this.imageList1.Images.SetKeyName(1, "Check.png");
            this.imageList1.Images.SetKeyName(2, "cloud.png");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 570);
            this.Controls.Add(this.cTreeView1);
            this.Controls.Add(this.cNavigationBar1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CNavigationBar cNavigationBar1;
        private Controls.CTreeView cTreeView1;
        private System.Windows.Forms.ImageList imageList1;
    }
}


namespace PocketWeather
{
    partial class Form3
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.t_loc = new System.Windows.Forms.TextBox();
            this.t_server = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.degree_c = new System.Windows.Forms.ComboBox();
            this.wind_c = new System.Windows.Forms.ComboBox();
            this.goback_b = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "保存";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "取消";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 49);
            this.label1.Text = "城市检索:\r\n(格式为\"[城市拼音],CHN\",例如北京就是\"beijing,CHN\")";
            // 
            // t_loc
            // 
            this.t_loc.Location = new System.Drawing.Point(3, 63);
            this.t_loc.Name = "t_loc";
            this.t_loc.Size = new System.Drawing.Size(216, 21);
            this.t_loc.TabIndex = 1;
            // 
            // t_server
            // 
            this.t_server.Location = new System.Drawing.Point(3, 108);
            this.t_server.Name = "t_server";
            this.t_server.Size = new System.Drawing.Size(216, 21);
            this.t_server.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(3, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 18);
            this.label2.Text = "WebServices URL:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(3, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 18);
            this.label3.Text = "温度显示格式:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(3, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(216, 18);
            this.label4.Text = "风速显示格式:";
            // 
            // degree_c
            // 
            this.degree_c.Items.Add("以摄氏度显示(℃)");
            this.degree_c.Items.Add("以华氏度显示(℉)");
            this.degree_c.Location = new System.Drawing.Point(3, 153);
            this.degree_c.Name = "degree_c";
            this.degree_c.Size = new System.Drawing.Size(216, 22);
            this.degree_c.TabIndex = 22;
            // 
            // wind_c
            // 
            this.wind_c.Items.Add("以米每秒显示(m/s)");
            this.wind_c.Items.Add("以英里每时显示(mph)");
            this.wind_c.Location = new System.Drawing.Point(3, 199);
            this.wind_c.Name = "wind_c";
            this.wind_c.Size = new System.Drawing.Size(216, 22);
            this.wind_c.TabIndex = 23;
            // 
            // goback_b
            // 
            this.goback_b.Location = new System.Drawing.Point(147, 236);
            this.goback_b.Name = "goback_b";
            this.goback_b.Size = new System.Drawing.Size(72, 20);
            this.goback_b.TabIndex = 28;
            this.goback_b.Text = "重置";
            this.goback_b.Click += new System.EventHandler(this.goback_b_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.goback_b);
            this.Controls.Add(this.wind_c);
            this.Controls.Add(this.degree_c);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.t_server);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.t_loc);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form3";
            this.Text = "设置";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form3_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox t_loc;
        private System.Windows.Forms.TextBox t_server;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox degree_c;
        private System.Windows.Forms.ComboBox wind_c;
        private System.Windows.Forms.Button goback_b;
    }
}
using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Reflection;

namespace PocketWeather
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            int is_beta = 0;
            Image icon = Properties.Resources.icon;//显示图标
            pictureBox1.Image = icon;

            Assembly assembly = Assembly.GetExecutingAssembly();//获取版本号
            Version version = assembly.GetName().Version;
            ver_num.Text = "" + version;
            switch (is_beta)
            {
                case 1:
                    beta_l.Text = "Beta";
                    beta_l.ForeColor = Color.Orange;
                    break;
                case 2:
                    beta_l.Text = "Preview";
                    beta_l.ForeColor = Color.MediumAquamarine;
                    break;
                case 3:
                    beta_l.Text = "Pre-RTM";
                    beta_l.ForeColor = Color.SlateBlue;
                    break;
                default:
                    beta_l.Text = "";
                    beta_l.ForeColor = Color.Black;
                    break;
            }
        }
    }
}
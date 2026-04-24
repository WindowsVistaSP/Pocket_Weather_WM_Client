using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.IO;

using System.Xml;

namespace PocketWeather
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }


        public static string loc_path = juedui_path() + "loc.txt";
        public static string server_path = juedui_path() + "server.txt";
        public static string degree_path = juedui_path() + "degree.txt";
        public static string wind_path = juedui_path() + "wind.txt";
        public static string loc = "beijing,CHN", server = "http://weather.winvistasp.top:1145/Service1.asmx", degree = "C", wind = "ms";//默认设置

        static string juedui_path()//绝对路径
        {
            string yuanlaide_path = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            char[] split_char = { '\\' };//分割接受的信息
            string[] path_split = new string[100];

            path_split = yuanlaide_path.Split(split_char);
            string houlaide_path = "";

            for (int i = 0; i < path_split.Length - 1; i++)
            {
                houlaide_path += path_split[i] + "\\";
            }
            return houlaide_path;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        

        private void Save_data()//保存文件
        {
            if (!((t_loc.Text == "")&&(t_server.Text=="")))
            {
            loc = t_loc.Text;
            server = t_server.Text;
            if (degree_c.SelectedIndex == 0)
            {
                degree = "C";
            }
            else
            {
                degree = "F";
            }

            if (wind_c.SelectedIndex == 0)
            {
                wind = "ms";
            }
            else
            {
                wind = "mph";
            }

            try
            {
                StreamWriter writer_loc = new StreamWriter(loc_path);
                StreamWriter writer_server = new StreamWriter(server_path);
                StreamWriter writer_degree = new StreamWriter(degree_path);
                StreamWriter writer_wind = new StreamWriter(wind_path);

                writer_loc.Write(loc);
                writer_server.Write(server);
                writer_degree.Write(degree);
                writer_wind.Write(wind);

                writer_loc.Close();
                writer_server.Close();
                writer_degree.Close();
                writer_wind.Close();
            }
            catch
            {
                MessageBox.Show("创建文件时发生错误");
            }


            this.Close();
            }
            else
            {
                MessageBox.Show("内容不能为空");
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Save_data();
            
        }
        private void Form3_Closing(object sender, CancelEventArgs e)
        {
            Save_data();
        }

        private void Form3_Load(object sender, EventArgs e)
        {


            if ((File.Exists(loc_path)) && (File.Exists(server_path)) && (File.Exists(degree_path)) && (File.Exists(wind_path)))//判断文件是否存在
            {
                StreamReader reader_loc = new StreamReader(loc_path);
                StreamReader reader_server = new StreamReader(server_path);
                StreamReader reader_degree = new StreamReader(degree_path);
                StreamReader reader_wind = new StreamReader(wind_path);

                loc = reader_loc.ReadToEnd();
                server = reader_server.ReadToEnd();
                degree = reader_degree.ReadToEnd();
                wind = reader_wind.ReadToEnd();

                reader_loc.Close();
                reader_server.Close();
                reader_degree.Close();
                reader_wind.Close();


                
            }
            else
            {
                StreamWriter writer_loc = new StreamWriter(loc_path);
                StreamWriter writer_server = new StreamWriter(server_path);
                StreamWriter writer_degree = new StreamWriter(degree_path);
                StreamWriter writer_wind = new StreamWriter(wind_path);

                writer_loc.Write(loc);
                writer_server.Write(server);
                writer_degree.Write(degree);
                writer_wind.Write(wind);

                writer_loc.Close();
                writer_server.Close();
                writer_degree.Close();
                writer_wind.Close();
            }

            t_loc.Text = loc;
            t_server.Text = server;

            if (degree == "C")
            {
                degree_c.SelectedIndex = 0;
            }
            else
            {
                degree_c.SelectedIndex = 1;
            }

            if (wind == "ms")
            {
                wind_c.SelectedIndex = 0;
            }
            else
            {
                wind_c.SelectedIndex = 1;
            }



        }

        private void goback_b_Click(object sender, EventArgs e)
        {
            t_loc.Text = "beijing,CHN";
            t_server.Text = "http://weather.winvistasp.top:1145/Service1.asmx";
            degree_c.SelectedIndex = 0;
            wind_c.SelectedIndex = 0;
        }








    }
}
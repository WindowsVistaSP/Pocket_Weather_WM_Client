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

using System.Threading;

namespace PocketWeather
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Thread th_get_weather;
        private bool is_stop = true;

        public static string loc_path = juedui_path() + "loc.txt";
        public static string server_path = juedui_path() + "server.txt";
        public static string degree_path = juedui_path() + "degree.txt";
        public static string wind_path = juedui_path() + "wind.txt";
        public static string loc = "beijing,CHN", server = "http://weather.winvistasp.top:1145/Service1.asmx", degree = "C", wind = "ms";//默认设置

        private void Start_get()
        {
            th_get_weather = new Thread(Get_weather);
            th_get_weather.Start();
            is_stop = false;
        }

        private void Stop_get()
        {
            th_get_weather.Abort();
            update_zhuangtai("已停止", 4);
            is_stop = true;
        }

        static string F_to_C(double F, string type)//摄氏度转换为华氏度
        {
            if (type == "C")
            {
                double C = Math.Round((F - 32) / 1.8, 0);
                return C + "℃";
            }
            else
            {
                return F + "℉";
            }

        }

        static string juedui_path()//绝对路径
        {
            string yuanlaide_path = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            char[] split_char = { '\\' };//分割接受的信息
            string[] path_split = new string[100];

            path_split = yuanlaide_path.Split(split_char);
            string houlaide_path = "";

            for (int i = 0; i < path_split.Length-1; i++)
            {
                houlaide_path += path_split[i]+"\\";
            }
            return houlaide_path;
        }

        static string mph_to_ms(double mph, string type)//英里每小时转换为米每秒
        {
            
            if (type == "ms")
            {
                double ms = Math.Round(mph * 0.44704, 0);
                return ms + "m/s";
            }
            else
            {
                return mph + "mph";
            }
        }

        private void Get_weather()
        {

            try
            {
                start_frm();
                update_zhuangtai("正在读取配置文件中...", 1);

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

                update_zhuangtai("正在接收天气信息中...", 1);

                get_weather_url.Service1 ws = new get_weather_url.Service1();
                ws.Url = server;
                string weather_inf = ws.Get_weather(loc);

                update_zhuangtai("正在更新窗体控件中...", 1);

                update_frm(weather_inf);

                


            }
            catch
            {
                if (!is_stop)
                {
                    update_zhuangtai("更新失败", 3);
                    menuItem2_zhuangtai();
                    th_get_weather.Abort();
                }
            }

        }

        public delegate void MethodInvoker();

        private void update_frm(string weather_inf)
        {
            if (weather_inf != "e")
            {



                char[] split_char = { '&' };//分割接受的信息
                string[] weather_split = new string[100];

                weather_split = weather_inf.Split(split_char);



                //处理数据
                BeginInvoke(new MethodInvoker(delegate()
                {

                    Image _0 = Properties.Resources._0;//图像
                    Image _1 = Properties.Resources._1;
                    Image _2 = Properties.Resources._2;
                    Image _3 = Properties.Resources._3;
                    Image _4 = Properties.Resources._4;
                    Image _5 = Properties.Resources._5;
                    Image _6 = Properties.Resources._6;
                    Image _7 = Properties.Resources._7;
                    Image _8 = Properties.Resources._8;
                    Image _9 = Properties.Resources._9;

                    string[] tianqi_name = new string[] { "阴", "小雨", "雾", "多云", "雪", "雷雨", "晴", "风", "大雨", "冰雹" };
                    Image[] tianqi_pic = new Image[] { _0, _1, _2, _3, _4, _5, _6, _7, _8, _9 };

                    b_wendu.Text = F_to_C(double.Parse(weather_split[1]), degree);//更改控件信息
                    b_shidu.Text = weather_split[2] + "%";
                    b_tianqi.Text = tianqi_name[int.Parse(weather_split[0])];
                    b_pic.Image = tianqi_pic[int.Parse(weather_split[0])];


                    char[] split_char_wind = { ' ' };//分割接受的信息
                    string[] wind_split = new string[10];

                    wind_split = weather_split[3].Split(split_char_wind);


                    b_fengsu.Text = mph_to_ms(double.Parse(wind_split[0]), wind);
                    string fengxiang_temp = "---";
                    if (wind_split.Length >= 2)
                    {
                        switch (wind_split[2])
                        {
                            case "Northwest":
                                fengxiang_temp = "西北风";
                                break;
                            case "North":
                                fengxiang_temp = "北风";
                                break;
                            case "Northeast":
                                fengxiang_temp = "东北风";
                                break;
                            case "West":
                                fengxiang_temp = "西风";
                                break;
                            case "East":
                                fengxiang_temp = "东风";
                                break;
                            case "Southwest":
                                fengxiang_temp = "西南风";
                                break;
                            case "South":
                                fengxiang_temp = "南风";
                                break;
                            case "Southeast":
                                fengxiang_temp = "东南风";
                                break;

                        }
                    }
                    b_fengxiang.Text = fengxiang_temp;


                    s_date_1.Text = weather_split[7];
                    s_wendu_l_1.Text = F_to_C(double.Parse(weather_split[5]), degree);
                    s_wendu_h_1.Text = F_to_C(double.Parse(weather_split[6]), degree);
                    s_tianqi_1.Text = tianqi_name[int.Parse(weather_split[4])];
                    s_pic_1.Image = tianqi_pic[int.Parse(weather_split[4])];

                    s_date_2.Text = weather_split[11];
                    s_wendu_l_2.Text = F_to_C(double.Parse(weather_split[9]), degree);
                    s_wendu_h_2.Text = F_to_C(double.Parse(weather_split[10]), degree);
                    s_tianqi_2.Text = tianqi_name[int.Parse(weather_split[8])];
                    s_pic_2.Image = tianqi_pic[int.Parse(weather_split[8])];

                    s_date_3.Text = weather_split[15];
                    s_wendu_l_3.Text = F_to_C(double.Parse(weather_split[13]), degree);
                    s_wendu_h_3.Text = F_to_C(double.Parse(weather_split[14]), degree);
                    s_tianqi_3.Text = tianqi_name[int.Parse(weather_split[12])];
                    s_pic_3.Image = tianqi_pic[int.Parse(weather_split[12])];

                    s_date_4.Text = weather_split[19];
                    s_wendu_l_4.Text = F_to_C(double.Parse(weather_split[17]), degree);
                    s_wendu_h_4.Text = F_to_C(double.Parse(weather_split[18]), degree);
                    s_tianqi_4.Text = tianqi_name[int.Parse(weather_split[16])];
                    s_pic_4.Image = tianqi_pic[int.Parse(weather_split[16])];

                    s_date_5.Text = weather_split[23];
                    s_wendu_l_5.Text = F_to_C(double.Parse(weather_split[21]), degree);
                    s_wendu_h_5.Text = F_to_C(double.Parse(weather_split[22]), degree);
                    s_tianqi_5.Text = tianqi_name[int.Parse(weather_split[20])];
                    s_pic_5.Image = tianqi_pic[int.Parse(weather_split[20])];


                    b_loc.Text = weather_split[24] + "";

                    Refresh();//刷新所有控件


                    update_zhuangtai("更新成功！", 2);
                    menuItem2_zhuangtai();

                }));
            }

        }


        private void start_frm()
        {
            
                BeginInvoke(new MethodInvoker(delegate()
                {
                    string start_s = "---";
                    Image start_p = Properties.Resources._e;//图像

                    b_wendu.Text = start_s;//更改控件信息
                    b_shidu.Text = start_s;
                    b_tianqi.Text = start_s;
                    b_pic.Image = start_p;




                    b_fengsu.Text = start_s;
                    b_fengxiang.Text = start_s;


                    s_date_1.Text = start_s;
                    s_wendu_l_1.Text = start_s;
                    s_wendu_h_1.Text = start_s;
                    s_tianqi_1.Text = start_s;
                    s_pic_1.Image = start_p;

                    s_date_2.Text = start_s;
                    s_wendu_l_2.Text = start_s;
                    s_wendu_h_2.Text = start_s;
                    s_tianqi_2.Text = start_s;
                    s_pic_2.Image = start_p;

                    s_date_3.Text = start_s;
                    s_wendu_l_3.Text = start_s;
                    s_wendu_h_3.Text = start_s;
                    s_tianqi_3.Text = start_s;
                    s_pic_3.Image = start_p;

                    s_date_4.Text = start_s;
                    s_wendu_l_4.Text = start_s;
                    s_wendu_h_4.Text = start_s;
                    s_tianqi_4.Text = start_s;
                    s_pic_4.Image = start_p;

                    s_date_5.Text = start_s;
                    s_wendu_l_5.Text = start_s;
                    s_wendu_h_5.Text = start_s;
                    s_tianqi_5.Text = start_s;
                    s_pic_5.Image = start_p;


                    b_loc.Text = start_s;

                    Refresh();//刷新所有控件

                }));

        }



        private void update_zhuangtai(string zhuangtai_s, int zhuangtai_i)
        {
            BeginInvoke(new MethodInvoker(delegate()
            {

                zhuangtai_l.Text = zhuangtai_s;

                switch (zhuangtai_i)
                {
                    case 1:
                        zhuangtai_l.ForeColor = Color.Orange;
                        break;
                    case 2:
                        zhuangtai_l.ForeColor = Color.LimeGreen;
                        break;
                    case 3:
                        zhuangtai_l.ForeColor = Color.Red;
                        break;
                    case 4:
                        zhuangtai_l.ForeColor = Color.Teal;
                        break;
                    default:
                        zhuangtai_l.ForeColor = Color.LimeGreen;
                        break;

                }

            }));

        }

        private void menuItem2_zhuangtai()
        {
            BeginInvoke(new MethodInvoker(delegate()
            {
                if (menuItem2.Text == "停止")
                {
                    menuItem2.Text = "刷新";
                    
                }
                else if (menuItem2.Text == "刷新")
                {
                    menuItem2.Text = "停止";
                    
                }

                

            }));

        }


        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (menuItem2.Text == "停止")
            {
                menuItem2.Text = "刷新";
                Stop_get();
            }
            else if (menuItem2.Text == "刷新")
            {
                menuItem2.Text = "停止";
                Start_get();
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Start_get();
            menuItem2_zhuangtai();
        }







        private void menuItem3_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();//打开关于窗口
            frm2.Show();
        }



        private void menuItem4_Click_1(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();//打开设置窗口
            frm3.Show();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            Application.Exit();//退出
        }








    }
}
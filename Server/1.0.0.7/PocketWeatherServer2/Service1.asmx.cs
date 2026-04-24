using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

using System.Net;
using System.Net.Sockets;
using System.IO;

using System.Xml;

using System.Reflection;

namespace PocketWeatherServer2
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Service1 : System.Web.Services.WebService
    {


        public static string xml_path = juedui_path() + "data.xml", log_path = juedui_path() + "data.log", txt_path = juedui_path() + "setphp.txt", php_url = "http://test.winvistasp.top/index.php?loc=";

        static string juedui_path()//绝对路径
        {
            string yuanlaide_path = AppDomain.CurrentDomain.BaseDirectory;
            return yuanlaide_path;
        }

        static void save_log(string mes_log)//保存log文件
        {
            string put_log = "[" + DateTime.Now.ToString() + "]:" + mes_log;
            StreamWriter writer_setphp = new StreamWriter(log_path, true);
            writer_setphp.WriteLine(put_log);
            writer_setphp.Close();
        }


        [WebMethod]
        public string Get_weather(string loc)//获取天气
        {


            try
            {


                if (File.Exists(txt_path))
                {
                    StreamReader reader_setphp = new StreamReader(txt_path);
                    php_url = reader_setphp.ReadToEnd();
                    reader_setphp.Close();
                }
                else
                {
                    StreamWriter writer_setphp = new StreamWriter(txt_path);
                    writer_setphp.Write(php_url);
                    writer_setphp.Close();
                }

                string url = php_url + loc;

                string return_value = "e";



                save_log("有客户端接入，正在下载xml文件...");
                var client = new WebClient();
                client.DownloadFile(url, xml_path);




                save_log("下载成功，正在读取xml文件...");

                XmlDocument xml_doc = new XmlDocument();
                xml_doc.Load(xml_path); // 加载XML文件

                XmlNodeList currentNodes = xml_doc.SelectNodes("//current");//读取xml文件
                XmlNodeList forecastNodes = xml_doc.SelectNodes("//forecast");
                XmlNodeList weatherNodes = xml_doc.SelectNodes("//weather");
                int b_skycode = 0, b_humidity = 0;
                string b_winddisplay = "0";
                double b_temperature = 0;
                double b_temperature_temp;
                foreach (XmlNode xml_node in currentNodes)//读取当前天气
                {
                    XmlAttribute skycode_Attr = xml_node.Attributes["skycode"];
                    XmlAttribute temperature_Attr = xml_node.Attributes["temperature"];
                    XmlAttribute humidity_Attr = xml_node.Attributes["humidity"];
                    XmlAttribute winddisplay_Attr = xml_node.Attributes["winddisplay"];

                    if (skycode_Attr != null)
                    {
                        b_skycode = Get_tianqi(int.Parse(skycode_Attr.Value));
                    }
                    if (temperature_Attr != null)
                    {
                        b_temperature_temp = double.Parse(temperature_Attr.Value);
                        b_temperature = b_temperature_temp;
                    }
                    if (humidity_Attr != null)
                    {
                        b_humidity = int.Parse(humidity_Attr.Value);
                    }
                    if (winddisplay_Attr != null)
                    {
                        b_winddisplay = winddisplay_Attr.Value;
                    }
                }

                int[] s_skycode = new int[] { 0, 0, 0, 0, 0 };
                double[] s_low = new double[] { 0, 0, 0, 0, 0 };
                double[] s_high = new double[] { 0, 0, 0, 0, 0 };
                string[] s_date = new string[] { "0", "0", "0", "0", "0" };
                int s_i = 0;
                string s_date_temp;
                double high_temp, low_temp;
                char[] split_char = { '-' };
                string[] s_date_split = new string[2];

                foreach (XmlNode xml_node in forecastNodes)//读取5日天气
                {
                    XmlAttribute skycode_Attr = xml_node.Attributes["skycodeday"];
                    XmlAttribute low_Attr = xml_node.Attributes["low"];
                    XmlAttribute high_Attr = xml_node.Attributes["high"];
                    XmlAttribute date_Attr = xml_node.Attributes["date"];
                    if (s_i < 5)
                    {
                        if (skycode_Attr != null)
                        {
                            s_skycode[s_i] = Get_tianqi(int.Parse(skycode_Attr.Value));
                        }
                        if (low_Attr != null)
                        {
                            low_temp = double.Parse(low_Attr.Value);
                            s_low[s_i] = low_temp;
                        }
                        if (high_Attr != null)
                        {
                            high_temp = double.Parse(high_Attr.Value);
                            s_high[s_i] = high_temp;
                        }
                        if (date_Attr != null)
                        {
                            s_date_temp = date_Attr.Value;
                            s_date_split = s_date_temp.Split(split_char);
                            s_date[s_i] = s_date_split[1] + "-" + s_date_split[2];

                        }
                    }
                    s_i++;
                }


                string b_location = "0";

                foreach (XmlNode xml_node in weatherNodes)//读取地址
                {
                    XmlAttribute locatio_Attr = xml_node.Attributes["weatherlocationname"];
                    if (locatio_Attr != null)
                    {
                        b_location = locatio_Attr.Value;
                    }
                }


                return_value = b_skycode + "&" + b_temperature + "&" + b_humidity + "&" + b_winddisplay;



                for (int i = 0; i < 5; i++)
                {
                    return_value += "&" + s_skycode[i] + "&" + s_low[i] + "&" + s_high[i] + "&" + s_date[i];
                }

                return_value += "&" + b_location;


                save_log("读取成功，正在发送天气信息...");

                return return_value;


            }
            catch (Exception e)
            {
                save_log("出现错误，错误信息："+e);
                return "e" + e;
            }
        }



        static int Get_tianqi(int input)//根据天气代码判断天气
        {
            int output;
            switch (input)
            {
                case 26:
                case 27:
                case 28:

                    output = 0;

                    break;

                case 35:
                case 39:
                case 45:
                case 46:

                    output = 1;

                    break;

                case 19:
                case 20:
                case 21:
                case 22:

                    output = 2;

                    break;

                case 29:
                case 30:
                case 33:

                    output = 3;

                    break;

                case 5:
                case 13:
                case 14:
                case 15:
                case 16:
                case 18:
                case 25:
                case 41:
                case 42:
                case 43:

                    output = 4;

                    break;

                case 1:
                case 2:
                case 3:
                case 4:
                case 37:
                case 38:
                case 47:

                    output = 5;

                    break;

                case 31:
                case 32:
                case 34:
                case 36:
                case 44://44表示Data Not Available，如果有需求可以删掉

                    output = 6;

                    break;

                case 23:
                case 24:

                    output = 7;

                    break;

                case 9:
                case 10:
                case 11:
                case 12:
                case 40:

                    output = 8;

                    break;

                case 6:
                case 7:
                case 8:
                case 17:

                    output = 9;

                    break;
                default:
                    output = 6;
                    break;

            }
            return output;
        }










    }
}

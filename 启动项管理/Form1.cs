using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace 启动项管理
{
    public partial class Form1 : Form
    {
        private int sCount = 10, voiceNum = 3;
        private string wifiOpen = "", wifiStop = "";
        private bool voice = false, contain = true, Close = false;
        private DateTime time1, time2;
        private bool isOpen = true;
        public Form1()
        {
            InitializeComponent();
            Mute();
            //SetVol();//关闭声音
            //getWindowsInfo();
            黑名单时进行的操作事件 += 黑名单时进行的操作;
            if (ConfigurationManager.AppSettings.Count > 0)
            {
                time1 = DateTime.Now;
                label1.Text = sCount + "秒内点击确定则终止启动项!";
                timer1.Interval = 1000;
                timer1.Tick += timer1_Tick1;
                timer1.Start();
                loadSetting();
            }
        }

        public delegate void 黑名单时进行的操作委托(string 节点名);

        private event 黑名单时进行的操作委托 黑名单时进行的操作事件 = null;

        void 黑名单时进行的操作(string 节点名)
        {
            int i = 0;
            try
            {
                while (true)
                {
                    string val = INI.ReadINI(节点名, 节点名 + "_" + i++);
                    if (string.IsNullOrEmpty(val)) break;
                    try
                    {
                        Process.Start(val);

                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
            catch (Exception e)
            {

            }
            timer1.Stop();
            VolumeUp();
            VolumeUp();
            VolumeUp();
            VolumeUp();
        }

        private void loadSetting()
        {
            var aa = new CfgHelper("sCount");
            try
            {
                sCount = int.Parse(aa.Value); //定时
            }
            catch (Exception exception)
            {
            }
            try
            {
                wifiOpen = new CfgHelper("wifiOpen").Value; //白名单 wifi
            }
            catch (Exception e)
            {
            }
            try
            {
                wifiStop = new CfgHelper("wifiStop").Value; //黑名单 wifi
            }
            catch (Exception e)
            {
            } // voice = false, contain = true;
            try
            {
                contain = bool.Parse(new CfgHelper("contain").Value); // wifi包含名称时起作用还是连接时
            }
            catch (Exception e)
            {
            }
            if (!string.IsNullOrEmpty(wifiOpen) || !string.IsNullOrEmpty(wifiStop))
            {
                string curWifiName = "";
                var wifis = Form2.getWifis(out curWifiName);
                if (contain)
                {
                    if (wifis.Contains(wifiOpen))
                    {
                        checkBox1.Enabled = false;
                        sCount = 0;
                        label1.Text = "wifi有白名单！立即启动";
                    }
                    else if (wifis.Contains(wifiStop))
                    {
                        timer1.Stop();
                        label1.Text = "wifi在黑名单！暂停启动";
                        if (Close) this.Close();
                        黑名单时进行的操作事件("黑名单启动项1");
                    }
                }else if (curWifiName == wifiOpen)
                {
                    checkBox1.Enabled = false;
                    sCount = 0;
                    label1.Text = "wifi有白名单！立即启动";
                }
                else if (curWifiName == wifiStop)
                {
                    timer1.Stop();
                    label1.Text = "wifi在黑名单！暂停启动";
                    if (Close) this.Close();
                    黑名单时进行的操作事件("黑名单启动项1");
                }
            }
            try
            {
                voice = bool.Parse(new CfgHelper("voice").Value); //音量
            }
            catch (Exception e)
            {
            }
            try
            {
                voiceNum = int.Parse(new CfgHelper("voiceNum").Value); //白名单 wifi
            }
            catch (Exception e)
            {
            }
            try
            {
                Close = bool.Parse(new CfgHelper("Close").Value); //音量
            }
            catch (Exception e)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            var fn = openFileDialog1.FileName;
            if (!string.IsNullOrEmpty(fn))
            {
                if (File.Exists(fn))
                {
                    string extName = Path.GetExtension(fn);
                    //var fn1 = fn.Substring(0, fn.Length - extName.Length);
                    var fn1 = fn.Substring(fn.LastIndexOf("\\") + 1);
                    var aa = new CfgHelper(fn1);
                    try
                    {
                        var old = aa.Value;
                        MessageBox.Show("已存在同名启动项，路径为：" + old);
                    }
                    catch (Exception exception)
                    {
                        aa.Value = fn;
                        MessageBox.Show("添加成功");
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
        private static void getWindowsInfo()
        {
            try
            {
                Process[] MyProcesses = Process.GetProcesses();
                string[] Minfo = new string[6];
                foreach (Process MyProcess in MyProcesses)
                {
                    if (MyProcess.MainWindowTitle.Length > 0)
                    {
                        Minfo[0] = MyProcess.MainWindowTitle;
                        Minfo[1] = MyProcess.Id.ToString();
                        Minfo[2] = MyProcess.ProcessName;
                        Minfo[3] = MyProcess.StartTime.ToString();
                        Console.WriteLine("标题：" + Minfo[0]);
                        Console.WriteLine("编号：" + Minfo[1]);
                        Console.WriteLine("进程：" + Minfo[2]);
                        Console.WriteLine("开始时间" + Minfo[3]);
                        Console.WriteLine();
                    }
                }
            }
            catch { }
        }
        private void timer1_Tick1(object sender, EventArgs e)
        {
            if (!isOpen) return;
            var s = sCount -(DateTime.Now - time1).Seconds;
            if ((DateTime.Now - time1).Seconds < sCount)
            {
                label1.Text = s + "秒内点击确定则终止启动项!";
                return;
            }
            timer1.Stop();
            label1.Text = "已执行！再次点击则关闭";
            isOpen = false;
            for (int i = 0; i < ConfigurationManager.AppSettings.Count; i++)
            {
                var val = new CfgHelper(ConfigurationManager.AppSettings.AllKeys[i]).Value;
                if(val.IndexOf(".") == -1) continue;
                Process.Start(val);
                //File.Open(new CfgHelper(ConfigurationManager.AppSettings.AllKeys[i]).Value, FileMode.Open);
            }
            Mute();
            if (voice)
            {
                SetVolunme(voiceNum);
            }
            if (Close)
            {
                checkBox1.Enabled = false;
                timer1.Interval = 100;
                timer1.Tick -= timer1_Tick1;
                timer1.Tick += timer1_Tick2;
                timer1.Start();
            }
        }

        private void timer1_Tick2(object sender, EventArgs e)
        {
            if (Close) this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (!isOpen)this.Close();
            if ((DateTime.Now - time1).Seconds <= sCount)
            {
                isOpen = false;
                timer1.Stop();
                label1.Text = "已停止，再次点击则关闭";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();

                (sender as CheckBox).Text = "已暂停";
            }

            else
            {
                timer1.Start();
                (sender as CheckBox).Text = "已开启";
            }
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
            this.Text = (sender as Label).Text;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Hide();
            var dr = new Form2().ShowDialog();
            this.Show();
            if (isOpen)
            {
                timer1.Start();
                loadSetting();
                time1 = DateTime.Now;
            }
        }
        /*
         存放启动项列表
         */

        private const byte VK_VOLUME_MUTE = 0xAD;
        private const byte VK_VOLUME_DOWN = 0xAE;
        private const byte VK_VOLUME_UP = 0xAF;
        private const UInt32 KEYEVENTF_EXTENDEDKEY = 0x0001;
        private const UInt32 KEYEVENTF_KEYUP = 0x0002;
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, UInt32 dwFlags, UInt32 dwExtraInfo);
        [DllImport("user32.dll")]
        static extern Byte MapVirtualKey(UInt32 uCode, UInt32 uMapType);

        public static void Mute()
        {
            //此方法只能开关切换音量 故改掉
            //keybd_event(VK_VOLUME_MUTE, MapVirtualKey(VK_VOLUME_MUTE, 0), KEYEVENTF_EXTENDEDKEY, 0);
            //keybd_event(VK_VOLUME_MUTE, MapVirtualKey(VK_VOLUME_MUTE, 0), KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            for (int i = 0; i < 50; i++)
            {
                VolumeDown();
            }
        }

        //减音量，每次减2 
        public static void VolumeDown()
        {
            keybd_event(VK_VOLUME_DOWN, MapVirtualKey(VK_VOLUME_DOWN, 0), KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(VK_VOLUME_DOWN, MapVirtualKey(VK_VOLUME_DOWN, 0), KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        } 
        //设置音量，参数为音量大小 
        public static void SetVolunme(int volume) 
        { 
            int half_volume = volume / 2; 
            for (int i = 0; i <= 50; i++) 
            { 
                VolumeDown(); 
            }
            for (int i = 0; i < volume; i++) 
            { 
                VolumeUp(); 
            } 
        } 
        //加音量,每次加2 
        public static void VolumeUp() 
        { 
            keybd_event(VK_VOLUME_UP, MapVirtualKey(VK_VOLUME_UP, 0), KEYEVENTF_EXTENDEDKEY, 0); 
            keybd_event(VK_VOLUME_UP, MapVirtualKey(VK_VOLUME_UP, 0), KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0); 
        } 
    }
    
}

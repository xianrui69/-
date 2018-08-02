using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 启动项管理
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            loadWifs();
            wifiOpen.DataSource = wifis;
            wifiOpen.ValueMember = "name";
            wifiStop.DataSource = wifis1;
            wifiStop.ValueMember = "name";
            loadSetting(Controls);
        }
        ArrayList wifis = new ArrayList();
        ArrayList wifis1 = new ArrayList();
        //private string[] wifis;
        public static string[] getWifis(out string dw3)
        {
            dw3 = "";
            wifi wf = new wifi();
            启动项管理.wifi.WLAN_AVAILABLE_NETWORK[] _wifis = wf.EnumerateAvailableNetwork();
            var wifis = new string[_wifis.Length] ;
            for (var i = 0; i < _wifis.Length; i++)
            {
                wifis[i] = _wifis[i].dot11Ssid.ucSSID;
                if (_wifis[i].dwFlags == 3) dw3 = wifis[i];
            }
            return wifis;
        }

        void loadWifs()
        {
            wifi wf = new wifi();
            启动项管理.wifi.WLAN_AVAILABLE_NETWORK[] _wifis = wf.EnumerateAvailableNetwork();
            //wifis = new string[_wifis.Length] ;
            wifis.Add(new { name = "--请选择--" });
            wifis1.Add(new { name = "--请选择--"});
            for (var i = 0; i < _wifis.Length; i++)
            {
                wifis.Add(new { name = _wifis[i].dot11Ssid.ucSSID });
                wifis1.Add(new { name = _wifis[i].dot11Ssid.ucSSID });
            }
        }

        public static void loadSetting(Control.ControlCollection c)
        {
            for (int i = 0; i < ConfigurationManager.AppSettings.Count; i++)
            {
                var key = ConfigurationManager.AppSettings.AllKeys[i];
                var val = new CfgHelper(ConfigurationManager.AppSettings.AllKeys[i]).Value;
                var finds = c.Find(key, false);
                if (finds != null && finds.Length > 0)
                {
                    var find = finds[0];
                    if (find is TextBox)
                    {
                        (find as TextBox).Text = val;
                    }
                    else if (find is CheckBox)
                    {
                        (find as CheckBox).Checked = bool.Parse(val);
                    }
                    else if (find is ComboBox)
                    {
                        try
                        {
                            (find as ComboBox).SelectedValue = (val);
                        }
                        catch (Exception e)
                        {
                            (find as ComboBox).SelectedText = (val);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control con in this.Controls)
            {
                var cofig = new CfgHelper(con.Name);
                if (con is TextBox)
                {
                    cofig.Value = (con as TextBox).Text;
                }
                else if (con is CheckBox)
                {
                    cofig.Value = (con as CheckBox).Checked + "";
                }
                else if (con is ComboBox)
                {
                    cofig.Value = (con as ComboBox).SelectedValue + "";
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //找到控件左边的框 然后禁用
        }

        private void voice_CheckedChanged(object sender, EventArgs e)
        {
            voiceNum.Enabled = (sender as CheckBox).Checked;
            if (voiceNum.Enabled) voiceNum.Text = "0";
        }
    }
}

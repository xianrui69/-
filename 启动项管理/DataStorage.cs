using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace System
{ 
    /// <summary>
    /// app.config操作类 注:调试模式无法对config进行实质更改,以及非调试也只是说根据bin里面的非调试config运行
    /// </summary>
    public class CfgHelper : IDataStorage
    {
        private static CfgHelper errMsg = new CfgHelper("lastErroMsg");
        private static CfgHelper errStackTrace = new CfgHelper("lastErroStackTrace");
        public static void setError(Exception e)
        {
            errMsg.Value = e.Message;
            errStackTrace.Value = e.StackTrace;
        }
        static Dictionary<string, CfgHelper> myExamples = new Dictionary<string, CfgHelper>();
        public CfgHelper(string key)
        {
            this.key = key;
            if (myExamples == null)myExamples=new Dictionary<string, CfgHelper>();
            if (!myExamples.ContainsKey(key)) myExamples.Add(key, this);
        }
        private string key = "";
        public string Value
        {
            get
            {
                var a = ConfigurationManager.AppSettings[key];
                if (a.Length == 0) return "";
                return a;
            }
            set
            {
                try
                {
                    Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings.Remove(key);
                    config.AppSettings.Settings.Add(key, value);
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);//重新加载新的配置文件
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public string Get(string key)
        {
            if (key == this.key) return Value;
            else return new CfgHelper(key).Value;
        }

        public bool Set(string key, string val)
        {
            try
            {
                if (key == this.key) Value = val;
                else new CfgHelper(key).Value = val;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
    /// <summary>
    /// ini操作类
    /// </summary>
    public class INI : IDataStorage<string[], string>
    {

        #region "声明变量"
        /// <summary>
        /// 遍历所有的父节点下的 父节点名 加_序号(从0开始) 名称的key
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="getKeyAction">找到key之后做的事</param>
        /// <returns>节点下 按规则的节点集合</returns>
        public static string[] ReadINI(string section, Action<string> getKeyAction = null)
        {
            try
            {
                List<string> sList = new List<string>();
                while (true)
                {
                    sList.Add(INI.ReadINI(section, section + "_" + sList.Count));
                    if (string.IsNullOrEmpty(sList.Last())) break;
                    else if(getKeyAction != null)
                    {
                        getKeyAction(sList.Last());
                    }
                }
                return sList.ToArray();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string ReadINI(string section, string key)
        {
            try
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(section, key, "", temp, 1024, strFilePath);
                return temp.ToString();
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static void WriteINI(string section, string key, string val)
        {
            WritePrivateProfileString(section, key, val, strFilePath);
        }

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        private static string strFilePath
        {
            get
            {
                var myPath = Application.StartupPath + "\\FileConfig.ini";
                if (!File.Exists(myPath))
                {
                    File.Create(myPath);
                }
                return myPath;
            }
        } //获取INI文件路径
        private static string strSec = ""; //INI文件名

        #endregion

        public string Get(string[] key)
        {
            if (key.Length == 0) return null;
            return ReadINI(key[0], key[1]);
        }

        public bool Set(string[] key, string val)
        {
            try
            {
                WriteINI(key[0], key[1], val);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    public interface IDataStorage : IDataStorage<string, string>
    {
    }

    public interface IDataStorage<TVal> : IDataStorage<string, TVal>
    {
    }

    public interface IDataStorage<Tkey, TVal>
    {
        TVal Get(Tkey key);
        bool Set(Tkey key, TVal val);
    }
}
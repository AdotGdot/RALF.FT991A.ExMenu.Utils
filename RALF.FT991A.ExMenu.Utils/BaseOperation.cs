using System;
using System.Configuration;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace RALF.FT991A.ExtendedMenu.Utils
{
    internal abstract class BaseOperation
    {
        protected Configuration config;
        protected StreamWriter backupFile;
        protected SerialPort port;

        protected int baudRate;
        protected string comPort;
        protected Parity parity;
        protected int stopBits;
        protected int dataBits;
        protected string backupDir;
        public abstract void Run();
        public string FileName { get; protected set; }
        protected void readAppConfig()
        {
            try
            {
                config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

                string value = config.AppSettings.Settings["baudRate"].Value;
                baudRate = Convert.ToInt32(value);
                comPort = config.AppSettings.Settings["comPort"].Value;
                parity = getParity();
                value = config.AppSettings.Settings["stopBits"].Value;
                stopBits = Convert.ToInt32(value);
                value = config.AppSettings.Settings["dataBits"].Value;
                dataBits = Convert.ToInt32(value);
                backupDir = config.AppSettings.Settings["backupDir"].Value;
                if (String.IsNullOrEmpty(backupDir))
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    backupDir = String.Format("{0}\\FT991A-backups", path);
                    DirectoryInfo di = Directory.CreateDirectory(backupDir);
                    setBackupDir();
                }
            }
            catch
            {
                throw new Exception(Constants.configError);
            }
        }
        internal void writeFooter()
        {
            Console.WriteLine("\tProject : {0}", Constants.projectName);
            Console.WriteLine("\t{0}", Constants.freeware);
        }
        protected void openComport()
        {
            port = new SerialPort(comPort, baudRate, parity, (int)dataBits, (StopBits)stopBits);
            port.RtsEnable = true;
            port.ReadTimeout = 100;
            port.Open();
        }
        protected void setBackupDir()
        {
            config.AppSettings.Settings["backupDir"].Value = backupDir;
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private Parity getParity()
        {
            string p = config.AppSettings.Settings["parity"].Value;
            switch (p)
            {
                default:
                case "0": return Parity.None;
                case "1": return Parity.Odd;
                case "2": return Parity.Even;
                case "3": return Parity.Mark;
            }
        }
    }
}

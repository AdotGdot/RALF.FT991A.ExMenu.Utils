using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace RALF.FT991A.ExtendedMenu.Utils
{
    internal class BackupOperation: BaseOperation
    {
        public override void Run()
        {
            readAppConfig();
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = backupDir;
                saveFileDialog.Filter = Constants.fileFilter;
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = false;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = saveFileDialog.FileName;
                    initBackupFile(FileName);
                    backup();
                    Thread.Sleep(100);
                    backupFile.Close();
                    port.Close();
                }
                else
                {
                    throw new Exception("backup operation canceled");
                }
            }
        }

        private void initBackupFile(string fileName)
        {
            if (File.Exists(fileName))
                File.WriteAllText(fileName, String.Empty);
            backupFile = new StreamWriter(fileName, append: true);
            backupFile.WriteLine(Constants.ralfHeader);
            backupFile.WriteLine(Constants.notRecommended);
            setBackupDir();
        }

        private void backup()
        {
            openComport();
            port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);

            string cmdFormat = "EX{0};";
            string cmd;
            for (int n = 0; n < 153; n++)
            {
                cmd = String.Format(cmdFormat, (n + 1).ToString().PadLeft(3, '0'));
                port.Write(cmd);
                Thread.Sleep(50);
            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string msgs = sp.ReadExisting();
            string[] responses = msgs.Split(';');
            foreach (string response in responses)
            {
                if (response != "?" && !String.IsNullOrEmpty(response) && response.StartsWith("EX"))
                {
                    string cmd = response.Substring(0, 5);
                    string line = String.Format("{0} : {1}", cmd, response);
                    backupFile.WriteLine(line);
                    Console.WriteLine(line);
                }
            }
        }
    }
}

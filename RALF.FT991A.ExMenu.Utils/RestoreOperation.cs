using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace RALF.FT991A.ExtendedMenu.Utils
{
    internal class RestoreOperation : BaseOperation
    {
        public override void Run()
        {
            readAppConfig();
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = backupDir;
                    openFileDialog.Filter = Constants.fileFilter;
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = false;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileName = openFileDialog.FileName;
                        restore(FileName);
                    }
                    else
                    {
                        throw new Exception("restore operation canceled");
                    }
                }
            }
        }

        private void restore(string filename)
        {
            openComport();
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                if (!line.StartsWith("#"))
                {
                    string[] parts = line.Split(':');
                    string cmd = parts[1].Trim();
                    port.Write(cmd);
                    Console.WriteLine(line);
                    Thread.Sleep(50);
                }
            }
            port.Close();
        }
    }
}

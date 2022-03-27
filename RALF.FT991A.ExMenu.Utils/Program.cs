using System;

namespace RALF.FT991A.ExtendedMenu.Utils
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            int selection = getUserSelection();
            switch (selection)
            {
                case 1:
                    // run backup
                    runBackupOperation();
                    anyKeyToExit();
                    break;
                case 2:
                    // run restore
                    runRestoreOperation();
                    anyKeyToExit();
                    break;
                default:
                    break;
            }
        }

        private static void runRestoreOperation()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\n\t{0}\n", Constants.projectName);
                Console.WriteLine("Starting restore operation ... \n");
                RestoreOperation restoreOperation = new RestoreOperation();
                restoreOperation.Run();
                Console.WriteLine("\n ... restore operation complete");
                Console.WriteLine("\n\tFile : {0}", restoreOperation.FileName);
                restoreOperation.writeFooter();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\tRestore failed : {0} ", ex.Message);
            }
        }

        private static void runBackupOperation()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\n\t{0}\n",Constants.projectName);
                Console.WriteLine("Starting backup operation ... \n");
                BackupOperation backupOperation = new BackupOperation();
                backupOperation.Run();
                Console.WriteLine("\n ... backup operation complete");
                Console.WriteLine("\n\tFile : {0}", backupOperation.FileName);
                backupOperation.writeFooter();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\tBackup failed : {0} ",  ex.Message);
            }
        }

        private static void anyKeyToExit()
        {
            Console.WriteLine("\nAny key to exit ...");
            Console.ReadKey();
        }
        private static int getUserSelection()
        {
            Console.Clear();
            Console.WriteLine("\n{0}\n",Constants.projectName);
            Console.WriteLine("1 - backup the extended menu");
            Console.WriteLine("2 - restore the extended menu");
            Console.WriteLine("3 - quit");
            Console.WriteLine("Enter your selection");
            int key = 0;
            string value = Console.ReadKey().KeyChar.ToString();
            if (!Int32.TryParse(value, out key))
                getUserSelection();
            return key;
        }
    }
}

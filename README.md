# RALF.FT991A.ExMenu.Utils
backup &amp; restore utility for FT-991A's extended menu
FT991A.ExMenuUtil.exe
Copyright (c) 2022 by A.G. 'Fritz' von Luternow

What It Is
----------
FT991A.ExMenuUtil.exe is a free open source Windows application 
which is designed to backup and restore the FT-991A's 
extended menu. The executable presents the UI for RALF.FT991A.ExMenuLib.dll 
which contains the communications, backup and restore functionalities.
The utility allows FT-991A owner to have multiple extended menu files 
which can be loaded as desired.

Installation 
------------
1. Download the FT991A.ExMenuUtil-executable.zip archive
2. Using WinZip or another ZIP utility extract the archive 
   to the desired directory
3. using a text editor open "FT991A.ExMenuUtils.exe.config"
4. set the comport params as needed. There are legends which provide the 
   proper aliases for parity and stopbit values.
5. set the directory where backups are to be stored. If a directory is 
   not set the files will be saved in the "Documents/FT991A-backups" folder
5. save the file

Uninstallation 
------------
1. Delete the install directory
2. Delete any previously saved backup files

Saving a Backup
---------------
1. The radio should be powered on and connected to the host computer
2. selecting menu option "1" will start the backup process by opening 
   a Save File dialog
3. In the dialog provide the backup file a meaningful name 
4. Select a location whereto save the file
4. Clicking the dialog's Save button starts the backup operation

Restoring a Backup
------------------
1. The radio should be powered on and connected to the host computer.
2. selecting menu option "2" will start the restore process by opening 
   an Open File dialog
3. In the dialog navigate to and select the desired backup file
4. Clicking the dialog's Open button will start the restore operation	

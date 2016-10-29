
<p align="center">
  <a href="http://www.yorku.ca/" target="_blank"">
  <img border="0" src="http://www.yorku.ca/yorkweb/demos/test-header/images/yorku-logo-rgb-240-122.jpg">
</p>

1. [What is CLog](#what)
2. [Requirements and Dependencies](#requirements)
3. [How to Install CLog](#install)
4. [How to Uninstall CLog](#uninstall)
5. [Instructions (Step-by-Step)](#instructions)
6. [Version Numbers](#version)

## <a name="what"></a>What is CLog
***
CLog is a C#.NET application that was designed and implemented to solve a problem in regards to part-time staff Classroom Operations logs. This application searches a specified directory on the York UIT share drive, reads the files into memory, searches, sorts and generates part-time staff logs for a Classroom Operations shift. <br></br>
<br></br>
This software is intended to work on the York UIT machines and by accounts with certain elevated privileges. Failure to meet these requirements will resulting in the application not working correctly or not running at all. <br></br>
<br></br>
If you have any comments/questions/concerns please feel free to [contact me](mailto:jhanperera@live.com).

## <a name="requirements"></a>Requirements and Dependencies
***
Listed bellow are the minimum requirements for this application to run. **PLEASE NOTE: The machine this application is installed on must have MS Office or at least MS Excel installed**
<br></br>

|   |Minimum Requirement|
|:-:|:-:|
|CPU|1 GHz clock speed, IA-32 or x64 architecture with SSE2 support|
|RAM|2 GB|
|Operating system|Windows 7<br>Windows 8<br>Windows 8.1<br>Windows 10</br>|
|Hard disk drive|256MB of free space|
|Software|.NET Framework 4.5 or higher. [MS Office (Excel is a must)](https://www.office.com/)|

<br></br>

## <a name="install"></a>How to Install CLog
***
1. Click on the download button bellow and save the install zip anywhere on your computer.
<p align="center">
  <a href="https://s3.amazonaws.com/jhansapps/CLog-Beta_v0.2.9_Setup.zip">
  <img src="http://diylogodesigns.com/blog/wp-content/uploads/2016/04/download-button.png">
</p>

2. Unzip the _CLog-Beta_vX.X.X_Setup.zip_ file anywhere on your computer. This can be done by right-clicking the zip file and selecting **Extract All...**. Follow the prompts and save the output folder anywhere on your computer. 
<p align="center">
   <img src="https://s3.amazonaws.com/jhansapps/docImages/Capture.PNG">
   <img height="150" width="150" src="http://www.clipartbest.com/cliparts/nTE/xEk/nTExEk5TA.jpeg">
   <img src="https://s3.amazonaws.com/jhansapps/docImages/Capture2.PNG">
</p>
3. Enter into the newly extracted _CLog-Beta_vX.X.X_Setup_ folder and double click on **CLogSetup.msi** installer file. NOTE: Sometimes the installer might need admin privileges to execute. If this is the case, you can right-click on **CLogSetup.msi** and then select **Run as administrator**.
<p align="center">
   <img src="https://s3.amazonaws.com/jhansapps/docImages/Capture3.PNG">
</p>
4. If a _Open File - Security Warning_ appears you can click **Run**. 

5. Follow the installation instruction that follow. Ensure the installation locations is set to: **C:\Program Files (x86)\YorkUIT\CLog-Beta\**. Otherwise there will be problems with saving and loading user settings.
<p align="center">
   <img src="https://s3.amazonaws.com/jhansapps/docImages/Capture5.PNG">
</p>

6. Once completed you should have a desktop icon and a start mean icon to launch the application. Users can run the application as a normal user or as an administrator if need be. 
<p align="center">
   <img src="https://s3.amazonaws.com/jhansapps/docImages/Capture6.PNG">
</p>

## <a name="uninstall"></a>How to Uninstall CLog
***
During the installation process something might go wrong that causes a corruption in the config files or the exe file. If the application is not running correctly then the best option is to do a clean re install of the application. A re-download might also be needed to ensure that no corruption occurs while all the binary files are being downloaded over the internet.

### Option 1) <br></br>
The first option is to run the **CLogSetup.msi** again and chose to uninstall option to remove all the binary files and config files. 
<br></br>
### Option 2) <br></br>
The second option is to remove the application through **Control Panel\Programs\Programs and Features**. The application is named **CLog-Beta** with the Publisher being **YorkUIT**. Right click **CLog-Beta** and select **Uninstall**. An installation wizard will then prompt you on how to uninstall the application.

## <a name="instructions"></a>Instructions (Step-by-Step)
***
<br></br>
## <a name="version"></a>Version Number & Patch Notes (Bold version is the current version)
***
**Version 0.2.9 - 10/26/16**
* Added the day of the week and painted it in red, as well as a easier to read date to the head.
* Implemented a better data merging feature that will find already existing crestron logouts and add notes that show up as "AV Shutdown" by the zone supervisors. Reducing redundant duplicates.
* Tweak the case when there are 3 employees are working a shift for better work distribution.
* Minor bug fixes and optimisations.
<br></br>

Version 0.2.8 - 10/17/16
* A complete overhaul of the zoning feature. Re-worked the central points and the amount of boarder buildings in each zone. This was applies to the scenario when there are 2, 3, 4, and 5 employees selected to work on a given shift.
* Change the detail window to show a little more information about what the application is doing. Added a red color to the time for easier readability.
* Implemented a few more optimisations to the overall system.
* Bug fixes.
<br></br>

Version 0.2.7 - 10/03/16
* Minor bug fix that would cause the system to not read the CLO file correctly.
* Removed MC from being reached by the south zoned area.
<br></br>

Version 0.2.6 - 10/03/16
* Updated all the Messages Box UI’s to the Metro UI.
* Changed all windows theme to Red, White, and black.
* Fine-tuned the task-ranking system to calculate the tasks correctly and fixed a bug that would cause the task rank to be calculated incorrectly.
* Implemented a log window that displays what is going on in the back ground accompanied with time stamps.
* Implemented a “Save Settings” check box that allows the user to save the current set parameters for future use. Settings are saved locally so the setting set by one user will not change other users settings.
* Minor optimisations and a few bug fixes.
<br></br>

Version 0.2.5 - 09/19/16
* Added more detail on the initial popup message about when the Zone Super logs were last modified. In the format of Day, Month, Year - Time.
* Fixed a bug that would cause one instance of Excel to not terminate correctly.
* Minor bug fixes and slight optimisations.
<br></br>

Version 0.2.4 - 09/12/16
* More bug fixes and optimisations
* Enabled editing of the start and end time of a shift during the log viewing stage
<br></br>

Version 0.2.3 - 09/09/16
* More bug fixes
* Added required text if the demo has no instructions
* Added logout/closing instructions to R N102
* Added version indicator in the settings frame.
<br></br>

Version 0.2.2 - 09/08/16
* Fixed a major bug that would cause the program to crash if 4 or more employees were selected to work a shift.
<br></br>

Version 0.2.1 -  09/02/16
* Implemented a permanent fix when when the clo file is not formatted correctly
* Minor bug fixes and optimizations.
<br></br>

Version 0.1.3 - 08/19/16
* Added a previous button during the log viewer stage so the user can return
* and change the name of the employee who was assigned to a specific log.
* UI improvements so that the application accounts for different resolutions and a modern look and feel.(Still experimental)
* The print dialog now shows up once.
* All the logs show up and on point now with no delay in between.
* Minor bug fixes and optimizations to make the program run with less memory.
<br></br>

Version 0.1.1 - 08/12/16
*  An installer was developed for the CLog.exe application
*  A print dialog will now show up before the print preview so the user can choose a printer to print from.
*  Removed the “CLO generator” tab
*  Minor bug fixes and optimizations
<br></br>

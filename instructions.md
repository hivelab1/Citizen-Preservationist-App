##App Install

##Citizen Preservationist DataCollector

Android
1. Enable Developer options in your Android settings 
2. Downloading the .apk Android app install file in a known location in your smartphone from your email or connecting it to your PC via usb
3. Browse your internal storage to that location, double tap on the apk, and follow instructions to install

iOS
1. Download and build CitPres DataCollector Unity project for IOS or download IOS XCode files for local deployment
2. Using XCode, build application to iOS device using your developer group settings

##Citizen Preservationist Map

Windows and MacOS
1. Download desired build folder to yuor location of choice
2. Navigate to said folder and run executable from there

---
##Using the Apps

##DataCollector App
1. Launch the app
2. Fill in and log the unique user and session/location IDs given to you during training
3. Watch given video guide or proceed to step 4
4. Align object you want to photograph/capture with crosshair/viewfinder
5. Tap the screen anywhere to take a picture
6. If the picture is not to your liking, tap back in the top left corner
7. Fill out as many metadata in the form as you can
8. Review the data
9. Tap submit button to save photograph and metadata
10. Repeat until data capture session is done

##Retrieving data collected via DataCollector

Android
1. Connect your smartphone to PC via usb
2. Accept computer connection on your smartphone (USB and Media transfer) 
3. In your PC, navigate to /Android/data/com.HiveLab.CitizenPreservationist/files/Artifacts/current Date and save the captured data to a known location/folder

IOS
1. Connect your Apple device to PC/Mac via usb
2. Navigate to /var/mobile/Containers/Data/Application/<guid>/Documents/Artifacts/current Date
3. In your computer, navigate to /Android/data/com.HiveLab.CitizenPreservationist/files/Artifacts/current Date and save the folders with collected data to a known location/folder

##Using Convert and Save

Windows
1. Copy the json folders (collected data) to a known location in your PC
2. Transfer "Convert and Save.exe" to this folder
3. Double click Convert and Save.exe
4. The json file is now converted in a .png image and metadata into a .csv file (ready to import in your GIS of choice)
5. All of the jsons are also converted into a json containing all data, and a csv containing all data.
6. The pictures taken are located in /files/Pictures/current Date
7. The pictures are also located in ./current Date/Pictures in the same folder the Convert and Save.exe files is ran.

MacOS
1. Copy the json folders (collected data) to a known location in your PC
2. Transfer "Convert and Save.py" to this folder
3. Press command + spacebar to open Spotlight
4. Open Terminal
5. Make sure you have python 3.x installed, where x is any version of python 3. (This can be confirmed with the following command:    "python --version") (Most MacOS devices come with python 2.7 installed.)
6. Navigate to the parent folder where the script and all of the data folders are located
7. Enter the command: "python3 "Convert and Save.py"" and press enter
8. The json file is now converted in a .png image and metadata into a .csv file (ready to import in your GIS of choice)
9. All of the jsons are also converted into a json containing all data, and a csv containing all data.
10. The pictures taken are located in /files/Pictures/current Date
11. The pictures are also located in ./current Date/Pictures in the same folder the Convert and Save.exe files is ran.


##Visualize photos, metadata, and artifact locations using CitPres Map

Windows and MacOS
1. Open the app
2. Confirm whether or not to use the default map and sample location coordinates
3. Click next
4. If this is the first time the app is opened, navigate to a folder containing all of the unconverted data, such that the folder contains subfolders, each of which is data collected on a separate occassion
5. View collected data

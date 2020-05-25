App Install

Citizen Preservationist
Android
1. Enable Developer options in your Android settings 
2. Downloading the .apk Android app intall file in a known location in your smartphone from email or connecting it to your PC via usb
3. Browse your internal storage to that location, double tap on the apk, and follow instructions to install

IOS
1. Download and build unity project for IOS or download IOS XCode files for local deployment
2. Using XCode, build application to device using your developer group settings

CitPres Map
Windows and MacOS
1. Download desired build folder to yuor location of choice
2. Navigate to said folder and run from there

Using the Apps

Open the app
1. Launch the app
2. Fill in and log the unique user and session/location IDs given to you
3. Watch given video guide or proceed to step 4
4. Align object you want to capture with crosshair/viewfinder
5. Tap the screen
6. If the picture is not to your liking, tap back in the top left corner
7. Fill out as many metadata in the form as you can
8. Tap save
9. Repeat until data capture session is done

Retrieving data
Android
1. Connect your smartphone to PC via usb
2. Navigate to /Android/data/com.HiveLab.CitizenPreservationist/files/Artifacts/current Date

IOS
1. Connect your apple device to PC via usb
2. Navigate to /var/mobile/Containers/Data/Application/<guid>/Documents/Artifacts/current Date

Using Convert and Save
3. Copy the json file (collected data) to a know location in your PC
3. Transfer "Convert and Save.exe" to this folder
4. Double click Convert and Save.exe
5. The json file is now converted in a .png image and metadata into a .csv file (ready to import in your GIS of choice)
6. All of the jsons are also converted into a json containing all data, and a csv containing all data.
7. The pictures taken are located in /files/Pictures/current Date
8. The pictures are also located in ./current Date/Pictures in the same folder the Convert and Save.exe files is ran.

View using CitPres Map
Windows and MacOS
1. Open the app
2. Confirm whether or not to use the default map and sample location coordinates
3. Click next
4. If this is the first time the app is opened, navigate to a folder containing all of the unconverted data, such that the folder contains subfolders, each of which is data collected on a separate occassion
5. View collected data

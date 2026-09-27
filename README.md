# photopicker-ADB-Ultimate
wifi connection interface to access common files on phone

PURPOSE: Create C# app to connect wirelessly to broken GALAXY S21 FE. regain access from PC to photos after hardware impairment using ADB bridge.

REASON: daughter board on phone had damaged pins on USB C port data pins, charging functionality not impaired.

RESULT: full access to desired directories on phone... DCIM/Camera, DCIM/Screenshots and Downloads

FUNCTION: download files to pc or upload to phone, fast and hands free

ADB server required on PC. download android ADK or SDK

no third party apps required.

phone configuration
developer options = ON
wireless debugging = ON 
Disable Wi-Fi Power Saving
Lock Screen Timeout
establish and assign static IP on network for phone
band steering off on the network

establish ADB pairing on phone

<<<DOS

adb pair 192.168.1.35:PAIRING_PORT

>>>

check success of pairing process 

<<<DOS

adb devices 

>>>

force phone to use port 5555 for file transfers, if not port will continually change

<<<DOS

adb connect 192.168.1.35:MAIN_PORT
adb tcpip 5555
adb connect 192.168.1.35:5555

>>>

// Extend screen timeout on the phone to 30 minutes while using the app
RunAdb("shell settings put system screen_off_timeout 1800000");

// Optional: Keep Wi-Fi active during sleep
RunAdb("shell settings put global wifi_sleep_policy 2");

run program and easy access to DCIM and download folders on phone with no third party app or tethering


















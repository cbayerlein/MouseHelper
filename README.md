# MouseHelper
Simple .net accessibility tool, which helps you do more with your keyboard

The little helper sits in the system tray and waits for the hotkey Ctrl-Alt-T. When the hotkey is pressed it examines the user interface of the app which currently in the foreground. It seearches for "invokable" (that is usuallly clickable) controls and displays numbers to mark the element and also shades the outline element itself darker. Additionaly, it displays listbox with the numbers and names of the elements plus an input box to input the number of the control you want to invoke. On pressing enter the choosen control is invoked and the tool disappears back into the system tray.

The software is in early alpha. There are many features I'd like to add (like a moveable and collapseable main window and a more robust overlay). Also, there are certainly many bugs and/or incostincesies in the behaviour. I'm a novice of c# and coded my first project after having a crash course for a week at work. 

**If you are interressted in contributing or beta-testing; I'd really appreciate any help!**

## Instalation

To try the software out just download the mousehelper.zip in the release directory and unpack to a directory on your local hard-drive. Then start the exe-file. If you see a little triangle in your system tray, the app is running and waiting for the hot-key.

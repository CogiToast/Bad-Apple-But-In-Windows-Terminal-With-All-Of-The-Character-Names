# Bad-Apple-But-In-Windows-Terminal-With-All-Of-The-Character-Names


I recommend checking as Code
=




You'll need:
DOTNET - 8.0.403
VS CODE (or) Notepad
Windows
NAudio version 2.2.
SixLabors.ImageSharp version 2.1.9
FFmpeg
Your own bad_apple.mp4
==========================

download ffmpeg from gyan.dev
download the program.cs and .csproj file (Also download bad_apple.mp3)
open your windows terminal (cmd)

make a new directory. (mkdir [NEW_DIR])
change to the new directory. (cd [NEW_DIR])

run "dotnet new console"
delete the program.cs and .csproj in the folder
(keep the 'bin' and 'obj')

once there, do "dotnet add package NAudio --version 2.2.1" and "dotnet add package SixLabors.ImageSharp --version 2.1.9"
once done, add the downloaded Program.cs and ShittyBadApple.csproj (also bad_apple.mp3) to the directory via folders.

run "program.cs" (I'm guessing you either have vs codes or notepad) and go to line 156 and replace it with your directory (that leads to bad_apple.mp3)

once everything is set up and ready, you can type in "dotnet run" inside of the root directory.

Made this in my Leisure time.
The final folder system should look like this:
FOLDER - Users
|
| - FOLDER - [YOUR_NAME]
  |- Program.cs
  |- ShittyBadApple.csproj
  |- FOLDER -video
    |- bad_apple.mp3
    |- bad_apple.mp4
    |- FOLDER - frams
    |- your ffmpeg frames
    


# Bad-Apple-But-In-Windows-Terminal-With-All-Of-The-Character-Names
Made this in my Leisure time.
=============
I recommend checking as Code
=============
==========================
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
download the program.cs and .csproj file (Also download the "video" folder)
open your windows terminal (cmd)
make a new directory. (mkdir [NEW_DIR])
change to the new directory. (cd [NEW_DIR])
once there, do "dotnet add package NAudio --version 2.2.1" and "dotnet add package SixLabors.ImageSharp --version 2.1.9"
once done, add the downloaded .cs and .csproj (also the "video" folder) to the directory via folders.
run "program.cs" (I'm guessing you either have vs codes or notepad) and go to line 156 and replace it with your directory (that leads to bad_apple.mp3)

once everything is set up and ready, you can type in "dotnet run" inside of the root directory.



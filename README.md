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
==========================

download ffmpeg from gyan.dev.
download the BadApple.cs and SBA.csproj file (Also download bad_apple.mp3).
open your windows terminal (cmd).

make a new directory. (mkdir [NEW_DIR]). * ( Assuming you're in C:\Users\[YOUR_NAME]\ ) *
change to the new directory. (cd [NEW_DIR]). * ( It should now look like C:\Users\[YOUR_NAME]\[NEW_DIR]\ ) *

run "dotnet new console".
(delete the program.cs and .csproj in the folder).
(keep the 'bin' and 'obj').

next, in the same directory do "dotnet add package NAudio --version 2.2.1" and "dotnet add package SixLabors.ImageSharp --version 2.1.9".
once done, add the downloaded Program.cs and ShittyBadApple.csproj (also bad_apple.mp3) to the directory via folders.

make new directory and name it "video". * ( Assuming you're in C:\Users\[YOUR_USER]\[NEW_DIR]\ ) *
(store your downloaded bad_apple.mp3 and bad_apple.mp4 file here as well.)

extract the ffmpeg file into any folder (so long as you remember where it is).
go into the ffmpeg folder and move the ffmpeg.exe from the folder to the new folder "video" we created.

and run ".\ffmpeg.exe -y -i bad_apple.mp4 -vf "scale=240:90,pad=320:90:(ow-iw)/2:0:black,format=gray" -fps_mode vfr frames\thumb%04d.png".


run "BadApple.cs" (I'm guessing you either have vs codes or notepad) 
Next, go to line 156 and replace it with your directory that leads to bad_apple.mp3. ( C:\Users\[YOUR_USER]\[NEW_DIR]\video\bad_apple.mp3 )

once everything is set up and ready, you can type in "dotnet run" inside of the root directory. ( C:\Users\[YOUR_USER]\[NEW_DIR]\ )

Made this in my Leisure time.
The final folder system should look like this:
FOLDER - Users
|
| - FOLDER - [YOUR_NAME]
  |- FOLDER - [NEW_DIR]
    |- obj
    |- bin
    |- Program.cs
    |- ShittyBadApple.csproj
    |- FOLDER - video
      |- ffmpeg.exe
      |- bad_apple.mp3
      |- bad_apple.mp4
      |- FOLDER - frames
        |- your ffmpeg  frames
    


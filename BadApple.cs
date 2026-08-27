using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using NAudio.Wave; 

class BadAppleConsole
{
    public struct CharacterInterval
    {
        public int StartFrame;
        public int EndFrame;
        public string Name;

        public CharacterInterval(int start, int end, string name)
        {
            StartFrame = start;
            EndFrame = end;
            Name = name.ToUpper(); 
        }
    }

    
    [StructLayout(LayoutKind.Sequential)]
    public struct Coord { public short X; public short Y; }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect { public short Left; public short Top; public short Right; public short Bottom; }

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct CharUnion { [FieldOffset(0)] public char UnicodeChar; }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CharInfo
    {
        public CharUnion Char;
        public ushort Attributes; 
    }

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern bool WriteConsoleOutputW(
        IntPtr hConsoleOutput,
        CharInfo[] lpBuffer,
        Coord dwBufferSize,
        Coord dwBufferCoord,
        ref SmallRect lpWriteRegion);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetStdHandle(int nStdHandle);

    const int STD_OUTPUT_HANDLE = -11;
    const ushort FOREGROUND_WHITE = 0x0007;

    
    const int Width = 320;   
    const int Height = 90;   
    const double TargetFps = 30.0;

    static void Main()
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.CursorVisible = false;

        
        if (!Console.IsOutputRedirected && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            try
            {
                Console.SetBufferSize(Math.Max(Width, Console.BufferWidth), Math.Max(Height, Console.BufferHeight));
                Console.SetWindowSize(Width, Height);
                Console.SetBufferSize(Width, Height);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Window auto-resize not supported: {ex.Message}");
                Thread.Sleep(1500);
                Console.Clear();
            }
        }

        IntPtr hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
        CharInfo[] screenBuffer = new CharInfo[Width * Height];
        
        Coord bufferSize = new Coord { X = Width, Y = Height };
        Coord bufferCoord = new Coord { X = 0, Y = 0 };
        SmallRect writeRegion = new SmallRect { Left = 0, Top = 0, Right = (short)(Width - 1), Bottom = (short)(Height - 1) };
       
        
        CharacterInterval[] timeline = new CharacterInterval[]
        {
            new CharacterInterval(0, 359, "REIMU"),          
            new CharacterInterval(360, 450, "APPLE"),        
            new CharacterInterval(451, 780, "MARISA"),
            new CharacterInterval(781, 869, "APPLE"),    
            new CharacterInterval(870, 1060, "PATCHOULI"),
            new CharacterInterval(1061, 1211, "REMILIA"),
            new CharacterInterval(1212, 1290, "CUP"),
            new CharacterInterval(1291, 1476, "SAKUYA"),
            new CharacterInterval(1477, 1491, "KNIFE"),
            new CharacterInterval(1492, 1740, "FLANDRE"),
            new CharacterInterval(1741, 1890, "YOUMU"),
            new CharacterInterval(1891, 2099, "YUYUKO"),
            new CharacterInterval(2100, 2310, "KOMACHI"),
            new CharacterInterval(2311, 2490, "EIKI"),
            new CharacterInterval(2491, 2790, "MOKOU"),
            new CharacterInterval(2791, 2940, "KEINE"),
            new CharacterInterval(2941, 3150, "EIREN"),
            new CharacterInterval(3151, 3330, "KAGUYA"),
            new CharacterInterval(3331, 3450, "PRISMRIVERS"),
            new CharacterInterval(3451, 3510, "LUNASA"),
            new CharacterInterval(3511, 3531, "MERLIN"),
            new CharacterInterval(3532, 3570, "LYRICA"),
            new CharacterInterval(3570, 3600, "CHEN"),
            new CharacterInterval(3601, 3630, "RAN"),
            new CharacterInterval(3631, 3660, "TEWI"),
            new CharacterInterval(3661, 3720, "REISEN"),
            new CharacterInterval(3721, 3780, "MOMIJI"),
            new CharacterInterval(3781, 3930, "SANAE"),
            new CharacterInterval(3931, 3975, "HINA"),
            new CharacterInterval(3976, 4050, "KANAKO"),
            new CharacterInterval(4051, 4170, "KANAKO,SUWAKO"),
            new CharacterInterval(4171, 4395, "YUKARI"),
            new CharacterInterval(4396, 4560, "TENSHI"),
            new CharacterInterval(4561, 4584, "YUKARI"),
            new CharacterInterval(4585, 4600, "TENSHI"),
            new CharacterInterval(4601, 4640, "TENSHI,YUKARI"),
            new CharacterInterval(4641, 4767, "AYA"),
            new CharacterInterval(4768, 4800, "PENCIL"),
            new CharacterInterval(4801, 5010, "SUIKA"),
            new CharacterInterval(5011, 5067, "DROPLET"),
            new CharacterInterval(5068, 5190, "ALICE"),
            new CharacterInterval(5191, 5430, "NITORI"),
            new CharacterInterval(5431, 5640, "YUUKA"),
            new CharacterInterval(5641, 5850, "ELLY"),
            new CharacterInterval(5851, 6572, "TOUHOU")
        };

        
        string projectRoot = Directory.GetCurrentDirectory();
        string videoPath = Path.Combine(projectRoot, "video");
        string framesPath = Path.Combine(videoPath, "frames");


        if (!Directory.Exists(framesPath))
        {
            projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
            videoPath = Path.Combine(projectRoot, "video");
            framesPath = Path.Combine(videoPath, "frames");
        }

        
        string audioFilePath = @"C:\Users\[YOUR_USER]\[NEW_DIR]\video\bad_apple.mp3";


        string[] realFrames = LoadRealFrames(framesPath);
        if (realFrames == null || realFrames.Length == 0)
        {
            Console.WriteLine("Error: Extraction dataset folders not verified or empty.");
            return;
        }

        
        if (!File.Exists(audioFilePath))
        {
            Console.WriteLine($"Warning: Track asset file not found at: {audioFilePath}");
            Console.WriteLine("Continuing with muted audio engine tracking configurations...");
            Thread.Sleep(3000);
        }

        Console.Clear();
        Console.WriteLine("Fully loading frames . . .");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        Thread.Sleep(1000);
        Console.WriteLine("Initializing Assets . . .");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        Thread.Sleep(1000);
        Console.WriteLine("Almost done . . .");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        Thread.Sleep(1000);
        Console.WriteLine("=============================================");
        Console.WriteLine("     ALL ASSETS LOADED & IN RAM ");
        Console.WriteLine("=============================================");
        Console.WriteLine("\n -> Press ANY KEY to start playing. <-");
        Console.ReadKey(true);
        Console.Clear();

        
        IWavePlayer wavePlayer = null;
        AudioFileReader audioReader = null;

        if (File.Exists(audioFilePath))
        {
            wavePlayer = new WaveOutEvent(); 
            audioReader = new AudioFileReader(audioFilePath);
            wavePlayer.Init(audioReader); 
            wavePlayer.Play(); 
        }

        
        TimeSpan frameTargetTime = TimeSpan.FromSeconds(1.0 / TargetFps);
        var stopwatch = Stopwatch.StartNew();
        
        while (true) 
        {
            TimeSpan currentPlaybackTime = stopwatch.Elapsed;
            

            int frameCount = (int)(currentPlaybackTime.TotalSeconds * TargetFps);

            
            if (frameCount >= realFrames.Length)
                break;

            string currentFrameData = realFrames[frameCount]; 

            
            string activeText = "BADAPPLE"; 
            foreach (var interval in timeline)
            {
                if (frameCount >= interval.StartFrame && frameCount <= interval.EndFrame)
                {
                    activeText = interval.Name;
                    break;
                }
            }

            
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int index = y * Width + x;
                    bool pixelIsWhite = currentFrameData[index] == '#';

                    if (pixelIsWhite) 
                    {
                        screenBuffer[index].Char.UnicodeChar = activeText[x % activeText.Length];
                        screenBuffer[index].Attributes = FOREGROUND_WHITE;
                    } 
                    else 
                    {
                        screenBuffer[index].Char.UnicodeChar = ' ';
                        screenBuffer[index].Attributes = 0;
                    }
                }
            }

            
            WriteConsoleOutputW(hConsole, screenBuffer, bufferSize, bufferCoord, ref writeRegion);
            

            TimeSpan nextFrameTarget = TimeSpan.FromSeconds((frameCount + 1) / TargetFps);
            TimeSpan timeBufferLeft = nextFrameTarget - stopwatch.Elapsed;
            
            if (timeBufferLeft > TimeSpan.Zero)
            {
                Thread.Sleep(timeBufferLeft);
            }
        }

        
        if (wavePlayer != null)
        {
            wavePlayer.Stop();
            wavePlayer.Dispose();
            audioReader.Dispose();
        }

        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("   *  Bad Apple execution completed flawlessly in sync!");
    }

    static string[] LoadRealFrames(string framesDirectory)
    {
        if (!Directory.Exists(framesDirectory))
        {
            Console.WriteLine($"Error: Folder path missing -> {framesDirectory}");
            return null;
        }

        
        string[] filePaths = Directory.GetFiles(framesDirectory, "*.png");
        if (filePaths.Length == 0) return null;
        
        Array.Sort(filePaths); 

        string[] loadedFrames = new string[filePaths.Length];
        Console.WriteLine($"Preloading {filePaths.Length} frames to memory for sync playback...");

        
        StringBuilder frameString = new StringBuilder(Width * Height);

        for (int i = 0; i < filePaths.Length; i++)
        {
            
            using (Image<L8> image = Image.Load<L8>(filePaths[i]))
            {
                frameString.Clear(); 

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        
                        byte pixelBrightness = image[x, y].PackedValue;
                        
                        
                        frameString.Append(pixelBrightness > 120 ? '#' : ' ');
                    }
                }
                loadedFrames[i] = frameString.ToString();
            }

            
            if (i % 1000 == 0 && i > 0)
            {
                Console.WriteLine($"Cached {i} / {filePaths.Length} frames in RAM...");
            }
        }

        return loadedFrames;
    }
}

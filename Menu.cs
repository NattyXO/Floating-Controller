using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Floating_Controller
{

    public partial class Menu : Form
    {
        public enum Orientations
        {
            DEGREES_CW_0 = 0,
            DEGREES_CW_90 = 3,
            DEGREES_CW_180 = 2,
            DEGREES_CW_270 = 1
        }
        public Menu()
        {
            InitializeComponent();
            picRotate.Visible = false;
            picRotate180.Visible = false;
            picRotate270.Visible = false;
            picAlwaysOnOFF.Visible = false;
            timer1.Interval = 15000; // Set the interval to 15 seconds (adjust as needed)
            timer1.Tick += PicAlwaysOnTimer_Tick;
            timer1.Start();
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        

        public void ToastShow(string type, string message)
        {
            TaostNotification toast = new TaostNotification(type, message);
            toast.Show();
        }

        private void picScreenShot_Click(object sender, EventArgs e)
        {
            // Call picExpandClose_Click with appropriate parameters
            Program.MainForm.picExpandClose_Click(sender, e);

            // Minimize Form1
            Program.MainForm.WindowState = FormWindowState.Minimized;

            // Capture the entire screen
            Bitmap screenshot = CaptureScreen();

            // Save the screenshot to the Pictures folder
            SaveScreenshot(screenshot);
            ToastShow("SUCCESS", "Save Successful.");

            // Restore Form1 to normal state
            Program.MainForm.WindowState = FormWindowState.Normal;
            Program.MainForm.picExpand_Click(sender, e);

        }
        private Bitmap CaptureScreen()
        {
            // Create a bitmap to store the screenshot
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            // Create a graphics object from the bitmap
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Capture the entire screen and draw it onto the bitmap
                g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            }

            return bitmap;
        }

        private void SaveScreenshot(Bitmap screenshot)
        {
            // Determine the path to the Pictures folder
            string picturesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Screenshots");

            // Create the Pictures\Screenshots folder if it doesn't exist
            if (!Directory.Exists(picturesFolder))
            {
                Directory.CreateDirectory(picturesFolder);
            }

            // Generate a unique filename for the screenshot (you can customize this)
            string fileName = $"Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";

            // Combine the path to the Pictures folder with the filename
            string filePath = Path.Combine(picturesFolder, fileName);

            // Save the screenshot to the specified file path
            screenshot.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

            // Optionally, you can display a message to the user or open the folder containing the screenshot
            ToastShow("SUCCESS", "Save Successful.");
        }

        private void Menu_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void bunifuGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void picRotate90_Click(object sender, EventArgs e)
        {
            Rotate(1, Orientations.DEGREES_CW_90);
            picRotate90.Visible = false;
            picRotate180.Visible = true;
        }

        private void picRotate180_Click(object sender, EventArgs e)
        {
            Rotate(1, Orientations.DEGREES_CW_180);
            picRotate180.Visible = false;
            picRotate270.Visible = true;
        }

        private void picRotate270_Click(object sender, EventArgs e)
        {
            Rotate(1, Orientations.DEGREES_CW_270);
            picRotate270.Visible = false;
            picRotate.Visible = true;
        }
        private void picRotate_Click(object sender, EventArgs e)
        {
            Rotate(1, Orientations.DEGREES_CW_0);
            picRotate.Visible = false;
            picRotate90.Visible = true;
        }

        public static bool Rotate(uint DisplayNumber, Orientations Orientation)
        {
            if (DisplayNumber == 0)
                throw new ArgumentOutOfRangeException("DisplayNumber", DisplayNumber, "First display is 1.");

            bool result = false;

            DISPLAY_DEVICE d = new DISPLAY_DEVICE();
            d.cb = Marshal.SizeOf(d);

            DEVMODE dm = new DEVMODE();

            if (!NativeMethods.EnumDisplayDevices(null, DisplayNumber - 1, ref d, 0))
                throw new ArgumentOutOfRangeException("DisplayNumber", DisplayNumber, "Number is greater than connected displays.");

            if (0 != NativeMethods.EnumDisplaySettings(
                d.DeviceName, NativeMethods.ENUM_CURRENT_SETTINGS, ref dm))
            {
                if ((dm.dmDisplayOrientation + (int)Orientation) % 2 == 1) // Need to swap height and width?
                {
                    int temp = dm.dmPelsHeight;
                    dm.dmPelsHeight = dm.dmPelsWidth;
                    dm.dmPelsWidth = temp;
                }

                switch (Orientation)
                {
                    case Orientations.DEGREES_CW_90:
                        dm.dmDisplayOrientation = NativeMethods.DMDO_270;
                        break;
                    case Orientations.DEGREES_CW_180:
                        dm.dmDisplayOrientation = NativeMethods.DMDO_180;
                        break;
                    case Orientations.DEGREES_CW_270:
                        dm.dmDisplayOrientation = NativeMethods.DMDO_90;
                        break;
                    case Orientations.DEGREES_CW_0:
                        dm.dmDisplayOrientation = NativeMethods.DMDO_DEFAULT;
                        break;
                    default:
                        break;
                }

                DISP_CHANGE ret = NativeMethods.ChangeDisplaySettingsEx(
                    d.DeviceName, ref dm, IntPtr.Zero,
                    DisplaySettingsFlags.CDS_UPDATEREGISTRY, IntPtr.Zero);

                result = ret == 0;
            }

            return result;
        }
        public static void ResetAllRotations()
        {
            try
            {
                uint i = 0;
                while (++i <= 64)
                {
                    Rotate(i, Orientations.DEGREES_CW_0);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Everything is fine, just reached the last display
            }
        }
        internal class NativeMethods
        {
            [DllImport("user32.dll")]
            internal static extern DISP_CHANGE ChangeDisplaySettingsEx(
                string lpszDeviceName, ref DEVMODE lpDevMode, IntPtr hwnd,
                DisplaySettingsFlags dwflags, IntPtr lParam);

            [DllImport("user32.dll")]
            internal static extern bool EnumDisplayDevices(
                string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice,
                uint dwFlags);

            [DllImport("user32.dll", CharSet = CharSet.Ansi)]
            internal static extern int EnumDisplaySettings(
                string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

            public const int DMDO_DEFAULT = 0;
            public const int DMDO_90 = 1;
            public const int DMDO_180 = 2;
            public const int DMDO_270 = 3;

            public const int ENUM_CURRENT_SETTINGS = -1;

        }

        // See: https://msdn.microsoft.com/en-us/library/windows/desktop/dd183565(v=vs.85).aspx
        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
        internal struct DEVMODE
        {
            public const int CCHDEVICENAME = 32;
            public const int CCHFORMNAME = 32;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            [System.Runtime.InteropServices.FieldOffset(0)]
            public string dmDeviceName;
            [System.Runtime.InteropServices.FieldOffset(32)]
            public Int16 dmSpecVersion;
            [System.Runtime.InteropServices.FieldOffset(34)]
            public Int16 dmDriverVersion;
            [System.Runtime.InteropServices.FieldOffset(36)]
            public Int16 dmSize;
            [System.Runtime.InteropServices.FieldOffset(38)]
            public Int16 dmDriverExtra;
            [System.Runtime.InteropServices.FieldOffset(40)]
            public DM dmFields;

            [System.Runtime.InteropServices.FieldOffset(44)]
            Int16 dmOrientation;
            [System.Runtime.InteropServices.FieldOffset(46)]
            Int16 dmPaperSize;
            [System.Runtime.InteropServices.FieldOffset(48)]
            Int16 dmPaperLength;
            [System.Runtime.InteropServices.FieldOffset(50)]
            Int16 dmPaperWidth;
            [System.Runtime.InteropServices.FieldOffset(52)]
            Int16 dmScale;
            [System.Runtime.InteropServices.FieldOffset(54)]
            Int16 dmCopies;
            [System.Runtime.InteropServices.FieldOffset(56)]
            Int16 dmDefaultSource;
            [System.Runtime.InteropServices.FieldOffset(58)]
            Int16 dmPrintQuality;

            [System.Runtime.InteropServices.FieldOffset(44)]
            public POINTL dmPosition;
            [System.Runtime.InteropServices.FieldOffset(52)]
            public Int32 dmDisplayOrientation;
            [System.Runtime.InteropServices.FieldOffset(56)]
            public Int32 dmDisplayFixedOutput;

            [System.Runtime.InteropServices.FieldOffset(60)]
            public short dmColor;
            [System.Runtime.InteropServices.FieldOffset(62)]
            public short dmDuplex;
            [System.Runtime.InteropServices.FieldOffset(64)]
            public short dmYResolution;
            [System.Runtime.InteropServices.FieldOffset(66)]
            public short dmTTOption;
            [System.Runtime.InteropServices.FieldOffset(68)]
            public short dmCollate;
            [System.Runtime.InteropServices.FieldOffset(72)]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;
            [System.Runtime.InteropServices.FieldOffset(102)]
            public Int16 dmLogPixels;
            [System.Runtime.InteropServices.FieldOffset(104)]
            public Int32 dmBitsPerPel;
            [System.Runtime.InteropServices.FieldOffset(108)]
            public Int32 dmPelsWidth;
            [System.Runtime.InteropServices.FieldOffset(112)]
            public Int32 dmPelsHeight;
            [System.Runtime.InteropServices.FieldOffset(116)]
            public Int32 dmDisplayFlags;
            [System.Runtime.InteropServices.FieldOffset(116)]
            public Int32 dmNup;
            [System.Runtime.InteropServices.FieldOffset(120)]
            public Int32 dmDisplayFrequency;
        }

        // See: https://msdn.microsoft.com/en-us/library/windows/desktop/dd183569(v=vs.85).aspx
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        internal struct DISPLAY_DEVICE
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;
            [MarshalAs(UnmanagedType.U4)]
            public DisplayDeviceStateFlags StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }

        // See: https://msdn.microsoft.com/de-de/library/windows/desktop/dd162807(v=vs.85).aspx
        [StructLayout(LayoutKind.Sequential)]
        internal struct POINTL
        {
            long x;
            long y;
        }

        internal enum DISP_CHANGE : int
        {
            Successful = 0,
            Restart = 1,
            Failed = -1,
            BadMode = -2,
            NotUpdated = -3,
            BadFlags = -4,
            BadParam = -5,
            BadDualView = -6
        }

        // http://www.pinvoke.net/default.aspx/Enums/DisplayDeviceStateFlags.html
        [Flags()]
        internal enum DisplayDeviceStateFlags : int
        {
            /// <summary>The device is part of the desktop.</summary>
            AttachedToDesktop = 0x1,
            MultiDriver = 0x2,
            /// <summary>The device is part of the desktop.</summary>
            PrimaryDevice = 0x4,
            /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
            MirroringDriver = 0x8,
            /// <summary>The device is VGA compatible.</summary>
            VGACompatible = 0x10,
            /// <summary>The device is removable; it cannot be the primary display.</summary>
            Removable = 0x20,
            /// <summary>The device has more display modes than its output devices support.</summary>
            ModesPruned = 0x8000000,
            Remote = 0x4000000,
            Disconnect = 0x2000000
        }

        // http://www.pinvoke.net/default.aspx/user32/ChangeDisplaySettingsFlags.html
        [Flags()]
        internal enum DisplaySettingsFlags : int
        {
            CDS_NONE = 0,
            CDS_UPDATEREGISTRY = 0x00000001,
            CDS_TEST = 0x00000002,
            CDS_FULLSCREEN = 0x00000004,
            CDS_GLOBAL = 0x00000008,
            CDS_SET_PRIMARY = 0x00000010,
            CDS_VIDEOPARAMETERS = 0x00000020,
            CDS_ENABLE_UNSAFE_MODES = 0x00000100,
            CDS_DISABLE_UNSAFE_MODES = 0x00000200,
            CDS_RESET = 0x40000000,
            CDS_RESET_EX = 0x20000000,
            CDS_NORESET = 0x10000000
        }

        [Flags()]
        internal enum DM : int
        {
            Orientation = 0x00000001,
            PaperSize = 0x00000002,
            PaperLength = 0x00000004,
            PaperWidth = 0x00000008,
            Scale = 0x00000010,
            Position = 0x00000020,
            NUP = 0x00000040,
            DisplayOrientation = 0x00000080,
            Copies = 0x00000100,
            DefaultSource = 0x00000200,
            PrintQuality = 0x00000400,
            Color = 0x00000800,
            Duplex = 0x00001000,
            YResolution = 0x00002000,
            TTOption = 0x00004000,
            Collate = 0x00008000,
            FormName = 0x00010000,
            LogPixels = 0x00020000,
            BitsPerPixel = 0x00040000,
            PelsWidth = 0x00080000,
            PelsHeight = 0x00100000,
            DisplayFlags = 0x00200000,
            DisplayFrequency = 0x00400000,
            ICMMethod = 0x00800000,
            ICMIntent = 0x01000000,
            MediaType = 0x02000000,
            DitherType = 0x04000000,
            PanningWidth = 0x08000000,
            PanningHeight = 0x10000000,
            DisplayFixedOutput = 0x20000000
        }

        private void picRotateReset_Click(object sender, EventArgs e)
        {
            ResetAllRotations();
        }

        public void SetPowerMode(string[] args)
        {
            try
            {
                ReadConfig();

                if (args.Length == 0)
                {
                    uint result = PowerGetEffectiveOverlayScheme(out Guid currentMode);
                    if (result == 0)
                    {
                        Console.WriteLine(currentMode);
                        if (currentMode == PowerMode.BetterBattery)
                        {
                            Console.WriteLine("Better battery");
                        }
                        else if (currentMode == PowerMode.BetterPerformance)
                        {
                            Console.WriteLine("Better performance");
                        }
                        else if (currentMode == PowerMode.BestPerformance)
                        {
                            Console.WriteLine("Best performance");
                        }
                    }
                    else
                    {
                        // Handle the result or throw an exception if needed.
                        Console.Error.WriteLine($"Failed to get power mode. Result: {result}\n");
                        return;
                    }
                }
                else if (args.Length == 1)
                {
                    string parameter = args[0].ToLower();
                    Guid powerMode;

                    if (parameter == "/?" || parameter == "-?")
                    {
                        Usage();
                        // Implement exit code or actions here.
                        return;
                    }
                    else if (parameter == "BetterBattery".ToLower())
                    {
                        powerMode = PowerMode.BetterBattery;
                    }
                    else if (parameter == "BetterPerformance".ToLower())
                    {
                        powerMode = PowerMode.BetterPerformance;
                    }
                    else if (parameter == "BestPerformance".ToLower())
                    {
                        powerMode = PowerMode.BestPerformance;
                    }
                    else
                    {
                        try
                        {
                            powerMode = new Guid(parameter);
                        }
                        catch (Exception)
                        {
                            Console.Error.WriteLine("Failed to parse GUID.\n");
                            Usage();
                            // Implement exit code or actions here.
                            return;
                        }
                    }
                    uint result = PowerSetActiveOverlayScheme(powerMode);

                    if (result == 0)
                    {
                        Console.WriteLine("Set power mode to {0}.", powerMode);
                    }
                    else
                    {
                        Console.Error.WriteLine("Failed to set power mode.\n");
                        Usage();
                        // Implement exit code or actions here.
                        return;
                    }
                }
                else
                {
                    Usage();
                    // Implement exit code or actions here.
                    return;
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine("{0}: {1}\n{2}", exception.GetType(), exception.Message, exception.StackTrace);
                Console.WriteLine();
                Usage();
                // Implement exit code or actions here.
                return;
            }

            // Implement exit code or actions here for successful execution.
            Console.WriteLine("Power mode setting completed successfully.");
        }


        private static void Usage()
        {
            Console.WriteLine(
                "PowerMode (GPLv3); used to set the active power mode on Windows 10, version 1709 or later\n" +
                "https://github.com/AaronKelley/PowerMode\n" +
                "\n" +
                "  PowerMode                    Report the current power mode\n" +
                "  PowerMode BetterBattery      Set the system to \"better battery\" mode\n" +
                "  PowerMode BetterPerformance  Set the system to \"better performance\" mode\n" +
                "  PowerMode BestPerformance    Set the system to \"best performance\" mode\n" +
                "  PowerMode <GUID>             Set the system to the mode identified by the GUID"
            );
        }

        private static void ReadConfig()
        {
            if (ConfigurationManager.AppSettings["BetterBatteryGuid"] != null)
            {
                PowerMode.BetterBattery = new Guid(ConfigurationManager.AppSettings["BetterBatteryGuid"]);
            }
            if (ConfigurationManager.AppSettings["BetterPerformanceGuid"] != null)
            {
                PowerMode.BetterPerformance = new Guid(ConfigurationManager.AppSettings["BetterPerformanceGuid"]);
            }
            if (ConfigurationManager.AppSettings["BestPerformanceGuid"] != null)
            {
                PowerMode.BestPerformance = new Guid(ConfigurationManager.AppSettings["BestPerformanceGuid"]);
            }
        }

        private static class PowerMode
        {
            public static Guid BetterBattery = new Guid("961cc777-2547-4f9d-8174-7d86181b8a7a");
            public static Guid BetterPerformance = new Guid("3af9B8d9-7c97-431d-ad78-34a8bfea439f");
            public static Guid BestPerformance = new Guid("ded574b5-45a0-4f42-8737-46345c09c238");
        }

        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerGetEffectiveOverlayScheme")]
        private static extern uint PowerGetEffectiveOverlayScheme(out Guid EffectiveOverlayPolicyGuid);

        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerSetActiveOverlayScheme")]
        private static extern uint PowerSetActiveOverlayScheme(Guid OverlaySchemeGuid);

        private void picEnergySaver_Click(object sender, EventArgs e)
        {
            PowerSetActiveOverlayScheme(PowerMode.BetterBattery);


            // Inform the user about the mode change.
            ToastShow("Energy Saver", "Power mode set to Better Battery.");

        }

        private void picAlwaysOn_Click(object sender, EventArgs e)
        {
            picAlwaysOn.Visible = false;
            picAlwaysOnOFF.Visible = true;
        }

        private void picAlwaysOnOFF_Click(object sender, EventArgs e)
        {
            
            picAlwaysOn.Visible = true;
            picAlwaysOnOFF.Visible = false;
            timer1.Stop();

        }
        private void PicAlwaysOnTimer_Tick(object sender, EventArgs e)
        {
            // Move the mouse pointer to a specific location
            Cursor.Position = new System.Drawing.Point(0, Cursor.Position.Y + 5);

            // Simulate pressing the Shift key one times
            for (int i = 0; i < 1; i++)
            {
                SendKeys.SendWait("+");
            }
        }
    }
}

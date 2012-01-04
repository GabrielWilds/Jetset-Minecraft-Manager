using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DiskPath = System.IO.Path;

namespace Core
{
    public class ProcessMan
    {
        private static string[] _batchText = { "@echo off", "setlocal", "set APPDATA=%~1", "\"%~2\" %~3 \"%~4\"" };

        public static void OpenProfileFolder(Core.MCProfile profile)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo explorer = new System.Diagnostics.ProcessStartInfo("explorer.exe");
            explorer.Arguments = profile.Path + "\\.minecraft";
            process.StartInfo = explorer;
            process.Start();
        }

        public static void LaunchGame(Core.MCProfile profile, string curDirectory)
        {
            string[] arg = new string[4];
            arg[0] = AddQuotes(profile.Path);
            arg[1] = AddQuotes(FileMan.GetJavaInstallationPath() + "\\bin\\javaw.exe");
            arg[2] = AddQuotes("-jar");
            arg[3] = AddQuotes(curDirectory + "\\minecraft.exe");
            string batString = AddQuotes(curDirectory + "\\mineBat.bat");
            string arguments = arg[0] + " " + arg[1] + " " + arg[2] + " " + arg[3];
            LaunchProcess(arguments, "\\minecraft.exe", curDirectory);
        }

        public static void LaunchCartograph(Core.MCProfile profile, string curDirectory)
        {
            string saveString = profile.Path + "\\.minecraft\\saves";
            string[] saves = Directory.GetDirectories(saveString);

            string[] arg = new string[3];
            arg[0] = AddQuotes(profile.Path);
            arg[1] = AddQuotes(profile.Path + "\\.minecraft" + "\\Cartograph_G_Renderer.exe");
            string batString = AddQuotes(curDirectory + "\\mineBat.bat");
            string arguments = arg[0] + " " + arg[1];
            LaunchProcess(arguments, "\\Cartograph_G.exe", curDirectory);
        }

        public static void LaunchProcess(string args, string proc, string curDirectory)
        {
            System.Diagnostics.Process bat = new System.Diagnostics.Process();
            if(!File.Exists(curDirectory + "\\localApp.bat"))
                File.WriteAllLines(curDirectory + "\\localApp.bat", _batchText);

            System.Diagnostics.ProcessStartInfo batinfo = new System.Diagnostics.ProcessStartInfo(curDirectory + "\\localApp.bat");
            batinfo.CreateNoWindow = true;
            batinfo.UseShellExecute = false;
            batinfo.Arguments = args;
            bat.StartInfo = batinfo;
            bat.Start();
            
        }

        public static string AddQuotes(string input)
        {
            return "\"" + input + "\"";
        }
    }
}

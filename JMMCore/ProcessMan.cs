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
        public static void LaunchGame(Core.MCProfile profile, string curDirectory)
        {
            string[] arg = new string[4];
            arg[0] = AddQuotes(profile.Path);
            arg[1] = AddQuotes(FileMan.GetJavaInstallationPath() + "\\bin\\javaw.exe");
            arg[2] = AddQuotes("-Xmx2048M -Xms2048M -jar " + curDirectory + "\\minecraft.exe");
            string batString = AddQuotes(curDirectory + "\\mineBat.bat");
            string arguments = arg[0] + " " + arg[1] + " " + arg[2];
            LaunchProcess(arguments, "\\minecraft.exe", curDirectory);
        }

        public static void LaunchCartograph(Core.MCProfile profile, string curDirectory)
        {
            string saveString = profile.Path + "\\.minecraft\\saves";
            string[] saves = Directory.GetDirectories(saveString);

            string[] arg = new string[3];
            arg[0] = AddQuotes(profile.Path);
            arg[1] = AddQuotes(profile.Path + "\\.minecraft" + "\\Cartograph_G_Renderer.exe");
            arg[2] = " ";
            string batString = AddQuotes(curDirectory + "\\mineBat.bat");
            string arguments = arg[0] + " " + arg[1] + " " + arg[2];
            LaunchProcess(arguments, "\\Cartograph_G.exe", curDirectory);
        }

        public static void LaunchProcess(string args, string proc, string curDirectory)
        {
            System.Diagnostics.Process bat = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo batinfo = new System.Diagnostics.ProcessStartInfo(curDirectory + "\\localApp.bat");
            batinfo.CreateNoWindow = false;
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

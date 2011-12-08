using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using DiskPath = System.IO.Path;
using Microsoft.Win32;
using SysConsole = System.Console;
using Core;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            SysConsole.WriteLine("Minecraft Profile Loader");
            SysConsole.WriteLine("==========");
            SysConsole.WriteLine();
            string curDirectory = Directory.GetCurrentDirectory();
            //string curDirectory = "C:\\Games\\Minecraft";
            string profileDirectory = DiskPath.Combine(curDirectory, "profiles");

            bool loop = true;
            while (loop)
            {
                MCProfile[] profiles = FileMan.GetProfiles(profileDirectory);

                SysConsole.WriteLine("Minecraft Profile Loader");
                SysConsole.WriteLine("==========");
                SysConsole.WriteLine("Type the number corresponding with your profile selection at the prompt:");

                ListProfiles(profiles);
                SysConsole.WriteLine(profiles.Length + ". Create New Profile");

                int selection = Input.GetUserNum(0, profiles.Length);
                if (selection == profiles.Length)
                {
                    bool loop2 = true;
                    while (loop2)
                    {
                        SysConsole.WriteLine("Enter the name of your new profile");
                        string profileName = SysConsole.ReadLine();
                        string newProfile = profileDirectory + "\\" + FileMan.ReplaceForbiddenChars(profileName);
                        SysConsole.WriteLine(newProfile);
                        if (Directory.Exists(newProfile))
                        {
                            SysConsole.WriteLine("This profile already exists!");
                        }
                        else
                        {
                            Directory.CreateDirectory(newProfile);
                            File.WriteAllText(newProfile + "\\profileinfo.txt", profileName);
                            Directory.CreateDirectory(newProfile + "\\.minecraft");
                            File.Copy(profiles[0].Path + "\\.minecraft\\lastlogin", newProfile + "\\.minecraft\\lastlogin");
                            loop2 = false;
                        }
                    }
                }

                else
                {
                    SysConsole.WriteLine();
                    SysConsole.WriteLine("You chose " + profiles[selection].Name + ". Pick an option:");
                    SysConsole.WriteLine("1. Launch Minecraft using this profile");
                    SysConsole.WriteLine("2. Launch Cartograph using this profile");
                    SysConsole.WriteLine("3. Rename this profile");
                    SysConsole.WriteLine("4. Copy this profile's contents into a newly named profile");
                    SysConsole.WriteLine("5. Delete this profile");
                    SysConsole.WriteLine("6. Go back to profile selection");
                    int option = Input.GetUserNum(1, 5);

                    switch (option)
                    {
                        case 1:
                            ProcessMan.LaunchGame(profiles[selection], curDirectory);
                            loop = false;

                            break;
                        case 2:
                            if (File.Exists(curDirectory + "\\Cartograph_G_Renderer.exe"))
                            {
                                ProcessMan.LaunchCartograph(profiles[selection], curDirectory);
                                loop = false;
                            }
                            else
                                SysConsole.WriteLine("Cartograph not found. Please place Cartograph in the same folder as Multi Minecraft Manager");                           
                            break;
                        case 3:
                            SysConsole.WriteLine("Enter new name:");
                            string rename = SysConsole.ReadLine();
                            FileMan.RenameProfile(profiles[selection], rename);
                            break;
                        case 4:
                            SysConsole.WriteLine("Enter new name:");
                            string copyName = SysConsole.ReadLine();
                            FileMan.CopyProfile(profiles[selection], profileDirectory, copyName);
                            break;
                        case 5:
                            FileMan.DeleteProfile(profiles[selection]);
                            break;
                        default:
                            break;
                    }
                }
                SysConsole.Clear();
            }
        }

        public static void ListProfiles(MCProfile[] profiles)
        {
            for (int i = 0; i < profiles.Length; i++)
                SysConsole.WriteLine(i + ". " + profiles[i].Name);

        }
    }
}
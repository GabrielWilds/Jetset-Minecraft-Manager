using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DiskPath = System.IO.Path;
using Microsoft.Win32;

namespace Core
{
    public class FileMan
    {
        public static Core.MCProfile[] GetProfiles(string profileDirectory)
        {
            string[] folders = Directory.GetDirectories(profileDirectory);
            if (folders.Length == 0)
                return null;
            else
            {
                List<string> profilePaths = new List<string>();
                foreach (string folder in folders)
                {
                    if (Directory.Exists(folder + "\\.minecraft"))
                        profilePaths.Add(folder);
                }

                Core.MCProfile[] profiles = new Core.MCProfile[profilePaths.Count];
                for (int i = 0; i < profiles.Length; i++)
                {
                    string profileName = "";
                    if (File.Exists(profilePaths[i] + "\\profileinfo.txt"))
                    {
                        string[] profileInfo = File.ReadAllLines(profilePaths[i] + "\\profileinfo.txt");
                        profileName = profileInfo[0];
                    }
                    else
                    {
                        Console.WriteLine("Found a seemingly valid profile without a set profile name. The folder name is " + profilePaths[i].Remove(0, profileDirectory.Length + 1) + ".");
                        Console.WriteLine("Please enter a name for the profile. If you'd like to use the folder name for the profile name, just press [enter].");
                        profileName = Console.ReadLine();
                    }

                    if (profileName == "")
                        profileName = profilePaths[i].Remove(0, profileDirectory.Length + 1);

                    profiles[i] = new MCProfile(profilePaths[i], profileName);
                }
                return profiles;
            }
        }

        public static string[] GetProfileNames(Core.MCProfile[] profiles)
        {
            string[] profileNames = new string[profiles.Length];
            for (int i = 0; i < profileNames.Length; i++)
                profileNames[i] = profiles[i].Name;

            return profileNames;
        }

        public static bool CheckForExistingProfile(MCProfile profile, string profileDirectory)
        {
            MCProfile[] profiles = GetProfiles(profileDirectory);
            bool foundProfile = false;
            for (int i = 0; i < profiles.Length; i++)
            {
                if (profiles[i] == profile)
                    foundProfile = true;
            }
            return foundProfile;
        }

        public static bool CheckForExistingProfile(string profileName, string profileDirectory)
        {
            MCProfile[] profiles = GetProfiles(profileDirectory);
            bool foundProfile = false;
            for (int i = 0; i < profiles.Length; i++)
            {
                if (profiles[i].Name == profileName)
                    foundProfile = true;
            }
            return foundProfile;
        }

        public static void CopyProfile(Core.MCProfile profile, string profileDirectory, string newName)
        {
            string folderName = ReplaceForbiddenChars(newName);
            string newProfile = DiskPath.Combine(profileDirectory + "\\" + folderName);

            if (!Directory.Exists(newProfile))
                Directory.CreateDirectory(newProfile);
            if (!File.Exists(newProfile + "\\profileinfo.txt"))
                File.WriteAllText(newProfile + "\\profileinfo.txt", newName);

            CopyFolder(profile.Path, newProfile);
        }

        public static void CopyFolder(string source, string target)
        {
            string[] subFolders = Directory.GetDirectories(source);
            foreach (string folder in subFolders)
            {
                string targetFolder = Path.Combine(target + "\\" + folder.Remove(0, source.Length + 1));
                if (!Directory.Exists(targetFolder))
                    Directory.CreateDirectory(targetFolder);

                CopyFolder(folder, targetFolder);
            }

            string[] files = Directory.GetFiles(source);
            foreach (string file in files)
            {
                string targetFile = Path.Combine(target + "\\" + file.Remove(0, source.Length + 1));
                if (!File.Exists(targetFile))
                    File.Copy(file, targetFile);
            }
        }

        public static void CreateNewProfile(string name, string profileDirectory, MCProfile loginSourceProfile)
        {
            string newProfile = profileDirectory + "\\" + ReplaceForbiddenChars(name);
            Directory.CreateDirectory(newProfile);
            File.WriteAllText(newProfile + "\\profileinfo.txt", name);
            Directory.CreateDirectory(newProfile + "\\.minecraft");
            string loginPath = loginSourceProfile.Path + "\\.minecraft\\lastlogin";
            if (File.Exists(loginPath))
                File.Copy(loginPath, newProfile + "\\.minecraft\\lastlogin");
        }

        public static void RenameProfile(Core.MCProfile profile, string newName)
        {
            File.WriteAllText(profile.Path + "\\profileinfo.txt", newName);
        }

        public static void DeleteProfile(Core.MCProfile profile)
        {
            Directory.Delete(profile.Path, true);
        }

        public static string ReplaceForbiddenChars(string input)
        {
            input = input.Replace('\\', '-');
            input = input.Replace('/', '-');
            input = input.Replace(':', ';');
            input = input.Replace('*', ',');
            input = input.Replace('?', '.');
            input = input.Replace('\"', '\'');
            input = input.Replace('<', '-');
            input = input.Replace('>', '-');
            input = input.Replace('|', '-');
            input = input.Replace('\"', '\'');
            return input;
        }

        public static String GetJavaInstallationPath()
        {
            String javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment";
            if (Environment.Is64BitOperatingSystem)
            {
                using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(javaKey))
                {
                    String currentVersion = baseKey.GetValue("CurrentVersion").ToString();
                    using (var homeKey = baseKey.OpenSubKey(currentVersion))
                        return homeKey.GetValue("JavaHome").ToString();
                }
            }
            else
            {
                using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(javaKey))
                {
                    String currentVersion = baseKey.GetValue("CurrentVersion").ToString();
                    using (var homeKey = baseKey.OpenSubKey(currentVersion))
                        return homeKey.GetValue("JavaHome").ToString();
                }
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.IO;
using Core;
using System.Windows;
using System.ComponentModel;

namespace UI
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        SolidColorBrush _brushColor = new SolidColorBrush();
        Core.MCProfile[] _profiles;
        MCProfile _selectedProfile;

        public SolidColorBrush ColorName
        {
            get { return _brushColor; }
        }

        public MCProfile[] Profiles
        {
            get { return _profiles; }
            set
            {
                _profiles = value;
                RaisePropertyChanged("Profiles");
            }
        }

        public MCProfile SelectedProfile
        {
            get { return _selectedProfile; }
            set
            {
                _selectedProfile = value;
                RaisePropertyChanged("SelectedProfile");
            }
        }

        public MainWindowViewModel()
        {
            _brushColor.Color = Colors.LightCoral;
            EnsureNeededFolders();
            GetProfiles();
        }

        public void GetProfiles()
        {
            Profiles = FileMan.GetProfiles(App.CurrentDirectory + "\\profiles");
            SelectedProfile = Profiles[0];
        }

        public void LoadProfile()
        {
            if (File.Exists(App.CurrentDirectory + "\\minecraft.exe"))
            {
                ProcessMan.LaunchGame(SelectedProfile, App.CurrentDirectory);
                MessageBox.Show(SelectedProfile.Name + " loaded, closing Jetset Minecraft Manager");
                Environment.Exit(0);
            }
            else
                MessageBox.Show("Place minecraft.exe (the launcher) in the same folder as this application! It's needed to actually launch the game!", "ERROR");


        }

        public void CopyProfile()
        {
            Window newProfileWindow = new NewProfileWindow("Copy Profile", "Copy name:", "Copy", "Copy", SelectedProfile);
            newProfileWindow.ShowDialog();
            GetProfiles();
        }

        public void RenameProfile()
        {
            Window newProfileWindow = new NewProfileWindow("Rename Profile", "New name:", "Rename", "Rename", SelectedProfile);
            newProfileWindow.ShowDialog();
            GetProfiles();
        }

        public void NewProfile()
        {
            Window newProfileWindow = new NewProfileWindow("New Profile", "Profile name:", "Create", "New", SelectedProfile);
            newProfileWindow.ShowDialog();
            GetProfiles();
        }

        public void DeleteProfile()
        {
            MessageBoxResult results = MessageBox.Show("Are you sure you want to delete the profile \"" + SelectedProfile.Name + "\"?", "", MessageBoxButton.YesNo);
            if (results == MessageBoxResult.Yes)
            {
                FileMan.DeleteProfile(SelectedProfile);
                GetProfiles();
            }
        }

        public void EnsureNeededFolders()
        {
            string profiles = App.CurrentDirectory + "\\profiles";
            if (!Directory.Exists(profiles))
            {
                MessageBox.Show("No \"profiles\" folder found! Creating one now!", "missing profiles folder");
                Directory.CreateDirectory(profiles);
                CreateDefaultProfile();
            }
            if (!Directory.EnumerateFileSystemEntries(profiles).Any())
            {
                CreateDefaultProfile();
            }
        }

        public void CreateDefaultProfile()
        {
            MCProfile importProfile = new MCProfile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft", "default");
            string defaultInstall = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft";
            
            if (Directory.Exists(defaultInstall))
            {
                MessageBox.Show("Importing your existing Minecraft install to the Jetset Minecraft Manager folder. Enter a name for this profile at the prompt.");
                Window newProfileWindow = new NewProfileWindow("Name Profile", "Profile name:", "Import", "Default", importProfile);
                newProfileWindow.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("No existing Minecraft install found! Go to http:\\www.minecraft.net and buy and/or install Minecraft!", "Why did you even download this app?");
                Environment.Exit(0);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

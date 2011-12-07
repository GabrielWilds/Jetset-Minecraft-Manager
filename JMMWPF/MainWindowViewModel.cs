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
        string _curDirectory = "";
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
                RaisePropertyChanged("SelectedProfiles");
            }
        }

        public MainWindowViewModel()
        {
            _brushColor.Color = Colors.LightCoral;
            //_curDirectory = Directory.GetCurrentDirectory();
            _curDirectory = "C:\\Games\\Minecraft";
            LoadProfiles();
        }

        public void LoadProfiles()
        {
            Profiles = FileMan.GetProfiles(Path.Combine(_curDirectory, "profiles"));
            SelectedProfile = Profiles[0];
        }

        public void LoadProfile()
        {
            ProcessMan.LaunchGame(SelectedProfile, _curDirectory);
        }

        public void CopyProfile()
        {
            MessageBox.Show("Function not yet ready!", "Error", MessageBoxButton.OK);
        }

        public void RenameProfile()
        {
            MessageBox.Show("Function not yet ready!", "Error", MessageBoxButton.OK);
        }

        public void NewProfile()
        {
            Window newProfileWindow = new NewProfileWindow();
            newProfileWindow.ShowDialog();
            LoadProfiles();
        }

        public void DeleteProfile()
        {
            MessageBoxResult results = MessageBox.Show("Are you sure you want to delete the profile \"" + SelectedProfile.Name + "\"?", "", MessageBoxButton.YesNo);
            if (results == MessageBoxResult.Yes)
            {
                FileMan.DeleteProfile(SelectedProfile);
                LoadProfiles();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

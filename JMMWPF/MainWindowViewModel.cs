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
            GetProfiles();
        }

        public void GetProfiles()
        {
            Profiles = FileMan.GetProfiles(App.CurDirectory + "\\profiles");
            SelectedProfile = Profiles[0];
        }

        public void LoadProfile()
        {
            ProcessMan.LaunchGame(SelectedProfile, App.CurDirectory);
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.IO;
using Core;

namespace UI
{
    class MainWindowViewModel
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
        }

        public MCProfile SelectedProfile
        {
            get { return _selectedProfile; }
            set { _selectedProfile = value; }
        }

        public MainWindowViewModel()
        {
            _brushColor.Color = Colors.LightCoral;
            //_curDirectory = Directory.GetCurrentDirectory();
            _curDirectory = "C:\\Games\\Minecraft";
            _profiles = FileMan.GetProfiles(Path.Combine(_curDirectory, "profiles"));
            _selectedProfile = _profiles[0];
        }

        public void LoadProfile()
        {
            ProcessMan.LaunchGame(SelectedProfile, _curDirectory);
        }

        

    }
}

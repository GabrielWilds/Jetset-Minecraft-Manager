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
        string[] _profileNames;
        SolidColorBrush _brushColor = new SolidColorBrush();
        Core.MCProfile[] _profiles;

        public SolidColorBrush ColorName
        {
            get { return _brushColor; }
        }

        public string[] Profiles
        {
            get { return _profileNames; }
        }

        public MainWindowViewModel()
        {
            _brushColor.Color = Colors.LightCoral;
            //_curDirectory = Directory.GetCurrentDirectory();
            _curDirectory = "C:\\Games\\Minecraft";
            _profiles = FileMan.GetProfiles(Path.Combine(_curDirectory, "profiles"));
            _profileNames = FileMan.GetProfileNames(_profiles);
            //_profileNames = new string[]{"Profile 1", "Profile 2"};
        }

        

    }
}

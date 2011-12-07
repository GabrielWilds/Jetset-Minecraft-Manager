using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Windows;

namespace UI
{
    class NewProfileWindowViewModel
    {
        public NewProfileWindowViewModel(Window window, string _action)
        {
            _window = window;
            Action = _action;
        }

        Window _window;
        public string NewProfileName
        {
            get;
            set;
        }

        public string Action
        {
            get;
            set;
        }

        public void ButtonAction()
        {
            switch(Action)
            {
                case "New":
                    CreateProfile();
                    break;
                case "Copy":
                    CopyProfile();
                    break;
                case "Rename":
                    RenameProfile();
                    break;
                default:
                    break;
            }
        }

        public void CreateProfile()
        {
            string profileDirectory = "C:\\Games\\Minecraft\\profiles";
            if (!FileMan.CheckForExistingProfile(NewProfileName, profileDirectory))
            {
                FileMan.CreateNewProfile(NewProfileName, profileDirectory);
                MessageBox.Show("New Profile " + NewProfileName + " was successfully created!");
                _window.Close();
            }
            else
                MessageBox.Show("This Profile Already Exists! Enter a different profile name.", "Error", MessageBoxButton.OK);
        }

        public void CopyProfile()
        {
        }

        public void RenameProfile()
        {
        }
    }
}

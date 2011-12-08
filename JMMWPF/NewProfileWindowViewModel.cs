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
        public NewProfileWindowViewModel(Window window, string _action, MCProfile selectedProfile)
        {
            _window = window;
            Action = _action;
            Profile = selectedProfile;
        }

        string _profileDirectory = "C:\\Games\\Minecraft\\profiles";
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

        public MCProfile Profile
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
            if (!FileMan.CheckForExistingProfile(NewProfileName, _profileDirectory))
            {
                FileMan.CreateNewProfile(NewProfileName, _profileDirectory);
                MessageBox.Show("New Profile " + NewProfileName + " was successfully created!");
                _window.Close();
            }
            else
                MessageBox.Show("This profile already exists! Enter a different profile name.", "Error", MessageBoxButton.OK);
        }

        public void CopyProfile()
        {
            if (!FileMan.CheckForExistingProfile(Profile, _profileDirectory))
            {
                FileMan.CopyProfile(Profile, _profileDirectory, NewProfileName);
                MessageBox.Show(Profile.Name + " was successfully copied to " + NewProfileName);
                _window.Close();
            }
            else
                MessageBox.Show("This new profile name already exists! Enter a different profile name.", "Error", MessageBoxButton.OK);
        }

        public void RenameProfile()
        {
            if (!FileMan.CheckForExistingProfile(Profile, _profileDirectory))
            {
                FileMan.RenameProfile(Profile, NewProfileName);
                MessageBox.Show(Profile.Name + " was successfully renamed to " + NewProfileName);
                _window.Close();
            }
            else
                MessageBox.Show("The entered name is already taken by an existing profile! Enter a different profile name.", "Error", MessageBoxButton.OK);

        }
    }
}

﻿using UnityEngine;
using UnityEngine.UI;
using SFB;

using Ultraleap.TouchFree.ServiceShared;

namespace Ultraleap.TouchFree.ServiceUI
{
    public class AdvancedSettingsScreen : MonoBehaviour
    {
        [Header("Resolution")]
        public InputField resolutionWidth;
        public InputField resolutionHeight;

        [Header("File Location")]
        public InputField fileLocation;

        private void OnEnable()
        {
            resolutionWidth.text = ConfigManager.PhysicalConfig.ScreenWidthPX.ToString();
            resolutionHeight.text = ConfigManager.PhysicalConfig.ScreenHeightPX.ToString();

            fileLocation.text = ConfigFileUtils.ConfigFileDirectory;

            // This combination allows users to highlight the text (to copy if desired) without
            // being able to edit
            fileLocation.interactable = true;
            fileLocation.readOnly = true;
        }

        public void SetResolution()
        {
            var width = int.Parse(resolutionWidth.text);
            var height = int.Parse(resolutionHeight.text);

            if (width < 200)
            {
                width = 200;
                resolutionWidth.text = "200";
            }

            if (height < 200)
            {
                height = 200;
                resolutionHeight.text = "200";
            }

            ConfigManager.PhysicalConfig.ScreenWidthPX = width;
            ConfigManager.PhysicalConfig.ScreenHeightPX = height;

            ConfigManager.PhysicalConfig.ConfigWasUpdated();
            ConfigManager.PhysicalConfig.SaveConfig();
        }

        public void SetFileLocation()
        {
            string[] paths = StandaloneFileBrowser.OpenFolderPanel("", "", false);

            if (paths.Length > 0)
            {
                if (ConfigFileUtils.ChangeConfigFileDirectory(paths[0]))
                {
                    fileLocation.text = paths[0];
                    ConfigManager.LoadConfigsFromFiles();
                }
            }
        }
    }
}
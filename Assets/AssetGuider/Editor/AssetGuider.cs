﻿/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AssetGuider.cs
 *  Description  :  Capture the event of open Asset in Unity and call OS
 *                  open the Asset use the default application.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/7/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Developer.AssetGuider
{
    public static class AssetGuider
    {
        #region Field and Property
        private const string SettingsPath = "Assets/AssetGuider/Settings/AssetGuiderSettings.asset";
        #endregion

        #region Private Method
        [MenuItem("Tool/Asset Guider Settings &G")]
        private static void FocusSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath(SettingsPath, typeof(AssetGuiderSettings)) as AssetGuiderSettings;
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<AssetGuiderSettings>();
                AssetDatabase.CreateAsset(settings, SettingsPath);
            }
            Selection.activeObject = settings;
        }

        [OnOpenAsset]
        private static bool OnOpenAsset(int instanceID, int line)
        {
            var settings = AssetDatabase.LoadAssetAtPath(SettingsPath, typeof(AssetGuiderSettings)) as AssetGuiderSettings;
            if (settings == null)
                return false;

            var assetPath = Application.dataPath + "/" + AssetDatabase.GetAssetPath(instanceID).Replace("Assets", string.Empty);
            if (Regex.IsMatch(Path.GetExtension(assetPath), settings.extensions))
            {
                Application.OpenURL(assetPath);
                return true;
            }
            else
                return false;
        }
        #endregion
    }
}
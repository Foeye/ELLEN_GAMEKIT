using UnityEditor;
using UnityEngine;

public class TextureMaxSizeFixer
{
    [MenuItem("Tools/Fix Texture Max Size")]
    public static void FixTextureMaxSize()
    {
        string[] guids = AssetDatabase.FindAssets("t:Texture");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

            if (importer != null)
            {
                bool modified = false;

                TextureImporterPlatformSettings platformSettings = importer.GetPlatformTextureSettings("Standalone");
                if (platformSettings.maxTextureSize > 1024)
                {
                    platformSettings.maxTextureSize = 1024;
                    importer.SetPlatformTextureSettings(platformSettings);
                    modified = true;
                }

                platformSettings = importer.GetPlatformTextureSettings("Windows");
                if (platformSettings.maxTextureSize > 1024)
                {
                    platformSettings.maxTextureSize = 1024;
                    importer.SetPlatformTextureSettings(platformSettings);
                    modified = true;
                }

                platformSettings = importer.GetPlatformTextureSettings("OSX");
                if (platformSettings.maxTextureSize > 1024)
                {
                    platformSettings.maxTextureSize = 1024;
                    importer.SetPlatformTextureSettings(platformSettings);
                    modified = true;
                }

                platformSettings = importer.GetPlatformTextureSettings("Linux");
                if (platformSettings.maxTextureSize > 1024)
                {
                    platformSettings.maxTextureSize = 1024;
                    importer.SetPlatformTextureSettings(platformSettings);
                    modified = true;
                }

                if (modified)
                {
                    AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
                    Debug.Log($"Fixed max size for texture: {path}");
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
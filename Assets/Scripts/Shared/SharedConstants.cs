using System;

namespace Assets.Scripts.Shared
{
    public static class SharedConstants
    {
        public static class AppFileConfigs
        {
            public const string ScreenshotFilesFolder = "Screenshots";
            public const string ScreenshotFileName = "Screenshot_";
            public const string SavesFilesFolder = "Saves";
            public const string SavesFileName = "Save_";

            public const int MaxSaveFiles = 1;
            public const int MaxScreenshots = 10;
        }

        public static class AppDefaultSettings
        {
            public const int InventoryMaxSize = 4;

            public const bool IsSoundEnabled = true;
            public const float MainVolume = 1f;
            public const float MusicVolume = 1f;
            public const float EffectsVolume = 1f;

            public const bool EnemyHealthBarVisible = true;
            public const bool DamageTextVisible = true;
            public const bool FpsShow = true;
            public const bool LootPicksAutomatically = true;
        }
    }
}
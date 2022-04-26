using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using Workshop;
using Object = UnityEngine.Object;
using VermilionDLL.HarmonyPain;

namespace VermilionDLL.HarmonyPain
{
    public static class SkinUtil
    {
        public static void GetArtWorks(DirectoryInfo dir) //Get images and artwork
        {
            if (dir.GetDirectories().Length != 0)
            {
                var directories = dir.GetDirectories();
                foreach (var t in directories) GetArtWorks(t);
            }

            foreach (var fileInfo in dir.GetFiles())
            {
                var texture2D = new Texture2D(2, 2);
                texture2D.LoadImage(File.ReadAllBytes(fileInfo.FullName));
                var value = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height),
                    new Vector2(0f, 0f));
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                ModParameters.ArtWorks[fileNameWithoutExtension] = value;
            }
        }

        public static void PreLoadBufIcons() //Add Custom Buff Icons
        {
            foreach (var baseGameIcon in Resources.LoadAll<Sprite>("Sprites/BufIconSheet/")
                         .Where(x => !BattleUnitBuf._bufIconDictionary.ContainsKey(x.name)))
                BattleUnitBuf._bufIconDictionary.Add(baseGameIcon.name, baseGameIcon);
            foreach (var artWork in ModParameters.ArtWorks.Where(x =>
                         !x.Key.Contains("Glow") && !x.Key.Contains("Default") &&
                         !BattleUnitBuf._bufIconDictionary.ContainsKey(x.Key)))
                BattleUnitBuf._bufIconDictionary.Add(artWork.Key, artWork.Value);
        }
    }
    public class SkinNames
    {
        public string Name { get; set; }
        public List<SkinParameters> SkinParameters { get; set; }
    }

    public class SkinParameters
    {
        public float PivotPosX { get; set; } = 0;
        public float PivotPosY { get; set; } = 0;
        public float PivotHeadX { get; set; } = 0;
        public float PivotHeadY { get; set; } = 0;
        public float HeadRotation { get; set; } = 0;
        public ActionDetail Motion { get; set; }
        public string FileName { get; set; }
    }

}

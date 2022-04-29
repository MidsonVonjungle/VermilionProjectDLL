using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Mod;
using UI;
using UnityEngine;
using VermilionDLL.HarmonyPain;

namespace VermilionDLL.Harmony_Pain
{
    public class IconAdder
    {

        public static void AddIcon(UISpriteDataManager __instance)
        {
            try
            {
                // W+H will get overwritten.
                Texture2D texture = new Texture2D(2, 2); // Initialise empty texture w/ width & height.
                Texture2D textureGlow = new Texture2D(2, 2); // Same thing as above, but for glow.
                                                             
                var bookIconDir = new DirectoryInfo(ModParameters.Path + "/ArtWork"); //Don't mind this, this is so that it connects to the ArtWork folder instead and it works
                //var bookIconDir = new DirectoryInfo(ResourceDir + "/BookIcon");// Looks for BookIcon folder in Resource.
                texture.LoadImage(File.ReadAllBytes(bookIconDir + "/VermilionIcon.png")); // Load image into texture var; replaces width & height to new texture.
                textureGlow.LoadImage(File.ReadAllBytes(bookIconDir + "/VermilionIconGlow.png")); // Same as above, but for glow. This can be the same texture if neccecary.
                UIIconManager.IconSet YourIcon = new UIIconManager.IconSet
                {
                    type = "VermilionIcon", //Icon/Story Type.
                    icon = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100), // Creates new sprite icon from texture.
                    color = Color.white, // Icon set is also used for things like stagger resist; this color modifies color for things like HP and BP.
                    iconGlow = Sprite.Create(textureGlow, new Rect(0f, 0f, textureGlow.width, textureGlow.height), new Vector2(0.5f, 0.5f), 100),
                    colorGlow = Color.white,
                };
                // Adds Icon to icon list.
                __instance._storyicons.Add(YourIcon);
            }
            catch
            {
                Singleton<ModContentManager>.Instance.AddErrorLog("Failed to load icon");
            }
		}
    } 
}

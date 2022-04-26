using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Battle.DiceAttackEffect;
using HarmonyLib;
using UI;
using UnityEngine;
using VermilionDLL.Harmony_Pain;

namespace VermilionDLL.HarmonyPain
{
    public class ModInitializer_Md5488 : ModInitializer
    {
        public override void OnInitializeMod() //Look for stuff to load in mods
        {
            ModParameters.Path = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            new Harmony("TheVermilionCrossExtra.Md5488").PatchAll();
            SkinUtil.GetArtWorks(new DirectoryInfo(ModParameters.Path + "/ArtWork")); //This will make use of a new folder in your Assemblies called 'ArtWork'
            SkinUtil.PreLoadBufIcons(); //This will load Buf Icons
            LocalizeUtil.AddLocalize(); //This will add localization, which you'll need to give a description to your buf

            Harmony harmony = new Harmony("LOR.whatever"); MethodInfo method = typeof(HarmoyPatch_Re21341).GetMethod("BookModel_SetXmlInfo"); //Code exclusively made for creating
            harmony.Patch(typeof(BookModel).GetMethod("SetXmlInfo", AccessTools.all), null, new HarmonyMethod(method), null, null, null);     //exclusive combat pages
                                                                                                                                              //harmony.PatchAll();
            
            harmony.Patch(typeof(UISpriteDataManager).GetMethod("SetStoryIconDictionary", AccessTools.all), new HarmonyMethod(typeof(IconAdder).GetMethod("AddIcon"))); //Book Icon Adder

            typeof(ModInitializer_Md5488).Assembly.GetTypes().ToList<Type>().FindAll((Type x) => x.Name.StartsWith("DiceAttackEffect_")).ForEach(delegate (Type x) //Creating Custom Effects
            {
                ModInitializer_Md5488.CustomEffects[x.Name.Substring(17)] = x;
            });
        }
        public static Dictionary<string, Type> CustomEffects = new Dictionary<string, Type>();
    }
}

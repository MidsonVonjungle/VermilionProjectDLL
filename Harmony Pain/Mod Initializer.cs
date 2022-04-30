using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Battle.DiceAttackEffect;
using HarmonyLib;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using MonoMod.Utils;
using UI;
using UnityEngine;

namespace VermilionDLL.HarmonyPain
{
    public class ModInitializer_Md5488 : ModInitializer
    {
        public override void OnInitializeMod() //Look for stuff to load in mods
        {
            InitParameters();
            MidsonModParameters.Path = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            new Harmony("TheVermilionCrossExtra.Md5488").PatchAll();
            MapStaticUtil.GetArtWorks(new DirectoryInfo(MidsonModParameters.Path + "/ArtWork"));//This will make use of a new folder in your Assemblies called 'ArtWork'
            UnitUtil.ChangeCardItem(ItemXmlDataList.instance, MidsonModParameters.PackageId);
            UnitUtil.ChangePassiveItem(MidsonModParameters.PackageId);
            SkinUtil.PreLoadBufIcons(); //This will load Buf Icons
            SkinUtil.LoadBookSkinsExtra(MidsonModParameters.PackageId);
            LocalizeUtil.AddLocalLocalize(MidsonModParameters.Path, MidsonModParameters.PackageId);
            LocalizeUtil.RemoveError();
            typeof(ModInitializer_Md5488).Assembly.GetTypes().ToList<Type>().FindAll((Type x) => x.Name.StartsWith("DiceAttackEffect_")).ForEach(delegate (Type x) //Creating Custom Effects
            {
                ModInitializer_Md5488.CustomEffects[x.Name.Substring(17)] = x;
            });
        }
        private static void InitParameters()
        {
            ModParameters.PackageIds.Add(MidsonModParameters.PackageId);
            MidsonModParameters.Path =
                Path.GetDirectoryName(
                    Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            ModParameters.Path.Add(MidsonModParameters.Path);
            ModParameters.SpritePreviewChange.AddRange(new Dictionary<string, List<LorId>> //Addthumbnail for a sprite
            {
                { "VermilionCrossThumbnail", new List<LorId> { new LorId(MidsonModParameters.PackageId, 10000001) } },
            });
            ModParameters.OnlyCardKeywords.AddRange(new List<Tuple<List<string>, List<LorId>, LorId>> //Add exclusive combat pages
            {
                new Tuple<List<string>, List<LorId>, LorId>(new List<string> { "VermilionPage_md5488" },
                    new List<LorId> { new LorId(MidsonModParameters.PackageId, 7) },
                    new LorId(MidsonModParameters.PackageId, 10000001))
            });
            ModParameters.BookList.AddRange(new List<LorId>
            {
                new LorId(MidsonModParameters.PackageId,1)
            });
        }
        public static Dictionary<string, Type> CustomEffects = new Dictionary<string, Type>();
    }
}

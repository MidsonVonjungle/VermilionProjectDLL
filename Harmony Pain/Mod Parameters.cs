using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace VermilionDLL.HarmonyPain
{
    public static class ModParameters
    {
        public const string PackageId = "TheVermilionCrossExtra.Md5488";
        public static string Path;
        public static readonly Dictionary<string, Sprite> ArtWorks = new Dictionary<string, Sprite>();
        //public static readonly Dictionary<string, Sprite> CustomEffects = new Dictionary<string, Sprite>();

        public static readonly List<int> PersonalCardList = new List<int> { };
        public static readonly List<int> EgoPersonalCardList = new List<int> { };
        public static readonly List<int> UntransferablePassives = new List<int> { };
        public static List<LorId> BooksIds = new List<LorId>();

        public static readonly List<int> NoInventoryCardList = new List<int> { };

        public static readonly List<SkinNames> SkinParameters = new List<SkinNames>
        {
            new SkinNames
            {
                Name = "ADD SKIN HERE",
                SkinParameters = new List<SkinParameters>
                {
                    new SkinParameters
                    {
                        PivotPosX = float.Parse("0"), PivotPosY = float.Parse("-193"),
                        Motion = ActionDetail.S1, FileName = "S1.png"
                    },
                    new SkinParameters
                    {
                        PivotPosX = float.Parse("-46"), PivotPosY = float.Parse("-239"),
                        Motion = ActionDetail.S2, FileName = "S2.png"
                    }
                }
            }
        };

        public static List<Tuple<string, List<int>, string>> SkinNameIds = new List<Tuple<string, List<int>, string>>
        {
            //new Tuple<string, List<int>, string>("ADD EGO SKIN", new List<int> { 1000003, 3 }, "ADD NORMAL SKIN")
        };
        
    }
}
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using HarmonyLib;
using LOR_XML;

namespace VermilionDLL.HarmonyPain
{
    public static class LocalizeUtil
    {
        public static void AddLocalize()
        {
            var dictionary =
                typeof(BattleEffectTextsXmlList).GetField("_dictionary", AccessTools.all)
                    ?.GetValue(Singleton<BattleEffectTextsXmlList>.Instance) as Dictionary<string, BattleEffectText>;
            var files = new DirectoryInfo(ModParameters.Path + "/Localize/" + "en" + "/EffectTexts")
                .GetFiles();
            foreach (var t in files)
                using (var stringReader = new StringReader(File.ReadAllText(t.FullName)))
                {
                    var battleEffectTextRoot =
                        (BattleEffectTextRoot)new XmlSerializer(typeof(BattleEffectTextRoot))
                            .Deserialize(stringReader);
                    foreach (var battleEffectText in battleEffectTextRoot.effectTextList)
                    {
                        dictionary.Remove(battleEffectText.ID);
                        dictionary?.Add(battleEffectText.ID, battleEffectText);
                    }
                }
        }
    }
}
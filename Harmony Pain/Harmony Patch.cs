﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battle.DiceAttackEffect;
using HarmonyLib;
using LOR_DiceSystem;
using UI;
using UnityEngine;
using Workshop;
using Object = UnityEngine.Object;

namespace VermilionDLL.HarmonyPain
{
    [HarmonyPatch]
    public class HarmoyPatch_Re21341
    {
        
        [HarmonyPostfix]
        [HarmonyPatch(typeof(BattleUnitView), "ChangeSkin")]
        public static void
            BattleUnitView_ChangeSkin(BattleUnitView __instance,
                string charName) //Allows changing Keypage skins midgame
        {
            if (!ModParameters.SkinNameIds.Exists(x => x.Item1.Contains(charName))) return;
            var skinInfo =
                typeof(BattleUnitView).GetField("_skinInfo", AccessTools.all)?.GetValue(__instance) as
                    BattleUnitView.SkinInfo;
            skinInfo.state = BattleUnitView.SkinState.Changed;
            skinInfo.skinName = charName;
            var currentMotionDetail = __instance.charAppearance.GetCurrentMotionDetail();
            __instance.DestroySkin();
            var gameObject =
                Object.Instantiate(
                    Singleton<AssetBundleManagerRemake>.Instance.LoadCharacterPrefab(charName, "",
                        out var resourceName), __instance.model.view.characterRotationCenter);
            var workshopBookSkinData =
                Singleton<CustomizingBookSkinLoader>.Instance.GetWorkshopBookSkinData(
                    ModParameters.PackageId, charName);
            gameObject.GetComponent<WorkshopSkinDataSetter>().SetData(workshopBookSkinData);
            __instance.charAppearance = gameObject.GetComponent<CharacterAppearance>();
            __instance.charAppearance.Initialize(resourceName);
            __instance.charAppearance.ChangeMotion(currentMotionDetail);
            __instance.charAppearance.ChangeLayer("Character");
            __instance.charAppearance.SetLibrarianOnlySprites(__instance.model.faction);
            __instance.model.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { charName };
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UnitDataModel), "EquipBook")]
        public static void UnitDataModel_EquipBookPostfix(BookModel newBook, bool force)
        {
            if (force) return;
            if (newBook != null && newBook.ClassInfo.id.packageId == ModParameters.PackageId &&
                ModParameters.SkinNameIds.Any(x =>
                    x.Item2.Contains(newBook.ClassInfo.id.id) && newBook.ClassInfo.CharacterSkin.Contains(x.Item1)))
                newBook.ClassInfo.CharacterSkin = new List<string>
                {
                    ModParameters.SkinNameIds.FirstOrDefault(x => newBook.ClassInfo.CharacterSkin.Contains(x.Item1))
                        ?.Item3
                };
        }
        [HarmonyPostfix, HarmonyPatch(typeof(BookModel), "SetXmlInfo")]
        public static void BookModel_SetXmlInfo(BookModel __instance)
        {
            const string modId = "TheVermilionCrossExtra.md5488";
            if (__instance.ClassInfo.id.packageId == modId)
            {
                List<DiceCardXmlInfo> _onlyCards = __instance.GetOnlyCards();
                _onlyCards.Clear();
                _onlyCards.AddRange(__instance.ClassInfo.EquipEffect.OnlyCard
                   .Select(x => ItemXmlDataList.instance.GetCardItem(new LorId(modId, x)))
                   .Where(x => x != null));
            }
        }
   
        [HarmonyPostfix]
        [HarmonyPatch(typeof(DiceEffectManager), "CreateBehaviourEffect")]
        public static void DiceEffectManager_CreateBehaviourEffect(DiceEffectManager __instance, ref DiceAttackEffect __result, string resource, float scaleFactor, BattleUnitView self, BattleUnitView target, float time)
        {
            bool flag = !string.IsNullOrEmpty(resource) && __result == null && ModInitializer_Md5488.CustomEffects.ContainsKey(resource);
            if (flag)
            {
                Type componentType = ModInitializer_Md5488.CustomEffects[resource];
                DiceAttackEffect diceAttackEffect = new GameObject(resource).AddComponent(componentType) as DiceAttackEffect;
                diceAttackEffect.Initialize(self, target, time);
                diceAttackEffect.SetScale(scaleFactor);
                __result = diceAttackEffect;
            }
        }
        [HarmonyPrefix]
        [HarmonyPatch(typeof(UISpriteDataManager), "Init")]
        public static void UISpriteDataManager_Init(UISpriteDataManager __instance)
        {
            foreach (var artWork in ModParameters.ArtWorks.Where(x =>
                         !x.Key.Contains("Glow") && !__instance._storyicons.Exists(y => y.type.Equals(x.Key))))
                __instance._storyicons.Add(new UIIconManager.IconSet
                {
                    type = artWork.Key,
                    icon = artWork.Value,
                    iconGlow = ModParameters.ArtWorks.FirstOrDefault(x => x.Key.Equals($"{artWork.Key}Glow")).Value ??
                               artWork.Value
                });
        }
    }
}


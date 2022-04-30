using System;
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
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VermilionDLL.Passives
{
    public class PassiveAbility_Crossguard_Md5488 : PassiveAbilityBase
    {
        private static readonly string _packageId = "TheVermilionCrossExtra.md5488";

        public override void OnStartBattle()
        {
            
            
                var battleDiceCardModel =
                    BattleDiceCardModel.CreatePlayingCard(
                        ItemXmlDataList.instance.GetCardItem(new LorId(_packageId, 8)));
                owner.cardSlotDetail.keepCard.AddBehaviours(battleDiceCardModel,
                    battleDiceCardModel.CreateDiceCardBehaviorList());
                owner.allyCardDetail.ExhaustCardInHand(battleDiceCardModel);
            
        }
    }
}

using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VermilionDLL.Card_Effects
{
    public class DiceCardSelfAbility_ObstructAbility_Md5488 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "[Start of Clash] If Speed is higher than the target's, all dice on the opponent's page lose 2 Power.";

        public override void OnStartParrying()
        {
			int speedDiceResultValue = this.card.speedDiceResultValue;
			BattleUnitModel target = this.card.target;
			int targetSlotOrder = this.card.targetSlotOrder;
			if (targetSlotOrder >= 0 && targetSlotOrder < target.speedDiceResult.Count)
			{
				SpeedDice speedDice = target.speedDiceResult[targetSlotOrder];
				if (speedDiceResultValue > speedDice.value)
				{
					BattlePlayingCardDataInUnitModel currentDiceAction = target.currentDiceAction;
					if (currentDiceAction == null)
					{
						return;
					}
					currentDiceAction.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
					{
						power = -2
					});
				}
			}
		}
    }
}

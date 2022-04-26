using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VermilionDLL.Bufs;

namespace VermilionDLL.Card_Effects
{
	public class DiceCardSelfAbility_RampStrike_Md5488 : DiceCardSelfAbilityBase
	{

		public static string Desc =
			"Each stack of Heat boost the power of the die in this page by 1. If this page hits an enemy, use it again on another random enemy once. (Does not re-target already hit enemies)";
		public override void OnUseCard()
		{
			BattleUnitBuf battleUnitBuf = owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_VermilionHeatBuf_md5488);
			int heat = battleUnitBuf.stack;
			if (battleUnitBuf != null)
			{
				this._successAttack = false;
				int power = heat;
				this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
				{
					power = power
				});
			}
		}
		public override void OnSucceedAttack()
		{
			this._successAttack = true;
		}
		public override void OnEndBattle()
		{
			if (_successAttack)
			{
				List<BattleUnitModel> aliveList_opponent = BattleObjectManager.instance.GetAliveList_opponent(owner.faction);
				BattlePlayingCardDataInUnitModel card = this.card;

				if (card != null)
				{
					card.target.bufListDetail.AddBuf(new BattleUnitBuf_VermilionStrike_md5488());
				}

				aliveList_opponent.RemoveAll((BattleUnitModel x) => x.bufListDetail.HasBuf<BattleUnitBuf_VermilionStrike_md5488>());
				bool condition = owner.bufListDetail.HasBuf<BattleUnitBuf_VermilionStrike_md5488>(); // You might want to change this to other status

				if (aliveList_opponent.Count > 0 && condition == false) // If the user doesn't have the status (already attacked), then stop
				{
					BattleUnitModel target = RandomUtil.SelectOne(aliveList_opponent);
					Singleton<StageController>.Instance.AddAllCardListInBattle(card, target, -1);
					owner.bufListDetail.AddBuf(new BattleUnitBuf_VermilionStrike_md5488()); // Add this status to self
				}
			}
		}

		private bool _successAttack;

		public class BattleUnitBuf_VermilionStrike_md5488 : BattleUnitBuf
		{
			public override void OnRoundEnd()
			{
				this.Destroy();
			}
		}
	}
}

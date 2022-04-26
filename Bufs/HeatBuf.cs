using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VermilionDLL.Bufs
{
	public class BattleUnitBuf_VermilionHeatBuf_md5488 : BattleUnitBuf
	{
		protected override string keywordId => "HeatBuf_md5488"; //Check the buf ID for the description
		protected override string keywordIconId => "HeatBuf"; //Check the filename for the icon
		public override void OnRoundEnd()
		{
			this.CheckStack();
		}
		public override void OnAddBuf(int addedStack)
		{
			if (this.stack > 7)
			{
				this.stack = 7;
			}
			if (this._owner.IsImmune(this.bufType))
			{
				this.stack = 0;
				this.Destroy();
			}
		}
		public void CheckStack()
		{
			if (this.stack > 7)
			{
				this.stack = 7;
			}
			if (this.stack <= 0)
			{
				this.Destroy();
			}
		}
		bool cond = false;

		public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
		{
			cond = true;
		}

		public override void OnSuccessAttack(BattleDiceBehavior behavior)
		{
			BattleUnitModel target = behavior.card.target;
			if (cond == true && target != null && behavior.Index == 0)
			{
				BattleCardTotalResult battleCardResultLog = _owner.battleCardResultLog;
				if (battleCardResultLog == null)
				{
					return;
				}

				if (target == null) return;
				var dmgBonus = this.stack;
				target.TakeBreakDamage(dmgBonus, DamageType.Buf, _owner);
				target.TakeDamage(dmgBonus, DamageType.Buf, _owner);
			}
			cond = false;
		}
	}
}

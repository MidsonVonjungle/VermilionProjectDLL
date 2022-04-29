using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VermilionDLL.Bufs;

namespace VermilionDLL.Passives
{
    public class PassiveAbility_TheVermilionCross_md5488 : PassiveAbilityBase
    {
        public override int OnGiveKeywordBufByCard(BattleUnitBuf buf, int stack, BattleUnitModel target)
        {
            if (buf.bufType == KeywordBuf.Burn)
            {
                int addstacks = 1;
                BattleUnitBuf battleUnitBuf = owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_VermilionHeatBuf_md5488);

                if (battleUnitBuf == null)
                {
                    owner.bufListDetail.AddBuf(new BattleUnitBuf_VermilionHeatBuf_md5488() { stack = 0 });
                    battleUnitBuf = owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_VermilionHeatBuf_md5488);
                }
                battleUnitBuf.stack += addstacks;
                battleUnitBuf.OnAddBuf(addstacks);
            }
            return 0;
        }
		public override void OnLoseParrying(BattleDiceBehavior behavior)
		{
            if (behavior.TargetDice != null && base.IsDefenseDice(behavior.TargetDice.Detail))
            {
                int removestacks = 1;
                BattleUnitBuf battleUnitBuf = owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_VermilionHeatBuf_md5488);

                if (battleUnitBuf != null)
                {
                    battleUnitBuf.stack -= removestacks;
                    if (battleUnitBuf.stack <= 0)
                    {
                        battleUnitBuf.Destroy();
                    }
                }
            }
        }
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            BattleUnitBuf battleUnitBuf = owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_VermilionHeatBuf_md5488);

            if (battleUnitBuf != null)
            {
                if (battleUnitBuf.stack >= 3)
                {
                    int num = battleUnitBuf.stack / 3;
                    behavior.ApplyDiceStatBonus(new DiceStatBonus
                    {
                        power = num
                    });
                }
            }
        }
        public override void OnRoundEnd()
        {
            int removestacks = 1;
            BattleUnitBuf battleUnitBuf = owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_VermilionHeatBuf_md5488);

            if (battleUnitBuf != null)
            {
                battleUnitBuf.stack -= removestacks;
                if (battleUnitBuf.stack <= 0)
                {
                    battleUnitBuf.Destroy();
                }
            }
        }
        public override void OnWaveStart()
        {
            owner.allyCardDetail.DrawCards(2);
        }
    }
}

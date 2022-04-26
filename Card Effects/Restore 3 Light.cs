using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VermilionDLL.Card_Effects
{
    public class DiceCardSelfAbility_Restore3Light_Md5488 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "[On Use] Restore 3 Light";

        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPointByCard(3);
        }
    }
}

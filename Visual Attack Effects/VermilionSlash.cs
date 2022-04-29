using Battle.DiceAttackEffect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VermilionDLL.HarmonyPain;

namespace VermilionDLL.Visual_Attack_Effects
{
	public class DiceAttackEffect_VermilionSlash_md5488 : DiceAttackEffect
	{
		public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
		{
			base.Initialize(self, target, destroyTime);
			this._self = self.model;
			this._selfTransform = self.atkEffectRoot;
			this._targetTransform = target.atkEffectRoot;
			base.transform.parent = this._selfTransform;
			base.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			base.transform.localPosition = Vector3.one;
			base.transform.localRotation = Quaternion.identity;
			this.duration = destroyTime;
			Texture2D texture2D = new Texture2D(2, 2);
			texture2D.LoadImage(File.ReadAllBytes(ModParameters.Path + "/CustomEffect/VermilionSlash.png"));
			this.spr.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.30f, 0.60f)); //Vector2(Horizontal, Vertical)
			this.ResetLocalTransform(base.transform);
			bool flag = this._self.direction == Direction.RIGHT;
			if (flag)
			{
				base.transform.localScale = new Vector3(-1f, 1f, 1f);
				this._selfTransform.localPosition = new Vector3(1.92f, 2.52f, 0f);
			}
			else
			{
				this._selfTransform.localPosition = new Vector3(-1.92f, 2.52f, 0f);
			}
		}


		protected override void Update()
		{
			base.Update();
			this.duration -= Time.deltaTime;
			this.spr.color = new Color(1f, 1f, 1f, this.duration * 2f);
		}


		public override void SetScale(float scaleFactor)
		{
			scaleFactor *= 0.7f;
			base.SetScale(scaleFactor);
		}


		private float duration;
	}
}

using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace VKE
{
	public class VampConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;
		[Header("[c/FF0000:Difficulty Presets(Select only one at a time)]\n[c/FF4245:Warning: Selection overrides advanced settings]")]
		[BackgroundColor(0, 0, 0)]
		public bool Easy;
		[BackgroundColor(0, 0, 0)]
		public bool Normal;
		[BackgroundColor(0, 0, 0)]
		public bool Hard;
		[BackgroundColor(0, 0, 0)]
		public bool Expert;

		[Header("[c/FF0000:Advanced Settings]")]
		[Range(0.25f, 3f)]
		[Increment(0.10f)]
		[DrawTicks]
		[DefaultValue(1f)]
		[Tooltip("This changes the amount of damage knives do, under 1 decreases damage, over 1 increases damage.")]
		[BackgroundColor(0, 0, 0)]
		[Slider]
		[SliderColor(160, 0, 0)]
		[Label("Damage multiplier")]
		public float DamageMultiplier = 1f;

		[Range(0.1f, 2f)]
		[Increment(0.10f)]
		[DrawTicks]
		[DefaultValue(1f)]
		[Tooltip("This changes the amount of healing you get from healing projectiles, under 1 decreases healing, over 1 increases healing")]
		[BackgroundColor(0, 0, 0)]
		[Slider]
		[SliderColor(160, 0, 0)]
		[Label("Heal Amount Multiplier")]
		public float HealAmntMultiplier = 1f;

		[Range(0, 100)]
		[Increment(10)]
		[DrawTicks]
		[DefaultValue(1)]
		[Tooltip("This changes the rate at which knives spawn healing projectiles, 0 is no chance, 1 is 100% chance")]
		[BackgroundColor(0, 0, 0)]
		[Slider]
		[SliderColor(160, 0, 0)]
		[Label("Heal Projectile Spawn Chance")]
		public float HealProjectileSpawnChance = 1f;

		//[Range(0, 100)]
		//[Increment(10)]
		//[DrawTicks]
		//[DefaultValue(1)]
		//[Tooltip("This changes the amount of defence that is stripped from enemies by ammo-based knives, 0 is no defense, 1 is the base value")]
		//[BackgroundColor(0, 0, 0)]
		//[Slider]
		//[SliderColor(160, 0, 0)]
		//[Label("Ammo Knives Defence-Break Multiplier")]
		//public float AmmoKnivesDefenceBreakMult = 1f;

		//[Range(0, 80)]
		//[Increment(1)]
		//[DrawTicks]
		//[DefaultValue(20)]
		//[Tooltip("This changes the number of possible active defense knives. WARNING: HIGHER VALUES GREATLY DECREASE PERFORMANCE")]
		//[BackgroundColor(0, 0, 0)]
		//[Slider]
		//[SliderColor(160, 0, 0)]
		//[Label("Allowed Active Defense Knives")]
		//public int ActiveDefenseKnives = 20;

		public override void OnChanged()
		{
			if(Easy)
            {
				Normal = false;
				Hard = false;
				Expert = false;
				DamageMultiplier = 1.2f;
				HealAmntMultiplier = 1f;
				HealProjectileSpawnChance = 0.80f;
				//AmmoKnivesDefenceBreakMult = 1f;
            }
			else if (Normal)
			{
				Easy = false;
				Hard = false;
				Expert = false;
				DamageMultiplier = 1f;
				HealAmntMultiplier = 0.5f;
				HealProjectileSpawnChance = 0.45f;
				//AmmoKnivesDefenceBreakMult = 1f;
			}
			else if (Hard)
			{
				Easy = false;
				Normal = false;
				Expert = false;
				DamageMultiplier = 0.9f;
				HealAmntMultiplier = 0.25f;
				HealProjectileSpawnChance = 0.15f;
				//AmmoKnivesDefenceBreakMult = 0.8f;
			}
			else if (Expert)
			{
				Easy = false;
				Hard = false;
				Normal = false;
				DamageMultiplier = 0.75f;
				HealAmntMultiplier = 0.1f;
				HealProjectileSpawnChance = 0.05f;
				//AmmoKnivesDefenceBreakMult = 0.3f;
			}
			VampKnives.ConfigDamageMult = DamageMultiplier;
			VampKnives.ConfigHealAmntMult = HealAmntMultiplier;
			VampKnives.HealProjectileSpawn = HealProjectileSpawnChance * 100f;
			//Projectiles.DefenseKnivesProj.ProjCount.MaxActive = ActiveDefenseKnives;
		}
	}
}

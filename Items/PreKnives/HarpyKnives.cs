using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.PreKnives
{
    public class HarpyKnives : KnifeItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harpy Knives");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 11;
            Item.crit = 3;
            Item.width = 44;
            Item.height = 50;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = 1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 0, 56, 32);
            Item.rare = 8;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("HarpyProj").Type;
            Item.shootSpeed = 17f;
        }
    }
}

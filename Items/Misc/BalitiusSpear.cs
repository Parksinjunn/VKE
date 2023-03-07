using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Misc
{
    public class BalitiusSpear : ModItem
    {
        //public override void SetStaticDefaults()
        //{
            //DisplayName.SetDefault("Balitiu's Spear");
            //Tooltip.SetDefault("A spear used by Balitiu to trap the souls of his enemies in Blood Crystals");
        //}

        public override void SetDefaults()
        {
            Item.damage = 1;
            //Item.DamageType = DamageClass.Melee;
            Item.width = 54;
            Item.height = 54;
            Item.useAnimation = 18;
            Item.useTime = 24;
            Item.shootSpeed = 3.7f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.value = Item.buyPrice(gold: 1);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.shoot = Mod.Find<ModProjectile>("BalitiusDaggerProj").Type;
        }
        //public override bool CanUseItem(Player player)
        //{
        //    return player.ownedProjectileCounts[Item.shoot] < 1;
        //}
    }
}
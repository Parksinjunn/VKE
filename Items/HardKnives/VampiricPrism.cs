using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using VKE.Tiles;
using VKE.Items.PreKnives;
using VKE.Projectiles.HardProj;

namespace VKE.Items.HardKnives
{
    public class VampiricPrism : KnifeItem
    {
        int Timer = 10;
        int TimerTick;
        int UseTimeMax = 900;
        int UseTime;
        float ItemUseTimeStore;
        float ItemAnimationTimeStore;
        bool PrismUsed;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vampiric Prism");
            Tooltip.SetDefault("Fires knives with increasing speed at the cost of life");
        }
        public override void SafeSetDefaults()
        {
            Item.damage = 104; 
            Item.width = 26;
            Item.height = 30;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            //item.channel = true;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0,10,0,0); 
            Item.rare = 10;  
          
 
            Item.UseSound = SoundID.Item13; //Default 39
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("VampiricPrismHeldProj").Type;
            Item.shootSpeed = 30f;
            Item.channel = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }
        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SengosForgottenBlades>());
            recipe.AddIngredient(ItemID.VampireKnives, 1);
            recipe.AddIngredient(ItemID.LastPrism, 1);
            recipe.AddIngredient(ItemID.LunarBar, 7);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
        public bool RegenedHealth;
        public override bool CanUseItem(Player player)
        {
            if(player.statLife > 1)
            {
                RegenedHealth = true;
            }
            if(player.statLife <= 1 && RegenedHealth)
            {
                VampPlayer.OvalDust(player.position, 5f, 5f, Color.Red, 67, 1.2f);
                RegenedHealth = false;
                return false;
            }
            else if(player.ownedProjectileCounts[ModContent.ProjectileType<VampiricPrismHeldProj>()] <= 0 && RegenedHealth);
                return true;
        }
    }

}

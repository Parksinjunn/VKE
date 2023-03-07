using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VKE.Tiles;

namespace VKE.Items.Accessories.Hoods
{
    public class PartyHood : ModItem
    {
        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Head}", EquipType.Head, this, equipTexture: new PartyHead());
            //EquipLoader.AddEquipTexture(Mod, $"{Texture}Alt_{EquipType.Head}", EquipType.Head, name: "BlockyAlt", equipTexture: new PartyHead());
        }
        private void SetupDrawing()
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            int equipSlotHead = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);
            //int equipSlotHeadAlt = EquipLoader.GetEquipSlot(Mod, "BlockyAlt", EquipType.Head);
            ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
            //ArmorIDs.Head.Sets.DrawHead[equipSlotHeadAlt] = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Partier's Hood");
            Tooltip.SetDefault("Imbues the user's knives with fun\nPress the hood key to pull up the hood for a stronger effect\n(set the hood key in the settings)");
            SetupDrawing();
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            VampPlayer p = player.GetModPlayer<VampPlayer>();
            p.PartyAccessory = true;
            p.Party = true;
            if (hideVisual)
            {
                p.PartyHideVanity = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 20);
            recipe.AddIngredient(ItemID.FlaskofParty, 15);
            recipe.AddTile(TileID.Loom);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 15);
            recipe.AddIngredient(ItemID.FlaskofParty, 10);
            recipe.AddTile(ModContent.TileType<VampTableTile>());
            recipe.Register();
        }
    }

    public class PartyHead : EquipTexture
    {
        public override void FrameEffects(Player player, EquipType type)
        {
            if (Main.rand.Next(20) == 0)
            {
                Dust.NewDust(player.position, player.width, player.height, 5);
            }
        }
    }
}
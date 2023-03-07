using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using VKE.Tiles;

namespace VKE.Items.Misc
{
    public class BloodCrystalSoul : ModItem
    {
        public int NPCID;
        public string NPCName;
        protected override bool CloneNewInstances => true;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Crystal");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(8, 7));
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 30;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(silver: 1);
            NPCID = -69;
            NPCName = "ERROR THROW ON GROUND";
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if(NPCID != -69)
            {
                NPC n = new NPC();
                n.SetDefaults(NPCID);
                NPCName = n.FullName;
            }
            base.Update(ref gravity, ref maxFallSpeed);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (NPCID != -69)
            {
                TooltipLine line = new TooltipLine(Mod, "DamageMod", "NPC: " + NPCName)
                {
                    OverrideColor = Color.Red
                };
                tooltips.Add(line);
            }
            else if (NPCID == -69)
            {
                TooltipLine line = new TooltipLine(Mod, "DamageMod", "Empty")
                {
                    OverrideColor = Color.DarkRed
                };
                tooltips.Add(line);
            }
        }

        public override void SaveData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            tag["NPCID"] = NPCID;
            tag["NPCName"] = NPCName;
        }
        public override void LoadData(TagCompound tag)
        {
            NPCID = tag.GetInt("NPCID");
            NPCName = tag.GetString("NPCName");
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(NPCID);
            writer.Write(NPCName);
        }
        public override void NetReceive(BinaryReader reader)
        {
            NPCID = reader.ReadInt32();
            NPCName = reader.ReadString();
        }

        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe();
        //    recipe.AddIngredient(this);
        //    recipe.AddTile(ModContent.TileType<VampTableTile>());
        //    recipe.Register();

        //    Recipe recipe2 = CreateRecipe();
        //    recipe2.AddIngredient(ModContent.ItemType<Materials.CrimsonCrystal>());
        //    recipe2.AddIngredient(ItemID.Ectoplasm, 10);
        //    recipe2.AddIngredient(ItemID.Ruby, 1);
        //    recipe2.AddIngredient(ItemID.Blinkroot, 5);
        //    recipe2.AddIngredient(ItemID.Moonglow, 5);
        //    recipe2.AddTile(ModContent.TileType<VampTableTile>());
        //    recipe2.Register();
        //}
    }
}
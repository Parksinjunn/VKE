using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace VKE.Items.Accessories.Hoods
{
    // This and several other classes show off using EquipTextures to do a Merfolk or Werewolf effect. 
    // Typically Armor items are automatically paired with an EquipTexture, but we can manually use EquipTextures to achieve more unique effects.
    // There is code for this effect in many places, look in the following files for the full implementation:
    // NPCs.ExamplePerson drops this item when killed
    // Items.Armor.ExampleCostume (below) is the accessory item that sets ExamplePlayer values. Note that this item does not have EquipTypes set. This is a vital difference and key to our approach.
    // Items.Armor.BlockyHead/Body/Legs (below) are EquipTexture classes. They simply disable the drawing of the player's head/body/legs respectively when they are set as the drawn EquipTexture. One spawns dust too.
    // ExampleMod.Load() shows calling AddEquipTexture 3 times with appropriate parameters. This is how we register EquipTexture manually instead of the automatic pairing of ModItem and EquipTexture that other equipment uses.
    // Buffs.Blocky is the Buff that is shown while in Blocky mode. The buff is responsible for the actual stat effects of the costume. It also needs to remove itself when not near town npcs.
    // ExamplePlayer has 5 bools. They manage the visibility and other things related to this effect.
    // ExamplePlayer.ResetEffects resets those bool, except blockyAccessoryPrevious which is special because of the order of hooks.
    // ExamplePlayer.UpdateVanityAccessories is responsible for forcing the visual effect of our costume if the item is in a vanity slot. Note that ModItem.UpdateVanity can't be used for this because it is called too late.
    // ExamplePlayer.UpdateEquips is responsible for applying the Blocky buff to the player if the conditions are met and the accessory is equipped.
    // ExamplePlayer.FrameEffects is most important. It overrides the drawn equipment slots and sets them to our Blocky EquipTextures. 
    // ExamplePlayer.ModifyDrawInfo is for some fun effects for our costume.
    // Remember that the visuals and the effects of Costumes must be kept separate. Follow this example for best results.
    public class MagesHood : ModItem
    {
        public override void Load()
        {
            // The code below runs only if we're not loading on a server
            if (Main.netMode == NetmodeID.Server)
                return;

            // Add equip textures
            EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Head}", EquipType.Head, this, equipTexture: new MageHead());

            //Add a separate set of equip textures by providing a custom name reference instead of an item reference
            //EquipLoader.AddEquipTexture(Mod, $"{Texture}Alt_{EquipType.Head}", EquipType.Head, name: "BlockyAlt", equipTexture: new MageHead());
        }

        // Called in SetStaticDefaults
        private void SetupDrawing()
        {
            // Since the equipment textures weren't loaded on the server, we can't have this code running server-side
            if (Main.netMode == NetmodeID.Server)
                return;

            int equipSlotHead = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);

            //int equipSlotHeadAlt = EquipLoader.GetEquipSlot(Mod, "BlockyAlt", EquipType.Head);

            ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
            //ArmorIDs.Head.Sets.DrawHead[equipSlotHeadAlt] = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cultist's Hood");
            Tooltip.SetDefault("The hood of a true mage\nPress the hood key to pull up the hood for a stronger effect\n(set the hood key in the settings)");
            SetupDrawing();
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.accessory = true;
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.rare = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            VampPlayer p = player.GetModPlayer<VampPlayer>();
            p.MageAccessory = true;
            p.Mage = true;
            if (hideVisual)
            {
                p.MageHideVanity = true;
            }
        }
    }

    public class MageHead : EquipTexture
    {
    }
}
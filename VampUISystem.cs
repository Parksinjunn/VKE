using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using vke.UI;

namespace vke
{
    public class VampUISystem : ModSystem
    {
        //internal BloodAltarUIState BloodAltarUI;
        //private UserInterface BloodAltarUIState;
        internal UserInterface BloodAltarUIState;
        private BloodAltarUI BloodAltarUI;

        // These two methods will set the state of our custom UI, causing it to show or hide
        public void ShowMyUI()
        {
            BloodAltarUIState?.SetState(BloodAltarUI);

        }

        public void HideMyUI()
        {
            BloodAltarUIState?.SetState(null);
        }

        public override void Load()
        {
            // Create custom interface which can swap between different UIStates
            BloodAltarUIState = new UserInterface();
            // Creating custom UIState
            BloodAltarUI = new BloodAltarUI();

            // Activate calls Initialize() on the UIState if not initialized, then calls OnActivate and then calls Activate on every child element
            BloodAltarUI.Activate();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            // Here we call .Update on our custom UI and propagate it to its state and underlying elements
            if (BloodAltarUIState?.CurrentState != null)
                BloodAltarUIState?.Update(gameTime);
        }

        // Adding a custom layer to the vanilla layer list that will call .Draw on your interface if it has a state
        // Setting the InterfaceScaleType to UI for appropriate UI scaling
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ExampleMod: Coins Per Minute",
                    delegate
                    {
                        if (BloodAltarUIState?.CurrentState != null)
                            BloodAltarUIState.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
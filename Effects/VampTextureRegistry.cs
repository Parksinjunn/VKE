using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

// Base namespace for convinience
namespace VKE.Effects
{
    public static class VampTextureRegistry
    {
        //#region Additive Textures
        //public static Asset<Texture2D> BlobBloomTexture => ModContent.Request<Texture2D>("FargowiltasSouls/ExtraTextures/AdditiveTextures/BlobGlow");
        //public static Asset<Texture2D> BloomTexture => ModContent.Request<Texture2D>("FargowiltasSouls/ExtraTextures/AdditiveTextures/Bloom");
        //public static Asset<Texture2D> DeviBorderTexture => ModContent.Request<Texture2D>("FargowiltasSouls/ExtraTextures/AdditiveTextures/DeviBorder");
        //public static Asset<Texture2D> HardEdgeRing => ModContent.Request<Texture2D>("FargowiltasSouls/ExtraTextures/AdditiveTextures/HardEdgeRing");
        //public static Asset<Texture2D> SoftEdgeRing => ModContent.Request<Texture2D>("FargowiltasSouls/ExtraTextures/AdditiveTextures/SoftEdgeRing");
        //#endregion

        //#region Misc Shader Textures
        //public static Asset<Texture2D> DeviRingTexture => ModContent.Request<Texture2D>("FargowiltasSouls/ExtraTextures/MiscShaderTextures/Ring1");
        //public static Asset<Texture2D> DeviRing2Texture => ModContent.Request<Texture2D>("FargowiltasSouls/ExtraTextures/MiscShaderTextures/Ring2");
        //public static Asset<Texture2D> DeviRing3Texture => ModContent.Request<Texture2D>("FargowiltasSouls/ExtraTextures/MiscShaderTextures/Ring3");
        //public static Asset<Texture2D> DeviRing4Texture => ModContent.Request<Texture2D>("FargowiltasSouls/ExtraTextures/MiscShaderTextures/Ring4");
        //#endregion

        //#region Trails
        public static Asset<Texture2D> VortexTrail => ModContent.Request<Texture2D>("VKE/Effects/Primitives/Trails/VortexTrail");
        public static Asset<Texture2D> WhispyTrail => ModContent.Request<Texture2D>("VKE/Effects/Primitives/Trails/WhispyTrail");
        public static Asset<Texture2D> CorkscrewTrail => ModContent.Request<Texture2D>("VKE/Effects/Primitives/Trails/CorkscrewTrail");
        public static Asset<Texture2D> FadedStreak => ModContent.Request<Texture2D>("VKE/Effects/Primitives/Trails/FadedStreak");
        public static Asset<Texture2D> TerraTrail => ModContent.Request<Texture2D>("VKE/Effects/Primitives/Trails/TerraTrail");
        public static Asset<Texture2D> DNATrail => ModContent.Request<Texture2D>("VKE/Effects/Primitives/Trails/DNAHelixTrail");
        public static Asset<Texture2D> SpikyTrail1 => ModContent.Request<Texture2D>("VKE/Effects/Primitives/Trails/SpikyTrail1");
        public static Asset<Texture2D> SpikyTrail2 => ModContent.Request<Texture2D>("VKE/Effects/Primitives/Trails/SpikyTrail2");
        public static Asset<Texture2D> BulbTrail => ModContent.Request<Texture2D>("VKE/Effects/Primitives/Trails/BulbTrail");
        public static Asset<Texture2D> LightningTrail => ModContent.Request<Texture2D>("VKE/Effects/Primitives/Trails/LightningTrail");


        //public static Asset<Texture2D> FadedThinGlowStreak => ModContent.Request<Texture2D>("FargowiltasSouls/ExtraTextures/Trails/FadedThinGlowStreak");
        //public static Asset<Texture2D> GenericStreak => ModContent.Request<Texture2D>("FargowiltasSouls/ExtraTextures/Trails/GenericStreak");
        //public static Asset<Texture2D> MutantStreak => ModContent.Request<Texture2D>("FargowiltasSouls/ExtraTextures/Trails/MutantStreak");
        //public static Asset<Texture2D> WillStreak => ModContent.Request<Texture2D>("FargowiltasSouls/ExtraTextures/Trails/WillStreak");
        //#endregion
    }
}
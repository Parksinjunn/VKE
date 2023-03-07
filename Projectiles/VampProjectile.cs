using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using Terraria.ID;
using System.Linq;
using VKE;
using static VKE.VampNet;

namespace VKE.Projectiles
{
    public class VampProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public static Dictionary<int, (Entity entity, IEntitySource source)> projOwners = new();

        //public override void OnSpawn(Projectile projectile, IEntitySource source)
        //{
        //    if (projectile.ModProjectile is ITrailProjectile)
        //    {
        //        if (Main.netMode == NetmodeID.SinglePlayer)
        //            (projectile.ModProjectile as ITrailProjectile).DoTrailCreation(RedeSystem.TrailManager);

        //        else
        //            VampKnives.WriteToPacket(VampKnives.Instance.GetPacket(), (byte)ModMessageType.SpawnTrail, projectile.whoAmI).Send();
        //    }
        //}
    }
}

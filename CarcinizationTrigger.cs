using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.CarcinizationHelper {
    [CustomEntity("BrokemiaHelper/carcinizationTrigger")]
    public class CarcinizationTrigger : Trigger {
        private EntityID id;
        private string type;
        private bool endLevel;
        private bool oncePerDeath;
        private bool oncePerSession;

        public CarcinizationTrigger(EntityData data, Vector2 offset, EntityID id) : base(data, offset) {
            type = data.Attr("type", "random");
            endLevel = data.Bool("endLevel", true);
            oncePerDeath = data.Bool("oncePerDeath", true);
            oncePerSession = data.Bool("oncePerSession", false);
        }

        public override void OnEnter(Player player) {
            base.OnEnter(player);
            Scene.Add(new Carcinization(type, endLevel ? self => EndLevelCallback(self, player) : null));
            if (oncePerDeath || oncePerSession) {
                RemoveSelf();
            }
            if (oncePerSession) {
                SceneAs<Level>().Session.DoNotLoad.Add(id);
            }
        }

        private bool EndLevelCallback(Carcinization self, Player player) {
            player.SceneAs<Level>().CompleteArea(false, false, false);
            player.StateMachine.State = 11;
            RemoveSelf();
            return false;
        }

    }
}

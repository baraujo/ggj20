using UnityEngine;

namespace PxlSquad {
    public class LayerManager {
        // TODO: grab all layers automatically and automate this?
        public static int GroundLayer = 1 << LayerMask.NameToLayer("Ground");
        public static int BackgroundLayer = 1 << LayerMask.NameToLayer("Background");
        public static int PlayerLayer = 1 << LayerMask.NameToLayer("Player");

        public static bool MatchesLayer(GameObject go, int layerMask) {
            if (layerMask == -1)
            {
                Debug.LogError("Invalid layer! Verify layer names or LayerManager class");
                return false;
            }
            return (1 << go.layer & layerMask) > 0;
        }
    }
}
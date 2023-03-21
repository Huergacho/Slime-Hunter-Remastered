using UnityEngine;

namespace Utilities
{
    public static class GameUtilities
    {
        public static bool IsGoInLayerMask(GameObject go, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << go.layer));
        }
        public static Vector3 LookTowardsMousePos(Camera currCam,Vector3 selfPos,Vector2 lookDir)
        {
            Vector3 worldScreenPosition = currCam.ScreenToWorldPoint(lookDir);
            Vector2 diff = worldScreenPosition - selfPos;
            var dist = Vector2.Distance(diff, selfPos);
            if (dist >= 1f)
            {
                return diff.normalized;
            }
            else
            {
                return Vector3.zero;
            }
        }
        public static void OnDrawGizmosSelected(float range, Vector3 position)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(position, range * 2);
        }
    }
}
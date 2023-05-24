using UnityEngine;

namespace _Main.Scripts.Objects
{
    public class ObjectDisguiser : MonoBehaviour
    {
        [SerializeField] private MeshFilter filter;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private MeshFilter outline;
        public MeshFilter Filter => filter;
        public MeshRenderer Renderer => _renderer;

        public void ChangeSkin(ObjectDisguiser disguiser)
        {
            this.filter.mesh = disguiser.Filter.sharedMesh;
            this._renderer.material = disguiser._renderer.sharedMaterial;
            if (outline == null)
            {
                return;
            }

            outline.mesh = disguiser.Filter.sharedMesh;
        }
    }
}
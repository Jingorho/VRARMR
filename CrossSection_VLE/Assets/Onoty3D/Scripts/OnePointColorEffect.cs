using UnityEngine;

namespace Onoty3D
{
    [ExecuteInEditMode]
    public class OnePointColorEffect : MonoBehaviour
    {
        public Material Material;

        public Color TargetColor = Color.white;

        [Range(0, 0.5f)]
        public float Near = 0.1f;

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            this.Material.SetColor("_TargetColor", this.TargetColor);
            this.Material.SetFloat("_Near", this.Near);

            Graphics.Blit(source, destination, this.Material);
        }
    }
}
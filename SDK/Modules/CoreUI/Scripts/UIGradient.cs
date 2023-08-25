using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OGT
{
    [RequireComponent(typeof(CanvasRenderer))]
    [AddComponentMenu(OGTConstants.kCreateComponentUIPath + "UIGradient", 11)]
    public class UIGradient : BaseMeshEffect
    {
	    [Expandable]
        [SerializeField] private TMP_ColorGradient m_gradient;

        public override void ModifyMesh(VertexHelper vertexHelper)
        {
            if (!enabled || m_gradient == default) 
                return;
            
            UIVertex vertex = default;
            for (int i = 0; i < vertexHelper.currentVertCount; i++) {
                vertexHelper.PopulateUIVertex(ref vertex, i);
                vertex.color *= m_gradient.GetBiLerpAtPosition(GradientExtensions.VerticesPositions[i % 4]);
                vertexHelper.SetUIVertex(vertex, i);
            }
        }

        private void OnDrawGizmosSelected()
        {
	        graphic.SetVerticesDirty();
        }
    }
    
    public static class GradientExtensions
    {
        public static Vector2[] VerticesPositions { get; } = { Vector2.up, Vector2.one, Vector2.right, Vector2.zero };

        public static Color GetBiLerpAtPosition(this TMP_ColorGradient gradient, Vector2 matrixIndex)
        {
            Color bl = gradient.bottomLeft, br = gradient.bottomRight, tl = gradient.topLeft, tr = gradient.topRight;
            switch (gradient.colorMode)
            {
                case ColorMode.Single:
                    tr = tl;
                    bl = tl;
                    br = tl;
                    break;
                case ColorMode.HorizontalGradient:
                    bl = tl;
                    br = tr;
                    break;
                case ColorMode.VerticalGradient:
                    tr = tl;
                    br = bl;
                    break;
            }
		
            Color a = Color.LerpUnclamped(br, tr, matrixIndex.x);
            Color b = Color.LerpUnclamped(bl, tl, matrixIndex.x);
            return Color.LerpUnclamped(a, b, matrixIndex.y);
        }
    }
}
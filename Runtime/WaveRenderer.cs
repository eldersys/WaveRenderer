using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WaveRenderer.Runtime
{
    [RequireComponent(typeof(CanvasRenderer))]
    [AddComponentMenu("UI/Wave Renderer")]
    public class WaveRenderer : Graphic
    {
        public List<Vector2> m_points = new List<Vector2>();
        [Min(3)] public int m_waveLength;
        public float m_waveAmplitude = 10;
        public float m_waveHeight = 36;
        public float m_waveResolution = 0.65f;
        public float m_moveWaveHorizontal;
        public float m_moveWaveVertical;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            DrawVerticesForPoint(vh);
            DrawTriangles(vh);
        }

        private void DrawVerticesForPoint(VertexHelper vh)
        {
            UIVertex vert = UIVertex.simpleVert;
            vert.color = color;
        
            if(m_points.Count > 0) 
                m_points.Clear();
        
            for (int i = 0; i < m_waveLength; i++)
            {
                Vector2 point = new Vector2(i * m_waveAmplitude, m_waveHeight*Mathf.Sin(
                    Mathf.Pow(m_waveResolution,2)*i+m_moveWaveHorizontal)+m_moveWaveVertical);
                m_points.Add(point);
                vh.AddVert(point, vert.color, Vector4.one);
            }

        }

        private void DrawTriangles(VertexHelper vh)
        {
            for (int i = 0; i < m_points.Count; i++)
            {
                int index = i;
                if (vh.currentIndexCount < m_points.Count)
                    vh.AddTriangle(index + 0, index + 1, index + 2);
                else
                    return;
            }
        }
    }
}
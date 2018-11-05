using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public enum GradientDirection
{
    Vertical, Horizontal
}

[AddComponentMenu("UI/Effects/Gradient")]
public class Gradient : BaseMeshEffect
{
    public Color color1 = Color.white;
    public Color color2 = Color.black;
    public float x = 0.5f, y = 0.5f;
    public float controlX = 1.0f, controlY = 1.0f;
    private Vector2 GDirection;

    public override void ModifyMesh(VertexHelper helper)
    {
        if (!IsActive() || helper.currentVertCount == 0)
            return;

        GDirection = new Vector2(x, y);
        List<UIVertex> vertices = new List<UIVertex>();
        helper.GetUIVertexStream(vertices);

        // Test
        float minX = vertices[0].position.x;
        float maxX = minX;
        float minY = vertices[0].position.y;
        float maxY = minY;

        for (int i = 1; i < vertices.Count; i++)
        {
            float x = vertices[i].position.x;
            if (x < minX)
                minX = x;
            else if (x > maxX)
                maxX = x;

            float y = vertices[i].position.y;
            if (y < minY)
                minY = y;
            else if (y > maxY)
                maxY = y;
        }
        float sizeX = (maxX - minX) * controlX, sizeY = (maxY - minY) * controlY;

        UIVertex v = new UIVertex();
        for (int i = 0; i < helper.currentVertCount; i++)
        {
            helper.PopulateUIVertex(ref v, i);
            v.color = Color.Lerp(color2, color1, (GDirection.x * (v.position.x - minX) / sizeX + GDirection.y * (v.position.y - minY) / sizeY));
            helper.SetUIVertex(v, i);
        }
    }

    private void Update()
    {
    }
}
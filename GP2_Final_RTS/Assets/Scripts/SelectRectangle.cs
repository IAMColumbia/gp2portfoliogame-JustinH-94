using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectRectangle
{
    static Texture2D texture;
    public static Texture2D Texture
    {
        get
        {
            if (texture == null)
            {
                texture = new Texture2D(1, 1);
                texture.SetPixel(0, 0, Color.white);
                texture.Apply();
            }
            return texture;
        }
    }

    public static void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, Texture);
        GUI.color = Color.green;
    }

    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        // Top
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        // Left
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        // Right
        DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        // Bottom
        DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }

    public static Rect GetScreenRect(Vector3 screenPosStart, Vector3 screenPosEnd)
    {
        screenPosStart.y = Screen.height - screenPosStart.y;
        screenPosEnd.y = Screen.height - screenPosEnd.y;

        var topLeft = Vector3.Min(screenPosStart, screenPosEnd);
        var bottomRight = Vector3.Max(screenPosStart, screenPosEnd);

        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cursor Set")]
public class CursorSet : ScriptableObject
{
    [System.Serializable]
    public struct CursorTexture
    {
        public Texture2D texture;
        public Vector2 hotspot;

        public void SetCursorTexture()
        {
            Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
        }
    }

    public CursorTexture normal;
    public CursorTexture onEdge;
    public CursorTexture onEdgeMoving;

}

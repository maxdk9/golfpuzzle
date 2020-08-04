

    using UnityEngine;

    public class EditorTile: MonoBehaviour
    {
        public ColorToPrefab mColorToPrefab;
        
        
        
        public void SetColorToPrefab(ColorToPrefab c)
        {
            mColorToPrefab = c;
            SpriteRenderer spriteRenderer = mColorToPrefab.prefab.GetComponent<SpriteRenderer>();
            this.GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
            this.GetComponent<SpriteRenderer>().color=spriteRenderer.color;
        }

        
        public static void RemoveAllTiles()
        {
            EditorTile[] editorTiles = GameObject.FindObjectsOfType<EditorTile>();
            foreach (EditorTile editorTile in editorTiles)
            {
                GameObject.Destroy(editorTile.gameObject);
            }
        }
    }

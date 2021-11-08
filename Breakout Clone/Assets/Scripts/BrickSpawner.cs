using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BrickSpawner : MonoBehaviour
{
    public bool SpawnBricks = false;

    [Header("Prefabs")]
    public GameObject Prefab;
    
    [Header("Settings")]
    public float Width = 50;
    public float Height = 10;

    public int Columns = 10;
    public int Rows = 7;

    public Vector2 BrickGap = Vector2.zero;

    public Gradient TopToBotGradient;

    List<GameObject> BrickList;

    private void OnValidate()
    {
        if (SpawnBricks)
        {
            SpawnBricks = false;
            Spawn();
        }
    }

    private void Update()
    {
        
    }
    void Spawn()
    {
        if (BrickList != null)
        {
            List<GameObject> brickCopy = new List<GameObject>(BrickList);
            //foreach (GameObject item in BrickList)
            //{
            //    brickCopy.Add(item);
            //}
            UnityEditor.EditorApplication.delayCall += () =>
            {
                for (int i = 0; i < brickCopy.Count; i++)
                {
                    UnityEditor.Undo.DestroyObjectImmediate(brickCopy[i]);
                    brickCopy[i] = null;
                }
            };

            
        }

        BrickList.Clear();

        Vector3 size = new Vector3((Width / Columns), (Height / Rows), 1.0f);

        float currentY = (size.y / 2) - (Height / 2);
        for (int i = 0; i < Rows; i++)
        {
            float currentX = (size.x / 2) - Width / 2;
            for (int j = 0; j < Columns; j++)
            {
                Vector3 newPos = new Vector3(currentX, currentY, 0.0f);

                Vector3 truesize = new Vector3(size.x - BrickGap.x, size.y - BrickGap.y, size.z);

                GameObject newBrick = Instantiate(Prefab, this.transform);
                newBrick.transform.localScale = truesize;
                newBrick.transform.position = transform.position + newPos;

                Color brickColor = TopToBotGradient.Evaluate((float)i / (float)Rows);

                newBrick.GetComponent<SpriteRenderer>().color = brickColor;

                BrickList.Add(newBrick);
                currentX += size.x;
            }
            currentY += size.y;
        }
    }

    void ClearAll()
    {
        
    }
 
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height));

        Gizmos.color = Color.blue;
        Vector3 size = new Vector3((Width / Columns), (Height / Rows), 1.0f);
        float currentY = (size.y / 2) - (Height /2);
        for (int i = 0; i < Rows; i++)
        {
            float currentX = (size.x / 2) - Width / 2;
            for (int j = 0; j < Columns; j++)
            {
                Vector3 newPos = new Vector3(currentX, currentY, 0.0f);
                Vector3 truesize = new Vector3(size.x - BrickGap.x, size.y - BrickGap.y, size.z);
                Gizmos.DrawWireCube(transform.position + newPos, truesize);
                currentX += size.x;

            }
            currentY += size.y;
        }
    }
}

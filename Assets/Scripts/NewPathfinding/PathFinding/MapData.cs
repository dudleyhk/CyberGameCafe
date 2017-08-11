using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public static int rows = 50;
    public static int cols = 50;
    public static List<Vector3> nodeCentrePositions;
    public float nodeWidth = 0f;
    public float nodeHeight = 0f;
    public SpriteRenderer map;

    private void Awake()
    {
        nodeCentrePositions = new List<Vector3>();
        nodeWidth = map.bounds.size.x / cols;
        nodeHeight = map.bounds.size.y / rows;

        CreatePositionList();
    }


    private void CreatePositionList()
    {
        float currentX = map.bounds.min.x;
        float currentY = map.bounds.min.y;

        for (int r = 0; r < rows; r++)
        {
            currentX = map.bounds.min.x;
            for (int c = 0; c < cols; c++)
            {
                float TRx = currentX + nodeWidth;
                float TRy = currentY + nodeHeight;
                Vector2 topRight = new Vector2(TRx, TRy);

                float Cx = topRight.x - (nodeWidth / 2);
                float Cy = topRight.y - (nodeHeight / 2);
                float Cz = map.bounds.min.z;
                Vector3 centre = new Vector3(Cx, Cy, Cz);

                nodeCentrePositions.Add(centre);

                // Move to next node along. 
                currentX += nodeWidth;
            }
            currentY += nodeHeight;
        }
    }


    public static Vector3 GetPositionOfIndex(int index)
    {
        if((index > rows * cols) || (index < 0))
        {
            print("incorrect index passed in");
            return new Vector3(-1, -1, -1);
        }
        return nodeCentrePositions[index];
    }
}

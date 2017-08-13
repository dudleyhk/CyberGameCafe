using System.Collections.Generic;
using UnityEngine;

public class NodeGraph
{
    public int rows = 0;
    public int cols = 0;
    public Node[] nodes = null;

    public NodeGraph(bool[,] grid)
    {
        rows = grid.GetLength(0);
        cols = grid.GetLength(1);

        nodes = new Node[grid.Length];
        for (var i = 0; i < nodes.Length; i++)
        {
            int r = (i / rows);
            int c = i % cols;

            var node = new Node();
            node.label = i.ToString();
            node.position = MapData.GetPositionOfIndex(i);
            node.solid = grid[r, c];

            //Debug.Log("node type for id " + i + " is " + node.type);

            nodes[i] = node;
        }


        for (var r = 0; r < rows; r++)
        {
            for(var c = 0; c < cols; c++)
            {
                var node = nodes[cols * r + c];

                // 0 = open tile, 1 = solid tile. 
                if(grid[r,c] == true)
                    continue;

                // Up
                if(r > 0)
                {
                    node.adjecent.Add(nodes[cols * (r - 1) + c]);
                }

                // Right
                if(c < (cols - 1))
                {
                    node.adjecent.Add(nodes[cols * r + c + 1]);
                }

                // Down
                if(r < (rows - 1))
                {
                    node.adjecent.Add(nodes[cols * (r + 1) + c]);
                }

                //Left
                if(c > 0)
                {
                    node.adjecent.Add(nodes[cols * r + c - 1]);
                }
            }
        }
    }

}

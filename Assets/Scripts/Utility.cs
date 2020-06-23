using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Utility : MonoBehaviour
{

    public static Vector3Int[] GetNeighbours(Tilemap t, Vector3Int p)
    {
        var x = p.x;
        var y = p.y;

        List<Vector3Int> pos = new List<Vector3Int>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                var newPos = new Vector3Int(x + i, y + j, 0);

                var worPosBase = t.CellToWorld(p);

                var worPos = t.CellToWorld(newPos);

                var dist = Mathf.Abs(Vector3.Distance(worPosBase, worPos));

                if (dist > 1.5f)
                {
                    continue;
                }

                pos.Add(newPos);
            }
        }

        return pos.ToArray();
    }
}

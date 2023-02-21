using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNode : MonoBehaviour
{
    public int x;
    public int z;

    public bool isWall;
    public WallNode(int _x, int _z, bool _isWall)
    {
        x = _x;
        z = _z;
        isWall = _isWall;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMazeGen : MonoBehaviour
{
    public GameObject wall;
    [Range(10, 50)] public int width = 28;
    [Range(10, 50)] public int height = 31;

    private List<GameObject> wallList;

    // Start is called before the first frame update
    private void Start()
    {
        GenerateMazeLayout();
    }

    private void GenerateMazeLayout()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var mazeWall = Instantiate(wall, new Vector3(x, 0, z), Quaternion.identity);
                mazeWall.name = "Wall[" + x + ", " + z + "]";
                mazeWall.transform.SetParent(gameObject.transform);
            }
        }
    }
}

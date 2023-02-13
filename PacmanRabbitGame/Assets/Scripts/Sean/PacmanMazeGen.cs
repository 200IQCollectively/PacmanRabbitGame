using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMazeGen : MonoBehaviour
{
    public GameObject wall;
    [Range(10, 50)] public int width = 28;
    [Range(10, 50)] public int height = 31;

    public List<GameObject> wallList;
    public List<GameObject> mazeList;

    public GameObject score;

    // Start is called before the first frame update
    private void Start()
    {
        GenerateMazeLayout();
    }

    private void GenerateMazeLayout()
    {
        for (int x = 0; x < width + 1; x++)
        {
            for (int z = 0; z < height + 1; z++)
            {
                var mazeWall = Instantiate(wall, new Vector3(x, 0, z), Quaternion.identity);
                mazeWall.name = "Wall[" + x + ", " + z + "]";
                mazeWall.transform.SetParent(gameObject.transform);

                if(x == 0 || x == width || z == 0 || z == height)
                {
                    wallList.Add(mazeWall);
                }

                else
                {
                    mazeList.Add(mazeWall);
                }
            }
        }

        CarveMaze();
    }

    private void CarveMaze()
    {
        foreach(GameObject wall in mazeList)
        {
            var scoreObj = Instantiate(score, new Vector3(wall.transform.position.x, 1, wall.transform.position.z), Quaternion.identity);
            scoreObj.transform.SetParent(gameObject.transform);
            Destroy(wall);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PacmanMazeGen : MonoBehaviour
{
    public GameObject wall;
    public GameObject walls;

    public NavMeshSurface floor;

    [Range(10, 50)] public int width = 28;
    [Range(10, 50)] public int height = 31;

    //Must be public/ private serializefield or it doesn't work and no one knows why
    public int[,] maze;

    public GameObject score;
    public GameObject scores;

    public GameObject spawner;
    public GameObject twoByFour;
    public GameObject threeByOne;
    public GameObject lWall;
    public GameObject tWall;

    private Vector3 xScale, zScale;

    public LayerMask layer = 7;

    public GameObject Player;

    // Start is called before the first frame update
    private void Start()
    {
        floor = gameObject.transform.Find("Plane").GetComponent<NavMeshSurface>();

        GenerateMazeLayout();
    }

    private void GenerateMazeLayout()
    {
        maze = new int[width, height];

        for (int x = 0; x < maze.GetLength(0) + 1; x++)
        {
            for(int z = 0; z < maze.GetLength(1) + 1; z++)
            {

                //Check if bottom or top of maze and instantiate walls scaled to size for performance

                //if(x == maze.GetLength(0) / 2 && z == 0 || x == maze.GetLength(0) / 2 && z == maze.GetLength(1))
                //{

                //}


                if (x == 0 || x == maze.GetLength(0) || z == 0 || z == maze.GetLength(1))
                {
                    var wallObj = Instantiate(wall, new Vector3(x, 0, z), Quaternion.identity);
                    wallObj.name = "Wall" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                if (x == maze.GetLength(0) / 2 && z == maze.GetLength(1) / 2)
                {
                    var spawnObj = Instantiate(spawner, new Vector3(x, 0.5f, z), Quaternion.Euler(0f, 180f, 0f));
                    spawnObj.name = "Spawner" + "[" + x + ", " + z + "]";
                    spawnObj.transform.SetParent(walls.transform);

                    Instantiate(Player, new Vector3(x, 1.5f, z - 4f), Quaternion.identity);
                }
            }
        }

        FillMaze(maze);

        floor.BuildNavMesh();
    }

    private void FillMaze(int[,] size)
    {
        for(int x = 0; x < size.GetLength(0) + 1; x++)
        {
            for(int z = 0; z < size.GetLength(1) + 1; z++)
            {
                //Spawn Obstacles
                int random = Random.Range(0, 4);

                switch (random)
                {
                    case 0:

                        if(!Physics.CheckBox(new Vector3(x + 0.5f, 1, z + 1.5f), new Vector3(2.5f, 1.5f, 3.5f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(twoByFour, new Vector3(x, 0.5f, z), Quaternion.identity);
                            wallObj.name = "2x4" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        break;

                    case 1:

                        if (!Physics.CheckBox(new Vector3(x + 1f, 1, z), new Vector3(3f, 1.5f, 1.5f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(threeByOne, new Vector3(x, 0.5f, z), Quaternion.identity);
                            wallObj.name = "v3x1" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        break;

                    case 2:

                        if (!Physics.CheckBox(new Vector3(x + 1f, 1, z + 0.5f), new Vector3(2.5f, 1.5f, 2f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(lWall, new Vector3(x, 0.5f, z), Quaternion.identity);
                            wallObj.name = "lWall" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        break;

                    case 3:

                        if (!Physics.CheckBox(new Vector3(x, 1, z + 1.5f), new Vector3(4.5f, 1.5f, 3f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(tWall, new Vector3(x, 0.5f, z), Quaternion.identity);
                            wallObj.name = "tWall" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        break;

                    default:
                        break;
                }
            }
        }

        AddScoreObjects(maze);
    }

    private void AddScoreObjects(int[,] size)
    {
        for(int x = 0; x < size.GetLength(0); x++)
        {
            for(int z = 0; z < size.GetLength(1); z++)
            {
                if (!Physics.CheckBox(new Vector3(x, 1, z), new Vector3(0.4f, 0.5f, 0.4f), Quaternion.identity, layer))
                {
                    var scoreObj = Instantiate(score, new Vector3(x, 1, z), Quaternion.identity);
                    scoreObj.name = "Score" + "[" + x + ", " + z + "]";
                    scoreObj.transform.SetParent(scores.transform);
                }
            }
        } 
    }

   private bool isXConnecting(int x, int nX)
    {
        return x + 1 == nX || x - 1 == nX; 
    }

    private void GetNeighbouringWalls(int x, int z)
    {
        int connectingWalls = 0;

        for(int nX = x - 1; nX <= x + 1; nX++)
        {
            for(int nZ = z - 1; nZ <= z + 1; nZ++)
            {
                if(isXConnecting(x, nX))
                {

                }
            }
        }
    }
}
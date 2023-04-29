using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//fix the mazegen to randomise through all possible nodes and layouts

public class PacmanMazeGen : MonoBehaviour
{
    public GameObject wall;
    public GameObject walls;

    public NavMeshSurface floor;

    private int width = 28;
    private int height = 31;

    public MeshFilter floorMF;
    public MeshCollider floorMC;

    //Must be public/ private serializefield or it doesn't work and no one knows why
    public int[,] maze;

    public GameObject score;
    public GameObject scores;
    public List<GameObject> scoreList;


    //MazeGen stuff
    public List<GameObject> wallTypes;
    public List<GameObject> checkedList;
    private bool notSpawned = false;

    public GameObject spawner;
    public GameObject hole;

    private Vector3 xScale;
    private Vector3 zScale;

    public LayerMask layer = 7;

    public GameObject Player;

    public GameHandler game;

    public GameObject PowerUpObj;

    //Level Stuff
    private int level = 1;
    private int minWidth = 20;
    private int maxWidth;
    private int minHeight = 20;
    private int maxHeight;

    //Minimap
    private Camera minimap;
    private int minimapSize = 10;

    // Start is called before the first frame update
    private void Start()
    {
        floor = gameObject.transform.Find("Floor").GetComponent<NavMeshSurface>();
        minimap = GameObject.Find("MinimapCamera").GetComponent<Camera>();
        game = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }

    private void GenerateMazeLayout()
    {
        switch (level)
        {
            case 0:
                break;
            case 1:
                maxWidth = 20;
                maxHeight = 20;
                break;
            case 2:
                maxWidth = 25;
                maxHeight = 25;
                minimapSize = 13;
                break;
            case 3:
                minWidth = 25;
                maxWidth = 30;
                minHeight = 25;
                maxHeight = 30;
                minimapSize = 15;
                break;
            case 4:
                minWidth = 30;
                maxWidth = 40;
                minHeight = 30;
                maxHeight = 40;
                minimapSize = 20;
                break;
            case 5:
                minWidth = 35;
                maxWidth = 45;
                minHeight = 35;
                maxHeight = 45;
                minimapSize = 23;
                break;
            default:
                minWidth = 35;
                maxWidth = 50;
                minHeight = 35;
                maxHeight = 50;
                minimapSize = 25;
                break;
        }

        minimap.orthographicSize = minimapSize;

        width = Random.Range(minWidth, maxWidth);
        height = Random.Range(minHeight, maxHeight);

        maze = new int[width, height];

        CreateFloor(maze);

        for (int x = 0; x < maze.GetLength(0) + 1; x++)
        {
            for (int z = 0; z < maze.GetLength(1) + 1; z++)
            {

                //Check if bottom or top of maze and instantiate walls scaled to size for performance
                if (x == maze.GetLength(0) / 2 && z == 0 || x == maze.GetLength(0) / 2 && z == maze.GetLength(1))
                {
                    xScale = new Vector3(100, 1, 1);
                    var wallObj = Instantiate(wall, new Vector3(x, 0, z), Quaternion.identity);
                    wallObj.transform.localScale = xScale;
                    wallObj.name = "Wall" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                else if (x == 0 && z == maze.GetLength(1) / 2 || x == maze.GetLength(0) && z == maze.GetLength(1) / 2)
                {
                    zScale = new Vector3(1, 1, 100);
                    var wallObj = Instantiate(wall, new Vector3(x, 0, z), Quaternion.identity);
                    wallObj.transform.localScale = zScale;
                    wallObj.name = "Wall" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                //Spawn Holes, Spawner, and Player

                if (x == 1 && z == maze.GetLength(1) / 2)
                {
                    var holeObj = Instantiate(hole, new Vector3(x - 1, 2.5f, z), Quaternion.Euler(0, 0, -90));
                    holeObj.name = "LeftHole";
                    holeObj.transform.SetParent(walls.transform);

                    GameObject.Find("OutsideFloor").transform.Find("OLeftHole").GetComponent<TeleportPlayer>().teleportTarget = holeObj.transform.Find("TeleportPoint").transform;
                }

                if (x == maze.GetLength(0) - 1 && z == maze.GetLength(1) / 2)
                {
                    var holeObj = Instantiate(hole, new Vector3(x + 1, 0.5f, z), Quaternion.Euler(0, 0, 90));
                    holeObj.name = "RightHole";
                    holeObj.transform.SetParent(walls.transform);

                    GameObject.Find("OutsideFloor").transform.Find("ORightHole").GetComponent<TeleportPlayer>().teleportTarget = holeObj.transform.Find("TeleportPoint").transform;
                }

                if (x == maze.GetLength(0) / 2 && z == maze.GetLength(1) / 2)
                {
                    var spawnObj = Instantiate(spawner, new Vector3(x, 0.5f, z), Quaternion.Euler(0f, 180f, 0f));
                    spawnObj.name = "Spawner" + "[" + x + ", " + z + "]";
                    spawnObj.transform.SetParent(walls.transform);

                    Transform spawnPoint = spawnObj.transform.Find("PlayerSpawnPoint").gameObject.transform;

                    minimap.transform.position = new Vector3(x, minimapSize, z);

                    if (GameObject.Find("TestPlayer(Clone)") == null)
                    {
                        var play = Instantiate(Player, new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z), Quaternion.identity);

                        Player.GetComponent<PlayerScript>().SetSpawn(spawnPoint);
                        game.SetPlayer(play.GetComponent<PlayerScript>());
                    }

                    else
                    {
                        GameObject.Find("TestPlayer(Clone)").gameObject.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
                    }
                }
            }
        }

        StartCoroutine(delayMazeGen());
    }

    private void CreateFloor(int[,] maze)
    {
        List<Vector3> floorVertices = new List<Vector3>();
        List<int> floorTriangles = new List<int>();
        Mesh floorMesh = new Mesh();

        floorVertices.Clear();
        floorTriangles.Clear();
        floorMesh.Clear();

        int startIndex = floorVertices.Count;
        floorVertices.Add(new Vector3(0f, 0f, 0f));
        floorVertices.Add(new Vector3(maze.GetLength(0), 0f, 0f));
        floorVertices.Add(new Vector3(0f, 0f, maze.GetLength(1)));
        floorVertices.Add(new Vector3(maze.GetLength(0), 0f, maze.GetLength(1)));

        floorTriangles.Add(startIndex + 0);
        floorTriangles.Add(startIndex + 2);
        floorTriangles.Add(startIndex + 1);

        floorTriangles.Add(startIndex + 1);
        floorTriangles.Add(startIndex + 2);
        floorTriangles.Add(startIndex + 3);

        floorMesh.vertices = floorVertices.ToArray();
        floorMesh.triangles = floorTriangles.ToArray();

        floorMesh.RecalculateNormals();

        floorMF.mesh = floorMesh;

        floorMC.sharedMesh = floorMF.mesh;
    }

    private void ClearMaze()
    {
        foreach (Transform children in walls.transform)
        {
            Destroy(children.gameObject);
        }
    }

    IEnumerator delayMazeGen()
    {
        yield return new WaitForSeconds(0.1f);

        FillMaze(maze);

        floor.BuildNavMesh();
    }

    private void FillMaze(int[,] size)
    {
        for(int x = 0; x < size.GetLength(0) + 1; x++)
        {
            for(int z = 0; z < size.GetLength(1) + 1; z++)
            {
                checkedList.Clear();
                notSpawned = false;

                RandomFill(x, z);

                if(notSpawned)
                {
                    LoopThroughShapes(x, z);
                }
            }
        }

        AddScoreObjects(maze);
    }

    private void RandomFill(int x, int z)
    {
        //Spawn Obstacles
        int random = Random.Range(0, 12);

        switch (random)
        {
            //Horizontal Shapes
            case 0:

                if (!Physics.CheckBox(new Vector3(x + 1.5f, 1, z - 0.5f), new Vector3(3f, 1.5f, 2f), Quaternion.identity, layer))
                {
                    var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 90f, 0));
                    wallObj.name = "2x4" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                else
                {
                    checkedList.Add(wallTypes[random].gameObject);
                    notSpawned = true;
                }

                break;

            case 1:

                if (!Physics.CheckBox(new Vector3(x + 1f, 1, z), new Vector3(3f, 1.5f, 1.5f), Quaternion.identity, layer))
                {
                    var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.identity);
                    wallObj.name = "h3x1" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                else
                {
                    checkedList.Add(wallTypes[random].gameObject);
                    notSpawned = true;
                }

                break;

            case 2:

                if (!Physics.CheckBox(new Vector3(x + 1f, 1, z + 0.5f), new Vector3(2.5f, 1.5f, 2f), Quaternion.identity, layer))
                {
                    var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.identity);
                    wallObj.name = "lWall" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                else
                {
                    checkedList.Add(wallTypes[random].gameObject);
                    notSpawned = true;
                }

                break;

            case 3:

                if (!Physics.CheckBox(new Vector3(x - 1f, 1, z - 0.5f), new Vector3(2.5f, 1.5f, 2f), Quaternion.identity, layer))
                {
                    var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 180f, 0));
                    wallObj.name = "lWall" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                else
                {
                    checkedList.Add(wallTypes[random].gameObject);
                    notSpawned = true;
                }

                break;

            case 4:

                if (!Physics.CheckBox(new Vector3(x, 1, z + 1f), new Vector3(3.5f, 1.5f, 3f), Quaternion.identity, layer))
                {
                    var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.identity);
                    wallObj.name = "tWall" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                else
                {
                    checkedList.Add(wallTypes[random].gameObject);
                    notSpawned = true;
                }

                break;

            case 5:

                if (!Physics.CheckBox(new Vector3(x, 1, z - 1f), new Vector3(3.5f, 1.5f, 3f), Quaternion.identity, layer))
                {
                    var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 180f, 0));
                    wallObj.name = "tWall" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                else
                {
                    checkedList.Add(wallTypes[random].gameObject);
                    notSpawned = true;
                }

                break;

            //Vertical
            case 6:

                if (!Physics.CheckBox(new Vector3(x + 0.5f, 1, z + 1.5f), new Vector3(2f, 1.5f, 3f), Quaternion.identity, layer))
                {
                    var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.identity);
                    wallObj.name = "2x4" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                else
                {
                    checkedList.Add(wallTypes[random].gameObject);
                    notSpawned = true;
                }

                break;

            case 7:

                if (!Physics.CheckBox(new Vector3(x, 1, z - 1f), new Vector3(1.5f, 1.5f, 3f), Quaternion.identity, layer))
                {
                    var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 90f, 0));
                    wallObj.name = "v3x1" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                else
                {
                    checkedList.Add(wallTypes[random].gameObject);
                    notSpawned = true;
                }

                break;

            case 8:

                if (!Physics.CheckBox(new Vector3(x - 0.5f, 1, z + 1f), new Vector3(2.5f, 1.5f, 2.5f), Quaternion.identity, layer))
                {
                    var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 270f, 0));
                    wallObj.name = "lWall" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                else
                {
                    checkedList.Add(wallTypes[random].gameObject);
                    notSpawned = true;
                }

                break;

            case 9:

                if (!Physics.CheckBox(new Vector3(x + 0.5f, 1, z - 1f), new Vector3(2.5f, 1.5f, 2.5f), Quaternion.identity, layer))
                {
                    var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 90f, 0));
                    wallObj.name = "lWall" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                else
                {
                    checkedList.Add(wallTypes[random].gameObject);
                    notSpawned = true;
                }

                break;

            case 10:

                if (!Physics.CheckBox(new Vector3(x - 1f, 1, z), new Vector3(3f, 1.5f, 4f), Quaternion.identity, layer))
                {
                    var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 270f, 0));
                    wallObj.name = "tWall" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                else
                {
                    checkedList.Add(wallTypes[random].gameObject);
                    notSpawned = true;
                }

                break;

            case 11:

                if (!Physics.CheckBox(new Vector3(x + 1f, 1, z), new Vector3(3f, 1.5f, 4f), Quaternion.identity, layer))
                {
                    var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 90f, 0));
                    wallObj.name = "tWall" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(walls.transform);
                }

                else
                {
                    checkedList.Add(wallTypes[random].gameObject);
                    notSpawned = true;
                }

                break;

            default:
                break;
        }
    }

    private void LoopThroughShapes(int x, int z)
    {
        for(int i = 0; i < checkedList.Count; i++)
        {
            int random = Random.Range(0, 12);

            if (wallTypes[random].gameObject == checkedList[i].gameObject)
            {
                break;
            }

            else
            {
                switch (random)
                {
                    //Horizontal Shapes
                    case 0:

                        if (!Physics.CheckBox(new Vector3(x + 1.5f, 1, z - 0.5f), new Vector3(3f, 1.5f, 2f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 90f, 0));
                            wallObj.name = "2x4" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        else
                        {
                            checkedList.Add(wallTypes[random]);
                        }

                        break;

                    case 1:

                        if (!Physics.CheckBox(new Vector3(x + 1f, 1, z), new Vector3(3f, 1.5f, 1.5f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.identity);
                            wallObj.name = "h3x1" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        else
                        {
                            checkedList.Add(wallTypes[random]);
                        }

                        break;

                    case 2:

                        if (!Physics.CheckBox(new Vector3(x + 1f, 1, z + 0.5f), new Vector3(2.5f, 1.5f, 2f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.identity);
                            wallObj.name = "lWall" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        else
                        {
                            checkedList.Add(wallTypes[random]);
                        }

                        break;

                    case 3:

                        if (!Physics.CheckBox(new Vector3(x - 1f, 1, z - 0.5f), new Vector3(2.5f, 1.5f, 2f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 180f, 0));
                            wallObj.name = "lWall" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        else
                        {
                            checkedList.Add(wallTypes[random]);
                        }

                        break;

                    case 4:

                        if (!Physics.CheckBox(new Vector3(x, 1, z + 1f), new Vector3(3.5f, 1.5f, 3f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.identity);
                            wallObj.name = "tWall" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        else
                        {
                            checkedList.Add(wallTypes[random]);
                        }

                        break;

                    case 5:

                        if (!Physics.CheckBox(new Vector3(x, 1, z - 1f), new Vector3(3.5f, 1.5f, 3f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 180f, 0));
                            wallObj.name = "tWall" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        else
                        {
                            checkedList.Add(wallTypes[random]);
                        }

                        break;

                    //Vertical
                    case 6:

                        if (!Physics.CheckBox(new Vector3(x + 0.5f, 1, z + 1.5f), new Vector3(2f, 1.5f, 3f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.identity);
                            wallObj.name = "2x4" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        else
                        {
                            checkedList.Add(wallTypes[random]);
                        }

                        break;

                    case 7:

                        if (!Physics.CheckBox(new Vector3(x, 1, z - 1f), new Vector3(1.5f, 1.5f, 3f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 90f, 0));
                            wallObj.name = "v3x1" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        else
                        {
                            checkedList.Add(wallTypes[random]);
                        }

                        break;

                    case 8:

                        if (!Physics.CheckBox(new Vector3(x - 0.5f, 1, z + 1f), new Vector3(2.5f, 1.5f, 2.5f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 270f, 0));
                            wallObj.name = "lWall" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        else
                        {
                            checkedList.Add(wallTypes[random]);
                        }

                        break;

                    case 9:

                        if (!Physics.CheckBox(new Vector3(x + 0.5f, 1, z - 1f), new Vector3(2.5f, 1.5f, 2.5f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 90f, 0));
                            wallObj.name = "lWall" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        else
                        {
                            checkedList.Add(wallTypes[random]);
                        }

                        break;

                    case 10:

                        if (!Physics.CheckBox(new Vector3(x - 1f, 1, z), new Vector3(3f, 1.5f, 4f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 270f, 0));
                            wallObj.name = "tWall" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        else
                        {
                            checkedList.Add(wallTypes[random]);
                        }

                        break;

                    case 11:

                        if (!Physics.CheckBox(new Vector3(x + 1f, 1, z), new Vector3(3f, 1.5f, 4f), Quaternion.identity, layer))
                        {
                            var wallObj = Instantiate(wallTypes[random].gameObject, new Vector3(x, 0.5f, z), Quaternion.Euler(0, 90f, 0));
                            wallObj.name = "tWall" + "[" + x + ", " + z + "]";
                            wallObj.transform.SetParent(walls.transform);
                        }

                        else
                        {
                            checkedList.Add(wallTypes[random]);
                        }

                        break;

                    default:
                        break;
                }
            }
        }
        
    }

    private void AddScoreObjects(int[,] size)
    {
        for(int x = 0; x < size.GetLength(0); x++)
        {
            for(int z = 0; z < size.GetLength(1); z++)
            {
                if (!Physics.CheckBox(new Vector3(x, 1, z), new Vector3(0.4f, 0.5f, 0.4f), Quaternion.identity, layer))
                {
                    if(x == 1 && z == 1 || x == width - 1 && z == 1 || x == 1 && z == height - 1 || x == width - 1 && z == height - 1)
                    {
                        AddPowerUps(x, z);
                    }

                    else
                    {
                        var scoreObj = Instantiate(score, new Vector3(x, 1, z), Quaternion.identity);
                        scoreObj.name = "Score" + "[" + x + ", " + z + "]";
                        scoreObj.transform.SetParent(scores.transform);

                        scoreList.Add(scoreObj);
                    }

                    game.SetCarrotAmount(1);
                }
            }
        } 
    }

    private void AddPowerUps(int x, int z)
    {
        var scoreObj = Instantiate(PowerUpObj, new Vector3(x, 1, z), Quaternion.identity);
        scoreObj.name = "PowerUp" + "[" + x + ", " + z + "]";
        scoreObj.transform.SetParent(scores.transform);
    }

    public IEnumerator DelayMazeGen()
    {
        ClearMaze();

        yield return new WaitForSeconds(0.1f);

        GenerateMazeLayout();
    }

    public int GetLevel()
    {
        return level;
    }

    public int SetLevel()
    {
        return level += 1;
    }
}
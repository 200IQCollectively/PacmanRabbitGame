using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMazeGen : MonoBehaviour
{
    public GameObject wall;

    [Range(10, 50)] public int width = 28;
    [Range(10, 50)] public int height = 31;

    //Must be public/ private serializefield or it doesn't work and no one knows why
    public List<WallNode> nodes;
    public List<WallNode> wallList;
    public List<WallNode> mazeList;
    public List<GameObject> mazeWallList;

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

                if (x == 0 || x == width || z == 0 || z == height)
                {
                    WallNode node = new WallNode(x, z, true);
                    nodes.Add(node);
                    var wallObj = Instantiate(wall, new Vector3(x, 0, z), Quaternion.identity);
                    wallObj.name = "Node[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(gameObject.transform);
                    wallList.Add(node);
                }

                else
                {
                    int random = Random.Range(0, 2);

                    if (random == 1)
                    {
                        WallNode node = new WallNode(x, z, true);
                        nodes.Add(node);
                        var wallObj = Instantiate(wall, new Vector3(x, 0, z), Quaternion.identity);
                        wallObj.name = "Node[" + x + ", " + z + "]";
                        wallObj.transform.SetParent(gameObject.transform);
                        mazeList.Add(node);
                        mazeWallList.Add(wallObj);
                    }

                    else
                    {
                        WallNode node = new WallNode(x, z, false);
                        nodes.Add(node);
                        var scoreObj = Instantiate(score, new Vector3(x, 1.5f, z), Quaternion.identity);
                        scoreObj.name = "Node[" + x + ", " + z + "]";
                        scoreObj.transform.SetParent(gameObject.transform);
                    }            
                }       
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMazeGen : MonoBehaviour
{
    public GameObject wall;

    [Range(10, 50)] public int width = 28;
    [Range(10, 50)] public int height = 31;

    //Must be public/ private serializefield or it doesn't work and no one knows why
    public int[,] maze;

    public GameObject score;

    // Start is called before the first frame update
    private void Start()
    {
        GenerateMazeLayout();
    }

    private void GenerateMazeLayout()
    {
        maze = new int[width, height];

        int wallBorderSize = 1;
        int[,] wallBorder = new int[width + wallBorderSize * 2, height + wallBorderSize * 2];

        for(int x = 0; x < wallBorder.GetLength(0); x++)
        {
            for(int z = 0; z < wallBorder.GetLength(1); z++)
            {
                if (x >= wallBorderSize && x < width + wallBorderSize && z >= wallBorderSize && z < height + wallBorderSize)
                {
                    wallBorder[x, z] = maze[x - wallBorderSize, z - wallBorderSize];

                    FillMaze(x, z);
                }

                else
                {
                    wallBorder[x, z] = 1;

                    var wallObj = Instantiate(wall, new Vector3(x, 0, z), Quaternion.identity);
                    wallObj.name = "Wall" + "[" + x + ", " + z + "]";
                    wallObj.transform.SetParent(gameObject.transform);
                }
            }
        }

        
    }

    private void FillMaze(int x, int z)
    {
        if(x > 0 || x < width || z > 0 || z < height)
        {
            int random = Random.Range(0, 2);

            if (random == 1)
            {
                var wallObj = Instantiate(wall, new Vector3(x, 0, z), Quaternion.identity);
                wallObj.name = "Wall" + "[" + x + ", " + z + "]";
                wallObj.transform.SetParent(gameObject.transform);
            }

            else
            {
                var scoreObj = Instantiate(score, new Vector3(x, 1, z), Quaternion.identity);
                scoreObj.name = "Score" + "[" + x + ", " + z + "]";
                scoreObj.transform.SetParent(gameObject.transform);
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


struct TileCoordinate
{
    public int tileX;
    public int tileZ;

    public TileCoordinate(int x, int z)
    {
        tileX = x;
        tileZ = z;
    }
}
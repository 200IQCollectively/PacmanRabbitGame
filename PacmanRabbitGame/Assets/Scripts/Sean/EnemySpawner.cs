using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;

    public GameObject enemy;

    public float timer = 4.0f;

    public GameHandler game;

    private Transform teleportPoint;

    private bool Released = false;

    private void Start()
    {
        game = GameObject.Find("GameHandler").GetComponent<GameHandler>();

        teleportPoint = transform.Find("EnemyTeleportPoint").gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        ReleaseEnemies();

        if(game.GetCarrotAmount() <= 0)
        {
            DestroyEnemies();
        }
    }

    private void ReleaseEnemies()
    {
        timer -= 1.0f * Time.deltaTime;
        if (game.isMultiplayer)
        { 
            if(timer <=0 && !Released)
            {
                game.ReleaseFoxPlayers();
                Released = !Released;
            }
        }
        else
        {
            

            if (enemies.Count != 4 && timer <= 0)
            {
                var fox = Instantiate(enemy, new Vector3(teleportPoint.position.x, teleportPoint.position.y, teleportPoint.position.z), Quaternion.identity);

                enemies.Add(fox);

                timer = 4.0f;
            }
        }
    }

    public void DestroyEnemies()
    {
        foreach(GameObject fox in enemies)
        {
            Destroy(fox);
        }
    }
}

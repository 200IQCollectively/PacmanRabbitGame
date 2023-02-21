using UnityEngine;


public class Pellet : MonoBehaviour
{
    public int points = 10;
    public rabbitplayer pacmans;

    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().PelletEaten(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("rabbit")) {
            Eat();
            pacmans.Eat();
        }
    }

}

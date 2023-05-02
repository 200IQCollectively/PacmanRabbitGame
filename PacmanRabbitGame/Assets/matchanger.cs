using UnityEngine;

public class matchanger : MonoBehaviour
{
    public Material[] materials; // An array of materials to apply to the AI characters

    private GameObject[] aiCharacters; // An array to hold references to the AI characters

    void Start()
    {
        // Find all game objects tagged as "AICharacter" and store them in the array
        aiCharacters = GameObject.FindGameObjectsWithTag("AI");

        // Loop through each AI character and apply a different material to each one
        for (int i = 0; i < aiCharacters.Length; i++)
        {
            Renderer renderer = aiCharacters[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = materials[i % materials.Length];
            }
        }
    }
}
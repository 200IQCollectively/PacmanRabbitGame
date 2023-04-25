using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Entry : MonoBehaviour
{
    private TextMeshProUGUI name;
    private TextMeshProUGUI pos;
    private TextMeshProUGUI score;

    private void Start()
    {
        name = gameObject.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>();
        pos = gameObject.transform.Find("Position").gameObject.GetComponent<TextMeshProUGUI>();
        score = gameObject.transform.Find("Score").gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void SetNamePositionScore(/*int _pos, */ int _score, string _name)
    {
        //pos.text = _pos.ToString();
        score.text = _score.ToString();
        name.text = _name.ToString(); 
    }
}

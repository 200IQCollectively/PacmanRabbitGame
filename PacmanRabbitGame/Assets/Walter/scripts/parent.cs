using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parent : MonoBehaviour
{

    public GameObject parents;

    public void rabbitonly(bool a)
    {
        parents.transform.GetChild(0).gameObject.SetActive(a);
    }
    public void explosioneffect(bool b)
    {
        parents.transform.GetChild(1).gameObject.SetActive(b);
    }
    public void whitewolfonly(bool c)
    {
        parents.transform.GetChild(2).gameObject.SetActive(c);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NODE: ScriptableObject 
{ 
    public enum State
    {
        Running,
        Failure,
        Success,

    }
    
}

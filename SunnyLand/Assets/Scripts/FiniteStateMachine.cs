using System.Collections;
using System.Collections.Generic;
using System;
using static System.Console;

using UnityEngine;


public delegate bool CallBack();

public struct State
{
    public Enum value;
    public CallBack callBack;
    public string name;

    public State(Enum value, CallBack callBack, string name)
    {
        this.value = value;
        this.callBack = callBack;
        this.name = name;
    }

    #region
    public static bool operator ==(State a, State b)
    {
        return a.name == b.name || a.value == b.value;
    }

    public static bool operator !=(State a, State b)
    {
        return a.name != b.name || a.value != b.value;
    }

    public override bool Equals(object obj)
    {

        return (obj.GetType() == GetType()) ? (name == ((State)(obj)).name) ? true : false : false;
    }

    // 경고 제거용
    //public override int GetHashCode()
    //{
    //    return base.GetHashCode();
    //}
    #endregion
}

public class FiniteStateMachine : MonoBehaviour
{
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

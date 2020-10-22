using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton
{
    protected static Singleton _instance;
    protected static Singleton Instance { get => GetInstance();}   

    protected Singleton()
    {

    }
    protected static Singleton GetInstance(){
            if (_instance != null)
                return _instance;
            _instance = new Singleton();
            return _instance;
        }
}

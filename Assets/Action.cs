using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action
{
    public abstract void Display(int x, int y);
    public abstract void Stop();
    public abstract void Start();
    public abstract Action Enact(int x,int y,World w);
}

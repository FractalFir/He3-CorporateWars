using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable()]
public abstract class Building 
{
    public abstract void Tick();
    public abstract void RefreshVisuals();
    public abstract GameObject GetPrefab();
    public abstract Building PlaceNewAt(int x, int y, WorldData worldData);
}

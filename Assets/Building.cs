using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable()]
public abstract class Building 
{
    protected int x;
    protected int y;
    protected float height;
    protected byte rotation;
    [System.NonSerialized()] 
    protected GameObject displayGO;
    public abstract void Tick(WorldData data);
    public void RefreshVisuals(){
        if(displayGO == null){
            SetupVisuals();
        }
    }
    public abstract GameObject GetPrefab();
    public virtual GameObject GetDisplayPrefab() => GetPrefab();
    public abstract Building PlaceNewAt(int x, int y, WorldData worldData);
    public virtual void SetupVisuals(){
        if(displayGO == null){
            GameObject prefab = GetPrefab();
            displayGO = GameObject.Instantiate(prefab,new Vector3((float)x + 0.5f,height,(float)y + 0.5f),Quaternion.AngleAxis(rotation*90, Vector3.up));
        }
    }
}

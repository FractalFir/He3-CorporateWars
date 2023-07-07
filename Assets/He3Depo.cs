using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class He3Depo : Building
{
    const float HE3_ROT_SEED = 914.24433553265f;
    [System.NonSerialized()] 
    GameObject He3DepoGO;
    int x;
    int y;
    //Value in range 0-3
    int rotation;
    public static He3Depo PlaceAt(int x, int y,WorldData data){
        int rotation = data.PlacementNoise(x,y,3,HE3_ROT_SEED);
        He3Depo depo = new He3Depo(x,y,rotation);
        depo.SetupVisuals();
        return depo;
    }
    public override Building PlaceNewAt(int x, int y,WorldData data){
        return PlaceAt(x,y,data);
    }
    private He3Depo(int x, int y,int rotation){
        this.rotation = rotation;
        this.x = x;
        this.y = y;
    }
    public override GameObject GetPrefab(){
        return World.GetHe3DepoPrefab();
    }
    public override void RefreshVisuals(){
        if(He3DepoGO == null){
            SetupVisuals();
        }
    }
    void SetupVisuals(){
        GameObject prefab = World.GetHe3DepoPrefab();
        GameObject go = GameObject.Instantiate(prefab,new Vector3((float)x + 0.5f,0.0f,(float)y + 0.5f),Quaternion.AngleAxis(rotation*90, Vector3.up));
    }
    public override void Tick(){
        //Visuals invliad, reset them!
        if(He3DepoGO == null){
            SetupVisuals();
        }
    }
}

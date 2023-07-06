using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RockType{
    Rocks1,
    Rocks2,
}
public class Rock : Building
{
    const int MAX_ROCK_TYPE = 2;
    const float ROCK_TYPE_SEED = 577.2446536f;
    const float ROCK_ROT_SEED = 224.244252265f;
    RockType type;
    [System.NonSerialized()] 
    GameObject rockGO;
    int x;
    int y;
    //Value in range 0-3
    int rockRotation;
    static RockType RockFromInt(int type){
        switch(type){
            case 0: 
                return RockType.Rocks1;
            case 1: 
                return RockType.Rocks2;
            default:
                return RockType.Rocks1;
        }
    }
    public static Building PlaceNewAt(int x, int y,WorldData data){
        RockType type = RockFromInt(data.PlacementNoise(x,y,MAX_ROCK_TYPE,ROCK_TYPE_SEED));
        int rotation = data.PlacementNoise(x,y,3,ROCK_ROT_SEED);
        Rock r = new Rock(x,y,type,rotation);
        r.SetupVisuals();
        return r;
    }
    private Rock(int x, int y, RockType type,int rockRotation){
        this.type = type;
        this.rockRotation = rockRotation;
        this.x = x;
        this.y = y;
    }
    void SetupVisuals(){
        GameObject prefab = World.GetRockGO(type);
        GameObject go = GameObject.Instantiate(prefab,new Vector3((float)x + 0.5f,0.0f,(float)y + 0.5f),Quaternion.AngleAxis(rockRotation*90, Vector3.up));
    }
    public override void Tick(){
        //Visuals invliad, reset them!
        if(rockGO == null){
            SetupVisuals();
        }
    }
}

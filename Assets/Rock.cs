using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RockType{
    Rocks1,
    Rocks2,
    Rocks3,
}
public class Rock : Building
{
    const int MAX_ROCK_TYPE = 3;
    const float ROCK_TYPE_SEED = 577.2446536f;
    const float ROCK_ROT_SEED = 224.244252265f;
    RockType type;
    //Value in range 0-3
    static RockType RockFromInt(int type){
        switch(type){
            case 0: 
                return RockType.Rocks1;
            case 1: 
                return RockType.Rocks2;
            case 2: 
                return RockType.Rocks3;
            default:
                return RockType.Rocks1;
        }
    }
    public static Building PlaceAt(int x, int y,WorldData data){
        RockType type = RockFromInt(data.PlacementNoise(x,y,MAX_ROCK_TYPE,ROCK_TYPE_SEED));
        int rotation = data.PlacementNoise(x,y,3,ROCK_ROT_SEED);
        Rock r = new Rock(x,y,type,(byte)rotation);
        r.height = data.GetHeightAt((float)x + 0.5f,(float)y + 0.5f);
        return r;
    }
    public override Building PlaceNewAt(int x, int y,WorldData data){
        return PlaceAt(x,y,data);
    }
    private Rock(int x, int y, RockType type,byte rotation){
        this.type = type;
        this.rotation = rotation;
        this.x = x;
        this.y = y;
    }
    public override GameObject GetPrefab(){
        return World.GetRockPrefab(type);
    }
    public override void Tick(WorldData data){
        //If Visuals invliad, reset them!
        RefreshVisuals();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class He3Depo : Building
{
    const float HE3_ROT_SEED = 914.24433553265f;
    //Value in range 0-3
    public static He3Depo PlaceAt(int x, int y,WorldData data){
        int rotation = data.PlacementNoise(x,y,3,HE3_ROT_SEED);
        He3Depo depo = new He3Depo(x,y,(byte)rotation);
        depo.height = data.GetHeightAt((float)x + 0.5f,(float)y + 0.5f);
        return depo;
    }
    public override Building PlaceNewAt(int x, int y,WorldData data){
        return PlaceAt(x,y,data);
    }
    private He3Depo(int x, int y,byte rotation){
        this.rotation = rotation;
        this.x = x;
        this.y = y;
    }
    public override GameObject GetPrefab(){
        return World.GetHe3DepoPrefab();
    }
    public override void Tick(WorldData data){
        //If Visuals invliad, reset them!
        RefreshVisuals();
    }
}

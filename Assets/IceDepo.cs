using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDepo : Building
{
    const float DEPO_ROT_SEED = 114.24433553265f;
    public static IceDepo PlaceAt(int x, int y, WorldData data){
        int rotation = data.PlacementNoise(x,y,3,DEPO_ROT_SEED);
        IceDepo iceDepo = new IceDepo(x,y,(byte)rotation);
        iceDepo.height = data.GetHeightAt((float)x + 0.5f,(float)y + 0.5f);
        return iceDepo;
    }
    private IceDepo(int x, int y,byte rotation){
        this.rotation = rotation;
        this.x = x;
        this.y = y;
    }
    public override Building PlaceNewAt(int x, int y, WorldData data) => PlaceAt(x,y,data);
    public override GameObject GetPrefab() => World.GetIceDepoPrefab();
    public override void Tick(WorldData data){}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite : Building
{
    const byte TOTAL_PROGRESS_NEEDED = 3;
    const float BUILD_ROT_SEED = 131.32f;
    byte progress;
    Building inProgress;
    public override GameObject GetPrefab() => World.GetConstructionSitePrefab();
    public override void SetupVisuals(){
        base.SetupVisuals();
        if(progress > TOTAL_PROGRESS_NEEDED/2){
            inProgress.SetupVisuals();
        }
    }
    public override void Tick(WorldData data){
        if(progress <= TOTAL_PROGRESS_NEEDED/2){
            height += 0.25f;
        }
        if(progress > TOTAL_PROGRESS_NEEDED/2){
            height -= 0.25f;
        }
        if(progress > TOTAL_PROGRESS_NEEDED){
            data.ReplaceBuilding(x,y,inProgress);
            GameObject.Destroy(displayGO);
        }
        displayGO.transform.position = new Vector3((float)x + 0.5f,height,(float)y + 0.5f);
        Debug.Log($"New pos {displayGO.transform.position},progress:{progress}");
        progress+=1;
    }
    public override GameObject GetDisplayPrefab()=>inProgress.GetDisplayPrefab();
    public static Dictionary<Building,ConstructionSite> builderCache = new Dictionary<Building,ConstructionSite>();
    public static ConstructionSite GetBuilderInstance(Building childBuilderInstance){
        ConstructionSite builderInstance;
        builderCache.TryGetValue(childBuilderInstance, out builderInstance);
        if(builderInstance == null){
            builderInstance = new ConstructionSite(childBuilderInstance);
            builderCache.Add(childBuilderInstance,builderInstance);
        }
        return builderInstance;
    }
    private ConstructionSite(Building inProgress){
        this.inProgress = inProgress;
    }
    private ConstructionSite(int x, int y, byte rotation,Building inProgress){
        this.rotation = rotation;
        this.x = x;
        this.y = y;
        this.inProgress = inProgress;
    }
    public override Building PlaceNewAt(int x,int y,WorldData data){
        int rotation = data.PlacementNoise(x,y,3,BUILD_ROT_SEED);
        Building inProgress = this.inProgress.PlaceNewAt(x,y,data);
        ConstructionSite cs = new ConstructionSite(x,y,(byte)rotation,inProgress);
        cs.height = data.GetHeightAt((float)x + 0.5f,(float)y + 0.5f) - 0.25f;
        return cs;
    }
}

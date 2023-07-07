using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementAction : Action
{
    static Dictionary<Building,GameObject> highlightChache = new Dictionary<Building,GameObject>(); 
    GameObject highligthGO;
    Building type;
    World w;
    public PlacementAction(World w, Building type){
        this.type = type;
        this.w = w;
        Start();
    }
    public override void Start(){
        GameObject chached;
        highlightChache.TryGetValue(type, out chached);
        if(chached == null){
            chached = GameObject.Instantiate(type.GetPrefab());
            MeshRenderer renderer = chached.GetComponent<MeshRenderer>();
            renderer.material = w.highlightMaterial;
            foreach(Collider c in chached.GetComponents<Collider> ()) {
                c.enabled = false;
            }
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            highlightChache.Add(type,chached);
        }
        chached.SetActive(true);
        highligthGO = chached;
    }
    public override void Display(int x, int y){
        highligthGO.transform.position = new Vector3((float)x + 0.5f,0.0f,(float)y + 0.5f);
    }
    public override void Stop(){
        highligthGO.SetActive(false);
    }
    public override Action Enact(int x,int y, World world){
        if(world.PlaceBuilding(x,y,type)){
            Stop();
            return null;
        }
        else{
            return this;
        }
    }
}

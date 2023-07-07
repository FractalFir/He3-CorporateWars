using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TileKind{
    Mare,
    Regolith,
    Highlands,
    Mountain,
}
static class TileKindMethods{
    public static Color GetColor(this TileKind kind){
        switch(kind){
            case TileKind.Mare:
                return new Color(0.05f,0.05f,26.0f/256.0f,1.0f);
            case TileKind.Regolith:
                return new Color(166.0f/256.0f,166.0f/256.0f,166.0f/256.0f,1.0f);
            case TileKind.Highlands:
                return new Color(190.0f/256.0f,190.0f/256.0f,190.0f/256.0f,1.0f);
            case TileKind.Mountain:
                return new Color(230.0f/256.0f,200.0f/256.0f,230.0f/256.0f,1.0f);
            default:
                throw new System.ArgumentException("ERROR: Tile kind declared and used, but has no color defined!");
        }
    }
}
public struct Tile{
    TileKind kind;
    public TileKind GetKind(){
        return kind;
    }
    public Tile(TileKind kind){
        this.kind = kind;
    }
}
public class WorldData 
{
    uint seed;
    float noiseSeedX;
    float noiseSeedY;
    Tile[,] grid;
    Building[,] buildings;
    int size;
    const float NOISE_SCALE = 0.125f;
    public int PlacementNoise(int x, int y,int max,float rSeed){
        return (int)(Mathf.PerlinNoise(x*.5f + noiseSeedX + rSeed, y*.5f + noiseSeedY - rSeed*0.242f)*max);
    }
    const float ROCK_SEED = 324.2453563f;
    const float HE_3_DEPO_SEED = 134.42345f;
    public bool PlaceBuilding(int x, int y,Building type){
        if(buildings[x,y] == null){
            buildings[x,y] = type.PlaceNewAt(x, y, this);
            return true;
        }
        return false;
    }
    public void RandomlyPlaceRocks(){
         for(int x = 0; x < size; x++){
            for(int y = 0; y < size; y++){
                if (0 == PlacementNoise(x,y,4,ROCK_SEED)){
                    this.buildings[x,y] = Rock.PlaceAt(x,y,this);
                }
            }
        }
    }
    public void RandomlyPlaceHe3Depos(){
         for(int x = 0; x < size; x++){
            for(int y = 0; y < size; y++){
                if (0 == PlacementNoise(x,y,32,HE_3_DEPO_SEED)){
                    this.buildings[x,y] = He3Depo.PlaceAt(x,y,this);
                }
            }
        }
    }
    public Mesh GenerateMesh(){
        Vector3[] vertices = new Vector3[size*size*4];
        int[] triangles = new int[size*size*6];
        Vector2[] uvs = new Vector2[size*size*4];
        Color[] colors = new Color[size*size*4];
        for(int x = 0; x < size; x++){
            for(int y = 0; y < size; y++){
                Color color = this[x,y].GetKind().GetColor();
                float h1 = GetHeightAt(x,y);
                float h2 = GetHeightAt(x + 1,y);
                float h3 = GetHeightAt(x + 1,y + 1);
                float h4 = GetHeightAt(x,y + 1);
                vertices[x*size*4 + y*4] = new Vector3((float)x,h1,(float)y);
                vertices[x*size*4 + y*4 + 1] = new Vector3((float)x + 1.0f ,h2,(float)y);
                vertices[x*size*4 + y*4 + 2] = new Vector3((float)x + 1.0f,h3,(float)y + 1.0f);
                vertices[x*size*4 + y*4 + 3] = new Vector3((float)x,h4,(float)y + 1.0f);
                colors[x*size*4 + y*4] = color;
                colors[x*size*4 + y*4 + 1] = color;
                colors[x*size*4 + y*4 + 2] = color;
                colors[x*size*4 + y*4 + 3] = color;
                uvs[x*size*4 + y*4] = new Vector2(x,y);
                uvs[x*size*4 + y*4 + 1] = new Vector2(x + 1,y);
                uvs[x*size*4 + y*4 + 2] = new Vector2(x + 1,y + 1);
                uvs[x*size*4 + y*4 + 3] = new Vector2(x,y + 1);
            }
        }
        for(int x = 0; x < size; x++){
            for(int y = 0; y < size; y++){
                triangles[x*size*6 + y*6] = x*size*4 + y*4 + 2;
                triangles[x*size*6 + y*6 + 1] = x*size*4 + y*4 + 1;
                triangles[x*size*6 + y*6 + 2] = x*size*4 + y*4;
                triangles[x*size*6 + y*6 + 3] = x*size*4 + y*4;
                triangles[x*size*6 + y*6 + 4] = x*size*4 + y*4 + 3;
                triangles[x*size*6 + y*6 + 5] = x*size*4 + y*4 + 2;
            }
        }
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
    public float GetHeightAt(float x, float y){
        float height = GetNoiseHeight(x,y);
        return Mathf.Clamp(height,0.25f,0.75f);
    } 
    float GetNoiseHeight(float x, float y){
        float height = Mathf.PerlinNoise(x*NOISE_SCALE + noiseSeedX, y*NOISE_SCALE + noiseSeedY)*.5f;
        height += Mathf.PerlinNoise(x*NOISE_SCALE*.5f + noiseSeedX, y*NOISE_SCALE*.5f + noiseSeedY)*.25f;
        height += Mathf.PerlinNoise(x*NOISE_SCALE*.25f + noiseSeedX, y*NOISE_SCALE*.25f + noiseSeedY)*.125f;
        height /= 0.875f;
        //Debug.Log($"height:{height} x:{x} y:{y}");
        return height;
    }
    public static TileKind TileKindFromHeight(float height){
        const float MARE_TRESHOLD = .25f;
        const float REGOLITH_THRESHOLD = 0.5f;
        const float HIGHLANDS_THRESHOLD = 0.875f;
        if(height < MARE_TRESHOLD){
            return TileKind.Mare;
        }
        else if (height < REGOLITH_THRESHOLD){
            return TileKind.Regolith;
        }
        else if (height < HIGHLANDS_THRESHOLD){
            return TileKind.Highlands;
        }
        else{
            return TileKind.Mountain;
        }
    }
    public int sideSize{
        get => size;
    }
    public WorldData(int size,uint seed){
        this.size = size;
        this.grid = new Tile[size,size];
        this.buildings = new Building[size,size];
        this.seed = seed;
        this.noiseSeedX = (float)(seed % System.UInt16.MaxValue);
        this.noiseSeedY = (float)(seed / System.UInt16.MaxValue);
        for(int x = 0; x < size; x++){
            for(int y = 0; y < size; y++){
                this.grid[x,y] = new Tile(TileKindFromHeight(GetNoiseHeight((float)x,(float)y)));
            }
        }
        RandomlyPlaceRocks();
        RandomlyPlaceHe3Depos();
    }
    public bool IsWithinRange(int posX, int posY, int targetX,int targetY,int range){
        if(targetX < 0 || targetY < 0 || targetX >= this.sideSize || targetY >= this.sideSize){
            return false;
        }
        int shortestDistance = System.Math.Abs(posX - targetX) + System.Math.Abs(posY - targetY);
        if(shortestDistance > range){
            return false;
        }
        ///TODO:evalute path viability
        return true;
    }
    public IEnumerable<Building> GetBuildingsWithinRange(int x, int y, int range){
        //TODO: changing those to be always within range should slightly the search speed up.
        int rangeStartX = x - range;
        int rangeEndX = x + range;
        int rangeStartY = y - range;
        int rangeEndY = y + range;
        for(int cx = rangeStartX; cx <= rangeEndX; cx++){
            for(int cy = rangeStartY; cy <= rangeEndY; cy++){
                if(IsWithinRange(x,y,cx,cy,range)){
                    Building b = this.buildings[x,y];
                    if(b != null)yield return b;
                }
            }
        }
    }
    public Tile this[int x,int y]{
        get => this.grid[x,y];
    }
}

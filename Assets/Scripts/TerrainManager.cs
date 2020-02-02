using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public Transform terrainT1;
    public Transform terrainT2;
    private int terrainIndex = 200;

    public void HandleRailIndex(int railIndex) {
        if((railIndex - GameManager.RAIL_LENGTH) > terrainIndex) {
            terrainT1.position += new Vector3(0, 0, 400);
            terrainIndex += 200;
            Transform temp = terrainT1;
            terrainT1 = terrainT2;
            terrainT2 = temp;
        }
    }
}

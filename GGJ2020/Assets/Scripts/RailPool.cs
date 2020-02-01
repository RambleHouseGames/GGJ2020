using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailPool : MonoBehaviour {

    public GameObject railPrefab;
    public Transform freeRailContainer;
    
    private readonly List<GameObject> freeRails = new List<GameObject>();

    public GameObject GetRail(Transform parent) {
        GameObject result;
        if (freeRails.Count > 0) {
            result = freeRails[freeRails.Count - 1];
            result.gameObject.SetActive(true);
            result.transform.SetParent(parent, false);
            freeRails.RemoveAt(freeRails.Count - 1);
        } else {
            result = CreateRail(parent);
        }
        return result;
    }

    private GameObject CreateRail(Transform parent) {
        return Object.Instantiate(railPrefab, parent);
    }

    public void DisposeRail(GameObject rail) {
        rail.gameObject.SetActive(false);
        rail.transform.SetParent(freeRailContainer, false);
        freeRails.Add(rail);
    }
}

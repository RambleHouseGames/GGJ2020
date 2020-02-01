using System.Collections;
using UnityEngine;

public class HitTypeUI : MonoBehaviour {
    public Transform leftSpawnLocation;
    public Transform rightSpawnLocation;

    public GameObject greatPrefab;
    public GameObject okPrefab;
    public GameObject missPrefab;

    public void ShowHit(HitType hitType, bool left) {
        GameObject hitPrefab;
        switch (hitType) {
            case HitType.Great:
                hitPrefab = greatPrefab;
                break;
            case HitType.OK:
                hitPrefab = okPrefab;
                break;
            case HitType.Miss:
                hitPrefab = missPrefab;
                break;
            default:
                hitPrefab = missPrefab;
                break;
        }
        Transform hitParent = left ? leftSpawnLocation : rightSpawnLocation;
        GameObject newHit = Instantiate(hitPrefab, hitParent);
        newHit.transform.localPosition = Vector3.zero;
        StartCoroutine(MoveHitUI(newHit, left));
    }

    private IEnumerator MoveHitUI(GameObject hitUI, bool left) {
        Transform t = hitUI.transform;
        float xSpeed = left ? -1f : 1f;
        float zSpeed = 4.5f;
        while (true) {
            t.localPosition += new Vector3(
                xSpeed * Time.deltaTime,
                0.666f * Time.deltaTime,
                zSpeed * Time.deltaTime
            );
            zSpeed -= 10 * Time.deltaTime;
            yield return null;
            if(t.localPosition.z < -2) {
                //Destroy(hitUI);
                yield break;
            }
        }
    }
}

public enum HitType {
    Great,
    OK,
    Miss
}

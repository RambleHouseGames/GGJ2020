using UnityEngine;

public class HitTypeUI : MonoBehaviour {
    public Transform leftSpawnLocation;
    public Transform rightSpawnLocation;

    public void ShowHit(HitType hitType, bool left) {

    }
}

public enum HitType {
    Great,
    OK,
    Miss
}

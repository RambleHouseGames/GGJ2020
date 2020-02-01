using System.Collections.Generic;
using UnityEngine;

public class RailSet {
    public GameObject rail;
    public BeatType beatType;
    public float time;
    public bool wasHit = false;
    public int railIndex;
    public GameObject decoration;

    public static RailSet  GetNearestMatchingRailSet(List<RailSet> rails, float time, BeatType beatType, out float nearestTime, out int nearestIndex) {
        RailSet nearestRail = rails[0];
        nearestTime = float.MaxValue;
        nearestIndex = 0;
        for (int i = 0; i < rails.Count; i++) {
            RailSet rail = rails[i];
            if (rail.beatType == beatType) {
                float timeDiff = Mathf.Abs(rail.time - time);
                if (timeDiff < nearestTime) {
                    nearestRail = rail;
                    nearestTime = timeDiff;
                    nearestIndex = i;
                }
            }
        }
        return nearestRail;
    }

    public static RailSet GetNearestRailSet(List<RailSet> rails, float time, out float nearestTime) {
        RailSet nearestRail = rails[0];
        nearestTime = float.MaxValue;
        for (int i = 0; i < rails.Count; i++) {
            RailSet rail = rails[i];
            float timeDiff = Mathf.Abs(rail.time - time);
            if (timeDiff < nearestTime) {
                nearestRail = rail;
                nearestTime = timeDiff;
            }
        }
        return nearestRail;
    }
}


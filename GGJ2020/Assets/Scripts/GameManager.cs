using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public RailPool railPool;
    public RailPool leftRailPool;
    public RailPool rightRailPool;
    public Transform railContainer;

    public TextAsset song1;
    private BeatType[] currentSong;

    public Transform cameraT;

    private List<RailSet> activeRails = new List<RailSet>();
    private bool gameStarted = false;
    private float startTime;
    private const float PERFECT_DISTANCE = 0.12f;
    private const float GOOD_DISTANCE = 0.2f;
    private const int RAIL_LENGTH = 40;
    private const int ON_BEAT_TIE_INDEX = 2;
    private const float BPM = 180;
    private const float BEATS_PER_SECOND = BPM / 60f;
    private const float SECONDS_PER_BEAT = 1f / BEATS_PER_SECOND;
    private int railIndex = 0;
    IEnumerator Start() {
        currentSong = SongParser.ParseSong(song1);
        for (int i = 0; i < RAIL_LENGTH; i++) {
            activeRails.Add(AddRail(railIndex, currentSong[railIndex]));
            railIndex++;
        }
        yield return new WaitForSeconds(1f);
        startTime = Time.time;
        gameStarted = true;
        for (int i = ON_BEAT_TIE_INDEX; i < activeRails.Count; i++) {
            float centerTime = startTime + GetTimeForIndex(i);
            activeRails[i].time = centerTime;
        }
    }

    private static float GetTimeForIndex(int i) {
        return ((i - ON_BEAT_TIE_INDEX) * SECONDS_PER_BEAT);
    }

    private RailSet AddRail(int index, BeatType beatType) {
        RailSet newRail = GetRailForBeatType(beatType);
        newRail.rail.transform.position = GetRailPosition(index);
        return newRail;
    }

    private Vector3 GetRailPosition(int index) {
        return new Vector3(0, 0, 1 * index);
    }

    private float timeSinceLastRail = SECONDS_PER_BEAT;
    void Update() {
        if (gameStarted) {
            if (timeSinceLastRail <= 0) {
                RailSet railToRemove = activeRails[0];
                GetPoolForBeatType(railToRemove.beatType).DisposeRail(railToRemove.rail);
                activeRails.RemoveAt(0);

                RailSet newRail = AddRail(railIndex, currentSong[railIndex]);
                float centerTime = startTime + GetTimeForIndex(railIndex);
                newRail.time = centerTime;
                activeRails.Add(newRail);

                railIndex++;

                timeSinceLastRail += SECONDS_PER_BEAT;
            }
            timeSinceLastRail -= Time.deltaTime;

            if (LeftPressed()) {
                HandleLeftHit();
            } else if (RightPressed()) {
                HandleRightHit();
            }
        }
    }

    private static bool LeftPressed() {
        return Input.GetKeyDown(KeyCode.LeftShift)
            || Input.GetKeyDown(KeyCode.LeftArrow);
    }

    private static bool RightPressed() {
        return Input.GetKeyDown(KeyCode.RightShift)
            || Input.GetKeyDown(KeyCode.RightArrow);
    }

    private void HandleLeftHit() {
        RailSet nearestRail = GetNearestRailSet(Time.time, out float nearestTime);
        HitType hitType = HitTypeForTime(nearestTime);
        Color theColor = Color.white;
        switch (hitType) {
            case HitType.Perfect:
                theColor = Color.green;
                break;
            case HitType.Good:
                theColor = Color.yellow;
                break;
            case HitType.Miss:
                theColor = Color.red;
                break;
        }
        nearestRail.rail.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = theColor;
        Debug.Log(nearestTime);
    }

    private void HandleRightHit(){
        RailSet nearestRail = GetNearestRailSet(Time.time, out float nearestTime);
        HitType hitType = HitTypeForTime(nearestTime);
        Color theColor = Color.white;
        switch (hitType) {
            case HitType.Perfect:
                theColor = Color.green;
                break;
            case HitType.Good:
                theColor = Color.yellow;
                break;
            case HitType.Miss:
                theColor = Color.red;
                break;
        }
        nearestRail.rail.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = theColor;
    }

    private void LateUpdate() {
        if (gameStarted) {
            cameraT.position += new Vector3(0, 0, BEATS_PER_SECOND * Time.deltaTime);
        }
    }

    private RailSet GetRailForBeatType(BeatType beatType) {
        RailPool pool = GetPoolForBeatType(beatType);
        GameObject newRail = pool.GetRail(railContainer);
        return new RailSet() {
            beatType = beatType,
            rail = newRail
        };
    }

    private RailPool GetPoolForBeatType(BeatType beatType) {
        switch (beatType) {
            case BeatType.Normal:
                return railPool;
            case BeatType.LeftBend:
                return leftRailPool;
            case BeatType.RightBend:
                return rightRailPool;
            default:
                return railPool;
        }
    }

    private class RailSet {
        public GameObject rail;
        public BeatType beatType;
        public float time;
    }

    private RailSet GetNearestRailSet(float time, out float nearestTime) {
        RailSet nearestRail = activeRails[0];
        nearestTime = float.MaxValue;
        for (int i = 0; i < activeRails.Count; i++) {
            RailSet rail = activeRails[i];
            float timeDiff = Mathf.Abs(rail.time - time);
            if(timeDiff < nearestTime) {
                nearestRail = rail;
                nearestTime = timeDiff;
            }
        }
        return nearestRail;
    }

    private void OnGUI() {
        GUI.Label(new Rect(0,0, 100, 100), Time.time.ToString());
    }

    private static HitType HitTypeForTime(float timeDiff) {
        if(timeDiff < PERFECT_DISTANCE) {
            return HitType.Perfect;
        } else if (timeDiff < GOOD_DISTANCE) {
            return HitType.Good;
        } else {
            return HitType.Miss;
        }
    }

    private enum HitType {
        Perfect,
        Good,
        Miss
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public RailPool railPool;
    public RailPool leftRailPool;
    public RailPool rightRailPool;
    public Transform railContainer;

    public AudioSource mainSource;
    public TextAsset song1;
    private BeatType[] currentSong;

    public Transform cameraT;
    public HitTypeUI hitTypeUI;

    public Text scoreText;
    private int Score {
        get {
            return SessionInfo.score;
        }
        set {
            SessionInfo.score = value;
            scoreText.text = "Score: " + SessionInfo.score.ToString();
        }
    }

    public Text healthText;
    private int health = 100;
    private int Health {
        get {
            return health;
        }
        set {
            health = value;
            if(health <= 0) {
                SceneManager.LoadScene("Result");
            }
            healthText.text = "Health: " + health.ToString();
        }
    }

    private List<RailSet> activeRails = new List<RailSet>();
    private bool gameStarted = false;
    private float startTime;
    private const float PERFECT_DISTANCE = 0.1f;
    private const float GOOD_DISTANCE = 0.2f;
    private const int RAIL_LENGTH = 50;
    private const int ON_BEAT_TIE_INDEX = 2;
    private const float BPM = 720;
    private const float BEATS_PER_SECOND = BPM / 60f;
    private const float SECONDS_PER_BEAT = 1f / BEATS_PER_SECOND;
    private int railIndex = 0;
    IEnumerator Start() {
        SessionInfo.Reset();
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
        mainSource.Play();
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
        return new Vector3(0, 0, index);
    }

    private float timeSinceLastRail = SECONDS_PER_BEAT;
    void Update() {
        if (gameStarted) {
            if (timeSinceLastRail <= 0) {
                RailSet railToRemove = activeRails[0];
                if(railToRemove.beatType == BeatType.Normal) {
                    if (railToRemove.wasHit) {
                        Health -= 5;
                    }
                } else {
                    if (!railToRemove.wasHit) {
                        Health -= 5;
                    }
                }
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
        RailSet nearestMatchingRail = RailSet.GetNearestMatchingRailSet(activeRails, Time.time, BeatType.LeftBend, out float nearestTime);
        HitType hitType = HitTypeForTime(nearestTime);
        HandleHitType(hitType);
        hitTypeUI.ShowHit(hitType, true);

        if (hitType != HitType.Miss) {
            nearestMatchingRail.wasHit = true;
        } else {
            RailSet nearestRail = RailSet.GetNearestRailSet(activeRails, Time.time, out nearestTime);
            nearestRail.wasHit = true;
        }
    }

    private void HandleRightHit() {
        RailSet nearestMatchingRail = RailSet.GetNearestMatchingRailSet(activeRails, Time.time, BeatType.RightBend, out float nearestTime);
        HitType hitType = HitTypeForTime(nearestTime);
        HandleHitType(hitType);
        hitTypeUI.ShowHit(hitType, false);

        if(hitType != HitType.Miss) {
            nearestMatchingRail.wasHit = true;
        } else {
            RailSet nearestRail = RailSet.GetNearestRailSet(activeRails, Time.time, out nearestTime);
            nearestRail.wasHit = true;
        }
    }

    private void HandleHitType(HitType hitType) {
        switch (hitType) {
            case HitType.Great:
                SessionInfo.greatCount++;
                Score += 100;
                break;
            case HitType.OK:
                SessionInfo.okCount++;
                Score += 50;
                break;
            case HitType.Miss:
                SessionInfo.missCount++;
                break;
        }
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

    private void OnGUI() {
        GUI.Label(new Rect(0,0, 100, 100), Time.time.ToString());
    }

    private static HitType HitTypeForTime(float timeDiff) {
        if(timeDiff < PERFECT_DISTANCE) {
            return HitType.Great;
        } else if (timeDiff < GOOD_DISTANCE) {
            return HitType.OK;
        } else {
            return HitType.Miss;
        }
    }
}

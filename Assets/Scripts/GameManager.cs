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
    public GameObject[] decorations;
    public GameObject fencePrefab;

    private BeatType[] currentSong;
    private BeatType GetBeatOfCurrentSong(int beatIndex) {
        if(beatIndex < currentSong.Length) {
            return currentSong[beatIndex];
        } else {
            GoToResultScreen();
            return BeatType.Normal;
        }
    }

    public Image fadeImage;

    public HammerManager hammerManager;
    public TerrainManager terrainManager;
    public ReadyGoManager readyGoManager;
    public AudioManager audioManager;
    public Transform headT;
    public Transform cameraT;
    private Vector3 cameraStartPos;

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
                GoToResultScreen();
            }
            healthText.text = "Health: " + health.ToString();
        }
    }

    private List<RailSet> activeRails = new List<RailSet>();
    private bool gameStarted = false;
    private float startTime;
    private const float PERFECT_DISTANCE = 0.09f;
    private const float GOOD_DISTANCE = 0.2f;
    public const int RAIL_LENGTH = 45;
    private const int ON_BEAT_TIE_INDEX = 2;
    private const float BPM = 720;
    private const float BEATS_PER_SECOND = BPM / 60f;
    private const float SECONDS_PER_BEAT = 1f / BEATS_PER_SECOND;
    private int railIndex = 0;
    IEnumerator Start() {
        cameraStartPos = headT.position;
        Health = 100;
        Score = 0;
        SessionInfo.Reset();
        TextAsset notes = audioManager.SetupSong(SessionInfo.stageNum == 0);

        startTime = audioManager.Time;
        currentSong = SongParser.ParseSong(notes);
        for (int i = 0; i < RAIL_LENGTH; i++) {
            activeRails.Add(AddRail(railIndex, GetBeatOfCurrentSong(railIndex)));
            railIndex++;
        }
        audioManager.PlaySong(2.9f);
        readyGoManager.ShowReadyGo(3f);
        yield return new WaitForSeconds(3f);
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        audioManager.Time = 0f;
#endif

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
        newRail.railIndex = index;
        Vector3 pos = GetRailPosition(index);
        newRail.rail.transform.position = pos;

        if (Random.Range(0f, 1f) > 0.9f) {
            GameObject newDecoration = Instantiate(decorations[Random.Range(0, decorations.Length)]);
            newRail.decoration = newDecoration;
            float x = Random.Range(-5f, 5f);
            x += (x > 0) ? 3f : -3f;
            Transform decT = newDecoration.transform;
            decT.position = new Vector3(x, 0, pos.z);
            decT.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
           
        } else if (Random.Range(0f, 1f) > 0.92f) {
            GameObject newFence = Instantiate(fencePrefab);
            newRail.decoration = newFence;
            float x = Random.Range(0f, 1f) > 0.5f ? 3.2f : -3.2f;
            newFence.transform.position = new Vector3(x, 0, pos.z);
        }
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
                if (railToRemove.beatType == BeatType.Normal) {
                    if (railToRemove.wasHit) {
                        Health -= 5;
                        cameraT.gameObject.GetComponent<ShakeCamera>().RequestCameraShake();
                    }
                } else {
                    if (!railToRemove.wasHit) {
                        Health -= 5;
                        cameraT.gameObject.GetComponent<ShakeCamera>().RequestCameraShake();
                    }
                }

                RemoveRailFromWorld(railToRemove);
                activeRails.RemoveAt(0);

                RailSet newRail = AddRail(railIndex, GetBeatOfCurrentSong(railIndex));
                float centerTime = startTime + GetTimeForIndex(railIndex);
                newRail.time = centerTime;
                activeRails.Add(newRail);

                railIndex++;
                terrainManager.HandleRailIndex(railIndex);

                timeSinceLastRail += SECONDS_PER_BEAT;
            }
            timeSinceLastRail -= Time.deltaTime;

            if (LeftPressed()) {
                HandleHit(true);
            } else if (RightPressed()) {
                HandleHit(false);
            }
        }
    }

    private void RemoveRailFromWorld(RailSet railToRemove) {
        if(railToRemove.decoration != null) {
            Destroy(railToRemove.decoration);
            railToRemove.decoration = null;
        }
        GetPoolForBeatType(railToRemove.beatType).DisposeRail(railToRemove.rail);
    }

    private static bool LeftPressed() {
        return Input.GetKeyDown(KeyCode.LeftShift)
            || Input.GetKeyDown(KeyCode.LeftArrow);
    }

    private static bool RightPressed() {
        return Input.GetKeyDown(KeyCode.RightShift)
            || Input.GetKeyDown(KeyCode.RightArrow);
    }

    private void HandleHit(bool left) {
        BeatType beatType = left ? BeatType.LeftBend : BeatType.RightBend;
        RailSet nearestMatchingRail = RailSet.GetNearestMatchingRailSet(activeRails, audioManager.Time, beatType, out float nearestTime, out int nearestIndex);
        HitType hitType = HitTypeForTime(nearestTime);
        HandleHitType(hitType);
        hitTypeUI.ShowHit(hitType, left);
        hammerManager.HitWithHammer(left);

        if (hitType != HitType.Miss) {
            nearestMatchingRail.wasHit = true;
            float oldTime = nearestMatchingRail.time;
            int oldIndex = nearestMatchingRail.railIndex;
            GameObject oldDecoration = nearestMatchingRail.decoration;
            nearestMatchingRail.decoration = null;
            RemoveRailFromWorld(nearestMatchingRail);
            RailSet newRail = AddRail(oldIndex, BeatType.Normal);
            newRail.time = oldTime;
            newRail.decoration = oldDecoration;
            activeRails[nearestIndex] = newRail;
        }
        else {
            RailSet nearestRail = RailSet.GetNearestRailSet(activeRails, Time.time, out nearestTime);
            nearestRail.wasHit = true;
        }
        //---------------------------
        // request hit effect (YUUKI)
        Transform hammer;
        hammer = left ? hammerManager.leftHammer : hammerManager.rightHammer;
        Transform tr = hammer.GetComponent<Hammer>().HitPosition;
        Quaternion rot = left ? Quaternion.Euler(70.0f, -30.0f, 0.0f) : Quaternion.Euler(70.0f, 30.0f, 0.0f);
        tr.rotation = rot;
        VFXManager.Instance.RequestHitEffect(tr, hitType);
        //---------------------------
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
            headT.position = cameraStartPos + new Vector3(0, 0, BEATS_PER_SECOND * (audioManager.Time - 0.1f));
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

    private Coroutine exitRoutine = null;
    private void GoToResultScreen() {
        if (exitRoutine == null) {
            fadeImage.gameObject.SetActive(true);
            Color startColor = fadeImage.color;
            Color endColor = Color.white;
            exitRoutine = this.CreateAnimationRoutine(
                0.666f,
                delegate (float progress) {
                    float easedProgress = Easing.easeInOutSine(0, 1, progress);
                    fadeImage.color = Color.Lerp(startColor, endColor, easedProgress);
                },
                delegate {
                    SceneManager.LoadScene("Result");
                    exitRoutine = null;
                }
            );
        }
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

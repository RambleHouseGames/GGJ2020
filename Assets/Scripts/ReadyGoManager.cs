using System.Collections;
using UnityEngine;

public class ReadyGoManager : MonoBehaviour {
    public RectTransform readyImageT;
    public RectTransform goImageT;

    private Vector2 readyFullSize;
    private Vector2 goFullSize;

    private void Awake() {
        readyFullSize = readyImageT.sizeDelta;
        goFullSize = goImageT.sizeDelta;
        readyImageT.sizeDelta = Vector2.zero;
        goImageT.sizeDelta = Vector2.zero;
    }

    public void ShowReadyGo(float length) {
        float startTime = Time.time;
        StartCoroutine(Step1(length, startTime));
    }

    private IEnumerator Step1(float length, float startTime) {
        yield return new WaitForSeconds(0.5f);
        this.CreateAnimationRoutine(
            0.333f,
            delegate (float progress) {
                readyImageT.sizeDelta = Vector2.Lerp(Vector2.zero, readyFullSize, progress);
            },
            delegate { Step2(length, startTime); }
        );
    }

    private const float SWITCH_ANIM_DURATION = 0.2f;
    private const float SWITCH_HALF_DURATION = SWITCH_ANIM_DURATION/2f;
    private void Step2(float length, float startTime) {
        Vector2 startSize = readyImageT.sizeDelta;
        Vector2 endSize = startSize * 1.09f;
        float elapsedTime = Time.time - startTime;
        float duration = length - (elapsedTime + SWITCH_HALF_DURATION);
        this.CreateAnimationRoutine(
            duration,
            delegate (float progress) {
                readyImageT.sizeDelta = Vector2.Lerp(startSize, endSize, progress);
            },
            delegate { SwitchToGo(); }
        );
    }

    private void SwitchToGo() {
        Vector2 readyStartSize = readyImageT.sizeDelta;
        Vector2 readyEndSize = Vector2.zero;
        Vector2 goStartSize = Vector2.zero;
        Vector2 goEndSize = goFullSize;
        this.CreateAnimationRoutine(
             SWITCH_ANIM_DURATION,
             delegate (float progress) {
                 readyImageT.sizeDelta = Vector2.Lerp(readyStartSize, readyEndSize, progress);
                 goImageT.sizeDelta = Vector2.Lerp(goStartSize, goEndSize, progress);
             },
             delegate { StartCoroutine(WaitAndDelete()); }
         );
    }

    private IEnumerator WaitAndDelete() {
        yield return new WaitForSeconds(1f);
        Vector2 goStartSize = goImageT.sizeDelta;
        Vector2 goEndSize = Vector2.zero;
        this.CreateAnimationRoutine(
             0.333f,
             delegate (float progress) {
                 goImageT.sizeDelta = Vector2.Lerp(goStartSize, goEndSize, progress);
             }
         );
    }
}

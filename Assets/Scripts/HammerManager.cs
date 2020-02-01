using UnityEngine;

public class HammerManager : MonoBehaviour {
    public Transform leftHammer;
    public Transform rightHammer;

    private Coroutine leftHammerRoutine = null;
    private Coroutine rightHammerRoutine = null;
    private const float ANIM_TIME = 0.666f;
    private const float startRotation = -5f;
    private const float endRotation = -45f;
    public void HitWithHammer(bool left) {
        Transform hammer = left ? leftHammer : rightHammer;
        if (left) {
            this.EnsureCoroutineStopped(ref leftHammerRoutine);
        } else {
            this.EnsureCoroutineStopped(ref rightHammerRoutine);
        }
        Coroutine newRoutine = this.CreateAnimationRoutine(
            ANIM_TIME,
            delegate(float progress) {
                float angle = Easing.easeOutSine(startRotation, endRotation, progress);
                hammer.localEulerAngles = new Vector3(angle, 0, 0);
            },
            delegate {
                if (left) {
                    leftHammerRoutine = null;
                } else {
                    rightHammerRoutine = null;
                }
            }
        );
        if (left) {
            leftHammerRoutine = newRoutine;
        } else {
            rightHammerRoutine = newRoutine;
        }
    }
}

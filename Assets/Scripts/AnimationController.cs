using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public void BlinkAnimation() => GetComponent<Animator>().SetBool("Blink", false);
}
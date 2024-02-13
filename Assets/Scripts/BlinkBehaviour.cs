using System.Collections;
using UnityEngine;

public class BlinkBehaviour : MonoBehaviour
{
    [SerializeField] Material blinkMaterial;
    SpriteRenderer spriteRenderer;
    Material originalMaterial;
    Coroutine blinkRoutine;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    public void Blink(float duration)
    {
        if (blinkRoutine != null)StopCoroutine(blinkRoutine);
        blinkRoutine = StartCoroutine(BlinkRoutine(duration));
    }

    IEnumerator BlinkRoutine(float duration)
    {
        spriteRenderer.material = blinkMaterial;
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = originalMaterial;
        blinkRoutine = null;
    }
}
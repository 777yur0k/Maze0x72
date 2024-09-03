using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    [SerializeField] float topBoundY = 10, bottomBoundY = -10, topBoundX = 5, bottomBoundX = -5, boundX = 0.15f, boundY = 0.05f;

    float getDelta(float lookAtPos, float transformPos, float bound)
    {
        float delta = lookAtPos - transformPos;

        if ((delta < bound) && (delta > -1 * bound)) return 0;
    
        if (transformPos < lookAtPos) return delta - bound;
        else return delta + bound;
    }

    void LateUpdate()
    {
        if(lookAt != null) transform.position = new(Mathf.Clamp(transform.position.x + getDelta(lookAt.position.x, transform.position.x, boundX), bottomBoundX, topBoundX), Mathf.Clamp(transform.position.y + getDelta(lookAt.position.y, transform.position.y, boundY), bottomBoundY, topBoundY), -10);
    }
}
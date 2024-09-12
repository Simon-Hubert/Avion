using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using NaughtyAttributes;

public class Avion : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float rollSpeed;
    [SerializeField] Transform cam;
    [SerializeField] AnimationCurve tilt;
    [SerializeField] float maxTilt = 12f;
    [SerializeField] float maxRoll = 30f;
    float targetRoll = 0f;


    private void FixedUpdate()
    {
        float t = Mathf.Clamp(AltitudeManager.instance.CurrentAltitude / 10000, 0f, 1f);
        transform.position += new Vector3(0,0,speed * Time.fixedDeltaTime);
        cam.position = new Vector3(cam.position.x, Mathf.Lerp(0, 100, t), cam.position.z);
        
        cam.localEulerAngles = new Vector3(Mathf.Lerp(maxTilt, 0, tilt.Evaluate(t)), cam.localEulerAngles.y, Mathf.MoveTowardsAngle(cam.localEulerAngles.z, targetRoll, rollSpeed * Time.fixedDeltaTime));
    }

    public void Roll(bool left)
    {
        targetRoll = left ? -maxRoll : maxRoll;  
    }

    [Button]
    public void Stabilize()
    {
        targetRoll = 0;
    }

    [Button]
    public void RollLeft() => Roll(true);
    [Button]
    public void RollRight() => Roll(false);
}

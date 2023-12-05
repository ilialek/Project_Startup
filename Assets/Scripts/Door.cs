using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen = false;
    [SerializeField]
    private bool IsRotatingDoor = false;
    [SerializeField]
    private float speed = 1.0f;
    [Header("Rotation Config")]
    [SerializeField]
    private float rotationAmount = 90f;
    [SerializeField]
    private float forwardDirection = 0f;

    private Vector3 StartRotation;
    private Vector3 forward;

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        forward = transform.right;
    }

    public void Open(Vector3 UserPosition)
    {
        if (!IsOpen) 
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor) 
            {
            float dot = Vector3.Dot(forward, (UserPosition - transform.position).normalized);
                //AnimationCoroutine = StartCoroutine(DoRotationDoor());
            }
        }
    }

    /*private IEnumerator DoRotationDoor(float ForwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (ForwardAmount & gt;= ForwardDirection)
{
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + RotationAmount, 0));
        }
else
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - RotationAmount, 0));
        }

    }*/
}

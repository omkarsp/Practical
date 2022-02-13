using System.Collections;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [HideInInspector] public bool isSelected = false;
    [HideInInspector] public Vector3 initialPosition;
    [HideInInspector] public Vector3 initialRotation;
    private Quaternion rot;
    private bool isMoving = false;
    private bool isRotating = false;
    private float moveSpeed = 10;
    private float rotateSpeed = 2;

    public void Initialize(float _speed)
    {
        initialPosition = transform.position;
        Debug.Log(initialPosition);
        initialRotation = transform.rotation.eulerAngles;
        moveSpeed = _speed;
        rot = transform.rotation;
    }

    public void ResetObject()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(ResetPositionRoutine());
        }
        if (!isRotating)
        {
            isRotating = true;
            StartCoroutine(ResetRotationRoutine());
        }
    }

    private IEnumerator ResetPositionRoutine()
    {
        while (Vector3.Distance(transform.position, initialPosition) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, Time.deltaTime * moveSpeed);
            yield return null;
        }

        transform.position = initialPosition;
        isMoving = false;
    }

    private IEnumerator ResetRotationRoutine()
    {
        while (Quaternion.Angle(transform.rotation, rot) > 1)
        {
            //if (Quaternion.Angle(transform.rotation, rot) > 1) isRotating = false;
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotateSpeed);
            yield return null;
        }
        transform.rotation = rot;
        isRotating = false;
    }

    public void UpdateMaterial(Material _mat)
    {
        GetComponent<Renderer>().material = _mat;
    }
}

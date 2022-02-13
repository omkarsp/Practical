using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float translateSpeed = 1;
    [SerializeField] private float rotationSpeed = 1;
    private Transform[] selectables = null;
    private HUD hud = null;
    Action<Transform> MovementAction;

    public void Initialize(Transform[] _selectables, HUD _hud)
    {
        selectables = _selectables;
        hud = _hud;
        MovementAction += hud.UpdateHud;
    }

    private void Update()
    {
        foreach (Transform obj in selectables)
        {
            Move(obj);
        }
    }

    public void Move(Transform obj)
    {
        if (obj.GetComponent<Selectable>().isSelected)
        {
            //translate
            float h = Input.GetAxis("Horizontal") * translateSpeed * Time.deltaTime;
            float v = Input.GetAxis("Vertical") * translateSpeed * Time.deltaTime;

            obj.position = new Vector2(obj.position.x + h,
                obj.position.y + v);

            //rotate
            float _speed = rotationSpeed * Time.deltaTime;

            if (Input.GetKey(KeyCode.I))
            {
                obj.Rotate(new Vector3(_speed, 0, 0));
            }

            if (Input.GetKey(KeyCode.O))
            {
                obj.Rotate(new Vector3(0, _speed, 0));
            }

            if (Input.GetKey(KeyCode.P))
            {
                obj.Rotate(new Vector3(0, 0, _speed));
            }

            MovementAction(obj.transform);
        }
    }

    private void OnDestroy()
    {
        MovementAction -= hud.UpdateHud;
    }
}

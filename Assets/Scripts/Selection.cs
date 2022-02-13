using System;
using UnityEngine;

public class Selection : MonoBehaviour
{
    [SerializeField] private Material metalMat;
    [SerializeField] private Material plasticMat;
    private Transform[] selectables = null;
    private Transform selectedObject = null;
    private HUD hud = null;
    Action<Transform> SelectedAction;

    public void Initialize(Transform[] _selectables, HUD _hud)
    {
        selectables = _selectables;
        hud = _hud;
        SelectedAction += hud.UpdateHud;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo) && hitInfo.transform.tag == "Selectable")
            {
                selectedObject = hitInfo.collider.transform;
                OnSelected();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedObject.GetComponent<Selectable>().UpdateMaterial(plasticMat);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedObject.GetComponent<Selectable>().UpdateMaterial(metalMat);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            foreach (Transform _transform in selectables)
            {
                _transform.GetComponent<Selectable>().ResetObject();
            }
        }
    }

    private void OnSelected()
    {
        //log
        Debug.Log(selectedObject.name.ToString() + " selected!");

        SelectedAction(selectedObject);

        foreach (Transform obj in selectables)
        {
            obj.GetComponent<Selectable>().isSelected = false;
            //obj.GetComponent<Renderer>().material.color = Color.white;
        }

        selectedObject.GetComponent<Selectable>().isSelected = true;
        //selectedObject.GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnDestroy()
    {
        SelectedAction -= hud.UpdateHud;
    }
}

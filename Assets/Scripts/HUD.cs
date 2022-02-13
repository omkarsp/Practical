using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text positionText;
    [SerializeField] private Text rotationText;

    public void UpdateHud(Transform selectedObject)
    {
        nameText.text = "Object: " + selectedObject.name;
        positionText.text = "Position: " + selectedObject.position.ToString();
        rotationText.text = "Rotation: " + selectedObject.eulerAngles.ToString();
    }
}

using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    Movement movement;
    Selection selection;

    [SerializeField] private Transform[] selectables;
    [SerializeField] private HUD hud;
    [SerializeField] private float resetMoveSpeed;

    private void Awake()
    {
        GetReferences();
        Initialize();

        foreach (Transform objTransform in selectables)
        {
            objTransform.GetComponent<Selectable>().Initialize(resetMoveSpeed);
        }
    }

    private void GetReferences()
    {
        movement = GetComponent<Movement>();
        selection = GetComponent<Selection>();
    }

    private void Initialize()
    {
        movement.Initialize(selectables, hud);
        selection.Initialize(selectables, hud);
    }
}

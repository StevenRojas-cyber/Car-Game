using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Refrigerante : MotorPart, IPointerDownHandler, IPointerUpHandler
{
    [Header("Refrigerante Properties")]
    [SerializeField] private float FillSpeed = 1f;
    [SerializeField] private float MaxRefrigeranteLevel = 10f;
    [SerializeField] private float CurrentRefrigeranteLevel = 10f;

    private bool isFilling = false;

    void Start()
    {
        PartName = "Refrigerante";
        currentState = PartsStates.Repaired;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"You clicked on {PartName}");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"You released the click on {PartName}");
    }

    void Update()
    {
        
    }
}

using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class FuelPipe : MotorPart, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Fuel Pipe Settings")]
    [SerializeField] private float MaxFuelKeyLaps = 5;

    private float PreviousAngle = 0f;
    private float AngleAcumulated;


    private Camera mainCamera;

    void Start()
    {
        PartName = "Fuel Pipe";
        currentState = PartsStates.Repaired;

        mainCamera = Camera.main;
    }

    
    void Update()
    {
        float MaxLapsInRadians = MaxFuelKeyLaps * Mathf.Deg2Rad;
        //Debug.Log($"Max Laps in Radians: {MaxLapsInRadians}");
    }
    
    
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 posicionRaton = mainCamera.ScreenToWorldPoint(eventData.position);
        posicionRaton.z = 0;
        Vector3 direccion = posicionRaton - transform.position;

        PreviousAngle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 posicionRaton = mainCamera.ScreenToWorldPoint(eventData.position);
        posicionRaton.z = 0;

        Vector3 direccion = posicionRaton - transform.position;

        float anguloActual = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        
        float diferencia = Mathf.DeltaAngle(PreviousAngle, anguloActual);

        
        AngleAcumulated += diferencia;

        
        PreviousAngle = anguloActual;


        if(AngleAcumulated < PreviousAngle && AngleAcumulated < 0)
        { 
            transform.rotation = Quaternion.Euler(0, 0, AngleAcumulated);
            Debug.Log("Current Angle (Degrees): " + AngleAcumulated);
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }


}

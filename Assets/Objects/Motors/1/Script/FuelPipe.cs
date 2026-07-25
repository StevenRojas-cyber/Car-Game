using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class FuelPipe : MotorPart, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Fuel Pipe Settings")]
    [SerializeField] private int MaxFuelKeyLaps = 5;
    [SerializeField] private float ReverseLapsMultiplier = 1f;
    [SerializeField] private float UnWindSpeed = 180f;
    [SerializeField] private SpriteRenderer Sprite;


    private bool AreLapsCompleted = false;
    private int MaxLapsInDegrees;
    private int currentNumberOfLaps;
    private float PreviousAngle = 0f;
    private float AngleAcumulated;



    private Camera mainCamera;

    void Start()
    {
        PartName = "Fuel Pipe";
        currentState = PartsStates.Repaired;


        MaxLapsInDegrees = MaxFuelKeyLaps * 360;
        mainCamera = Camera.main;
        transform.rotation = Quaternion.Euler(0,0,MaxLapsInDegrees);
    }

    
    void Update()
    {
        DiscountLaps();

        if(AreLapsCompleted)
        {
            currentState = PartsStates.Repaired;
            Sprite.color = Color.green;
        }
        else
        {
            currentState = PartsStates.Damaged;
            AreLapsCompleted = false;
            Sprite.color = Color.red;
        }
    }
    
    
    void DiscountLaps()
    {
        if (AngleAcumulated < 0)
        {
            // Sumamos grados para acercarlo de vuelta a 0 (desenroscar)
            AngleAcumulated += UnWindSpeed * ReverseLapsMultiplier * Time.deltaTime;

            // Limitamos para que no se pase de 0 (posición de reposo)
            if (AngleAcumulated > 0)
            {
                AngleAcumulated = 0f;
            }

            // Aplicamos la rotación visual
            transform.rotation = Quaternion.Euler(0, 0, AngleAcumulated);

            // Recalculamos las vueltas actuales usando el valor absoluto
            currentNumberOfLaps = (int)(Mathf.Abs(AngleAcumulated) / 360f);

            // Si las vueltas caen por debajo del máximo, ya no están completas
            if (currentNumberOfLaps < MaxFuelKeyLaps)
            {
                AreLapsCompleted = false;
            }
        }
    }


    //Logica de rotaion con el mouse
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (AreLapsCompleted) return;

        Vector3 posicionRaton = mainCamera.ScreenToWorldPoint(eventData.position);
        posicionRaton.z = 0;
        Vector3 direccion = posicionRaton - transform.position;

        PreviousAngle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (AreLapsCompleted) return;

        Vector3 posicionRaton = mainCamera.ScreenToWorldPoint(eventData.position);
        posicionRaton.z = 0;
        Vector3 direccion = posicionRaton - transform.position;

        float anguloActual = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        
        float diferencia = Mathf.DeltaAngle(PreviousAngle, anguloActual);
        

        if(diferencia < 0)
        {
            AngleAcumulated += diferencia;
            PreviousAngle = anguloActual;
        }

        
        transform.rotation = Quaternion.Euler(0, 0, AngleAcumulated);
      

        currentNumberOfLaps = ( (int)(AngleAcumulated / 360f) ) * -1;


        if( currentNumberOfLaps >= MaxFuelKeyLaps )
        {
            AreLapsCompleted = true;
            Debug.Log("Listas Las vueltas!!");
        }
        else
        {
            Debug.Log("Laps: " +  currentNumberOfLaps);
        }


    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }


}

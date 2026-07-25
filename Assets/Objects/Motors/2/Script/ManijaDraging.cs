using UnityEngine;
using UnityEngine.EventSystems;

public class ManijaDraging : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("RopeMotor Body")]
    [SerializeField] private GameObject Body;



    private Vector3 initialPosition;
    private Vector3 currentPosition;
    private Vector3 lastPosition;
    
    private bool CanDrag = true;
    private PartsStates MotorState;
    private Camera currentCamera;
    private RopeMotor ropeMotor;


    


    void Start()
    {
        initialPosition = transform.position;
        currentCamera = Camera.main;

        if (Body == null) return;

        ropeMotor = Body.GetComponent<RopeMotor>();
    }



    
    void Update()
    {
        MotorState = ropeMotor.GetCurrentState();
        
        switch(MotorState) 
        {
            case PartsStates.Damaged:
                CanDrag = true;
                break;

            default:
                CanDrag = false;
                break;
        }
    }





    float CalculateVelocity(Vector3 lastPosition, float deltaTime)
    {
        return Vector3.Distance(initialPosition, lastPosition) / deltaTime;
    }

    void ResetPositions()
    {
        transform.position = initialPosition;
    }

    bool IsTheIdealVelocity(float velocity)
    {
        return velocity >= ropeMotor.GetDragIdealSpeed();
    }





    //Logica de arrastre de la manija, para que al arrastrarla se mueva el motor de cuerda
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clickeado: " + name);
    }


    //Arrastre
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!CanDrag) return;
    
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");

        if(!CanDrag) return;

        currentPosition = currentCamera.ScreenToWorldPoint(eventData.position);
        currentPosition.z = initialPosition.z; // Mantener la misma profundidad en el eje Z

        transform.position = currentPosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!CanDrag) return;

        Debug.Log("OnEndDrag");



        lastPosition = currentPosition;

        int currentVelocity = (int)CalculateVelocity(lastPosition, Time.deltaTime);


        if(IsTheIdealVelocity(currentVelocity))
        {
            Debug.Log("Ideal Velocity Reached: " + currentVelocity);
            ropeMotor.PartRepaired();
            ropeMotor.SetRunningTime();
        }
        else
        {
            Debug.Log("Not Ideal Velocity: " + currentVelocity);
            ropeMotor.PartDamaged();
        }

        ResetPositions();
    }

    
}

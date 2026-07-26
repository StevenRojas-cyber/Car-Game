using UnityEngine;
using UnityEngine.EventSystems;

public class RopeMotor : MotorPart
{
    [Header("Rope Motor Properties")]
    [SerializeField] private float DragIdealSpeed = 1f;
    [SerializeField] private GameObject Manija;
    [SerializeField] private SpriteRenderer Sprite;
    
    private float RunningTime = 0f;


    void Start()
    {
        PartName = "Rope Motor";
        RunningTime = 30f;
        currentState = PartsStates.Good;
        
    }

    void Update()
    {
        switch (currentState)
        {
            case PartsStates.Damaged:
                Sprite.color = Color.red;
                break;

            case PartsStates.Danger:
                Sprite.color = Color.yellow;
                break;

            case PartsStates.Good:
                Sprite.color = Color.green;
                break;
        }

        RuninTimeRemaining();
    }



    public override void PartDamaged()
    {
        currentState = PartsStates.Damaged;

        RunningTime = 0f;

        Debug.Log("Motor Cuerda Sufrio Daños");
    }

    public override void PartRepaired()
    {
        currentState = PartsStates.Good;
    }



    //Devolver el valor de la velocidad ideal de arrastre del motor de cuerda
    public float GetDragIdealSpeed()
    {
        return DragIdealSpeed;
    }

    //Devolver el estado actual del motor de cuerda
    public PartsStates GetCurrentState()
    {
        return currentState;
    }

    public float SetRunningTime()
    {
        RunningTime = Random.Range(10f, 30f);
        return RunningTime;
    }

    public void RuninTimeRemaining()
    {
        RunningTime -= Time.deltaTime;

        if (RunningTime <= 0f)
        {
            currentState = PartsStates.Damaged;
            return;
        }

        if(RunningTime > 0f && RunningTime <= 15)
        {
            currentState = PartsStates.Danger;
        }
        else
        {
            currentState = PartsStates.Good;
        }
        
    }
}

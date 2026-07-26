using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hamster : MotorPart, IPointerClickHandler
{
    [Header("Hamster Properties")]
    [SerializeField] private float SleepTimeRemainingMultiplier = 1f;
    [SerializeField] private float HamsterSleepTime = 5f;
    [SerializeField] private Animator HamAnimator;
    [SerializeField] private SpriteRenderer HamsterSprite;
    [SerializeField] private TMP_Text SleepTimeText;

    bool IsHamsterSleeped;
    
    void Start()
    {
        PartName = "Hamster";
        IsHamsterSleeped = false;
        HamsterSleepTime = 100f;
    }

    
    void Update()
    {

        if (IsHamsterSleeped) 
        { 
            HamsterSprite.color = Color.red;
            currentState = PartsStates.Damaged;

            HamAnimator.SetBool("isRunning", false);
        }

        if (!IsHamsterSleeped && HamsterSleepTime > 0f && HamsterSleepTime <= 50f)
        {
            SleppTimeRemaining();
            currentState = PartsStates.Danger;
            HamsterSprite.color = Color.yellow;

            HamAnimator.SetBool("isRunning", true);
        }

        if (!IsHamsterSleeped && HamsterSleepTime >= 50f)
        {
            SleppTimeRemaining();
            HamsterSprite.color = Color.green;
            currentState = PartsStates.Good;
            HamAnimator.SetBool("isRunning", true);
        }
        
        

    }


    public override void PartDamaged()
    {
        currentState = PartsStates.Damaged;

        IsHamsterSleeped = true;
        HamsterSleepTime = 0f;
    }


    void SleppTimeRemaining()
    {
        if (IsHamsterSleeped) return;

        HamsterSleepTime -= SleepTimeRemainingMultiplier * Time.deltaTime;

        SleepTimeText.text = (int)(HamsterSleepTime) + " 's";

        if (HamsterSleepTime <= 0f)
        {
            IsHamsterSleeped = true;
        }

    }


    float SetTimeToSleep()
    {
        float sleepTime = Random.Range(50f, 100f);
        
        return sleepTime;
    }


    

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Hamster clicked");

        if(!IsHamsterSleeped) return;

        IsHamsterSleeped = false;

        HamsterSleepTime = SetTimeToSleep();
    }
}

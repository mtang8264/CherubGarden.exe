using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chao : MonoBehaviour
{
    private SpriteRenderer sp;
    private ParticleSystem parts;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public bool isInspected;

    public bool canMove;
    public Vector3 moveTarget;
    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;
    public Color baseColor;

    [Header("Finals")]
    public float moveSpeed;
    public float maxX, minX, maxY, minY;
    public float MaxForValues;
    public float MinForValues;


    [Header("Attributes")]
    public string myName;
    public float HungerValue;
    public float HappinessValue;
    public float LifeValue;
    public float TiredValue;

    public int Level; // Start at 1 --> 2,3 --> after 3 is full, can transform!  
    public int Element;

    [Header("Conditions")]
    public bool isSleeping;
    public float SleepRate;
    public bool isEating;
    public bool isFlower;
    public bool isWatering;
    public bool isEviling;
    public bool canGive;
    public bool isQuestion;
    public bool isNo;
    public bool isHappy;
    public bool canBePet;
    public float petTimer;
    public bool canTransform;
    private float timer;
    public float StatDecreaseTime;


    void Start()
    {
        sp = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        parts = this.GetComponent<ParticleSystem>();

        timer = StatDecreaseTime;
        canBePet = true;
        canGive = true;
        isSleeping = false;

        moveTarget = this.transform.position;
        parts.Stop();
    }

    void Update()
    {
        if (Element == 0)
        {
            this.transform.localScale = new Vector3(3.5f, 3.5f, 1f);

        }
        else
        {
            this.transform.localScale = new Vector3(5f, 5f, 1f);
        }

        ManageAttributes();
        ManageAnimations();
        ManageStatDecrease();

        ManageMovement();
    }

    void ManageStatDecrease()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // CON
            HungerValue -= 0.1f;
            HappinessValue -= 0.15f;
            LifeValue -= 0.1f;


            timer = StatDecreaseTime;
        }

    }

    void ManageAttributes()
    {
        if (HungerValue > MaxForValues)
        {
            HungerValue = MaxForValues;
        }
        else if (HungerValue < MinForValues)
        {
            HungerValue = MinForValues;
        }

        if (HappinessValue > MaxForValues)
        {
            HappinessValue = MaxForValues;
        }
        else if (HappinessValue < MinForValues)
        {
            HappinessValue = MinForValues;
        }

        if (TiredValue > MaxForValues)
        {
            TiredValue = MaxForValues;
            isSleeping = true;
        }
        else if (TiredValue < MinForValues)
        {
            TiredValue = MinForValues;
            isSleeping = false;
        }

        if (LifeValue > MaxForValues)
        {
            LifeValue = MaxForValues;
        }
        else if (LifeValue < MinForValues)
        {
            LifeValue = MinForValues;
        }

        //~~~~~~~~~~~~

        if (isSleeping) // If not sleeping, Move Around...
        {
            TiredValue -= SleepRate * Time.deltaTime;
            //sp.color = Color.black;
        }
        else // Go to sleep...
        {
            TiredValue += (SleepRate / 2) * Time.deltaTime;
            //sp.color = Color.blue;
        }

        // ~~~~~~~~~~~~~ 
        // LEVEL UP VALUES

        if (HungerValue >= MaxForValues)
        {
            if (Element == 0)
            {
                Element = 1;
                StartCoroutine(ParticleBurst());
                HungerValue = 4;
            }

        }
        if (HappinessValue >= MaxForValues)
        {
            if (Element == 0)
            {
                Element = 2;
                StartCoroutine(ParticleBurst());
                HappinessValue = 4;
            }
            
        }
        if (LifeValue >= MaxForValues)
        {
            if (Element == 0)
            {
                Element = 3;
                StartCoroutine(ParticleBurst());
                LifeValue = 1;
            }

        }
        if (HungerValue <= MinForValues)
        {
            Element = 4;
            StartCoroutine(ParticleBurst());
            HungerValue = 2;
            HappinessValue = 2;
            LifeValue = 0;
        }

        if (Element == 1 && HungerValue >= MaxForValues / 2)
        {
            parts.Play();
            canTransform = true;
        }
        else if (Element == 2 && HappinessValue >= MaxForValues / 2)
        {
            parts.Play();
            canTransform = true;
        }
        else if (Element == 3 && LifeValue >= MaxForValues / 2)
        {
            parts.Play();
            canTransform = true;
        }
        else if (Element == 4 && HungerValue >= MaxForValues / 2 && HappinessValue >= MaxForValues / 2)
        {
            parts.Play();
            canTransform = true;
        }
    }


    private void ManageMovement()
    {
        float distToTarget = Vector2.Distance(this.transform.position, moveTarget);

        if (canMove && distToTarget > 0.1f && !isInspected && !isSleeping)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveTarget, moveSpeed * Time.deltaTime);
            //Debug.Log(distToTarget);

        }
        else // arrived at target
        {
            if (canMove)
            {
                StartCoroutine(WaitToMove(Random.Range(4f, 10f)));
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    private IEnumerator WaitToMove(float sec)
    {
        canMove = false;
        yield return new WaitForSeconds(sec);
        canMove = true;

        float newX = Random.Range(minX, maxX);
        float newY = Random.Range(minY, maxY);
        moveTarget = new Vector2(newX, newY);
    }

    private void ManageAnimations()
    {

        switch (Element) // Manage current color
        {
            case 1:
                sp.color = color1;
                break;
            case 2:
                sp.color = color2;
                break;
            case 3:
                sp.color = color3;
                break;
            case 4:
                sp.color = color4;
                break;
            default:
                sp.color = baseColor;
                break;
        }


        if (canMove)
        {
            anim.SetInteger("AnimState", 1);
        }
        else
        {
            anim.SetInteger("AnimState", 0);
        }



        if (petTimer < 100)
        {
            petTimer += Time.deltaTime * 20;
            canBePet = false;
        }
        else
        {
            petTimer = 100;
            canBePet = true;
        }
    }

    public void GetPet()
    {
        if (canBePet)
        {
            HappinessValue+= 0.5f;
            petTimer = 0;
            StartCoroutine(Happy());
        }
    }

    IEnumerator ParticleBurst()
    {
        parts.Play();
        yield return new WaitForSeconds(2f);
        parts.Stop();
    }


    public void GetFood()
    {
        StartCoroutine(GetFoodCo());
    }

    IEnumerator GetFoodCo()
    {
        canGive = false;
        isEating = true;
        yield return new WaitForSeconds(1.5f);
        isEating = false;
        canGive = true;

        if (Element == 1)
        {
            HungerValue += 1.5f;
            // HAPPY
            StartCoroutine(Happy());
        }
        else if (Element == 4)
        {
            HappinessValue-=0.25f;
            HungerValue++;
            // NO
            StartCoroutine(No());
        }
        else
        {
            HungerValue++;
        }
    }
    public void GetFlower()
    {
        StartCoroutine(GetFlowerCo());
    }

    IEnumerator GetFlowerCo()
    {
        canGive = false;
        isFlower = true;
        yield return new WaitForSeconds(2.15f);
        isFlower = false;
        canGive = true;

        if (Element == 2)
        {
            HappinessValue += 1.5f;
            // HAPPY
            StartCoroutine(Happy());
        }
        else if (Element == 4)
        {
            HappinessValue -= 1.5f;
            // NO
            StartCoroutine(No());
        }
        else
        {
            HappinessValue++;
        }
    }
    public void GetWater()
    {
        StartCoroutine(GetWaterCo());
    }
    IEnumerator GetWaterCo()
    {
        canGive = false;
        isWatering = true;
        yield return new WaitForSeconds(2.3f);
        isWatering = false;
        canGive = true;

        if (Element == 3)
        {
            LifeValue += 2f;
            // HAPPY
            StartCoroutine(Happy());
        }
        else if (Element == 0)
        {
            LifeValue++;
        }
        else
        {
            // QUESTION
            LifeValue += 0.5f;
            StartCoroutine(Question());
        }
    }
    public void GetEvil()
    {
        StartCoroutine(GetEvilCo());
    }
    IEnumerator GetEvilCo()
    {
        canGive = false;
        isEviling = true;
        yield return new WaitForSeconds(1.5f);
        isEviling = false;
        canGive = true;

        if (Element == 4)
        {
            HappinessValue += 1.5f;
            HungerValue += 1.5f;
            // HAPPY
            StartCoroutine(Happy());
        }
        else
        {
            HappinessValue-=1.5f;
            HungerValue--;
            // NO
            StartCoroutine(No());
        }
    }

    IEnumerator Question()
    {
        isQuestion = true;
        yield return new WaitForSeconds(2f);
        isQuestion = false;
    }
    IEnumerator No()
    {
        isNo = true;
        yield return new WaitForSeconds(2f);
        isNo = false;
    }
    IEnumerator Happy()
    {
        isHappy = true;
        yield return new WaitForSeconds(2f);
        isHappy = false;
    }
}

using UnityEngine;

public class YeetController : MonoBehaviour
{
    public static YeetController instance = null;

    public float yeetDuration = 0f;
    public float yeetTimeScale = 0f;

    public GameObject yeetedPassenger = null;

    public bool isYeetActivated = false;

    public int initialNbPassenger;
    public int nbPassenger;

    public Passenger[] array;

    public Vector2 defaultYeetSpeed;

    public Passenger.PassengerType yeetedPassengerType;

    void Start()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;

        nbPassenger = initialNbPassenger;
    }


    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

   

    void Update()
    {
        if (Input.GetButtonDown("Yeet"))
        {
            isYeetActivated = true;
        }

        Debug.Log(Time.timeScale);

        if (isYeetActivated)
        {
            Yeet();
        }
    }


    private void Yeet()
    {
        Time.timeScale = 0.05f;
        Cursor.visible = true;
        yeetTimeScale += 1 / 60f;

 

        if (yeetTimeScale >= yeetDuration || yeetedPassenger)
        {
            if (!yeetedPassenger)
            {
                //int i = Random.Range(0, array.Length);
                array[initialNbPassenger - nbPassenger].GetComponent<Rigidbody2D>().velocity = defaultYeetSpeed;
                array[initialNbPassenger - nbPassenger].GetComponent<Rigidbody2D>().velocity = defaultYeetSpeed;
                yeetedPassengerType = array[initialNbPassenger - nbPassenger].GetComponent<Passenger>().type;
                nbPassenger--;
            }
            isYeetActivated = false;
            yeetTimeScale = 0f;
            Time.timeScale = 1f;
            Cursor.visible = false;
            yeetedPassenger = null;
        }
    }


    public void OnYeeted(Passenger.PassengerType _passengerType)
    {
        yeetedPassengerType = _passengerType;
    }

}

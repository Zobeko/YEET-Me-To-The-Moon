using UnityEngine;
using UnityEngine.UI;

public class YeetController : MonoBehaviour
{
    public static YeetController instance = null;

    public float yeetDuration = 0f;
    public float yeetTimeScale = 0f;

    public GameObject yeetedPassenger = null;

    public bool isYeetActivated;
    

    public int initialNbPassenger;
    public int nbPassenger;

    public Passenger[] array;

    public Vector2 defaultYeetSpeed;

    public Passenger.PassengerType yeetedPassengerType;

    private AudioSource audioSource = null;
    [SerializeField] private AudioClip[] passengerDeathClipArray = null;
    [SerializeField] private AudioClip yeetActivationClip = null;


    public CanvasGroup myCG;
    private bool flash = false;

    void Start()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;

        nbPassenger = initialNbPassenger;
        audioSource = this.GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(yeetActivationClip);
        }


        if (isYeetActivated)
        {
            Yeet();
        }

        if (flash) //Pour le flash
        {

            myCG.alpha = myCG.alpha - Time.deltaTime;
            if (myCG.alpha <= 0)
            {
                myCG.alpha = 0;
                flash = false;
            }
        }
    }


    private void Yeet()
    {
        

        Time.timeScale = 0.05f;
        Cursor.visible = true;
        yeetTimeScale += Time.deltaTime * 20 ;


 

        if (yeetTimeScale > yeetDuration + 0.1f || yeetedPassenger)
        {
            flash = true;
            myCG.alpha = 1;

            if (!yeetedPassenger)
            {
                array[initialNbPassenger - nbPassenger].GetComponent<Rigidbody2D>().velocity = defaultYeetSpeed;
                //array[initialNbPassenger - nbPassenger].GetComponent<Rigidbody2D>().velocity = defaultYeetSpeed;
                yeetedPassengerType = array[initialNbPassenger - nbPassenger].GetComponent<Passenger>().type;
                nbPassenger--;
                audioSource.PlayOneShot(passengerDeathClipArray[Random.Range(0, passengerDeathClipArray.Length)]);


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

using Unity.VisualScripting;
using UnityEngine;

public class TongueController : MonoBehaviour
{
    [SerializeField] SpriteRenderer tongueRenderer;
    [SerializeField] float tongueSpeed = 3f;
    [SerializeField] float minTongueSpeed = 3f;
    [SerializeField] float maxTongueSpeed = 20f;
    [SerializeField] float maxTongueLength = 3f;
    [SerializeField] float extendedDelay = 0.2f;
    [SerializeField] float pressTimer = 0f;
    [SerializeField] float maxChargeTime = 5f;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool canMove = true;

    private bool hasExtended = false;

    private bool startTimer = false;

    void Start()
    {
        TongueManager.Instance.SetTongue(gameObject);
        Debug.Log(TongueManager.Instance.GetTongue().transform.position);
    }

    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            StartCoroutine(ExtendAndRetractTongue());
        }

        */
              
        // Check if the left mouse button is pressed
        TongueManager.Instance.SetSlider((tongueSpeed - minTongueSpeed) / (maxTongueSpeed - minTongueSpeed));


        if(startTimer){
            pressTimer += Time.deltaTime;
            Debug.Log(pressTimer);
        }

        if(pressTimer < maxChargeTime){
            tongueSpeed = pressTimer * (maxTongueSpeed - minTongueSpeed) + minTongueSpeed;
        }
        else{
            tongueSpeed = maxTongueSpeed;
        }

        if (Input.GetMouseButtonDown(0) && canMove && !startTimer)
        {
            startTimer = true;
        }
        else if(Input.GetMouseButtonUp(0) && canMove){
            Debug.Log(tongueSpeed);
            
            StartCoroutine(ExtendAndRetractTongue(tongueSpeed));
            pressTimer = 0f;
            startTimer = false;           
        }
    }

    // Coroutine for extending and retracting the tongue
    public System.Collections.IEnumerator ExtendAndRetractTongue(float tongueSpeedTriggered)
    {
        Debug.Log("ExtendAndRetract started with " + tongueSpeedTriggered + " tongue speed");
        
        canMove = false;

        Vector3 direction = transform.up; // Assuming the frog's head is facing upwards
        
        startPosition = transform.position;
        targetPosition = startPosition + direction * maxTongueLength;

        // Extend the tongue
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // Move the tongue forward
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, tongueSpeedTriggered * Time.deltaTime);
            yield return null;
        }

        hasExtended = true;

        yield return new WaitForSeconds(extendedDelay);

        while (Vector3.Distance(transform.position, startPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, tongueSpeedTriggered * Time.deltaTime);
            yield return null;
        }

        hasExtended = false;
        canMove = true;
    }

    public bool GetCanMove(){
        return canMove;
    }

    public bool GetHasExtended(){
        return hasExtended;
    }

    public Vector3 GetTonguePosition(){
        return transform.position;
    }
}

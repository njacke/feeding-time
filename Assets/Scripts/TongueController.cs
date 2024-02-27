using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TongueController : MonoBehaviour
{
    [SerializeField] SpriteRenderer tongueRenderer;
    [SerializeField] float tongueSpeed = 15f;
    [SerializeField] float tongueLength = 1.2f;
    [SerializeField] float minTongueLength = 1.2f;
    [SerializeField] float maxTongueLength = 3.2f;
    [SerializeField] float extendedDelay = 0.2f;
    [SerializeField] float pressTimer = 0f;
    [SerializeField] float maxChargeTime = 1f;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool canMove = true;
    private bool hasExtended = false;
    private bool freezeTongue = false;
    private bool startTimer = false;

    void Start()
    {
        TongueManager.Instance.SetTongue(gameObject);
        Debug.Log(TongueManager.Instance.GetTongue().transform.position);
    }

    void Update()
    {

        if (!GameManager.Instance.GetGameActive()){
            freezeTongue = true;
            canMove = false;
            //Debug.Log("Freeze tongue set to " + freezeTongue);            
        }

        else{
            TongueManager.Instance.SetSlider((tongueLength - minTongueLength) / (maxTongueLength - minTongueLength));

            if(startTimer){
                pressTimer += Time.deltaTime;
                //Debug.Log(pressTimer);
            }

            if(pressTimer < maxChargeTime){
                tongueLength = pressTimer / maxChargeTime * (maxTongueLength - minTongueLength) + minTongueLength;
            }            
            else{
                tongueLength = maxTongueLength;
            }

            if (Input.GetMouseButtonDown(0) && canMove && !startTimer)
            {
                startTimer = true;
            }
            else if(Input.GetMouseButtonUp(0) && canMove){
                Debug.Log(tongueLength);
                
                StartCoroutine(ExtendAndRetractTongue(tongueLength));
                pressTimer = 0f;
                startTimer = false;           
            }
        }
    }

    //coroutine extend + retract tongue
    public System.Collections.IEnumerator ExtendAndRetractTongue(float tongueLengthTriggered)
    {
        Debug.Log("ExtendAndRetract started with " + tongueLengthTriggered + " tongue length");
        
        canMove = false;

        Vector3 direction = transform.up;
        
        startPosition = transform.position;
        targetPosition = startPosition + direction * tongueLengthTriggered;

        //extend tongue
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f && !freezeTongue)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, tongueSpeed * Time.deltaTime);
            yield return null;
        }


        yield return new WaitForSeconds(extendedDelay);

        hasExtended = true;
        
        //retract tongue
        while (Vector3.Distance(transform.position, startPosition) > 0.01f && !freezeTongue)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, tongueSpeed * Time.deltaTime);
            yield return null;
        }

        hasExtended = false;

        if (!freezeTongue){
            canMove = true;
        }
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

    public float GetTongueSpeed(){
        return tongueSpeed;
    }
}

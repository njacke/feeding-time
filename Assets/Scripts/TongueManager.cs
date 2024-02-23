using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TongueManager : MonoBehaviour
{
    public static TongueManager Instance { get; private set; }

    [SerializeField] Slider slider;

    private GameObject tongue;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetSlider(float sliderValue){
        slider.value = sliderValue;
        //Debug.Log(slider.value);
    }
    
    public void SetTongue(GameObject tongueObject)
    {
        tongue = tongueObject;
    }

    public GameObject GetTongue()
    {
        return tongue;
    }
}

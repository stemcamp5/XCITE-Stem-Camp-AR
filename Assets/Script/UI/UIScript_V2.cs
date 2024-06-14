using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript_V2 : MonoBehaviour
{

    [Header("Particle Generation")]
    public GameObject particleGen;
    public GameObject spawn;
    public TextMeshProUGUI conc_str;
    public Slider conc_slider;
    private static int conc_num = 1;

    [Header("Lid")]
    public GameObject pressureManager;
    public GameObject lid;
    private static bool upLidActive;
    private static bool downLidActive;

    [Header("Temperature")]
    public Slider temp_slider;
    private static bool temp_point_up;


    [Header("Selection")]
    public int selection = 0;

    private ParticleGeneration PG;



    private void Start()
    {
        PG = particleGen.GetComponent<ParticleGeneration>();
        Temperature_Change(0.15f);
    }

    private void FixedUpdate()
    {
        QuantityNum();

        if (temp_point_up)
        {
            Temperature_Change(temp_slider.value);
        }

        if (upLidActive)
        {
            pressureManager.GetComponent<Pressure_Manager>().Lid_Up();
        }
        else if (downLidActive)
        {
            pressureManager.GetComponent<Pressure_Manager>().Lid_Down();
            
        }
    }

    public void CreateButton()
    {
        //create NO2 object with specified quantity at random location. IGNORE THIRD PARAMETER HERE, 4th indicates if particle is splitting.
        PG.InstantiateGameObjects(selection, conc_num, new Vector3(0, 0, 0));
        //Debug.Log(selection);
    }

    public void DestroyButton()
    {
        for (int i = 0; i < conc_num; i++)
        {
            PG.DestroyGameObject();
        }
        
    }


    public void Clear_Button()
    {
        List <GameObject> generatedList = PG.GetGeneratedList();
        while (generatedList.Count != 0)
        {
            PG.DestroyGameObject();

        }
    }

    public void Temperature_Change(float value)
    {
        List<GameObject> generatedList = PG.GetGeneratedList();

        int i = 0;
        while (i < generatedList.Count)
        {
            generatedList[i].GetComponent<ParticlePhysics>().Modify_Average_Speed(value);
            i++;
        }
    }

    public void Object_Selection(int num) { selection = num; }

    public void QuantityNum() { conc_num = (int)conc_slider.value; conc_str.text = conc_num.ToString(); }

    public void Lid_Up(bool up) { upLidActive = up; }

    public void Lid_Down(bool down) { downLidActive = down; }

    public void Temp_Slider(bool up) { temp_point_up = up; }

    public void Set_Lid(GameObject newLid) { pressureManager.GetComponent<Pressure_Manager>().Set_Lid(newLid); }

    public void Dismiss_Welcome() { GameObject.Find("AR Session Origin").GetComponent<PlacePrefab>().enabled = true; }


}



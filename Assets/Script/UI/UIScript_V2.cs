
// This script handles user interactions for spawning particles and managing their behavior in a pressure chamber.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/*
  The following ‘using’ statements import various libraries and namespaces:
  - System.Collections and System.Collections.Generic: Provide access to collections such as lists and arrays.
  - UnityEngine: Provides access to Unity’s core functionality for creating games and applications.
  - UnityEngine.UI: Allows usage of Unity’s UI components like buttons, sliders, and text elements.
  - TMPro: Allows usage of TextMeshPro components for rendering high-quality text in the UI.
*/

public class UIScript_V2 : MonoBehaviour
{
    // Variables for managing particle generation
    [Header("Particle Generation")]
    public GameObject particleGen;
    public GameObject spawn;
    public TextMeshProUGUI conc_str;
    public Slider conc_slider;
    private static int conc_num = 1;

    //// Variables for managing the lid of the pressure chamber
    //[Header("Lid")]
    //public GameObject pressureManager;
    //public GameObject lid;
    //private static bool upLidActive;
    //private static bool downLidActive;

    // Variables for managing the lid of the pressure chamber
    [Header("Temperature")]
    public Slider temp_slider;
    private static bool temp_point_up;

    // Variables for selecting the type of particle to generate
    [Header("Selection")]
    public int selection = 0;

    // Reference to the ParticleGeneration script, which handles particle creation and destruction
    private ParticleGeneration PG;


    // Initialization method
    private void Start()
    {
        PG = particleGen.GetComponent<ParticleGeneration>(); // Gets the ParticleGeneration component from the particleGen
        Temperature_Change(0.15f); // Initializes the temperature change
    }

    // Physics update method called at fixed time intervals
    private void FixedUpdate()
    {
        QuantityNum(); // Updates the number of particles based on the slider value

        if (temp_point_up)
        {
            Temperature_Change(temp_slider.value);  // Adjusts the temperature based on the slider value
        }
    }

    public void CreateButton()
    {
        // Creates particles at a random location with the specified quantity
        PG.InstantiateGameObjects(selection, conc_num, new Vector3(0, 0, 0));
        //Debug.Log(selection);
    }

    // Method to handle the destroy button press
    public void DestroyButton()
    {
        for (int i = 0; i < conc_num; i++)
        {
            PG.DestroyGameObject();
        }
        
    }

    // Method to clear all generated particles
    public void Clear_Button()
    {
        List <GameObject> generatedList = PG.GetGeneratedList();
        while (generatedList.Count != 0)
        {
            PG.DestroyGameObject();

        }
    }

    // Method to change the temperature of all particles
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

    // Methods to set various states and selections
    public void Object_Selection(int num) { selection = num; }
    
    public void QuantityNum() { conc_num = (int)conc_slider.value; conc_str.text = conc_num.ToString(); }

    public void Temp_Slider(bool up) { temp_point_up = up; }

    public void Dismiss_Welcome() { GameObject.Find("AR Session Origin").GetComponent<PlacePrefab>().enabled = true; }


}



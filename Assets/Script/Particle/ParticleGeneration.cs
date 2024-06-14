using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleGeneration : MonoBehaviour
{

    [Header("Particle")]
    private GameObject generate;

    [SerializeField] public List<GameObject> objectList;

    [SerializeField] static public List<GameObject> generatedList;
    

    [Header("Spawn")]
    public GameObject spawn;
    private float spawn_x, spawn_y, spawn_z;
    private float spawnHeight;

    private void Awake()
    {
        generatedList = new List<GameObject>();
    }


    private void Update()
    {
        spawn_x = spawn.transform.position.x;
        spawn_y = spawn.transform.position.y;
        spawn_z = spawn.transform.position.z;
    }

    //function takes in the type of object, the number of object it should spawn, and the position to spawn it at. 
    public void InstantiateGameObjects(int selectedObj, int count, Vector3 position)
    {
        GameObject prefab = objectList[selectedObj];
        //Assign random variables to x, y, z rotation axis
        var rV = prefab.transform.rotation.eulerAngles;
        
        float newPos_X = position.x;
        float newPos_Y = position.y;
        float newPos_Z = position.z;

        //Create new molecule at random position and add it to list
        for (int i = 0; i < count; i++)
        {
            rV.x = Random.Range(-180f, 180f);
            rV.y = Random.Range(-180f, 180f);
            rV.z = Random.Range(-180f, 180f);
            prefab.transform.rotation = Quaternion.Euler(rV);

            //randPos holds random position

            newPos_X = Random.Range(spawn_x - .168f, spawn_x + 0.168f);
            newPos_Y = Random.Range(spawn_y + 0.03f, spawn_y + (0.19f + (.29f * spawnHeight)));
            newPos_Z = Random.Range(spawn_z - .1f, spawn_z + .1f);

            //Debug.Log("spawn_y + (.2f + 10f * spawnHeight): " + (spawn_y + (.2f + 10f * spawnHeight)));
            position = new Vector3(newPos_X, newPos_Y, newPos_Z);
  
                
            //generate holds an instant of prefab with random position and current rotation
            generate = Instantiate(prefab, position, prefab.transform.rotation);

            //adds instant to the NO2 list.
            generatedList.Add(generate);

            //Debug.Log("Molecule List count after spawn = " + moleculeList.Count);
            generate.transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime));
        }
    }

    //Currently it only destroys last object that was added to list after creating one
    public void DestroyGameObject()
    {
        int lastIndex = generatedList.Count - 1;
        if (generatedList.Count != 0)
        {
            Destroy(generatedList[lastIndex]);
            generatedList.RemoveAt(lastIndex);
            generatedList.TrimExcess();
        }
    }

    public void Spawn_Height(float num)
    {

        spawnHeight = num;
        Debug.LogWarning("this: " + (spawn.transform.position.y + 1.9f) + " SpawnHeight: " + spawnHeight + " num: " + num);
    }

    public float Get_Spawn_Height()
    {
        return spawnHeight;
    }

    public GameObject Get_Spawner()
    {
        return spawn;
    }

    public void Set_Spawner(GameObject newSpawn)
    {
        spawn = newSpawn;
    }

    public List<GameObject> GetGeneratedList()
    {
        return generatedList;
    }

}

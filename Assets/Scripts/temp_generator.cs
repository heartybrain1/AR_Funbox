using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using System.IO;
public class temp_generator : MonoBehaviour
{
    public Transform animalGrp;
    string str;
    int index;

    void Start()
    {

        str = temp_index.str;
        index = temp_index.index;
        Debug.Log(str);
        Debug.Log(index);


        //string path1 = "PTYPE/" + str;





        /*
        TextAsset text = Resources.Load(path1) as TextAsset;
        PatternState data = JsonUtility.FromJson<PatternState>(text.ToString());

        //branch
        int numBranch = data.branches.Length;
        Transform branchGrp = transform.GetChild(1).GetChild(2);
        for (int i = 0; i < numBranch; i++)
        {
            //type of the branch
            int countEnum = System.Enum.GetValues(typeof(branchTYPE)).Length;
            int chance = Random.Range(0, countEnum);
            branchTYPE type = (branchTYPE)chance;

            //create the branch gameobject of type
            string path2 = "Prefabs/tmp/" + type.ToString() + "_tmp";
            GameObject branchPrefab = Resources.Load<GameObject>(path2);
            GameObject obj = Instantiate(branchPrefab);
          
            obj.transform.SetParent(branchGrp);

            //branch lane
            int lane = data.branches[i].lane;
            if (lane == 1)
            {
                obj.transform.GetChild(0).localEulerAngles = new Vector3(0, 0, 30);
            }
            else if (lane == 2)
            {
                obj.transform.GetChild(0).localEulerAngles = new Vector3(0, 0, 0);
            }
            else if (lane == 3)
            {
                obj.transform.GetChild(0).localEulerAngles = new Vector3(0, 0, -30);
            }

            //branch position
            obj.GetComponent<SplineController>().Spline = transform.GetChild(1).GetChild(1).GetComponent<CurvySpline>();
            float range = (data.branches[i].pos);
            obj.GetComponent<SplineController>().Position = range;
       
        }

        //animal
        int numAnimal = data.animals.Length;
      
        for (int i = 0; i < numAnimal; i++)
        {
            //type of animal
            int type = data.animals[i].type;
            animalTYPE myAnimalTYPE = (animalTYPE)type;

            //create the animal gameobject of type
            string path2 = "Prefabs/animal/" + myAnimalTYPE.ToString();
            GameObject animalPrefab = Resources.Load<GameObject>(path2);
            GameObject obj = Instantiate(animalPrefab);
            obj.transform.SetParent(animalGrp);
            obj.transform.localPosition = new Vector3(0, 0, 0);

            //SET ANIMAL COMMON PROPERTIES
            Animal animal = obj.GetComponent<Animal>();
            animal.SpeedWalk = 1;
            animal.SpeedRotate = 100;
            animal.Activated = false;
           
            //animal lane
            int lane = data.animals[i].lane;
            if (lane == 1)
            {
                obj.transform.GetChild(0).localEulerAngles = new Vector3(0, 30, 0);
            }
            else if (lane == 2)
            {
                obj.transform.GetChild(0).localEulerAngles = new Vector3(0, 0, 0);
            }
            else if (lane == 3)
            {
                obj.transform.GetChild(0).localEulerAngles = new Vector3(0, -30, 0);
            }

            //animal position
            obj.GetComponent<SplineController>().Spline = transform.GetChild(1).GetChild(1).GetComponent<CurvySpline>();
            float range = (data.animals[i].pos );
            //float relative = range * 0.1f;
            obj.GetComponent<SplineController>().Position = range;
            obj.GetComponent<SplineController>().Speed = 0;
            obj.GetComponent<SplineController>().Play();
            animal.DelayTime = obj.GetComponent<SplineController>().Position * 1 / 2 + 0.5f;
        }
            */
    }

    public void tempActivate()
    {
        int count = animalGrp.childCount;
        for (int i = 0; i < count; i++)
        {
           // animalGrp.GetChild(i).GetComponent<Animal>().activateAnimal();
        }
    }
     
    }

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;


public class Display : MonoBehaviour
{
    public TextAsset xmlRawData;
    public List<GameObject> projectGroup = new List<GameObject>();
    public GameObject squarePrefab;
 
    public Toggle sumToggle;
    public Toggle numberToggle;
    public Vector3 initialScale;

    public Toggle economy;
    public Toggle comfort;
    public Toggle premium;


    // Start is called before the first frame update
    void Start()
    {
        sumToggle.onValueChanged.AddListener(delegate { ToggleValueChangedSum(sumToggle); });
        economy.onValueChanged.AddListener(delegate { ToggleValueChangedClassType(economy); });
        comfort.onValueChanged.AddListener(delegate { ToggleValueChangedClassType(comfort); });
        premium.onValueChanged.AddListener(delegate { ToggleValueChangedClassType(premium); });
        GetXMLData();
        CreateSquares();
        ReSizeHeight();
    }



    public void GetXMLData()
    {
        string data = xmlRawData.text;
        StringCollection collection = XmlReader.XmlGetMany(data, "/Projects/Project");
        char character = ':';
        
        foreach (string text in collection)
        {
            GameObject obj = Instantiate(squarePrefab);
            obj.AddComponent<Project>();

            string [] projectData = text.Split(character);
            
            obj.GetComponent<Project>().ID = projectData[0];
            obj.GetComponent<Project>().nameOfProject = projectData[1];
            obj.GetComponent<Project>().position = ConvertToVectorTwo(projectData[2]);
            obj.GetComponent<Project>().classType = projectData[3];
            obj.GetComponent<Project>().sum = int.Parse(projectData[4]);
            obj.GetComponent<Project>().numberOfApartments = int.Parse(projectData[5]);
            projectGroup.Add(obj);            
        }
    }

    public void CreateSquares()
    {
        foreach (GameObject obj in projectGroup)
        {
            Project aProject = obj.GetComponent<Project>();
            obj.transform.position = new Vector3(aProject.position.x, aProject.position.y, 0);
            initialScale = obj.transform.localScale;
            PaintColor(obj.GetComponent<SpriteRenderer>(), aProject.classType);
            if (sumToggle.isOn)
            {
                obj.GetComponentInChildren<TMP_Text>().text = aProject.sum.ToString();
            }
            else if(numberToggle.isOn)
            {
                obj.GetComponentInChildren<TMP_Text>().text = aProject.numberOfApartments.ToString();
            }            
        }
    }

    public void ReSizeHeight()
    {
        if (sumToggle.isOn)
        {
            ReSizeBasedOnSum();
        }
        else
        {
            ReSizeBasedOnNumber();
        }        
    }







    public void ReSizeBasedOnSum()
    {
        List<int> sumList = new List<int>();
        foreach (GameObject obj in projectGroup)
        {
            obj.transform.localScale = initialScale;
            Project pro = obj.GetComponent<Project>();
            sumList.Add(pro.sum);
        }
        int min = sumList.Min();
        foreach (GameObject obj in projectGroup)
        {
            Project pro = obj.GetComponent<Project>();
            if (pro.sum > min)
            {
                int val = pro.sum * 1 / min;
                obj.transform.localScale = new Vector3(obj.transform.localScale.x, obj.transform.localScale.y * val, obj.transform.localScale.z);
            }
        }
    }
    public void ReSizeBasedOnNumber()
    {
        List<int> numberList = new List<int>();
        foreach (GameObject obj in projectGroup)
        {
            obj.transform.localScale = initialScale;
            Project pro = obj.GetComponent<Project>();
            numberList.Add(pro.numberOfApartments);
        }
        int min = numberList.Min();
        foreach (GameObject obj in projectGroup)
        {
            Project pro = obj.GetComponent<Project>();
            if (pro.numberOfApartments > min)
            {
                int val = pro.numberOfApartments * 1 / min;
                obj.transform.localScale = new Vector3(obj.transform.localScale.x, obj.transform.localScale.y * val, obj.transform.localScale.z);
            }
        }
    }
    void ToggleValueChangedSum(Toggle change)
    {
        if (change.isOn)
        {
            foreach (GameObject obj in projectGroup)
            {
                obj.GetComponentInChildren<TMP_Text>().text = obj.GetComponent<Project>().sum.ToString();
                ReSizeBasedOnSum();
            }
        }
        else
        {
            foreach (GameObject obj in projectGroup)
            {
                obj.GetComponentInChildren<TMP_Text>().text = obj.GetComponent<Project>().numberOfApartments.ToString();
                ReSizeBasedOnNumber();
            }
        }
    }

    void ToggleValueChangedClassType(Toggle change)
    {
        switch (change.name)
        {
            case "Economy":
                SortWithClassType(change.isOn, change.name);
                break;
            case "Comfort":
                SortWithClassType(change.isOn, change.name);
                break;
            case "Premium":
                SortWithClassType(change.isOn, change.name);
                break;
            default:
                break;
        }
    }


    public void SortWithClassType(bool val, string classType)
    {
        if (val)
        {
            foreach (GameObject obj in projectGroup)
            {
                if (obj.GetComponent<Project>().classType.Equals(classType))
                {
                    obj.SetActive(true);
                }
            }
        }
        else
        {
            foreach (GameObject obj in projectGroup)
            {
                if (obj.GetComponent<Project>().classType.Equals(classType))
                {
                    obj.SetActive(false);
                }
            }
        }        
    }

    public void PaintColor(SpriteRenderer sp, string classType)
    {
        switch (classType)
        {
            case "Economy":
                sp.color = Color.blue;
                break;
            case "Comfort":
                sp.color = Color.cyan;
                break;
            case "Premium":
                sp.color = Color.green;
                break;
            default:                
                break;
        }
    }
    public Vector2 ConvertToVectorTwo(string text)
    {        
        string [] positionText = text.Split(',');
        Vector2 pos = new Vector2(float.Parse(positionText[0]), float.Parse(positionText[1]));
        return pos;
    }
}

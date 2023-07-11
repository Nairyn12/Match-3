using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResourceCounter : MonoBehaviour
{
    [SerializeField] private Text goldCounterText, coalCounterText, foodCounterText, woodCounterText, metallCounterText, knowCounterText;

    private void Start()
    {        
        goldCounterText.text = ResourcesContainer.gold.ToString();
        coalCounterText.text = ResourcesContainer.coal.ToString();
        foodCounterText.text = ResourcesContainer.food.ToString();
        woodCounterText.text = ResourcesContainer.wood.ToString();
        metallCounterText.text = ResourcesContainer.metall.ToString();
        knowCounterText.text = ResourcesContainer.know.ToString();
    }

    public void ConsiderResources(List<Tile> resources)
    {
        goldCounterText.text = (ResourcesContainer.gold += ConsiderSpecificResources(resources, "Gold")).ToString();
        coalCounterText.text = (ResourcesContainer.coal += ConsiderSpecificResources(resources, "Coal")).ToString();
        foodCounterText.text = (ResourcesContainer.food += ConsiderSpecificResources(resources, "Food")).ToString();
        woodCounterText.text = (ResourcesContainer.wood += ConsiderSpecificResources(resources, "Wood")).ToString();
        metallCounterText.text = (ResourcesContainer.metall += ConsiderSpecificResources(resources, "Metall")).ToString();
        knowCounterText.text = (ResourcesContainer.know += ConsiderSpecificResources(resources, "Know")).ToString();
    }

    private int ConsiderSpecificResources(List<Tile> resourcesList, string typeOfResource)
    {
        int count = 0;

        for (int i = 0; i < resourcesList.Count; i++)
        {
            if (resourcesList[i].TypeOfTiles == typeOfResource)
            {
                count++;
            }
        }

        if (count > 2) return count - 2;
        else return 0;
    }

    public void OutputtingResourcesToIinterface ()
    {
        goldCounterText.text = ResourcesContainer.gold.ToString();
        coalCounterText.text = ResourcesContainer.coal.ToString();
        foodCounterText.text = ResourcesContainer.food.ToString();
        woodCounterText.text = ResourcesContainer.wood.ToString();
        metallCounterText.text = ResourcesContainer.metall.ToString();
        knowCounterText.text = ResourcesContainer.know.ToString();
    }
}

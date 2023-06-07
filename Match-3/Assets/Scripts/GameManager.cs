using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public event Action loadingDefenders;
    public event Action savingDefenders;

    private int warriorsCount, knightsCount, heroesCount, healersCount, wizzardsCount, dragonsCount,
        stockadesCount, forgesCount, barracksCount, mageTowersCount, marketsCount, dragonCavesCount;

    public int WarriorsCount
    {
        get => warriorsCount;
        set { warriorsCount = value; }
    }

    public int KnightsCount
    {
        get => knightsCount;
        set { knightsCount = value; }
    }

    public int HeroesCount
    {
        get => heroesCount;
        set { heroesCount = value; }
    }

    public int HealersCount
    {
        get => healersCount;
        set { healersCount = value; }
    }

    public int WizzardsCount
    {
        get => wizzardsCount;
        set { wizzardsCount = value; }
    }

    public int DragonsCount
    {
        get => dragonsCount;
        set { dragonsCount = value; }
    }

    public int StockadesCount
    {
        get => stockadesCount;
        set { stockadesCount = value; }
    }

    public int ForgesCount
    {
        get => forgesCount;
        set { forgesCount = value; }
    }

    public int BarracksCount
    {
        get => barracksCount;
        set { barracksCount = value; }
    }

    public int MageTowersCount
    {
        get => mageTowersCount;
        set { mageTowersCount = value; }
    }

    public int MarketsCount
    {
        get => marketsCount;
        set { marketsCount = value; }
    }

    public int DragonCavesCount
    {
        get => dragonCavesCount;
        set { dragonCavesCount = value; }
    }

    private int nextSceneOffset;

    private void Awake()
    {
        
    }

    private void Start()
    {
        PlayerPrefs.GetInt("Gold", ResourcesContainer.gold);
        PlayerPrefs.GetInt("Coal", ResourcesContainer.coal);
        PlayerPrefs.GetInt("Food", ResourcesContainer.food);
        PlayerPrefs.GetInt("Wood", ResourcesContainer.wood);
        PlayerPrefs.GetInt("Metall", ResourcesContainer.metall);

        warriorsCount = PlayerPrefs.GetInt("Warrior", warriorsCount);
        knightsCount = PlayerPrefs.GetInt("Knight", knightsCount);
        heroesCount = PlayerPrefs.GetInt("Hero", heroesCount);
        healersCount = PlayerPrefs.GetInt("Healer", healersCount);
        wizzardsCount = PlayerPrefs.GetInt("Wizzard", wizzardsCount);
        dragonsCount = PlayerPrefs.GetInt("Dragon", dragonsCount);

        stockadesCount = PlayerPrefs.GetInt("Stockades", stockadesCount);
        forgesCount = PlayerPrefs.GetInt("Forges", forgesCount);
        barracksCount = PlayerPrefs.GetInt("Barracks", barracksCount);
        mageTowersCount = PlayerPrefs.GetInt("MageTowers", mageTowersCount);
        marketsCount = PlayerPrefs.GetInt("Markets", marketsCount);
        dragonCavesCount = PlayerPrefs.GetInt("DragonCaves", dragonCavesCount);

        Debug.Log("Количество воинов в загрузку: " + PlayerPrefs.GetInt("Warrior", warriorsCount));
        Debug.Log("Уровень ограды в загрузку: " + PlayerPrefs.GetInt("Stockades", stockadesCount));

        loadingDefenders?.Invoke();        
    }

    public void LoadingSceneInOrder()
    {
        nextSceneOffset = 1;
        SaveResources();
        SaveDefenders();
        SaveBuilds();
        StartCoroutine(LoadScreen());
    }

    public void LoadingPreviousScene()
    {
        nextSceneOffset = -1;
        SaveResources();
        SaveDefenders();
        SaveBuilds();
        StartCoroutine(LoadScreen());
    }

    public void QuitFromGame()
    {
        SaveResources();
        SaveDefenders();
        SaveBuilds();
        Application.Quit();
    }

    public void PlayerPrefsHasKey(string KeyName, int resource)
    {
        if (PlayerPrefs.HasKey(KeyName))
        {
            resource = PlayerPrefs.GetInt(KeyName);
        }
        else
            resource = 0;
    }

    private void SaveResources()
    {
        PlayerPrefs.SetInt("Gold", ResourcesContainer.gold);
        PlayerPrefs.SetInt("Coal", ResourcesContainer.coal);
        PlayerPrefs.SetInt("Food", ResourcesContainer.food);
        PlayerPrefs.SetInt("Wood", ResourcesContainer.wood);
        PlayerPrefs.SetInt("Metall", ResourcesContainer.metall);
    }

    private void SaveDefenders ()
    {
        savingDefenders?.Invoke();

        Debug.Log("Количество воинов в сохранение: " + warriorsCount);
        //Debug.Log("Количество рыцарей в сохранение: " + knightsCount);
        //Debug.Log("Количество героев в сохранение: " + heroesCount);
        //Debug.Log("Количество лекарей в сохранение: " + healersCount);
        //Debug.Log("Количество волшебников в сохранение: " + wizzardsCount);
        //Debug.Log("Количество драконов: " + dragonsCount);

        PlayerPrefs.SetInt("Warrior", warriorsCount);
        PlayerPrefs.SetInt("Knight", knightsCount);
        PlayerPrefs.SetInt("Hero", heroesCount);
        PlayerPrefs.SetInt("Healer", healersCount);
        PlayerPrefs.SetInt("Wizzard", wizzardsCount);
        PlayerPrefs.SetInt("Dragon", dragonsCount);

        PlayerPrefs.Save();
    }

    private void SaveBuilds()
    {
        savingDefenders?.Invoke();

        Debug.Log("Уровень ограды сохранение: " + stockadesCount);
        //Debug.Log("Количество рыцарей в сохранение: " + knightsCount);
        //Debug.Log("Количество героев в сохранение: " + heroesCount);
        //Debug.Log("Количество лекарей в сохранение: " + healersCount);
        //Debug.Log("Количество волшебников в сохранение: " + wizzardsCount);
        //Debug.Log("Количество драконов: " + dragonsCount);

        PlayerPrefs.SetInt("Stockades", stockadesCount);
        PlayerPrefs.SetInt("Forges", forgesCount);
        PlayerPrefs.SetInt("Barracks", barracksCount);
        PlayerPrefs.SetInt("MageTowers", mageTowersCount);
        PlayerPrefs.SetInt("Markets", marketsCount);
        PlayerPrefs.SetInt("DragonCaves", dragonCavesCount);

        PlayerPrefs.Save();
    }

    IEnumerator LoadScreen()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + nextSceneOffset);

        while (!asyncLoad.isDone)
        {
            //loadSlider.value = asyncLoad.progress;
            yield return null;
        }
    }
}

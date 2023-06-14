using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public event Action loadingEvent;
    public event Action savingEvent;
    public event Action battleEvent;

    private int _randomBetweenCountOfAttacks;
    private int _countOfAttacks;
    private int _betweenCountOfAttacks;

    public int RandomBetweenCountOfAttacks
    {
        get => _randomBetweenCountOfAttacks;
        set { _randomBetweenCountOfAttacks = value; }
    }

    public int CountOfAttacks
    {
        get => _countOfAttacks;
        set { _countOfAttacks = value; }
    }

    public int BetweenCountOfAttacks
    {
        get => _betweenCountOfAttacks;
        set { _betweenCountOfAttacks = value; }
    }

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

    private void Start()
    {
        PlayerPrefs.GetInt("Gold", ResourcesContainer.gold);
        PlayerPrefs.GetInt("Coal", ResourcesContainer.coal);
        PlayerPrefs.GetInt("Food", ResourcesContainer.food);
        PlayerPrefs.GetInt("Wood", ResourcesContainer.wood);
        PlayerPrefs.GetInt("Metall", ResourcesContainer.metall);

        warriorsCount = PlayerPrefs.GetInt("Warrior", warriorsCount);
        knightsCount = PlayerPrefs.GetInt("Knight", knightsCount);
        heroesCount = 0;
        //heroesCount = PlayerPrefs.GetInt("Hero", heroesCount);
        healersCount = PlayerPrefs.GetInt("Healer", healersCount);
        wizzardsCount = PlayerPrefs.GetInt("Wizzard", wizzardsCount);
        dragonsCount = PlayerPrefs.GetInt("Dragon", dragonsCount);

        stockadesCount = PlayerPrefs.GetInt("Stockades", stockadesCount);
        forgesCount = PlayerPrefs.GetInt("Forges", forgesCount);
        barracksCount = PlayerPrefs.GetInt("Barracks", barracksCount);
        mageTowersCount = PlayerPrefs.GetInt("MageTowers", mageTowersCount);
        marketsCount = PlayerPrefs.GetInt("Markets", marketsCount);
        dragonCavesCount = PlayerPrefs.GetInt("DragonCaves", dragonCavesCount);

        _countOfAttacks = PlayerPrefs.GetInt("CountOfAttacks", _countOfAttacks);
        _betweenCountOfAttacks = PlayerPrefs.GetInt("BetweenCountOfAttacks", _betweenCountOfAttacks);
        _randomBetweenCountOfAttacks = PlayerPrefs.GetInt("RandomBetweenCountOfAttacks", _randomBetweenCountOfAttacks);

        Debug.Log("�������� _countOfAttacks: " + _countOfAttacks);
        Debug.Log("�������� _betweenCountOfAttacks: " + _betweenCountOfAttacks);
        Debug.Log("�������� _randomBetweenCountOfAttacks: " + _randomBetweenCountOfAttacks);

        loadingEvent?.Invoke();        
    }

    public void LoadingSceneInOrder()
    {
        Debug.Log("����� �����: " + SceneManager.GetActiveScene().buildIndex);

        if (SceneManager.GetActiveScene().buildIndex == 1 && _betweenCountOfAttacks >= _randomBetweenCountOfAttacks)
        {
            battleEvent?.Invoke();
            return;
        }        

        nextSceneOffset = 1;
        SaveResources();
        SaveDefenders();
        SaveBuilds();
        StartCoroutine(LoadScreen());

        SaveCountOfAttacks();

        Debug.Log("���������� _countOfAttacks: " + _countOfAttacks);
        Debug.Log("���������� _betweenCountOfAttacks: " + _betweenCountOfAttacks);
        Debug.Log("���������� _randomBetweenCountOfAttacks: " + _randomBetweenCountOfAttacks);
    }

    public void LoadingPreviousScene()
    {
        nextSceneOffset = -1;
        SaveResources();
        SaveDefenders();
        SaveBuilds();
        SaveCountOfAttacks();
        StartCoroutine(LoadScreen());
        Debug.Log("���������� _countOfAttacks: " + _countOfAttacks);
        Debug.Log("���������� _betweenCountOfAttacks: " + _betweenCountOfAttacks);
        Debug.Log("���������� _randomBetweenCountOfAttacks: " + _randomBetweenCountOfAttacks);
    }

    public void QuitFromGame()
    {
        SaveResources();
        SaveDefenders();
        SaveBuilds();
        SaveCountOfAttacks();
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
        savingEvent?.Invoke();

        //Debug.Log("���������� ������ � ����������: " + warriorsCount);
        //Debug.Log("���������� ������� � ����������: " + knightsCount);
        //Debug.Log("���������� ������ � ����������: " + heroesCount);
        //Debug.Log("���������� ������� � ����������: " + healersCount);
        //Debug.Log("���������� ����������� � ����������: " + wizzardsCount);
        //Debug.Log("���������� ��������: " + dragonsCount);

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
        savingEvent?.Invoke();

        //Debug.Log("������� ������ ����������: " + stockadesCount);
        //Debug.Log("���������� ������� � ����������: " + knightsCount);
        //Debug.Log("���������� ������ � ����������: " + heroesCount);
        //Debug.Log("���������� ������� � ����������: " + healersCount);
        //Debug.Log("���������� ����������� � ����������: " + wizzardsCount);
        //Debug.Log("���������� ��������: " + dragonsCount);

        PlayerPrefs.SetInt("Stockades", stockadesCount);
        PlayerPrefs.SetInt("Forges", forgesCount);
        PlayerPrefs.SetInt("Barracks", barracksCount);
        PlayerPrefs.SetInt("MageTowers", mageTowersCount);
        PlayerPrefs.SetInt("Markets", marketsCount);
        PlayerPrefs.SetInt("DragonCaves", dragonCavesCount);

        PlayerPrefs.Save();
    }

    private void SaveCountOfAttacks()
    {
        savingEvent?.Invoke();

        PlayerPrefs.SetInt("CountOfAttacks", _countOfAttacks);
        PlayerPrefs.SetInt("BetweenCountOfAttacks", _betweenCountOfAttacks);
        PlayerPrefs.SetInt("RandomBetweenCountOfAttacks", _randomBetweenCountOfAttacks);
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

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

    private float _soundVolume = 5.0f, _musicVolume = 5.0f;
    public float SoundVolume { get => _soundVolume; set => _soundVolume = value; }
    public float MusicVolume { get => _musicVolume; set => _musicVolume = value; }


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

        _soundVolume = PlayerPrefs.GetFloat("Sound", _soundVolume);
        _musicVolume = PlayerPrefs.GetFloat("Music", _musicVolume);

        Debug.Log("ЗАГРУЗКА _countOfAttacks: " + _countOfAttacks);
        Debug.Log("ЗАГРУЗКА _betweenCountOfAttacks: " + _betweenCountOfAttacks);
        Debug.Log("ЗАГРУЗКА _randomBetweenCountOfAttacks: " + _randomBetweenCountOfAttacks);

        loadingEvent?.Invoke();        
    }

    public void LoadingSceneInOrder()
    {
        Debug.Log("Номер сцены: " + SceneManager.GetActiveScene().buildIndex);

        if (SceneManager.GetActiveScene().buildIndex == 1 && _betweenCountOfAttacks >= _randomBetweenCountOfAttacks)
        {
            battleEvent?.Invoke();
            return;
        }        

        nextSceneOffset = 1;
        SaveResources();
        SaveDefenders();
        SaveBuilds();
        SaveMusicSound();
        StartCoroutine(LoadScreen());

        SaveCountOfAttacks();

        Debug.Log("СОХРАНЕНИЕ _countOfAttacks: " + _countOfAttacks);
        Debug.Log("СОХРАНЕНИЕ _betweenCountOfAttacks: " + _betweenCountOfAttacks);
        Debug.Log("СОХРАНЕНИЕ _randomBetweenCountOfAttacks: " + _randomBetweenCountOfAttacks);
    }

    public void LoadingPreviousScene()
    {
        nextSceneOffset = -1;
        SaveResources();
        SaveDefenders();
        SaveBuilds();
        SaveMusicSound();
        SaveCountOfAttacks();
        StartCoroutine(LoadScreen());
        Debug.Log("СОХРАНЕНИЕ _countOfAttacks: " + _countOfAttacks);
        Debug.Log("СОХРАНЕНИЕ _betweenCountOfAttacks: " + _betweenCountOfAttacks);
        Debug.Log("СОХРАНЕНИЕ _randomBetweenCountOfAttacks: " + _randomBetweenCountOfAttacks);
    }

    public void QuitFromGame()
    {
        SaveResources();
        SaveDefenders();
        SaveBuilds();
        SaveMusicSound();
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

        //Debug.Log("Количество воинов в сохранение: " + warriorsCount);
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
        savingEvent?.Invoke();

        //Debug.Log("Уровень ограды сохранение: " + stockadesCount);
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

    private void SaveCountOfAttacks()
    {
        savingEvent?.Invoke();

        PlayerPrefs.SetInt("CountOfAttacks", _countOfAttacks);
        PlayerPrefs.SetInt("BetweenCountOfAttacks", _betweenCountOfAttacks);
        PlayerPrefs.SetInt("RandomBetweenCountOfAttacks", _randomBetweenCountOfAttacks);
    }

    private void SaveMusicSound()
    {
        PlayerPrefs.SetFloat("Sound", _soundVolume);
        PlayerPrefs.SetFloat("Music", _musicVolume);
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

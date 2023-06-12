using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPosition : MonoBehaviour
{
    public GameObject _barrackGO;
    public GameObject _forgeGO;
    public GameObject _mageTowerGO;
    public GameObject _marketGO;

    public Sprite _ruinsBarrackSprite;
    public Sprite _ruinsForgeSprite;
    public Sprite _ruinsMageTowerSprite;
    public Sprite _ruinsMarketSprite;

    [SerializeField] private Sprite _barrackSprite;
    [SerializeField] private Sprite _forgeSprite;
    [SerializeField] private Sprite _mageTowerSprite;
    [SerializeField] private Sprite _marketSprite;
    


    private void Start()
    {
        _barrackGO.SetActive(false);
    }

    public void OnBuilding(int quantity, GameObject build)
    {
        if (quantity == 1)
            build.SetActive(true);
    }

    public void OnRuinsBuilding(int quantity, GameObject build, Sprite ruin)
    {
        build.GetComponent<SpriteRenderer>().sprite = ruin;
    }

}

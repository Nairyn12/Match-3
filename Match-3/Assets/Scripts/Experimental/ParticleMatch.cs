using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMatch : MonoBehaviour
{
    [SerializeField] private ParticleSystem goldSystem;
    [SerializeField] private ParticleSystem foodSystem;
    [SerializeField] private ParticleSystem woodSystem;
    [SerializeField] private ParticleSystem knowSystem;
    [SerializeField] private ParticleSystem coalSystem;
    [SerializeField] private ParticleSystem metallSystem;    

    public void PlayParticleSystem(Tile tile)
    {
        ParticleSystem system = new();
       
        Vector3 position = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z-1);
        if (tile.TypeOfTiles == "Gold") system = Instantiate(goldSystem, position, Quaternion.identity);
        if (tile.TypeOfTiles == "Food") system = Instantiate(foodSystem, position, Quaternion.identity);
        if (tile.TypeOfTiles == "Wood") system = Instantiate(woodSystem, position, Quaternion.identity);
        if (tile.TypeOfTiles == "Know") system = Instantiate(knowSystem, position, Quaternion.identity);
        if (tile.TypeOfTiles == "Coal") system = Instantiate(coalSystem, position, Quaternion.identity);
        if (tile.TypeOfTiles == "Metall") system = Instantiate(metallSystem, position, Quaternion.identity);

        ParticleSystemRenderer systRend = system.gameObject.GetComponent<ParticleSystemRenderer>();
        systRend.sortingOrder = 10;
        system.Play();        
        StartCoroutine(DestroyParticleSystem(system));
    }

    IEnumerator DestroyParticleSystem(ParticleSystem particleSystem)
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(particleSystem);
    }
}

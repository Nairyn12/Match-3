using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TilesObject : ScriptableObject
{ 
    [SerializeField] private List<string> _idTags = new List<string>();
    [SerializeField] private List<Sprite> _sprites = new List<Sprite>();

    [SerializeField] private Dictionary<string, Sprite> typeOfTiles = new();

    public Dictionary<string, Sprite> TypeOfTiles { get => typeOfTiles; set => typeOfTiles = value; }
    public List<string> IdTags { get => _idTags; set => _idTags = value; }
    public List<Sprite> Sprites { get => _sprites; set => _sprites = value; }
    

   

    

}

using UnityEngine;

namespace EncounterScene
{
    public class SpawnToken: MonoBehaviour
    {
        [SerializeField] private GameObject tokenPrefab;

        public void InstantiateToken(Character character)
        {
            var token = Instantiate(tokenPrefab, GameObject.Find("Grid").transform);
            token.GetComponent<Token>().Initialize(character);
            
            token.transform.position = GridControl.MapToGridCoordinates(Camera.main.transform.position,
                EncounterData.Instance.Size, Grid.PlateSize);
        }
        
        public void InstantiateToken(Character character, Vector2 position)
        {
            var token = Instantiate(tokenPrefab, GameObject.Find("Grid").transform);
            token.GetComponent<Token>().Initialize(character);

            token.transform.position = GridControl.GetPlateCoordinates(position, 
                EncounterData.Instance.Size, Grid.PlateSize);
        }
    }
}
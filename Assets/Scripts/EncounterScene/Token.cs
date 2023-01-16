using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static EncounterScene.GridControl;
using static EncounterScene.Grid;

namespace EncounterScene
{
    public class Token : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        private Character _character;
        public Character Character => _character;

        public void Initialize(Character character)
        {
            _character = character;
            GetComponent<BoxCollider2D>().size = new Vector2(PlateSize, PlateSize);
            
            GetComponent<Image>().rectTransform.sizeDelta = new Vector2(PlateSize, PlateSize);
            GetComponent<Image>().sprite = mImage.GetSprite(_character.Image, PlateSize, PlateSize);
            
            EncounterManager.Instance.AddToken(gameObject);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = MapToGridCoordinates(transform.position,
                EncounterData.Instance.Size, PlateSize);
            
            EncounterManager.Instance.SetLastToken(gameObject);
        }
    }
}

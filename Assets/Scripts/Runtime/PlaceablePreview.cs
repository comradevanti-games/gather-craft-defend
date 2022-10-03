using UnityEngine;

namespace GatherCraftDefend
{

    public class PlaceablePreview : MonoBehaviour
    {

        [SerializeField] private new Camera camera;
        [SerializeField] private SpriteRenderer spriteRenderer;

        
        private void Update()
        {
            var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            var snapped = mousePos.WithZ(0).SnapToGrid();
            transform.position = snapped;
        }
        
        public void Show(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
            enabled = true;
        }

        public void Hide()
        {
            spriteRenderer.sprite = null;
            enabled = false;
        }

    }

}
using UnityEngine;

namespace EncounterScene
{
    public class MoveCamera : MonoBehaviour
    {
        [SerializeField] private float scaleSpeed;
        [SerializeField] private float moveSpeed;

        private Vector2 movement;
        private bool _dragging;

        private void Start()
        {
            movement = new Vector2();
        }

        private void Update()
        {
            GetComponent<Camera>().orthographicSize -= Input.mouseScrollDelta.y * scaleSpeed;
            
            float scale = GetComponent<Camera>().orthographicSize;

            if (GetComponent<Camera>().orthographicSize < 50)
                GetComponent<Camera>().orthographicSize = 50;

            if (Input.GetMouseButtonDown(1))
                _dragging = true;

            if (_dragging)
            {
                movement.x -= Input.GetAxis("Mouse X") * scale * moveSpeed * Time.deltaTime;
                movement.y -= Input.GetAxis("Mouse Y") * scale * moveSpeed * Time.deltaTime;
            }
            
            if (Input.GetMouseButtonUp(1))
                _dragging = false;
        }

        private void FixedUpdate()
        {
            float scale = GetComponent<Camera>().orthographicSize;
            
            movement.x += Input.GetAxisRaw("Horizontal") * scale / 50;
            movement.y += Input.GetAxisRaw("Vertical")* scale / 50;

            transform.position = new Vector3(movement.x, movement.y, -10);
        }
    }
}
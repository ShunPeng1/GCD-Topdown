using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private InputActionReference pointerPosition;
    private float mousePosX;
    private float mousePosY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = GetPointerInput();
        mousePosX = mousePos.x / mousePos.magnitude;
        mousePosY = mousePos.y / mousePos.magnitude;
    }
    private Vector2 GetPointerInput() {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}

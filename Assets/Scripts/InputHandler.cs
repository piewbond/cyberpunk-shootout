using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private GameEnv gameEnv;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started || gameEnv.isPlayedOnModel) return;
        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (!rayHit.collider) return;

        var hitObject = rayHit.collider.gameObject;
        if (hitObject.CompareTag("Weapon"))
        {
            var weapon = hitObject.GetComponent<WeaponController>();
            weapon.OnClick();
        }
        if (hitObject.CompareTag("Player"))
        {
            var player = hitObject.GetComponent<PlayerController>();
            player.OnClick();
        }
    }
}

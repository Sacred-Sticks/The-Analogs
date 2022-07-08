using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerInput;
    [SerializeField] private string actionMapName;
    [Space]

    [SerializeField] private string selectValueStr;
    private InputAction selectValueAction;
    private float selectValue;

    [SerializeField] private string activateValueStr;
    private InputAction activateValueAction;
    private float activateValue;

    private void Awake()
    {
        var actionMap = playerInput.FindActionMap(actionMapName);

        selectValueAction = actionMap.FindAction(selectValueStr);
        activateValueAction = actionMap.FindAction(activateValueStr);

        selectValueAction.performed += OnSelectValueChanged;
        selectValueAction.canceled += OnSelectValueChanged;
        selectValueAction.Enable();

        activateValueAction.performed += OnActivateValueChanged;
        activateValueAction.canceled += OnActivateValueChanged;
        activateValueAction.Enable();
    }

    private void OnSelectValueChanged(InputAction.CallbackContext context)
    {
        selectValue = context.ReadValue<float>();
    }

    private void OnActivateValueChanged(InputAction.CallbackContext context)
    {
        activateValue = context.ReadValue<float>();
    }

    public float GetSelectValue()
    {
        return selectValue;
    }

    public float GetActivateValue()
    {
        return activateValue;
    }
}

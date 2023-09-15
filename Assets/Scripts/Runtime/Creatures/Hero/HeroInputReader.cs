using UnityEngine;
using Scripts.Creatures.Hero;
using UnityEngine.InputSystem;

namespace Scripts
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField]
        private Hero hero;

        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            hero.SetDirection(direction);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                hero.Interact();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                hero.Attack();
        }

        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.started)
                hero.StartThrowing();

            if (context.canceled)
                hero.UseInventory();
        }

        public void OnNextItem(InputAction.CallbackContext context)
        {
            if (context.performed)
                hero.NextItem();
        }

        public void OnDropDown(InputAction.CallbackContext context)
        {
            if (context.performed)
                hero.DropDown();
        }

        public void OnUsePerk(InputAction.CallbackContext context)
        {
            if (context.performed)
                hero.UsePerk();
        }

        public void OnToggleFlashlight(InputAction.CallbackContext context)
        {
            if (context.performed)
                hero.ToggleFlashlight();
        }
    }
}
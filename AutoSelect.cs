using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace primemakers.runner.menus
{
    public class AutoSelect : MonoBehaviour
    {
        [SerializeField] private bool _isPopupButton;
        private Selectable _selected;
        
        private void OnEnable()
        {
            if(_isPopupButton && EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
                _selected = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
            
            StartCoroutine(SelectButton(GetComponent<Selectable>()));
        }

        private void OnDisable()
        {
            if(_isPopupButton)
                _selected?.Select();
        }
        
        private IEnumerator SelectButton(Selectable button)
        {
            yield return new WaitForSeconds(.02F);
            button?.Select();
        }
    }
}

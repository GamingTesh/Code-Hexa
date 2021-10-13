using UnityEngine;
using TMPro;

namespace GTS.Manager.UI
{
    /// <summary>
    /// UI element for notifications
    /// </summary>
    public class NotificationPopup : MonoBehaviour
    {
        public TextMeshProUGUI messageText;

        private void Awake()
        {
            Destroy(gameObject, 3f);
        }

        public void SetMessage(string message)
        {
            messageText.text = message;
        }
    }
}

using UnityEngine;

namespace GTS.Manager
{
    /// <summary>
    /// Handles notifications
    /// </summary>
    public class NotificationManager : MonoBehaviour
    {
        #region singleton
        public static NotificationManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        #endregion

        public GameObject notificationPopup;
        public Transform notificationHolder;

        public void Notify(string message)
        {
            GameObject GO = Instantiate(notificationPopup, notificationHolder);
            UI.NotificationPopup popup = GO.GetComponent<UI.NotificationPopup>();
            popup.SetMessage(message);
        }
    }
}

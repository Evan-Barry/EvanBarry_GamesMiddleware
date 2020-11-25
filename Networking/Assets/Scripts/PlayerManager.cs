using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;

using System.Collections;

namespace Com.MyCompany.MyGame
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        public float health = 1f;

        void Start()
        {
            CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();


            if (_cameraWork != null)
            {
                if (photonView.IsMine)
                {
                    _cameraWork.OnStartFollowing();
                }
            }
            else
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }
        }

        void Update()
        {
            if (health <= 0f)
            {
                GameManager.Instance.LeaveRoom();
            }
        }

        void onTriggerEnter(Collider other)
        {
            if(!photonView.IsMine)
            {
                return;
            }

            if(!other.name.Contains("Player"))
            {
                return;
            }

            health -= 0.1f;
        }

        void onTriggerStay(Collider other)
        {
            if(!photonView.IsMine)
            {
                return;
            }

            if(!other.name.Contains("Player"))
            {
                return;
            }

            health -= 0.1f*Time.deltaTime;
        }
    }
}
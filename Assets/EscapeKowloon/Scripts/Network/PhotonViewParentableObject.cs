using Photon.Pun;
using UnityEngine;

namespace EscapeKowloon.Scripts.Network
{
    public class PhotonViewParentableObject : MonoBehaviour
    {
        [SerializeField] private string _parentPathVr = "";
        [SerializeField] private string _alternativePath = "";

        void Start()
        {
            ParentObject(_parentPathVr);
        }

        void ParentObject(string path)
        {
            var photonView = GetComponent<PhotonView>();
            var parent = GameObject.Find(photonView.IsMine ? path : _alternativePath);

            {
                var t = transform;
                t.parent = parent.transform;
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
            }
        }
    }
}
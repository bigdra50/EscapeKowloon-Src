using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

// https://qiita.com/kousaku-maron/items/106619d0c065be155bbb
namespace EscapeKowloon.Scripts.Escaper
{
    public class TeleportHandler : MonoBehaviour
    {
        [SerializeField] GameObject pointer;
        [SerializeField] private OVRScreenFade _fade;
        [SerializeField] private Camera _head;
        [SerializeField] private Material material;

        [SerializeField] private float distance = 10;

        [SerializeField] private float dropHeight = 5;

        [SerializeField] private int positionCount = 10;

        [SerializeField] private float width = 0.1f;

        private OVRPlayerController _ovrPlayerController;
        private GameObject _line;
        private LineRenderer _lRend;
        private bool _isDrawActive;
        private Vector3 _teleportPoint;
        private RaycastHit _hit;
        private bool _isMovable;

        void Start()
        {
            OVRManager.display.RecenterPose();
            InitLine();
            _ovrPlayerController = GetComponent<OVRPlayerController>();
            _isMovable = true;
        }

        void Update()
        {
            Draw();
        }

        public void StartDraw()
        {
            _isDrawActive = true;
            _line.SetActive(true);
        }

        private GameObject obj;

        public void StopDraw(Transform current)
        {
            _isDrawActive = false;
            _line.SetActive(false);
            TryTeleport(current);
            obj = null;
        }

        void InitLine()
        {
            _line = new GameObject("Line");
            _line.transform.parent = pointer.transform;

            _lRend = _line.AddComponent<LineRenderer>();
            _lRend.receiveShadows = false;
            _lRend.shadowCastingMode = ShadowCastingMode.Off;
            _lRend.loop = false;
            _lRend.positionCount = positionCount;
            _lRend.startWidth = width;
            _lRend.endWidth = width;

            _lRend.material = material;
        }

        void Draw()
        {
            if (!_isDrawActive) return;
            Vector3 p0 = pointer.transform.position;
            Vector3 p1 = pointer.transform.position + pointer.transform.forward * distance / 2;
            Vector3 p2 = pointer.transform.position + pointer.transform.forward * distance;
            p2.y = p0.y - dropHeight;

            Vector3 _b012 = p0;

            for (int i = 0; i < positionCount; i++)
            {
                float amp = ((float) i / (float) (positionCount - 1));
                Vector3 b01 = Vector3.Lerp(p0, p1, amp);
                Vector3 b12 = Vector3.Lerp(p1, p2, amp);

                Vector3 b012 = Vector3.Lerp(b01, b12, amp);

                if (Physics.Linecast(_b012, b012, out _hit))
                {
                    _teleportPoint = _hit.point;
                    for (int i2 = i; i2 < positionCount; i2++)
                    {
                        _lRend.SetPosition(i2, _teleportPoint);
                    }

                    break;
                }
                else
                {
                    _lRend.SetPosition(i, b012);
                    _b012 = b012;
                }
            }
        }

        private bool TryTeleport(Transform currentPoint)
        {
            if (_hit.collider.gameObject.layer != LayerMask.NameToLayer("TeleportLayer")) return false;
            var to = new Vector3(_teleportPoint.x, currentPoint.position.y, _teleportPoint.z);
            _ovrPlayerController.enabled = false;
            currentPoint.position = to;
            _ovrPlayerController.enabled = true;
            //await Teleport(currentPoint, to, .2f);
            return true;
        }


        private async UniTask Teleport(Transform current, Vector3 targetPos, float fadeTime)
        {
            var prevFadeTime = _fade.fadeTime;
            _fade.fadeTime = fadeTime;
            await _fade.Fade(0, 1f);
            current.position = targetPos;
            await _fade.Fade(1f, 0);
            _fade.fadeTime = prevFadeTime;
        }
    }
}
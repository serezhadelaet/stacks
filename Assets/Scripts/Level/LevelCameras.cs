using Cinemachine;
using UnityEngine;

namespace Level
{
    public class LevelCameras : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera followCamera;
        [SerializeField] private CinemachineVirtualCamera finishCamera;

        public void BindCamerasTo(Transform follow, Transform lookAt)
        {
            BindCamera(followCamera, follow, lookAt);
            BindCamera(finishCamera, follow, lookAt);
        }

        public void EnableFinishCamera()
        {
            finishCamera.enabled = true;
        }

        private void BindCamera(ICinemachineCamera cam, Transform follow, Transform lookAt)
        {
            cam.Follow = follow;
            cam.LookAt = lookAt;
        }
    }
}
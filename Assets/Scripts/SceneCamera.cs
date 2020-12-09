using Cinemachine;
using UnityEngine;

public class SceneCamera : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtualCamera.Follow = Player.Instance.transform;
    }

}

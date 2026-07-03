using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;
public class CharacterInitialize : NetworkBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private AudioListener _audio;
    public override void OnNetworkSpawn ()
    {
        if (!IsOwner)
        {
            _camera.enabled = false;
            _audio.enabled = false;
            _camera.gameObject.SetActive(false);
        }
    }
}

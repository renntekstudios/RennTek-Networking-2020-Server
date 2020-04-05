using System.Collections;

using UnityEngine.SceneManagement;
using UnityEngine;

using RennTekNetworking.Client.Clients;
using RennTekNetworking.Client.Packet.Sendable;

namespace RennTekNetworking.Client.Public.Managers
{
    public class r_SceneManager : MonoBehaviour
    {
        public static r_SceneManager instance;

        private void Awake()
        {
            if (instance)
                Destroy(instance.gameObject);
            instance = this;
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void LoadSceneAsync(string _sceneName)
        {
            StartCoroutine(Load(_sceneName));
        }

        private IEnumerator Load(string _sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName);

            while (!asyncLoad.isDone)
                yield return null;

            r_SendRequestsPacket.SendRequestServerData();
            r_SendPlayerPacket.SendNetworkName(PlayerPrefs.GetString("_Username"));

            //When done then spawn player
            r_SendInstantiationPacket.SendInstantiateLocalPlayer();
        }
    }
}

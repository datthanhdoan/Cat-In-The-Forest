// using UnityEngine;

// /// <summary>
// /// Dùng huy hiệu chân mèo để mở map mới
// /// </summary>
// public class T_UnlockMap : Task
// {
//     [SerializeField] int _coinRequire;
//     [Tooltip("Level to unlock - R_Region")]
//     [SerializeField] int unlockLevel;

//     protected new void Start()
//     {
//         base.Start();
//         string t1 = "Locked";
//         string t2 = _coinRequire.ToString();
//         UpdateTaskContent(t1, t2);
//     }

//     protected override void CheckTask()
//     {
//         Debug.Log("Call Check Task");
//         if (_resource.coin >= _coinRequire)
//         {
//             // _button.interactable = true;
//             if (!_map.CheckMaxLevel())
//             {
//                 Debug.Log("Unlock map");
//                 int coinAfter = _resource.coin - _coinRequire;
//                 _resource.SetCoin(coinAfter);

//                 _map.UpdateLevel();

//                 // update map
//                 _map.region.transform.GetChild(unlockLevel - 1).gameObject.SetActive(true);
//                 MapManager.instance.UpdateNavMesh();
//                 gameObject.SetActive(false);
//             }
//         }
//     }

// }
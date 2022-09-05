// using System.Collections;
// using System.Collections.Generic;
// using Core.Common;
// using UnityEngine;
//
//
// namespace DataAccount
// {
//
//
//     public class InHouse
//     {
//       public List<GameObject> ItemDecorInHouse =new List<GameObject>();
//       public List<GameObject> BtnDecorInHouse =new List<GameObject>();
//
//       public void SetItemDecor(GameObject obj)
//       {
//           ItemDecorInHouse.Add(obj);
//           DataAccountPlayer.SaveInHouse();
//           GameManager.Instance.PostEvent(EventID.InHouse);
//       }
//
//       public void SetBtnDecor(GameObject obj)
//       {
//           BtnDecorInHouse.Add(obj);
//           DataAccountPlayer.SaveInHouse();
//           GameManager.Instance.PostEvent(EventID.InHouse);
//       }
//       
//      
//     }
// }

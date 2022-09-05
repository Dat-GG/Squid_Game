using System.Collections.Generic;
using Challenger3.Gameplay.Managers;
using Challenger3.Marble;
using Challenger3.SpineAnimation;
using Core.Common;
using Spine;
using Spine.Unity;
using TMPro;
using UnityEngine;

namespace Challenger3.Enemy
{
    public class EnemyController3 : MonoBehaviour
    {
        // private Indicator indicator;
        [HideInInspector] public GameObject spawnMarbleEnemy;
        [SerializeField] private GameObject enemyMarble;
        [SerializeField] private Transform spawnPos;
        [SerializeField] private TMP_Text nameEnemy;
        [SerializeField] private SkeletonAnimation skeletonAnimation;
        [SerializeField] private float speedMarble;
        [SerializeField] private Spine3 enemySpine;
        internal Spine3 EnemySpine => enemySpine;

        internal bool EndThrow;
        private GameManagerChallenger3 _gameManagerChallenger3;

        private void Start()
        {
            _gameManagerChallenger3 = FindObjectOfType<GameManagerChallenger3>();
            // indicator = FindObjectOfType<Indicator>();
            nameEnemy.text = _gameManagerChallenger3.nameEnemyNumber.ToString();
            SetRandomSkinForBot();
        }
    
        public void EnemyThrowSecond()
        {
            if (!EndThrow)
            {
                enemySpine.PlayThrowStart();

                var randomX = Random.Range(-200f, -150f);
                var direction = new Vector2(randomX, 100);
                // Debug.Log(direction);
                speedMarble = Random.Range(8, 13);
                // Debug.Log(speedMarble);
                
                this.StartDelayMethod(1f, delegate
                {
                    enemySpine.PlayThrowEnd();
                    
                    spawnMarbleEnemy = Instantiate(enemyMarble, spawnPos.transform.position, Quaternion.identity);
                    var marbleRig = spawnMarbleEnemy.GetComponent<Rigidbody2D>();
                    marbleRig.AddForce(direction * speedMarble);
                });
                
                this.StartDelayMethod(1.5f, () => { EndThrow = true; });
            }
        }

        internal void EnemyThrowFirst()
        {
            enemySpine.PlayThrowStart();

            var randomX = Random.Range(-200f, -150f);
            var direction = new Vector2(randomX, 100);
            // Debug.Log(direction);
            speedMarble = Random.Range(8, 13);
            // Debug.Log(speedMarble);
                
            this.StartDelayMethod(1f, delegate
            {
                enemySpine.PlayThrowEnd();
                    
                spawnMarbleEnemy = Instantiate(enemyMarble, spawnPos.transform.position, Quaternion.identity);
                spawnMarbleEnemy.GetComponent<EnemyMarble>().enabled = false;
                var marbleRig = spawnMarbleEnemy.GetComponent<Rigidbody2D>();
                marbleRig.AddForce(direction * speedMarble);
            });
                
            this.StartDelayMethod(6f, () =>
            {
                _gameManagerChallenger3.SetPlayerTurnSecond();
            });
        }
    
        private void SetRandomSkinForBot()
        {
            var listSkins = skeletonAnimation.skeletonDataAsset.GetSkeletonData(true).Skins.Items;
            List<Skin> skins = new List<Skin>(listSkins);
           
            for (var i = skins.Count - 1; i >= 0; i--)
            {
                var skin = skins[i];
                var isCanNotUse = enemySpine.IsCanNotUseSkin(skin.Name);
                if (isCanNotUse)
                    skins.RemoveAt(i);
            }

            var rdIndex = Random.Range(0, skins.Count);
            var skinName = skins[rdIndex].Name;
            
            skeletonAnimation.Skeleton.SetSkin(skinName);
            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
            skeletonAnimation.AnimationState.Apply(skeletonAnimation.Skeleton);
        }
    }
}
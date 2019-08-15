using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amaze
{

    public class LockCamera : MonoBehaviour
    {
        public BattleManager bm;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.position = bm.obj.transform.position + new Vector3(0, 300, 0);
        }
    }
}
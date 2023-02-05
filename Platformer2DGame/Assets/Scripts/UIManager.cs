using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class UIManager : MonoBehaviour
    {

        public Text coinText;
        public Text KeyText;
        public int count;

        public void CoinUI( int count)
        {
            
            coinText.text = count.ToString(); 
        }

        public void KeyUI(int count)
        {

            KeyText.text = count.ToString();
        }

    }
}

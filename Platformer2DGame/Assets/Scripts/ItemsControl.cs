using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public enum Items
    {
        Coin,
        Health,
        Diamond,
    }

    public class ItemsControl : MonoBehaviour
    {
        public static ItemsControl items;
        public Items Type;
        public int Amount;
    }
}

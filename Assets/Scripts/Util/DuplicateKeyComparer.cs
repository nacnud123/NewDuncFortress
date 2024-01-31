using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DuplicateKeyComparer<TKey>
                :
             IComparer<TKey> where TKey : IComparable
{
    #region IComparer<TKey> Members

    private int equalReturn;

    public DuplicateKeyComparer(bool equalValueAtEnd = false)
    {
        this.equalReturn = equalValueAtEnd ? -1 : 1;
    }

    public int Compare(TKey x, TKey y)
    {
        int result = x.CompareTo(y);

        if (result == 0)
        {
            return equalReturn; // Handle equality as beeing greater
        }
        else
        {
            return result;
        }
    }

    #endregion
}

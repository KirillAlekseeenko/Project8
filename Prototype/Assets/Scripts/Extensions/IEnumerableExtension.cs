using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class IEnumerableExtension {
    
	public static T Min<T, TComparable>(this IEnumerable<T> sequence, System.Func<T, TComparable> selector) 
		where TComparable : System.IComparable
    {
		var minElement = sequence.First();
		var min = selector(minElement);
		foreach(var element in sequence)
		{
			var comparable = selector(element);
			if(min.CompareTo(selector(element)) < 0)
			{
				min = comparable;
				minElement = element;
			}
		}
		return minElement;
    }
	
}

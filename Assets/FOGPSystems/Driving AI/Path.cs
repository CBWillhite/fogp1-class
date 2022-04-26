using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Driving.path
{
	public class Path : MonoBehaviour
	{
		public Color acolor;

		private List<Transform> nodes = new List<Transform>();

		void OnDrawGizmos()
		{
			Gizmos.color = acolor;

			Transform[] pathCreate = GetComponentsInChildren<Transform>();
			nodes = new List<Transform>();

			for (int i = 0; i < pathCreate.Length; i++)
			{
				if (pathCreate[i] != transform)
				{
					nodes.Add(pathCreate[i]);
				}
			}

			for (int i = 0; i < nodes.Count; i++)
			{
				Vector3 thisNode = nodes[i].position;
				Vector3 prevNode = nodes[0].position;

				if (i > 0)
				{
					prevNode = nodes[i - 1].position;
				}
				else if (i == 0 && nodes.Count > 1)
				{
					prevNode = nodes[nodes.Count - 1].position;
				}

				Gizmos.DrawLine(prevNode, thisNode);
				Gizmos.DrawWireSphere(thisNode, 0.3f);
			}
		}
	}
}
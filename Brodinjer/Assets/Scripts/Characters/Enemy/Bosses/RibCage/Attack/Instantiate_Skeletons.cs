using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Instantiate_Skeletons : MonoBehaviour
{
    public GameObject SkeletonPrefab;
    public List<Transform> InstantiateSpots;
    public int numSkeletons;
    public IntData totalSkeletons;

    private List<Transform> spots;
    private int index;

    public void InstantiateSkeletons()
    {
        if (totalSkeletons.value < numSkeletons)
        {
            spots = InstantiateSpots;
            for (int i = 0; i < numSkeletons - totalSkeletons.value; i++)
            {
                index = Random.Range(0, spots.Count);
                Instantiate(SkeletonPrefab, spots[index].position, SkeletonPrefab.transform.rotation).SetActive(true);
                spots.RemoveAt(index);
                if (spots.Count == 0)
                    spots = InstantiateSpots;
            }

            totalSkeletons.value = numSkeletons;
        }
    }

}

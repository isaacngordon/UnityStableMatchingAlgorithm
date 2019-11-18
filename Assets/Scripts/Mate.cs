using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mate : MonoBehaviour
{

    GameObject[] potentialPartners;
    int[] potentialPartnerRatings;
    int numPotentials;
    GameObject partner;
    bool hasPartner;
    int index;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rate(GameObject[] partners)
    {
        potentialPartners = partners;
        numPotentials = partners.Length;
        index = 0;

        potentialPartnerRatings = new int[numPotentials];
        for(int i = 0; i < numPotentials; i++)
        {
            potentialPartnerRatings[i] = RatePartner(potentialPartners[i]);
        }

        SortPartners();
    }

    //sort partners 
    void SortPartners()
    {
        
        MergeSort(potentialPartners, potentialPartnerRatings, 0, numPotentials - 1);
        

        void MergeSort(GameObject[] objArr, int[] rateArry, int left, int right)
        {
           
            if (left < right)
            {
                int m = (left + (right - 1) ) / 2;
                MergeSort(objArr, rateArry, left, m);
                MergeSort(objArr, rateArry, m + 1, right);
                Merge(objArr, rateArry, left, m, right);

            }

        }//MergeSort

        void Merge(GameObject[] objArr, int[] rateArr, int left, int middle, int right)
        {
           
            int i, j, k;
            int n1 = middle - left + 1;
            int n2 = right - middle;

            //temp arrays
            GameObject[] leftObj = new GameObject[n1];
            GameObject[] rightObj = new GameObject[n2];
            int[] leftRate = new int[n1];
            int[] rightRate = new int[n2];

            //fill left temp arrays
            for (i = 0; i < n1; i++)
            {
                leftObj[i] = objArr[left + i];
                leftRate[i] = rateArr[left + i];

            }

            //fill right temp arrays
            for (j = 0; j < n2; j++)
            {
                rightObj[j] = objArr[middle + 1 + j];
                rightRate[j] = rateArr[middle + 1 + j];
            }

            i = 0;      // Initial index of first subarray 
            j = 0;      // Initial index of second subarray 
            k = left;   // Initial index of merged subarray

            //Merge
            while( i < n1 && j < n2)
            {
                if(leftRate[i] <= rightRate[j])
                {
                    rateArr[k] = leftRate[i];
                    objArr[k] = leftObj[i];
                    i++;
                } else
                {
                    rateArr[k] = rightRate[j];
                    objArr[k] = rightObj[j];
                    j++;
                }
                k++;
            }
            //Copy remaining elements
            while(i < n1)
            {
                rateArr[k] = leftRate[i];
                objArr[k] = leftObj[i];
                i++;
                k++;
            }
            while(j < n1)
            {
                rateArr[k] = rightRate[j];
                objArr[k] = rightObj[j];
                j++;
                k++;
            }
        }//Merge
        
    }


    void Swap(int i, int j)
    {
        int tempVal = potentialPartnerRatings[i];
        GameObject tempObj = potentialPartners[i];

        potentialPartnerRatings[i] = potentialPartnerRatings[j];
        potentialPartners[i] = potentialPartners[j];

        potentialPartnerRatings[j] = tempVal;
        potentialPartners[j] = tempObj;

    }

    //TODO: Rates a partner based on prefferred genes.... until then random ratings
    int RatePartner(GameObject potential)
    {
        return Random.Range(0, 101);
    }
}

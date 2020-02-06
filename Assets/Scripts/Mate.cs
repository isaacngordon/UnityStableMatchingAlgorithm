using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mate : MonoBehaviour
{

    GameObject[] potentialPartners;
    int[] potentialPartnerRatings;
    int numPotentials;
    GameObject partner;
    Vector3 ip;

    bool hasPartner = false;
    int index = 0;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInitialPosition(Vector3 initial)
    {
        ip = initial;
    }

    public Vector3 GetInitialPosition()
    {
        return ip;
    }

    public void SetListOfPartners(GameObject[] arr)
    {
        potentialPartners = arr; 
    }

    public GameObject GetPartner()
    {
        return partner;
    }

    public void Rate(GameObject[] partners)
    {
        potentialPartners = (UnityEngine.GameObject[])partners.Clone();
        numPotentials = partners.Length;
        index = 0;

        potentialPartnerRatings = new int[numPotentials];
        for(int i = 0; i < numPotentials; i++)
        {
            potentialPartnerRatings[i] = RatePartner(potentialPartners[i]);
        }


        SortPartners();
    }

    //sort partners by rating
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
            //Debug.Log("Partner: " + potentialPartners.ToString() + " Ratings: " + potentialPartnerRatings.ToString());

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

    //TODO: Rates a partner based on preferred genes.... until then random ratings
    int RatePartner(GameObject potential)
    {
        return Random.Range(0, 101);
    }

    public bool FindSomePartner()
    {
        if (hasPartner) return true;

        //if (potentialPartners[1] == null)
        //{
        //    throw new System.Exception("ooooweeeee");
        //}

        //get next potential partner
        //move to her
        //propose to her
        //if she says yes, both move to center-ish
        //else increment index and return false;

        //get next potential
        GameObject potential = potentialPartners[index];

        //move near to potential
        Vector3 fp = potential.transform.position;
        Vector3 newPos = (-fp.normalized) + fp;
        this.transform.position = newPos;

        //propose
        bool success = potential.GetComponent<Mate>().InspectProposal(gameObject);
        if (success)
        {
            partner = potential;
            hasPartner = true;

            ////Update positions of this and partner
            //Vector3 fp = partner.transform.position;
            //Vector3 newPos = (-fp.normalized) + fp;
            //this.transform.position = newPos;
        }
        else
        {
            hasPartner = false;
            index++;

            //move this back to its original position
            transform.position = ip;
        }

        return success;
    }

    public bool InspectProposal(GameObject proposer)
    {

        //if(potentialPartners[1] == null)
        //{
        //    throw new System.Exception();
        //}

        // if this mate has a partner
        if (hasPartner)
        {
            // see if the proposer precedes the currentPartner in potentialPartners
            // since array is sorted by rating, we can iterate until current partner's index
            for (int k = 0; k < index; k++)
            {
                //if proposer has been found, the proposer is preferred to the cuttent partner
                if (proposer == potentialPartners[k])
                {
                    // success

                    // ditch current partner, send him pack to his initial position
                    partner.GetComponent<Mate>().hasPartner = false;
                    partner.GetComponent<Mate>().index++;
                    partner.transform.position = partner.GetComponent<Mate>().GetInitialPosition();

                    //take new partner
                    index = k;
                    hasPartner = true;
                    partner = proposer;
                    return true;
                }
            } 
        }

        else
        {
            //else no partner, accept
            for (int k = 0; k < potentialPartners.Length; k++)
            {
                if (proposer == potentialPartners[k])
                {
                    index = k;
                    break;
                }
            }
            hasPartner = true;
            partner = proposer;
            return true;
        }

        //otherwise return false
        return false;
    }

   


}

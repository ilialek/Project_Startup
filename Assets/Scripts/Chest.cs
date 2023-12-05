using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject upperPart;
    public GameObject lowerPart;

    [System.Serializable]
    public class LootItem
    {
        public GameObject itemPrefab;
        public float rarity; 
    }

    public LootItem[] lootPool;

    private bool isOpened = false;

    public bool IsOpened
    {
        get { return isOpened; }
    }

    public void OpenChest()
    {
        isOpened = true;
        PlayChestOpeningAnimation();
        SpawnLoot();
    }

    void PlayChestOpeningAnimation()
    {
        upperPart.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        upperPart.transform.localPosition = new Vector3(0, 1, -0.7f);
    }

    void SpawnLoot()
    {      
        int numberOfItemsToSpawn = 2;

        for (int i = 0; i < numberOfItemsToSpawn; i++)
        {
            float randomValue = Random.value;

            float totalRarity = 0f;
            foreach (LootItem lootItem in lootPool)
            {
                totalRarity += lootItem.rarity;
            }

            randomValue *= totalRarity;

            foreach (LootItem lootItem in lootPool)
            {
                if (randomValue < lootItem.rarity)
                {
                    Vector3 spawnPosition = transform.position + Vector3.up; 
                    Instantiate(lootItem.itemPrefab, spawnPosition, Quaternion.identity);
                    break; 
                }

                randomValue -= lootItem.rarity;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crafter : ItemDropper, IInteractable
{
    [Space]
    [SerializeField] private List<RecipeSO> recipes;

    private List<ItemSO> putItems = new List<ItemSO>(3);

    public void OnInteraction(GameObject interactObject)
    {
        Player player = interactObject.GetComponent<Player>();

        if (player == null) return;

        ItemSO item = player.SelectedItem;

        putItems.Add(item);
        player.FeedItem();

        if(putItems.Count == 3) 
        {
            RecipeSO recipe = null;

            foreach(var recipeSO in recipes)
            {
                List<bool> isContains = new List<bool>();

                foreach(var putItem in putItems)
                {
                    if(recipeSO.Recipe.Contains(putItem))
                    {
                        isContains.Add(true);
                    }
                    else
                    {
                        isContains.Add(false);
                        break;
                    }
                }

                if(!isContains.Contains(false) && isContains.Count == 3)
                {
                    recipe = recipeSO;
                    break;
                }

                isContains = new List<bool>();
            }

            if(recipe == null)
            {
                foreach(var putItem in putItems)
                {
                    SpawnItem(putItem, 1);
                }
                //putItems.Clear();
                putItems = new List<ItemSO>();
                return;
            }

            SpawnItem(recipe.Result, 1);
        }
    }
}

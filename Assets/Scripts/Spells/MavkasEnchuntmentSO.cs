using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="MavkasEnchuntmentLogic", menuName ="Spells/Spells Logic/Mavkas Enchantment Logic")]
public class MavkasEnchuntmentSO : SpellLogicSOBase
{
    private Player player;

    [Space]
    [SerializeField] private List<NPCDescriptionSO> affectedNPC;

    [Space]
    [SerializeField] private float freezeTime = 1.5f;

    public override void Activate(Player player)
    {
        this.player = player;

        Debug.Log($"Freezing NPCs {player.name}");

        FreezeEnemies();
    }

    private void FreezeEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, player.NoticeRadius.radius);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Damageable"))
            {
                // Checking if this is an befriended NPC
                BefriendableNPC npc = collider.GetComponentInParent<BefriendableNPC>();

                if (npc != null)
                {
                    if (player.Animals.Animals.Contains(npc))
                    {
                        continue;
                    }
                }

                NPCBase npcBase = collider.GetComponentInParent<NPCBase>();

                if (npcBase != null)
                {
                    if (affectedNPC.Contains(npcBase.DescriptionSO))
                    {
                        Debug.Log($"Freeze NPC {npcBase.name}");
                        npcBase.Freeze(freezeTime);
                    }
                }
            }
        }
    }
}

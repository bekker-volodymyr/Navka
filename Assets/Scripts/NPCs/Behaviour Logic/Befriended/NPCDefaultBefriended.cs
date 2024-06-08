using UnityEngine;

[CreateAssetMenu(fileName ="Befriended-DefaultBefriended", menuName ="NPC Logic/Befriended Logic/Default Befriended")]
public class NPCDefaultBefriended : NPCBefriendedSOBase
{
    private float followSpeed = 2.0f;
    private float stoppingDistance = 1.5f;
    private float wanderRadius = 3f;
    private float wanderTimer = 2f;
    private float timer;

    public override void Initialize(BefriendableNPC npc)
    {
        base.Initialize(npc);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        timer = wanderTimer;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Vector2 direction = player.transform.position - npc.transform.position;
        float distance = direction.magnitude;

        if (distance > stoppingDistance)
        {
            direction.Normalize();
            Vector2 velocity = direction * followSpeed;
            npc.Move(velocity);
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomCircle(player.transform.position, wanderRadius);
                Vector3 velocity = (newPos - npc.transform.position).normalized * followSpeed;
                npc.Move(velocity);
                timer = 0;
            }
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    public override void DoAnimationTriggerEventLogic(NPCBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    Vector2 RandomCircle(Vector2 center, float radius)
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float x = center.x + radius * Mathf.Cos(angle);
        float y = center.y + radius * Mathf.Sin(angle);
        return new Vector2(x, y);
    }
}

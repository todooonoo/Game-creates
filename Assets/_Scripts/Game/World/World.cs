using UnityEngine;

public enum WorldType
{
    World3D = 0,
    WorldRight2D = 1,
    WorldUp2D = 2,
    WorldFront2D = 3,
}

public class World : MonoBehaviour {

    public WorldType worldType;
    private Player player;
    private GameCamera gameCamera;
    private WorldObjectClone[] clones;
    protected Checkpoint[] checkpoints;

    private static readonly string combineSfxName = "Combine";

    public virtual void InitWorld()
    {
    }

    public GameCamera GameCamera
    {
        get
        {
            if (!gameCamera)
                gameCamera = GetComponentInChildren<GameCamera>(true);
            return gameCamera;
        }
    }

    public Player Player
    {
        get
        {
            if (!player)
                player = GetComponentInChildren<Player>(true);
            return player;
        }
    }
    
    public void TransitionIn()
    {
        if (clones == null)
            clones = GetComponentsInChildren<WorldObjectClone>(true);

        for(int i = 0; i < clones.Length; i++)
        {
            clones[i].SetCurrentPosition(this);
        }
    }

    public void TransitionOut()
    {
        if (clones == null)
            clones = GetComponentsInChildren<WorldObjectClone>(true);
        
        for (int i = 0; i < clones.Length; i++)
        {
            clones[i].SetOriginPosition(this);
        }
        TransitionScreen.Instance.interactIcon.SetActive(false);
    }

    public void CheckPlayerCollision()
    {
        var p2 = Player.GetComponent<Player2D>();
        if (worldType != WorldType.World3D)
        {
            var col = p2.CheckCollision();

            if(col)
            {
                var combinable = col.GetComponentInParent<Combinable>();

                if(combinable)
                {
                    AudioManager.Instance.PlaySFX(combineSfxName);
                    p2.SetCombinable(combinable);
                }
            } else
            {
                p2.SetCombinable(null);
            }
        }
    }

    public void SetPlayerCollision(bool active)
    {
        Player.SetCollision(active);
    }

    public Vector3 ClosestCheckpoint(Vector3 pos)
    {
        if(checkpoints == null)
            checkpoints = GetComponentsInChildren<Checkpoint>(true);

        var targetPos = pos;
        if(checkpoints.Length > 0)
        {
            targetPos = checkpoints[0].RespawnPos;
            var sqrDist = Vector3.SqrMagnitude(targetPos - pos);

            for(int i = 1; i < checkpoints.Length; i++)
            {
                var newPos = checkpoints[i].RespawnPos;
                if (Vector3.SqrMagnitude(newPos - pos) < sqrDist)
                    targetPos = newPos;
            }
        }
        return targetPos;
    }

    public virtual Vector3 GetCurrentWorldPos(Vector3 pos)
    {
        return pos;
    }

    public virtual Vector3 GetOriginWorldPos(Vector3 currentPos, Vector3 originPos)
    {
        return currentPos;
    }
}

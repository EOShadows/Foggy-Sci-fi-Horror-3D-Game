using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLimbs : MonoBehaviour
{
    private Dictionary<string, EnemyLimb> limbs;
    private bool limbs_set = false;

    struct limb_hit_info
    {
        public EnemyLimb limb;
        public Vector3 hit_point;

        public limb_hit_info(EnemyLimb limb, Vector3 hit_point)
        {
            this.limb = limb;
            this.hit_point = hit_point;
        }
    };

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (!limbs_set)
        {
            GetLimbs();
        }
    }

    private void GetLimbs()
    {
        limbs = new Dictionary<string, EnemyLimb>();

        foreach (var limb in GetComponentsInChildren<EnemyLimb>())
        {
            limbs.Add(limb.gameObject.name, limb);
        }

        limbs_set = true;
    }

    public void AttachToLimb(Transform attachable, Vector3 at)
    {
        var limb_info = GetClosestLimb(attachable, at);

        if (!IsNull(limb_info))
        {
            limb_info.limb.Attach(attachable, limb_info.hit_point);
        }
        else
        {
            Debug.LogWarning("EnemyLimbs: AttachToLimb(): Could not get limb");
        }
    }

    private limb_hit_info GetClosestLimb(Transform attachable, Vector3 point)
    {
        Ray ray = new Ray(attachable.position, attachable.forward);

        float bestDist = float.PositiveInfinity;
        EnemyLimb bestLimb = null;

        foreach (var pair in limbs)
        {
            float dist = Vector3.Distance(pair.Value.transform.position, point);

            if(bestDist > dist)
            {
                bestDist = dist;
                bestLimb = pair.Value;
            }
        }

        Vector3 pos = Vector3.zero;

        if (bestLimb)
        {
            var collider = bestLimb.GetComponent<Collider>();

            if (collider)
            {
                pos = collider.ClosestPoint(point);
            }
        }

        return new limb_hit_info(bestLimb, pos);
    }

    private limb_hit_info NullLimbInfo()
    {
        return new limb_hit_info(null, Vector3.zero);
    }

    private bool IsNull(limb_hit_info lhi)
    {
        return lhi.limb == null;
    }
}

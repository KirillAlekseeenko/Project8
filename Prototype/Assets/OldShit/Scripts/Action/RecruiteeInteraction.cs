using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class RecruiteeInteraction : Interaction
{
    const float distanceToRecruiter = 1f;

    NavMeshAgent navMeshAgent;
    Citizen citizenComponent;
    Recruiter recruiterComponent;

    public RecruiteeInteraction(Unit citizen, Unit recruiter)
    {
        actionOwner = citizen;
        actionReceiver = recruiter;
        navMeshAgent = citizen.GetComponent<NavMeshAgent>();
        citizenComponent = citizen.GetComponent<Citizen>();
        recruiterComponent = recruiter.GetComponent<Recruiter>();
    }

    public override ActionState State
    {
        get
        {
            if(recruiterComponent == null || !recruiterComponent.IsRecruiting)
            {
                navMeshAgent.ResetPath();
                return new ActionState(true, -1);
            }
            if(Vector3.Distance(actionOwner.transform.position, actionReceiver.transform.position) <= distanceToRecruiter)
            {
                if(navMeshAgent.hasPath)
                {
                    navMeshAgent.ResetPath();
                    citizenComponent.StartClapping();
                }
            }
            else
            {
                if (!navMeshAgent.hasPath)
                {
                    citizenComponent.StopClapping();
                    navMeshAgent.SetDestination(actionReceiver.transform.position);
                }

            }
            return new ActionState(false, -1);
        }
    }

    public override void Finish()
    {
        citizenComponent.StopClapping();
    }

    public override void Perform()
    {
        navMeshAgent.SetDestination(actionReceiver.transform.position);
    }
}

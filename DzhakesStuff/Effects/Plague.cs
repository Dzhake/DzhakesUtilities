﻿using Light2D;
using RogueLibsCore;
using UnityEngine;

namespace DzhakesStuff
{
    [EffectParameters(EffectLimitations.None)]
    public class Plague : CustomEffect
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomEffect<Plague>()
                .WithName(new CustomNameInfo("Plague"))
                .WithDescription(new CustomNameInfo("Slowly lose health and infect nearby people."));
        }

        public override int GetEffectTime() => 9999;
        public override int GetEffectHate() => 0;
        public override void OnAdded() {}
        public override void OnRemoved() {}
        public override void OnUpdated(EffectUpdatedArgs e)
        {
            e.UpdateDelay = 1f;
            if (!Owner.dead)
                Owner.ChangeHealth(-3f);

            Vector2 ownerPos = Owner.curPosition;

            foreach (Agent agent in gc.agentList)
            {
                if (Vector2.Distance(agent.curPosition,ownerPos) < 5f * 0.64f && agent.movement.HasLOSPosition360(ownerPos) && (PlagueEverywhere.Timer <= 0 || agent.isPlayer != 0))
                {
                    agent.AddEffect<Plague>();
                }
            }
        }
    }
}

using System;

namespace Lundgren.Game
{
    public static class GameData
    {
        private static readonly string[] _stageIds =
        {
            "Princess Peach's Castle",
            "Rainbow Cruise",
            "Kongo Jungle",
            "Jungle Japes",
            "Termina Great Bay",
            "Hyrule Temple",
            "Yoshi's Story",
            "Yoshi's Island",
            "Fountain of Dreams",
            "Green Greens",
            "Corneria",
            "Venom",
            "Brinstar",
            "Brinstar Depths",
            "Onett",
            "Fourside",
            "Mute City",
            "Big Blue",
            "Pokemon Stadium",
            "Poke Floats",
            "Mushroom Kingdom",
            "Mushroom Kingdom 2",
            "Icicle Mountain",
            "Flat Zone",
            "Battlefield",
            "Final Destination",
            "Dream Land",
            "Yoshi's Island - Past",
            "Kongo Jungle - Past",
            "Random",
            "??"

        };

        // https://code.google.com/p/vgce/wiki/ssbmCharID
        private static readonly string[] _externalCharIds =
        {
            "Captain Falcon",
            "Donkey Kong",
            "Fox",
            "Mr. Game & Watch",
            "Kirby",
            "Bowser",
            "Link",
            "Luigi",
            "Mario",
            "Marth",
            "Mewtwo",
            "Ness",
            "Peach",
            "Pikachu",
            "Ice Climbers",
            "Jigglypuff",
            "Samus",
            "Yoshi",
            "Zelda",
            "Sheik",
            "Falco",
            "Young Link",
            "Dr. Mario",
            "Roy",
            "Pichu",
            "Ganondorf",
            "Master Hand",
            "Wireframe Male (Boy)",
            "Wireframe Female (Girl)",
            "Giga Bowser",
            "Crazy Hand",
            "Sandbag",
            "Popo",
            "None"
        };

        private static readonly string[] _interalCharIds =
        {
            "Donkey Kong",
            "Kirby",
            "Bowser",
            "Link",
            "Sheik",
            "Ness",
            "Peach",
            "Popo",
            "Nana",
            "Pikachu",
            "Samus",
            "Yoshi",
            "JigglyPuff",
            "Mewtwo",
            "Luigi",
            "Marth",
            "Zelda",
            "Young Link",
            "Dr. Mario",
            "Falco",
            "Pichu",
            "Mr. Game & Watch",
            "Ganondorf",
            "Roy",
            "Master Hand",
            "Crazy Hand",
            "Wireframe Male (Boy)",
            "Wireframe Female (Girl)",
            "Giga Bowser",
            "Sandbag"
        };

        private static readonly string[] _animationID =
        {
            "DeadDown",
            "DeadLeft",
            "DeadRight",
            "DeadUp",
            "DeadUpStar",
            "DeadUpStarIce",
            "DeadUpFall",
            "DeadUpFallHitCamera",
            "DeadUpFallHitCameraFlat",
            "DeadUpFallIce",
            "DeadUpFallHitCameraIce",
            "Sleep",
            "Rebirth",
            "RebirthWait",
            "Wait",
            "WalkSlow",
            "WalkMiddle",
            "WalkFast",
            "Turn",
            "TurnRun",
            "Dash",
            "Run",
            "RunDirect",
            "RunBrake",
            "KneeBend",
            "JumpF",
            "JumpB",
            "JumpAerialF",
            "JumpAerialB",
            "Fall",
            "FallF",
            "FallB",
            "FallAerial",
            "FallAerialF",
            "FallAerialB",
            "FallSpecial",
            "FallSpecialF",
            "FallSpecialB",
            "DamageFall",
            "Squat",
            "SquatWait",
            "SquatRv",
            "Landing",
            "LandingFallSpecial",
            "Attack11",
            "Attack12",
            "Attack13",
            "Attack100Start",
            "Attack100Loop",
            "Attack100End",
            "AttackDash",
            "AttackS3Hi",
            "AttackS3HiS",
            "AttackS3S",
            "AttackS3LwS",
            "AttackS3Lw",
            "AttackHi3",
            "AttackLw3",
            "AttackS4Hi",
            "AttackS4HiS",
            "AttackS4S",
            "AttackS4LwS",
            "AttackS4Lw",
            "AttackHi4",
            "AttackLw4",
            "AttackAirN",
            "AttackAirF",
            "AttackAirB",
            "AttackAirHi",
            "AttackAirLw",
            "LandingAirN",
            "LandingAirF",
            "LandingAirB",
            "LandingAirHi",
            "LandingAirLw",
            "DamageHi1",
            "DamageHi2",
            "DamageHi3",
            "DamageN1",
            "DamageN2",
            "DamageN3",
            "DamageLw1",
            "DamageLw2",
            "DamageLw3",
            "DamageAir1",
            "DamageAir2",
            "DamageAir3",
            "DamageFlyHi",
            "DamageFlyN",
            "DamageFlyLw",
            "DamageFlyTop",
            "DamageFlyRoll",
            "LightGet",
            "HeavyGet",
            "LightThrowF",
            "LightThrowB",
            "LightThrowHi",
            "LightThrowLw",
            "LightThrowDash",
            "LightThrowDrop",
            "LightThrowAirF",
            "LightThrowAirB",
            "LightThrowAirHi",
            "LightThrowAirLw",
            "HeavyThrowF",
            "HeavyThrowB",
            "HeavyThrowHi",
            "HeavyThrowLw",
            "LightThrowF4",
            "LightThrowB4",
            "LightThrowHi4",
            "LightThrowLw4",
            "LightThrowAirF4",
            "LightThrowAirB4",
            "LightThrowAirHi4",
            "LightThrowAirLw4",
            "HeavyThrowF4",
            "HeavyThrowB4",
            "HeavyThrowHi4",
            "HeavyThrowLw4",
            "SwordSwing1",
            "SwordSwing3",
            "SwordSwing4",
            "SwordSwingDash",
            "BatSwing1",
            "BatSwing3",
            "BatSwing4",
            "BatSwingDash",
            "ParasolSwing1",
            "ParasolSwing3",
            "ParasolSwing4",
            "ParasolSwingDash",
            "HarisenSwing1",
            "HarisenSwing3",
            "HarisenSwing4",
            "HarisenSwingDash",
            "StarRodSwing1",
            "StarRodSwing3",
            "StarRodSwing4",
            "StarRodSwingDash",
            "LipStickSwing1",
            "LipStickSwing3",
            "LipStickSwing4",
            "LipStickSwingDash",
            "ItemParasolOpen",
            "ItemParasolFall",
            "ItemParasolFallSpecial",
            "ItemParasolDamageFall",
            "LGunShoot",
            "LGunShootAir",
            "LGunShootEmpty",
            "LGunShootAirEmpty",
            "FireFlowerShoot",
            "FireFlowerShootAir",
            "ItemScrew",
            "ItemScrewAir",
            "DamageScrew",
            "DamageScrewAir",
            "ItemScopeStart",
            "ItemScopeRapid",
            "ItemScopeFire",
            "ItemScopeEnd",
            "ItemScopeAirStart",
            "ItemScopeAirRapid",
            "ItemScopeAirFire",
            "ItemScopeAirEnd",
            "ItemScopeStartEmpty",
            "ItemScopeRapidEmpty",
            "ItemScopeFireEmpty",
            "ItemScopeEndEmpty",
            "ItemScopeAirStartEmpty",
            "ItemScopeAirRapidEmpty",
            "ItemScopeAirFireEmpty",
            "ItemScopeAirEndEmpty",
            "LiftWait",
            "LiftWalk1",
            "LiftWalk2",
            "LiftTurn",
            "GuardOn",
            "Guard",
            "GuardOff",
            "GuardSetOff",
            "GuardReflect",
            "DownBoundU",
            "DownWaitU",
            "DownDamageU",
            "DownStandU",
            "DownAttackU",
            "DownFowardU",
            "DownBackU",
            "DownSpotU",
            "DownBoundD",
            "DownWaitD",
            "DownDamageD",
            "DownStandD",
            "DownAttackD",
            "DownFowardD",
            "DownBackD",
            "DownSpotD",
            "Passive",
            "PassiveStandF",
            "PassiveStandB",
            "PassiveWall",
            "PassiveWallJump",
            "PassiveCeil",
            "ShieldBreakFly",
            "ShieldBreakFall",
            "ShieldBreakDownU",
            "ShieldBreakDownD",
            "ShieldBreakStandU",
            "ShieldBreakStandD",
            "FuraFura",
            "Catch",
            "CatchPull",
            "CatchDash",
            "CatchDashPull",
            "CatchWait",
            "CatchAttack",
            "CatchCut",
            "ThrowF",
            "ThrowB",
            "ThrowHi",
            "ThrowLw",
            "CapturePulledHi",
            "CaptureWaitHi",
            "CaptureDamageHi",
            "CapturePulledLw",
            "CaptureWaitLw",
            "CaptureDamageLw",
            "CaptureCut",
            "CaptureJump",
            "CaptureNeck",
            "CaptureFoot",
            "EscapeF",
            "EscapeB",
            "Escape",
            "EscapeAir",
            "ReboundStop",
            "Rebound",
            "ThrownF",
            "ThrownB",
            "ThrownHi",
            "ThrownLw",
            "ThrownLwWomen",
            "Pass",
            "Ottotto",
            "OttottoWait",
            "FlyReflectWall",
            "FlyReflectCeil",
            "StopWall",
            "StopCeil",
            "MissFoot",
            "CliffCatch",
            "CliffWait",
            "CliffClimbSlow",
            "CliffClimbQuick",
            "CliffAttackSlow",
            "CliffAttackQuick",
            "CliffEscapeSlow",
            "CliffEscapeQuick",
            "CliffJumpSlow1",
            "CliffJumpSlow2",
            "CliffJumpQuick1",
            "CliffJumpQuick2",
            "AppealR",
            "AppealL",
            "ShoulderedWait",
            "ShoulderedWalkSlow",
            "ShoulderedWalkMiddle",
            "ShoulderedWalkFast",
            "ShoulderedTurn",
            "ThrownFF",
            "ThrownFB",
            "ThrownFHi",
            "ThrownFLw",
            "CaptureCaptain",
            "CaptureYoshi",
            "YoshiEgg",
            "CaptureKoopa",
            "CaptureDamageKoopa",
            "CaptureWaitKoopa",
            "ThrownKoopaF",
            "ThrownKoopaB",
            "CaptureKoopaAir",
            "CaptureDamageKoopaAir",
            "CaptureWaitKoopaAir",
            "ThrownKoopaAirF",
            "ThrownKoopaAirB",
            "CaptureKirby",
            "CaptureWaitKirby",
            "ThrownKirbyStar",
            "ThrownCopyStar",
            "ThrownKirby",
            "BarrelWait",
            "Bury",
            "BuryWait",
            "BuryJump",
            "DamageSong",
            "DamageSongWait",
            "DamageSongRv",
            "DamageBind",
            "CaptureMewtwo",
            "CaptureMewtwoAir",
            "ThrownMewtwo",
            "ThrownMewtwoAir",
            "WarpStarJump",
            "WarpStarFall",
            "HammerWait",
            "HammerWalk",
            "HammerTurn",
            "HammerKneeBend",
            "HammerFall",
            "HammerJump",
            "HammerLanding",
            "KinokoGiantStart",
            "KinokoGiantStartAir",
            "KinokoGiantEnd",
            "KinokoGiantEndAir",
            "KinokoSmallStart",
            "KinokoSmallStartAir",
            "KinokoSmallEnd",
            "KinokoSmallEndAir",
            "Entry",
            "EntryStart",
            "EntryEnd",
            "DamageIce",
            "DamageIceJump",
            "CaptureMasterhand",
            "CapturedamageMasterhand",
            "CapturewaitMasterhand",
            "ThrownMasterhand",
            "CaptureKirbyYoshi",
            "KirbyYoshiEgg",
            "CaptureLeadead",
            "CaptureLikelike",
            "DownReflect",
            "CaptureCrazyhand",
            "CapturedamageCrazyhand",
            "CapturewaitCrazyhand",
            "ThrownCrazyhand",
            "BarrelCannonWait",
            "Wait1",
            "Wait2",
            "Wait3",
            "Wait4",
            "WaitItem",
            "SquatWait1",
            "SquatWait2",
            "SquatWaitItem",
            "GuardDamage",
            "EscapeN",
            "AttackS4Hold",
            "HeavyWalk1",
            "HeavyWalk2",
            "ItemHammerWait",
            "ItemHammerMove",
            "ItemBlind",
            "DamageElec",
            "FuraSleepStart",
            "FuraSleepLoop",
            "FuraSleepEnd",
            "WallDamage",
            "CliffWait1",
            "CliffWait2",
            "SlipDown",
            "Slip",
            "SlipTurn",
            "SlipDash",
            "SlipWait",
            "SlipStand",
            "SlipAttack",
            "SlipEscapeF",
            "SlipEscapeB",
            "AppealS",
            "Zitabata",
            "CaptureKoopaHit",
            "ThrownKoopaEndF",
            "ThrownKoopaEndB",
            "CaptureKoopaAirHit",
            "ThrownKoopaAirEndF",
            "ThrownKoopaAirEndB",
            "ThrownKirbyDrinkSShot",
            "ThrownKirbySpitSShot"
        };

        public static string Stage(int stageNum)
        {
            return _stageIds[stageNum];
        }

        public static string ExternalChar(byte charNum)
        {
            if (charNum == 99)
                return "None";
            return _externalCharIds[charNum];
        }

        public static string Animation(int state)
        {
            return "??";
            /*
            if (state < 382 && state >= 0)
                return _animationID[state];
            else
                return "None";
                */
        }
    }
}
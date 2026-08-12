# MainGame - FuzzyGraph2 graph report

Source: `Assets/FuzzyGraph_2/Editor/Graph/MainGame.fuzzygraph2`
Exported: 2026-08-13 00:43:57

## Compile check

`[fuzzygraph2] compiled 'export check' | events 35 | variables 11 | expressions 79 | rules 54 | bands 52 | fallbacks 35 | consequences 26 | write-backs 55`

## Event overview

### Inspect (E01)

Rules:
- `R34` reveal_noticed_carpet | z=0.9 | root C35

Consequences:
- `O04` Vendor.NoValidOutput | min=0 | fallback=True
- `O02` Vendor.DefaultResponse | min=0 | fallback=False
- `O03` Vendor.Nervous | min=0.4 | fallback=False
- `O01` Vendor.RevealsSecret | min=0.7 | fallback=False
  - write-back `W01`

### AmbientSocialPressure (E02)

Rules:
- `R04` pressure_player_loitering | z=0.6 | root C04
- `R05` pressure_near_and_loitering | z=0.9 | root C05
- `R07` pressure_loitering_nervous_vendor | z=0.95 | root C07
- `R01` pressure_near | z=0.55 | root C03
- `R02` pressure_mid | z=0.35 | root C01
- `R03` pressure_far | z=0.15 | root C02

Consequences:
- `O05` SocialPressure.Calm | min=0 | fallback=False
- `O07` SocialPressure.Watched | min=0.6 | fallback=False
- `O08` SocialPressure.Intervention | min=0.72 | fallback=False
- `O06` SocialPressure.Uneasy | min=0.3 | fallback=False
- `O09` SocialPressure.NoContext | min=0.7 | fallback=True

### BazaarTalkToGuard (E03)

Rules:
- `R11` guard_notices_held_flowers | z=1 | root C13
- `R08` guard_first_conversation | z=0.1 | root C08
- `R09` guard_repeat_conversation | z=0.5 | root C49
- `R38` guard_dismisses_player | z=0.95 | root C68
- `R36` guard_repetitions_request | z=0.8 | root C45
- `R39` guard_allowed_entry_repeat | z=0.3 | root C50

Consequences:
- `O10` Guard.FirstConversation | min=0 | fallback=False
  - write-back `W02`
  - write-back `W03`
  - write-back `W46`
- `O15` Guard.NoticesFlowers | min=0.75 | fallback=False
- `O12` Guard.NoContext | min=0 | fallback=True
- `O63` Guard.AllowedRepeat | min=0.25 | fallback=False
- `O11` Guard.RepeatConversation | min=0.4 | fallback=False
  - write-back `W04`
- `O62` Guard.DismissesPlayer | min=0.8 | fallback=False
  - write-back `W45`
- `O59` Guard.RequestsItems | min=0.55 | fallback=False
  - write-back `W41`

### PickFlowers (E04)

Rules:
- `R10` pick_flowers | z=0.9 | root C10

Consequences:
- `O13` Player.PicksFlowers | min=0.5 | fallback=False
  - write-back `W05`
- `O14` Player.AlreadyHasFlowers | min=0.7 | fallback=True

### OfferFlowersToGuard (E05)

Rules:
- `R12` player_offers_flowers | z=0.9 | root C14

Consequences:
- `O16` Guard.FlowersOffered | min=0.5 | fallback=False
  - write-back `W06`
  - write-back `W07`
- `O17` Guard.CannotOfferFlowers | min=0.7 | fallback=True

### TeaseGuardAboutFlowers (E06)

Rules:
- `R13` player_teases_guard | z=0.9 | root C14

Consequences:
- `O18` Guard.TeasedAboutFlowers | min=0.5 | fallback=False
  - write-back `W08`
  - write-back `W09`
- `O19` Guard.CannotTeaseAboutFlowers | min=0.7 | fallback=True

### GiveFlowersToGuard (E07)

Rules:
- `R14` guard_accepts_flowers | z=0.9 | root C14

Consequences:
- `O20` Guard.AcceptsFlowers | min=0.5 | fallback=False
  - write-back `W10`
  - write-back `W11`
  - write-back `W12`
  - write-back `W15`
- `O21` Guard.CannotGiveFlowers  | min=0.7 | fallback=True

### RefuseFlowersToGuard (E08)

Rules:
- `R15` player_refuses_flowers | z=0.9 | root C14

Consequences:
- `O22` Guard.FlowersRefused | min=0.5 | fallback=False
  - write-back `W13`
- `O23` Guard.CannotRefuseFlowers | min=0.7 | fallback=True

### DelayFlowersToGuard (E09)

Rules:
- `R16` player_delays_flowers | z=0.9 | root C14

Consequences:
- `O24` Guard.FlowersDelayed | min=0.5 | fallback=False
  - write-back `W14`
- `O25` Guard.CannotDelayFlowers | min=0.7 | fallback=True

### BazaarTalkToVendor (E10)

Rules:
- `R17` vendor_first_meeting | z=0.1 | root C15
- `R18` vendor_returning | z=0.8 | root C16

Consequences:
- `O27` Vendor.FirstMeeting | min=0 | fallback=False
  - write-back `W16`
  - write-back `W17`
  - write-back `W32`
  - write-back `W33`
- `O26` Vendor.Returning | min=0.5 | fallback=False
  - write-back `W18`
- `O28` Vendor.NoValidDialogue | min=0 | fallback=True

### OfferVendorFlowers (E11)

Rules:
- `R19` vendor_flowers | z=0.5 | root C17

Consequences:
- `O29` Vendor.FlowersOffered | min=0 | fallback=False
  - write-back `W19`
  - write-back `W20`
- `O30` Vendor.FlowersNoContext | min=0 | fallback=True

### VendorAskHelp (E12)

Rules:
- `R20` vendor_help_request | z=0.5 | root C18

Consequences:
- `O31` Vendor.AgreesHelp | min=0 | fallback=False
  - write-back `W21`
  - write-back `W22`
- `O32` Vendor.HelpNoContext | min=0.7 | fallback=True

### PickCoins (E13)

Rules:
- `R21` pick_coins | z=0.9 | root C21

Consequences:
- `O33` Player.PicksCoins | min=0 | fallback=False
  - write-back `W23`
- `O34` Player.AlreadyHasCoins | min=0 | fallback=True

### OfferVendorCoins (E14)

Rules:
- `R22` vendor_coins | z=0.5 | root C24

Consequences:
- `O35` Vendor.CoinsAccepted | min=0 | fallback=False
  - write-back `W24`
  - write-back `W25`
- `O36` Vendor.CoinsUnavailable | min=0.7 | fallback=True

### VendorRoyalLie (E15)

Rules:
- `R23` vendor_royal_lie | z=0.4 | root C25

Consequences:
- `O37` Vendor.ConsidersRoyalClaim | min=0 | fallback=False
  - write-back `W26`
  - write-back `W27`
- `O38` Vendor.RoyalClaimNoContext | min=0 | fallback=True

### VendorRobberyWarning (E16)

Rules:
- `R24` vendor_robbery_warning | z=0.45 | root C26

Consequences:
- `O39` Vendor.ConsidersRobberyWarning | min=0 | fallback=False
  - write-back `W28`
  - write-back `W29`
- `O40` Vendor.WarningNoContext | min=0 | fallback=True

### VendorAdmitRobbery (E17)

Rules:
- `R25` vendor_admit_robbery | z=0.75 | root C27

Consequences:
- `O41` Vendor.AlarmedByConfession | min=0 | fallback=False
  - write-back `W30`
  - write-back `W31`
- `O42` Vendor.ConfessionNoContext | min=0 | fallback=True

### VendorEvaluateState (E18)

Rules:
- `R26` vendor_calm | z=0.15 | root C32
- `R28` vendor_alarmed | z=0.95 | root C29
- `R27` vendor_nervous | z=0.6 | root C33

Consequences:
- `O43` Vendor.StateCalm | min=0 | fallback=False
- `O44` Vendor.StateNervous | min=0.45 | fallback=False
  - write-back `W34`
- `O45` Vendor.StateAlarmed | min=0.8 | fallback=False
  - write-back `W35`
- `O46` Vendor.StateUnknown | min=0 | fallback=True

### NoticeVendorReaction (E19)

Rules:
- `R29` notice_vendor_reaction | z=0.7 | root C34

Consequences:
- `O47` Player.NoticesVendorReaction | min=0.5 | fallback=False
  - write-back `W36`
- `O48` Player.DoesNotNoticeVendorReaction | min=0 | fallback=True

### VendorPitJump (E20)

Rules:
- `R30` rule_id | z=0.9 | root C36

Consequences:
- `O49` Player.JumpsIntoPit | min=0.5 | fallback=False
  - write-back `W53`
- `O50` Player.CannotJumpIntoPit | min=0 | fallback=True

### VendorPitExcuse (E21)

Rules:
- `R31` vendor_pit_excuse  | z=0.6 | root C37

Consequences:
- `O51` Player.AttemptsExcuse | min=0.5 | fallback=False
- `O52` Player.CannotExcuse | min=0 | fallback=True

### VendorPitTimeout (E22)

Rules:
- `R32` vendor_pit_timeout | z=0.9 | root C38

Consequences:
- `O53` Vendor.CatchesPlayerAtPit | min=0.5 | fallback=False
  - write-back `W37`
- `O54` Vendor.PitTimeoutInvalid | min=0 | fallback=True

### VendorCallsGuard (E23)

Rules:
- `R33` vendor_calls_guard | z=0.9 | root C39

Consequences:
- `O55` Guard.AlertedByVendor | min=0.5 | fallback=False
  - write-back `W38`
- `O56` Guard.NotAlerted | min=0 | fallback=True

### GuardArrestPlayer (E24)

Rules:
- `R35` guard_arrests_player | z=0.9 | root C64

Consequences:
- `O57` Guard.HasArrestedPlayer | min=0.5 | fallback=False
  - write-back `W39`
  - write-back `W40`
- `O58` Guard.ArrestInvalid | min=0 | fallback=True

### GiveCoinsToGuard (E25)

Rules:
- `R37` player_bribes_guard | z=0.9 | root C42

Consequences:
- `O60` Guard.OfferedBribe | min=0.5 | fallback=False
  - write-back `W42`
  - write-back `W43`
  - write-back `W44`
- `O61` Vendor.RevealsSecret | min=0 | fallback=True

### EvaluateLoitering (E26)

Rules:
- `R41` carpet_loiter_mid | z=0.35 | root C01
- `R40` carpet_loiter_near | z=0.55 | root C03
- `R44` carpet_loiter_near_and_time | z=0.9 | root C05
- `R43` carpet_loiter_time | z=0.6 | root C04
- `R45` carpet_loiter_nervous_vendor | z=0.95 | root C07
- `R42` carpet_loiter_far | z=0.15 | root C02

Consequences:
- `O64` CarpetLoiter.Calm | min=0 | fallback=False
- `O65` CarpetLoiter.Uneasy | min=0.3 | fallback=False
- `O66` CarpetLoiter.Watched | min=0.6 | fallback=False
- `O67` CarpetLoiter.Intervention | min=0.72 | fallback=False
- `O68` CarpetLoiter.NoContext | min=0 | fallback=True

### VendorBlackmail (E27)

Rules:
- `R46` vendor_blackmail | z=0.9 | root C51

Consequences:
- `O69` Vendor.ReportsBlackmail | min=0.5 | fallback=False
  - write-back `W47`
- `O70` Vendor.BlackmailInvalid | min=0 | fallback=True

### VendorDeceive (E28)

Rules:
- `R47` vendor_deceive | z=0.9 | root C52

Consequences:
- `O71` Vendor.DeceptionBegins | min=0.5 | fallback=False
- `O72` Vendor.DeceptionInvalid | min=0 | fallback=True

### VendorDeceiveThreat (E29)

Rules:
- `R48` vendor_deceive_threat | z=0.9 | root C53

Consequences:
- `O73` Vendor.Intimidated | min=0.5 | fallback=False
  - write-back `W48`
- `O74` Vendor.IntimidationNoContext | min=0 | fallback=True

### VendorDeceiveReveal (E30)

Rules:
- `R49` vendor_deceive_reveal | z=0.9 | root C54

Consequences:
- `O75` Vendor.DeceiveReveal | min=0.5 | fallback=False
  - write-back `W49`
- `O76` Vendor.DeceiveRevealFallback | min=0 | fallback=True

### VendorFlirt (E31)

Rules:
- `R50` vendor_flirt | z=0.5 | root C55

Consequences:
- `O77` Vendor.FlirtAccepted | min=0.5 | fallback=False
- `O78` Vendor.FlirtFallback | min=0 | fallback=True

### InspectTreeHollow (E32)

Rules:
- `R51` tree_hollow_available | z=1 | root C56

Consequences:
- `O79` TreeHollow.Discovered | min=1 | fallback=False
  - write-back `W50`
- `O80` TreeHollow.AlreadyInspected | min=0 | fallback=True

### SearchTreeHollow (E33)

Rules:
- `R52` tree_hollow_search | z=1 | root C57

Consequences:
- `O81` TreeHollow.SearchDNE | min=0 | fallback=True
- `O82` TreeHollow.SearchStarted | min=1 | fallback=False
  - write-back `W51`
  - write-back `W52`

### LeaveTreeHollow (E34)

Rules:
- `R53` tree_hollow_walk_away | z=1 | root C58

Consequences:
- `O83` TreeHollow.WalkAway | min=1 | fallback=False
- `O84` LeaveTreeHollow.Fallback | min=0 | fallback=True

### EnterVault (E35)

Rules:
- `R54` enter_vault_accepted | z=1 | root C59
- `R55` entry_vault_denied | z=0.5 | root C66

Consequences:
- `O85` Vault.EntryAllowed | min=1 | fallback=False
  - write-back `W54`
- `O86` Vault.DeniedEntry | min=0.5 | fallback=False
  - write-back `W55`
- `O87` EnterVault.Fallback | min=0 | fallback=True

## Every node

### E01 - EventNode

- Event ID: Inspect

### R01 - RuleNode

- Rule ID: pressure_near
- Consequent: 0.55

### O01 - ConsequenceNode

- Outcome ID: Vendor.RevealsSecret
- Minimum: 0.7
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Pit.Revealed

### O02 - ConsequenceNode

- Outcome ID: Vendor.DefaultResponse
- Minimum: 0
- Fallback: False
- Fire Event: False

### W01 - WriteBackNode

- Target Key: Player.FoundPitRoute
- Operation: Set
- Value Type: Bool
- Value: True

### C01 - CriterionNode

- Mode: FuzzyNumber
- Variable ID: Player.DistanceToCarpet
- Set: Mid
- Shape: Range
- Minimum: 0
- Maximum: 10
- Point A: 2
- Point B: 4
- Point C: 6
- Point D: 8

### R02 - RuleNode

- Rule ID: pressure_mid
- Consequent: 0.35

### C02 - CriterionNode

- Mode: FuzzyNumber
- Variable ID: Player.DistanceToCarpet
- Set: Far
- Shape: High
- Minimum: 0
- Maximum: 10
- Point A: 6
- Point B: 10

### R03 - RuleNode

- Rule ID: pressure_far
- Consequent: 0.15

### O03 - ConsequenceNode

- Outcome ID: Vendor.Nervous
- Minimum: 0.4
- Fallback: False
- Fire Event: False

### O04 - ConsequenceNode

- Outcome ID: Vendor.NoValidOutput
- Minimum: 0
- Fallback: True
- Fire Event: False

### E02 - EventNode

- Event ID: AmbientSocialPressure

### C03 - CriterionNode

- Mode: FuzzyNumber
- Variable ID: Player.DistanceToCarpet
- Set: Near
- Shape: Low
- Minimum: 0
- Maximum: 10
- Point A: 0
- Point B: 4

### C04 - CriterionNode

- Mode: FuzzyNumber
- Variable ID: Player.LoiterTimeNearCarpet
- Set: Lingering
- Shape: High
- Minimum: 0
- Maximum: 14.9
- Point A: 3
- Point B: 10

### C05 - CriterionNode

- Mode: And

### R04 - RuleNode

- Rule ID: pressure_player_loitering
- Consequent: 0.6

### R05 - RuleNode

- Rule ID: pressure_near_and_loitering
- Consequent: 0.9

### O05 - ConsequenceNode

- Outcome ID: SocialPressure.Calm
- Minimum: 0
- Fallback: False
- Fire Event: False

### O06 - ConsequenceNode

- Outcome ID: SocialPressure.Uneasy
- Minimum: 0.3
- Fallback: False
- Fire Event: False

### O07 - ConsequenceNode

- Outcome ID: SocialPressure.Watched
- Minimum: 0.6
- Fallback: False
- Fire Event: False

### O08 - ConsequenceNode

- Outcome ID: SocialPressure.Intervention
- Minimum: 0.72
- Fallback: False
- Fire Event: False

### O09 - ConsequenceNode

- Outcome ID: SocialPressure.NoContext
- Minimum: 0.7
- Fallback: True
- Fire Event: False

### C06 - CriterionNode

- Mode: FuzzyNumber
- Variable ID: Vendor.Nervousness
- Set: Nervous
- Shape: High
- Minimum: 0
- Maximum: 10
- Point A: 3
- Point B: 6

### R06 - RuleNode

- Rule ID: pressure_vendor_nervous
- Consequent: 0.8

### C07 - CriterionNode

- Mode: And

### R07 - RuleNode

- Rule ID: pressure_loitering_nervous_vendor
- Consequent: 0.95

### E03 - EventNode

- Event ID: BazaarTalkToGuard

### R08 - RuleNode

- Rule ID: guard_first_conversation
- Consequent: 0.1

### R09 - RuleNode

- Rule ID: guard_repeat_conversation
- Consequent: 0.5

### O10 - ConsequenceNode

- Outcome ID: Guard.FirstConversation
- Minimum: 0
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: GuardIntroduction

### W02 - WriteBackNode

- Target Key: Guard.HasMetPlayer
- Operation: Set
- Value Type: Bool
- Value: True

### W03 - WriteBackNode

- Target Key: Guard.TalkCount
- Operation: Set
- Value Type: Int
- Value: 1

### O11 - ConsequenceNode

- Outcome ID: Guard.RepeatConversation
- Minimum: 0.4
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: GuardRepeat

### W04 - WriteBackNode

- Target Key: Guard.TalkCount
- Operation: Add
- Value Type: Int
- Value: 1

### O12 - ConsequenceNode

- Outcome ID: Guard.NoContext
- Minimum: 0
- Fallback: True
- Fire Event: False

### C08 - CriterionNode

- Mode: DoesNotExist
- Variable ID: Guard.HasMetPlayer

### C09 - CriterionNode

- Mode: BoolEquals
- Variable ID: Guard.HasMetPlayer
- Expected Bool: True

### E04 - EventNode

- Event ID: PickFlowers

### C10 - CriterionNode

- Mode: DoesNotExist
- Variable ID: Player.HasFlowers

### R10 - RuleNode

- Rule ID: pick_flowers
- Consequent: 0.9

### O13 - ConsequenceNode

- Outcome ID: Player.PicksFlowers
- Minimum: 0.5
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Flowers.Picked

### W05 - WriteBackNode

- Target Key: Player.HasFlowers
- Operation: Set
- Value Type: Bool
- Value: True

### O14 - ConsequenceNode

- Outcome ID: Player.AlreadyHasFlowers
- Minimum: 0.7
- Fallback: True
- Fire Event: False

### C11 - CriterionNode

- Mode: BoolEquals
- Variable ID: Guard.HasMetPlayer
- Expected Bool: True

### C12 - CriterionNode

- Mode: BoolEquals
- Variable ID: Player.HasFlowers
- Expected Bool: True

### C13 - CriterionNode

- Mode: And

### R11 - RuleNode

- Rule ID: guard_notices_held_flowers
- Consequent: 1

### O15 - ConsequenceNode

- Outcome ID: Guard.NoticesFlowers
- Minimum: 0.75
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: GuardNoticesFlowers

### E05 - EventNode

- Event ID: OfferFlowersToGuard

### C14 - CriterionNode

- Mode: BoolEquals
- Variable ID: Player.HasFlowers
- Expected Bool: True

### R12 - RuleNode

- Rule ID: player_offers_flowers
- Consequent: 0.9

### O16 - ConsequenceNode

- Outcome ID: Guard.FlowersOffered
- Minimum: 0.5
- Fallback: False
- Fire Event: False

### W06 - WriteBackNode

- Target Key: Guard.PlayerOfferedFlowers
- Operation: Set
- Value Type: Bool
- Value: True

### W07 - WriteBackNode

- Target Key: Guard.Relationship
- Operation: Add
- Value Type: Float
- Value: 1

### O17 - ConsequenceNode

- Outcome ID: Guard.CannotOfferFlowers
- Minimum: 0.7
- Fallback: True
- Fire Event: False

### E06 - EventNode

- Event ID: TeaseGuardAboutFlowers

### R13 - RuleNode

- Rule ID: player_teases_guard
- Consequent: 0.9

### O18 - ConsequenceNode

- Outcome ID: Guard.TeasedAboutFlowers
- Minimum: 0.5
- Fallback: False
- Fire Event: False

### W08 - WriteBackNode

- Target Key: Player.TeasedGuard
- Operation: Set
- Value Type: Bool
- Value: True

### W09 - WriteBackNode

- Target Key: Guard.Suspicion
- Operation: Add
- Value Type: Float
- Value: 20

### O19 - ConsequenceNode

- Outcome ID: Guard.CannotTeaseAboutFlowers
- Minimum: 0.7
- Fallback: True
- Fire Event: False

### E07 - EventNode

- Event ID: GiveFlowersToGuard

### R14 - RuleNode

- Rule ID: guard_accepts_flowers
- Consequent: 0.9

### O20 - ConsequenceNode

- Outcome ID: Guard.AcceptsFlowers
- Minimum: 0.5
- Fallback: False
- Fire Event: False

### W10 - WriteBackNode

- Target Key: Player.HasFlowers
- Operation: Set
- Value Type: Bool
- Value: False

### W11 - WriteBackNode

- Target Key: Guard.Charmed
- Operation: Set
- Value Type: Bool
- Value: True

### W12 - WriteBackNode

- Target Key: Guard.AllowedEntry
- Operation: Set
- Value Type: Bool
- Value: True

### O21 - ConsequenceNode

- Outcome ID: Guard.CannotGiveFlowers 
- Minimum: 0.7
- Fallback: True
- Fire Event: False

### E08 - EventNode

- Event ID: RefuseFlowersToGuard

### R15 - RuleNode

- Rule ID: player_refuses_flowers
- Consequent: 0.9

### O22 - ConsequenceNode

- Outcome ID: Guard.FlowersRefused
- Minimum: 0.5
- Fallback: False
- Fire Event: False

### W13 - WriteBackNode

- Target Key: Guard.PlayerRefusedFlowers
- Operation: Set
- Value Type: Bool
- Value: True

### O23 - ConsequenceNode

- Outcome ID: Guard.CannotRefuseFlowers
- Minimum: 0.7
- Fallback: True
- Fire Event: False

### E09 - EventNode

- Event ID: DelayFlowersToGuard

### R16 - RuleNode

- Rule ID: player_delays_flowers
- Consequent: 0.9

### O24 - ConsequenceNode

- Outcome ID: Guard.FlowersDelayed
- Minimum: 0.5
- Fallback: False
- Fire Event: False

### W14 - WriteBackNode

- Target Key: Guard.PlayerDelayedFlowers
- Operation: Set
- Value Type: Bool
- Value: True

### O25 - ConsequenceNode

- Outcome ID: Guard.CannotDelayFlowers
- Minimum: 0.7
- Fallback: True
- Fire Event: False

### W15 - WriteBackNode

- Target Key: Guard.Relationship
- Operation: Add
- Value Type: Float
- Value: 20

### E10 - EventNode

- Event ID: BazaarTalkToVendor

### C15 - CriterionNode

- Mode: DoesNotExist
- Variable ID: Vendor.HasMetPlayer

### R17 - RuleNode

- Rule ID: vendor_first_meeting
- Consequent: 0.1

### C16 - CriterionNode

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### R18 - RuleNode

- Rule ID: vendor_returning
- Consequent: 0.8

### O26 - ConsequenceNode

- Outcome ID: Vendor.Returning
- Minimum: 0.5
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: VendorReturning

### O27 - ConsequenceNode

- Outcome ID: Vendor.FirstMeeting
- Minimum: 0
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: VendorFirstMeeting

### W16 - WriteBackNode

- Target Key: Vendor.HasMetPlayer
- Operation: Set
- Value Type: Bool
- Value: True

### W17 - WriteBackNode

- Target Key: Vendor.InteractionCount
- Operation: Add
- Value Type: Int
- Value: 1

### W18 - WriteBackNode

- Target Key: Vendor.InteractionCount
- Operation: Add
- Value Type: Int
- Value: 1

### O28 - ConsequenceNode

- Outcome ID: Vendor.NoValidDialogue
- Minimum: 0
- Fallback: True
- Fire Event: False

### E11 - EventNode

- Event ID: OfferVendorFlowers

### C17 - CriterionNode

- Mode: And

### R19 - RuleNode

- Rule ID: vendor_flowers
- Consequent: 0.5

### O29 - ConsequenceNode

- Outcome ID: Vendor.FlowersOffered
- Minimum: 0
- Fallback: False
- Fire Event: False

### W19 - WriteBackNode

- Target Key: Vendor.RemembersFlowers
- Operation: Set
- Value Type: Bool
- Value: True

### W20 - WriteBackNode

- Target Key: Vendor.Suspicion
- Operation: Add
- Value Type: Float
- Value: 5

### O30 - ConsequenceNode

- Outcome ID: Vendor.FlowersNoContext
- Minimum: 0
- Fallback: True
- Fire Event: False

### E12 - EventNode

- Event ID: VendorAskHelp

### C18 - CriterionNode

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### R20 - RuleNode

- Rule ID: vendor_help_request
- Consequent: 0.5

### O31 - ConsequenceNode

- Outcome ID: Vendor.AgreesHelp
- Minimum: 0
- Fallback: False
- Fire Event: False

### W21 - WriteBackNode

- Target Key: Vendor.Nervousness
- Operation: Add
- Value Type: Float
- Value: 5

### W22 - WriteBackNode

- Target Key: Vendor.Suspicion
- Operation: Add
- Value Type: Float
- Value: 3

### O32 - ConsequenceNode

- Outcome ID: Vendor.HelpNoContext
- Minimum: 0.7
- Fallback: True
- Fire Event: False

### C19 - CriterionNode

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### C20 - CriterionNode

- Mode: BoolEquals
- Variable ID: Player.HasFlowers
- Expected Bool: True

### E13 - EventNode

- Event ID: PickCoins

### C21 - CriterionNode

- Mode: DoesNotExist
- Variable ID: Player.HasCoins

### R21 - RuleNode

- Rule ID: pick_coins
- Consequent: 0.9

### O33 - ConsequenceNode

- Outcome ID: Player.PicksCoins
- Minimum: 0
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Coins.Picked

### W23 - WriteBackNode

- Target Key: Player.HasCoins
- Operation: Set
- Value Type: Bool
- Value: True

### O34 - ConsequenceNode

- Outcome ID: Player.AlreadyHasCoins
- Minimum: 0
- Fallback: True
- Fire Event: False

### E14 - EventNode

- Event ID: OfferVendorCoins

### C22 - CriterionNode

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### C23 - CriterionNode

- Mode: BoolEquals
- Variable ID: Player.HasCoins
- Expected Bool: True

### C24 - CriterionNode

- Mode: And

### R22 - RuleNode

- Rule ID: vendor_coins
- Consequent: 0.5

### O35 - ConsequenceNode

- Outcome ID: Vendor.CoinsAccepted
- Minimum: 0
- Fallback: False
- Fire Event: False

### W24 - WriteBackNode

- Target Key: Player.HasCoins
- Operation: Set
- Value Type: Bool
- Value: False

### W25 - WriteBackNode

- Target Key: Vendor.WasBribed
- Operation: Set
- Value Type: Bool
- Value: True

### O36 - ConsequenceNode

- Outcome ID: Vendor.CoinsUnavailable
- Minimum: 0.7
- Fallback: True
- Fire Event: False

### E15 - EventNode

- Event ID: VendorRoyalLie

### C25 - CriterionNode

- Mode: Exists
- Variable ID: Vendor.WasBribed

### R23 - RuleNode

- Rule ID: vendor_royal_lie
- Consequent: 0.4

### O37 - ConsequenceNode

- Outcome ID: Vendor.ConsidersRoyalClaim
- Minimum: 0
- Fallback: False
- Fire Event: False

### W26 - WriteBackNode

- Target Key: Vendor.Suspicion
- Operation: Add
- Value Type: Float
- Value: 2

### W27 - WriteBackNode

- Target Key: Vendor.Nervousness
- Operation: Add
- Value Type: Float
- Value: 3

### O38 - ConsequenceNode

- Outcome ID: Vendor.RoyalClaimNoContext
- Minimum: 0
- Fallback: True
- Fire Event: False

### E16 - EventNode

- Event ID: VendorRobberyWarning

### C26 - CriterionNode

- Mode: Exists
- Variable ID: Vendor.WasBribed

### R24 - RuleNode

- Rule ID: vendor_robbery_warning
- Consequent: 0.45

### O39 - ConsequenceNode

- Outcome ID: Vendor.ConsidersRobberyWarning
- Minimum: 0
- Fallback: False
- Fire Event: False

### W28 - WriteBackNode

- Target Key: Vendor.Suspicion
- Operation: Add
- Value Type: Float
- Value: 1

### W29 - WriteBackNode

- Target Key: Vendor.Nervousness
- Operation: Add
- Value Type: Float
- Value: 5

### O40 - ConsequenceNode

- Outcome ID: Vendor.WarningNoContext
- Minimum: 0
- Fallback: True
- Fire Event: False

### E17 - EventNode

- Event ID: VendorAdmitRobbery

### C27 - CriterionNode

- Mode: Exists
- Variable ID: Vendor.WasBribed

### R25 - RuleNode

- Rule ID: vendor_admit_robbery
- Consequent: 0.75

### O41 - ConsequenceNode

- Outcome ID: Vendor.AlarmedByConfession
- Minimum: 0
- Fallback: False
- Fire Event: False

### W30 - WriteBackNode

- Target Key: Vendor.Suspicion
- Operation: Add
- Value Type: Float
- Value: 8

### W31 - WriteBackNode

- Target Key: Vendor.Nervousness
- Operation: Add
- Value Type: Float
- Value: 6

### O42 - ConsequenceNode

- Outcome ID: Vendor.ConfessionNoContext
- Minimum: 0
- Fallback: True
- Fire Event: False

### W32 - WriteBackNode

- Target Key: Vendor.Suspicion
- Operation: Set
- Value Type: Float
- Value: 0

### W33 - WriteBackNode

- Target Key: Vendor.Nervousness
- Operation: Set
- Value Type: Float
- Value: 0

### E18 - EventNode

- Event ID: VendorEvaluateState

### C28 - CriterionNode

- Mode: FuzzyNumber
- Variable ID: Vendor.Suspicion
- Set: SuspicionLow
- Shape: Low
- Minimum: 0
- Maximum: 10
- Point A: 2
- Point B: 6

### C29 - CriterionNode

- Mode: FuzzyNumber
- Variable ID: Vendor.Suspicion
- Set: SuspicionHigh
- Shape: High
- Minimum: 0
- Maximum: 10
- Point A: 5
- Point B: 8

### C30 - CriterionNode

- Mode: FuzzyNumber
- Variable ID: Vendor.Nervousness
- Set: NervousnessLow
- Shape: Low
- Minimum: 0
- Maximum: 10
- Point A: 2
- Point B: 5

### C31 - CriterionNode

- Mode: FuzzyNumber
- Variable ID: Vendor.Nervousness
- Set: NervousnessHigh
- Shape: High
- Minimum: 0
- Maximum: 10
- Point A: 3
- Point B: 6

### R26 - RuleNode

- Rule ID: vendor_calm
- Consequent: 0.15

### R27 - RuleNode

- Rule ID: vendor_nervous
- Consequent: 0.6

### R28 - RuleNode

- Rule ID: vendor_alarmed
- Consequent: 0.95

### O43 - ConsequenceNode

- Outcome ID: Vendor.StateCalm
- Minimum: 0
- Fallback: False
- Fire Event: False

### O44 - ConsequenceNode

- Outcome ID: Vendor.StateNervous
- Minimum: 0.45
- Fallback: False
- Fire Event: False

### W34 - WriteBackNode

- Target Key: Vendor.ReactionUnlocked
- Operation: Set
- Value Type: Bool
- Value: True

### O45 - ConsequenceNode

- Outcome ID: Vendor.StateAlarmed
- Minimum: 0.8
- Fallback: False
- Fire Event: False

### W35 - WriteBackNode

- Target Key: Vendor.ShouldCallGuard
- Operation: Set
- Value Type: Bool
- Value: True

### O46 - ConsequenceNode

- Outcome ID: Vendor.StateUnknown
- Minimum: 0
- Fallback: True
- Fire Event: False

### C32 - CriterionNode

- Mode: And

### C33 - CriterionNode

- Mode: And

### E19 - EventNode

- Event ID: NoticeVendorReaction

### C34 - CriterionNode

- Mode: FuzzyNumber
- Variable ID: Vendor.Nervousness
- Set: VendorReactionNotice
- Shape: High
- Minimum: 0
- Maximum: 10
- Point A: 3
- Point B: 6

### R29 - RuleNode

- Rule ID: notice_vendor_reaction
- Consequent: 0.7

### O47 - ConsequenceNode

- Outcome ID: Player.NoticesVendorReaction
- Minimum: 0.5
- Fallback: False
- Fire Event: False

### W36 - WriteBackNode

- Target Key: Player.NoticedVendorReaction
- Operation: Set
- Value Type: Bool
- Value: True

### O48 - ConsequenceNode

- Outcome ID: Player.DoesNotNoticeVendorReaction
- Minimum: 0
- Fallback: True
- Fire Event: False

### C35 - CriterionNode

- Mode: BoolEquals
- Variable ID: Player.NoticedVendorReaction
- Expected Bool: True

### E20 - EventNode

- Event ID: VendorPitJump

### C36 - CriterionNode

- Mode: BoolEquals
- Variable ID: Player.FoundPitRoute
- Expected Bool: True

### R30 - RuleNode

- Rule ID: rule_id
- Consequent: 0.9

### O49 - ConsequenceNode

- Outcome ID: Player.JumpsIntoPit
- Minimum: 0.5
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Pit.JumpChosen

### O50 - ConsequenceNode

- Outcome ID: Player.CannotJumpIntoPit
- Minimum: 0
- Fallback: True
- Fire Event: False

### E21 - EventNode

- Event ID: VendorPitExcuse

### C37 - CriterionNode

- Mode: BoolEquals
- Variable ID: Player.FoundPitRoute
- Expected Bool: True

### R31 - RuleNode

- Rule ID: vendor_pit_excuse 
- Consequent: 0.6

### O51 - ConsequenceNode

- Outcome ID: Player.AttemptsExcuse
- Minimum: 0.5
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Pit.ExcuseChosen

### O52 - ConsequenceNode

- Outcome ID: Player.CannotExcuse
- Minimum: 0
- Fallback: True
- Fire Event: False

### E22 - EventNode

- Event ID: VendorPitTimeout

### C38 - CriterionNode

- Mode: BoolEquals
- Variable ID: Player.FoundPitRoute
- Expected Bool: True

### R32 - RuleNode

- Rule ID: vendor_pit_timeout
- Consequent: 0.9

### O53 - ConsequenceNode

- Outcome ID: Vendor.CatchesPlayerAtPit
- Minimum: 0.5
- Fallback: False
- Fire Event: False

### W37 - WriteBackNode

- Target Key: Guard.PursuePlayer
- Operation: Set
- Value Type: Bool
- Value: True

### O54 - ConsequenceNode

- Outcome ID: Vendor.PitTimeoutInvalid
- Minimum: 0
- Fallback: True
- Fire Event: False

### E23 - EventNode

- Event ID: VendorCallsGuard

### C39 - CriterionNode

- Mode: BoolEquals
- Variable ID: Player.FoundPitRoute
- Expected Bool: True

### R33 - RuleNode

- Rule ID: vendor_calls_guard
- Consequent: 0.9

### O55 - ConsequenceNode

- Outcome ID: Guard.AlertedByVendor
- Minimum: 0.5
- Fallback: False
- Fire Event: False

### W38 - WriteBackNode

- Target Key: Guard.PursuePlayer
- Operation: Set
- Value Type: Bool
- Value: True

### O56 - ConsequenceNode

- Outcome ID: Guard.NotAlerted
- Minimum: 0
- Fallback: True
- Fire Event: False

### R34 - RuleNode

- Rule ID: reveal_noticed_carpet
- Consequent: 0.9

### E24 - EventNode

- Event ID: GuardArrestPlayer

### C40 - CriterionNode

- Mode: BoolEquals
- Variable ID: Guard.PursuePlayer
- Expected Bool: True

### R35 - RuleNode

- Rule ID: guard_arrests_player
- Consequent: 0.9

### O57 - ConsequenceNode

- Outcome ID: Guard.HasArrestedPlayer
- Minimum: 0.5
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Player.Arrested

### W39 - WriteBackNode

- Target Key: Player.Arrested
- Operation: Set
- Value Type: Bool
- Value: True

### W40 - WriteBackNode

- Target Key: Guard.PursuePlayer
- Operation: Set
- Value Type: Bool
- Value: False

### O58 - ConsequenceNode

- Outcome ID: Guard.ArrestInvalid
- Minimum: 0
- Fallback: True
- Fire Event: False

### C41 - CriterionNode

- Mode: FuzzyNumber
- Variable ID: Guard.TalkCount
- Set: High
- Shape: High
- Minimum: 0
- Maximum: 10
- Point A: 2
- Point B: 4

### R36 - RuleNode

- Rule ID: guard_repetitions_request
- Consequent: 0.8

### O59 - ConsequenceNode

- Outcome ID: Guard.RequestsItems
- Minimum: 0.55
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: GuardRequests

### W41 - WriteBackNode

- Target Key: Guard.AskedForFlowers
- Operation: Set
- Value Type: Bool
- Value: True

### E25 - EventNode

- Event ID: GiveCoinsToGuard

### C42 - CriterionNode

- Mode: BoolEquals
- Variable ID: Player.HasCoins
- Expected Bool: True

### R37 - RuleNode

- Rule ID: player_bribes_guard
- Consequent: 0.9

### O60 - ConsequenceNode

- Outcome ID: Guard.OfferedBribe
- Minimum: 0.5
- Fallback: False
- Fire Event: False

### W42 - WriteBackNode

- Target Key: Player.BribedGuard
- Operation: Set
- Value Type: Bool
- Value: True

### W43 - WriteBackNode

- Target Key: Guard.Suspicion
- Operation: Add
- Value Type: Float
- Value: 40

### W44 - WriteBackNode

- Target Key: Guard.PursuePlayer
- Operation: Set
- Value Type: Bool
- Value: True

### O61 - ConsequenceNode

- Outcome ID: Vendor.RevealsSecret
- Minimum: 0
- Fallback: True
- Fire Event: False

### C43 - CriterionNode

- Mode: FuzzyNumber
- Variable ID: Guard.Suspicion
- Set: High
- Shape: High
- Minimum: 0
- Maximum: 40
- Point A: 10
- Point B: 20

### R38 - RuleNode

- Rule ID: guard_dismisses_player
- Consequent: 0.95

### O62 - ConsequenceNode

- Outcome ID: Guard.DismissesPlayer
- Minimum: 0.8
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: GuardDismissesPlayer

### W45 - WriteBackNode

- Target Key: Guard.DismissedPlayer
- Operation: Set
- Value Type: Bool
- Value: True

### W46 - WriteBackNode

- Target Key: Guard.AllowedEntry
- Operation: Set
- Value Type: Bool
- Value: False

### C44 - CriterionNode

- Mode: BoolEquals
- Variable ID: Guard.AllowedEntry
- Expected Bool: False

### C45 - CriterionNode

- Mode: And

### C46 - CriterionNode

- Mode: BoolEquals
- Variable ID: Guard.AllowedEntry
- Expected Bool: False

### C47 - CriterionNode

- Mode: And

### C48 - CriterionNode

- Mode: BoolEquals
- Variable ID: Guard.AllowedEntry
- Expected Bool: False

### C49 - CriterionNode

- Mode: And

### C50 - CriterionNode

- Mode: BoolEquals
- Variable ID: Guard.AllowedEntry
- Expected Bool: True

### R39 - RuleNode

- Rule ID: guard_allowed_entry_repeat
- Consequent: 0.3

### O63 - ConsequenceNode

- Outcome ID: Guard.AllowedRepeat
- Minimum: 0.25
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: GuardAllowedRepeat

### E26 - EventNode

- Event ID: EvaluateLoitering

### R40 - RuleNode

- Rule ID: carpet_loiter_near
- Consequent: 0.55

### R41 - RuleNode

- Rule ID: carpet_loiter_mid
- Consequent: 0.35

### R42 - RuleNode

- Rule ID: carpet_loiter_far
- Consequent: 0.15

### R43 - RuleNode

- Rule ID: carpet_loiter_time
- Consequent: 0.6

### R44 - RuleNode

- Rule ID: carpet_loiter_near_and_time
- Consequent: 0.9

### R45 - RuleNode

- Rule ID: carpet_loiter_nervous_vendor
- Consequent: 0.95

### O64 - ConsequenceNode

- Outcome ID: CarpetLoiter.Calm
- Minimum: 0
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: VendorCarpetCalm

### O65 - ConsequenceNode

- Outcome ID: CarpetLoiter.Uneasy
- Minimum: 0.3
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: VendorCarpetUneasy

### O66 - ConsequenceNode

- Outcome ID: CarpetLoiter.Watched
- Minimum: 0.6
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: VendorCarpetWatched

### O67 - ConsequenceNode

- Outcome ID: CarpetLoiter.Intervention
- Minimum: 0.72
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: VendorCarpetIntervention

### O68 - ConsequenceNode

- Outcome ID: CarpetLoiter.NoContext
- Minimum: 0
- Fallback: True
- Fire Event: False

### E27 - EventNode

- Event ID: VendorBlackmail

### R46 - RuleNode

- Rule ID: vendor_blackmail
- Consequent: 0.9

### C51 - CriterionNode

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### O69 - ConsequenceNode

- Outcome ID: Vendor.ReportsBlackmail
- Minimum: 0.5
- Fallback: False
- Fire Event: False

### W47 - WriteBackNode

- Target Key: Guard.PursuePlayer
- Operation: Set
- Value Type: Bool
- Value: True

### O70 - ConsequenceNode

- Outcome ID: Vendor.BlackmailInvalid
- Minimum: 0
- Fallback: True
- Fire Event: False

### E28 - EventNode

- Event ID: VendorDeceive

### C52 - CriterionNode

- Mode: Exists
- Variable ID:  Vendor.HasMetPlayer

### R47 - RuleNode

- Rule ID: vendor_deceive
- Consequent: 0.9

### O71 - ConsequenceNode

- Outcome ID: Vendor.DeceptionBegins
- Minimum: 0.5
- Fallback: False
- Fire Event: False

### O72 - ConsequenceNode

- Outcome ID: Vendor.DeceptionInvalid
- Minimum: 0
- Fallback: True
- Fire Event: False

### C53 - CriterionNode

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### E29 - EventNode

- Event ID: VendorDeceiveThreat

### R48 - RuleNode

- Rule ID: vendor_deceive_threat
- Consequent: 0.9

### O73 - ConsequenceNode

- Outcome ID: Vendor.Intimidated
- Minimum: 0.5
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Vendor.MaxReaction

### W48 - WriteBackNode

- Target Key: Vendor.Nervousness
- Operation: Set
- Value Type: Float
- Value: 10

### O74 - ConsequenceNode

- Outcome ID: Vendor.IntimidationNoContext
- Minimum: 0
- Fallback: True
- Fire Event: False

### E30 - EventNode

- Event ID: VendorDeceiveReveal

### O75 - ConsequenceNode

- Outcome ID: Vendor.DeceiveReveal
- Minimum: 0.5
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Pit.Revealed

### C54 - CriterionNode

- Mode: BoolEquals
- Variable ID: Vendor.HasMetPlayer
- Expected Bool: True

### R49 - RuleNode

- Rule ID: vendor_deceive_reveal
- Consequent: 0.9

### W49 - WriteBackNode

- Target Key: Player.FoundPitRoute
- Operation: Set
- Value Type: Bool
- Value: True

### O76 - ConsequenceNode

- Outcome ID: Vendor.DeceiveRevealFallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### E31 - EventNode

- Event ID: VendorFlirt

### R50 - RuleNode

- Rule ID: vendor_flirt
- Consequent: 0.5

### C55 - CriterionNode

- Mode: BoolEquals
- Variable ID: Vendor.HasMetPlayer
- Expected Bool: True

### O77 - ConsequenceNode

- Outcome ID: Vendor.FlirtAccepted
- Minimum: 0.5
- Fallback: False
- Fire Event: False

### O78 - ConsequenceNode

- Outcome ID: Vendor.FlirtFallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### E32 - EventNode

- Event ID: InspectTreeHollow

### R51 - RuleNode

- Rule ID: tree_hollow_available
- Consequent: 1

### C56 - CriterionNode

- Mode: DoesNotExist
- Variable ID: Player.HasTreeKey

### O79 - ConsequenceNode

- Outcome ID: TreeHollow.Discovered
- Minimum: 1
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: TreeHollowDiscovery

### W50 - WriteBackNode

- Target Key: Player.InspectedTreeHollow
- Operation: Set
- Value Type: Bool
- Value: True

### O80 - ConsequenceNode

- Outcome ID: TreeHollow.AlreadyInspected
- Minimum: 0
- Fallback: True
- Fire Event: False

### E33 - EventNode

- Event ID: SearchTreeHollow

### C57 - CriterionNode

- Mode: DoesNotExist
- Variable ID: Player.HasTreeKey

### R52 - RuleNode

- Rule ID: tree_hollow_search
- Consequent: 1

### O81 - ConsequenceNode

- Outcome ID: TreeHollow.SearchDNE
- Minimum: 0
- Fallback: True
- Fire Event: False

### W51 - WriteBackNode

- Target Key: Player.HasTreeKe
- Operation: Set
- Value Type: Bool
- Value: True

### O82 - ConsequenceNode

- Outcome ID: TreeHollow.SearchStarted
- Minimum: 1
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: TreeHollow.SearchStarted

### W52 - WriteBackNode

- Target Key: Guard.Suspicion
- Operation: Add
- Value Type: Float
- Value: 10

### C58 - CriterionNode

- Mode: BoolEquals
- Variable ID: Player.InspectedTreeHollow
- Expected Bool: True

### E34 - EventNode

- Event ID: LeaveTreeHollow

### R53 - RuleNode

- Rule ID: tree_hollow_walk_away
- Consequent: 1

### O83 - ConsequenceNode

- Outcome ID: TreeHollow.WalkAway
- Minimum: 1
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: TreeHollow.WalkAway

### O84 - ConsequenceNode

- Outcome ID: LeaveTreeHollow.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: True
- Target: DialogueGraph
- Payload: VendorSecretRevealed

### E35 - EventNode

- Event ID: EnterVault

### C59 - CriterionNode

- Mode: BoolEquals
- Variable ID: Guard.AllowedEntry
- Expected Bool: True

### R54 - RuleNode

- Rule ID: enter_vault_accepted
- Consequent: 1

### R55 - RuleNode

- Rule ID: entry_vault_denied
- Consequent: 0.5

### C60 - CriterionNode

- Mode: DoesNotExist
- Variable ID: Guard.AllowedEntry

### O85 - ConsequenceNode

- Outcome ID: Vault.EntryAllowed
- Minimum: 1
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Vault.Enter

### W53 - WriteBackNode

- Target Key: Player.EnteredVault
- Operation: Set
- Value Type: Bool
- Value: True

### W54 - WriteBackNode

- Target Key: Player.EnteredVault
- Operation: Set
- Value Type: Bool
- Value: True

### O86 - ConsequenceNode

- Outcome ID: Vault.DeniedEntry
- Minimum: 0.5
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: GuardArrestsPlayer

### C61 - CriterionNode

- Mode: BoolEquals
- Variable ID: Guard.AllowedEntry
- Expected Bool: False

### C62 - CriterionNode

- Mode: DoesNotExist
- Variable ID: Guard.AllowedEntry

### C63 - CriterionNode

- Mode: Or

### C64 - CriterionNode

- Mode: Or

### O87 - ConsequenceNode

- Outcome ID: EnterVault.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### C65 - CriterionNode

- Mode: BoolEquals
- Variable ID: Guard.AllowedEntry
- Expected Bool: False

### C66 - CriterionNode

- Mode: Or

### W55 - WriteBackNode

- Target Key: Guard.PursuePlayer
- Operation: Set
- Value Type: Bool
- Value: True

### C67 - CriterionNode

- Mode: NumberCompare
- Variable ID: Guard.TalkCount
- Comparison: LessThanOrEqual
- Compare A: 2

### C68 - CriterionNode

- Mode: And

## Every connection

- `E01.consequences` -> `O04.event`
- `E01.consequences` -> `O02.event`
- `E01.consequences` -> `O03.event`
- `E01.consequences` -> `O01.event`
- `E01.rules` -> `R34.event`
- `O01.writeBacks` -> `W01.consequence`
- `C01.rules` -> `R02.criteria`
- `C01.rules` -> `R41.criteria`
- `C02.rules` -> `R03.criteria`
- `C02.rules` -> `R42.criteria`
- `E02.consequences` -> `O05.event`
- `E02.consequences` -> `O07.event`
- `E02.consequences` -> `O08.event`
- `E02.consequences` -> `O06.event`
- `E02.consequences` -> `O09.event`
- `E02.rules` -> `R04.event`
- `E02.rules` -> `R05.event`
- `E02.rules` -> `R07.event`
- `E02.rules` -> `R01.event`
- `E02.rules` -> `R02.event`
- `E02.rules` -> `R03.event`
- `C03.rules` -> `C05.criteriaA`
- `C03.rules` -> `R01.criteria`
- `C03.rules` -> `R40.criteria`
- `C04.rules` -> `C05.criteriaB`
- `C04.rules` -> `R04.criteria`
- `C04.rules` -> `C07.criteriaA`
- `C04.rules` -> `R43.criteria`
- `C05.rules` -> `R05.criteria`
- `C05.rules` -> `R44.criteria`
- `C06.rules` -> `R06.event`
- `C06.rules` -> `C07.criteriaB`
- `C07.rules` -> `R07.criteria`
- `C07.rules` -> `R45.criteria`
- `E03.consequences` -> `O10.event`
- `E03.consequences` -> `O15.event`
- `E03.consequences` -> `O12.event`
- `E03.consequences` -> `O63.event`
- `E03.consequences` -> `O11.event`
- `E03.consequences` -> `O62.event`
- `E03.consequences` -> `O59.event`
- `E03.rules` -> `R11.event`
- `E03.rules` -> `R08.event`
- `E03.rules` -> `R09.event`
- `E03.rules` -> `R38.event`
- `E03.rules` -> `R36.event`
- `E03.rules` -> `R39.event`
- `O10.writeBacks` -> `W02.consequence`
- `O10.writeBacks` -> `W03.consequence`
- `O10.writeBacks` -> `W46.consequence`
- `O11.writeBacks` -> `W04.consequence`
- `C08.rules` -> `R08.criteria`
- `C09.rules` -> `C49.criteriaA`
- `E04.consequences` -> `O13.event`
- `E04.consequences` -> `O14.event`
- `E04.rules` -> `R10.event`
- `C10.rules` -> `R10.criteria`
- `O13.writeBacks` -> `W05.consequence`
- `C11.rules` -> `C13.criteriaB`
- `C12.rules` -> `C13.criteriaA`
- `C13.rules` -> `R11.criteria`
- `E05.consequences` -> `O16.event`
- `E05.consequences` -> `O17.event`
- `E05.rules` -> `R12.event`
- `C14.rules` -> `R15.criteria`
- `C14.rules` -> `R16.criteria`
- `C14.rules` -> `R13.criteria`
- `C14.rules` -> `R14.criteria`
- `C14.rules` -> `R12.criteria`
- `O16.writeBacks` -> `W06.consequence`
- `O16.writeBacks` -> `W07.consequence`
- `E06.consequences` -> `O18.event`
- `E06.consequences` -> `O19.event`
- `E06.rules` -> `R13.event`
- `O18.writeBacks` -> `W08.consequence`
- `O18.writeBacks` -> `W09.consequence`
- `E07.consequences` -> `O20.event`
- `E07.consequences` -> `O21.event`
- `E07.rules` -> `R14.event`
- `O20.writeBacks` -> `W10.consequence`
- `O20.writeBacks` -> `W11.consequence`
- `O20.writeBacks` -> `W12.consequence`
- `O20.writeBacks` -> `W15.consequence`
- `E08.consequences` -> `O22.event`
- `E08.consequences` -> `O23.event`
- `E08.rules` -> `R15.event`
- `O22.writeBacks` -> `W13.consequence`
- `E09.consequences` -> `O24.event`
- `E09.consequences` -> `O25.event`
- `E09.rules` -> `R16.event`
- `O24.writeBacks` -> `W14.consequence`
- `E10.consequences` -> `O27.event`
- `E10.consequences` -> `O26.event`
- `E10.consequences` -> `O28.event`
- `E10.rules` -> `R17.event`
- `E10.rules` -> `R18.event`
- `C15.rules` -> `R17.criteria`
- `C16.rules` -> `R18.criteria`
- `O26.writeBacks` -> `W18.consequence`
- `O27.writeBacks` -> `W16.consequence`
- `O27.writeBacks` -> `W17.consequence`
- `O27.writeBacks` -> `W32.consequence`
- `O27.writeBacks` -> `W33.consequence`
- `E11.consequences` -> `O29.event`
- `E11.consequences` -> `O30.event`
- `E11.rules` -> `R19.event`
- `C17.rules` -> `R19.criteria`
- `O29.writeBacks` -> `W19.consequence`
- `O29.writeBacks` -> `W20.consequence`
- `E12.consequences` -> `O31.event`
- `E12.consequences` -> `O32.event`
- `E12.rules` -> `R20.event`
- `C18.rules` -> `R20.criteria`
- `O31.writeBacks` -> `W21.consequence`
- `O31.writeBacks` -> `W22.consequence`
- `C19.rules` -> `C17.criteriaA`
- `C20.rules` -> `C17.criteriaB`
- `E13.consequences` -> `O33.event`
- `E13.consequences` -> `O34.event`
- `E13.rules` -> `R21.event`
- `C21.rules` -> `R21.criteria`
- `O33.writeBacks` -> `W23.consequence`
- `E14.consequences` -> `O35.event`
- `E14.consequences` -> `O36.event`
- `E14.rules` -> `R22.event`
- `C22.rules` -> `C24.criteriaA`
- `C23.rules` -> `C24.criteriaB`
- `C24.rules` -> `R22.criteria`
- `O35.writeBacks` -> `W24.consequence`
- `O35.writeBacks` -> `W25.consequence`
- `E15.consequences` -> `O37.event`
- `E15.consequences` -> `O38.event`
- `E15.rules` -> `R23.event`
- `C25.rules` -> `R23.criteria`
- `O37.writeBacks` -> `W26.consequence`
- `O37.writeBacks` -> `W27.consequence`
- `E16.consequences` -> `O39.event`
- `E16.consequences` -> `O40.event`
- `E16.rules` -> `R24.event`
- `C26.rules` -> `R24.criteria`
- `O39.writeBacks` -> `W28.consequence`
- `O39.writeBacks` -> `W29.consequence`
- `E17.consequences` -> `O41.event`
- `E17.consequences` -> `O42.event`
- `E17.rules` -> `R25.event`
- `C27.rules` -> `R25.criteria`
- `O41.writeBacks` -> `W30.consequence`
- `O41.writeBacks` -> `W31.consequence`
- `E18.consequences` -> `O43.event`
- `E18.consequences` -> `O44.event`
- `E18.consequences` -> `O45.event`
- `E18.consequences` -> `O46.event`
- `E18.rules` -> `R26.event`
- `E18.rules` -> `R28.event`
- `E18.rules` -> `R27.event`
- `C28.rules` -> `C32.criteriaB`
- `C28.rules` -> `C33.criteriaA`
- `C29.rules` -> `R28.criteria`
- `C30.rules` -> `C32.criteriaA`
- `C31.rules` -> `C33.criteriaB`
- `O44.writeBacks` -> `W34.consequence`
- `O45.writeBacks` -> `W35.consequence`
- `C32.rules` -> `R26.criteria`
- `C33.rules` -> `R27.criteria`
- `E19.consequences` -> `O47.event`
- `E19.consequences` -> `O48.event`
- `E19.rules` -> `R29.event`
- `C34.rules` -> `R29.criteria`
- `O47.writeBacks` -> `W36.consequence`
- `C35.rules` -> `R34.criteria`
- `E20.consequences` -> `O49.event`
- `E20.consequences` -> `O50.event`
- `E20.rules` -> `R30.event`
- `C36.rules` -> `R30.criteria`
- `O49.writeBacks` -> `W53.consequence`
- `E21.consequences` -> `O51.event`
- `E21.consequences` -> `O52.event`
- `E21.rules` -> `R31.event`
- `C37.rules` -> `R31.criteria`
- `E22.consequences` -> `O53.event`
- `E22.consequences` -> `O54.event`
- `E22.rules` -> `R32.event`
- `C38.rules` -> `R32.criteria`
- `O53.writeBacks` -> `W37.consequence`
- `E23.consequences` -> `O55.event`
- `E23.consequences` -> `O56.event`
- `E23.rules` -> `R33.event`
- `C39.rules` -> `R33.criteria`
- `O55.writeBacks` -> `W38.consequence`
- `E24.consequences` -> `O57.event`
- `E24.consequences` -> `O58.event`
- `E24.rules` -> `R35.event`
- `C40.rules` -> `C64.criteriaB`
- `O57.writeBacks` -> `W39.consequence`
- `O57.writeBacks` -> `W40.consequence`
- `C41.rules` -> `C45.criteriaA`
- `O59.writeBacks` -> `W41.consequence`
- `E25.consequences` -> `O60.event`
- `E25.consequences` -> `O61.event`
- `E25.rules` -> `R37.event`
- `C42.rules` -> `R37.criteria`
- `O60.writeBacks` -> `W42.consequence`
- `O60.writeBacks` -> `W43.consequence`
- `O60.writeBacks` -> `W44.consequence`
- `C43.rules` -> `C47.criteriaA`
- `O62.writeBacks` -> `W45.consequence`
- `C44.rules` -> `C45.criteriaB`
- `C45.rules` -> `R36.criteria`
- `C46.rules` -> `C47.criteriaB`
- `C47.rules` -> `C68.criteriaA`
- `C48.rules` -> `C49.criteriaB`
- `C49.rules` -> `R09.criteria`
- `C50.rules` -> `R39.criteria`
- `E26.consequences` -> `O64.event`
- `E26.consequences` -> `O65.event`
- `E26.consequences` -> `O66.event`
- `E26.consequences` -> `O67.event`
- `E26.consequences` -> `O68.event`
- `E26.rules` -> `R41.event`
- `E26.rules` -> `R40.event`
- `E26.rules` -> `R44.event`
- `E26.rules` -> `R43.event`
- `E26.rules` -> `R45.event`
- `E26.rules` -> `R42.event`
- `E27.consequences` -> `O69.event`
- `E27.consequences` -> `O70.event`
- `E27.rules` -> `R46.event`
- `C51.rules` -> `R46.criteria`
- `O69.writeBacks` -> `W47.consequence`
- `E28.consequences` -> `O71.event`
- `E28.consequences` -> `O72.event`
- `E28.rules` -> `R47.event`
- `C52.rules` -> `R47.criteria`
- `C53.rules` -> `R48.criteria`
- `E29.consequences` -> `O73.event`
- `E29.consequences` -> `O74.event`
- `E29.rules` -> `R48.event`
- `O73.writeBacks` -> `W48.consequence`
- `E30.consequences` -> `O75.event`
- `E30.consequences` -> `O76.event`
- `E30.rules` -> `R49.event`
- `O75.writeBacks` -> `W49.consequence`
- `C54.rules` -> `R49.criteria`
- `E31.consequences` -> `O77.event`
- `E31.consequences` -> `O78.event`
- `E31.rules` -> `R50.event`
- `C55.rules` -> `R50.criteria`
- `E32.consequences` -> `O79.event`
- `E32.consequences` -> `O80.event`
- `E32.rules` -> `R51.event`
- `C56.rules` -> `R51.criteria`
- `O79.writeBacks` -> `W50.consequence`
- `E33.consequences` -> `O81.event`
- `E33.consequences` -> `O82.event`
- `E33.rules` -> `R52.event`
- `C57.rules` -> `R52.criteria`
- `O82.writeBacks` -> `W51.consequence`
- `O82.writeBacks` -> `W52.consequence`
- `C58.rules` -> `R53.criteria`
- `E34.consequences` -> `O83.event`
- `E34.consequences` -> `O84.event`
- `E34.rules` -> `R53.event`
- `E35.consequences` -> `O85.event`
- `E35.consequences` -> `O86.event`
- `E35.consequences` -> `O87.event`
- `E35.rules` -> `R54.event`
- `E35.rules` -> `R55.event`
- `C59.rules` -> `R54.criteria`
- `C60.rules` -> `C66.criteriaA`
- `O85.writeBacks` -> `W54.consequence`
- `O86.writeBacks` -> `W55.consequence`
- `C61.rules` -> `C63.criteriaB`
- `C62.rules` -> `C63.criteriaA`
- `C63.rules` -> `C64.criteriaA`
- `C64.rules` -> `R35.criteria`
- `C65.rules` -> `C66.criteriaB`
- `C66.rules` -> `R55.criteria`
- `C67.rules` -> `C68.criteriaB`
- `C68.rules` -> `R38.criteria`

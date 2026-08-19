# MainGame - FuzzyGraph2 graph report

Source: `Assets/FuzzyGraph_2/Editor/Graph/MainGame.fuzzygraph2`
Exported: 2026-08-19 19:01:40

## Compile check

`[fuzzygraph2] compiled 'export check' | events 55 | variables 11 | expressions 123 | rules 77 | bands 74 | fallbacks 54 | consequences 43 | write-backs 85`

## Event overview

### Inspect (E01)

Rules:
- `R34` reveal_noticed_carpet | z=0.9 | root C82

Consequences:
- `O04` Vendor.NoValidOutput | min=0 | fallback=True
- `O02` Vendor.DefaultResponse | min=0 | fallback=False
- `O03` Vendor.Nervous | min=0.4 | fallback=False
- `O01` Vendor.RevealsSecret | min=0.7 | fallback=False
  - write-back `W01`

### AmbientSocialPressure (E02)

Rules:
- `R02` pressure_mid | z=0.35 | root C89
- `R01` pressure_near | z=0.55 | root C85
- `R03` pressure_far | z=0.15 | root C90
- `R04` pressure_player_loitering | z=0.6 | root C87
- `R07` pressure_loitering_nervous_vendor | z=0.95 | root C86
- `R05` pressure_near_and_loitering | z=0.9 | root C06
- `R06` pressure_vendor_nervous | z=0.8 | root C88

Consequences:
- `O05` SocialPressure.Calm | min=0 | fallback=False
- `O07` SocialPressure.Watched | min=0.6 | fallback=False
- `O08` SocialPressure.Intervention | min=0.72 | fallback=False
- `O06` SocialPressure.Uneasy | min=0.3 | fallback=False
- `O09` SocialPressure.NoContext | min=0.7 | fallback=True

### BazaarTalkToGuard (E03)

Rules:
- `R11` guard_notices_held_flowers | z=1 | root C42
- `R08` guard_first_conversation | z=0.1 | root C43
- `R09` guard_repeat_conversation | z=0.5 | root C52
- `R38` guard_dismisses_player | z=0.95 | root C46
- `R36` guard_repetitions_request | z=0.8 | root C49
- `R39` guard_allowed_entry_repeat | z=0.3 | root C53

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
- `R10` pick_flowers | z=0.9 | root C02

Consequences:
- `O13` Player.PicksFlowers | min=0.5 | fallback=False
  - write-back `W05`
- `O14` Player.AlreadyHasFlowers | min=0.7 | fallback=True

### OfferFlowersToGuard (E05)

Rules:
- `R12` player_offers_flowers | z=0.9 | root C54

Consequences:
- `O16` Guard.FlowersOffered | min=0.5 | fallback=False
  - write-back `W06`
  - write-back `W07`
- `O17` Guard.CannotOfferFlowers | min=0.7 | fallback=True

### TeaseGuardAboutFlowers (E06)

Rules:
- `R13` player_teases_guard | z=0.9 | root C54

Consequences:
- `O18` Guard.TeasedAboutFlowers | min=0.5 | fallback=False
  - write-back `W08`
  - write-back `W09`
- `O19` Guard.CannotTeaseAboutFlowers | min=0.7 | fallback=True

### GiveFlowersToGuard (E07)

Rules:
- `R14` guard_accepts_flowers | z=0.9 | root C54

Consequences:
- `O20` Guard.AcceptsFlowers | min=0.5 | fallback=False
  - write-back `W10`
  - write-back `W11`
  - write-back `W12`
  - write-back `W15`
- `O21` Guard.CannotGiveFlowers  | min=0.7 | fallback=True

### RefuseFlowersToGuard (E08)

Rules:
- `R15` player_refuses_flowers | z=0.9 | root C54

Consequences:
- `O22` Guard.FlowersRefused | min=0.5 | fallback=False
  - write-back `W13`
- `O23` Guard.CannotRefuseFlowers | min=0.7 | fallback=True

### DelayFlowersToGuard (E09)

Rules:
- `R16` player_delays_flowers | z=0.9 | root C54

Consequences:
- `O24` Guard.FlowersDelayed | min=0.5 | fallback=False
  - write-back `W14`
- `O25` Guard.CannotDelayFlowers | min=0.7 | fallback=True

### BazaarTalkToVendor (E10)

Rules:
- `R17` vendor_first_meeting | z=0.1 | root C67
- `R18` vendor_returning | z=0.8 | root C68

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
- `R19` vendor_flowers | z=0.5 | root C74

Consequences:
- `O29` Vendor.FlowersOffered | min=0 | fallback=False
  - write-back `W19`
  - write-back `W20`
- `O30` Vendor.FlowersNoContext | min=0 | fallback=True

### VendorAskHelp (E12)

Rules:
- `R20` vendor_help_request | z=0.5 | root C01

Consequences:
- `O31` Vendor.AgreesHelp | min=0 | fallback=False
  - write-back `W21`
  - write-back `W22`
- `O32` Vendor.HelpNoContext | min=0.7 | fallback=True

### PickCoins (E13)

Rules:
- `R21` pick_coins | z=0.9 | root C16

Consequences:
- `O33` Player.PicksCoins | min=0 | fallback=False
  - write-back `W23`
- `O34` Player.AlreadyHasCoins | min=0 | fallback=True

### OfferVendorCoins (E14)

Rules:
- `R22` vendor_coins | z=0.5 | root C71

Consequences:
- `O35` Vendor.CoinsAccepted | min=0 | fallback=False
  - write-back `W24`
  - write-back `W25`
- `O36` Vendor.CoinsUnavailable | min=0.7 | fallback=True

### VendorRoyalLie (E15)

Rules:
- `R23` vendor_royal_lie | z=0.4 | root C75

Consequences:
- `O37` Vendor.ConsidersRoyalClaim | min=0 | fallback=False
  - write-back `W26`
  - write-back `W27`
- `O38` Vendor.RoyalClaimNoContext | min=0 | fallback=True

### VendorRobberyWarning (E16)

Rules:
- `R24` vendor_robbery_warning | z=0.45 | root C83

Consequences:
- `O39` Vendor.ConsidersRobberyWarning | min=0 | fallback=False
  - write-back `W28`
  - write-back `W29`
- `O40` Vendor.WarningNoContext | min=0 | fallback=True

### VendorAdmitRobbery (E17)

Rules:
- `R25` vendor_admit_robbery | z=0.75 | root C84

Consequences:
- `O41` Vendor.AlarmedByConfession | min=0 | fallback=False
  - write-back `W30`
  - write-back `W31`
- `O42` Vendor.ConfessionNoContext | min=0 | fallback=True

### VendorEvaluateState (E18)

Rules:
- `R26` vendor_calm | z=0.15 | root C78
- `R28` vendor_alarmed | z=0.95 | root C80
- `R27` vendor_nervous | z=0.6 | root C81

Consequences:
- `O43` Vendor.StateCalm | min=0 | fallback=False
- `O44` Vendor.StateNervous | min=0.45 | fallback=False
  - write-back `W34`
- `O45` Vendor.StateAlarmed | min=0.8 | fallback=False
  - write-back `W35`
- `O46` Vendor.StateUnknown | min=0 | fallback=True

### NoticeVendorReaction (E19)

Rules:
- `R29` notice_vendor_reaction | z=0.7 | root C05

Consequences:
- `O47` Player.NoticesVendorReaction | min=0.5 | fallback=False
  - write-back `W36`
- `O48` Player.DoesNotNoticeVendorReaction | min=0 | fallback=True

### VendorPitJump (E20)

Rules:
- `R30` rule_id | z=0.9 | root C03

Consequences:
- `O49` Player.JumpsIntoPit | min=0.5 | fallback=False
  - write-back `W53`
- `O50` Player.CannotJumpIntoPit | min=0 | fallback=True

### VendorPitExcuse (E21)

Rules:
- `R31` vendor_pit_excuse  | z=0.6 | root C94

Consequences:
- `O51` Player.AttemptsExcuse | min=0.5 | fallback=False
- `O52` Player.CannotExcuse | min=0 | fallback=True

### VendorPitTimeout (E22)

Rules:
- `R32` vendor_pit_timeout | z=0.9 | root C91

Consequences:
- `O53` Vendor.CatchesPlayerAtPit | min=0.5 | fallback=False
  - write-back `W37`
- `O54` Vendor.PitTimeoutInvalid | min=0 | fallback=True

### VendorCallsGuard (E23)

Rules:
- `R33` vendor_calls_guard | z=0.9 | root C95

Consequences:
- `O55` Guard.AlertedByVendor | min=0.5 | fallback=False
  - write-back `W38`
- `O56` Guard.NotAlerted | min=0 | fallback=True

### GuardArrestPlayer (E24)

Rules:
- `R35` guard_arrests_player | z=0.9 | root C59

Consequences:
- `O57` Guard.HasArrestedPlayer | min=0.5 | fallback=False
  - write-back `W39`
  - write-back `W40`
- `O58` Guard.ArrestInvalid | min=0 | fallback=True

### GiveCoinsToGuard (E25)

Rules:
- `R37` player_bribes_guard | z=0.9 | root C60

Consequences:
- `O60` Guard.OfferedBribe | min=0.5 | fallback=False
  - write-back `W42`
  - write-back `W43`
  - write-back `W44`
- `O61` Vendor.RevealsSecret | min=0 | fallback=True

### EvaluateLoitering (E26)

Rules:
- `R41` carpet_loiter_mid | z=0.35 | root C89
- `R40` carpet_loiter_near | z=0.55 | root C85
- `R44` carpet_loiter_near_and_time | z=0.9 | root C06
- `R43` carpet_loiter_time | z=0.6 | root C87
- `R45` carpet_loiter_nervous_vendor | z=0.95 | root C86
- `R42` carpet_loiter_far | z=0.15 | root C90

Consequences:
- `O64` CarpetLoiter.Calm | min=0 | fallback=False
- `O65` CarpetLoiter.Uneasy | min=0.3 | fallback=False
- `O66` CarpetLoiter.Watched | min=0.6 | fallback=False
  - write-back `W78`
- `O67` CarpetLoiter.Intervention | min=0.72 | fallback=False
  - write-back `W79`
- `O68` CarpetLoiter.NoContext | min=0 | fallback=True

### VendorBlackmail (E27)

Rules:
- `R46` vendor_blackmail | z=0.9 | root C96

Consequences:
- `O69` Vendor.ReportsBlackmail | min=0.5 | fallback=False
  - write-back `W47`
- `O70` Vendor.BlackmailInvalid | min=0 | fallback=True

### VendorDeceive (E28)

Rules:
- `R47` vendor_deceive | z=0.9 | root C97

Consequences:
- `O71` Vendor.DeceptionBegins | min=0.5 | fallback=False
- `O72` Vendor.DeceptionInvalid | min=0 | fallback=True

### VendorDeceiveThreat (E29)

Rules:
- `R48` vendor_deceive_threat | z=0.9 | root C98

Consequences:
- `O73` Vendor.Intimidated | min=0.5 | fallback=False
  - write-back `W48`
- `O74` Vendor.IntimidationNoContext | min=0 | fallback=True

### VendorDeceiveReveal (E30)

Rules:
- `R49` vendor_deceive_reveal | z=0.9 | root C93

Consequences:
- `O75` Vendor.DeceiveReveal | min=0.5 | fallback=False
  - write-back `W49`
- `O76` Vendor.DeceiveRevealFallback | min=0 | fallback=True

### VendorFlirt (E31)

Rules:
- `R50` vendor_flirt | z=0.5 | root C92

Consequences:
- `O77` Vendor.FlirtAccepted | min=0.5 | fallback=False
  - write-back `W76`
- `O78` Vendor.FlirtFallback | min=0 | fallback=True

### InspectTreeHollow (E32)

Rules:
- `R51` tree_hollow_available | z=1 | root C63

Consequences:
- `O79` TreeHollow.Discovered | min=1 | fallback=False
  - write-back `W50`
- `O80` TreeHollow.AlreadyInspected | min=0 | fallback=True

### SearchTreeHollow (E33)

Rules:
- `R52` tree_hollow_search | z=1 | root C62

Consequences:
- `O81` TreeHollow.SearchDNE | min=0 | fallback=True
- `O82` TreeHollow.SearchStarted | min=1 | fallback=False
  - write-back `W51`
  - write-back `W52`

### LeaveTreeHollow (E34)

Rules:
- `R53` tree_hollow_walk_away | z=1 | root C61

Consequences:
- `O83` TreeHollow.WalkAway | min=1 | fallback=False
- `O84` LeaveTreeHollow.Fallback | min=0 | fallback=True

### EnterVault (E35)

Rules:
- `R54` enter_vault_accepted | z=1 | root C64
- `R55` entry_vault_denied | z=0.5 | root C07

Consequences:
- `O85` Vault.EntryAllowed | min=1 | fallback=False
  - write-back `W54`
- `O86` Vault.DeniedEntry | min=0.5 | fallback=False
  - write-back `W55`
- `O87` EnterVault.Fallback | min=0 | fallback=True

### VaultLockSolved (E36)

Rules:
- `R56` vault_lock_solved | z=1 | root C09

Consequences:
- `O88` Vault.LockSolved | min=1 | fallback=False
  - write-back `W56`
- `O89` Vault.LockSolvedFallback | min=0 | fallback=True

### InspectVaultHinges (E37)

Rules:
- `R57` inspect_vault_hinges | z=1 | root C22

Consequences:
- `O90` Vault.HingesInspected | min=1 | fallback=False
  - write-back `W57`
  - write-back `W60`
- `O91` InspectVaultHinges.Fallback | min=0 | fallback=True

### UseTreeKey (E38)

Rules:
- `R58` use_tree_key | z=1 | root C10

Consequences:
- `O92` Vault.TreeKeyAccepted | min=1 | fallback=False
  - write-back `W58`
- `O93` UseTreeKey.Fallback | min=0 | fallback=True

### VaultDoorInteract (E39)

Rules:
- `R59` vault_door_interact | z=1 | root C17

Consequences:
- `O94` Vault.DoorInteraction | min=1 | fallback=False
  - write-back `W59`
  - write-back `W65`
- `O95` VaultDoorInteract.Fallback | min=0 | fallback=True

### ChooseVaultCombination (E40)

Rules:
- `R60` choose_vault_combination | z=1 | root C23

Consequences:
- `O96` Vault.CombinationChosen | min=1 | fallback=False
- `O97` VaultCombi.Fallback | min=0 | fallback=True

### VaultLockFailed (E41)

Rules:
- `R61` vault_lock_failed | z=1 | root C33

Consequences:
- `O98` Vault.LockFailed | min=1 | fallback=False
  - write-back `W61`
  - write-back `W62`
- `O99` VaultLockFailed.Fallback | min=0 | fallback=True

### VaultDoorNoiseCheck (E42)

Rules:
- `R62` door_opened_carefully | z=0.2 | root C08
- `R63` Vault.DoorOpenedNoisily | z=0.8 | root C34

Consequences:
- `O100` Vault.DoorOpenedQuietly | min=0 | fallback=False
  - write-back `W63`
- `O101` Vault.DoorOpenedNoisily | min=0.5 | fallback=False
  - write-back `W64`
- `O102` VaultDoorNoiseCheck.Fallback | min=0 | fallback=True

### VaultAlertCheck (E43)

Rules:
- `R64` vault_security_alert | z=1 | root C102

Consequences:
- `O103` Vault.SecurityAlerted | min=1 | fallback=False
  - write-back `W66`
- `O104` Vault.SecurityCalm | min=0 | fallback=True

### PickupGuardDisguise (E44)

Rules:
- `R65` pickup_guard_disguise | z=1 | root C18

Consequences:
- `O105` Player.TakesGuardDisguise | min=1 | fallback=False
  - write-back `W67`
- `O106` PickupGuardDisguise.Fallback | min=0 | fallback=True

### StealVaultFruit (E45)

Rules:
- `R66` steal_vault_fruit | z=1 | root C11

Consequences:
- `O107` Player.TakesVaultFruit | min=1 | fallback=False
  - write-back `W68`
- `O108` StealVaultFruit.Fallback | min=0 | fallback=True

### ExitVault (E46)

Rules:
- `R67` exit_vault | z=1 | root C21

Consequences:
- `O109` Vault.PlayerExits | min=1 | fallback=False
  - write-back `W69`
- `O110` ExitVault.Fallback | min=0 | fallback=True

### FinalGuardReaction (E47)

Rules:
- `R68` guard_accepts_deception | z=0.2 | root C32
- `R69` guard_rejects_deception | z=1 | root C04

Consequences:
- `O111` Guard.DeceptionAccepted | min=0 | fallback=False
  - write-back `W70`
- `O112` Guard.DeceptionRejected | min=0.5 | fallback=False
  - write-back `W71`
- `O113` FinalGuardReaction.Fallback | min=0 | fallback=True
  - write-back `W72`

### NewKeyPicked (E48)

Rules:
- `R70` new_key | z=1 | root C27

Consequences:
- `O114` NewKeyacquired | min=1 | fallback=False
  - write-back `W73`
- `O115` NewKeyFallback | min=0 | fallback=True

### GuardFellIntoPit (E49)

Rules:
- `R71` rule_id | z=1 | root C12

Consequences:
- `O116` Guard.Unavailable | min=1 | fallback=False
  - write-back `W74`
- `O119` GuardFellIntoPit | min=0 | fallback=True

### CheckVaultExit (E50)

Rules:
- `R72` vault_exit_safe | z=1 | root C15

Consequences:
- `O117` Vault.GateExit | min=1 | fallback=False
  - write-back `W75`
- `O118` CheckVaultExit.Blocked | min=0 | fallback=True

### BazaarReturnCheck (E51)

Rules:
- `R73` bazaar_return_from_vault_gate | z=1 | root C37

Consequences:
- `O120` Bazaar.ReturnFromVaultGate | min=1 | fallback=False
- `O121` Bazaar.ReturnNormal | min=0 | fallback=True

### VendorChoiceRevealPit (E52)

Rules:
- `R74` vendor_choice_reveals_pit | z=1 | root C105

Consequences:
- `O122` Vendor.ChoiceRevealsPit | min=0.5 | fallback=False
  - write-back `W77`
- `O123` VendorChoiceRevealPit.Fallback | min=0 | fallback=True

### VendorDirectVaultAsk (E53)

Rules:
- `R75` vendor_direct_vault_ask | z=0.55 | root C106

Consequences:
- `O124` Vendor.DirectVaultAsk | min=0.5 | fallback=False
  - write-back `W80`
  - write-back `W81`
- `O125` VendorDirectVaultAsk.Fallback | min=0 | fallback=True

### VendorSplitDeal (E54)

Rules:
- `R76` vendor_split_deal | z=0.65 | root C109

Consequences:
- `O126` Vendor.SplitDealAccepted | min=0.5 | fallback=False
  - write-back `W82`
  - write-back `W83`
- `O127` VendorSplitDeal | min=0 | fallback=True

### VendorGuardGrudge (E55)

Rules:
- `R77` vendor_guard_grudge | z=0.75 | root C111

Consequences:
- `O128` Vendor.Sympathizes | min=0.5 | fallback=False
  - write-back `W84`
  - write-back `W85`

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

### R02 - RuleNode

- Rule ID: pressure_mid
- Consequent: 0.35

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

### R06 - RuleNode

- Rule ID: pressure_vendor_nervous
- Consequent: 0.8

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

### E04 - EventNode

- Event ID: PickFlowers

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

### R17 - RuleNode

- Rule ID: vendor_first_meeting
- Consequent: 0.1

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

### E13 - EventNode

- Event ID: PickCoins

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

### E19 - EventNode

- Event ID: NoticeVendorReaction

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

### E20 - EventNode

- Event ID: VendorPitJump

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
- Payload: Pit.RevealedQuietly

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

### R52 - RuleNode

- Rule ID: tree_hollow_search
- Consequent: 1

### O81 - ConsequenceNode

- Outcome ID: TreeHollow.SearchDNE
- Minimum: 0
- Fallback: True
- Fire Event: False

### W51 - WriteBackNode

- Target Key: Player.HasTreeKey
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

### R54 - RuleNode

- Rule ID: enter_vault_accepted
- Consequent: 1

### R55 - RuleNode

- Rule ID: entry_vault_denied
- Consequent: 0.5

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

### O87 - ConsequenceNode

- Outcome ID: EnterVault.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### W55 - WriteBackNode

- Target Key: Guard.PursuePlayer
- Operation: Set
- Value Type: Bool
- Value: True

### E36 - EventNode

- Event ID: VaultLockSolved

### R56 - RuleNode

- Rule ID: vault_lock_solved
- Consequent: 1

### O88 - ConsequenceNode

- Outcome ID: Vault.LockSolved
- Minimum: 1
- Fallback: False
- Fire Event: False

### W56 - WriteBackNode

- Target Key: Vault.PuzzleSolved
- Operation: Set
- Value Type: Bool
- Value: True

### O89 - ConsequenceNode

- Outcome ID: Vault.LockSolvedFallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### E37 - EventNode

- Event ID: InspectVaultHinges

### R57 - RuleNode

- Rule ID: inspect_vault_hinges
- Consequent: 1

### O90 - ConsequenceNode

- Outcome ID: Vault.HingesInspected
- Minimum: 1
- Fallback: False
- Fire Event: False

### W57 - WriteBackNode

- Target Key: Player.HingesData
- Operation: Set
- Value Type: Bool
- Value: True

### O91 - ConsequenceNode

- Outcome ID: InspectVaultHinges.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### E38 - EventNode

- Event ID: UseTreeKey

### R58 - RuleNode

- Rule ID: use_tree_key
- Consequent: 1

### O92 - ConsequenceNode

- Outcome ID: Vault.TreeKeyAccepted
- Minimum: 1
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Vault.OpenDoor

### W58 - WriteBackNode

- Target Key: Vault.PuzzleSolved
- Operation: Set
- Value Type: Bool
- Value: True

### O93 - ConsequenceNode

- Outcome ID: UseTreeKey.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### E39 - EventNode

- Event ID: VaultDoorInteract

### R59 - RuleNode

- Rule ID: vault_door_interact
- Consequent: 1

### O94 - ConsequenceNode

- Outcome ID: Vault.DoorInteraction
- Minimum: 1
- Fallback: False
- Fire Event: True
- Target: DialogueGraph
- Payload: VaultDoor

### O95 - ConsequenceNode

- Outcome ID: VaultDoorInteract.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### E40 - EventNode

- Event ID: ChooseVaultCombination

### R60 - RuleNode

- Rule ID: choose_vault_combination
- Consequent: 1

### O96 - ConsequenceNode

- Outcome ID: Vault.CombinationChosen
- Minimum: 1
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Vault.BeginLock

### O97 - ConsequenceNode

- Outcome ID: VaultCombi.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### W59 - WriteBackNode

- Target Key: Vault.CanInspectHinges
- Operation: Set
- Value Type: Bool
- Value: True

### W60 - WriteBackNode

- Target Key: Vault.CanInspectHinges
- Operation: Set
- Value Type: Bool
- Value: False

### E41 - EventNode

- Event ID: VaultLockFailed

### R61 - RuleNode

- Rule ID: vault_lock_failed
- Consequent: 1

### O98 - ConsequenceNode

- Outcome ID: Vault.LockFailed
- Minimum: 1
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Vault.GuardAlerted

### W61 - WriteBackNode

- Target Key: Vault.AlarmTriggered
- Operation: Set
- Value Type: Bool
- Value: True

### W62 - WriteBackNode

- Target Key: Guard.Alerted
- Operation: Set
- Value Type: Bool
- Value: True

### O99 - ConsequenceNode

- Outcome ID: VaultLockFailed.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### E42 - EventNode

- Event ID: VaultDoorNoiseCheck

### R62 - RuleNode

- Rule ID: door_opened_carefully
- Consequent: 0.2

### O100 - ConsequenceNode

- Outcome ID: Vault.DoorOpenedQuietly
- Minimum: 0
- Fallback: False
- Fire Event: False

### W63 - WriteBackNode

- Target Key: Vault.DoorMadeNoise
- Operation: Set
- Value Type: Bool
- Value: False

### R63 - RuleNode

- Rule ID: Vault.DoorOpenedNoisily
- Consequent: 0.8

### O101 - ConsequenceNode

- Outcome ID: Vault.DoorOpenedNoisily
- Minimum: 0.5
- Fallback: False
- Fire Event: False

### O102 - ConsequenceNode

- Outcome ID: VaultDoorNoiseCheck.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### W64 - WriteBackNode

- Target Key: Vault.DoorMadeNoise
- Operation: Set
- Value Type: Bool
- Value: True

### E43 - EventNode

- Event ID: VaultAlertCheck

### R64 - RuleNode

- Rule ID: vault_security_alert
- Consequent: 1

### W65 - WriteBackNode

- Target Key: Vault.AlarmTriggered
- Operation: Set
- Value Type: Bool
- Value: False

### O103 - ConsequenceNode

- Outcome ID: Vault.SecurityAlerted
- Minimum: 1
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Vault.GuardAlerted

### W66 - WriteBackNode

- Target Key: Guard.Alerted
- Operation: Set
- Value Type: Bool
- Value: True

### O104 - ConsequenceNode

- Outcome ID: Vault.SecurityCalm
- Minimum: 0
- Fallback: True
- Fire Event: False

### E44 - EventNode

- Event ID: PickupGuardDisguise

### R65 - RuleNode

- Rule ID: pickup_guard_disguise
- Consequent: 1

### O105 - ConsequenceNode

- Outcome ID: Player.TakesGuardDisguise
- Minimum: 1
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Disguise.Picked

### W67 - WriteBackNode

- Target Key: Player.HasDisguise
- Operation: Set
- Value Type: Bool
- Value: True

### O106 - ConsequenceNode

- Outcome ID: PickupGuardDisguise.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### E45 - EventNode

- Event ID: StealVaultFruit

### R66 - RuleNode

- Rule ID: steal_vault_fruit
- Consequent: 1

### O107 - ConsequenceNode

- Outcome ID: Player.TakesVaultFruit
- Minimum: 1
- Fallback: False
- Fire Event: False

### W68 - WriteBackNode

- Target Key: Player.HasFruit
- Operation: Set
- Value Type: Bool
- Value: True

### O108 - ConsequenceNode

- Outcome ID: StealVaultFruit.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### E46 - EventNode

- Event ID: ExitVault

### R67 - RuleNode

- Rule ID: exit_vault
- Consequent: 1

### O109 - ConsequenceNode

- Outcome ID: Vault.PlayerExits
- Minimum: 1
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Vault.Exit

### W69 - WriteBackNode

- Target Key: Player.ExitedVault
- Operation: Set
- Value Type: Bool
- Value: True

### O110 - ConsequenceNode

- Outcome ID: ExitVault.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: True
- Target: DialogueGraph
- Payload: StealReminder

### E47 - EventNode

- Event ID: FinalGuardReaction

### R68 - RuleNode

- Rule ID: guard_accepts_deception
- Consequent: 0.2

### R69 - RuleNode

- Rule ID: guard_rejects_deception
- Consequent: 1

### O111 - ConsequenceNode

- Outcome ID: Guard.DeceptionAccepted
- Minimum: 0
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Vault.GuardLeaves

### O112 - ConsequenceNode

- Outcome ID: Guard.DeceptionRejected
- Minimum: 0.5
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Vault.GuardChase

### O113 - ConsequenceNode

- Outcome ID: FinalGuardReaction.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: True
- Target: GameplaySignal
- Payload: Vault.GuardChase

### W70 - WriteBackNode

- Target Key: Guard.LeftVault
- Operation: Set
- Value Type: Bool
- Value: True

### W71 - WriteBackNode

- Target Key: Guard.PursuePlayer
- Operation: Set
- Value Type: Bool
- Value: True

### W72 - WriteBackNode

- Target Key: Guard.PursuePlayer
- Operation: Set
- Value Type: Bool
- Value: True

### E48 - EventNode

- Event ID: NewKeyPicked

### R70 - RuleNode

- Rule ID: new_key
- Consequent: 1

### O114 - ConsequenceNode

- Outcome ID: NewKeyacquired
- Minimum: 1
- Fallback: False
- Fire Event: False

### W73 - WriteBackNode

- Target Key: Player.HasTreeKey
- Operation: Set
- Value Type: Bool
- Value: True

### O115 - ConsequenceNode

- Outcome ID: NewKeyFallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### E49 - EventNode

- Event ID: GuardFellIntoPit

### R71 - RuleNode

- Rule ID: rule_id
- Consequent: 1

### O116 - ConsequenceNode

- Outcome ID: Guard.Unavailable
- Minimum: 1
- Fallback: False
- Fire Event: False

### W74 - WriteBackNode

- Target Key: Guard.InsidePit
- Operation: Set
- Value Type: Bool
- Value: True

### E50 - EventNode

- Event ID: CheckVaultExit

### R72 - RuleNode

- Rule ID: vault_exit_safe
- Consequent: 1

### O117 - ConsequenceNode

- Outcome ID: Vault.GateExit
- Minimum: 1
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Vault.GateExit

### O118 - ConsequenceNode

- Outcome ID: CheckVaultExit.Blocked
- Minimum: 0
- Fallback: True
- Fire Event: True
- Target: DialogueGraph
- Payload: UnableToExit

### O119 - ConsequenceNode

- Outcome ID: GuardFellIntoPit
- Minimum: 0
- Fallback: True
- Fire Event: False

### W75 - WriteBackNode

- Target Key: Player.UsedVaultGateExit
- Operation: Set
- Value Type: Bool
- Value: True

### E51 - EventNode

- Event ID: BazaarReturnCheck

### R73 - RuleNode

- Rule ID: bazaar_return_from_vault_gate
- Consequent: 1

### O120 - ConsequenceNode

- Outcome ID: Bazaar.ReturnFromVaultGate
- Minimum: 1
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Bazaar.ReturnFromVaultGate

### O121 - ConsequenceNode

- Outcome ID: Bazaar.ReturnNormal
- Minimum: 0
- Fallback: True
- Fire Event: False

### C01 - CriterionNodeV2

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### C02 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Player.HasFlowers

### C03 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.FoundPitRoute
- Expected Bool: True

### C04 - CriterionNodeV2

- Mode: NumberCompare
- Variable ID: Guard.Suspicion
- Comparison: GreaterThanOrEqual
- Compare A: 20

### C05 - CriterionNodeV2

- Mode: FuzzyNumber
- Variable ID: Vendor.Nervousness
- Set: VendorReactionNotice
- Shape: High
- Minimum: 0
- Maximum: 10
- Point A: 3
- Point B: 6

### C06 - CriterionNodeV2

- Mode: And

### C07 - CriterionNodeV2

- Mode: Or

### C08 - CriterionNodeV2

- Mode: Not

### C09 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Vault.PuzzleSolved

### C10 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.HasTreeKey
- Expected Bool: True

### C11 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Player.HasFruit

### C12 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Guard.InsidePit

### C13 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Guard.InsidePit
- Expected Bool: True

### C14 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.HasFruit
- Expected Bool: True

### C15 - CriterionNodeV2

- Mode: And

### C16 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Player.HasCoins

### C17 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Vault.PuzzleSolved

### C18 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Player.HasDisguise

### C19 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.HasFruit
- Expected Bool: True

### C20 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Guard.InsidePit

### C21 - CriterionNodeV2

- Mode: And

### C22 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Player.HingesData

### C23 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Vault.PuzzleSolved

### C24 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Vault.DoorMadeNoise
- Expected Bool: True

### C25 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Vault.AlarmTriggered
- Expected Bool: True

### C26 - CriterionNodeV2

- Mode: Or

### C27 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Player.HasTreeKey

### C28 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.HasDisguise
- Expected Bool: True

### C29 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Player.HasFruit

### C30 - CriterionNodeV2

- Mode: And

### C31 - CriterionNodeV2

- Mode: NumberCompare
- Variable ID: Guard.Relationship
- Comparison: InclusiveRange
- Compare A: 10
- Compare B: 30

### C32 - CriterionNodeV2

- Mode: And

### C33 - CriterionNodeV2

- Mode: Exists
- Variable ID: Vault.AlarmTriggered

### C34 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Vault.CanInspectHinges
- Expected Bool: True

### C35 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Guard.InsidePit
- Expected Bool: True

### C36 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.UsedVaultGateExit
- Expected Bool: True

### C37 - CriterionNodeV2

- Mode: And

### C38 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.HasFlowers
- Expected Bool: True

### C39 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Guard.HasMetPlayer
- Expected Bool: True

### C40 - CriterionNodeV2

- Mode: And

### C41 - CriterionNodeV2

- Mode: NumberCompare
- Variable ID: Guard.TalkCount
- Comparison: LessThanOrEqual
- Compare A: 2

### C42 - CriterionNodeV2

- Mode: And

### C43 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Guard.HasMetPlayer

### C44 - CriterionNodeV2

- Mode: FuzzyNumber
- Variable ID: Guard.Suspicion
- Set: High
- Shape: High
- Minimum: 0
- Maximum: 40
- Point A: 10
- Point B: 20

### C45 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Guard.AllowedEntry
- Expected Bool: False

### C46 - CriterionNodeV2

- Mode: And

### C47 - CriterionNodeV2

- Mode: FuzzyNumber
- Variable ID: Guard.TalkCount
- Set: High
- Shape: High
- Minimum: 0
- Maximum: 10
- Point A: 2
- Point B: 4

### C48 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Guard.AllowedEntry
- Expected Bool: False

### C49 - CriterionNodeV2

- Mode: And

### C50 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Guard.HasMetPlayer
- Expected Bool: True

### C51 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Guard.AllowedEntry
- Expected Bool: False

### C52 - CriterionNodeV2

- Mode: And

### C53 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Guard.AllowedEntry
- Expected Bool: True

### C54 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.HasFlowers
- Expected Bool: True

### C55 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Guard.AllowedEntry
- Expected Bool: False

### C56 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Guard.AllowedEntry

### C57 - CriterionNodeV2

- Mode: Or

### C58 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Guard.PursuePlayer
- Expected Bool: True

### C59 - CriterionNodeV2

- Mode: Or

### C60 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.HasCoins
- Expected Bool: True

### C61 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.InspectedTreeHollow
- Expected Bool: True

### C62 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Player.HasTreeKey

### C63 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Player.HasTreeKey

### C64 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Guard.AllowedEntry
- Expected Bool: True

### C65 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Guard.AllowedEntry

### C66 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Guard.AllowedEntry
- Expected Bool: False

### C67 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Vendor.HasMetPlayer

### C68 - CriterionNodeV2

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### C69 - CriterionNodeV2

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### C70 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.HasCoins
- Expected Bool: True

### C71 - CriterionNodeV2

- Mode: And

### C72 - CriterionNodeV2

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### C73 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.HasFlowers
- Expected Bool: True

### C74 - CriterionNodeV2

- Mode: And

### C75 - CriterionNodeV2

- Mode: Exists
- Variable ID: Vendor.WasBribed

### C76 - CriterionNodeV2

- Mode: FuzzyNumber
- Variable ID: Vendor.Nervousness
- Set: NervousnessLow
- Shape: Low
- Minimum: 0
- Maximum: 10
- Point A: 2
- Point B: 5

### C77 - CriterionNodeV2

- Mode: FuzzyNumber
- Variable ID: Vendor.Suspicion
- Set: SuspicionLow
- Shape: Low
- Minimum: 0
- Maximum: 10
- Point A: 2
- Point B: 6

### C78 - CriterionNodeV2

- Mode: And

### C79 - CriterionNodeV2

- Mode: FuzzyNumber
- Variable ID: Vendor.Nervousness
- Set: NervousnessHigh
- Shape: High
- Minimum: 0
- Maximum: 10
- Point A: 3
- Point B: 6

### C80 - CriterionNodeV2

- Mode: FuzzyNumber
- Variable ID: Vendor.Suspicion
- Set: SuspicionHigh
- Shape: High
- Minimum: 0
- Maximum: 10
- Point A: 5
- Point B: 8

### C81 - CriterionNodeV2

- Mode: And

### C82 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.NoticedVendorReaction
- Expected Bool: True

### C83 - CriterionNodeV2

- Mode: Exists
- Variable ID: Vendor.WasBribed

### C84 - CriterionNodeV2

- Mode: Exists
- Variable ID: Vendor.WasBribed

### C85 - CriterionNodeV2

- Mode: FuzzyNumber
- Variable ID: Player.DistanceToCarpet
- Set: Near
- Shape: Low
- Minimum: 0
- Maximum: 10
- Point A: 0
- Point B: 4

### C86 - CriterionNodeV2

- Mode: And

### C87 - CriterionNodeV2

- Mode: FuzzyNumber
- Variable ID: Player.LoiterTimeNearCarpet
- Set: Lingering
- Shape: High
- Minimum: 0
- Maximum: 14.9
- Point A: 3
- Point B: 10

### C88 - CriterionNodeV2

- Mode: FuzzyNumber
- Variable ID: Vendor.Nervousness
- Set: Nervous
- Shape: High
- Minimum: 0
- Maximum: 10
- Point A: 3
- Point B: 6

### C89 - CriterionNodeV2

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

### C90 - CriterionNodeV2

- Mode: FuzzyNumber
- Variable ID: Player.DistanceToCarpet
- Set: Far
- Shape: High
- Minimum: 0
- Maximum: 10
- Point A: 6
- Point B: 10

### C91 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.FoundPitRoute
- Expected Bool: True

### C92 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Vendor.HasMetPlayer
- Expected Bool: True

### C93 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Vendor.HasMetPlayer
- Expected Bool: True

### C94 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.FoundPitRoute
- Expected Bool: True

### C95 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Player.FoundPitRoute
- Expected Bool: True

### C96 - CriterionNodeV2

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### C97 - CriterionNodeV2

- Mode: Exists
- Variable ID:  Vendor.HasMetPlayer

### C98 - CriterionNodeV2

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### C99 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Guard.PursuePlayer

### C100 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Guard.PursuePlayer
- Expected Bool: False

### C101 - CriterionNodeV2

- Mode: Or

### C102 - CriterionNodeV2

- Mode: And

### W76 - WriteBackNode

- Target Key: Player.HasCoins
- Operation: Set
- Value Type: Bool
- Value: True

### E52 - EventNode

- Event ID: VendorChoiceRevealPit

### C103 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Vendor.HasMetPlayer
- Expected Bool: True

### C104 - CriterionNodeV2

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### C105 - CriterionNodeV2

- Mode: Or

### R74 - RuleNode

- Rule ID: vendor_choice_reveals_pit
- Consequent: 1

### O122 - ConsequenceNode

- Outcome ID: Vendor.ChoiceRevealsPit
- Minimum: 0.5
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Pit.VendorChoiceReveal

### W77 - WriteBackNode

- Target Key: Player.FoundPitRoute
- Operation: Set
- Value Type: Bool
- Value: True

### O123 - ConsequenceNode

- Outcome ID: VendorChoiceRevealPit.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### W78 - WriteBackNode

- Target Key: Player.NoticedVendorReaction
- Operation: Set
- Value Type: Bool
- Value: True

### W79 - WriteBackNode

- Target Key: Player.NoticedVendorReaction
- Operation: Set
- Value Type: Bool
- Value: True

### E53 - EventNode

- Event ID: VendorDirectVaultAsk

### C106 - CriterionNodeV2

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### R75 - RuleNode

- Rule ID: vendor_direct_vault_ask
- Consequent: 0.55

### O124 - ConsequenceNode

- Outcome ID: Vendor.DirectVaultAsk
- Minimum: 0.5
- Fallback: False
- Fire Event: False

### W80 - WriteBackNode

- Target Key: Vendor.Suspicion
- Operation: Add
- Value Type: Float
- Value: 4

### W81 - WriteBackNode

- Target Key: Vendor.Nervousness
- Operation: Add
- Value Type: Float
- Value: 2

### O125 - ConsequenceNode

- Outcome ID: VendorDirectVaultAsk.Fallback
- Minimum: 0
- Fallback: True
- Fire Event: False

### E54 - EventNode

- Event ID: VendorSplitDeal

### C107 - CriterionNodeV2

- Mode: Exists
- Variable ID: Vendor.HasMetPlayer

### R76 - RuleNode

- Rule ID: vendor_split_deal
- Consequent: 0.65

### O126 - ConsequenceNode

- Outcome ID: Vendor.SplitDealAccepted
- Minimum: 0.5
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Pit.VendorChoiceReveal

### W82 - WriteBackNode

- Target Key: Vendor.SplitDealOffered
- Operation: Set
- Value Type: Bool
- Value: True

### O127 - ConsequenceNode

- Outcome ID: VendorSplitDeal
- Minimum: 0
- Fallback: True
- Fire Event: False

### C108 - CriterionNodeV2

- Mode: DoesNotExist
- Variable ID: Player.FoundPitRoute

### C109 - CriterionNodeV2

- Mode: And

### W83 - WriteBackNode

- Target Key: Player.FoundPitRoute
- Operation: Set
- Value Type: Bool
- Value: True

### E55 - EventNode

- Event ID: VendorGuardGrudge

### C110 - CriterionNodeV2

- Mode: BoolEquals
- Variable ID: Guard.DismissedPlayer
- Expected Bool: True

### C111 - CriterionNodeV2

- Mode: Or

### C112 - CriterionNodeV2

- Mode: Exists
- Variable ID: Guard.DismissedPlayer

### R77 - RuleNode

- Rule ID: vendor_guard_grudge
- Consequent: 0.75

### O128 - ConsequenceNode

- Outcome ID: Vendor.Sympathizes
- Minimum: 0.5
- Fallback: False
- Fire Event: True
- Target: GameplaySignal
- Payload: Pit.VendorChoiceReveal

### W84 - WriteBackNode

- Target Key: Vendor.SharedGuardGrudge
- Operation: Set
- Value Type: Bool
- Value: True

### W85 - WriteBackNode

- Target Key: Player.FoundPitRoute
- Operation: Set
- Value Type: Bool
- Value: True

## Every connection

- `E01.consequences` -> `O04.event`
- `E01.consequences` -> `O02.event`
- `E01.consequences` -> `O03.event`
- `E01.consequences` -> `O01.event`
- `E01.rules` -> `R34.event`
- `O01.writeBacks` -> `W01.consequence`
- `E02.consequences` -> `O05.event`
- `E02.consequences` -> `O07.event`
- `E02.consequences` -> `O08.event`
- `E02.consequences` -> `O06.event`
- `E02.consequences` -> `O09.event`
- `E02.rules` -> `R02.event`
- `E02.rules` -> `R01.event`
- `E02.rules` -> `R03.event`
- `E02.rules` -> `R04.event`
- `E02.rules` -> `R07.event`
- `E02.rules` -> `R05.event`
- `E02.rules` -> `R06.event`
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
- `E04.consequences` -> `O13.event`
- `E04.consequences` -> `O14.event`
- `E04.rules` -> `R10.event`
- `O13.writeBacks` -> `W05.consequence`
- `E05.consequences` -> `O16.event`
- `E05.consequences` -> `O17.event`
- `E05.rules` -> `R12.event`
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
- `O26.writeBacks` -> `W18.consequence`
- `O27.writeBacks` -> `W16.consequence`
- `O27.writeBacks` -> `W17.consequence`
- `O27.writeBacks` -> `W32.consequence`
- `O27.writeBacks` -> `W33.consequence`
- `E11.consequences` -> `O29.event`
- `E11.consequences` -> `O30.event`
- `E11.rules` -> `R19.event`
- `O29.writeBacks` -> `W19.consequence`
- `O29.writeBacks` -> `W20.consequence`
- `E12.consequences` -> `O31.event`
- `E12.consequences` -> `O32.event`
- `E12.rules` -> `R20.event`
- `O31.writeBacks` -> `W21.consequence`
- `O31.writeBacks` -> `W22.consequence`
- `E13.consequences` -> `O33.event`
- `E13.consequences` -> `O34.event`
- `E13.rules` -> `R21.event`
- `O33.writeBacks` -> `W23.consequence`
- `E14.consequences` -> `O35.event`
- `E14.consequences` -> `O36.event`
- `E14.rules` -> `R22.event`
- `O35.writeBacks` -> `W24.consequence`
- `O35.writeBacks` -> `W25.consequence`
- `E15.consequences` -> `O37.event`
- `E15.consequences` -> `O38.event`
- `E15.rules` -> `R23.event`
- `O37.writeBacks` -> `W26.consequence`
- `O37.writeBacks` -> `W27.consequence`
- `E16.consequences` -> `O39.event`
- `E16.consequences` -> `O40.event`
- `E16.rules` -> `R24.event`
- `O39.writeBacks` -> `W28.consequence`
- `O39.writeBacks` -> `W29.consequence`
- `E17.consequences` -> `O41.event`
- `E17.consequences` -> `O42.event`
- `E17.rules` -> `R25.event`
- `O41.writeBacks` -> `W30.consequence`
- `O41.writeBacks` -> `W31.consequence`
- `E18.consequences` -> `O43.event`
- `E18.consequences` -> `O44.event`
- `E18.consequences` -> `O45.event`
- `E18.consequences` -> `O46.event`
- `E18.rules` -> `R26.event`
- `E18.rules` -> `R28.event`
- `E18.rules` -> `R27.event`
- `O44.writeBacks` -> `W34.consequence`
- `O45.writeBacks` -> `W35.consequence`
- `E19.consequences` -> `O47.event`
- `E19.consequences` -> `O48.event`
- `E19.rules` -> `R29.event`
- `O47.writeBacks` -> `W36.consequence`
- `E20.consequences` -> `O49.event`
- `E20.consequences` -> `O50.event`
- `E20.rules` -> `R30.event`
- `O49.writeBacks` -> `W53.consequence`
- `E21.consequences` -> `O51.event`
- `E21.consequences` -> `O52.event`
- `E21.rules` -> `R31.event`
- `E22.consequences` -> `O53.event`
- `E22.consequences` -> `O54.event`
- `E22.rules` -> `R32.event`
- `O53.writeBacks` -> `W37.consequence`
- `E23.consequences` -> `O55.event`
- `E23.consequences` -> `O56.event`
- `E23.rules` -> `R33.event`
- `O55.writeBacks` -> `W38.consequence`
- `E24.consequences` -> `O57.event`
- `E24.consequences` -> `O58.event`
- `E24.rules` -> `R35.event`
- `O57.writeBacks` -> `W39.consequence`
- `O57.writeBacks` -> `W40.consequence`
- `O59.writeBacks` -> `W41.consequence`
- `E25.consequences` -> `O60.event`
- `E25.consequences` -> `O61.event`
- `E25.rules` -> `R37.event`
- `O60.writeBacks` -> `W42.consequence`
- `O60.writeBacks` -> `W43.consequence`
- `O60.writeBacks` -> `W44.consequence`
- `O62.writeBacks` -> `W45.consequence`
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
- `O66.writeBacks` -> `W78.consequence`
- `O67.writeBacks` -> `W79.consequence`
- `E27.consequences` -> `O69.event`
- `E27.consequences` -> `O70.event`
- `E27.rules` -> `R46.event`
- `O69.writeBacks` -> `W47.consequence`
- `E28.consequences` -> `O71.event`
- `E28.consequences` -> `O72.event`
- `E28.rules` -> `R47.event`
- `E29.consequences` -> `O73.event`
- `E29.consequences` -> `O74.event`
- `E29.rules` -> `R48.event`
- `O73.writeBacks` -> `W48.consequence`
- `E30.consequences` -> `O75.event`
- `E30.consequences` -> `O76.event`
- `E30.rules` -> `R49.event`
- `O75.writeBacks` -> `W49.consequence`
- `E31.consequences` -> `O77.event`
- `E31.consequences` -> `O78.event`
- `E31.rules` -> `R50.event`
- `O77.writeBacks` -> `W76.consequence`
- `E32.consequences` -> `O79.event`
- `E32.consequences` -> `O80.event`
- `E32.rules` -> `R51.event`
- `O79.writeBacks` -> `W50.consequence`
- `E33.consequences` -> `O81.event`
- `E33.consequences` -> `O82.event`
- `E33.rules` -> `R52.event`
- `O82.writeBacks` -> `W51.consequence`
- `O82.writeBacks` -> `W52.consequence`
- `E34.consequences` -> `O83.event`
- `E34.consequences` -> `O84.event`
- `E34.rules` -> `R53.event`
- `E35.consequences` -> `O85.event`
- `E35.consequences` -> `O86.event`
- `E35.consequences` -> `O87.event`
- `E35.rules` -> `R54.event`
- `E35.rules` -> `R55.event`
- `O85.writeBacks` -> `W54.consequence`
- `O86.writeBacks` -> `W55.consequence`
- `E36.consequences` -> `O88.event`
- `E36.consequences` -> `O89.event`
- `E36.rules` -> `R56.event`
- `O88.writeBacks` -> `W56.consequence`
- `E37.consequences` -> `O90.event`
- `E37.consequences` -> `O91.event`
- `E37.rules` -> `R57.event`
- `O90.writeBacks` -> `W57.consequence`
- `O90.writeBacks` -> `W60.consequence`
- `E38.consequences` -> `O92.event`
- `E38.consequences` -> `O93.event`
- `E38.rules` -> `R58.event`
- `O92.writeBacks` -> `W58.consequence`
- `E39.consequences` -> `O94.event`
- `E39.consequences` -> `O95.event`
- `E39.rules` -> `R59.event`
- `O94.writeBacks` -> `W59.consequence`
- `O94.writeBacks` -> `W65.consequence`
- `E40.consequences` -> `O96.event`
- `E40.consequences` -> `O97.event`
- `E40.rules` -> `R60.event`
- `E41.consequences` -> `O98.event`
- `E41.consequences` -> `O99.event`
- `E41.rules` -> `R61.event`
- `O98.writeBacks` -> `W61.consequence`
- `O98.writeBacks` -> `W62.consequence`
- `E42.consequences` -> `O100.event`
- `E42.consequences` -> `O101.event`
- `E42.consequences` -> `O102.event`
- `E42.rules` -> `R62.event`
- `E42.rules` -> `R63.event`
- `O100.writeBacks` -> `W63.consequence`
- `O101.writeBacks` -> `W64.consequence`
- `E43.consequences` -> `O103.event`
- `E43.consequences` -> `O104.event`
- `E43.rules` -> `R64.event`
- `O103.writeBacks` -> `W66.consequence`
- `E44.consequences` -> `O105.event`
- `E44.consequences` -> `O106.event`
- `E44.rules` -> `R65.event`
- `O105.writeBacks` -> `W67.consequence`
- `E45.consequences` -> `O107.event`
- `E45.consequences` -> `O108.event`
- `E45.rules` -> `R66.event`
- `O107.writeBacks` -> `W68.consequence`
- `E46.consequences` -> `O109.event`
- `E46.consequences` -> `O110.event`
- `E46.rules` -> `R67.event`
- `O109.writeBacks` -> `W69.consequence`
- `E47.consequences` -> `O111.event`
- `E47.consequences` -> `O112.event`
- `E47.consequences` -> `O113.event`
- `E47.rules` -> `R68.event`
- `E47.rules` -> `R69.event`
- `O111.writeBacks` -> `W70.consequence`
- `O112.writeBacks` -> `W71.consequence`
- `O113.writeBacks` -> `W72.consequence`
- `E48.consequences` -> `O114.event`
- `E48.consequences` -> `O115.event`
- `E48.rules` -> `R70.event`
- `O114.writeBacks` -> `W73.consequence`
- `E49.consequences` -> `O116.event`
- `E49.consequences` -> `O119.event`
- `E49.rules` -> `R71.event`
- `O116.writeBacks` -> `W74.consequence`
- `E50.consequences` -> `O117.event`
- `E50.consequences` -> `O118.event`
- `E50.rules` -> `R72.event`
- `O117.writeBacks` -> `W75.consequence`
- `E51.consequences` -> `O120.event`
- `E51.consequences` -> `O121.event`
- `E51.rules` -> `R73.event`
- `C01.rules` -> `R20.criteria`
- `C02.rules` -> `R10.criteria`
- `C03.rules` -> `R30.criteria`
- `C04.rules` -> `R69.criteria`
- `C05.rules` -> `R29.criteria`
- `C06.rules` -> `R44.criteria`
- `C06.rules` -> `R05.criteria`
- `C07.rules` -> `R55.criteria`
- `C08.rules` -> `R62.criteria`
- `C09.rules` -> `R56.criteria`
- `C10.rules` -> `R58.criteria`
- `C11.rules` -> `R66.criteria`
- `C12.rules` -> `R71.criteria`
- `C13.rules` -> `C15.criteriaA`
- `C14.rules` -> `C15.criteriaB`
- `C15.rules` -> `R72.criteria`
- `C16.rules` -> `R21.criteria`
- `C17.rules` -> `R59.criteria`
- `C18.rules` -> `R65.criteria`
- `C19.rules` -> `C21.criteriaA`
- `C20.rules` -> `C21.criteriaB`
- `C21.rules` -> `R67.criteria`
- `C22.rules` -> `R57.criteria`
- `C23.rules` -> `R60.criteria`
- `C24.rules` -> `C26.criteriaA`
- `C25.rules` -> `C26.criteriaB`
- `C26.rules` -> `C102.criteriaA`
- `C27.rules` -> `R70.criteria`
- `C28.rules` -> `C30.criteriaA`
- `C29.rules` -> `C30.criteriaB`
- `C30.rules` -> `C32.criteriaA`
- `C31.rules` -> `C32.criteriaB`
- `C32.rules` -> `R68.criteria`
- `C33.rules` -> `R61.criteria`
- `C34.rules` -> `R63.criteria`
- `C34.rules` -> `C08.criteriaA`
- `C35.rules` -> `C37.criteriaA`
- `C36.rules` -> `C37.criteriaB`
- `C37.rules` -> `R73.criteria`
- `C38.rules` -> `C40.criteriaA`
- `C39.rules` -> `C40.criteriaB`
- `C40.rules` -> `C42.criteriaA`
- `C41.rules` -> `C42.criteriaB`
- `C42.rules` -> `R11.criteria`
- `C43.rules` -> `R08.criteria`
- `C44.rules` -> `C46.criteriaA`
- `C45.rules` -> `C46.criteriaB`
- `C46.rules` -> `R38.criteria`
- `C47.rules` -> `C49.criteriaA`
- `C48.rules` -> `C49.criteriaB`
- `C49.rules` -> `R36.criteria`
- `C50.rules` -> `C52.criteriaA`
- `C51.rules` -> `C52.criteriaB`
- `C52.rules` -> `R09.criteria`
- `C53.rules` -> `R39.criteria`
- `C54.rules` -> `R15.criteria`
- `C54.rules` -> `R16.criteria`
- `C54.rules` -> `R13.criteria`
- `C54.rules` -> `R12.criteria`
- `C54.rules` -> `R14.criteria`
- `C55.rules` -> `C57.criteriaB`
- `C56.rules` -> `C57.criteriaA`
- `C57.rules` -> `C59.criteriaA`
- `C58.rules` -> `C59.criteriaB`
- `C59.rules` -> `R35.criteria`
- `C60.rules` -> `R37.criteria`
- `C61.rules` -> `R53.criteria`
- `C62.rules` -> `R52.criteria`
- `C63.rules` -> `R51.criteria`
- `C64.rules` -> `R54.criteria`
- `C65.rules` -> `C07.criteriaA`
- `C66.rules` -> `C07.criteriaB`
- `C67.rules` -> `R17.criteria`
- `C68.rules` -> `R18.criteria`
- `C69.rules` -> `C71.criteriaA`
- `C70.rules` -> `C71.criteriaB`
- `C71.rules` -> `R22.criteria`
- `C72.rules` -> `C74.criteriaA`
- `C73.rules` -> `C74.criteriaB`
- `C74.rules` -> `R19.criteria`
- `C75.rules` -> `R23.criteria`
- `C76.rules` -> `C78.criteriaA`
- `C77.rules` -> `C78.criteriaB`
- `C77.rules` -> `C81.criteriaA`
- `C78.rules` -> `R26.criteria`
- `C79.rules` -> `C81.criteriaB`
- `C80.rules` -> `R28.criteria`
- `C81.rules` -> `R27.criteria`
- `C82.rules` -> `R34.criteria`
- `C83.rules` -> `R24.criteria`
- `C84.rules` -> `R25.criteria`
- `C85.rules` -> `C06.criteriaA`
- `C85.rules` -> `R01.criteria`
- `C85.rules` -> `R40.criteria`
- `C86.rules` -> `R07.criteria`
- `C86.rules` -> `R45.criteria`
- `C87.rules` -> `C06.criteriaB`
- `C87.rules` -> `R04.criteria`
- `C87.rules` -> `C86.criteriaA`
- `C87.rules` -> `R43.criteria`
- `C88.rules` -> `C86.criteriaB`
- `C88.rules` -> `R06.criteria`
- `C89.rules` -> `R02.criteria`
- `C89.rules` -> `R41.criteria`
- `C90.rules` -> `R03.criteria`
- `C90.rules` -> `R42.criteria`
- `C91.rules` -> `R32.criteria`
- `C92.rules` -> `R50.criteria`
- `C93.rules` -> `R49.criteria`
- `C94.rules` -> `R31.criteria`
- `C95.rules` -> `R33.criteria`
- `C96.rules` -> `R46.criteria`
- `C97.rules` -> `R47.criteria`
- `C98.rules` -> `R48.criteria`
- `C99.rules` -> `C101.criteriaA`
- `C100.rules` -> `C101.criteriaB`
- `C101.rules` -> `C102.criteriaB`
- `C102.rules` -> `R64.criteria`
- `E52.consequences` -> `O122.event`
- `E52.consequences` -> `O123.event`
- `E52.rules` -> `R74.event`
- `C103.rules` -> `C105.criteriaB`
- `C104.rules` -> `C105.criteriaA`
- `C105.rules` -> `R74.criteria`
- `O122.writeBacks` -> `W77.consequence`
- `E53.consequences` -> `O124.event`
- `E53.consequences` -> `O125.event`
- `E53.rules` -> `R75.event`
- `C106.rules` -> `R75.criteria`
- `O124.writeBacks` -> `W80.consequence`
- `O124.writeBacks` -> `W81.consequence`
- `E54.consequences` -> `O126.event`
- `E54.consequences` -> `O127.event`
- `E54.rules` -> `R76.event`
- `C107.rules` -> `C109.criteriaA`
- `O126.writeBacks` -> `W82.consequence`
- `O126.writeBacks` -> `W83.consequence`
- `C108.rules` -> `C109.criteriaB`
- `C109.rules` -> `R76.criteria`
- `E55.consequences` -> `O128.event`
- `E55.rules` -> `R77.event`
- `C110.rules` -> `C111.criteriaB`
- `C111.rules` -> `R77.criteria`
- `C112.rules` -> `C111.criteriaA`
- `O128.writeBacks` -> `W84.consequence`
- `O128.writeBacks` -> `W85.consequence`

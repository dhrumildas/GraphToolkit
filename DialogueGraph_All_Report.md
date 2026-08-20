# Dialogue Graph Audit

Generated: 20-08-2026 19:36:47
Graphs found: 27

========================================
## TreeHollowDiscovery
========================================

Path: `Assets/DialogueSystem/Graphs/Env/TreeHollowDiscovery.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `9061c982-3723-4103-bf83-760e1ee004ae`
- Speaker: `Player`
- Dialogue: `There's something wedged inside the hollow.`
- Next: D02

#### D02 - Dialogue

- Runtime Node ID: `2d085bb0-bfba-4635-b57c-3d8747952565`
- Speaker: `Player`
- Dialogue: `I could reach it... but the guard is looking straight at me.`
- Next: C01

#### C01 - Choice

- Runtime Node ID: `e194f8ab-0083-424e-81aa-0ffbb885dc8b`
- Speaker: `Player`
- Dialogue: <none>
- Choices:
  - Choice 0: `[Search Anyway]`
    - Fuzzy Event ID: `SearchTreeHollow`
    - Required Bool Key: <none>
    - Destination: <END/EXTERNAL>
  - Choice 1: `[Let it go]`
    - Fuzzy Event ID: `LeaveTreeHollow`
    - Required Bool Key: <none>
    - Destination: D03

#### D03 - Dialogue

- Runtime Node ID: `30652394-6c4d-4e3c-a3d8-51b6a4c22a46`
- Speaker: `Player`
- Dialogue: `Eh... maybe just trash.`
- Next: <END/EXTERNAL>

### Flow

- D01 --> D02
- D02 --> C01
- C01 --[[Search Anyway]]--> <END/EXTERNAL>
- C01 --[[Let it go]]--> D03
- D03 --> <END/EXTERNAL>


========================================
## TreeKeyFound
========================================

Path: `Assets/DialogueSystem/Graphs/Env/TreeKeyFound.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `2cba393c-b695-4800-ad22-b12d8d969907`
- Speaker: `Player`
- Dialogue: `What's this?`
- Next: D02

#### D02 - Dialogue

- Runtime Node ID: `13042d81-6c84-470a-a73a-c11492948be2`
- Speaker: `Player`
- Dialogue: `A key?`
- Next: D03

#### D03 - Dialogue

- Runtime Node ID: `7a16269b-8391-4191-823d-ce69bb1db843`
- Speaker: `Player`
- Dialogue: `Could come in handy I suppose.`
- Next: <END/EXTERNAL>

### Flow

- D01 --> D02
- D02 --> D03
- D03 --> <END/EXTERNAL>


========================================
## GuardAllowedRepeat
========================================

Path: `Assets/DialogueSystem/Graphs/GuardAllowedRepeat.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `763e0fd4-8c4c-44eb-b350-006b1c5c09ac`
- Speaker: `Guard`
- Dialogue: `Go on! Go inside!`
- Next: D02

#### D02 - Dialogue

- Runtime Node ID: `5cde4a03-c876-4a69-9be9-cde2fb3950d1`
- Speaker: `Guard`
- Dialogue: `I will wait outside.`
- Next: D03

#### D03 - Dialogue

- Runtime Node ID: `035c12ee-d4a8-4616-b1c5-8bc4e4daff5f`
- Speaker: `Guard`
- Dialogue: `For you...`
- Next: D04

#### D04 - Dialogue

- Runtime Node ID: `76df91a1-7c03-4e67-94ea-d5f4082b40f6`
- Speaker: `Guard`
- Dialogue: `Mon cher 💕`
- Next: <END/EXTERNAL>

### Flow

- D01 --> D02
- D02 --> D03
- D03 --> D04
- D04 --> <END/EXTERNAL>


========================================
## GuardArrestsPlayer
========================================

Path: `Assets/DialogueSystem/Graphs/GuardArrestsPlayer.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `dcf32e96-73e6-4c7b-af1f-757d6b7161b5`
- Speaker: `Guard`
- Dialogue: `Awww Helll NAHH!!!`
- Next: <END/EXTERNAL>

### Flow

- D01 --> <END/EXTERNAL>


========================================
## GuardDismissesPlayer
========================================

Path: `Assets/DialogueSystem/Graphs/GuardDismissesPlayer.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `ca9273b2-b4ba-495e-9c8b-5293d70dc6d1`
- Speaker: `Guard`
- Dialogue: `One more and you are behind bars!`
- Next: <END/EXTERNAL>

#### D02 - Dialogue

- Runtime Node ID: `b1216305-d6a9-485d-9313-8d8867511102`
- Speaker: <none>
- Dialogue: <none>
- Next: <END>

### Flow

- D01 --> <END/EXTERNAL>


========================================
## GuardEndingCharmed
========================================

Path: `Assets/DialogueSystem/Graphs/GuardEndingCharmed.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `9ac6db2b-51d5-4ba7-9276-2f28272820e7`
- Speaker: `Guard`
- Dialogue: `Oh, you sure don't want to keep me waiting`
- Next: <END/EXTERNAL>

### Flow

- D01 --> <END/EXTERNAL>


========================================
## GuardEndingRude
========================================

Path: `Assets/DialogueSystem/Graphs/GuardEndingRude.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `2272bad1-364d-4409-89c8-fbb90e5eaa59`
- Speaker: `Guard`
- Dialogue: `Brother, this job doesnt pay me enough. Just go man. Pls`
- Next: <END/EXTERNAL>

### Flow

- D01 --> <END/EXTERNAL>


========================================
## GuardIntroduction
========================================

Path: `Assets/DialogueSystem/Graphs/GuardIntroduction.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `c0c6e24c-b111-4b0e-8be1-4aebe3ce0d7f`
- Speaker: `Guard`
- Dialogue: `Halt. What business do you have at the vault?`
- Next: C01

#### D02 - Dialogue

- Runtime Node ID: `78bd0a2a-28f6-4820-a176-3a68427c5d43`
- Speaker: <none>
- Dialogue: <none>
- Next: <END>

#### C01 - Choice

- Runtime Node ID: `2c15e105-0c92-4355-960a-54f5ff8c7a95`
- Speaker: `Player`
- Dialogue: `How do you respond?`
- Choices:
  - Choice 0: `I am only looking around.`
    - Fuzzy Event ID: <none>
    - Required Bool Key: <none>
    - Destination: D04
  - Choice 1: `None of your business.`
    - Fuzzy Event ID: <none>
    - Required Bool Key: <none>
    - Destination: D06
  - Choice 2: `I brought you something.`
    - Fuzzy Event ID: <none>
    - Required Bool Key: <none>
    - Destination: D07

#### D03 - Dialogue

- Runtime Node ID: `5c05618e-284c-4418-a455-a77bf95bd024`
- Speaker: <none>
- Dialogue: <none>
- Next: <END>

#### D04 - Dialogue

- Runtime Node ID: `59aee64f-1e3e-4aba-b2f9-87b3fbc296bd`
- Speaker: `Guard`
- Dialogue: `Then do not cause trouble.`
- Next: <END/EXTERNAL>

#### D05 - Dialogue

- Runtime Node ID: `29b5818f-5fb7-4158-84da-aad80aad4d1e`
- Speaker: <none>
- Dialogue: <none>
- Next: <END>

#### D06 - Dialogue

- Runtime Node ID: `ef6a1c86-5db6-4475-b71f-c248f3cb389a`
- Speaker: `Guard`
- Dialogue: `Watch your tone.`
- Next: <END/EXTERNAL>

#### D07 - Dialogue

- Runtime Node ID: `3f9d0733-29a1-4a0a-8914-2c295ed51869`
- Speaker: `Guard`
- Dialogue: `Not Interested`
- Next: <END/EXTERNAL>

### Flow

- D01 --> C01
- C01 --[I am only looking around.]--> D04
- C01 --[None of your business.]--> D06
- C01 --[I brought you something.]--> D07
- D04 --> <END/EXTERNAL>
- D06 --> <END/EXTERNAL>
- D07 --> <END/EXTERNAL>


========================================
## GuardNoticesFlowers
========================================

Path: `Assets/DialogueSystem/Graphs/GuardNoticesFlowers.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `5c57878f-1c75-462b-ade0-dccb1d857ead`
- Speaker: `Guard`
- Dialogue: `Oohh... Flowers? Someone special eh?`
- Next: C01

#### C01 - Choice

- Runtime Node ID: `4a384818-fd8c-4fca-a6a7-3047cd5b9da6`
- Speaker: `Player`
- Dialogue: `Ummm..`
- Choices:
  - Choice 0: `For you, mon ceur.`
    - Fuzzy Event ID: `OfferFlowersToGuard`
    - Required Bool Key: <none>
    - Destination: D02
  - Choice 1: `For someone special yes`
    - Fuzzy Event ID: `TeaseGuardAboutFlowers`
    - Required Bool Key: <none>
    - Destination: D04

#### D02 - Dialogue

- Runtime Node ID: `f7494d14-29ab-4152-88fa-bf1fa8271e35`
- Speaker: `Player`
- Dialogue: `I insist!`
- Next: D03

#### D03 - Dialogue

- Runtime Node ID: `c491af60-9b34-456b-80d1-2a1e0e5acf59`
- Speaker: `Guard`
- Dialogue: `Oh no you shouldn't.`
- Next: C02

#### C02 - Choice

- Runtime Node ID: `ccde32ae-a419-4db6-8e8b-49d5fc50d293`
- Speaker: `Player`
- Dialogue: `Ahh...`
- Choices:
  - Choice 0: `NO! NO! I INSIST`
    - Fuzzy Event ID: `GiveFlowersToGuard`
    - Required Bool Key: `Player.HasFlowers`
    - Destination: D06
  - Choice 1: `Ah well.. I tried`
    - Fuzzy Event ID: `RefuseFlowersToGuard`
    - Required Bool Key: <none>
    - Destination: D07

#### D04 - Dialogue

- Runtime Node ID: `2856f75b-1874-4df8-84fc-a0f13dba9659`
- Speaker: `Player`
- Dialogue: `Someone who can make my day, worth remembering.`
- Next: D05

#### D05 - Dialogue

- Runtime Node ID: `35ef10f4-2097-4325-b08a-757ad8ac6d51`
- Speaker: `Guard`
- Dialogue: `I wonder who they can be? Hmmmmmm....`
- Next: C03

#### D06 - Dialogue

- Runtime Node ID: `247d7e6c-dd0f-4dae-b75c-0e01c921c6ea`
- Speaker: `Guard`
- Dialogue: `Well, if you are going to be that loud... Maybe you can go inside and try not to be... "loud".`
- Next: <END/EXTERNAL>

#### C03 - Choice

- Runtime Node ID: `02fdb631-6c21-4b11-938a-76d270fdbd5c`
- Speaker: `Player`
- Dialogue: `In that Case...`
- Choices:
  - Choice 0: `I think, you are that someone special.`
    - Fuzzy Event ID: <none>
    - Required Bool Key: `Player.HasFlowers`
    - Destination: D03
  - Choice 1: `I'll let you know when I find them!`
    - Fuzzy Event ID: `DelayFlowersToGuard`
    - Required Bool Key: <none>
    - Destination: D07

#### D07 - Dialogue

- Runtime Node ID: `f22af96f-82f8-4a75-bade-204fbccaca75`
- Speaker: `Guard`
- Dialogue: `Oh! Oh? Oh... Awwww.`
- Next: <END/EXTERNAL>

### Flow

- D01 --> C01
- C01 --[For you, mon ceur.]--> D02
- C01 --[For someone special yes]--> D04
- D02 --> D03
- D03 --> C02
- C02 --[NO! NO! I INSIST]--> D06
- C02 --[Ah well.. I tried]--> D07
- D04 --> D05
- D05 --> C03
- D06 --> <END/EXTERNAL>
- C03 --[I think, you are that someone special.]--> D03
- C03 --[I'll let you know when I find them!]--> D07
- D07 --> <END/EXTERNAL>


========================================
## GuardRepeat
========================================

Path: `Assets/DialogueSystem/Graphs/GuardRepeat.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `a42cc0e7-db46-47f2-b413-d1bf9a44f95e`
- Speaker: `Guard`
- Dialogue: `Piss Off!`
- Next: <END/EXTERNAL>

#### D02 - Dialogue

- Runtime Node ID: `da31fffc-fe47-435a-8bf2-275ecc58a9de`
- Speaker: <none>
- Dialogue: <none>
- Next: <END>

### Flow

- D01 --> <END/EXTERNAL>


========================================
## GuardRequests
========================================

Path: `Assets/DialogueSystem/Graphs/GuardRequests.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `0e47c872-0ac9-4023-a77b-90ef0d2ad678`
- Speaker: `Guard`
- Dialogue: `Now, you better have something for me before I get you arrested...`
- Next: C01

#### C01 - Choice

- Runtime Node ID: `ac53e20c-5c17-43b2-a0a6-d5315ee4e73c`
- Speaker: `Player`
- Dialogue: `(What should I give him?)`
- Choices:
  - Choice 0: `Give Flowers`
    - Fuzzy Event ID: `GiveFlowersToGuard`
    - Required Bool Key: `Player.HasFlowers`
    - Destination: D02
  - Choice 1: `Give Coins`
    - Fuzzy Event ID: `GiveCoinsToGuard`
    - Required Bool Key: `Player.HasCoins`
    - Destination: D06
  - Choice 2: `I'll think about it.`
    - Fuzzy Event ID: <none>
    - Required Bool Key: <none>
    - Destination: <END/EXTERNAL>

#### D02 - Dialogue

- Runtime Node ID: `7b0fdb22-99cb-4ebd-bb27-f65294ad0b14`
- Speaker: `Guard`
- Dialogue: `Ooh. Thank you!`
- Next: D03

#### D03 - Dialogue

- Runtime Node ID: `72f536c3-ba55-4d8c-9bdd-c2eb0cfad234`
- Speaker: `Player`
- Dialogue: `Well??`
- Next: D04

#### D04 - Dialogue

- Runtime Node ID: `fc213d97-dd0a-4ffb-8565-9a79318556b2`
- Speaker: `Guard`
- Dialogue: `Oh! You may Pass.`
- Next: D05

#### D05 - Dialogue

- Runtime Node ID: `229afa40-5f96-4db2-b758-1fd807dab712`
- Speaker: `Guard`
- Dialogue: `But DO NOT make any NOISE`
- Next: <END/EXTERNAL>

#### D06 - Dialogue

- Runtime Node ID: `7b7dd786-84c9-45de-8154-9c005b5df92b`
- Speaker: `Guard`
- Dialogue: `Attempting to bribe an OFFICER?! Hands behind your back "cowboy".`
- Next: <END/EXTERNAL>

### Flow

- D01 --> C01
- C01 --[Give Flowers]--> D02
- C01 --[Give Coins]--> D06
- C01 --[I'll think about it.]--> <END/EXTERNAL>
- D02 --> D03
- D03 --> D04
- D04 --> D05
- D05 --> <END/EXTERNAL>
- D06 --> <END/EXTERNAL>


========================================
## GuardScold
========================================

Path: `Assets/DialogueSystem/Graphs/GuardScold.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `f2f0475c-d225-470b-aab8-481d407b8dd5`
- Speaker: `Guard`
- Dialogue: `Oi! What exactly are you doing with your hand inside that tree?`
- Next: D02

#### D02 - Dialogue

- Runtime Node ID: `d753d0d3-ae61-4f0a-b3ef-0be994182d29`
- Speaker: `Player`
- Dialogue: `Oh I was minding.`
- Next: D03

#### D03 - Dialogue

- Runtime Node ID: `2b6695e9-4eaa-4161-a8ee-23ca85e367db`
- Speaker: `Guard`
- Dialogue: `Minding?`
- Next: D04

#### D04 - Dialogue

- Runtime Node ID: `ccebf1c0-eab7-4261-aa08-cd6833c556f2`
- Speaker: `Player`
- Dialogue: `Minding the tree, it is my business after all.`
- Next: D05

#### D05 - Dialogue

- Runtime Node ID: `b2da3732-9201-45df-b17f-48af985c9d5d`
- Speaker: `Guard`
- Dialogue: `There's nothing over here. Now GIT!`
- Next: <END/EXTERNAL>

### Flow

- D01 --> D02
- D02 --> D03
- D03 --> D04
- D04 --> D05
- D05 --> <END/EXTERNAL>


========================================
## GuardVaultChase
========================================

Path: `Assets/DialogueSystem/Graphs/GuardVaultChase.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `cde1b53b-ead6-4690-91ba-45b5951ec78b`
- Speaker: `Guard`
- Dialogue: `YOU!? You should have taken..`
- Next: D02

#### D02 - Dialogue

- Runtime Node ID: `e2711823-40ee-4588-9394-df344cfa04f9`
- Speaker: `Guard`
- Dialogue: `THE GODDAMN FLOWERS!!!!`
- Next: <END/EXTERNAL>

### Flow

- D01 --> D02
- D02 --> <END/EXTERNAL>


========================================
## GuardVaultLeave
========================================

Path: `Assets/DialogueSystem/Graphs/GuardVaultLeave.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `abfd19e3-2f30-4c10-a181-ae754a4c26ac`
- Speaker: `Guard`
- Dialogue: `Right. Looks like everything's in order. I'll get the door for you.`
- Next: <END/EXTERNAL>

### Flow

- D01 --> <END/EXTERNAL>


========================================
## StealReminder
========================================

Path: `Assets/DialogueSystem/Graphs/Vault/StealReminder.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `34f8a456-3798-4221-8a0d-774337d4806f`
- Speaker: `Player`
- Dialogue: `I still need the apple.`
- Next: <END/EXTERNAL>

### Flow

- D01 --> <END/EXTERNAL>


========================================
## UnableToExit
========================================

Path: `Assets/DialogueSystem/Graphs/Vault/UnableToExit.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `aacbabd6-837f-4391-b634-a786b21cc574`
- Speaker: `Player`
- Dialogue: `Cant go through there, the guard will catch me`
- Next: D02

#### D02 - Dialogue

- Runtime Node ID: `f1fdf1e9-f69d-4eac-abf6-b1a734bf07aa`
- Speaker: `Player`
- Dialogue: `Plus, I also need the apple!`
- Next: <END/EXTERNAL>

### Flow

- D01 --> D02
- D02 --> <END/EXTERNAL>


========================================
## VaultDoor
========================================

Path: `Assets/DialogueSystem/Graphs/Vault/VaultDoor.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `26490532-b34f-45d9-80c0-bf0b5b31efbc`
- Speaker: <none>
- Dialogue: `A locked door! The apple is rigth there!`
- Next: D03

#### D02 - Dialogue

- Runtime Node ID: `8ee49378-9560-464d-ae7b-3ea358198ec4`
- Speaker: <none>
- Dialogue: <none>
- Next: <END>

#### D03 - Dialogue

- Runtime Node ID: `3bc344f4-67ca-4343-b187-2288af982de7`
- Speaker: <none>
- Dialogue: `The lock is old... and those hinges don't look much better.`
- Next: C01

#### C01 - Choice

- Runtime Node ID: `ca964a87-eb8b-4dcb-9a36-03eaea416840`
- Speaker: <none>
- Dialogue: `How do I get this open?`
- Choices:
  - Choice 0: `[Inspect Hinges]`
    - Fuzzy Event ID: `InspectVaultHinges`
    - Required Bool Key: `Vault.CanInspectHinges`
    - Destination: D04
  - Choice 1: `[Use Key]`
    - Fuzzy Event ID: `UseTreeKey`
    - Required Bool Key: `Player.HasTreeKey`
    - Destination: D05
  - Choice 2: `[Try Combination Lock]`
    - Fuzzy Event ID: `ChooseVaultCombination`
    - Required Bool Key: <none>
    - Destination: D06

#### D04 - Dialogue

- Runtime Node ID: `b4a77ad6-9916-48c3-bba4-db4a2b72912c`
- Speaker: <none>
- Dialogue: `The pins are worn nearly through. If I know where to put the pressure, I might keep this thing quiet.`
- Next: D08

#### D05 - Dialogue

- Runtime Node ID: `1c3ecce7-fc8b-4eac-bd37-d598e08b0f91`
- Speaker: <none>
- Dialogue: `The key from the tree... it actually fits!`
- Next: <END/EXTERNAL>

#### D06 - Dialogue

- Runtime Node ID: `73938ccf-4136-4860-9640-73f690d802c0`
- Speaker: <none>
- Dialogue: `In the name of all the gods that exist across all pantheons`
- Next: D07

#### D07 - Dialogue

- Runtime Node ID: `6a76937f-3150-4968-af8c-be7768ddbfd6`
- Speaker: <none>
- Dialogue: `OPEN SESAME!!`
- Next: <END/EXTERNAL>

#### D08 - Dialogue

- Runtime Node ID: `465fb53a-236d-4d37-b017-d2fdd76e6215`
- Speaker: <none>
- Dialogue: `I should decide how I want to open this.`
- Next: C01

### Flow

- D01 --> D03
- D03 --> C01
- C01 --[[Inspect Hinges]]--> D04
- C01 --[[Use Key]]--> D05
- C01 --[[Try Combination Lock]]--> D06
- D04 --> D08
- D05 --> <END/EXTERNAL>
- D06 --> D07
- D07 --> <END/EXTERNAL>
- D08 --> C01


========================================
## VendorCarpetCalm
========================================

Path: `Assets/DialogueSystem/Graphs/VendorDialogues/VendorCarpetCalm.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `8542ba84-84d8-4f7c-93ba-c4f092bca1af`
- Speaker: `Vendor`
- Dialogue: `See anything you like?`
- Next: <END/EXTERNAL>

### Flow

- D01 --> <END/EXTERNAL>


========================================
## VendorCarpetIntervention
========================================

Path: `Assets/DialogueSystem/Graphs/VendorDialogues/VendorCarpetIntervention.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `59ac23f0-ba29-4958-ba13-b5888cf69bc5`
- Speaker: `Vendor`
- Dialogue: `Oi! Stay in your lane boy!`
- Next: <END/EXTERNAL>

### Flow

- D01 --> <END/EXTERNAL>


========================================
## VendorCarpetUneasy
========================================

Path: `Assets/DialogueSystem/Graphs/VendorDialogues/VendorCarpetUneasy.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `362d6211-cbcb-4839-8911-d13ab0166be3`
- Speaker: `Vendor`
- Dialogue: `You've been staring at that carpet for a while.`
- Next: <END/EXTERNAL>

### Flow

- D01 --> <END/EXTERNAL>


========================================
## VendorCarpetWatched
========================================

Path: `Assets/DialogueSystem/Graphs/VendorDialogues/VendorCarpetWatched.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `752bba39-9840-47bd-9980-8fbf502c9744`
- Speaker: `Vendor`
- Dialogue: `You're awfully interested in that carpet.`
- Next: <END/EXTERNAL>

### Flow

- D01 --> <END/EXTERNAL>


========================================
## VendorEndingDefault
========================================

Path: `Assets/DialogueSystem/Graphs/VendorDialogues/VendorEndingDefault.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `5e9fde96-a3cb-436b-bf0d-69d4fefe67e2`
- Speaker: `Vendor`
- Dialogue: `You're alive? Frankly, that's more than I expected.`
- Next: <END/EXTERNAL>

### Flow

- D01 --> <END/EXTERNAL>


========================================
## VendorEndingGrudge
========================================

Path: `Assets/DialogueSystem/Graphs/VendorDialogues/VendorEndingGrudge.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `0a705dae-0d14-4b5f-92f0-258c1fc7eefd`
- Speaker: `Vendor`
- Dialogue: `HA! I knew sending you in there would ruin his day.`
- Next: <END/EXTERNAL>

### Flow

- D01 --> <END/EXTERNAL>


========================================
## VendorEndingSplit
========================================

Path: `Assets/DialogueSystem/Graphs/VendorDialogues/VendorEndingSplit.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `6e0979da-7a92-4f8e-b0e1-593b79566425`
- Speaker: `Vendor`
- Dialogue: `You made it! ...Now, about my cut.`
- Next: <END/EXTERNAL>

### Flow

- D01 --> <END/EXTERNAL>


========================================
## VendorFirstMeeting
========================================

Path: `Assets/DialogueSystem/Graphs/VendorDialogues/VendorFirstMeeting.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `d721d3cb-d8a1-4b76-a6ed-bbdec52a8fb5`
- Speaker: `Vendor`
- Dialogue: `Greetings! What carpet do you need?`
- Next: C01

#### C01 - Choice

- Runtime Node ID: `b6f3617f-8f83-4ede-b607-e75077e06acd`
- Speaker: `Player`
- Dialogue: `Funny you should ask...`
- Choices:
  - Choice 0: `I am looking for someinformation`
    - Fuzzy Event ID: <none>
    - Required Bool Key: <none>
    - Destination: D02
  - Choice 1: `I am interested in that carpet over there.`
    - Fuzzy Event ID: <none>
    - Required Bool Key: `Player.NoticedVendorReaction`
    - Destination: D03
  - Choice 2: `To be frank, I'd like to get inside the vault.`
    - Fuzzy Event ID: `VendorDirectVaultAsk`
    - Required Bool Key: <none>
    - Destination: D04

#### D02 - Dialogue

- Runtime Node ID: `81cc4868-795a-4dd3-a810-222dc5547766`
- Speaker: `Vendor`
- Dialogue: `And, what do you bring, in exchange?`
- Next: C02

#### D03 - Dialogue

- Runtime Node ID: `f4c732e4-8608-4732-9b79-7c74f0ec5a04`
- Speaker: `Vendor`
- Dialogue: `We sell carpets, not rumors!`
- Next: C08

#### D04 - Dialogue

- Runtime Node ID: `45844ac4-181f-4cec-b2dc-1e7ba81a5069`
- Speaker: `Vendor`
- Dialogue: `Bold of you to think of me as a Crime Partner!`
- Next: <END/EXTERNAL>

#### C02 - Choice

- Runtime Node ID: `ce99a011-47ef-40db-af58-8862d460c673`
- Speaker: `Player`
- Dialogue: `Let me see...`
- Choices:
  - Choice 0: `Coins`
    - Fuzzy Event ID: `OfferVendorCoins`
    - Required Bool Key: `Player.HasCoins`
    - Destination: D05
  - Choice 1: `Flowers`
    - Fuzzy Event ID: `OfferVendorFlowers`
    - Required Bool Key: `Player.HasFlowers`
    - Destination: D07
  - Choice 2: `I promise something, but first some info`
    - Fuzzy Event ID: <none>
    - Required Bool Key: <none>
    - Destination: D09

#### D05 - Dialogue

- Runtime Node ID: `3f9211f8-4e3d-4e50-80b4-697fc3a7e2db`
- Speaker: `Vendor`
- Dialogue: `Ah… Why these rags of arts are worth mere pennies.`
- Next: D06

#### D06 - Dialogue

- Runtime Node ID: `47b86f9c-cad4-454c-8fc2-b678dc514cc8`
- Speaker: `Vendor`
- Dialogue: `You seek something more than carpets, don’t you?`
- Next: C05

#### D07 - Dialogue

- Runtime Node ID: `a7281363-4586-4a0c-a405-fa73a534ddf6`
- Speaker: `Vendor`
- Dialogue: `Ugh! Peasant!`
- Next: D08

#### D08 - Dialogue

- Runtime Node ID: `a6ee7d2d-32e8-45f7-9438-ff804bd3f90e`
- Speaker: `Vendor`
- Dialogue: `Get these away from me and come back when you have something useful for me.`
- Next: <END/EXTERNAL>

#### D09 - Dialogue

- Runtime Node ID: `70ba4eae-9714-4e06-b5b6-62dea9650997`
- Speaker: `Vendor`
- Dialogue: `Okay then, but I must recieve something GOOD.`
- Next: C03

#### C03 - Choice

- Runtime Node ID: `fd6e6d01-1277-442f-a49b-d4868bf37daa`
- Speaker: `Player`
- Dialogue: `You see...`
- Choices:
  - Choice 0: `I was hoping you'd help me get inside that vault.`
    - Fuzzy Event ID: `VendorAskHelp`
    - Required Bool Key: <none>
    - Destination: D10
  - Choice 1: `I can hide your cimes, just help me out here`
    - Fuzzy Event ID: `VendorBlackmail`
    - Required Bool Key: <none>
    - Destination: D14
  - Choice 2: `I seek you, brother. We were lost once.`
    - Fuzzy Event ID: `VendorDeceive`
    - Required Bool Key: <none>
    - Destination: D20
  - Choice 3: `I seek you... you look rich and, might I say, handsome.`
    - Fuzzy Event ID: `VendorFlirt`
    - Required Bool Key: <none>
    - Destination: D37

#### D10 - Dialogue

- Runtime Node ID: `4b2aa476-913e-4281-b3d4-d9ae7099ee11`
- Speaker: `Vendor`
- Dialogue: `And, why would I help you in that case?`
- Next: C04

#### C04 - Choice

- Runtime Node ID: `a80ea58a-7ac4-4af3-aca5-64c569da4788`
- Speaker: `Player`
- Dialogue: `You might like the answer...`
- Choices:
  - Choice 0: `I'd split the money once I sell it.`
    - Fuzzy Event ID: `VendorSplitDeal`
    - Required Bool Key: <none>
    - Destination: D45
  - Choice 1: `That guard, PISSES me off!`
    - Fuzzy Event ID: `VendorGuardGrudge`
    - Required Bool Key: `Guard.WasRudeToPlayer`
    - Destination: D46

#### C05 - Choice

- Runtime Node ID: `ff3a942b-e602-4699-8fa8-61e2a2590881`
- Speaker: `Player`
- Dialogue: `The thing is...`
- Choices:
  - Choice 0: `I'm here on royal business.`
    - Fuzzy Event ID: `VendorRoyalLie`
    - Required Bool Key: <none>
    - Destination: D11
  - Choice 1: `I heard someone plans to rob the vault`
    - Fuzzy Event ID: `VendorRobberyWarning`
    - Required Bool Key: <none>
    - Destination: D12
  - Choice 2: `Because I'm going to rob it`
    - Fuzzy Event ID: `VendorAdmitRobbery`
    - Required Bool Key: <none>
    - Destination: D13

#### D11 - Dialogue

- Runtime Node ID: `5456bef5-fe5d-4f6b-bed5-9cc2f9e0a41e`
- Speaker: `Vendor`
- Dialogue: `Royal business, is it? The guard would be of better help.`
- Next: <END/EXTERNAL>

#### D12 - Dialogue

- Runtime Node ID: `62a463ff-1008-40db-ad46-3976ddd36f54`
- Speaker: `Vendor`
- Dialogue: `Someone plans to rob it? I suggest you inform it to the guard`
- Next: <END/EXTERNAL>

#### D13 - Dialogue

- Runtime Node ID: `44b8deb4-3002-4f0a-88f2-4a2c668bbe4c`
- Speaker: `Vendor`
- Dialogue: `You know you've got some mouth on you? I'll call the guards if you bother me again!`
- Next: <END/EXTERNAL>

#### D14 - Dialogue

- Runtime Node ID: `719d6211-99c1-4287-95ef-0fc702f2bb13`
- Speaker: `Player`
- Dialogue: `I know the skeletons in your closet are rotting. I can help you`
- Next: D15

#### D15 - Dialogue

- Runtime Node ID: `18ecf9a4-e139-4a30-a4ce-c8a81bd8e1a4`
- Speaker: `Player`
- Dialogue: `That is... if you cooperate`
- Next: D16

#### D16 - Dialogue

- Runtime Node ID: `07737138-a6c7-4642-813f-b958ec7b4210`
- Speaker: `Vendor`
- Dialogue: `My dirt? Friend, you're about to join them.`
- Next: D17

#### D17 - Dialogue

- Runtime Node ID: `7a14d03f-f4ec-4616-a87a-772633b0dcae`
- Speaker: `Player`
- Dialogue: `I beg your pardon?`
- Next: D18

#### D18 - Dialogue

- Runtime Node ID: `1cd2907c-1f00-48ea-bd88-b7bdba225a2b`
- Speaker: `Vendor`
- Dialogue: `GUARD! This guy is a MENACE! He is stealing, THE APPLE!!!`
- Next: D19

#### D19 - Dialogue

- Runtime Node ID: `7c4f6a36-39b8-43e9-92e5-ffe77e590d29`
- Speaker: `Player`
- Dialogue: `Aw shit, here we go again.`
- Next: <END/EXTERNAL>

#### D20 - Dialogue

- Runtime Node ID: `5d36bb6d-03d4-4161-bbee-734bc81802d9`
- Speaker: `Vendor`
- Dialogue: `Oh... No...`
- Next: D21

#### D21 - Dialogue

- Runtime Node ID: `173531ec-c52e-40e4-9165-033dd4d1438f`
- Speaker: `Player`
- Dialogue: `Oh... Yea...`
- Next: D22

#### D22 - Dialogue

- Runtime Node ID: `3a50c766-2347-4c69-a1b7-4a0ae8eb98fa`
- Speaker: `Vendor`
- Dialogue: `Oh, Gods!!`
- Next: D23

#### D23 - Dialogue

- Runtime Node ID: `9f3a1d3a-644e-4a11-ab9b-4b3595869374`
- Speaker: `Player`
- Dialogue: `Oh, the Devil!`
- Next: D24

#### D24 - Dialogue

- Runtime Node ID: `940c5eb1-4575-4a1f-852e-c9b090cd082f`
- Speaker: `Vendor`
- Dialogue: `Goodness Almighty!!!`
- Next: D25

#### D25 - Dialogue

- Runtime Node ID: `b536ad77-a1b2-43cb-b76d-13dc8c59c9be`
- Speaker: `Player`
- Dialogue: `Can we stop now? I know you... killed Uncle Ben!`
- Next: D26

#### D26 - Dialogue

- Runtime Node ID: `80b5ac27-aa8d-4bc7-9685-1ef10511469a`
- Speaker: `Player`
- Dialogue: `Now if you just let me enter the vault`
- Next: D27

#### D27 - Dialogue

- Runtime Node ID: `0012ca8f-55ae-453d-a581-783dbbe75c71`
- Speaker: `Vendor`
- Dialogue: `Uncle Ben?`
- Next: D28

#### D28 - Dialogue

- Runtime Node ID: `52f2ebc5-8497-4cba-aaa5-1c18208fa812`
- Speaker: `Player`
- Dialogue: `Yeah, the one who stubbed his toe on our 2nd birthday`
- Next: D29

#### D29 - Dialogue

- Runtime Node ID: `d6ae5be0-43be-4938-a73c-eb1e0de4d986`
- Speaker: `Vendor`
- Dialogue: `The one with 3 wives?`
- Next: D30

#### D30 - Dialogue

- Runtime Node ID: `9665e188-9c79-4cd0-92e3-60233fa84bfb`
- Speaker: `Player`
- Dialogue: `4 actually.`
- Next: D31

#### D31 - Dialogue

- Runtime Node ID: `8549ea59-1b77-4b50-b6ff-fac9c709ff09`
- Speaker: `Vendor`
- Dialogue: `Ay! Would you look at that? More money for us!`
- Next: D32

#### D32 - Dialogue

- Runtime Node ID: `a2cc7658-07a0-4c49-a25d-aa827acd7a17`
- Speaker: `Vendor`
- Dialogue: `Isn't that right? Brother? Who ratted me out?`
- Next: D33

#### D33 - Dialogue

- Runtime Node ID: `af0a32ec-8629-4d77-9b35-c0b6554bb257`
- Speaker: `Player`
- Dialogue: `Now, ain't that sweet.`
- Next: D34

#### D34 - Dialogue

- Runtime Node ID: `31028ae6-00cf-4644-9630-ba4710ac3396`
- Speaker: `Player`
- Dialogue: `Now, before I slice your face off... The vault, please, dear brother?`
- Next: D35

#### D35 - Dialogue

- Runtime Node ID: `caf89782-5446-4e7d-9ac5-ab59dbe4d43c`
- Speaker: `Vendor`
- Dialogue: `Right, right, of course. Guar...!!`
- Next: C06

#### C06 - Choice

- Runtime Node ID: `f0be86d3-4fda-4b6e-90ec-c3adbdae9d99`
- Speaker: `Player`
- Dialogue: `Now, now.`
- Choices:
  - Choice 0: `[Threaten]`
    - Fuzzy Event ID: `VendorDeceiveThreat`
    - Required Bool Key: <none>
    - Destination: D36

#### D36 - Dialogue

- Runtime Node ID: `384d36bb-c1c9-4977-bd27-36dabae9955b`
- Speaker: `Vendor`
- Dialogue: `Okay! Okay, fine!`
- Next: C07

#### C07 - Choice

- Runtime Node ID: `cbfa9f3c-8f1c-455d-a41a-fb97ace81d18`
- Speaker: `Player`
- Dialogue: `See? Such love amongst brothers!`
- Choices:
  - Choice 0: `[Un-Threaten]`
    - Fuzzy Event ID: `VendorDeceiveReveal`
    - Required Bool Key: <none>
    - Destination: <END/EXTERNAL>

#### D37 - Dialogue

- Runtime Node ID: `79bfbbe4-4496-4df1-8192-ae0142962ee0`
- Speaker: `Vendor`
- Dialogue: `...`
- Next: D38

#### D38 - Dialogue

- Runtime Node ID: `fdc5a481-d3ba-4899-b68d-8bddf832925f`
- Speaker: `Vendor`
- Dialogue: `You are not my type.`
- Next: D39

#### D39 - Dialogue

- Runtime Node ID: `34237c4d-cad8-4c4e-a1f0-eff39802b539`
- Speaker: `Vendor`
- Dialogue: `But that Guard over there...`
- Next: D40

#### D40 - Dialogue

- Runtime Node ID: `ee86660f-0749-48fc-8e82-1d497c9e2be9`
- Speaker: `Player`
- Dialogue: `You want me to.. charm him? For you?`
- Next: D41

#### D41 - Dialogue

- Runtime Node ID: `e6be64ba-6c41-4e32-9c46-3da9542c2f7b`
- Speaker: `Vendor`
- Dialogue: `YES! Here, take these gold coins and try to make him understand`
- Next: D42

#### D42 - Dialogue

- Runtime Node ID: `745a4626-8145-4f52-8185-ba0cd11b975d`
- Speaker: `Vendor`
- Dialogue: `He would very VERY much like it.`
- Next: D43

#### D43 - Dialogue

- Runtime Node ID: `607cc99f-e70b-4698-9c94-f4e5d49abbae`
- Speaker: `Vendor`
- Dialogue: `*GASP* Don't tell him they are from me!`
- Next: <END/EXTERNAL>

#### C08 - Choice

- Runtime Node ID: `52477f65-b924-453e-ae50-ec4783a8b732`
- Speaker: `Player`
- Dialogue: `I know what's there`
- Choices:
  - Choice 0: `[Secret entrance]`
    - Fuzzy Event ID: `VendorChoiceRevealPit`
    - Required Bool Key: <none>
    - Destination: D44
  - Choice 1: `[The dead remains]`
    - Fuzzy Event ID: `VendorChoiceRevealPit`
    - Required Bool Key: <none>
    - Destination: D44

#### D44 - Dialogue

- Runtime Node ID: `97d54566-1f0c-44de-a417-50b69f3b09bf`
- Speaker: `Vendor`
- Dialogue: `Ahh! No please, just GO! There's the pit. Just please LEAVE ME ALONE!`
- Next: <END/EXTERNAL>

#### D45 - Dialogue

- Runtime Node ID: `8ec8c097-8279-4da2-b723-0acc8375e768`
- Speaker: `Vendor`
- Dialogue: `Now you're speaking my language. Just make sure my cut is worth the trouble.`
- Next: <END/EXTERNAL>

#### D46 - Dialogue

- Runtime Node ID: `97936190-3ec3-4cec-9f5b-a37d49e5db3a`
- Speaker: `Vendor`
- Dialogue: `He doesn't appeal to me either. You know what? I have an idea...`
- Next: D47

#### D47 - Dialogue

- Runtime Node ID: `5dc65a2f-a6a5-49e2-8c29-f60bf7a01719`
- Speaker: `Vendor`
- Dialogue: `See that carpet? There's a way underneath it. To the VAULT! Go cause some trouble.`
- Next: <END/EXTERNAL>

### Flow

- D01 --> C01
- C01 --[I am looking for someinformation]--> D02
- C01 --[I am interested in that carpet over there.]--> D03
- C01 --[To be frank, I'd like to get inside the vault.]--> D04
- D02 --> C02
- D03 --> C08
- D04 --> <END/EXTERNAL>
- C02 --[Coins]--> D05
- C02 --[Flowers]--> D07
- C02 --[I promise something, but first some info]--> D09
- D05 --> D06
- D06 --> C05
- D07 --> D08
- D08 --> <END/EXTERNAL>
- D09 --> C03
- C03 --[I was hoping you'd help me get inside that vault.]--> D10
- C03 --[I can hide your cimes, just help me out here]--> D14
- C03 --[I seek you, brother. We were lost once.]--> D20
- C03 --[I seek you... you look rich and, might I say, handsome.]--> D37
- D10 --> C04
- C04 --[I'd split the money once I sell it.]--> D45
- C04 --[That guard, PISSES me off!]--> D46
- C05 --[I'm here on royal business.]--> D11
- C05 --[I heard someone plans to rob the vault]--> D12
- C05 --[Because I'm going to rob it]--> D13
- D11 --> <END/EXTERNAL>
- D12 --> <END/EXTERNAL>
- D13 --> <END/EXTERNAL>
- D14 --> D15
- D15 --> D16
- D16 --> D17
- D17 --> D18
- D18 --> D19
- D19 --> <END/EXTERNAL>
- D20 --> D21
- D21 --> D22
- D22 --> D23
- D23 --> D24
- D24 --> D25
- D25 --> D26
- D26 --> D27
- D27 --> D28
- D28 --> D29
- D29 --> D30
- D30 --> D31
- D31 --> D32
- D32 --> D33
- D33 --> D34
- D34 --> D35
- D35 --> C06
- C06 --[[Threaten]]--> D36
- D36 --> C07
- C07 --[[Un-Threaten]]--> <END/EXTERNAL>
- D37 --> D38
- D38 --> D39
- D39 --> D40
- D40 --> D41
- D41 --> D42
- D42 --> D43
- D43 --> <END/EXTERNAL>
- C08 --[[Secret entrance]]--> D44
- C08 --[[The dead remains]]--> D44
- D44 --> <END/EXTERNAL>
- D45 --> <END/EXTERNAL>
- D46 --> D47
- D47 --> <END/EXTERNAL>


========================================
## VendorPitConfrontation
========================================

Path: `Assets/DialogueSystem/Graphs/VendorDialogues/VendorPitConfrontation.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `329cfcde-bae7-4d4a-a5a2-ab2b249a909d`
- Speaker: `Vendor`
- Dialogue: `What in the name of GODS are you DOING?! `
- Next: C01

#### C01 - Choice

- Runtime Node ID: `ff233390-316a-4c37-9874-43258bbadffd`
- Speaker: `Player`
- Dialogue: `(I have to do something QUICK)`
- Choices:
  - Choice 0: `[Jump in]`
    - Fuzzy Event ID: `VendorPitJump`
    - Required Bool Key: <none>
    - Destination: <END/EXTERNAL>
  - Choice 1: `[Give excuse]`
    - Fuzzy Event ID: `VendorPitExcuse`
    - Required Bool Key: <none>
    - Destination: D02

#### D02 - Dialogue

- Runtime Node ID: `2dab1387-a333-4d05-90bb-00dc4d469f7d`
- Speaker: `Player`
- Dialogue: `Now now, we can definitely reason with this`
- Next: D03

#### D03 - Dialogue

- Runtime Node ID: `c6c6aad3-0842-471e-aa5b-c2fc8468ef4f`
- Speaker: `Vendor`
- Dialogue: `YOU! Dug that up. It was YOU!`
- Next: D04

#### D04 - Dialogue

- Runtime Node ID: `71f353f4-5392-44cb-a81f-1bf9fb675bef`
- Speaker: `Player`
- Dialogue: `Oh no that was you, I was simply using it.`
- Next: D05

#### D05 - Dialogue

- Runtime Node ID: `11a6f9a4-1f49-48c0-ab68-576aaf9c8a77`
- Speaker: `Vendor`
- Dialogue: `Guard! HELP!!! ROBBER!!! MENACE! THIEF!!!`
- Next: <END/EXTERNAL>

### Flow

- D01 --> C01
- C01 --[[Jump in]]--> <END/EXTERNAL>
- C01 --[[Give excuse]]--> D02
- D02 --> D03
- D03 --> D04
- D04 --> D05
- D05 --> <END/EXTERNAL>


========================================
## VendorReturning
========================================

Path: `Assets/DialogueSystem/Graphs/VendorDialogues/VendorReturning.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `717b66b7-7016-4736-84fd-b62b0751df97`
- Speaker: `Vendor`
- Dialogue: `Greetings! What carpe— ...Oh. It's you.`
- Next: C01

#### C01 - Choice

- Runtime Node ID: `c198997b-e2f3-42d1-8357-0526b0beb5d2`
- Speaker: `Player`
- Dialogue: <none>
- Choices:
  - Choice 0: `Let's talkBusiness`
    - Fuzzy Event ID: <none>
    - Required Bool Key: <none>
    - Destination: D02
  - Choice 1: `About that Carpet`
    - Fuzzy Event ID: <none>
    - Required Bool Key: `Player.NoticedVendorReaction`
    - Destination: D03
  - Choice 2: `Never Mind`
    - Fuzzy Event ID: <none>
    - Required Bool Key: <none>
    - Destination: <END/EXTERNAL>

#### D02 - Dialogue

- Runtime Node ID: `1a21acfc-0b15-4b3d-9e95-17b6598bf910`
- Speaker: `Vendor`
- Dialogue: `Fine. What are you offering`
- Next: C02

#### D03 - Dialogue

- Runtime Node ID: `15ad31e5-d787-4032-b3fe-e6ed4b7f34d6`
- Speaker: `Vendor`
- Dialogue: `You're still interested in that thing`
- Next: D06

#### C02 - Choice

- Runtime Node ID: `3a8b2bc4-1239-4cc9-b596-c26d5421ac58`
- Speaker: `Player`
- Dialogue: <none>
- Choices:
  - Choice 0: `Coins`
    - Fuzzy Event ID: `OfferVendorCoins`
    - Required Bool Key: `Player.HasCoins`
    - Destination: D04
  - Choice 1: `Flowers`
    - Fuzzy Event ID: `OfferVendorFlowers`
    - Required Bool Key: `Player.HasFlowers`
    - Destination: D07
  - Choice 2: `Something else`
    - Fuzzy Event ID: <none>
    - Required Bool Key: <none>
    - Destination: D09
  - Choice 3: `Never Mind`
    - Fuzzy Event ID: <none>
    - Required Bool Key: <none>
    - Destination: <END/EXTERNAL>

#### D04 - Dialogue

- Runtime Node ID: `c3ee73a1-7d10-4901-ab1a-8b72baeb5176`
- Speaker: `Vendor`
- Dialogue: `I see you DO mean business. Now, what can I offer?`
- Next: <END>

#### D05 - Dialogue

- Runtime Node ID: `9dc40591-f822-4e1a-93cb-87e08aa6e2a6`
- Speaker: `Vendor`
- Dialogue: `Ahh! No please, just GO! There's the pit. Just please LEAVE ME ALONE!`
- Next: <END/EXTERNAL>

#### D06 - Dialogue

- Runtime Node ID: `96121e28-676f-4d44-bbe4-a3749e020095`
- Speaker: `Vendor`
- Dialogue: `I told you! We sell carpets, not rumors!`
- Next: C03

#### C03 - Choice

- Runtime Node ID: `adb7846d-5348-4a76-8937-b226398eb607`
- Speaker: `Player`
- Dialogue: `I know what's there`
- Choices:
  - Choice 0: `[Secret entrance]`
    - Fuzzy Event ID: `VendorChoiceRevealPit`
    - Required Bool Key: <none>
    - Destination: D05
  - Choice 1: `[The dead remains]`
    - Fuzzy Event ID: `VendorChoiceRevealPit`
    - Required Bool Key: <none>
    - Destination: D05

#### D07 - Dialogue

- Runtime Node ID: `0a58bdaa-6928-4fb3-9846-9cd7bc7492d5`
- Speaker: `Vendor`
- Dialogue: `Ugh! Peasant!`
- Next: D08

#### D08 - Dialogue

- Runtime Node ID: `64f5fcab-2d3e-41ff-a569-7be068959b31`
- Speaker: `Vendor`
- Dialogue: `Get these away from me and come back when you have something useful for me.`
- Next: <END/EXTERNAL>

#### D09 - Dialogue

- Runtime Node ID: `7fba709d-ab46-4cdf-a81b-b0cd7ae7059e`
- Speaker: `Vendor`
- Dialogue: `Okay then, but I must recieve something GOOD.`
- Next: C04

#### C04 - Choice

- Runtime Node ID: `a797badd-03a7-4172-9ff5-a6a2e002c2d0`
- Speaker: `Player`
- Dialogue: `You see...`
- Choices:
  - Choice 0: `I was hoping you'd help me get inside that vault.`
    - Fuzzy Event ID: `VendorAskHelp`
    - Required Bool Key: <none>
    - Destination: D10
  - Choice 1: `I can hide your cimes, just help me out here`
    - Fuzzy Event ID: `VendorBlackmail`
    - Required Bool Key: <none>
    - Destination: D11
  - Choice 2: `I seek you, brother. We were lost once.`
    - Fuzzy Event ID: `VendorDeceive`
    - Required Bool Key: <none>
    - Destination: D18
  - Choice 3: `I seek you... you look rich and, might I say, handsome.`
    - Fuzzy Event ID: `VendorFlirt`
    - Required Bool Key: <none>
    - Destination: D34

#### D10 - Dialogue

- Runtime Node ID: `e506a6a6-624a-4d24-95a5-1220331d4370`
- Speaker: `Vendor`
- Dialogue: `And, why would I help you in that case?`
- Next: C05

#### C05 - Choice

- Runtime Node ID: `d44286a8-677b-4a90-a35c-f57c63cf051f`
- Speaker: `Player`
- Dialogue: `You might like the answer...`
- Choices:
  - Choice 0: `I'd split the money once I sell it.`
    - Fuzzy Event ID: `VendorSplitDeal`
    - Required Bool Key: <none>
    - Destination: D41
  - Choice 1: `That guard, PISSES me off!`
    - Fuzzy Event ID: `VendorGuardGrudge`
    - Required Bool Key: `Guard.WasRudeToPlayer`
    - Destination: D43

#### D11 - Dialogue

- Runtime Node ID: `e4eed20a-6ae9-4675-86af-2f0c4e1f85bb`
- Speaker: `Player`
- Dialogue: `I know the skeletons in your closet are rotting. I can help you`
- Next: D13

#### D12 - Dialogue

- Runtime Node ID: `40c20795-483b-4980-bae3-d41875dfaea5`
- Speaker: `Vendor`
- Dialogue: `My dirt? Friend, you're about to join them.`
- Next: D14

#### D13 - Dialogue

- Runtime Node ID: `a5a79d6e-6e5b-4b58-a901-7ad18b6363b3`
- Speaker: `Player`
- Dialogue: `That is... if you cooperate`
- Next: D12

#### D14 - Dialogue

- Runtime Node ID: `18a72e0f-9494-45be-b99f-dd2a07a1ad48`
- Speaker: `Player`
- Dialogue: `I beg your pardon?`
- Next: D15

#### D15 - Dialogue

- Runtime Node ID: `2204dd59-c67f-47ac-95de-7cc5234add0a`
- Speaker: `Vendor`
- Dialogue: `GUARD! This guy is a MENACE! He is stealing, THE APPLE!!!`
- Next: D16

#### D16 - Dialogue

- Runtime Node ID: `fe47f994-c463-4568-83e9-fcecca376164`
- Speaker: `Player`
- Dialogue: `Aw shit, here we go again.`
- Next: <END>

#### D17 - Dialogue

- Runtime Node ID: `aa6d2fd3-23c2-4e80-a5da-f539235ab670`
- Speaker: `Player`
- Dialogue: `Oh, the Devil!`
- Next: D25

#### D18 - Dialogue

- Runtime Node ID: `19fc84e1-2079-4890-a454-42d76f487198`
- Speaker: `Vendor`
- Dialogue: `Oh... No...`
- Next: D19

#### D19 - Dialogue

- Runtime Node ID: `37fc2c5b-e7d2-41ed-82c5-8b1ae277f8ef`
- Speaker: `Player`
- Dialogue: `Oh... Yea...`
- Next: D20

#### D20 - Dialogue

- Runtime Node ID: `d799f6a2-ed06-401b-9441-81451a93884d`
- Speaker: `Vendor`
- Dialogue: `Oh, Gods!!`
- Next: D17

#### D21 - Dialogue

- Runtime Node ID: `14572190-0f38-4701-9419-e5fbbdfc558d`
- Speaker: `Vendor`
- Dialogue: `The one with 3 wives?`
- Next: D22

#### C06 - Choice

- Runtime Node ID: `2cf682ff-1d26-4abd-b5ff-00cbe73c09c8`
- Speaker: `Player`
- Dialogue: `See? Such love amongst brothers!`
- Choices:
  - Choice 0: `[Un-Threaten]`
    - Fuzzy Event ID: `VendorDeceiveReveal`
    - Required Bool Key: <none>
    - Destination: <END/EXTERNAL>

#### D22 - Dialogue

- Runtime Node ID: `96645592-701b-4eb3-a9b1-c179a46378e2`
- Speaker: `Player`
- Dialogue: `4 actually.`
- Next: D30

#### D23 - Dialogue

- Runtime Node ID: `f073cf44-b5c2-45f1-b46c-98c8a02a72f4`
- Speaker: `Player`
- Dialogue: `Now if you just let me enter the vault`
- Next: D24

#### D24 - Dialogue

- Runtime Node ID: `943ea949-0d14-4336-b4e3-29da49922c04`
- Speaker: `Vendor`
- Dialogue: `Uncle Ben?`
- Next: D27

#### D25 - Dialogue

- Runtime Node ID: `aab899a6-9310-4701-89cf-4627b951cc4a`
- Speaker: `Vendor`
- Dialogue: `Goodness Almighty!!!`
- Next: D26

#### D26 - Dialogue

- Runtime Node ID: `a0a1d9d3-858d-4285-989c-9e2b6fdfce6f`
- Speaker: `Player`
- Dialogue: `Can we stop now? I know you... killed Uncle Ben!`
- Next: D23

#### D27 - Dialogue

- Runtime Node ID: `04dd7d62-0ec8-49dd-b6c5-8101a4851d27`
- Speaker: `Player`
- Dialogue: `Yeah, the one who stubbed his toe on our 2nd birthday`
- Next: D21

#### D28 - Dialogue

- Runtime Node ID: `02e497ff-80f3-4678-9897-823f3879ddc1`
- Speaker: `Vendor`
- Dialogue: `Okay! Okay, fine!`
- Next: C06

#### D29 - Dialogue

- Runtime Node ID: `b5551c34-1a56-43eb-8555-e8a66edf6068`
- Speaker: `Vendor`
- Dialogue: `Right, right, of course. Guar...!!`
- Next: C07

#### D30 - Dialogue

- Runtime Node ID: `57766e3e-738b-4f77-99bf-79adddb8ea89`
- Speaker: `Vendor`
- Dialogue: `Ay! Would you look at that? More money for us!`
- Next: D33

#### D31 - Dialogue

- Runtime Node ID: `b3fdf84a-c352-42f5-8197-0dbea2fb3f68`
- Speaker: `Player`
- Dialogue: `Now, ain't that sweet.`
- Next: D32

#### D32 - Dialogue

- Runtime Node ID: `64be1c10-d61e-4973-9f29-89187218b4ad`
- Speaker: `Player`
- Dialogue: `Now, before I slice your face off... The vault, please, dear brother?`
- Next: D29

#### D33 - Dialogue

- Runtime Node ID: `811be171-72b9-4ab1-a21a-27f74c2f782a`
- Speaker: `Vendor`
- Dialogue: `Isn't that right? Brother? Who ratted me out?`
- Next: D31

#### C07 - Choice

- Runtime Node ID: `e4402c5c-9f4d-4e0f-ac99-89971e60e027`
- Speaker: `Player`
- Dialogue: `Now, now.`
- Choices:
  - Choice 0: `[Threaten]`
    - Fuzzy Event ID: `VendorDeceiveThreat`
    - Required Bool Key: <none>
    - Destination: D28

#### D34 - Dialogue

- Runtime Node ID: `44f456bd-33ce-4fce-a0fb-75af1096c5e4`
- Speaker: `Vendor`
- Dialogue: `...`
- Next: D36

#### D35 - Dialogue

- Runtime Node ID: `6b522932-9bdb-45d6-9b76-199e74c06e93`
- Speaker: `Vendor`
- Dialogue: `But that Guard over there...`
- Next: D37

#### D36 - Dialogue

- Runtime Node ID: `f65b8692-92c0-4448-a95a-332e1e271441`
- Speaker: `Vendor`
- Dialogue: `You are not my type.`
- Next: D35

#### D37 - Dialogue

- Runtime Node ID: `1e08605c-bcb7-4981-a29c-3f0d18680dc1`
- Speaker: `Player`
- Dialogue: `You want me to.. charm him? For you?`
- Next: D39

#### D38 - Dialogue

- Runtime Node ID: `6978963d-70f5-41cf-8130-4161742bbfae`
- Speaker: `Vendor`
- Dialogue: `*GASP* Don't tell him they are from me!`
- Next: <END/EXTERNAL>

#### D39 - Dialogue

- Runtime Node ID: `96b86836-8bc0-4335-81b0-2d2a8faa6afa`
- Speaker: `Vendor`
- Dialogue: `YES! Here, take these gold coins and try to make him understand`
- Next: D40

#### D40 - Dialogue

- Runtime Node ID: `6dde2b4c-5411-4a6c-be56-9c894bc67387`
- Speaker: `Vendor`
- Dialogue: `He would very VERY much like it.`
- Next: D38

#### D41 - Dialogue

- Runtime Node ID: `69982db8-eeb7-4204-a763-9a49e35820c5`
- Speaker: `Vendor`
- Dialogue: `Now you're speaking my language. Just make sure my cut is worth the trouble.`
- Next: <END/EXTERNAL>

#### D42 - Dialogue

- Runtime Node ID: `e5507069-0abd-405f-b5bf-2661e0b5b15c`
- Speaker: `Vendor`
- Dialogue: `See that carpet? There's a way underneath it. To the VAULT! Go cause some trouble.`
- Next: <END/EXTERNAL>

#### D43 - Dialogue

- Runtime Node ID: `dcff1c5c-8907-4379-9008-ddb4d5a9c706`
- Speaker: `Vendor`
- Dialogue: `He doesn't appeal to me either. You know what? I have an idea...`
- Next: D42

### Flow

- D01 --> C01
- C01 --[Let's talkBusiness]--> D02
- C01 --[About that Carpet]--> D03
- C01 --[Never Mind]--> <END/EXTERNAL>
- D02 --> C02
- D03 --> D06
- C02 --[Coins]--> D04
- C02 --[Flowers]--> D07
- C02 --[Something else]--> D09
- C02 --[Never Mind]--> <END/EXTERNAL>
- D05 --> <END/EXTERNAL>
- D06 --> C03
- C03 --[[Secret entrance]]--> D05
- C03 --[[The dead remains]]--> D05
- D07 --> D08
- D08 --> <END/EXTERNAL>
- D09 --> C04
- C04 --[I was hoping you'd help me get inside that vault.]--> D10
- C04 --[I can hide your cimes, just help me out here]--> D11
- C04 --[I seek you, brother. We were lost once.]--> D18
- C04 --[I seek you... you look rich and, might I say, handsome.]--> D34
- D10 --> C05
- C05 --[I'd split the money once I sell it.]--> D41
- C05 --[That guard, PISSES me off!]--> D43
- D11 --> D13
- D12 --> D14
- D13 --> D12
- D14 --> D15
- D15 --> D16
- D17 --> D25
- D18 --> D19
- D19 --> D20
- D20 --> D17
- D21 --> D22
- C06 --[[Un-Threaten]]--> <END/EXTERNAL>
- D22 --> D30
- D23 --> D24
- D24 --> D27
- D25 --> D26
- D26 --> D23
- D27 --> D21
- D28 --> C06
- D29 --> C07
- D30 --> D33
- D31 --> D32
- D32 --> D29
- D33 --> D31
- C07 --[[Threaten]]--> D28
- D34 --> D36
- D35 --> D37
- D36 --> D35
- D37 --> D39
- D38 --> <END/EXTERNAL>
- D39 --> D40
- D40 --> D38
- D41 --> <END/EXTERNAL>
- D42 --> <END/EXTERNAL>
- D43 --> D42



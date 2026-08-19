# Dialogue Graph Audit

Generated: 19-08-2026 19:02:05
Graphs found: 22

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
## VendorFirstMeeting
========================================

Path: `Assets/DialogueSystem/Graphs/VendorDialogues/VendorFirstMeeting.dialoguegraph`

Entry: D01

### Nodes

#### D01 - Dialogue

- Runtime Node ID: `6960e357-03a0-4b20-a5b3-abe64d729301`
- Speaker: `Vendor`
- Dialogue: `Greetings! What carpet do you need?`
- Next: C01

#### C01 - Choice

- Runtime Node ID: `dc010747-61c6-4b25-be16-ac7dc9739e23`
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

- Runtime Node ID: `467bf8e0-293b-4d36-9b31-d8a4bc7ffee8`
- Speaker: `Vendor`
- Dialogue: `And, what do you bring, in exchange?`
- Next: C02

#### D03 - Dialogue

- Runtime Node ID: `e1f41c31-3455-461a-9dd5-423f4a42782c`
- Speaker: `Vendor`
- Dialogue: `We sell carpets, not rumors!`
- Next: C08

#### D04 - Dialogue

- Runtime Node ID: `917aab39-ef35-4f65-8952-3d55ae9c27a8`
- Speaker: `Vendor`
- Dialogue: `Bold of you to think of me as a Crime Partner!`
- Next: <END/EXTERNAL>

#### C02 - Choice

- Runtime Node ID: `8acc5021-5c97-4ffc-b0a8-8fb6be3d4cbf`
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

- Runtime Node ID: `dc388d36-ecf7-4f05-9d70-32d42924cacb`
- Speaker: `Vendor`
- Dialogue: `Ah… Why these rags of arts are worth mere pennies.`
- Next: D06

#### D06 - Dialogue

- Runtime Node ID: `23b3b2de-c4ec-43fd-b562-60ad6d9fc08b`
- Speaker: `Vendor`
- Dialogue: `You seek something more than carpets, don’t you?`
- Next: C05

#### D07 - Dialogue

- Runtime Node ID: `5441b5cb-7385-434f-b582-f7ba9b276987`
- Speaker: `Vendor`
- Dialogue: `Ugh! Peasant!`
- Next: D08

#### D08 - Dialogue

- Runtime Node ID: `52324476-4ef9-431a-adbc-77d551b00e9e`
- Speaker: `Vendor`
- Dialogue: `Get these away from me and come back when you have something useful for me.`
- Next: <END/EXTERNAL>

#### D09 - Dialogue

- Runtime Node ID: `757ffd7a-d9f7-483e-8948-cc1dd062d7e7`
- Speaker: `Vendor`
- Dialogue: `Okay then, but I must recieve something GOOD.`
- Next: C03

#### C03 - Choice

- Runtime Node ID: `dc8697a7-433c-47f5-b0d0-7769cb03fcde`
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

- Runtime Node ID: `8139b18b-7d11-4f43-acef-e0f01c6c0228`
- Speaker: `Vendor`
- Dialogue: `And, why would I help you in that case?`
- Next: C04

#### C04 - Choice

- Runtime Node ID: `3cd8073e-89ab-4b36-b3e6-618e1bf22221`
- Speaker: `Player`
- Dialogue: `You might like the answer...`
- Choices:
  - Choice 0: `I'd split the money once I sell it.`
    - Fuzzy Event ID: `VendorSplitDeal`
    - Required Bool Key: <none>
    - Destination: D45
  - Choice 1: `That guard, PISSES me off!`
    - Fuzzy Event ID: `VendorGuardGrudge`
    - Required Bool Key: `Guard.DismissedPlayer`
    - Destination: D46

#### C05 - Choice

- Runtime Node ID: `e5e39cb9-4923-420c-a167-5e2af9f82ec4`
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

- Runtime Node ID: `f91db521-0979-446a-86a2-343a88fd07e1`
- Speaker: `Vendor`
- Dialogue: `Royal business, is it? The guard would be of better help.`
- Next: <END/EXTERNAL>

#### D12 - Dialogue

- Runtime Node ID: `12408d2e-4ac6-4efe-94a5-77953b090402`
- Speaker: `Vendor`
- Dialogue: `Someone plans to rob it? I suggest you inform it to the guard`
- Next: <END/EXTERNAL>

#### D13 - Dialogue

- Runtime Node ID: `4b2cb962-7224-448e-a2d0-a35b03a1b5ad`
- Speaker: `Vendor`
- Dialogue: `You know you've got some mouth on you? I'll call the guards if you bother me again!`
- Next: <END/EXTERNAL>

#### D14 - Dialogue

- Runtime Node ID: `245738b0-be55-4e2d-a411-3429fd0e960f`
- Speaker: `Player`
- Dialogue: `I know the skeletons in your closet are rotting. I can help you`
- Next: D15

#### D15 - Dialogue

- Runtime Node ID: `316def70-4e8b-4311-b019-36c7aae35451`
- Speaker: `Player`
- Dialogue: `That is... if you cooperate`
- Next: D16

#### D16 - Dialogue

- Runtime Node ID: `0fb47911-ad96-48b5-b352-48d60ec7abaa`
- Speaker: `Vendor`
- Dialogue: `My dirt? Friend, you're about to join them.`
- Next: D17

#### D17 - Dialogue

- Runtime Node ID: `695cea63-49ce-428b-8488-25b883120992`
- Speaker: `Player`
- Dialogue: `I beg your pardon?`
- Next: D18

#### D18 - Dialogue

- Runtime Node ID: `ef06dfe1-9404-46ef-aa12-4ee7036a88c0`
- Speaker: `Vendor`
- Dialogue: `GUARD! This guy is a MENACE! He is stealing, THE APPLE!!!`
- Next: D19

#### D19 - Dialogue

- Runtime Node ID: `14c521fe-514a-48c7-b980-b74c18ae3349`
- Speaker: `Player`
- Dialogue: `Aw shit, here we go again.`
- Next: <END/EXTERNAL>

#### D20 - Dialogue

- Runtime Node ID: `b3cc6fdf-e65f-453a-a81f-2a3e580aa30d`
- Speaker: `Vendor`
- Dialogue: `Oh... No...`
- Next: D21

#### D21 - Dialogue

- Runtime Node ID: `cef75754-7336-46f1-8c5b-b58544096ba5`
- Speaker: `Player`
- Dialogue: `Oh... Yea...`
- Next: D22

#### D22 - Dialogue

- Runtime Node ID: `6d5a7499-3912-4f0e-a28e-970f1217dfcb`
- Speaker: `Vendor`
- Dialogue: `Oh, Gods!!`
- Next: D23

#### D23 - Dialogue

- Runtime Node ID: `62fa74c8-929c-4cee-a222-21c61019058d`
- Speaker: `Player`
- Dialogue: `Oh, the Devil!`
- Next: D24

#### D24 - Dialogue

- Runtime Node ID: `1604ac9b-3d5d-4429-9d4e-07f901ae4acb`
- Speaker: `Vendor`
- Dialogue: `Goodness Almighty!!!`
- Next: D25

#### D25 - Dialogue

- Runtime Node ID: `c8061548-26f4-4c5e-a45f-15e7c3ed143a`
- Speaker: `Player`
- Dialogue: `Can we stop now? I know you... killed Uncle Ben!`
- Next: D26

#### D26 - Dialogue

- Runtime Node ID: `b20d3e90-5175-4374-b442-91de92501873`
- Speaker: `Player`
- Dialogue: `Now if you just let me enter the vault`
- Next: D27

#### D27 - Dialogue

- Runtime Node ID: `bbe07c11-ef6c-4085-ac6f-d3730bd008ee`
- Speaker: `Vendor`
- Dialogue: `Uncle Ben?`
- Next: D28

#### D28 - Dialogue

- Runtime Node ID: `c7c3ef8f-dd2c-4ad6-8817-a4e812721fc3`
- Speaker: `Player`
- Dialogue: `Yeah, the one who stubbed his toe on our 2nd birthday`
- Next: D29

#### D29 - Dialogue

- Runtime Node ID: `e846921e-9b75-4c3c-9032-60824876d25a`
- Speaker: `Vendor`
- Dialogue: `The one with 3 wives?`
- Next: D30

#### D30 - Dialogue

- Runtime Node ID: `44a9be16-58d5-429d-90fe-7ed5cfd43de7`
- Speaker: `Player`
- Dialogue: `4 actually.`
- Next: D31

#### D31 - Dialogue

- Runtime Node ID: `24ddee32-c53b-46db-abea-46740c39ad36`
- Speaker: `Vendor`
- Dialogue: `Ay! Would you look at that? More money for us!`
- Next: D32

#### D32 - Dialogue

- Runtime Node ID: `62c40054-7d89-4f59-aacb-0a2262a9d7e0`
- Speaker: `Vendor`
- Dialogue: `Isn't that right? Brother? Who ratted me out?`
- Next: D33

#### D33 - Dialogue

- Runtime Node ID: `a8efb5e2-d03e-4ecd-8db5-f061a7471cfe`
- Speaker: `Player`
- Dialogue: `Now, ain't that sweet.`
- Next: D34

#### D34 - Dialogue

- Runtime Node ID: `dd4225a5-f885-4f58-9a62-f30e87f29d23`
- Speaker: `Player`
- Dialogue: `Now, before I slice your face off... The vault, please, dear brother?`
- Next: D35

#### D35 - Dialogue

- Runtime Node ID: `c993db27-e2e4-4614-9d21-f89322eec90f`
- Speaker: `Vendor`
- Dialogue: `Right, right, of course. Guar...!!`
- Next: C06

#### C06 - Choice

- Runtime Node ID: `b570f2bb-3c96-4557-a9ca-934f4dbbed92`
- Speaker: `Player`
- Dialogue: `Now, now.`
- Choices:
  - Choice 0: `[Threaten]`
    - Fuzzy Event ID: `VendorDeceiveThreat`
    - Required Bool Key: <none>
    - Destination: D36

#### D36 - Dialogue

- Runtime Node ID: `48378668-b575-4fec-90ec-660eb5cad943`
- Speaker: `Vendor`
- Dialogue: `Okay! Okay, fine!`
- Next: C07

#### C07 - Choice

- Runtime Node ID: `f607afef-b0aa-4350-8c9f-365d92ffb45a`
- Speaker: `Player`
- Dialogue: `See? Such love amongst brothers!`
- Choices:
  - Choice 0: `[Un-Threaten]`
    - Fuzzy Event ID: `VendorDeceiveReveal`
    - Required Bool Key: <none>
    - Destination: <END/EXTERNAL>

#### D37 - Dialogue

- Runtime Node ID: `e0b86904-4f0d-4bf4-8747-84dbe7c7436e`
- Speaker: `Vendor`
- Dialogue: `...`
- Next: D38

#### D38 - Dialogue

- Runtime Node ID: `e1d61f3b-5916-42a6-8907-0adb381b219a`
- Speaker: `Vendor`
- Dialogue: `You are not my type.`
- Next: D39

#### D39 - Dialogue

- Runtime Node ID: `38d53857-5744-426d-adcf-7680dc30e43b`
- Speaker: `Vendor`
- Dialogue: `But that Guard over there...`
- Next: D40

#### D40 - Dialogue

- Runtime Node ID: `ba68af4a-1d6b-44eb-93b4-a22da1d1eab8`
- Speaker: `Player`
- Dialogue: `You want me to.. charm him? For you?`
- Next: D41

#### D41 - Dialogue

- Runtime Node ID: `bccacca6-54fa-4692-b786-cc4f4b5f4e32`
- Speaker: `Vendor`
- Dialogue: `YES! Here, take these gold coins and try to make him understand`
- Next: D42

#### D42 - Dialogue

- Runtime Node ID: `61dcf9f4-b331-4574-969d-41375fe26456`
- Speaker: `Vendor`
- Dialogue: `He would very VERY much like it.`
- Next: D43

#### D43 - Dialogue

- Runtime Node ID: `6b4a41df-c362-4653-ae23-1a21d8969b76`
- Speaker: `Vendor`
- Dialogue: `*GASP* Don't tell him they are from me!`
- Next: <END/EXTERNAL>

#### C08 - Choice

- Runtime Node ID: `5e0e0475-67ad-486f-b722-136363098760`
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

- Runtime Node ID: `c07b7e48-3316-4ddc-b09e-77c3817b9359`
- Speaker: `Vendor`
- Dialogue: `Ahh! No please, just GO! There's the pit. Just please LEAVE ME ALONE!`
- Next: <END/EXTERNAL>

#### D45 - Dialogue

- Runtime Node ID: `ff8dd04d-33a6-4d6c-9b0d-477e874ccefc`
- Speaker: `Vendor`
- Dialogue: `Now you're speaking my language. Just make sure my cut is worth the trouble.`
- Next: <END/EXTERNAL>

#### D46 - Dialogue

- Runtime Node ID: `05f0fcba-effc-41c2-830b-0844e74783c5`
- Speaker: `Vendor`
- Dialogue: `He doesn't appeal to me either. You know what? I have an idea...`
- Next: D47

#### D47 - Dialogue

- Runtime Node ID: `b9713486-557c-4815-becc-d6cca06c1fbf`
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

- Runtime Node ID: `92c32a97-d4a9-4ced-a24c-9ca00791ea84`
- Speaker: `Vendor`
- Dialogue: `Greetings! What carpe— ...Oh. It's you.`
- Next: C01

#### C01 - Choice

- Runtime Node ID: `01b1468f-5eaf-450e-915c-cbe4582e63e7`
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

- Runtime Node ID: `10cc66c2-b3a7-4ba5-9587-d121028eda38`
- Speaker: `Vendor`
- Dialogue: `Fine. What are you offering`
- Next: C02

#### D03 - Dialogue

- Runtime Node ID: `e8d7bfee-8308-401e-a970-a20ee5adb3fc`
- Speaker: `Vendor`
- Dialogue: `You're still interested in that thing`
- Next: D06

#### C02 - Choice

- Runtime Node ID: `62199204-2ae3-4b40-a706-4c9744631c1e`
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

- Runtime Node ID: `2c125351-2e98-41f7-bafa-ce9883bdd554`
- Speaker: `Vendor`
- Dialogue: `I see you DO mean business. Now, what can I offer?`
- Next: <END>

#### D05 - Dialogue

- Runtime Node ID: `7170acae-99c7-4385-a324-5c62feadecb6`
- Speaker: `Vendor`
- Dialogue: `Ahh! No please, just GO! There's the pit. Just please LEAVE ME ALONE!`
- Next: <END/EXTERNAL>

#### D06 - Dialogue

- Runtime Node ID: `3546281a-ba12-480e-a20c-93cf98a5fe7d`
- Speaker: `Vendor`
- Dialogue: `I told you! We sell carpets, not rumors!`
- Next: C03

#### C03 - Choice

- Runtime Node ID: `35470936-43ee-414c-a34c-6ac76e43413d`
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

- Runtime Node ID: `46b7a5d1-0e41-45eb-93dd-1d58c81ef5a4`
- Speaker: `Vendor`
- Dialogue: `Ugh! Peasant!`
- Next: D08

#### D08 - Dialogue

- Runtime Node ID: `a9d5daec-62b5-41b4-9fe1-d32b74f7b8d9`
- Speaker: `Vendor`
- Dialogue: `Get these away from me and come back when you have something useful for me.`
- Next: <END/EXTERNAL>

#### D09 - Dialogue

- Runtime Node ID: `97134596-47c0-4a92-b005-63ff9cb4c514`
- Speaker: `Vendor`
- Dialogue: `Okay then, but I must recieve something GOOD.`
- Next: C04

#### C04 - Choice

- Runtime Node ID: `195feed3-09f9-4b3a-9485-b028ab6e0871`
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

- Runtime Node ID: `55b6a17a-b4c7-405e-8ee0-22472a1cf2a9`
- Speaker: `Vendor`
- Dialogue: `And, why would I help you in that case?`
- Next: C05

#### C05 - Choice

- Runtime Node ID: `35e9eec3-9372-4135-b34c-0269ff91ead1`
- Speaker: `Player`
- Dialogue: `You might like the answer...`
- Choices:
  - Choice 0: `I'd split the money once I sell it.`
    - Fuzzy Event ID: `VendorSplitDeal`
    - Required Bool Key: <none>
    - Destination: D41
  - Choice 1: `That guard, PISSES me off!`
    - Fuzzy Event ID: `VendorGuardGrudge`
    - Required Bool Key: `Guard.DismissedPlayer`
    - Destination: D43

#### D11 - Dialogue

- Runtime Node ID: `02521712-8a01-4152-a796-0388474c1d23`
- Speaker: `Player`
- Dialogue: `I know the skeletons in your closet are rotting. I can help you`
- Next: D13

#### D12 - Dialogue

- Runtime Node ID: `8df4850d-92d1-4826-8340-b723e50f0af0`
- Speaker: `Vendor`
- Dialogue: `My dirt? Friend, you're about to join them.`
- Next: D14

#### D13 - Dialogue

- Runtime Node ID: `0d9f50a3-dfb0-4f34-9b5a-105e1efcde9e`
- Speaker: `Player`
- Dialogue: `That is... if you cooperate`
- Next: D12

#### D14 - Dialogue

- Runtime Node ID: `214952bb-18fe-4dcc-af6e-3114b9d76c66`
- Speaker: `Player`
- Dialogue: `I beg your pardon?`
- Next: D15

#### D15 - Dialogue

- Runtime Node ID: `15a4f243-498d-4b80-9d16-7780258b33dc`
- Speaker: `Vendor`
- Dialogue: `GUARD! This guy is a MENACE! He is stealing, THE APPLE!!!`
- Next: D16

#### D16 - Dialogue

- Runtime Node ID: `1557c196-e092-441f-bc93-e492ed7aea20`
- Speaker: `Player`
- Dialogue: `Aw shit, here we go again.`
- Next: <END>

#### D17 - Dialogue

- Runtime Node ID: `5029e388-a6a7-4832-8618-77515f429c64`
- Speaker: `Player`
- Dialogue: `Oh, the Devil!`
- Next: D25

#### D18 - Dialogue

- Runtime Node ID: `224c2861-1529-4082-ba1e-5ea389f25924`
- Speaker: `Vendor`
- Dialogue: `Oh... No...`
- Next: D19

#### D19 - Dialogue

- Runtime Node ID: `f150d741-dc31-4b19-9f7b-8eca11ad8eaf`
- Speaker: `Player`
- Dialogue: `Oh... Yea...`
- Next: D20

#### D20 - Dialogue

- Runtime Node ID: `b7f096ea-c043-44cb-8f8e-e7d564ecbc7e`
- Speaker: `Vendor`
- Dialogue: `Oh, Gods!!`
- Next: D17

#### D21 - Dialogue

- Runtime Node ID: `fb4c4eaf-8ecd-45b3-8fb7-4e4db2948913`
- Speaker: `Vendor`
- Dialogue: `The one with 3 wives?`
- Next: D22

#### C06 - Choice

- Runtime Node ID: `fee8c56f-cfc6-4dda-adf8-3abacfc0749c`
- Speaker: `Player`
- Dialogue: `See? Such love amongst brothers!`
- Choices:
  - Choice 0: `[Un-Threaten]`
    - Fuzzy Event ID: `VendorDeceiveReveal`
    - Required Bool Key: <none>
    - Destination: <END/EXTERNAL>

#### D22 - Dialogue

- Runtime Node ID: `9f217366-3475-4e3c-8d02-5ac70b2092ee`
- Speaker: `Player`
- Dialogue: `4 actually.`
- Next: D30

#### D23 - Dialogue

- Runtime Node ID: `837afd1c-c5df-4192-b28b-4fc22932f512`
- Speaker: `Player`
- Dialogue: `Now if you just let me enter the vault`
- Next: D24

#### D24 - Dialogue

- Runtime Node ID: `dcb2aace-3157-4629-a249-c93dbfe45e56`
- Speaker: `Vendor`
- Dialogue: `Uncle Ben?`
- Next: D27

#### D25 - Dialogue

- Runtime Node ID: `ded57bf7-b54e-4253-ba23-e4d754e104bc`
- Speaker: `Vendor`
- Dialogue: `Goodness Almighty!!!`
- Next: D26

#### D26 - Dialogue

- Runtime Node ID: `8e74d1f4-5a0d-48d3-9b6c-a14aa9d01dcb`
- Speaker: `Player`
- Dialogue: `Can we stop now? I know you... killed Uncle Ben!`
- Next: D23

#### D27 - Dialogue

- Runtime Node ID: `6a2b8dae-978c-4513-bf0a-20782395fc7a`
- Speaker: `Player`
- Dialogue: `Yeah, the one who stubbed his toe on our 2nd birthday`
- Next: D21

#### D28 - Dialogue

- Runtime Node ID: `b6e2e29e-4197-426d-80d0-6007a5c9b811`
- Speaker: `Vendor`
- Dialogue: `Okay! Okay, fine!`
- Next: C06

#### D29 - Dialogue

- Runtime Node ID: `3c4b89aa-21e1-4e4e-b6d3-fc9b9a1b21f6`
- Speaker: `Vendor`
- Dialogue: `Right, right, of course. Guar...!!`
- Next: C07

#### D30 - Dialogue

- Runtime Node ID: `e987f4eb-e829-4ce5-86f1-6384f25c1514`
- Speaker: `Vendor`
- Dialogue: `Ay! Would you look at that? More money for us!`
- Next: D33

#### D31 - Dialogue

- Runtime Node ID: `50765601-d6a2-4142-880d-73257f946ab2`
- Speaker: `Player`
- Dialogue: `Now, ain't that sweet.`
- Next: D32

#### D32 - Dialogue

- Runtime Node ID: `52198d2c-a655-4b7f-b3da-31ab456e7334`
- Speaker: `Player`
- Dialogue: `Now, before I slice your face off... The vault, please, dear brother?`
- Next: D29

#### D33 - Dialogue

- Runtime Node ID: `405004e3-f6c8-42fd-ab96-80b3959a5e15`
- Speaker: `Vendor`
- Dialogue: `Isn't that right? Brother? Who ratted me out?`
- Next: D31

#### C07 - Choice

- Runtime Node ID: `3a9508e1-74c2-4e40-871c-33c413c5fb0c`
- Speaker: `Player`
- Dialogue: `Now, now.`
- Choices:
  - Choice 0: `[Threaten]`
    - Fuzzy Event ID: `VendorDeceiveThreat`
    - Required Bool Key: <none>
    - Destination: D28

#### D34 - Dialogue

- Runtime Node ID: `e8e78531-3ef5-45ee-aa8e-e43a9239c316`
- Speaker: `Vendor`
- Dialogue: `...`
- Next: D36

#### D35 - Dialogue

- Runtime Node ID: `ce7b4a34-680e-47ef-9072-b00783461094`
- Speaker: `Vendor`
- Dialogue: `But that Guard over there...`
- Next: D37

#### D36 - Dialogue

- Runtime Node ID: `e32a1536-753f-4554-8bda-b03d7bfe9f03`
- Speaker: `Vendor`
- Dialogue: `You are not my type.`
- Next: D35

#### D37 - Dialogue

- Runtime Node ID: `39f62aa5-03b0-44b4-905f-6d80e9764562`
- Speaker: `Player`
- Dialogue: `You want me to.. charm him? For you?`
- Next: D39

#### D38 - Dialogue

- Runtime Node ID: `b7566ca9-c20b-4020-be16-744c6a819ef1`
- Speaker: `Vendor`
- Dialogue: `*GASP* Don't tell him they are from me!`
- Next: <END/EXTERNAL>

#### D39 - Dialogue

- Runtime Node ID: `b590481d-de3d-4f91-8617-5967b6c0436d`
- Speaker: `Vendor`
- Dialogue: `YES! Here, take these gold coins and try to make him understand`
- Next: D40

#### D40 - Dialogue

- Runtime Node ID: `abb4e07e-95e1-42a9-addf-e7d553f88515`
- Speaker: `Vendor`
- Dialogue: `He would very VERY much like it.`
- Next: D38

#### D41 - Dialogue

- Runtime Node ID: `9aa6f340-0780-4b95-b446-9c77a313c7dd`
- Speaker: `Vendor`
- Dialogue: `Now you're speaking my language. Just make sure my cut is worth the trouble.`
- Next: <END/EXTERNAL>

#### D42 - Dialogue

- Runtime Node ID: `cbcb6f0d-5733-4c9b-a4ba-a2e57f2cdaa7`
- Speaker: `Vendor`
- Dialogue: `See that carpet? There's a way underneath it. To the VAULT! Go cause some trouble.`
- Next: <END/EXTERNAL>

#### D43 - Dialogue

- Runtime Node ID: `e872d539-dca1-4e3f-a93c-21904b1410cc`
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



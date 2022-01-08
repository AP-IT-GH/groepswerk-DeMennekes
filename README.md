# Penality Bowling

1. [Inleiding](#inleiding)
    - [Samenvatting](#samenvatting)
2. [Methoden](#methoden)
    - [Installatie](#installatie)
    - [Spelverloop](#spelverloop)
    - [Observaties, mogelijke acties en beloningen](#observaties-mogelijke-acties-en-beloningen)
    - [Spelobjecten](#spelobjecten)
    - [Gedragingen](#gedragingen)
    - [One-pager](#one-pager)
3. [Resultaten](#resultaten)
    - [Tensorboard](#tensorboard)
4. [Conclusie](#conclusie)

## Inleiding

Het concept van onze leefwereld is een spel waarbij het de bedoeling is om een doelpunt te scoren bij ons uitstekend getrainde doelman.

### Samenvatting

Via deze tutorial zullen wij u stapsgewijs gidsen doorheen ons eindproject van VR Experience in combinatie met Machine Learning. Tijdens het project maakten we gebruik van Unity, Anaconda en de ML-Agents package om een doelman (Agent) te laten bewegen om zo de aankomende ballen tegen te houden. Om de bal te rollen richting doel is het aangeraden gebruik te maken van een Oculus Quest.

Clone dit project en volg onze stappen om tot een gewenst resultaat te komen. Heel veel plezier met het uitvoeren van deze opdracht!

## Methoden

Om het project tot een goed einde te brengen en te kunnen reproduceren zullen er een aantal zaken klaargezet/geïnstalleerd moeten worden.

### Installatie

#### Software

Zorg er voor dat deze beide zeker geïnstalleerd zijn voordat je verder gaat met de volgende stappen.

Software        | Versie
--------------- | -------
Unity           | 2020.3.24f1
ML-Agents       | 2.0.0
Visual Studio   |
Python 3        | 3
Anaconda        |

#### Voorinstallatie

Om een mooie omgeving te creëren hebben we gebruik gemaakt van een package uit de Asset Store genaamd *Super Goalie(Basic)*. Je kan deze in de asset store installeren en vervolgens toevoegen in jouw Unity project.

![image](https://user-images.githubusercontent.com/61239203/148071708-74a0d733-ed7b-4fac-add5-76a9856aa812.png)

### Spelverloop

In het spel is een speelveld voorzien voor de speler die kan deelnemen aan het spel via een een *Oculus Quest*. Als gebruiker kan je jezelf verplaatsen doorheen het terrein.

Het is de bedoeling dat je de voetbal vastneemt en vervolgens rolt richting het doel. In het doel zal een getrainde keeper staan die je bal zal proberen tegen te houden.

> Er is de mogelijkheid om de moeilijkheidsgraad om te kunnen scoren, aan te passen. Dan zal de keeper beter getraind zijn en steeds vaker de bal tegenhouden.

### Observaties, mogelijke acties en beloningen

Observaties                             | Beloning/bestraffing
--------------------------------------- | ---------------------
Bal komt af op agent                    | 0
Bal collide met agent (random plaats)   | +0.2f
Bal collide met binnenste van "vangnet" | +1.0f + einde episode
Bal is in goal                          | -1.0f + einde episode

### Spelobjecten

Onderstaande afbeelding toont de volledige hiërarchie binnen de spelobjecten met hun benaming zoals ze in deze handleiding gebruikt zullen worden.
> insert afbeelding

Het terrein zelf bestaat uit een 3D stadion waar je doorheen kan lopen als gebruiker met een VR-bril (Oculus Quest).

#### Player

De gebruiker zelf kan doorheen de spelomgeving lopen. Het doel is om de bal vast te nemen en proberen te scoren bij de getrainde keeper.

#### Keeper (AI Agent)

Gedurende het spel is de keeper de agent. Deze zal getraind worden op 3 verschillende niveau's:

Niveau      | Beschrijving
----------- | ------------
Easy        | De keeper is niet tot zijn optimale capaciteiten getraind en zal dus niet alle ballen kunnen pakken.
Average     | De keeper is net iets beter getraind als het vorige niveau.
Advanced    | Hier is het de bedoeling dat de keeper het beste is getraind en het moeilijker is om bij hem te kunnen scoren.

#### Goal

Het doel van het spel is om in de goal te kunnen scoren als speler. Dit gebeurt als de bal die gerold werd over de achterlijn van de goal gaat.

### Gedragingen

De objecten in het spel hebben bepaalde gedragingen die gedefinieerd worden in scripts. Hier beschrijven we welke scripts bij welke objecten horen en wat de gedragingen van de objecten dus precies zijn.

#### Agent.cs

De Agent.cs script bepaalt het gedrag van de Keeper. In dit script doen we het volgende:

- De startpositie van de keeper instellen
- De bewegingsrichting (links/rechts) van de keeper bepalen en andere bewegingsrichtingen uitsluiten
- Het scorebord updaten bij elke frame
- Acties toewijzen aan de linker en rechter pijltoetsen, zodat we als menselijke controller kunnen testen
- De keeper links of rechts laten bewegen wanneer de Decision Requester een actie heeft gekozen voor de keeper
- De bewegingskracht van de keeper bepalen
- Wanneer de agent collide met de bal, dan geven we hem een beloning, verwijderen we de bal van het veld en eindigen we de episode.

#### Axis.cs

Korte beschrijving van het script.

#### BallDespawner.cs

Korte beschrijving van het script.

#### BallSpawner.cs

Korte beschrijving van het script.

> Nog gedetailleerder maken: "script A zorgt ervoor dat ... wordt gebruikt door object B ..."

### One-pager

> Alle informatie van de one-pager
> Indien van toepassing: waar jullie afwijken van de one-pager en waarom

## Resultaten

Zoals hierboven uitgelegd wordt de agent getraind. Die trainingen leverden uiteindelijk een resultaat op.

Dan rest ons nog de vraag: wat kunnen we afleiden en uiteindelijk concluderen uit de resultaten?

### Tensorboard

Het resultaat van de trainingsfase kun je observeren op Tensorboard. We hebben in totaal zes trainingen gedaan. 

Dit is de legende van de training-resultaten met hun run-id en bijhorende kleur op Tensorboard:

![image](https://user-images.githubusercontent.com/35467395/148654117-7001d56f-1686-4ab4-b2b5-68830031c7b1.png)

We bespreken vervolgens de Tensorboard grafieken en hun beschrijving.

#### Training01

Hier is de grafiek van de eerste training te zien. Zoals verwacht is het resultaat van de eerste training ondermaads.

![image](https://user-images.githubusercontent.com/35467395/148653157-9275bce5-2ecf-44d2-8666-41bd0585d281.png)

#### Training02 - Training05

Trainingen 02 tot 05 bespreken we samen. Deze trainingen hebben we elke keer vroeger moeten stoppen omdat we merkten dat ze niet zo goed werkte. Om dat probleem op te lossen hebben we de hoogte van de rays en mass van de keeper aangepast. Het resultaat is op volgende afbeelding te zien.

![image](https://user-images.githubusercontent.com/35467395/148653454-367673c4-c488-43db-9d76-2506598331b2.png)

#### Training06

Uiteindelijk hebben we voor Training06 een stuk code toegevoegd om de force dat op de keeper staat er af te halen. Dat hebben we gedaan omdat de keeper momentum kreeg van de bal en daardoor ver weg vloog (lol). Het resultaat van Training06 kun je hier zien:

![image](https://user-images.githubusercontent.com/35467395/148653619-67518b06-84e3-4e79-a2ca-fab00f5b27c0.png)

## Conclusie

Tijdens dit project hebben we met behulp van Unity en Machine Learning een spel gecreëerd waarin een gebruiker met een *Oculus Quest* doelpunten kan proberen scoren in de goal van een getrainde ML Agent.

Hier zie je een overzicht van de resultaten van alle zes trainingen. Het is duidelijk te zien dat de resultaten sterk uiteen lopen.

![image](https://user-images.githubusercontent.com/35467395/148652731-0238f65a-38d0-4409-a5c2-11aa68f51c96.png)

Ongeacht de "problemen" vinden we dat de we uiteindelijk met een mooi resultaat zijn geëindigd met een Agent die we eigenlijk bijna perfect kunnen noemen.

> Verbeteringen naar de toekomst toe?

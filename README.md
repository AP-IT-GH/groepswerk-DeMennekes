# Penality Bowling

1. [Inleiding](#inleiding)
    - [Samenvatting](#samenvatting)
2. [Methoden](#methoden)
    - [Installatie](#installatie)
    - [Spelverloop](#spelverloop)
    - [OAB](#oab)
3. [Resultaten](#resultaten)
4. [Conclusie](#conclusie)

## Inleiding

Het concept van onze leefwereld is een spel waarbij het de bedoeling is om een doelpunt te scoren bij ons uitstekend getrainde doelman.

### Samenvatting

Via deze tutorial zullen wij u stapsgewijs gidsen doorheen ons eindproject van VR Experience in combinatie met Machine Learning. Tijdens het project maakten we gebruik van Unity, Anaconda en de ML-Agents package om een doelman (Agent) te laten bewegen om zo de aankomende ballen tegen te houden. Om de bal te rollen richting doel is het aangeraden gebruik te maken van een Oculus Quest. Clone dit project en volg onze stappen om tot een gewenst resultaat te komen. Heel veel plezier met het uitvoeren van deze opdracht!

## Methoden

Om het project tot een goed einde te brengen en te kunnen reproduceren zullen er een aantal zaken klaargezet/geïnstalleerd moeten worden.

### Installatie

#### Software

Software    | Versie
----------- | ---------
Unity       | 2020.3.21f1
ML-Agents   | 2.0.0

Zorg er voor dat deze beide zeker geïnstalleerd zijn voordat je verder gaat met de volgende stappen.

#### Voorinstallatie

Om een mooie omgeving te creëren hebben we gebruik gemaakt van een package uit de Asset Store genaamd *Super Goalie(Basic)*. Je kan deze in de asset store installeren en vervolgens toevoegen in jouw Unity project.

![image](https://user-images.githubusercontent.com/61239203/148071708-74a0d733-ed7b-4fac-add5-76a9856aa812.png)

### Spelverloop

In het spel is een speelveld voorzien voor de speler die kan deelnemen aan het spel via een een *Oculus Quest*. Als gebruiker kan je jezelf verplaatsen doorheen het terrein. Het is de bedoeling dat je de voetbal vastneemt en vervolgens rolt richting het doel. In het doel zal een getrainde keeper staan die je bal zal proberen tegen te houden. (Er is de mogelijkheid om de moeilijkheidsgraad om te kunnen scoren, aan te passen. Dan zal de keeper beter getraind zijn en steeds vaker de bal tegenhouden.)

### Observaties, mogelijke acties en beloningen

Observaties | Beloning/bestraffing
--- | ---
Bal komt af op agent    | 0
Bal collide met agent (random plaats)   | +0.2f
Bal collide met binnenste van "vangnet" | +1.0f + einde episode
Bal is in goal  | -1.0f + einde episode

### Objecten + gedragingen

Het terrein zelf bestaat uit een 3D stadion waar je doorheen kan lopen als gebruiker (Oculus Quest).

#### Player

De gebruiker zelf kan doorheen de spelomgeving lopen. Het doel is om de bal vast te nemen en proberen te scoren bij de getrainde keeper.

#### AI Agent

Gedurende het spel is de keeper de agent. Deze zal getraind worden op 3 verschillende niveau's:

- Easy: de keeper is niet tot zijn optimale capaciteiten getraind en zal dus niet alle ballen kunnen pakken.
- Average: de keeper is net iets beter getraind als het vorige niveau.
- Advanced: hier is het de bedoeling dat de keeper het beste is getraind en het moeilijker is om bij hem te kunnen scoren.

#### Doel

Het doel van het spel is om in de goal te kunnen scoren als speler. Dit gebeurt als de bal die gerold werd over de achterlijn van de goal gaat.

### One-pager

> Alle informatie van de one-pager
> Indien van toepassing: waar jullie afwijken van de one-pager en waarom

## Resultaten van de training

> Resultaten van de training met Tensorboard afbeeldingen

### Tensorboard

> Tensorboard afbeeldingen
> Beschrijving van de Tensorboard grafieken

### Waarnemingen

> Opvallende waarnemingen tijdens het trainen

## Conclusie

> Eén zin die nog eens samenvat wat jullie hebben gedaan
> Kort overzicht resultaten (2 á 3 zinnen zonder cijfers te vernoemen)
> Een 'persoonlijke' visie op de resultaten, wat betekenen de resultaten nu eigenlijk
> Verbeteringen naar de toekomst toe

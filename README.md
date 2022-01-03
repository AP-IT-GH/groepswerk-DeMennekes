# VR Experience: Penality Bowling


# Inhoudstafel
1. [Introductie](#introductie)
2. [Nodige software en voorinstallatie](#benodigdheden)
3. [Spelverloop](#spelverloop)
4. [De spelomgeving](#spelomgeving)
    - [Player](#player)
    - [AI Agent](#agent)
    - [Doel](#doel)
5. [Scripts (C#)](#allescripts)
    - [Agent.cs](#agentscript)
    - [Axis.cs](#axisscript)
    - [BallDespawner.cs](#despawnerscript)
    - [BallSpawner.cs](#spawnerscript)
6. [Training](#training)
    - [Anaconda](#anaconda)
    - [Configuratie](#configuratie)
    - [Tensorboard](#tensorboard)
    - [Resultaten](#resultaten)
7. [Conclusie](#conclusie)

<br>

## Introductie <a name="introductie"></a>
Via deze tutorial zullen wij u stapsgewijs gidsen doorheen ons eindproject van VR Experience in combinatie met Machine Learning . Tijdens het project maakten we gebruik van Unity, Anaconda en de ML-Agents package om een doelman (Agent) te laten bewegen om zo de aankomende ballen tegen te houden. Het concept van onze leefwereld is een spel waarbij het de bedoeling is om een doelpunt te scoren bij ons uitstekend getrainde doelman. Om de bal te rollen richting doel is het aangeraden gebruik te maken van een Oculus Quest.  Clone dit project en volg onze stappen om tot een gewenst resultaat te komen. Heel veel plezier met het uitvoeren van deze opdracht!
<br>

## Nodige software en voorinstallatie <a name="benodigdheden"></a>
Om het project tot een goed einde te brengen zullen er een aantal zaken klaargezet/geïnstalleerd moeten worden. 
### Software
Om het project te kunnen bouwen zullen we gebruik maken van Unity als development platform. Om machine learning te integreren maken we gebruik van het ML-Agents package. Zorg er voor dat deze beide zeker geïnstalleerd zijn voordat je verder gaat met de volgende stappen. 
### Voorinstallatie
Om een mooie omgeving te creëren hebben we gebruik gemaakt van een package uit de asset store genaamd …. . Je kan deze in de asset store installeren en vervolgens toevoegen in jouw Unity project. 
<br>

## Spelverloop <a name="spelverloop"></a>
Het spel zal als volgt gaan: 
Er is een speelveld voorzien voor de speler die kan deelnemen aan het spel via een een Oculus Quest. Als gebruiker kan je jezelf verplaatsen doorheen het terrein. Het is de bedoeling dat je de voetbal vastneemt en vanaf het schuine platform laat rollen richting het doel. In het doel zal een getrainde keeper staan die je bal zal proberen tegen te houden. Er is de mogelijkheid om de moeilijkheidsgraad om te kunnen scoren, aan te passen. Dan zal de keeper beter getraind zijn en steeds vaker de bal tegenhouden. 
<br>

## De spelomgeving <a name="spelomgeving"></a>
Het terrein zelf bestaat uit een 3D stadion waar je doorheen kan lopen als gebruiker (Oculus Quest)
### Player <a name="player"></a>

### AI Agent <a name="agent"></a>
Gedurende het spel is de keeper de agent. Deze zal getraind worden op 3 verschillende niveau's:<br>
Easy: De keeper is niet tot zijn optimale capaciteiten getraind en zal dus niet alle ballen kunnen pakken.<br>
Average: De keeper is net iets beter getraind als het vorige niveau. <br>
Advanced: Hier is het de bedoeling dat de keeper het beste is getraind en het moeilijker is om bij hem te kunnen scoren.<br>
### Doel <a name="doel"></a>
Het doel van het spel is om in de goal te kunnen scoren als speler. Dit gebeurt als de bal die gerolt werd over de achterlijn van de goal gaat.
<br>

## Scripts <a name="allescripts"></a>
### Agent.cs <a name="agentscript"></a>
### Axis.c <a name="axisscript"></a>
### BallDespawner.cs <a name="despawnerscript"></a>
### BallSpawner.cs <a name="spawnerscript"></a>

<br>

## Training <a name="training"></a>
### Anaconda <a name="anaconda"></a>
### Configuratie <a name="configuratie"></a>
### Tensorboard <a name="tensorboard"></a>
### Resultaten <a name="resultaten"></a>

<br>

## Conclusie <a name="conclusie"></a>

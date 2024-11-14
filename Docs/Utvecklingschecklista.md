# Utvecklingsplan

## Buggar

### 1. Fix Console Output
- [V] Vid en nyskapad transaktion ger programmet en promt
	  b�de i logiken och vid CustomerMenu. Ger en dubbelpromt 2 g�nger om
	  Vi beh�ver ta bort f�rsta promten i logiken och endast ha den i CustomerMenu

### 2. Text Korrigering
- [V] �ndra text fr�n "f�rv�ntad v�ntetid" f�r transaktion till mer tydligt. Ex. "f�rv�ntad transaktionsk�rning"

## Funktioner

### 1. SavingsAccount Implementation
- [V] L�gg till OpenSavingsAccount i MenuChoice enum
- [V] Uppdatera MenuText dictionary
- [V] Skapa HandleOpenNewAccount metod i CustomerMenu
- [V] Testa sparkontofunktionalitet

### 2. Admin Funktionalitet
- [V] F�rdigst�ll Admin.cs
- [V] Skapa HandleAdminLogin i MainMenu och skapa en instans av Admin likt HandleCustomerLogin
- [V] Utveckla AdminMenu och koppla mot Admin.cs

## Extra

### 1. Dynamisk Transaktionstid (�ndrar vi tiden i TransactionScheduler beh�ver vi �ndra alla h�rdkodade text)
- [V] Skapa variabel f�r 15-minuters tiden (15 min �r dock krav fr�n backlog). 
- [V] Ers�tt alla h�rdkodade referenser
- [V] Testa att tiden uppdateras �verallt
# Utvecklingsplan

## Buggar

### 1. Fix Console Output
- [ ] Vid en nyskapad transaktion ger programmet en promt
	  b�de i logiken och vid CustomerMenu. Ger en dubbelpromt 2 g�nger om
	  Vi beh�ver ta bort f�rsta promten i logiken och endast ha den i CustomerMenu

### 2. Text Korrigering
- [ ] �ndra text fr�n "f�rv�ntad v�ntetid" f�r transaktion till mer tydligt. Ex. "f�rv�ntad transaktionsk�rning"

## Funktioner

### 1. SavingsAccount Implementation
- [ ] L�gg till OpenSavingsAccount i MenuChoice enum
- [ ] Uppdatera MenuText dictionary
- [ ] Skapa HandleOpenNewAccount metod i CustomerMenu
- [ ] Testa sparkontofunktionalitet

### 2. Admin Funktionalitet
- [ ] F�rdigst�ll Admin.cs
- [ ] Skapa HandleAdminLogin i MainMenu och skapa en instans av Admin likt HandleCustomerLogin
- [ ] Utveckla AdminMenu och koppla mot Admin.cs

## Extra

### 1. Dynamisk Transaktionstid (�ndrar vi tiden i TransactionScheduler beh�ver vi �ndra alla h�rdkodade text)
- [ ] Skapa variabel f�r 15-minuters tiden (15 min �r dock krav fr�n backlog). 
- [ ] Ers�tt alla h�rdkodade referenser
- [ ] Testa att tiden uppdateras �verallt
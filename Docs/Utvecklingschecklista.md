# Utvecklingsplan

## Buggar

### 1. Fix Console Output
- [V] Vid en nyskapad transaktion ger programmet en promt
	  både i logiken och vid CustomerMenu. Ger en dubbelpromt 2 gånger om
	  Vi behöver ta bort första promten i logiken och endast ha den i CustomerMenu

### 2. Text Korrigering
- [V] Ändra text från "förväntad väntetid" för transaktion till mer tydligt. Ex. "förväntad transaktionskörning"

## Funktioner

### 1. SavingsAccount Implementation
- [V] Lägg till OpenSavingsAccount i MenuChoice enum
- [V] Uppdatera MenuText dictionary
- [V] Skapa HandleOpenNewAccount metod i CustomerMenu
- [V] Testa sparkontofunktionalitet

### 2. Admin Funktionalitet
- [V] Färdigställ Admin.cs
- [V] Skapa HandleAdminLogin i MainMenu och skapa en instans av Admin likt HandleCustomerLogin
- [V] Utveckla AdminMenu och koppla mot Admin.cs

## Extra

### 1. Dynamisk Transaktionstid (Ändrar vi tiden i TransactionScheduler behöver vi ändra alla hårdkodade text)
- [V] Skapa variabel för 15-minuters tiden (15 min är dock krav från backlog). 
- [V] Ersätt alla hårdkodade referenser
- [V] Testa att tiden uppdateras överallt
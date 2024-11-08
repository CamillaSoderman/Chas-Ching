# Utvecklingsplan

## Buggar

### 1. Fix Console Output
- [ ] Vid en nyskapad transaktion ger programmet en promt
	  både i logiken och vid CustomerMenu. Ger en dubbelpromt 2 gånger om
	  Vi behöver ta bort första promten i logiken och endast ha den i CustomerMenu

### 2. Text Korrigering
- [ ] Ändra text från "förväntad väntetid" för transaktion till mer tydligt. Ex. "förväntad transaktionskörning"

## Funktioner

### 1. SavingsAccount Implementation
- [ ] Lägg till OpenSavingsAccount i MenuChoice enum
- [ ] Uppdatera MenuText dictionary
- [ ] Skapa HandleOpenNewAccount metod i CustomerMenu
- [ ] Testa sparkontofunktionalitet

### 2. Admin Funktionalitet
- [ ] Färdigställ Admin.cs
- [ ] Skapa HandleAdminLogin i MainMenu och skapa en instans av Admin likt HandleCustomerLogin
- [ ] Utveckla AdminMenu och koppla mot Admin.cs

## Extra

### 1. Dynamisk Transaktionstid (Ändrar vi tiden i TransactionScheduler behöver vi ändra alla hårdkodade text)
- [ ] Skapa variabel för 15-minuters tiden (15 min är dock krav från backlog). 
- [ ] Ersätt alla hårdkodade referenser
- [ ] Testa att tiden uppdateras överallt
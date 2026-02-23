# FitnessTracker

Ez egy egyszerű Windows Forms alkalmazás, amely lehetővé teszi sporttevékenységek rögzítését CSV fájlba, majd az adatok MySQL adatbázisba történő feltöltését.

---

## Funkciók

- Sportág, dátum, időtartam (perc) és helyszín rögzítése.
- Adatok CSV fájlba írása és mentése a helyi gépen.
- CSV adatok importálása MySQL adatbázisba.
- Ellenőrzés a kötelező mezők és az időtartam helyessége szempontjából.
- Automatikus CSV fájl törlés az adatbázisba történő sikeres feltöltés után.

---

## Telepítés

```
1. Klónozd a projektet a GitHub-ról:

git clone https://github.com/felhasznalonev/FitnessTracker.git

2. Nyisd meg a projektet Visual Studio-ban (.sln fájl).

2. Telepítsd a szükséges NuGet csomagokat:
- CsvHelper
- MySql.Data

4. Állítsd be a dictionaryPath változót a projekt Form1.cs fájljában a helyi elérési útra:
public static string dictionaryPath = "C:\\Users\\Patrik\\Documents\\GitHub\\FitnessTracker\\FitnessTracker\\FitnessTracker";

5. Állítsd be a MySQL kapcsolatot a button2_Click metódusban:
string connectionString = "Server=localhost;Database=adatok;Uid=root";
```
## Használat:
```
1. Indítsd el az alkalmazást.

2. Töltsd ki a mezőket:
- Sportág: a sport neve.
- Dátum: az esemény dátuma.
- Időtartam (perc): az edzés időtartama pozitív egész számként.
- Helyszín: az edzés helyszíne.

3. Kattints az IMPORT gombra, hogy a beírt adat CSV fájlba kerüljön.

4. Ha az adatok CSV-ben vannak, kattints az EXPORT gombra, hogy az adatok MySQL adatbázisba kerüljenek.

5. Sikeres feltöltés esetén a CSV fájl automatikusan törlődik.
```

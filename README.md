# sistem-alegere-teme
Aplicație web pentru gestionarea alegerii temelor de proiect de către studenți.

Structura fișierelor:
-Controllers
-Models
-ViewModels
-Views
oAccount
oHome
oShared
oTeme

Într-un proiect ASP.NET Core MVC, structura fișierelor urmează modelul Model-View-Controller (MVC).

1. Controllers (Controlere)
Controlerele sunt responsabile pentru logica aplicației și gestionarea cererilor HTTP. Ele acționează ca intermediari între Model și View, preluând datele de la model și transmițându-le către view-uri pentru a fi afișate utilizatorului.
Exemple:
AccountController.cs – gestionează autentificarea, înregistrarea utilizatorilor.
HomeController.cs – gestionează pagina principală a aplicației.
TemeController.cs – gestionează funcționalitățile legate de teme.

2. Models (Modele)
Modelele reprezintă datele și logica de acces la acestea. De obicei, ele sunt mapate pe baza de date și definesc structura obiectelor pe care aplicația le manipulează.
Exemple:
Users.cs
DificultyLevel.cs
Items.cs
TemaAleasa.cs

3. ViewModels
ViewModel-urile sunt obiecte care combină mai multe modele și date necesare pentru a fi afișate într-un view. Ele sunt utile pentru a evita expunerea directă a modelelor către view-uri și pentru a optimiza datele transmise.
Exemple:
RegisterViewModel.cs 
LoginViewModel.cs 
TemeViewModel.cs 
VerifyEmailViewModel.cs 
ChangePasswordViewModel.cs 

4. Views (Vederi)
View-urile sunt responsabile pentru afișarea interfeței utilizatorului. Acestea folosesc Razor (.cshtml) pentru a combina HTML cu datele primite de la controler.
Structura folderului Views:
Account – conține pagini legate de autentificare (Login.cshtml, Register.cshtml etc.).
Home – conține pagini generale, precum Index.cshtml.
Shared – conține elemente reutilizabile, precum _Layout.cshtml (layout-ul principal) și _ValidationScriptsPartial.cshtml (pentru validarea formularelor).
Teme – conține pagini pentru gestionarea temelor (Index.cshtml, AlegereTema.cshtml, Create.cshtml etc.).

5. Conexiuni între componente
Controlerul primește cereri HTTP, procesează datele din Model, apoi trimite un ViewModel către View.
Modelul interacționează cu baza de date, de obicei printr-un ORM precum Entity Framework Core.
View-urile afișează datele primite de la controler folosind sintaxa Razor.

Exemplu de flux de lucru
1.Utilizatorul accesează /Teme/Edit/1.
2.TemeController primește cererea, preia datele temei cu ID = 1 din Model și le pune într-un ViewModel.
3.View-ul Edit.cshtml afișează datele temei utilizând Razor.
Structura bazei de date:


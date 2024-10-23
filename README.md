# Aurible - Application Angular Ionic / C# Dotnet
Bienvenue dans le dépôt du backend de Aurible, une application dérivée d'Audible. Aurible permet aux utilisateurs de profiter de la lecture de livres grâce un Text-to-Speech aux petits oignons.

🎯 Objectif
L'objectif principal d'Aurible est de fournir une expérience de lecture fluide et immersive, où les utilisateurs peuvent écouter leurs livres préférés grâce à un service de TTS performant. Le projet utilise des technologies Azure pour la gestion de la connexion utilisateur et la conversion du TTS.

✨ Fonctionnalités
Connexion via Microsoft Azure : Les utilisateurs peuvent se connecter en toute sécurité en utilisant leur compte Microsoft grâce à Azure Active Directory (Azure AD).
Lecture avec TTS (Text-to-Speech) : Profitez d'une lecture automatisée de vos livres grâce à la technologie de synthèse vocale d'Azure.
Gestion des livres : Accédez à une bibliothèque de livres numériques à écouter.
🔧 Technologies
Ce projet utilise les technologies suivantes :

Azure Active Directory (Azure AD) pour la gestion des utilisateurs et de l'authentification.
Azure Text-to-Speech (TTS) pour la lecture des livres.
Ionic Angular pour le développement frontend.
C# Dotnet et des API pour la communication entre le frontend et le backend.
🚀 Démarrer le projet
Pour commencer avec ce projet en local :

Clonez ce dépôt :
git clone https://github.com/VivienS5/AuribleDotnet-back.git

Installez les dépendances :
dotnet restore

Configurez les variables d'environnement pour Azure :
Créez un fichier appsettings.json ou utilisez les fichiers secrets .NET pour y renseigner les configurations nécessaires, telles que :
Les identifiants Azure AD pour l'authentification.
Les paramètres pour le service Azure Text-to-Speech.
La chaîne de connexion pour la base de données SQL Server.

Mettez en place la base de données :
Utilisez les migrations Entity Framework pour configurer la base de données :
dotnet ef database update

Lancez l'application backend :
dotnet run
Le serveur backend tournera sur http://localhost:5176.

🔗 Partie frontend
Pour la partie frontend du projet, veuillez vous référer aux instructions présentes dans le dépôt suivant : https://github.com/VivienS5/AuribleDotnet-front

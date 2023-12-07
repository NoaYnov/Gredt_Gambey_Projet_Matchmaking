# Projet de Matchmaking et Jeu en Réseau

Ce projet vise à créer un système de Matchmaking en réseau pour des jeux de plateau au tour par tour. Il se compose de trois éléments principaux : un serveur de Matchmaking, un logiciel client et une base de données.

## Fonctionnalités

### Modèle de Données

Le projet implémente un modèle de données comprenant :

- **File d'Attente** : Chaque attendant est caractérisé par :
  - Moyen de communication (par exemple, IP et port)
  - Pseudo
  - Date d'entrée dans la file

- **Matchs** : Contient pour chaque match :
  - Moyen de communication avec joueur 1 et joueur 2
  - Plateau de jeu
  - Statut du match (terminé ou non)
  - Résultat : victoire du joueur 1, du joueur 2 ou égalité

- **Tours** : Chaque tour comprend :
  - Lien avec le match
  - Joueur ayant joué (joueur 1 ou joueur 2)
  - Information sur le coup joué (selon le jeu choisi)

### Serveur de Matchmaking

Le serveur de Matchmaking offre :

- Liaison avec la base de données
- Système de socket avec actions telles que :
  - Arrivée d'un client dans la file d'attente (réception)
  - Début d'un match (envoi)
  - Réception et envoi de tours
  - Fin d'un match (envoi)
- Vérification constante de la file d'attente et création de matchs en conséquence
- Logique de jeu pour un jeu de plateau au tour par tour (ex : Puissance 4, Dames, Morpion)

### Logiciel Client

Le logiciel client comprend :

- Système de socket avec actions comme :
  - Entrée en file d'attente (envoi)
  - Réception du début d'un match
  - Envoi de coups joués
  - Réception des coups adverses
  - Réception de la fin d'un match
- Implémentation partielle de la logique du jeu choisi
  - Interface Homme-Machine (IHM) pour jouer
  - Interface en ligne de commande (CLI) avec intelligence artificielle (IA)

## Difficulté des Tâches

Les fonctionnalités sont catégorisées par niveaux de difficulté :

- **Niveau 1** : Modèle de données de base
- **Niveau 2** : Implémentation partielle de la logique du jeu
- **Niveau 3** : Implémentation IHM ou CLI avec IA
- **Niveau 5** : Systèmes de socket pour les interactions client-serveur et logique de jeu

---

Ce README offre une vue d'ensemble des composants et des fonctionnalités attendues dans le projet de Matchmaking en réseau. Il peut être étendu avec des instructions d'installation, d'utilisation et de contribution, selon les besoins du projet.

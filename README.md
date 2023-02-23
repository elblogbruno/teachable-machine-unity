# Labo 3 

## 1. Introduction

Ce laboratoire a pour but de developer le jeu Roll-A-Ball en interagissant avec notre main grace a Google Teachable Machine.

- Roll-A-Ball-Teachable contient le projet avec l'intégration de la Teachable Machine.
- Server_node contient le serveur qui sera utilisé pour communiquer avec la Teachable Machine.
- P5_code contient le code javascript qui exécute le modèle dans le navigateur.
- Labo3-ws-teachable contient le projet créé avec le tutoriel de Patrice.

Notre jeu peut reconnaître 8 gestes. Les quatre premiers gestes consistent à déplacer la balle à droite, à gauche, en bas et en haut.
Nous avons deux autres gestes dédiés à la pause du jeu. L'un permet de mettre le jeu en pause, et l'autre de masquer le menu de pause.
Nous avons un geste pour faire sauter le ballon. Notre dernier geste est une action neutre, ce qui signifie que rien ne se passe lorsque nous le faisons.

Le HUD de notre jeu indique le nombre de ramassages, la vitesse du joueur, ses mouvements (gestes ou clavier), 
et le temps restant pour terminer le jeu.

Nous disposons également de deux modes de jeu, l'un avec les gestes et l'autre avec le clavier. 
L'utilisateur peut changer de mode en appuyant sur la touche "m" du clavier.
Ils peuvent également mettre le jeu en pause en appuyant sur la touche "p" du clavier.

Nous disposons de deux fonctionnalités supplémentaires. La première est que le joueur peut sauter. 
La seconde est qu'il y a quatre plateformes dans le jeu. Les joueurs doivent sauter dessus pour ramasser les objets.

Pour gagner le jeu, vous devez ramasser tous les objets sur l'écran en 1 minute. 
Si vous dépassez une minute, vous perdez. Une autre façon de perdre est de sauter en dehors de la plate-forme.


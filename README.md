# SheepIt2

mardi 26 ou mercredi 27 => gamemanager instancie un loup et de smoutons à des spawn différents. Gère le cycle de changement de niveau
mouton/loup mouvement différents, spells, manger/points

gamemanager => server authority. Score point est seulement sur le serveur et va se synchronized pour donner aux clients sont score.
Quand il sais que tout les mecs sont capout, il set les skin et réactive les joueurs, voir le respawn peut être.

Commands pour le comptage de point?
RPC pour la stratégie et skin?

Dans le lobby une ligne où on doit mettre "ready" est un joueur. Le autocreate enleve la creation automatique. et le On serverAddPlayer est appellé à ce moment

TODO:
Game manager:
- gérer cas d'arret quand prédateur mange (sachant que points peuvent varié si survie précédamment)
- Refactoriser Timer (limit négative pour illimité/event quand timer fini/ etc...)
- Network du OnCollision - voir pour améliorer le network pour éviter les collision c^oté clients qui trigger pas côté serveur 
- Ou laisser le GameManager présent sur les client et mettre des isserveur dans tout les fonctions, et mettre neplace un système pour pas compter deux fois les points
- Mettre isPreda dans le tableau de score

NEXT:
- map plus grande
- refactor du code
- ability mouton
- ability loup
- génération de map

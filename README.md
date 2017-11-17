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
- récupéré tout les joueurs qui ont été instancié avec NetworkIdentity.clientAuthorityOwner (.connectionId)
- recrée map des joueurs (en gardant LobbyPlayerInfo?) et des points en utilisant cet ID
- comptage des points avec couleur pour indiquer chaque player


NEXT:
- timer pour les rounds
- map plus grande
- refactor du code
- ability mouton
- ability loup
- génération de map

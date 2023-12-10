# altv-server

Dieses Skript dient lediglich als eine Vorlage!

Da ich angeschrieben wurde von mehreren und diese Personen oft nur rein schnuppern wollten, veröffentliche ich dieses Skript nun für alle.

### Meine Fahrzeuge funktionieren nicht?
Es ist wichtig das ein Fahrzeug mit `/tempveh` erstellt wird und diese dann mit einem weiteren Befehl welches im Quellcode zu finden ist in die Datenbank eingetragen wird, mit der Menge an Tank, welches das Fahrzeug haben kann und den Verbrauch, daraufhin kann man mit `/veh` sich das Fahrzeug spawnen und es gehört dann der Person.

### Ich finde mein Fahrzeug nicht mehr wieder?
Alle Fahrzeuge werden von dem Spieler gespeichert, sobald dieser die Verbindung zum Server verliert (Disconect/Timeout), also wird es an der letzten Position zu finden sein.

### Ich kann mich nicht bei Discord anmelden?
Überprüfe [Hier](https://github.com/ThisAccountHasBeenSuspended/altv-server/blob/main/resources/nameless/client/Source/login/main.ts#L16) ob du dein Token richtig angegeben hast.

## Tasten
Es könnte sein das ich die ein oder andere Taste vergessen habe, dies sind aber alle die mir spontan eingefallen sind.
- `T`
- - Öffnet den Chat.
- `F3`
- - Stellt die Reichweite zum Sprechen ein.
- `B`
- - Dies wurde leider nicht fertig aber es erstellt für den Client ein Objekt welches mit `Enter` dann vom Server für alle Spieler erstellt wird. (Idee dahinter war gewesen es für z.B. das LSPD zu benutzen)
- `F9`
- - Geht in den NoClip wenn man Admin auf Level 1 oder höher ist.
- `STRG`
- - Startet ein Fahrzeug.
- `L`
- - Schließt ein Fahrzeug ab welches einem gehört.

## Quellcode
- `resources/server/Source`
- - Der Quellcode vom Server kann einfach als `Debug` kompiliert werden und wird dann automatisch direkt in den richtigen Ordner kompiliert (Kein Copy&Paste der Dateien notwendig).
- `resources/nameless/webview_source`
- - Die Quellcodes von z.B. dem HUD welche einfach mit `npm run build` erstellt werden können und wird dann automatisch direkt in den richtigen Ordner kompiliert (Kein Copy&Paste der Dateien notwendig).
- `resources/nameless/client`
- - Der Quellcode für den Client befindet sich unter `Source` aber in diesem Ordner wird das Skript mithilfe von TypeScript in JavaScript direkt in den richtigen Ordner kompiliert (Kein Copy&Paste der Dateien notwendig).

## Bilder
![Auswahl](https://cdn.discordapp.com/attachments/371271207384907776/1182899090959388672/image.png?ex=65865fa0&is=6573eaa0&hm=38ee610e29628178ac303e14d1caf6e986e1fab4a497005efbe1e8c12f4c6b49&)
![Reichweite](https://cdn.discordapp.com/attachments/371271207384907776/1182899093597585470/image.png?ex=65865fa1&is=6573eaa1&hm=82f1cb85a0c021e591279093336b085dc381743888cdc3cbd7b051689e77dcd4&)
![Fahrzeug Aufschließen](https://cdn.discordapp.com/attachments/371271207384907776/1182899091483656222/image.png?ex=65865fa0&is=6573eaa0&hm=d758698d7d039a77c6215d5695c1b7e8d647f819996f61729cef59a0960e185a&)
![Tacho](https://cdn.discordapp.com/attachments/371271207384907776/1182899092003770398/image.png?ex=65865fa0&is=6573eaa0&hm=8c188b50326d97dd6596da8c937c9a45abf07e113621f4968a301ebb107e7ec7&)
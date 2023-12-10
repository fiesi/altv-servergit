import * as alt from 'alt-client';

import { Character as CharacterEvent } from '../../events.js';

var view: alt.WebView = null;

alt.onServer(CharacterEvent.SelectionCreate, (chars: any) => {
    view = new alt.WebView('http://resource/client/webviews/character/index.html', false);
    view.on(CharacterEvent.Select, (id: number) => alt.emitServer(CharacterEvent.Select, id));
    view.on(CharacterEvent.Create, () => alt.emitServer(CharacterEvent.Create));
    view.focus();
    view.emit('character:route', '/select');
    view.emit(CharacterEvent.SelectionCreate, chars);
    
    alt.showCursor(true);
    alt.toggleGameControls(false);
    alt.toggleVoiceControls(false);
});

alt.onServer(CharacterEvent.SelectionDestroy, () => {
    alt.toggleVoiceControls(true);
    alt.toggleGameControls(true);
    alt.showCursor(false);
    
    if (view !== null) {
        view.unfocus();
        view.destroy();
        view = null;
    }
});
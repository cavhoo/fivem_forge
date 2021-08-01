import React, { useEffect, useState } from 'react';
import { Container } from '@material-ui/core';
import { CharacterCreator, ICharacter, ICharacterCreatorProps } from './views/characterCreator/CharacterCreator';
import { MuiPickersUtilsProvider } from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';

export interface IUiMessageData {
  targetUI: string;
  payload: {
    eventType: string;
    eventData: any;
  }
}



function App () {
  const [uiVisibility, setUiVisibility] = useState({
    characterCreator: false,
  });

  useEffect(() => {
    window.addEventListener("message", ({ data, type }) => {
      const { targetUI, payload } = (data as IUiMessageData);
      const { eventType } = payload;

      if (eventType === "open") {
        switch (targetUI) {
          case "characterCreator":
            setUiVisibility({...uiVisibility, characterCreator: true});
        }
      }
    });
  });

  return (
    <MuiPickersUtilsProvider utils={DateFnsUtils}>
      <Container>
        {
          uiVisibility.characterCreator && (
            <CharacterCreator />
          )
        }
      </Container>

    </MuiPickersUtilsProvider>
  );
}

export default App;

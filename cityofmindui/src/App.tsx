import DateFnsUtils from '@date-io/date-fns';
import { Container } from '@material-ui/core';
import { MuiPickersUtilsProvider } from '@material-ui/pickers';
import React, { useEffect, useState } from 'react';
import {CharacterCreator, Atm, Needs} from './views';
import {runNuiCallback} from "./utils/fetch";

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
    atm: false,
    needs: true,
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
      
      if (eventType === "close") {
        switch (targetUI) {
          case "characterCreator":
            setUiVisibility({...uiVisibility, characterCreator: false});
        }
      }
    });
    
   
  }, []);

  return (
    <MuiPickersUtilsProvider utils={DateFnsUtils}>
      <Container>
        {
          uiVisibility.characterCreator && (
            <CharacterCreator />
          )
        }
        {
          uiVisibility.atm && (
              <Atm />
          )
        }
        {
          uiVisibility.needs && (
              <Needs />
          )
        }
      </Container>

    </MuiPickersUtilsProvider>
  );
}

export default App;

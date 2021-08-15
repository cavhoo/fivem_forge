import { faFemale, faMale } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Grid, Slider, Typography } from "@material-ui/core"
import { useState } from "react"

export interface IParentProps {
  momNames: string[];
  dadNames: string[];
  skinFactor: number;
  faceFactor: number;
  mom: number;
  dad: number;
  onParentDataChanged: (mom: number, dad: number, skinFactor: number, faceFactor: number) => void;
}

export const Parents = ({ momNames, dadNames, skinFactor, faceFactor, mom, dad, onParentDataChanged }: IParentProps) => {
  const [momIndex, setMomIndex] = useState(mom);
  const [dadIndex, setDadIndex] = useState(dad);
  const [skinFactorValue, setSkinFactor] = useState(skinFactor);
  const [faceFactorValue, setFaceFactor] = useState(faceFactor);

  const onParentDataUpdated = () => {
    onParentDataChanged(
      momIndex,
      dadIndex,
      skinFactorValue / 10,
      faceFactorValue / 10
    );
  }


  return (
    <div style={{ width: '100%' }}>
      <div>
        <Typography variant="h5" align="center">Parents</Typography>
      </div>
      <div>
        <Typography>
          Mom - {momNames ? momNames[momIndex] : "--"}
        </Typography>
        <Slider
          key="momSlider"
          defaultValue={momIndex}
          max={momNames ? momNames.length - 1 : 0}
          onChange={(evt, val) => {
            setMomIndex(val as number);
            onParentDataUpdated();
          }}
        />
      </div>
      <div>
        <Typography>
          Dad - {dadNames ? dadNames[dadIndex] : "--"}
        </Typography>
        <Slider
          key="dadSlider"
          defaultValue={dadIndex}
          max={dadNames ? dadNames.length - 1 : 0}
          onChange={(evt, val) => {
            setDadIndex(val as number);
            onParentDataUpdated();
          }}
        />
      </div>
      <div>
        <Typography>
          Face Resemblence
        </Typography>
        <Grid container spacing={2}>
          <Grid item>
            <FontAwesomeIcon icon={faFemale} />
          </Grid>
          <Grid item xs>
            <Slider
              key="faceFactorSlider"
              defaultValue={faceFactor * 10}
              max={10}
              min={0}
              onChange={(evt, val) => {
                setFaceFactor(val as number);
                onParentDataUpdated();
              }}
            />
          </Grid>
          <Grid item>
            <FontAwesomeIcon icon={faMale} />
          </Grid>
        </Grid>
      </div>
      <div>
        <Typography>
          Skin Resemblence
        </Typography>
        <Grid container spacing={2}>
          <Grid item>
            <FontAwesomeIcon icon={faFemale} />
          </Grid>
          <Grid item xs>
            <Slider
              key="skinFactorSlider"
              defaultValue={skinFactor * 10}
              max={10}
              min={0}
              onChange={(evt, val) => {
                setSkinFactor(val as number);
                onParentDataUpdated();
              }}
            />
          </Grid>
          <Grid item>
            <FontAwesomeIcon icon={faMale} />
          </Grid>
        </Grid>
      </div>
      <div>
        <Typography>
          Height
        </Typography>
        <Grid container spacing={2}>
          <Grid item>
              <FontAwesomeIcon icon={faMale} size="xs" />
          </Grid>
          <Grid item xs>
            <Slider
              key="heightSlider"
            />
          </Grid>
          <Grid item>
              <FontAwesomeIcon icon={faMale} size="lg" />
          </Grid>
        </Grid>
      </div>
    </div>
  )
}

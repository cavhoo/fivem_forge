import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Grid,
  Slider,
  Typography,
} from "@material-ui/core";
import { Color, ColorPicker } from "../../components/colors/ColorPicker";

export interface IMakeUpChangedData {
  colorIndex: number;
  variantIndex: number;
  tattooIndex: number;
  blushIndex: number;
  blushColorIndex: number;
}

export interface IMakeUpProps {
  currentMakeUp: { colorIndex: number; variantIndex: number };
  currentTattoo: number;

  currentBlush: { colorIndex: number, variantIndex: number };
  colors: Color[];
  makeUpVariants: string[];
  tattooVariants: number;
  onMakeUpChanged: ({
    colorIndex,
    variantIndex,
    tattooIndex,
  }: IMakeUpChangedData) => void;
}

export const MakeUp = ({
  colors,
  makeUpVariants,
  currentMakeUp,
  currentTattoo,
  currentBlush,
  tattooVariants,
  onMakeUpChanged,
}: IMakeUpProps) => {
  const onSelectMakeUp = (variant: number) => {
    onMakeUpChanged({
      colorIndex: currentMakeUp.colorIndex,
      variantIndex: variant,
      tattooIndex: currentTattoo,
      blushColorIndex: currentBlush.colorIndex,
      blushIndex: currentBlush.variantIndex,
    });
  };

  const onSelectMakeUpColor = (color: Color) => {
    const colorIndex = colors.findIndex((c) => c.value === color.value);

    onMakeUpChanged({
      colorIndex,
      variantIndex: currentMakeUp.variantIndex,
      tattooIndex: currentTattoo,
      blushColorIndex: currentBlush.colorIndex,
      blushIndex: currentBlush.variantIndex,
    });
  };

  const onSelectTattoo = (tattoo: number) => {
    onMakeUpChanged({
      ...currentMakeUp,
      tattooIndex: tattoo,
      blushColorIndex: currentBlush.colorIndex,
      blushIndex: currentBlush.variantIndex,
    });
  };

  const onSelectBlushType = (type: number) => {
    onMakeUpChanged({
      ...currentMakeUp,
      tattooIndex: currentTattoo,
      blushColorIndex: currentBlush.colorIndex,
      blushIndex: type,
    });
  }

  const onSelectBlushColor = (color: number) => {
    onMakeUpChanged({
      ...currentMakeUp,
      tattooIndex: currentTattoo,
      blushColorIndex: color,
      blushIndex: currentBlush.variantIndex,
    })
  }

  return (
    <>
      <Typography variant="h5" align="center">
        Makeup / Tattoos
      </Typography>
      <Grid container spacing={2}>
        <Grid item>MakeUp Variant</Grid>
        <Grid item xs>
          <Slider
            defaultValue={currentMakeUp.variantIndex}
            min={0}
            max={makeUpVariants.length}
            onChange={(ev, value) => onSelectMakeUp(value as number)}
          />
        </Grid>
      </Grid>
      <Grid container spacing={2}>
        <Grid item>
          <Accordion>
            <AccordionSummary>
              <Typography>Makeup Color</Typography>
            </AccordionSummary>
            <AccordionDetails>
              <ColorPicker
                colors={colors}
                selectedColor={colors[currentMakeUp.colorIndex]}
                onColorChanged={(color) => onSelectMakeUpColor(color)}
              />
            </AccordionDetails>
          </Accordion>
        </Grid>
      </Grid>
      <Grid container spacing={2}>
        <Grid item>
          Blush Variant
        </Grid>
        <Grid item xs>
          <Slider
            defaultValue={0}
            min={0}
            max={6}
            onChange={(ev, value) => onSelectBlushType(value as number)}
          />
        </Grid>
      </Grid>
      <Accordion>
        <AccordionSummary>Blush Color</AccordionSummary>
        <AccordionDetails>
          <ColorPicker
            colors={colors} 
            selectedColor={colors[currentBlush.colorIndex]} 
            onColorChanged={(color) => onSelectBlushColor(colors.findIndex((c) => c.value === color.value))} 
          />
        </AccordionDetails>
      </Accordion>
        <Grid container spacing={2}>
          <Grid item>Tattoo Variant</Grid>
          <Grid item xs>
            <Slider
              defaultValue={currentTattoo}
              min={0}
              max={tattooVariants}
              onChange={(ev, value) => onSelectTattoo(value as number)}
            />
          </Grid>
        </Grid>
    </>
      );
};

import {Color, ColorPicker} from "../../../components/colors/ColorPicker";
import {AttributeMenu, IAttributeMenuConfigItem} from "./AttributeMenu";
import {FemaleHairStyles, MaleHairStyles} from "../../../../models";
import {Grid, Slider, Typography, Accordion, AccordionSummary, AccordionDetails} from "@material-ui/core";

const hairAttributes: IAttributeMenuConfigItem[] = [
    {
        id: "hairStyle",
        label: "hair.style",
        min: 0,
        max: 100,
        default: 5,
    }
];

const maleHairStyleArrtribute: IAttributeMenuConfigItem = {
    id: "hairStyle",
    label: "hair.style",
    min: 0,
    max: MaleHairStyles.length,
    default: 0,
}

const femaleHairStyleAttribute: IAttributeMenuConfigItem = {
    id: "hairStyle",
    label: "hair.style",
    min: 0,
    max: FemaleHairStyles.length,
    default: 0,
}


export interface IHairMenuProps {
    selectedGender: string;
    hairColors: Color[];
    onHairStyleChanged: (hairStyle: number, baseColor: number, highlightColor: number) => void;
    selectedHairStyle: number;
    selectedHairBaseColor: number;
    selectedHairHighlightColor: number;
}

export const HairMenu = ({
                             selectedGender,
                             hairColors,
                             onHairStyleChanged,
                             selectedHairStyle,
                             selectedHairBaseColor,
                             selectedHairHighlightColor,
                         }: IHairMenuProps) => {
    const availableStyles = selectedGender === "female" ? femaleHairStyleAttribute : maleHairStyleArrtribute;
    // Out of bounds check in case gender hairstyles have a different amount.
    const currentHairstyles = selectedHairStyle < availableStyles.max ? selectedHairStyle : 0;

    const handleStyleChanged = (style: number) => {
        onHairStyleChanged(style, selectedHairHighlightColor, selectedHairBaseColor);
    }

    const handleBaseColorChange = (color: Color) => {
        onHairStyleChanged(selectedHairStyle, hairColors.indexOf(color), selectedHairHighlightColor);
    }

    const handleHighlightColorChange = (color: Color) => {
        onHairStyleChanged(selectedHairStyle, selectedHairBaseColor, hairColors.indexOf(color));
    }


    return (
        <>
            <Grid container spacing={2}>
                <Grid item>
                    <Typography>
                        Hair Style
                    </Typography>
                </Grid>
                <Grid item xs>
                    <Slider
                        defaultValue={availableStyles.default}
                        max={availableStyles.max}
                        min={availableStyles.min}
                        value={currentHairstyles}
                        onChange={(evt, value) => handleStyleChanged(value as number)}
                    />
                </Grid>
            </Grid>
            <Grid container spacing={2}>
                <Accordion>
                    <AccordionSummary>
                        <Typography>Hair Base Color</Typography>
                    </AccordionSummary>
                    <AccordionDetails>
                        <ColorPicker selectedColor={hairColors[selectedHairBaseColor]} colors={hairColors}
                                     onColorChanged={handleBaseColorChange}/>
                    </AccordionDetails>
                </Accordion>
            </Grid>
            <Grid container spacing={2}>
                <Accordion>
                    <AccordionSummary>
                        <Typography>Hair Highlight Color</Typography>
                    </AccordionSummary>
                    <AccordionDetails>
                        <ColorPicker selectedColor={hairColors[selectedHairHighlightColor]} colors={hairColors}
                                     onColorChanged={handleHighlightColorChange}/>
                    </AccordionDetails>
                </Accordion>
            </Grid>
        </>
    );
}
import {Button, Grid, Typography} from "@material-ui/core"
import {useEffect, useState} from "react"
import {Feet} from "./clothMenus/Feet"
import {Head} from "./clothMenus/Head"
import {Legs} from "./clothMenus/Legs"
import {Torso} from "./clothMenus/Torso"
import {runNuiCallback} from "../../../utils/fetch";
import {ICharacterClothing} from "../../../models/ICharacterClothing";

enum ClothingSubMenus {
    Main,
    Head,
    Torso,
    Legs,
    Feet,
}

export interface IClothingProps {
    hatVariations: number;
    maskVariations: number;
    shoeVariations: number;
    pantsVariations: number;
    glassesVariations: number;
    jewelleryVariations: number;
    shirtVariations: number;
    jacketVariations: number;
    characterClothing: ICharacterClothing;
    onClothingChanged: (clothing: ICharacterClothing) => void;
}

export const Clothes = ({
    hatVariations,
    maskVariations,
    pantsVariations,
    shirtVariations,
    glassesVariations,
    shoeVariations,
    jacketVariations,
    jewelleryVariations,
    characterClothing,
    onClothingChanged,
}: IClothingProps) => {
    const [subMenuOpen, setSubMenuOpen] = useState(ClothingSubMenus.Main)
    const focusCameraOnBodyPart = async (menu: ClothingSubMenus) => {
        let bodypart = "Body";
        switch (menu) {
            case ClothingSubMenus.Feet:
                bodypart = "Feet"
                break;
            case ClothingSubMenus.Head:
                bodypart = "Head";
                break;
            case ClothingSubMenus.Legs:
                bodypart = "Legs";
                break;
            case ClothingSubMenus.Torso:
                bodypart = "Torso";
                break;
            default:
                bodypart = "Body";
                break;
        }
        await runNuiCallback("highlightBodyPart", {bodypart});
    }

    const changeMenu = (menu: ClothingSubMenus) => {
        void focusCameraOnBodyPart(menu);
        setSubMenuOpen(menu);
    }
    
    const handleHeadClothingChanged = (hat: number, mask:number, glasses:number) => {
        onClothingChanged({...characterClothing, hat, mask, glasses});
    }
    
    const handleTorsoClothingChanged = (shirt: number, jacket: number) => {
        onClothingChanged({...characterClothing, shirt, jacket});
    }
    
    const handleLegClothingChanged = (pants: number) => {
        onClothingChanged({...characterClothing, pants});
    }
    
    const handleShoeClothingChanged = (shoes: number) => {
        onClothingChanged({...characterClothing, shoes});
    }

    return (
        <>
            <Typography variant="h5" align="center">Clothes</Typography>
            {
                subMenuOpen === ClothingSubMenus.Main && (
                    <Grid container xs={12} spacing={2}>
                        <Grid item xs={6}>
                            <Button
                                fullWidth
                                color="primary"
                                variant="contained"
                                onClick={() => changeMenu(ClothingSubMenus.Head)}
                            >
                                <Typography>Head</Typography>
                            </Button>
                        </Grid>
                        <Grid item xs={6}>
                            <Button
                                fullWidth
                                color="primary"
                                variant="contained"
                                onClick={() => changeMenu(ClothingSubMenus.Torso)}
                            >
                                <Typography>Torso</Typography>
                            </Button>
                        </Grid>
                        <Grid item xs={6}>
                            <Button
                                fullWidth
                                color="primary"
                                variant="contained"
                                onClick={() => changeMenu(ClothingSubMenus.Legs)}
                            >
                                <Typography>Legs</Typography>
                            </Button>
                        </Grid>
                        <Grid item xs={6}>
                            <Button
                                fullWidth
                                color="primary"
                                variant="contained"
                                onClick={() => changeMenu(ClothingSubMenus.Feet)}
                            >
                                <Typography>Feet</Typography>
                            </Button>
                        </Grid>
                    </Grid>
                )
            }
            {
                subMenuOpen === ClothingSubMenus.Head && (
                    <>
                        <Head
                            hatVariations={hatVariations}
                            maskVariations={maskVariations}
                            glassesVariations={glassesVariations}
                            selectedHat={characterClothing.hat}
                            selectedMask={characterClothing.mask}
                            selectedGlasses={characterClothing.glasses}
                            onHeadClothingChanged={handleHeadClothingChanged}
                        />
                    </>
                )
            }
            {
                subMenuOpen === ClothingSubMenus.Torso && (
                    <>
                        <Torso
                            shirtVariations={shirtVariations}
                            jacketVariations={jacketVariations}
                            selectedJacket={characterClothing.jacket}
                            selectedShirt={characterClothing.shirt}
                            onTorsoClothingChanged={handleTorsoClothingChanged}
                        />
                    </>
                )
            }
            {
                subMenuOpen === ClothingSubMenus.Legs && (
                    <>
                        <Legs
                            pantsVariations={pantsVariations}
                            selectedPants={characterClothing.pants}
                            onPantsClothingChanged={handleLegClothingChanged}
                        />
                    </>
                )
            }
            {
                subMenuOpen === ClothingSubMenus.Feet && (
                    <>
                        <Feet
                            shoeVariations={shoeVariations}
                            selectedShoe={characterClothing.shoes}
                            onShoeClothingChanged={handleShoeClothingChanged}
                        />
                    </>
                )
            }
            {
                subMenuOpen !== ClothingSubMenus.Main && (
                    <Grid container>
                        <Grid item>
                            <Button onClick={() => changeMenu(ClothingSubMenus.Main)}>
                                <Typography>Back</Typography>
                            </Button>
                        </Grid>
                    </Grid>
                )
            }
        </>
    )
}
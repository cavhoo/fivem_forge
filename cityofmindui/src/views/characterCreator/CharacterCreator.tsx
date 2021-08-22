import {Button, Container, Grid, makeStyles, MobileStepper, responsiveFontSizes, Typography} from "@material-ui/core"
import {useEffect, useState} from "react"
import {runNuiCallback} from "../../utils/fetch";
import {
    Parents,
    Face,
    Clothes,
    IPersonalDataChanged,
    MakeUp,
    IMakeUpChangedData, PersonalData,
} from "./menus";
import {ICharacter, ICharacterCreatorInitialData, IFaceProperties} from "../../models";
import {ICharacterClothing} from "../../models/ICharacterClothing";

export enum MenuItems {
    PersonalInfo,
    Parents,
    Face,
    MakeupTattoos,
    Clothes,
}


const useStyles = makeStyles({
    root: {
        maxWidth: "20vw",
        backgroundColor: "#FAFAFA",
        margin: "unset",
        position: "absolute",
        left: "5vw",
        top: "5vh",
        minHeight: "50vh",
        borderRadius: "15px"
    },
    createButton: {
        top: "15vh",
    }
});

export const CharacterCreator = () => {
    const classes = useStyles();
    const [activeStep, setActiveStep] = useState(0);
    const [initialData, setInitialData] = useState({} as ICharacterCreatorInitialData);
    const [character, SetCharacter] = useState({
        firstname: "",
        lastname: "",
        birthdate: "01.01.1970",
        mom: 0,
        dad: 0,
        gender: "female",
        skinFactor: 0.5,
        faceFactor: 0.5,
        chin: {
            width: 0.5,
            gapSize: 0.5,
            forward: 0.5,
            height: 0.5,
        },
        cheeks: {
            cheekWidth: 0.5,
            cheekBoneHeight: 0.5,
            cheekBoneWidth: 0.5,
        },
        nose: {
            width: 0.5,
            tipLowering: 0.5,
            tipHeight: 0.5,
            tipLength: 0.5,
            boneBend: 0.5,
            boneOffset: 0.5,
        },
        eyes: {
            color: 1,
            opening: 0.5,
            browBulkiness: 0.5,
            browHeight: 0.5,
            eyeBrowStyle: 0,
            eyeBrowColor: 0,
        },
        lipThickness: 0.5,
        makeUpColor: 0,
        makeUpVariant: 0,
        tattoo: 0,
        hairStyle: 2,
        hairBaseColor: 0,
        hairHighlightColor: 0,
        nationality: "german",
        lipStickColor: 0,
        beardColor: 0,
        beardStyle: 255,
        lipStickVariant: 0,
        chestHairColor: 0,
        chestHairVariant: 0,
        blushColor: 0,
        blushVariant: 0,
        clothes: {
            hat: -1,
            mask: -1,
            shoes: -1,
            pants: -1,
            shirt: -1,
            jacket: -1,
            glasses: -1,
            jewellery: -1
        }
    } as ICharacter);

    useEffect(() => {
        const loadData = async () => await runNuiCallback("getInitialData", {ui: "CharacterCreator"});
        loadData().then(async (resp: Response) => resp.json()).then((responseData) => {
            const data = JSON.parse(responseData);
            setInitialData({
                ...data.payload.eventData,
            });
        });
        const clothingData = async () => await runNuiCallback("getClothingForCurrentCharacter", {gender: character.gender});
        clothingData().then(async (resp: Response) => resp.json().then(async (responseData) => {
            setInitialData({
                ...initialData,
                clothes: {
                    jackets: responseData.shirts,
                    shoes: responseData.shoes,
                    masks: responseData.masks,
                    hats: responseData.hats,
                    pants: responseData.pants,
                    glasses: responseData.glasses,
                    shirts: responseData.underShirts,
                    jewellery: responseData.jewellery
                }
            });
            await runNuiCallback("updateCharacter", character);
        }));
    }, [])

    useEffect(() => {
        void runNuiCallback("updateCharacter", character);
    }, [character]);

    const onParentDataChanged = async (mom: number, dad: number, skinFactor: number, faceFactor: number) => {
        SetCharacter({
            ...character,
            mom,
            dad,
            skinFactor,
            faceFactor,
        });
    }

    const onFaceDataChanged = async ({chin, cheeks, eyes, nose, lipThickness, hairStyle}: IFaceProperties) => {
        SetCharacter({
            ...character,
            chin,
            nose,
            cheeks,
            eyes,
            lipThickness,
            hairStyle: hairStyle.style,
            hairBaseColor: hairStyle.baseColor,
            hairHighlightColor: hairStyle.highlightColor
        })
    }

    const onMakeUpChanged = async ({
                                       colorIndex,
                                       variantIndex,
                                       tattooIndex,
                                       blushColorIndex,
                                       blushIndex
                                   }: IMakeUpChangedData) => {
        SetCharacter({
            ...character,
            makeUpColor: colorIndex,
            makeUpVariant: variantIndex,
            tattoo: tattooIndex,
            blushColor: blushColorIndex,
            blushVariant: blushIndex,
        })
    }

    const onPersonalDataChanged = async ({
                                             firstname,
                                             lastname,
                                             birthdate,
                                             gender,
                                             nationality
                                         }: IPersonalDataChanged) => {
        SetCharacter({
            ...character,
            firstname,
            lastname,
            birthdate,
            nationality,
            gender
        })
    }

    const onClothingChanged = async (clothes: ICharacterClothing) => {
        SetCharacter({
            ...character,
            clothes
        })
    }

    const onCreateCharacter = async () => {
        await runNuiCallback("createCharacter", character);
    }

    const renderActiveMenuItem = () => {
        if (initialData === undefined) {
            return (<Typography>No Data Available</Typography>)
        }

        switch (activeStep) {
            case MenuItems.Parents:
                return (<Parents
                    momNames={initialData.moms}
                    dadNames={initialData.dads}
                    onParentDataChanged={onParentDataChanged}
                    mom={character.mom}
                    dad={character.dad}
                    faceFactor={character.faceFactor}
                    skinFactor={character.skinFactor}
                />);
            case MenuItems.Face:
                return (<Face
                    selectedGender={character.gender}
                    browColors={initialData.makeUpColors.map((color) => ({value: `rgb(${color.Red}, ${color.Green}, ${color.Blue})`}))}
                    hairColors={initialData.hairColors.map((color) => ({value: `rgb(${color.Red}, ${color.Green}, ${color.Blue})`}))}
                    eyes={character.eyes}
                    chin={character.chin}
                    cheeks={character.cheeks}
                    lipThickness={character.lipThickness}
                    nose={character.nose}
                    hairStyle={{
                        style: character.hairStyle,
                        baseColor: character.hairBaseColor,
                        highlightColor: character.hairHighlightColor
                    }}
                    onFaceUpdated={onFaceDataChanged}
                />);
            case MenuItems.MakeupTattoos:
                return (<MakeUp
                    currentTattoo={character.tattoo}
                    currentMakeUp={{variantIndex: character.makeUpVariant, colorIndex: character.makeUpColor}}
                    currentBlush={{variantIndex: character.blushVariant, colorIndex: character.blushColor}}
                    colors={initialData.makeUpColors.map((color) => ({value: `rgb(${color.Red}, ${color.Green}, ${color.Blue})`}))}
                    makeUpVariants={initialData.makeUpVariants}
                    tattooVariants={initialData.tattooVariants}
                    onMakeUpChanged={onMakeUpChanged}
                />);
            case MenuItems.Clothes:
                return (<Clothes
                    shirtVariations={initialData.clothes.shirts}
                    jacketVariations={initialData.clothes.jackets}
                    shoeVariations={initialData.clothes.shoes}
                    hatVariations={initialData.clothes.hats}
                    glassesVariations={initialData.clothes.glasses}
                    maskVariations={initialData.clothes.masks}
                    jewelleryVariations={initialData.clothes.jewellery}
                    pantsVariations={initialData.clothes.pants}
                    characterClothing={character.clothes}
                    onClothingChanged={onClothingChanged}
                />);
            case MenuItems.PersonalInfo:
                return (<PersonalData
                    firstname={character.firstname}
                    lastname={character.lastname}
                    nationality={character.nationality}
                    birthdate={character.birthdate}
                    gender={character.gender}
                    onUpdatePersonalData={onPersonalDataChanged}
                />);
        }
    }

    return (
        <Container
            className={classes.root}
        >
            <MobileStepper
                variant="dots"
                steps={5}
                position="static"
                activeStep={activeStep}
                nextButton={
                    <Button
                        onClick={() => setActiveStep(activeStep + 1)}
                        disabled={activeStep === 4}
                    >
                        Next
                    </Button>
                }
                backButton={
                    <Button
                        onClick={() => setActiveStep(activeStep - 1)}
                        disabled={activeStep === 0}
                    >
                        Prev
                    </Button>
                }
            />
            {
                renderActiveMenuItem()
            }
            <Grid item xs={12}>
                <Button
                    className={classes.createButton}
                    fullWidth
                    color="primary"
                    variant="contained"
                    onClick={onCreateCharacter}
                >
                    Create
                </Button>
            </Grid>
        </Container>
    )
}

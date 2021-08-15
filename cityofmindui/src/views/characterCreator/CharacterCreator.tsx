import { Button, Container, Grid, makeStyles, MobileStepper, responsiveFontSizes } from "@material-ui/core"
import { useEffect, useState } from "react"
import { useGetInitialDataQuery } from "../../store/services/characterApi";
import { runNuiCallback } from "../../utils/fetch";
import { Parents, Face, IChinData, IEyeData, ICheekData, INoseData, IFaceProperties } from "./menus";
import { Clothes } from "./menus/Clothes";
import { IMakeUpChangedData, MakeUp } from "./menus/MakeUp";
import { IPersonalDataChanged, PersonalData } from "./menus/PersonalData";


export interface ICharacter {
	firstname: string;
	lastname: string;
	birthdate: string;
	nationality: string;
	gender: string;
	mom: number;
	dad: number;
	skinFactor: number;
	faceFactor: number;
	chin: IChinData;
	eyes: IEyeData;
	cheeks: ICheekData;
	nose: INoseData;
	lipThickness: number;
	lipStickColor: number;
	lipStickVariant: number;
	tattoo: number;
	makeUpVariant: number;
	makeUpColor: number;
	hairStyle: number;
	hairBaseColor: number;
	hairHighlightColor: number;
	beardStyle: number;
	beardColor: number;
	chestHairVariant: number;
	chestHairColor: number;
	blushColor: number;
	blushVariant: number;
}

export enum MenuItems {
	PersonalInfo,
	Parents,
	Face,
	MakeupTattoos,
	Clothes,
}

export interface ICharacterCreatorProps {
	moms: string[];
	dads: string[];
	gender: string;
	makeUpColors: { Index: number, Red: number, Green: number, Blue: number }[];
	hairColors: { Index: number, Red: number, Green: number, Blue: number}[];
	makeUpVariants: string[];
	tattooColors: string[];
	tattooVariants: number;
	beardStyles: string[];
	femaleHairStyles: string[];
	maleHairStyles: string[];
	clothes: {
		torso: string[],
		hats: string[],
		shoes: string[],
		pants: string[],
		glasses: string[],
		masks: string[],
		bags: string[],
	}
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
	const { data, error, isLoading } = useGetInitialDataQuery(null);
	const [activeStep, setActiveStep] = useState(0);
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
		hairStyle: 0,
		hairBaseColor: 0,
		hairHighlightColor: 0,
		nationality: "German",
		lipStickColor: 0,
		beardColor: 0,
		beardStyle: 0,
		lipStickVariant: 0,
		chestHairColor: 0,
		chestHairVariant: 0,
		blushColor: 0,
		blushVariant: 0,
	} as ICharacter);

	useEffect(() => {
		const loadData = async () => await runNuiCallback("getInitialData", { ui: "CharacterCreator" });
		loadData().then(async (resp: Response) => resp.json()).then((responseData) => {
			const data = JSON.parse(responseData);
			setData({
				...data.payload.eventData,
			});
		});
	}, []);

	useEffect(() => {
		runNuiCallback("updateCharacter", character);
	}, [character])

	const onParendDataChanged = async (mom: number, dad: number, skinFactor: number, faceFactor: number) => {
		SetCharacter({
			...character,
			mom,
			dad,
			skinFactor,
			faceFactor,
		});
	}

	const onFaceDataChanged = async ({ chin, cheeks, eyes, nose, lipThickness, hairStyle }: IFaceProperties) => {
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

	const onMakeUpChanged = async ({ colorIndex, variantIndex, tattooIndex, blushColorIndex, blushIndex }: IMakeUpChangedData) => {
		SetCharacter({
			...character,
			makeUpColor: colorIndex,
			makeUpVariant: variantIndex,
			tattoo: tattooIndex,
			blushColor: blushColorIndex,
			blushVariant: blushIndex,
		})
	}

	const onPersonalDataChanged = async ({ firstname, lastname, birthdate, gender, nationality }: IPersonalDataChanged) => {
		SetCharacter({
			...character,
			firstname,
			lastname,
			birthdate,
			nationality,
			gender
		})
	}

	const onCreateCharacter = async () => {
		await runNuiCallback("createCharacter", character);
	}

	const renderActiveMenuItem = () => {
		switch (activeStep) {
			case MenuItems.Parents:
				return (<Parents
					momNames={data.moms}
					dadNames={data.dads}
					onParentDataChanged={onParendDataChanged}
					mom={character.mom}
					dad={character.dad}
					faceFactor={character.faceFactor}
					skinFactor={character.skinFactor}
				/>);
			case MenuItems.Face:
				return (<Face
					browColors={data.makeUpColors.map((color) => ({ value: `rgb(${color.Red}, ${color.Green}, ${color.Blue})` }))}
					hairColors={data.hairColors.map((color) => ({ value: `rgb(${color.Red}, ${color.Green}, ${color.Blue})` }))}
					eyes={character.eyes}
					chin={character.chin}
					cheeks={character.cheeks}
					lipThickness={character.lipThickness}
					nose={character.nose}
					hairStyle={{ style: character.hairStyle, baseColor: character.hairBaseColor, highlightColor: character.hairHighlightColor }}
					onFaceUpdated={onFaceDataChanged}
				/>);
			case MenuItems.MakeupTattoos:
				console.log(data.makeUpColors[0]);
				return (<MakeUp
					currentTattoo={character.tattoo}
					currentMakeUp={{ variantIndex: character.makeUpVariant, colorIndex: character.makeUpColor }}
					currentBlush={{ variantIndex: character.blushVariant, colorIndex: character.blushColor }}
					colors={data.makeUpColors.map((color) => ({ value: `rgb(${color.Red}, ${color.Green}, ${color.Blue})` }))}
					makeUpVariants={data.makeUpVariants}
					tattooVariants={data.tattooVariants}
					onMakeUpChanged={onMakeUpChanged}
				/>);
			case MenuItems.Clothes:
				return (<Clothes />);
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

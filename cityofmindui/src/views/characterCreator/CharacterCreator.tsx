import { Button, Container, MobileStepper } from "@material-ui/core"
import { useEffect, useState } from "react"
import { runNuiCallback } from "../../utils/fetch";
import { Parents, Face, IChinData, IEyeData, ICheekData, INoseData } from "./menus";
import { Clothes } from "./menus/Clothes";
import { MakeUp } from "./menus/MakeUp";
import { PersonalData } from "./menus/PersonalData";


export interface ICharacter {
	mom: number;
	dad: number;
	skinFactor: number;
	faceFactor: number;

	chin: IChinData;
	eyes: IEyeData;
	cheeks: ICheekData;
	nose: INoseData;
	lipThickness: number;
}

export enum MenuItems {
	Parents,
	Face,
	MakeupTattoos,
	Clothes,
	PersonalInfo,
}

export interface ICharacterCreatorProps {
	moms: string[];
	dads: string[];
	makeUpColors: string[];
	makeUpVariants: string[];
	tattooColors: string[];
	tattooVariants: string[];
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


export const CharacterCreator = () => {
	const [activeStep, setActiveStep] = useState(0);
	const [data, setData] = useState({} as ICharacterCreatorProps);
	const [character, SetCharacter] = useState({
		mom: 0,
		dad: 0,
		skinFactor: 0.5,
		faceFactor: 0.5,
		chin: {},
		cheeks: {},
		nose: {},
		eyes: {},
		lipThickness: 0.5,
	} as ICharacter);

	useEffect(() => {
		var loadData = async () => await runNuiCallback("getInitialData", { ui: "CharacterCreator" });
		loadData().then((resp: Response) => resp.json()).then((responseData) => {
			setData({
				...data,
				moms: responseData.moms
			});
			console.log(data.moms);
	});
	}, []);


const onParendDataChanged = async (mom: number, dad: number, skinFactor: number, faceFactor: number) => {
	console.log("Update character state");
	SetCharacter({
		...character,
		mom,
		dad,
		skinFactor,
		faceFactor
	});
	await runNuiCallback("updateCharacter", character);
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
			return (<Face eyes={character.eyes} chin={character.chin} cheeks={character.cheeks} lipThickness={character.lipThickness} nose={character.nose} />);
		case MenuItems.MakeupTattoos:
			return (<MakeUp colors={data.makeUpColors} variants={data.makeUpVariants} />);
		case MenuItems.Clothes:
			return (<Clothes />);
		case MenuItems.PersonalInfo:
			return (<PersonalData />);
	}
}

return (
	<Container
		style={{ backgroundColor: "#A3A3A3", maxWidth: "300px" }}
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
	</Container>
)
}
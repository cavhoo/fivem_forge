import { Button, Grid, Slider, Typography } from "@material-ui/core"
import {ChangeEvent, useEffect} from "react"
import { runNuiCallback } from "../../../../utils/fetch"

export interface ITorsoProps {
	shirtVariations: number;
	jacketVariations: number;
	selectedShirt: number;
	selectedJacket: number;
	onTorsoClothingChanged: (shirt: number, jacket: number) => void;
}

export const Torso = ({shirtVariations, jacketVariations, onTorsoClothingChanged, selectedShirt, selectedJacket}: ITorsoProps) => {

	useEffect(() => {
		const focusTorso = async () => await runNuiCallback("highlightBodyPart", { bodypart: "Torso"});
		void focusTorso();
	})
	
	const handleShirtChange = (event:ChangeEvent<{}>, value: number | number[]) => onTorsoClothingChanged(value as number, selectedJacket);
	const handleJacketChange = (event:ChangeEvent<{}>, value: number | number[]) => onTorsoClothingChanged(selectedShirt, value as number);

	return (
		<>
			<Grid container spacing={5}>
				<Grid item>
					Shirts
				</Grid>
				<Grid item xs>
					<Slider 
						min={0} 
						max={shirtVariations}
						value={selectedShirt}
						onChange={handleShirtChange}
					/>
				</Grid>
			</Grid>
			<Grid container spacing={5}>
				<Grid item>
					Jackets
				</Grid>
				<Grid item xs>
					<Slider 
						min={0} 
						max={jacketVariations}
						value={selectedJacket}
						onChange={handleJacketChange}
					/>
				</Grid>
			</Grid>
		</>
	)
}
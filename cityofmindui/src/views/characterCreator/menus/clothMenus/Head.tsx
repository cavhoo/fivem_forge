import { Button, Grid, Slider, Typography } from "@material-ui/core"
import {ChangeEvent, useEffect} from "react"
import { runNuiCallback } from "../../../../utils/fetch";

export interface IHeadProps {
	hatVariations: number;
	maskVariations: number;
	glassesVariations: number;
	selectedHat: number;
	selectedMask: number;
	selectedGlasses: number;
	onHeadClothingChanged: (hat: number, mask: number, glasses: number) => void;
}

export const Head = ({hatVariations, maskVariations, glassesVariations, selectedHat, selectedMask,selectedGlasses, onHeadClothingChanged}: IHeadProps) => {
	
	const handleHatChange = (event: ChangeEvent<{}>, value: number|number[]) => onHeadClothingChanged(value as number, selectedMask, selectedGlasses );
	const handleMaskChange = (event: ChangeEvent<{}>, value: number|number[]) => onHeadClothingChanged(selectedHat, value as number, selectedGlasses);
	const handleGlassChange = (event: ChangeEvent<{}>, value: number|number[]) => onHeadClothingChanged(selectedHat, selectedMask, value as number);
	
	useEffect(() => {
		const focusHead = async () => await runNuiCallback("highlightBodyPart", { bodypart: "Head"})
		void focusHead();
	});

	return (
		<>
			<Grid container spacing={5}>
				<Grid item>
					Hats
				</Grid>
				<Grid item xs>
					<Slider 
						min={0} 
						max={hatVariations}
						value={selectedHat}
						onChange={handleHatChange}
					/>
				</Grid>
			</Grid>
			<Grid container spacing={5}>
				<Grid item>
					Mask
				</Grid>
				<Grid item xs>
					<Slider
						min={0}
						max={maskVariations}
						value={selectedMask}
						onChange={handleMaskChange}
					/>
				</Grid>
			</Grid>
			<Grid container spacing={5}>
				<Grid item>
					Glasses
				</Grid>
				<Grid item xs>
					<Slider
						min={0}
						max={glassesVariations}
						value={selectedGlasses}
						onChange={handleGlassChange}
					/>
				</Grid>
			</Grid>
		</>
	)
}
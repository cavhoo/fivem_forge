import { Button, Grid, Slider, Typography } from "@material-ui/core"
import {ChangeEvent, useEffect} from "react"
import { runNuiCallback } from "../../../../utils/fetch"

export interface ILegsProps {
	pantsVariations: number;
	selectedPants: number;
	onPantsClothingChanged: (pants: number) => void;
}

export const Legs = ({pantsVariations, onPantsClothingChanged, selectedPants}: ILegsProps) => {
	useEffect(() => {
		const focusLegs = async () => await runNuiCallback("highlightBodyPart", { bodypart: "Legs"});
		focusLegs();
	})
	
	const handlePantsChanged = (event: ChangeEvent<{}>, value: number | number[]) => onPantsClothingChanged(value as number)

	return (
		<>
			<Grid container spacing={5}>
				<Grid item>
					Pants
				</Grid>
				<Grid item xs>
					<Slider min={0} max={pantsVariations} value={selectedPants} onChange={handlePantsChanged}/>
				</Grid>
			</Grid>
		</>
	)
}
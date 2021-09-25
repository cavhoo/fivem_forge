import { Button, Grid, Slider, Typography } from "@material-ui/core"
import {ChangeEvent, useEffect} from "react"
import { runNuiCallback } from "../../../../utils/fetch"

export interface IFeetProps {
	shoeVariations: number;
	selectedShoe: number;
	onShoeClothingChanged: (shoe: number) => void;
}

export const Feet = ({shoeVariations, selectedShoe, onShoeClothingChanged}: IFeetProps) => {
	useEffect(() => {
		const focusFeet = async () =>	await runNuiCallback("highlightBodyPart", { bodypart: "Feet"});
		focusFeet();
	})
	
	const handleShoeChanged = (event: ChangeEvent<{}>, value:number | number[]) => onShoeClothingChanged(value as number);

	return (
		<>
			<Grid container spacing={5}>
				<Grid item>
					Shoes
				</Grid>
				<Grid item xs>
					<Slider min={0} max={shoeVariations} value={selectedShoe} onChange={handleShoeChanged} />
				</Grid>
			</Grid>
		</>
	)
}
import { Button, Grid, Slider, Typography } from "@material-ui/core"
import { useEffect } from "react"
import { runNuiCallback } from "../../../../utils/fetch"

export const Feet = () => {
	useEffect(() => {
		var focusFeet = async () => {
			await runNuiCallback("highlightBodyPart", { bodypart: "Feet"});
		};

		focusFeet();
	})


	return (
		<>
			<Grid container spacing={5}>
				<Grid item>
					Shoes
				</Grid>
				<Grid item xs>
					<Slider min={0} max={10} />
				</Grid>
			</Grid>
		</>
	)
}
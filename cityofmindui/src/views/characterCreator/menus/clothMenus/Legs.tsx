import { Button, Grid, Slider, Typography } from "@material-ui/core"
import { useEffect } from "react"
import { runNuiCallback } from "../../../../utils/fetch"

export const Legs = () => {
	useEffect(() => {
		const focusLegs = async () => await runNuiCallback("highlightBodyPart", { bodypart: "Legs"});
		focusLegs();
	})

	return (
		<>
			<Grid container spacing={5}>
				<Grid item>
					Pants
				</Grid>
				<Grid item xs>
					<Slider min={0} max={10} />
				</Grid>
			</Grid>
			<Grid container spacing={5}>
				<Grid item>
					Shorts
				</Grid>
				<Grid item xs>
					<Slider min={0} max={10} />
				</Grid>
			</Grid>
		</>
	)
}
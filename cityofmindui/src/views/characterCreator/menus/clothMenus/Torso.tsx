import { Button, Grid, Slider, Typography } from "@material-ui/core"
import { useEffect } from "react"
import { runNuiCallback } from "../../../../utils/fetch"

export const Torso = () => {

	useEffect(() => {
		const focusTorso = async () => await runNuiCallback("highlightBodyPart", { bodypart: "Torso"});
		focusTorso();
	})

	return (
		<>
			<Grid container spacing={5}>
				<Grid item>
					Shirts
				</Grid>
				<Grid item xs>
					<Slider min={0} max={10} />
				</Grid>
			</Grid>
			<Grid container spacing={5}>
				<Grid item>
					Jackets
				</Grid>
				<Grid item xs>
					<Slider min={0} max={10} />
				</Grid>
			</Grid>
		</>
	)
}
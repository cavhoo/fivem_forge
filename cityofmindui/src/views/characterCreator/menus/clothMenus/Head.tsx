import { Button, Grid, Slider, Typography } from "@material-ui/core"
import { useEffect } from "react"
import { runNuiCallback } from "../../../../utils/fetch";

export const Head = () => {

	useEffect(() => {
		const focusHead = async () => await runNuiCallback("highlightBodyPart", { bodypart: "Head"})
		focusHead();
	});

	return (
		<>
			<Grid container spacing={5}>
				<Grid item>
					Hats
				</Grid>
				<Grid item xs>
					<Slider min={0} max={10} />
				</Grid>
			</Grid>
			<Grid container spacing={5}>
				<Grid item>
					Glasses
				</Grid>
				<Grid item xs>
					<Slider min={0} max={10} />
				</Grid>
			</Grid>
		</>
	)
}
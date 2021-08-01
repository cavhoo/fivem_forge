import { Grid, Slider, Typography } from "@material-ui/core"
import { Color, ColorPicker, IColorItemProps } from "../../components/colors/ColorPicker"

const colorValues: Color[] = [
	{ value: '#000'},
	{ value: '#F00'},
	{ value: '#0F0' },
	{ value: '#00F' },
	{ value: '#003230'}
]

export interface IMakeUp {
	colors: string[];
	variants: string[];
}

export const MakeUp = ({colors, variants}: IMakeUp) => {
	return (
		<>
			<Typography variant="h5" align="center">Makeup  / Tattoos</Typography>
			<Grid container spacing={2}> 
				<Grid item>
					MakeUp Variant
				</Grid>
				<Grid item xs>
					<Slider 
						defaultValue={0}
						min={0}
						max={10}
					/>
				</Grid>
			</Grid>
			<Grid container spacing={2}>
				<Grid item>
					Makeup Color
				</Grid>
				<Grid item>
					<ColorPicker colors={colorValues} selectedColor={{value: "#000"}} onColorChanged={() => console.log("Color picked!")} />
				</Grid>
			</Grid>
			<Grid container spacing={2}>
				<Grid item>
					Tattoo Variant
				</Grid>
				<Grid item xs>
					<Slider defaultValue={0} min={0} max={10} />
				</Grid>
			</Grid>
			<Grid container spacing={2}>
				<Grid item>
					Tattoo Color
				</Grid>
				<Grid item xs>
					<ColorPicker colors={[]} selectedColor={null} onColorChanged={() => console.log("Tatt color changed")} />
				</Grid>
			</Grid>
		</>
	)
}
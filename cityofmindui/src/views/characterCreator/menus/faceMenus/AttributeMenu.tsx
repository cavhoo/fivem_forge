import { Grid, Slider, Typography } from "@material-ui/core"

export interface IAttributeMenuConfigItem {
	label: string;
	max: number;
	min: number;
	default: number;
	id: string;
}
export interface IAttributeMenuProps {
	attributes: IAttributeMenuConfigItem[];
	onAttributeChanged: (attributeChangedValue:{id: string, value: number}) => void;
}

export const AttributeMenu = ({attributes, onAttributeChanged}: IAttributeMenuProps) => {
	return (
		<>
			{
				attributes.map((config) => (
					<Grid container spacing={2} key={config.id}>
						<Grid item>
							<Typography>
								{config.label}
							</Typography>
						</Grid>
						<Grid item xs>
							<Slider
								defaultValue={config.default}
								max={config.max}
								min={config.min}
								onChange={(evt, value) => onAttributeChanged({ id: config.id, value: (value as number / config.max)})}
							/>
						</Grid>
					</Grid>
				))
			}
		</>
	)
}
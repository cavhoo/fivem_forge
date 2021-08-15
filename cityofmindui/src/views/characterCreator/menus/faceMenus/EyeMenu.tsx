import { Accordion, AccordionDetails, AccordionSummary, Typography } from "@material-ui/core";
import { Color, ColorPicker } from "../../../components/colors/ColorPicker";
import { AttributeMenu, IAttributeMenuConfigItem } from "./AttributeMenu";

const EyeMenuItems: IAttributeMenuConfigItem[] = [
	{
		id: 'opening',
		default: 5,
		max: 10,
		min: 0,
		label: 'eye.opening'
	},
	{
		id: 'browHeight',
		default: 5,
		max: 10,
		min: 0,
		label: 'eye.browheight',
	},
	{
		id: 'browBulkiness',
		default: 5,
		max: 10,
		min: 0,
		label: 'eye.browthickness'
	},
	{
		id: 'color',
		default: 5,
		max: 10,
		min: 0,
		label: 'eye.color'
	},
	{
		id: 'eyeBrowStyle',
		default: 0,
		max: 33,
		min: 0,
		label: 'eye.browStyle'
	}
];

export interface IEyeMenuProps {
	browColor: Color;
	browColors: Color[];
	onEyeChanged: (eyeChanged: { id: string, value: number }) => void;
}

export const EyeMenu = ({ onEyeChanged, browColor, browColors }: IEyeMenuProps) => (
	<>
		<AttributeMenu attributes={EyeMenuItems} onAttributeChanged={onEyeChanged} />
		<Accordion>
			<AccordionSummary>
				<Typography>Eyebrow Color</Typography>
			</AccordionSummary>
			<AccordionDetails>
				<ColorPicker selectedColor={browColor} colors={browColors} onColorChanged={(color) => onEyeChanged({ id: 'eyeBrowColor', value: browColors.findIndex((c) => c.value === color.value) })} />
			</AccordionDetails>
		</Accordion>
	</>
)
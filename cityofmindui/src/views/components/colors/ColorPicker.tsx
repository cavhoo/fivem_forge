import { Radio, makeStyles, RadioProps, IconButton, Button, ButtonProps, RadioGroup } from "@material-ui/core"
import CheckIcon from '@material-ui/icons/Check'
import { useState } from "react";
export interface Color {
	value: string;
}


export interface IColorItemProps {
	colorValue: string;
}

const useColorItemStyles = (color: string) => makeStyles({
	root: {
		padding: 1,
		'&:hover': {
			backgroundColor: 'transparent',
		},
	},
	checkedIcon: {
		border: '1px solid pink',
	}
});

interface IColorSelectButtonProps {
	colorValue: string;
	checked?: boolean;
}

const useColorSelectButtonStyles = (color: string, checked: boolean) => (makeStyles({
	root: {
		padding: 0,
		margin: 0,
		borderRadius: 0,
		backgroundColor: color,
		width: 48,
		height: 48,
		minWidth: 48,
		border: checked ? '1px solid white' : 'none',
	}
})());

const ColorSelectButton = ({ colorValue, checked = false, ...rest }: IColorSelectButtonProps & ButtonProps) => {
	const classes = useColorSelectButtonStyles(colorValue, checked);

	return (
		<Button
			className={classes.root}
			{...rest}
		>
			{
				checked && <CheckIcon style={{ color: 'white' }} />
			}
		</Button>
	)
}



const ColorItem = ({ colorValue, ...rest }: IColorItemProps & RadioProps) => {
	const classes = useColorItemStyles(colorValue)();
	return (
		<Radio
			className={classes.root}
			color="default"
			checkedIcon={<ColorSelectButton colorValue={colorValue} variant='contained' color='primary' checked />}
			icon={<ColorSelectButton colorValue={colorValue} variant='contained' color='primary' />}
			{...rest}
		/>
	)
};
export interface ColorPickerProps {
	selectedColor: Color | null;
	colors: Color[];
	onColorChanged: (color: Color) => void;
}

export const ColorPicker = ({ selectedColor, colors, onColorChanged }: ColorPickerProps) => {
	const [currentColor, setCurrentColor] = useState(selectedColor);

	const handleColorPick = (color: Color) => {
		const newColor = colors.find(c => c.value === color.value);
		setCurrentColor(newColor ?? colors[0]);
		onColorChanged(color);
	}


	return (
		<>
			<div>
				{
					colors.map((color, index) => (
						<ColorItem
							key={index}
							colorValue={color.value}
							checked={ currentColor ? (color.value === currentColor?.value) : false}
							onChange={() => handleColorPick(color)} />
					))
				}
			</div>
		</>
	)
}
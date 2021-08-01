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
		id: 'browheight',
		default: 5,
		max: 10,
		min: 0,
		label: 'eye.browheight',
	},
	{
		id: 'browthickness',
		default: 5,
		max: 10,
		min: 0,
		label: 'eye.browthickness'
	},
	{
		id: 'eyecolor',
		default: 5,
		max: 10,
		min: 0,
		label: 'eye.color'
	}
];

export interface IEyeMenuProps {
	onEyeChanged: (eyeChanged: {id: string, value: number}) => void;
}

export const EyeMenu = ({onEyeChanged}: IEyeMenuProps) => <AttributeMenu attributes={EyeMenuItems} onAttributeChanged={onEyeChanged} />
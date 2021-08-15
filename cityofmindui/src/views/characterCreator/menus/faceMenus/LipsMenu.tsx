import { AttributeMenu, IAttributeMenuConfigItem } from "./AttributeMenu";

const LipMenuItems: IAttributeMenuConfigItem[] = [
	{
		id: 'lipThickness',
		default: 5,
		max: 10,
		min: 0,
		label: "lips.thickness"
	}
]

export interface ILipsMenuProps {
	onLipsChanged: (value: number) => void;
}
export const LipsMenu = ({onLipsChanged}: ILipsMenuProps) => <AttributeMenu attributes={LipMenuItems} onAttributeChanged={({id, value}) => onLipsChanged(value)} />
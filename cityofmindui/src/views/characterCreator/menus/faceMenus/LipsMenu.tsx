import { AttributeMenu, IAttributeMenuConfigItem } from "./AttributeMenu";

const LipMenuItems: IAttributeMenuConfigItem[] = [
	{
		id: 'lipthickness',
		default: 5,
		max: 10,
		min: 0,
		label: "lips.thickness"
	}
]

export interface ILipsMenuProps {
	onLipsChanged: (lipsChanged: {id: string, value: number}) => void;
}
export const LipsMenu = ({onLipsChanged}: ILipsMenuProps) => <AttributeMenu attributes={LipMenuItems} onAttributeChanged={onLipsChanged} />
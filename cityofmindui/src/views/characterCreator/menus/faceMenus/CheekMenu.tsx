import { AttributeMenu, IAttributeMenuConfigItem } from "./AttributeMenu";

const CheekMenuItems: IAttributeMenuConfigItem[] = [
	{
		id: 'cheekWidth',
		default: 5,
		max: 10,
		min: 0,
		label: 'cheek.width'
	},
	{
		id: 'cheekBoneHeight',
		default: 5,
		max: 10,
		min: 0,
		label: 'cheek.width'
	},
	{
		id: 'cheekBoneWidth',
		default: 5,
		max: 10,
		min: 0,
		label: 'cheek.width'
	}
]

export interface ICheekMenuProps {
	onCheekChanged: (cheekChanged: {id: string, value: number}) => void;
}

export const CheekMenu = ({onCheekChanged}: ICheekMenuProps) => <AttributeMenu attributes={CheekMenuItems} onAttributeChanged={onCheekChanged} />
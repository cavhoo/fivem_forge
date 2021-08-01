import { AttributeMenu, IAttributeMenuConfigItem } from "./AttributeMenu";

const CheekMenuItems: IAttributeMenuConfigItem[] = [
	{
		id: 'cheekwidth',
		default: 5,
		max: 10,
		min: 0,
		label: 'cheek.width'
	},
	{
		id: 'cheekboneheight',
		default: 5,
		max: 10,
		min: 0,
		label: 'cheek.width'
	},
	{
		id: 'cheekbonewidth',
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
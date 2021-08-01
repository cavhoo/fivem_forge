import { AttributeMenu, IAttributeMenuConfigItem } from "./AttributeMenu";

const NoseMenuConfig: IAttributeMenuConfigItem[] = [
	{
		id: "width",
		label: "nose.width",
		max: 10,
		min: 0,
		default: 5,
	},
	{
		id: 'length',
		label: "nose.length",
		max: 10,
		min: 0,
		default: 5,
	},
	{
		id: 'height',
		label: "nose.height",
		max: 10,
		min: 0,
		default: 5,
	},
	{
		id: 'lowering',
		label: "nose.lowering",
		max: 10,
		min: 0,
		default: 5,
	},
	{
		id: 'bonebend',
		label: "nose.bonebend",
		max: 10,
		min: 0,
		default: 5,
	},
	{
		id: 'boneoffset',
		label: "nose.boneoffset",
		max: 10,
		min: 0,
		default: 5,
	},

];

export interface INoseMenuProps {
	onNoseChanged: (noseChanged: { id: string, value: number }) => void;

}
export const NoseMenu = ({onNoseChanged}:INoseMenuProps) => (
	<AttributeMenu attributes={NoseMenuConfig} onAttributeChanged={onNoseChanged} />
);

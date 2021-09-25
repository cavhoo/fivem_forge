import { INoseData } from "../../../../models";
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
		id: 'tipLength',
		label: "nose.length",
		max: 10,
		min: 0,
		default: 5,
	},
	{
		id: 'tipHeight',
		label: "nose.height",
		max: 10,
		min: 0,
		default: 5,
	},
	{
		id: 'tipLength',
		label: "nose.lowering",
		max: 10,
		min: 0,
		default: 5,
	},
	{
		id: 'boneBend',
		label: "nose.bonebend",
		max: 10,
		min: 0,
		default: 5,
	},
	{
		id: 'boneOffset',
		label: "nose.boneoffset",
		max: 10,
		min: 0,
		default: 5,
	},

];

export interface INoseMenuProps {
	noseData: INoseData;
	onNoseChanged: (noseChanged: { id: string, value: number }) => void;

}
export const NoseMenu = ({onNoseChanged}:INoseMenuProps) => (
	<AttributeMenu attributes={NoseMenuConfig} onAttributeChanged={onNoseChanged} />
);

import { Color } from "../../../components/colors/ColorPicker";
import { AttributeMenu, IAttributeMenuConfigItem } from "./AttributeMenu";

export interface IHairMenuProps {
    attributes: IAttributeMenuConfigItem[];
    hairColors: Color[];
    onHairStyleChanged: (hairStyle: { id: string, value: number }) => void;
}

export const HairMenu = ({
    attributes,
    onHairStyleChanged,
}: IHairMenuProps) => {
    return (
        <>
            <AttributeMenu attributes={attributes} onAttributeChanged={onHairStyleChanged} />
        </>
    );
}
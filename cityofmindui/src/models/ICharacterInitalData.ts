import {IClothes} from "./IClothes";
import {IRawColor} from "./Colors";

export interface ICharacterCreatorInitialData {
	moms: string[];
	dads: string[];
	gender: string;
	makeUpColors: IRawColor[];
	hairColors: IRawColor[];
	makeUpVariants: string[];
	tattooColors: string[];
	tattooVariants: number;
	beardStyles: string[];
	femaleHairStyles: string[];
	maleHairStyles: string[];
	clothes: IClothes;
}

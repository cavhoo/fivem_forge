export interface ICharacterCreatorInitialData {
	moms: string[];
	dads: string[];
	gender: string;
	makeUpColors: { Index: number, Red: number, Green: number, Blue: number }[];
	hairColors: { Index: number, Red: number, Green: number, Blue: number}[];
	makeUpVariants: string[];
	tattooColors: string[];
	tattooVariants: number;
	beardStyles: string[];
	femaleHairStyles: string[];
	maleHairStyles: string[];
	clothes: {
		torso: string[],
		hats: string[],
		shoes: string[],
		pants: string[],
		glasses: string[],
		masks: string[],
		bags: string[],
	}
}

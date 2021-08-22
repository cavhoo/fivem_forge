import {ICheekData, IChinData, IEyeData, INoseData} from "./face";
import {ICharacterClothing} from "./ICharacterClothing";

export interface ICharacter {
    firstname: string;
    lastname: string;
    birthdate: string;
    nationality: string;
    gender: string;
    mom: number;
    dad: number;
    skinFactor: number;
    faceFactor: number;
    chin: IChinData;
    eyes: IEyeData;
    cheeks: ICheekData;
    nose: INoseData;
    lipThickness: number;
    lipStickColor: number;
    lipStickVariant: number;
    tattoo: number;
    makeUpVariant: number;
    makeUpColor: number;
    hairStyle: number;
    hairBaseColor: number;
    hairHighlightColor: number;
    beardStyle: number;
    beardColor: number;
    chestHairVariant: number;
    chestHairColor: number;
    blushColor: number;
    blushVariant: number;
    clothes: ICharacterClothing;
}
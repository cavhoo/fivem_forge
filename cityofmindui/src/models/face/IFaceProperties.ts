import {ICheekData, IChinData, IEyeData, INoseData} from ".";

export interface IFaceProperties {
    eyes: IEyeData;
    nose: INoseData;
    cheeks: ICheekData;
    chin: IChinData;
    lipThickness: number;
    hairStyle: {
        style: number,
        baseColor: number,
        highlightColor: number
    }
}

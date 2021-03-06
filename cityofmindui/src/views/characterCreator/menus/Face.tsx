import { Button, Container, Grid, Typography } from "@material-ui/core";
import { useDebugValue, useEffect, useState } from "react";
import { Color } from "../../components/colors/ColorPicker";
import { CheekMenu } from "./faceMenus/CheekMenu";
import { EyeMenu } from "./faceMenus/EyeMenu";
import { HairMenu } from "./faceMenus/HairMenu";
import { LipsMenu } from "./faceMenus/LipsMenu";
import { NoseMenu } from "./faceMenus/NoseMenu";
import {IEyeData, INoseData, ICheekData, IChinData} from "../../../models";

enum FaceSubmenu {
	Main,
	Nose,
	Cheeks,
	Eyes,
	Lips,
	Hair,
}
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

export interface IFaceProps {
	browColors: Color[];
	hairColors: Color[];
	onFaceUpdated: (data: IFaceProperties) => void;
	selectedGender: string;
}



export const Face = ({cheeks, chin, eyes,nose, onFaceUpdated, browColors, hairStyle, hairColors, selectedGender }: IFaceProperties & IFaceProps) => {
	const [activeSubmenu, setActiveSubmenu] = useState(FaceSubmenu.Main);
	const [cheekData, setCheekData] = useState(cheeks);
	const [noseData, setNoseData] = useState(nose);
	const [eyeData, setEyeData] = useState(eyes);
	const [chinData, setChinData] = useState(chin);
	const [hairData, setHairData] = useState(hairStyle);
	const [lipThickness, setLipThickness] = useState(0.5);

	const setMenuActive = (menuId: FaceSubmenu) => {
		setActiveSubmenu(menuId);
	}

	const onNoseDataChanged = ({id, value}: {id:string, value: number}) => {
		setNoseData({
			...noseData,
			[id]: value,
		});
		onFaceUpdated({nose: {...noseData, [id]: value}, eyes: eyeData, cheeks: cheekData, chin: chinData, lipThickness, hairStyle: hairData});
	}

	const onEyeDataChanged = ({id, value}: {id:string, value: number}) => {
		setEyeData({
			...eyeData,
			[id]: value,
		});
		onFaceUpdated({nose: noseData, eyes: {...eyeData, [id]: value}, cheeks: cheekData, chin: chinData, lipThickness, hairStyle: hairData});
	}

	const onCheekDataChanged = ({id, value}: {id: string, value: number}) => {
		setCheekData({
			...cheekData,
			[id]: value,
		});
		onFaceUpdated({nose: noseData, eyes: eyeData, cheeks: {...cheekData, [id]: value}, chin: chinData, lipThickness, hairStyle: hairData});
	}

	const onChinDataChanged = ({id, value}: {id: string, value: number}) => {
		setChinData({
			...chinData,
			[id]:value,
		});
		onFaceUpdated({nose: noseData, eyes: eyeData, cheeks: cheekData, chin: {...chinData, [id]: value}, lipThickness, hairStyle: hairData});
	}

	const onHairStyleChanged = (style: number, baseColor: number, highlightColor: number) => {
		setHairData({
			style,
			baseColor,
			highlightColor
		})
		onFaceUpdated({ nose: noseData, eyes: eyeData, cheeks: cheekData, chin: chinData, lipThickness, hairStyle: {style, baseColor, highlightColor}})
	}

	const onLipsChanged = (value:number) => {
		setLipThickness(value);
	};


	return (
		<Container>
			<Typography variant="h5" align="center">
				Face
			</Typography>
			{
				activeSubmenu === FaceSubmenu.Main && (
					<Grid container spacing={2}>
						<Grid item xs={6}>
							<Button color="primary" variant="contained" fullWidth onClick={() => setMenuActive(FaceSubmenu.Nose)}>
								<Typography>Nose</Typography>
							</Button>
						</Grid>
						<Grid item xs={6}>
							<Button color="primary" variant="contained" fullWidth onClick={() => setMenuActive(FaceSubmenu.Cheeks)}>
								<Typography>Cheeks</Typography>
							</Button>
						</Grid>
						<Grid item xs={6}>
							<Button color="primary" variant="contained" fullWidth onClick={() => setMenuActive(FaceSubmenu.Eyes)}>
								<Typography>Eyes</Typography>
							</Button>
						</Grid>
						<Grid item xs={6}>
							<Button color="primary" variant="contained" fullWidth onClick={() => setMenuActive(FaceSubmenu.Lips)}>
								<Typography>Lips</Typography>
							</Button>
						</Grid>
						<Grid item xs={12}>
							<Button color="primary" variant="contained" fullWidth onClick={() => setMenuActive(FaceSubmenu.Hair)}>
								<Typography>Hair</Typography>
							</Button>
						</Grid>
					</Grid>
				)
			}
			{
				activeSubmenu === FaceSubmenu.Nose && (
					<>
						<NoseMenu noseData={noseData} onNoseChanged={onNoseDataChanged} />
					</>
				)
			}
			{
				activeSubmenu === FaceSubmenu.Eyes && (
					<>
						<EyeMenu onEyeChanged={onEyeDataChanged} browColors={browColors} browColor={browColors[0]} />
					</>
				)
			}
			{
				activeSubmenu === FaceSubmenu.Lips && (
					<>
						<LipsMenu onLipsChanged={onLipsChanged} />
					</>
				)
			}
			{
				activeSubmenu === FaceSubmenu.Cheeks && (
					<>
						<CheekMenu onCheekChanged={onCheekDataChanged} />
					</>
				)
			}
			{
				activeSubmenu === FaceSubmenu.Hair && (
					<>
						<HairMenu 
							onHairStyleChanged={onHairStyleChanged} 
							hairColors={hairColors}
							selectedGender={selectedGender}
							selectedHairStyle={hairData.style}
							selectedHairBaseColor={hairData.baseColor}
							selectedHairHighlightColor={hairData.highlightColor}
						/>
					</>
				)
			}
			{
				activeSubmenu !== FaceSubmenu.Main && (
					<Button fullWidth onClick={() => setMenuActive(FaceSubmenu.Main)}>
						Back
					</Button>
				)
			}
			
		</Container>
	);
};

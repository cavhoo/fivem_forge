import { Button, Container, Grid, Typography } from "@material-ui/core";
import { useEffect, useState } from "react";
import { runNuiCallback } from "../../../utils/fetch";
import { CheekMenu } from "./faceMenus/CheekMenu";
import { EyeMenu } from "./faceMenus/EyeMenu";
import { LipsMenu } from "./faceMenus/LipsMenu";
import { NoseMenu } from "./faceMenus/NoseMenu";

export interface ICheekData {
	cheekBoneWidth: number;
	cheekBoneHeight: number;
	cheekWidth: number;
}
export interface IEyeData {
	color: number;
	browHeight: number;
	browBulkiness: number;
	opening: number;
}

export interface INoseData {
	width: number;
	tipLength: number;
	tipHeight: number;
	tipLowering: number;
	boneBend: number;
	boneOffset: number;
}

export interface IChinData {
	width: number;
	forward: number;
	height: number;
	gapSize: number;
}

export interface IFaceProperties {
	eyes: IEyeData;
	nose: INoseData;
	cheeks: ICheekData;
	chin: IChinData;
	lipThickness: number;
}

enum FaceSubmenu {
	Main,
	Nose,
	Cheeks,
	Eyes,
	Lips,
}

export const Face = ({cheeks, chin, eyes,nose }: IFaceProperties) => {
	const [activeSubmenu, setActiveSubmenu] = useState(FaceSubmenu.Main);

	useEffect(() => {
		var focusFace = async () => {
			await runNuiCallback("highlightBodyPart", { bodypart: "Face" })
		};
		focusFace();
	})

	const setMenuActive = (menuId: FaceSubmenu) => {
		setActiveSubmenu(menuId);
	}

	const onAttributeChanged = ({ id, value }: { id: string, value: number }) => {
		switch (id) {
			default:
				console.log(id, value);
		}
	}

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
					</Grid>
				)
			}
			{
				activeSubmenu === FaceSubmenu.Nose && (
					<>
						<NoseMenu onNoseChanged={onAttributeChanged} />
					</>
				)
			}
			{
				activeSubmenu === FaceSubmenu.Eyes && (
					<>
						<EyeMenu onEyeChanged={onAttributeChanged} />
					</>
				)
			}
			{
				activeSubmenu === FaceSubmenu.Lips && (
					<>
						<LipsMenu onLipsChanged={onAttributeChanged} />
					</>
				)
			}
			{
				activeSubmenu === FaceSubmenu.Cheeks && (
					<>
						<CheekMenu onCheekChanged={onAttributeChanged} />
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

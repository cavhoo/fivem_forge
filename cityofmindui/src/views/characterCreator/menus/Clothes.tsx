import { Button, Grid, Typography } from "@material-ui/core"
import { useState } from "react"
import { Feet } from "./clothMenus/Feet"
import { Head } from "./clothMenus/Head"
import { Legs } from "./clothMenus/Legs"
import { Torso } from "./clothMenus/Torso"

enum ClothingSubMenus {
	Main,
	Head,
	Torso,
	Legs,
	Feet,
}

export interface IClothesProps {
	torsoItems: string[];
	headItems: string[];
	legItems: string[];
	feetItems: string[];
	
}

export const Clothes = () => {
	const [subMenuOpen, setSubMenuOpen] = useState(ClothingSubMenus.Main)

	const changeMenu = (menu: ClothingSubMenus) => {
		setSubMenuOpen(menu);
	}

	return (
		<>
		<Typography variant="h5" align="center">Clothes</Typography>
			{
				subMenuOpen === ClothingSubMenus.Main && (
					<Grid container xs={12} spacing={2}>
						<Grid item xs={6}>
							<Button
								fullWidth
								color="primary"
								variant="contained"
								onClick={() => changeMenu(ClothingSubMenus.Head)}
							>
								<Typography>Head</Typography>
							</Button>
						</Grid>
						<Grid item xs={6}>
							<Button
								fullWidth
								color="primary"
								variant="contained"
								onClick={() => changeMenu(ClothingSubMenus.Torso)}
							>
								<Typography>Torso</Typography>
							</Button>
						</Grid>
						<Grid item xs={6}>
							<Button
								fullWidth
								color="primary"
								variant="contained"
								onClick={() => changeMenu(ClothingSubMenus.Legs)}
							>
								<Typography>Legs</Typography>
							</Button>
						</Grid>
						<Grid item xs={6}>
							<Button
								fullWidth
								color="primary"
								variant="contained"
								onClick={() => changeMenu(ClothingSubMenus.Feet)}
							>
								<Typography>Feet</Typography>
							</Button>
						</Grid>
					</Grid>
				)
			}
			{
				subMenuOpen === ClothingSubMenus.Head && (
					<>
						<Head />
					</>
				)
			}
			{
				subMenuOpen === ClothingSubMenus.Torso && (
					<>
						<Torso />
					</>
				)
			}
			{
				subMenuOpen === ClothingSubMenus.Legs && (
					<>
						<Legs />
					</>
				)
			}
			{
				subMenuOpen === ClothingSubMenus.Feet && (
					<>
						<Feet />
					</>
				)
			}
			{
				subMenuOpen !== ClothingSubMenus.Main && (
					<Grid container>
						<Grid item>
							<Button onClick={() => changeMenu(ClothingSubMenus.Main)}>
								<Typography>Back</Typography>
							</Button>
						</Grid>
					</Grid>
				)
			}
		</>
	)
}
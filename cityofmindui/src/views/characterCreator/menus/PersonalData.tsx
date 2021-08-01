import { Button, FormControl, Grid, InputLabel, makeStyles, MenuItem, Select, TextField, Typography } from "@material-ui/core"
import { DatePicker } from "@material-ui/pickers";

const usePersonalDataStyles = makeStyles({
	createButton: {
		top: "15vh",
	}
})
export const PersonalData = () => {
	const classes = usePersonalDataStyles();
	return (
		<>
			<Typography variant="h5" align="center">Personal Data</Typography>
			<Grid container spacing={2}>
				<Grid item xs={6}>
					<Typography>Firstname:</Typography>
					<TextField />
				</Grid>
				<Grid item xs={6}>
					<Typography>Lastname:</Typography>
					<TextField />
				</Grid>
				<Grid item xs={12}>
					<Typography>Age</Typography>
					<DatePicker
						fullWidth
						value={"01.07.2021"}
						onChange={(date) => console.log(date)}
					/>
				</Grid>
				<Grid item xs={12}>
					<Typography>Nationality:</Typography>
					<Select
						fullWidth
						labelId="demo-simple-select-label"
						id="demo-simple-select"
						value={"german"}
						onChange={(evt) => console.log(evt.target.value)}
					>
						<MenuItem value={"german"}>German</MenuItem>
						<MenuItem value={"french"}>French</MenuItem>
						<MenuItem value={"austrian"}>Austrian</MenuItem>
					</Select>
				</Grid>
				<Grid item xs={12}>
					<Button
						className={classes.createButton}
						fullWidth
						color="primary"
						variant="contained"
					>
						Create
					</Button>
				</Grid>
			</Grid>
		</>
	)
}
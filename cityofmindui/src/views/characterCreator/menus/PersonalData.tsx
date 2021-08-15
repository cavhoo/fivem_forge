import { Button, FormControl, Grid, InputLabel, makeStyles, MenuItem, Select, Switch, TextField, Typography } from "@material-ui/core"
import { DatePicker } from "@material-ui/pickers";

const usePersonalDataStyles = makeStyles({

});

export interface IPersonalData {
	firstname: string;
	lastname: string;
	nationality: string;
	birthdate: string;
	gender: string;
	onUpdatePersonalData: (data: IPersonalDataChanged) => void;
}


export type IPersonalDataChanged = Omit<IPersonalData, "onUpdatePersonalData">;


export const PersonalData = ({firstname, lastname, nationality, birthdate, gender, onUpdatePersonalData}: IPersonalData) => {
	const classes = usePersonalDataStyles();
	return (
		<>
			<Typography variant="h5" align="center">Personal Data</Typography>
			<Grid container spacing={2}>
				<Grid item xs={12}>
					<Typography>Gender:</Typography>
					<Switch
						checked={gender === "female"}
						onChange={(evt, checked) => onUpdatePersonalData({firstname, lastname, nationality, birthdate, gender: checked ? "female" : "male"})}
					/>
				</Grid>
				<Grid item xs={6}>
					<Typography>Firstname:</Typography>
					<TextField 
						onChange={(evt) => onUpdatePersonalData({ firstname: evt.target.value, lastname, nationality, birthdate, gender})}
					/>
				</Grid>
				<Grid item xs={6}>
					<Typography>Lastname:</Typography>
					<TextField 
						onChange={(evt) => onUpdatePersonalData({lastname: evt.target.value, firstname, nationality, birthdate, gender})}
					/>
				</Grid>
				<Grid item xs={12}>
					<DatePicker
						fullWidth
						disableFuture
						openTo="year"
						format="dd/MM/yyyy"
						views={["year", "month", "date"]}
						value={birthdate}
						label={"Birthdate"}
						onChange={(date) => onUpdatePersonalData({firstname, lastname, nationality, birthdate: date?.toDateString() ?? Date.toString(), gender})}
					/>
				</Grid>
				<Grid item xs={12}>
					<Typography>Nationality:</Typography>
					<Select
						fullWidth
						labelId="demo-simple-select-label"
						id="demo-simple-select"
						value={nationality}
						onChange={(evt) => onUpdatePersonalData({firstname, lastname, nationality: evt.target.value as string, gender, birthdate})}
					>
						<MenuItem value={"german"}>German</MenuItem>
						<MenuItem value={"french"}>French</MenuItem>
						<MenuItem value={"austrian"}>Austrian</MenuItem>
					</Select>
				</Grid>
	
			</Grid>
		</>
	)
}
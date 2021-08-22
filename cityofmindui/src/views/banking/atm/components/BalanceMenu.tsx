import {makeStyles, TextField, Typography, withStyles} from "@material-ui/core";
import {MenuButton} from "./MenuButton";
import {AtmMenu} from "./MainMenu";
import {AtmTextField} from "./AtmTextField";

export interface BalanceMenuProps  {
    balance: number;
    currency: string;
    onBack: () => void;
}

const useStyles = makeStyles({
    root: {
        display: "grid",
        gridTemplateColumns:"49% 49%",
        gridTemplateRows: "70px 70px 70px 70px",
        gridColumnGap: "15px",
        gridRowGap: "10px",
        position: "absolute",
        width: "100%",
        bottom: "10px",
    },
    balanceTitle: {
        color: "whitesmoke"
    },
    balanceAmount: {
        color: "whitesmoke"
    }
})

export const BalanceMenu = ({onBack, balance, currency}: BalanceMenuProps) => {
    const classes = useStyles();
    
    return (
        <div className={classes.root}>
            <div style={{gridColumn: "span 2", gridRow: "span 2", textAlign: "center"}}>
                <Typography variant="h4" className={classes.balanceTitle}>:Current Balance</Typography>
                <AtmTextField className={classes.balanceAmount} id="available-amount" value={(balance / 100).toFixed(2)} disabled variant="outlined" InputProps={{endAdornment: currency}}/>
            </div>
            <MenuButton onClick={onBack} label={"Exit"} style={{gridRow: 4}}/>
        </div>
    )
}
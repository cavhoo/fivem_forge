import {makeStyles, TextField, Typography} from "@material-ui/core";
import {MenuButton} from "./MenuButton";
import {ChangeEvent, useEffect, useState} from "react";
import {AtmTextField} from "./AtmTextField";

export interface WithdrawMenuProps {
    currentBalance: number;
    currency: string;
    onWithdrawAmount: (amount: number) => void;
    goBack: () => void;
}

const useStyles = makeStyles({
    root: {
        display: "grid",
        gridTemplateColumns: "49% 49%",
        gridTemplateRows: "70px 70px 70px 70px",
        gridColumnGap: "15px",
        gridRowGap: "10px",
        position: "absolute",
        width: "100%",
        bottom: "10px",
    },
    label: {
        color: "whitesmoke",
    }
})

export const WithdrawMenu = ({currentBalance, currency, onWithdrawAmount, goBack}: WithdrawMenuProps) => {
    const [withdrawAmount, setWithdrawAmount] = useState("0.00");
    const classes = useStyles();

    const handleWithdrawAmountChanged = (event: ChangeEvent<HTMLInputElement>) => {
        const {value} = event.target;
        if (Number(value) * 100 > currentBalance) {
            setWithdrawAmount((currentBalance / 100).toFixed(2));
        } else {
            setWithdrawAmount(value);
        }
    }
    
    useEffect(() => {
        console.log(withdrawAmount);
    }, [withdrawAmount])

    return (
        <div className={classes.root}>
            <div style={{gridColumn: "span 2", textAlign: "center"}}>
                <AtmTextField type="number" id="available-amount" value={(currentBalance / 100.0).toFixed(2)} disabled label="Available Balance" variant="outlined"
                           onChange={handleWithdrawAmountChanged} InputProps={{endAdornment: currency}}/>
            </div>
            <div style={{gridColumn: "span 2", textAlign: "center"}}>
                <AtmTextField id="outlined-basic" label="Withdraw Amount" variant="outlined"
                              value={withdrawAmount}
                           onChange={handleWithdrawAmountChanged} InputProps={{endAdornment: currency}}/>
            </div>

            <MenuButton label={"Back"} onClick={goBack} style={{ gridRow: 4}}/>
            <MenuButton label={"Withdraw"} onClick={() => onWithdrawAmount(Number(withdrawAmount))} style={{gridRow: 4}}/>
        </div>
    )
}
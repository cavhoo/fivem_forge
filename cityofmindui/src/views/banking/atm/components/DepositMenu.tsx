import {makeStyles} from "@material-ui/core";
import {MenuButton} from "./MenuButton";
import {AtmTextField} from "./AtmTextField";
import {ChangeEvent, useState} from "react";

const useStyles = makeStyles({
    root: {
      width: "100%",
      height: "100%",
    },
    deposit: {
      marginTop: "50px",  
    },
    grid: {
        display: "grid",
        gridTemplateColumns: "49% 49%",
        gridTemplateRows: "70px",
        gridColumnGap: "15px",
        gridRowGap: "10px",
        position: "absolute",
        width: "100%",
        bottom: "10px",
    },
    label: {
        color: "whitesmoke",
    }
});

export interface DepositMenuProps {
    onBack: () => void;
    onDeposit: (amount: number) => void;
    currency: string;
}

export const DepositMenu = ({onBack, currency, onDeposit}: DepositMenuProps) => {
    const [depositAmount, setDepositAmount] = useState("0");
    const classes = useStyles();

    const handleDepositAmountChanged = (event: ChangeEvent<HTMLInputElement>) => {
        setDepositAmount(event.target.value);
    }
    
    const handleDepositConfirm = () => {
        onDeposit(Number(depositAmount) * 100);
    }
    
    return (
        <div>
            <div className={classes.deposit} style={{ textAlign: "center"}}>
                <AtmTextField id="outlined-basic" label="Deposit Amount" variant="outlined"
                              value={depositAmount}
                              onChange={handleDepositAmountChanged} InputProps={{endAdornment: currency}}/>
            </div>
            <div className={classes.grid}>
                <MenuButton label={"Back"} onClick={() => onBack()}/>
                <MenuButton label={"Deposit"} onClick={() => handleDepositConfirm()}/>
            </div>
        </div>
    )
}
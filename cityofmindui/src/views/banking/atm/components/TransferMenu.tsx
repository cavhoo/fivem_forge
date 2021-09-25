import {makeStyles} from "@material-ui/core";
import {AtmTextField} from "./AtmTextField";
import {ChangeEvent, useState} from "react";
import {MenuButton} from "./MenuButton";

export interface TransferMenuProps {
    currentBalance: number;
    onTypeAccountOwner: (owner: string) => void;
    onTransferConfirmed: (accountOwner: string, accountNumber: string, message: string, amount: number) => void;
    onBack: () => void;
}

const useStyles = makeStyles({
    form: {
        display: "flex",
        flexDirection: "column",
        "& *": {
            margin: "0 5px 5px 5px",

        }
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
})
export const TransferMenu = ({currentBalance, onTypeAccountOwner, onTransferConfirmed, onBack}: TransferMenuProps) => {
    const [accountOwner, setAccountOwner] = useState("");
    const [accountNumber, setAccountNumber] = useState("");
    const [message, setMessage] = useState("");
    const [amount, setAmount] = useState(0);

    const classes = useStyles();

    const handleOwnerChange = (event: ChangeEvent<HTMLInputElement>) => {
        setAccountOwner(event.target.value);
    }
    const handleAccountNumberChange = (event: ChangeEvent<HTMLInputElement>) => {
        setAccountNumber(event.target.value);
    }

    const handleMessageChange = (event: ChangeEvent<HTMLInputElement>) => {
        setMessage(event.target.value);
    }
    const handleAmountChange = (event: ChangeEvent<HTMLInputElement>) => {
        setAmount(Number(event.target.value) * 100);
    }

    return (
        <div>
            <div className={classes.form}>
                <AtmTextField variant="outlined" label={"Recipient Name"} value={accountOwner}
                              onChange={handleOwnerChange}/>
                <AtmTextField variant="outlined" label={"Recipient Acount"} value={accountNumber}
                              onChange={handleAccountNumberChange}
                              disabled={accountOwner.length === 0}/>
                <AtmTextField variant="outlined" label={"Message"} value={message} onChange={handleMessageChange}
                              disabled={accountNumber.length === 0}/>
                <AtmTextField variant="outlined" label={"Amount"} value={(amount / 100).toFixed(2)} onChange={handleAmountChange}
                              disabled={accountNumber.length === 0}/>
            </div>
            <div className={classes.grid}>
                <MenuButton label={"Back"} onClick={onBack}/>
                <MenuButton label={"Transfer"} onClick={onBack}/>
            </div>
        </div>
    )
}
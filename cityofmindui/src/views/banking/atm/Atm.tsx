import {makeStyles, Typography} from "@material-ui/core";
import {useEffect, useState} from "react";
import {AtmMenu, BalanceMenu, MainMenu, TransferMenu, WithdrawMenu} from "./components";
import {DepositMenu} from "./components/DepositMenu";
import {runNuiCallback} from "../../../utils/fetch";

export interface AtmProps {
    bankName: string;
    accountOwner: string;
    currentBalance: string;
    withdrawableAmounts: number[];
    onWithDraw: (amount: number) => void;
    onTransfer: (amount: number, target: string) => void;
}

const useStyles = makeStyles({
    root: {
        margin: "0 auto",
        width: "1000px",
        height: "768px",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        position: "relative",
        userSelect: "none",
        backdropFilter: "blur(5px)",
    },
    bankName: {
        fontWeight: "bold",
        textAlign: "center",
        margin: 0,
        color: "whitesmoke"
    },
    frame: {
        backgroundImage: "url(assets/atmframe.png)",
        backgroundRepeat: "no-repeat",
        backgroundSize: "cover",
        width: "100%",
        height: "100%",
        position: "absolute"
    },
    content: {
        width: "700px",
        height: "530px",
        backgroundColor: "blue",
        overflow: "hidden",
        position: "relative"
    }
})

export interface IAccountInformation {
    accountOwner: string;
    accountNumber: string;
    saldo: number;
    withdrawableAmount: number[];
}


export const Atm = () => {
    const [activeMenu, setActiveMenu] = useState(AtmMenu.Main);
    const [account, setAccount] = useState({} as IAccountInformation);
    const classes = useStyles();
    
    useEffect(() => {
        runNuiCallback("atmMachine", {getAccount: true}).then( async (resp) => {
            const json = await resp.json();
            setAccount(json);
        });
    }, []);
    
    const handleMenuSelect = async (menu: number) => {
        if (menu === AtmMenu.Main) {
            await runNuiCallback("atmMachine", { close: true});
        } else {
            setActiveMenu(menu);
        }
    }
    
    const handleWithDrawMoney = async (amount: number) => {
        await runNuiCallback("atmMachine", { withdraw: amount})
    }
    
    const handleDepositMoney = async (amount: number) => {
        await runNuiCallback("atmMachine", { deposit: amount})
    }
    
    const handleTransferMoney = async (accountOwner: string, accountNumber:string, message:string, amount: number) => {
        await runNuiCallback("atmMachine", { transfer: { accountNumber, accountOwner, message, amount }});
    }
    
    const handleSearchAccountOwner = async (accountOwner: string) => {
        await runNuiCallback("atmMachine", { search: accountOwner});
    }
    

    const renderAtmMenu = ():JSX.Element => {
        switch(activeMenu) {
            case AtmMenu.Main:
                return <MainMenu onFunctionSelect={handleMenuSelect} />
            case AtmMenu.Balance:
                return <BalanceMenu balance={account.saldo} currency={"Euro"} onBack={() => handleMenuSelect(AtmMenu.Main)} />
            case AtmMenu.Withdraw:
                return <WithdrawMenu currentBalance={account.saldo} currency={"Euro"} onWithdrawAmount={handleWithDrawMoney} goBack={() => handleMenuSelect(AtmMenu.Main)} />
            case AtmMenu.Transfer:
                return <TransferMenu currentBalance={account.saldo} onTransferConfirmed={handleTransferMoney} onTypeAccountOwner={handleSearchAccountOwner} onBack={() => handleMenuSelect(AtmMenu.Main)}/>
            case AtmMenu.Deposit:
                return <DepositMenu onBack={() => handleMenuSelect(AtmMenu.Main)} currency={"Euro"} onDeposit={handleDepositMoney}/>
            default:
                return <>Empty</>
        }
    }
    
    return (
        <div className={classes.root}>
            <div className={classes.frame} />
            <div className={classes.content}>
                <Typography className={classes.bankName} variant={"h2"}>City of Mind Federal Reserve</Typography>
                {renderAtmMenu()}
            </div>
        </div>
    )
}
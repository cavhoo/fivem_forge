import {MenuButton} from "./MenuButton";
import {makeStyles} from "@material-ui/core";

export enum AtmMenu {
    Main,
    Withdraw,
    Balance,
    Deposit,
    Transfer
}

export interface MainMenuProps {
    onFunctionSelect: (id: number) => void;
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
        bottom: 0,
    }
})

export const MainMenu = ({onFunctionSelect}: MainMenuProps) => {
    const classes = useStyles();
    return (
        <div className={classes.root}>
            <MenuButton onClick={() => onFunctionSelect(AtmMenu.Balance)} label={"Balance"} />
            <MenuButton onClick={() => onFunctionSelect(AtmMenu.Withdraw)} label={"Withdraw"} />
            <MenuButton onClick={() => onFunctionSelect(AtmMenu.Transfer)} label={"Transfer"} />
            <MenuButton onClick={() => onFunctionSelect(AtmMenu.Deposit)} label={"Deposit"} />
            <MenuButton onClick={() => onFunctionSelect(AtmMenu.Main)} label={"Exit"} />
        </div>
    )
}
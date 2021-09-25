import {Button, makeStyles, withStyles} from "@material-ui/core";
import {CSSProperties} from "react";


export interface MenuButtonProps {
    label: string;
    onClick: () => void;
    style?: CSSProperties;
}

const useStyles = makeStyles({
    root: {
        backgroundColor: "white",
        color: "black",
        borderRadius: 0,
    }
})

export const MenuButton = ({label, onClick, ...rest}: MenuButtonProps) => {
    const classes = useStyles();
    return (
        <Button {...rest} onClick={() => onClick()} variant={"contained"} className={classes.root}>{label}</Button>
    )
}
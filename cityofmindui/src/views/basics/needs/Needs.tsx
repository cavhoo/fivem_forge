import {makeStyles} from "@material-ui/core";
import { Wc, Fastfood, LocalDrink } from "@material-ui/icons"
import {useEffect, useState} from "react";
import {StatusIcon} from "./componets/StatusIcon";
import {runNuiCallback} from "../../../utils/fetch";
import {stat} from "fs";

export interface INeedsProps {

}

const useStyles = makeStyles({
    root: {
        display: "flex",
        position: "absolute",
    },
});


export const Needs = () => {
    const [hunger, setHunger] = useState(100);
    const [thirst, setThirst] = useState(100);
    const [pee, setPee] = useState(100);
    const [statusUpdate, setStatusUpdate] = useState(false);
   
    const classes = useStyles();
    
    const getStatusUpdate = () => {
        runNuiCallback("character/status", {}).then(async (response) => {
            const json = await response.json();
            setHunger(json.Hunger);
            setThirst(json.Thirst);
            setPee(json.Pee);
            setTimeout(() => {
                setStatusUpdate(true);
            }, 5000)
        });
    }
    
    useEffect(() => {
        getStatusUpdate();
    }, []);
    
    useEffect(() => {
        if (statusUpdate) {
            getStatusUpdate();
            setStatusUpdate(false);
        }
    }, [statusUpdate]);
    
    
    return (
        <div className={classes.root}>
            <StatusIcon max={100} current={hunger} icon={<Fastfood fontSize="small" />} size={[40, 40]} statusBarWidth={4} />
            <StatusIcon max={100} current={thirst} icon={<LocalDrink fontSize="small" />} size={[40, 40]} statusBarWidth={4} />
            <StatusIcon max={100} current={pee} icon={<Wc fontSize="small" />} size={[40, 40]} statusBarWidth={4} />
        </div>
    )
}
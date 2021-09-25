import {Box, makeStyles} from "@material-ui/core";
import React from "react";

export interface StatusIconProps {
    max: number;
    current: number;
    icon: JSX.Element;
    size: [number, number];
    statusBarWidth: number;
}

const useStyles = (circumfence: number, current: number) => makeStyles({
    root: {
        width: "40px",
        height: "40px",
        position: "relative",
        display: "flex",
        justifyContent: "center",
        alignItems: "center"
    },
    icon: {
      fontSize: "12px",  
    },
    barRoot: {
        position: "absolute",
    },
    barPath: {
        strokeDasharray: `${circumfence} ${circumfence}`,
        strokeDashoffset: current,
        transition: "stroke-dashoffset 0.35s",
        transform: "rotate(-90deg)",
        transformOrigin: "50% 50%",
    },
});


export const StatusIcon = ({max, current, icon, statusBarWidth, size}: StatusIconProps) => {
    const strokeColor = (current: number) => {
        if (current > 85) {
            return "green";
        }
        
        if (current > 60) {
            return "yellow";
        }
        
        if (current > 30) {
            return "orange";
        }
        
        if (current > 0) {
            return "red";
        }
    }
    const [width, height] = size;
    const radius = (width - 2 * statusBarWidth) / 2;
    const circumference = radius * 2 * Math.PI;
    const centerX = width / 2;
    const centerY = height / 2;
    const currentProgress = circumference - ((current / max) * circumference); 
    
    const classes = useStyles(circumference, currentProgress)();
    
    return (
        <Box className={classes.root}>
            <div className={classes.icon}>
                {icon}
            </div>
            <svg
                className={classes.barRoot}
                width="40"
                height="40"
            >
                <circle 
                    className={classes.barPath}
                    stroke={strokeColor(current)}
                    strokeWidth={statusBarWidth}
                    fill="transparent"
                    r={radius}
                    cx={centerX}
                    cy={centerY}
                />
            </svg>
        </Box>
    )
}
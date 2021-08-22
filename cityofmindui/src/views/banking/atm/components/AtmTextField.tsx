import {TextField, withStyles} from "@material-ui/core";

export const AtmTextField = withStyles({
    root: {
        "& .MuiInputLabel-outlined": {
            color: "whitesmoke",
        },
        "& label.Mui-disabled": {
            color: "whitesmoke",
        },
        '& label.Mui-focused': {
            color: 'whitesmoke',
        },
        '& .MuiInput-underline:after': {
            borderBottomColor: 'green',
        },
        '& .MuiOutlinedInput-root': {
            color: "whitesmoke",
            "&.Mui-disabled": {
                color: "whitesmoke",
            },
            '&.Mui-disabled fieldset': {
                borderColor: "whitesmoke",
            },
            '& fieldset': {
                borderColor: 'white',
            },
            '&:hover fieldset': {
                borderColor: 'green',
            },
            '&.Mui-focused fieldset': {
                borderColor: 'green',
            },
        },
    },
})(TextField);
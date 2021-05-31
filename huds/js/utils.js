/**
 * Returns the current maximum width of the client.
 * @returns number The width of the window.
 */
const getClientWidth = () => document.documentElement.clientWidth;

/**
 * Returns the current maximum height of the client.
 * @returns number The height of the window.
 */
const getClientHeight = () => document.documentElement.clientHeight;

/**
 * 
 * @param {{top, right, bottom, left}} param0 Padding object. 
 * @returns {x, y} The position with padding applied.
 */
const positionWithPadding = ({top, right, bottom, left}) => {


    return {x: 0, y: 0};
}
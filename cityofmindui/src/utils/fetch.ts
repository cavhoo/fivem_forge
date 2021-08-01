declare let GetParentResourceName:any;

export const runNuiCallback = <T>(api:string, payload:T) => fetch(`https://${GetParentResourceName()}/${api}`, {
	method: 'POST',
	headers: {
	  'Content-Type': 'application/json; charset=UTF-8',
	},
  body: JSON.stringify(payload)
})

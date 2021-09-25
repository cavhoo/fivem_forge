export const runNuiCallback = <T>(api:string, payload:T) => fetch(`https://cityofmindclient/${api}`, {
	method: 'POST',
	headers: {
	  'Content-Type': 'application/json; charset=UTF-8',
	},
  body: JSON.stringify(payload)
})

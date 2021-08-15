import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react"
import { ICharacterCreatorInitialData } from "../../models/ICharacterInitalData";
declare const GetParentResourceName:any;
export const characterApi = createApi({
	reducerPath: "characterApi",
	baseQuery: fetchBaseQuery({ baseUrl: `https://${GetParentResourceName()}/character`}),
	endpoints: (builder) => ({
		getInitialData: builder.query<ICharacterCreatorInitialData, null>({
			query: () => `/initialData`,
		}),
	})
})

export const { useGetInitialDataQuery } = characterApi;
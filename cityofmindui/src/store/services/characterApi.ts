import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react"
import { ICharacterCreatorInitialData } from "../../models";
import {ICharacter} from "../../models";
declare const GetParentResourceName:any;
export const characterApi = createApi({
	reducerPath: "characterApi",
	baseQuery: fetchBaseQuery({ baseUrl: `https://${GetParentResourceName()}/character/`}),
	endpoints: (builder) => ({
		getInitialData: builder.query<ICharacterCreatorInitialData, null>({
			query: () => ({url: `initialData`, method: "POST"}),
		}),
		updateCharacter: builder.mutation({
			query: (character: ICharacter) => ({
				url: "updateCharacter",
				method: "POST",
				body: JSON.stringify(character)
			})
		}),
		createCharacter: builder.mutation({
			query:() => ({url: "createCharacter", method: "POST"})
		})
	})
})

export const { useGetInitialDataQuery, useCreateCharacterMutation, useUpdateCharacterMutation } = characterApi;